using System.Linq;
using RimWorld;
using Verse;

namespace BloodTypes
{
    public class ThoughtWorker_Apply : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (p == null) return false;

            GenerateBloodType(p);

            return false;
        }

        public static void GenerateBloodType(Pawn pawn)
        {
            if (PawnHelper.IsHaveHediff(pawn, HediffDefOf.BloodType)) return;

            bool moreThanOne = false;
            BloodType current = null;
            var parents = pawn.relations.DirectRelations.Where(x => x.def == PawnRelationDefOf.Parent);
            foreach (var relation in parents)
            {
                var bloodDiff = relation.otherPawn.GetBloodType();
                if (bloodDiff?.BloodType == null) continue;
                if (current == null)
                {
                    current = bloodDiff.BloodType;
                }
                else
                {
                    moreThanOne = true;
                    current = current.Child(bloodDiff.BloodType);
                }
            }

            if (current == null) current = BloodType.Random();
            else if (!moreThanOne)
            {
                current = current.Child();
            }


            AddBloodType(pawn, current);
        }


        private static void AddBloodType(Pawn pawn, BloodType current)
        {
            var myDef = (BloodTypeHediffWithComps) pawn.health.AddHediff(HediffDefOf.BloodType);
            myDef.BloodType = current;
        }
    }
}
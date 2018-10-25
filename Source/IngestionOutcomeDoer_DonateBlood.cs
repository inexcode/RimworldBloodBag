using System;
using RimWorld;
using Verse;

namespace BloodTypes
{
    public class IngestionOutcomeDoer_DonateBlood : IngestionOutcomeDoer
    {
        public float severity = -1f;
        public HediffDef hediffDef;
        public ChemicalDef toleranceChemical;
        private bool divideByBodySize;

        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
        {
            if (ingested!=null && ingested is BloodBagThingWithComps bag)
            {
                
                if (!pawn.GetBloodType()?.BloodType.CanGetBlood(bag?.BloodType) ?? false)
                {
                    //TODO blood incompatibility, MVP FoodPoison
                    var d = pawn?.health?.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
                    if(d!=null) d.Severity = Rand.Value / 3f;
                }
                
            }
            
            Hediff hediff = HediffMaker.MakeHediff(this.hediffDef, pawn, (BodyPartRecord) null);
            float effect = (double) this.severity <= 0.0 ? this.hediffDef.initialSeverity : this.severity;
            if (this.divideByBodySize)
                effect /= pawn.BodySize;
            AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize(pawn, this.toleranceChemical, ref effect);
            hediff.Severity = effect;
            pawn.health.AddHediff(hediff, (BodyPartRecord) null, new DamageInfo?(), (DamageWorker.DamageResult) null);
        }
    }
}
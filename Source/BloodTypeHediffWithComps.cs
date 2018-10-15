using Verse;

namespace BloodTypes
{
    public class BloodTypeHediffWithComps : HediffWithComps
    {
        public BloodType BloodType;

        public override string LabelInBrackets => BloodType?.ToString();

        public override void ExposeData()
        {
            Scribe_Deep.Look(ref BloodType, "BloodType");
            base.ExposeData();
        }
    }
}
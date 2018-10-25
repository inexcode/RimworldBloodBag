using Verse;

namespace BloodTypes
{
    public class BloodTypeHediffWithComps : HediffWithComps
    {
        public BloodType BloodType = BloodType.Random();

        public override string LabelInBrackets => BloodType?.ToString() ?? "Blood!";

        public override void ExposeData()
        {
            Scribe_Deep.Look(ref BloodType, "BloodType");
            base.ExposeData();
        }
    }
}
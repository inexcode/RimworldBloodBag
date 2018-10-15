using Verse;

namespace BloodTypes
{
    public class BloodTypeHediffWithComps : HediffWithComps
    {
        public BloodType BloodType;

        public override string LabelBase => BloodType.ToString();
    }
}
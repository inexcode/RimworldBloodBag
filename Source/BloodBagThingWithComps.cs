using Verse;

namespace BloodTypes
{
    public class BloodBagThingWithComps : ThingWithComps
    {
        public BloodType bloodType;

        public override string LabelShort =>
            base.LabelShort + (bloodType != null ? "[" + bloodType + "]" : "[Universal]");

        public override bool CanStackWith(Thing other)
        {
            return other is BloodBagThingWithComps withComps
                   && base.CanStackWith(withComps) 
                   && withComps.bloodType == bloodType;
        }
    }
}
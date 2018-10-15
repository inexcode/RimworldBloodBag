using Verse;

namespace BloodTypes
{
    public class BloodBagThingWithComps : ThingWithComps
    {
        public BloodType bloodType;

        public override string Label =>
            base.Label + (bloodType != null ? "[" + bloodType + "]" : "[Universal]");

        
        public override bool CanStackWith(Thing other)
        {
            return other is BloodBagThingWithComps withComps
                   && base.CanStackWith(withComps) 
                   && withComps?.bloodType == bloodType;
        }
    }
}
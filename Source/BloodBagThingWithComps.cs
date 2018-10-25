using Verse;

namespace BloodTypes
{
    public class BloodBagThingWithComps : ThingWithComps
    {
        public BloodType BloodType;

        public override string Label =>
            base.Label + (BloodType != null ? "[" + BloodType + "]" : "[Universal]");

        
        public override bool CanStackWith(Thing other)
        {
            return other is BloodBagThingWithComps withComps
                   && base.CanStackWith(withComps) 
                   && withComps.BloodType.Equals(BloodType);
        }
        
        public override void ExposeData()
        {
            Scribe_Deep.Look(ref BloodType, "BloodType");
            base.ExposeData();
        }
    }
}
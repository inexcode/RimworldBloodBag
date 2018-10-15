using Verse;

namespace BloodTypes
{
    public class BloodBagThingWithComps : ThingWithComps
    {
        public BloodType BloodType = BloodType.Random();

        public override string Label =>
            base.Label + (BloodType != null ? "[" + BloodType + "]" : "[Universal]");

        
        public override bool CanStackWith(Thing other)
        {
            return other is BloodBagThingWithComps withComps
                   && base.CanStackWith(withComps) 
                   && withComps?.BloodType == BloodType;
        }
        
        public override void ExposeData()
        {
            Scribe_Deep.Look(ref BloodType, "BloodType");
            base.ExposeData();
        }
    }
}
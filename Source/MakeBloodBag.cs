using System.Collections.Generic;
using RimWorld;
using Verse;

namespace BloodTypes
{
    public class MakeBloodBag : Recipe_Surgery
    {
        
        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            var bloodBag = (BloodBagThingWithComps)ThingMaker.MakeThing(ThingDefOf.BloodBag);
            bloodBag.bloodType = pawn.GetBloodType().BloodType;
            GenPlace.TryPlaceThing(bloodBag, billDoer.Position, billDoer.Map, ThingPlaceMode.Near);
            
            base.ApplyOnPawn(pawn, part, billDoer, ingredients, bill);
        }
    }
}
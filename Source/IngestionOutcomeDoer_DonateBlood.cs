using System;
using RimWorld;
using Verse;

namespace BloodTypes
{
    public class IngestionOutcomeDoer_DonateBlood : IngestionOutcomeDoer_GiveHediff
    {
        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
        {
            //TODO check bad blood
            if (ingested is BloodBagThingWithComps)
            {
                var bag = (BloodBagThingWithComps) ingested;
                if (!pawn.GetBloodType().BloodType.CanGetBlood(bag.BloodType))
                {
                    //TODO blood incompatibility, MVP FoodPoison
                    var d = pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
                    d.Severity = Rand.Value / 3f;
                }
                base.DoIngestionOutcomeSpecial(pawn, ingested);
            }
            
        }
    }
}
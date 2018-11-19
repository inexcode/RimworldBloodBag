using System.Collections.Generic;
using System.Linq;
using Harmony;
using RimWorld;
using Verse;

namespace BloodTypes.Harmony
{
    [HarmonyPatch(typeof(DefGenerator), "GenerateImpliedDefs_PreResolve")]
    public static class DefGenerator_GenerateImpliedDefs_PreResolve
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            RecipeDef DonateBlood = DefDatabase<RecipeDef>.GetNamed("DonateBlood");

            var humanoidRaces = HumanoidRaces();

            foreach (var humanoidRace in humanoidRaces)
            {
                Log.Message("Adding in draw blood");
                if (humanoidRace.recipes == null)
                    humanoidRace.recipes = new List<RecipeDef>();
                humanoidRace.recipes.Add(DonateBlood);
            }
        }

        private static IEnumerable<BodyDef> FleshBodiedRaces(IEnumerable<ThingDef> humanoidRaces)
        {
            var fleshBodies = humanoidRaces
                .Select(t => t.race.body)
                .Distinct();
            return fleshBodies;
        }

        public static IEnumerable<ThingDef> HumanoidRaces()
        {
            var fleshRaces = DefDatabase<ThingDef>
                .AllDefsListForReading
                .Where(t => t.race?.IsFlesh ?? false); // return __instance.FleshType != FleshTypeDefOf.Mechanoid;

            var humanoidRaces = fleshRaces.Where(td => td.race.Humanlike);
            return humanoidRaces;
        }
    }
}
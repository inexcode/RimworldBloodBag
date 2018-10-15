using System.Reflection;
using Harmony;
using Verse;

namespace BloodTypes.Harmony
{

    [StaticConstructorOnStartup]
    class Main : Mod
    {
        public Main(ModContentPack content) : base(content)
        {
            var harmony = HarmonyInstance.Create("BloodTypes.Harmony");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }


    

}

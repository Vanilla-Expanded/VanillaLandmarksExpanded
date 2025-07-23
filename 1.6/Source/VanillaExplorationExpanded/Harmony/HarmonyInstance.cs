using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Collections.Generic;
using RimWorld.Planet;
using System.Linq;
using System;
using RimWorld.BaseGen;



namespace VanillaExplorationExpanded
{

    

    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.VanillaExplorationExpanded");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }


    }


   



}

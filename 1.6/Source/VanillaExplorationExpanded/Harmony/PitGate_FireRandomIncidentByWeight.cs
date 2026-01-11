using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

using System;
using UnityEngine;


namespace VanillaExplorationExpanded
{

    [HarmonyPatch(typeof(PitGate))]
    [HarmonyPatch("FireRandomIncidentByWeight")]
    public static class VanillaExplorationExpanded_PitGate_FireRandomIncidentByWeight_Patch
    {
        [HarmonyPrefix]
        public static bool DetectInertPitGate(PitGate __instance)
        {
            if(__instance.def == InternalDefOf.VEE_InertPitGate)
            {

                return false;
            }
            return true;







        }


    }


}












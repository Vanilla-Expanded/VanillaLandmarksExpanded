using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

using System;
using UnityEngine;


namespace VanillaExplorationExpanded
{

    [HarmonyPatch(typeof(Plant))]
    [HarmonyPatch("GrowthRateFactor_Drought", MethodType.Getter)]
    public static class VanillaExplorationExpanded_Plant_GrowthRateFactor_Drought_Patch
    {
        [HarmonyPostfix]
        public static void DetectFertileRains(Plant __instance, ref float __result)
        {

            if (__instance.Map.gameConditionManager.ConditionIsActive(InternalDefOf.VEE_FertileRainsGameCondition))
            {
                __result = GameCondition_FertileRains.PlantGrowthRateFactor;
            }






        }


    }


}












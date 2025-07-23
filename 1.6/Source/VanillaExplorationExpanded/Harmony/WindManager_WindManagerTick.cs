using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

using System;
using UnityEngine;


namespace VanillaExplorationExpanded
{

    [HarmonyPatch(typeof(WindManager))]
    [HarmonyPatch("WindManagerTick")]
   
    public static class VanillaExplorationExpanded_WindManager_WindManagerTick_Patch
    {
        [HarmonyPostfix]
        public static void ReduceWindToZero(Map ___map,ref float ___cachedWindSpeed, ref float ___plantSwayHead)
        {
            if (___map != null)
            {
                if (___map.TileInfo?.Mutators?.Contains(InternalDefOf.VEE_NoWind) == true)
                {
                    ___cachedWindSpeed = 0f;
                    ___plantSwayHead = 0f;
                }

            }

        }


    }


}












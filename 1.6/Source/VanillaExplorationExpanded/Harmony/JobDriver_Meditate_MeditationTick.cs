using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

using System;
using UnityEngine;


namespace VanillaExplorationExpanded
{

    [HarmonyPatch(typeof(JobDriver_Meditate))]
    [HarmonyPatch("MeditationTick")]
  
    public static class VanillaExplorationExpanded_JobDriver_Meditate_MeditationTick_Patch
    {
        [HarmonyPostfix]
        public static void AddGrass(JobDriver_Meditate __instance)
        {
            if (MeditationFocusDefOf.Natural.CanPawnUse(__instance.pawn))
            {
                int num = GenRadial.NumCellsInRadius(MeditationUtility.FocusObjectSearchRadius);
                for (int i = 0; i < num; i++)
                {
                    IntVec3 c = __instance.pawn.Position + GenRadial.RadialPattern[i];
                    if (c.InBounds(__instance.pawn.Map))
                    {
                        Plant plant = c.GetPlant(__instance.pawn.Map);
                        if (plant != null && plant.def == InternalDefOf.VEE_Plant_TreeAnima_Ancient)
                        {
                            plant.TryGetComp<CompSpawnSubplant>()?.AddProgress(JobDriver_Meditate.AnimaTreeSubplantProgressPerTick);
                        }
                    }
                }
            }

        }


    }


}












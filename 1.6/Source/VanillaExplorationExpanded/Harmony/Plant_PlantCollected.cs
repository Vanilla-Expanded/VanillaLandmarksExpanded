using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

using System;
using UnityEngine;


namespace VanillaExplorationExpanded
{

    [HarmonyPatch(typeof(Plant))]
    [HarmonyPatch("PlantCollected")]
    public static class VanillaExplorationExpanded_Plant_PlantCollected_Patch
    {
        [HarmonyPrefix]
        public static void AddSecondaryOutput(Plant __instance, Pawn by)
        {
            foreach (TileMutatorDef mutator in __instance.Map.TileInfo.Mutators)
            {
                if (mutator.Worker != null && mutator.Worker is TileMutatorWorker_PoisonousFlora)
                {
                    float sev = 0.05f;
                    sev *= Mathf.Max(1f - by.GetStatValue(StatDefOf.ToxicResistance), 0f);
                    if (ModsConfig.BiotechActive)
                    {
                        sev *= Mathf.Max(1f - by.GetStatValue(StatDefOf.ToxicEnvironmentResistance), 0f);
                    }
                   
                    if (sev != 0f)
                    {
                        Effecter damageEffecter = InternalDefOf.Impact_Toxic.Spawn();
                        damageEffecter.Trigger(by, by);
                        damageEffecter.Cleanup();
                        HealthUtility.AdjustSeverity(by, HediffDefOf.ToxicBuildup, sev);
                       
                    }

                }
            }

                   


     


        }


    }


}












using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

using System;
using UnityEngine;


namespace VanillaExplorationExpanded
{

    [HarmonyPatch(typeof(HediffGiver_Terrain))]
    [HarmonyPatch("OnIntervalPassed")]
    public static class VanillaExplorationExpanded_HediffGiver_Terrain_OnIntervalPassed_Patch
    {
        [HarmonyPostfix]
        public static void DoTerrainEffects(Pawn pawn)
        {
            if (pawn.Spawned)
            {
                TerrainDef terrain = pawn.Position.GetTerrain(pawn.Map);
                
                if (StaticCollections.sulfuricTerrains.Contains(terrain) && Rand.MTBEventOccurs(250, 1f, 60f))
                {
                    int num = 2;
                    DamageInfo dinfo = new DamageInfo(DamageDefOf.AcidBurn, num);
                    dinfo.SetBodyRegion(BodyPartHeight.Bottom, BodyPartDepth.Outside);
                    pawn.TakeDamage(dinfo);
                }
                if (terrain == InternalDefOf.VEE_Quicksand)
                {
                    pawn.health.AddHediff(InternalDefOf.VEE_QuicksandHediff);
                }

            }

        }


    }


}












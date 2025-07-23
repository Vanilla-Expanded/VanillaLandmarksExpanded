using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

using System;
using UnityEngine;


namespace VanillaExplorationExpanded
{

    [HarmonyPatch(typeof(PawnUtility))]
    [HarmonyPatch("GetManhunterOnDamageChance")]
    [HarmonyPatch(new Type[] { typeof(Pawn), typeof(Thing), typeof(float) })]
    public static class VanillaExplorationExpanded_PawnUtility_GetManhunterOnDamageChance_Patch
    {
        [HarmonyPostfix]
        public static void IncreaseChance(ref float __result, Pawn pawn)
        {
            if (pawn.Map != null) {
                if (pawn.Map.TileInfo?.Mutators?.Contains(InternalDefOf.VEE_AggressiveHerds) == true)
                {
                    __result += 0.2f;
                }
            
            }

        }


    }


}












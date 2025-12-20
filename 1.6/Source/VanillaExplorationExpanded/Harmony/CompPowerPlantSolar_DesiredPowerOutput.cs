using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;
using UnityEngine;
using System;


namespace VanillaExplorationExpanded
{


    [HarmonyPatch(typeof(CompPowerPlantSolar))]
    [HarmonyPatch("DesiredPowerOutput", MethodType.Getter)]
    public static class VanillaExplorationExpanded_CompPowerPlantSolar_DesiredPowerOutput_Patch
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> ModifySunOutput(IEnumerable<CodeInstruction> codeInstructions)
        {
            var codes = codeInstructions.ToList();

           
            var solarOutputMultiplier = AccessTools.Method(typeof(VanillaExplorationExpanded_CompPowerPlantSolar_DesiredPowerOutput_Patch), "MultiplySolarOutput");

            for (var i = 0; i < codes.Count; i++)
            {

                if (codes[i].opcode == OpCodes.Mul )
                {

                    yield return codes[i];
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, solarOutputMultiplier);
                    yield return new CodeInstruction(OpCodes.Mul);
                }

                else yield return codes[i];
            }
        }


        public static float MultiplySolarOutput(CompPowerPlantSolar comp)
        {
            Map map = comp.parent.Map;
            float multiplier = 1;
            if (map.Tile.Tile.Mutators.Contains(InternalDefOf.VEE_MoreSolarPower))
            {
                multiplier = 1.25f;
            }
            if (map.Tile.Tile.Mutators.Contains(InternalDefOf.VEE_LessSolarPower))
            {
                multiplier = 0.75f;
            }
           
            return multiplier;
        }

    }
}
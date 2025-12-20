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
    [HarmonyPatch("PostDraw")]
    public static class VanillaExplorationExpanded_CompPowerPlantSolar_PostDraw_Patch
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> ModifySunOutput(IEnumerable<CodeInstruction> codeInstructions)
        {
            var codes = codeInstructions.ToList();

            var solarOutputMultiplier = AccessTools.Method(typeof(VanillaExplorationExpanded_CompPowerPlantSolar_DesiredPowerOutput_Patch), "MultiplySolarOutput");

            for (var i = 0; i < codes.Count; i++)
            {

                if (codes[i].opcode == OpCodes.Neg)
                {

                    yield return codes[i];
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, solarOutputMultiplier);
                    yield return new CodeInstruction(OpCodes.Mul);
                }

                else yield return codes[i];
            }
        }



    }
}
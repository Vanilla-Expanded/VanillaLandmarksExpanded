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


    [HarmonyPatch(typeof(CompPowerPlantWind))]
    [HarmonyPatch("CompTick")]
    public static class VanillaExplorationExpanded_CompPowerPlantWind_CompTick_Patch
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> ModifyWindOutput(IEnumerable<CodeInstruction> codeInstructions)
        {
            var codes = codeInstructions.ToList();


            for (var i = 0; i < codes.Count; i++)
            {

                if (codes[i].opcode == OpCodes.Ldc_R4 && (float)codes[i].operand == 1.5f)
                {

                   
                    yield return new CodeInstruction(OpCodes.Ldc_R4,2f);
                }

                else yield return codes[i];
            }
        }


     

    }
}

using RimWorld;
using Verse;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_EltexDeposits : TileMutatorWorker
    {
      

        public TileMutatorWorker_EltexDeposits(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GeneratePostTerrain(Map map)
        {
           
               
                GenStep_ScatterLumpsMineable genStep_ScatterLumpsMineable = new GenStep_ScatterLumpsMineable();
                genStep_ScatterLumpsMineable.maxValue = float.MaxValue;
                genStep_ScatterLumpsMineable.count = 1;
                genStep_ScatterLumpsMineable.forcedLumpSize = new IntRange(4, 6).RandomInRange;
                genStep_ScatterLumpsMineable.forcedDefToScatter = InternalDefOf.VPE_EltexOre;
                genStep_ScatterLumpsMineable.Generate(map, default(GenStepParams));
            
        }
    }
}
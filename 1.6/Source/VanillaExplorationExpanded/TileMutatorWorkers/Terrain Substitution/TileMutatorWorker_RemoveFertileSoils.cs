using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_RemoveFertileSoils : TileMutatorWorker
    {
        public TileMutatorWorker_RemoveFertileSoils(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GeneratePostTerrain(Map map)
        {
         
                foreach (IntVec3 cell in map.AllCells)
                {
                    if (cell.GetTerrain(map).fertility>1)
                    {
                        map.terrainGrid.SetTerrain(cell, TerrainDefOf.Gravel);
                    }
                }

           


        }
    }
}
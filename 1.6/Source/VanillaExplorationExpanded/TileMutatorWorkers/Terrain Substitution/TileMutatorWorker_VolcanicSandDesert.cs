using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_VolcanicSandDesert : TileMutatorWorker
    {
        public TileMutatorWorker_VolcanicSandDesert(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GenerateCriticalStructures(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {

                if (cell.GetTerrain(map) == TerrainDefOf.Sand)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_VolcanicSand);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.SoftSand)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_SoftVolcanicSand);
                }

            }

        }
    }
}
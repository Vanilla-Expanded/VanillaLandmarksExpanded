using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_LittoralDunes : TileMutatorWorker_Bay
    {
        public TileMutatorWorker_LittoralDunes(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GeneratePostTerrain(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {

                if (cell.GetTerrain(map) == TerrainDefOf.Sand)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_LittoralDuneSand);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.SoftSand)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_LittoralDuneSand);
                }

            }
            base.GeneratePostTerrain(map);

        }
    }
}
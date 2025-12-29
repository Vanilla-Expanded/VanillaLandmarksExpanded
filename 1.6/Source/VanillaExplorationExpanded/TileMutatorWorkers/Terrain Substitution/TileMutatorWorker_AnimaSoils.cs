using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_AnimaSoils : TileMutatorWorker
    {
        public TileMutatorWorker_AnimaSoils(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GenerateCriticalStructures(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {
                if (cell.GetTerrain(map) == TerrainDefOf.Soil)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_AnimaSoil);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.Sand)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_AnimaSand);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.SoilRich || cell.GetTerrain(map) == TerrainDefOf.Riverbank)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_AnimaSoilRich);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.Mud)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_AnimaCrackedSoil);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowFreshWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_AnimaWaterShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_AnimaWaterMovingShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_AnimaWaterMovingChestDeep);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepFreshWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_AnimaWaterDeep);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowOceanWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_AnimaWaterOceanShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepOceanWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_AnimaWaterOceanDeep);
                }
            }

        }
    }
}
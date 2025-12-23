
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_StagnantRivulet : TileMutatorWorker_River
    {
     

        public TileMutatorWorker_StagnantRivulet(TileMutatorDef def)
            : base(def)
        {
        }

        public override bool IsValidTile(PlanetTile tile, PlanetLayer layer)
        {
            Tile tileInfo = Find.WorldGrid[tile];
            SurfaceTile surface = tileInfo as SurfaceTile;
            if (surface != null)
            {
                return surface?.Rivers?.Count > 0;
            }
            else return false;
            
        }

        public override string GetLabel(PlanetTile tile)
        {
            return def.label;
        }

        public override void GenerateCriticalStructures(Map map)
        {
            base.GenerateCriticalStructures(map);
            foreach (IntVec3 cell in map.AllCells)
            {
                if (cell.GetTerrain(map)==MapGenUtility.ShallowFreshWaterTerrainAt(cell,map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_StagnantRiverWater);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_SwampyWaterMovingShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_SwampyWaterMovingChestDeep);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.Riverbank)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.MarshyTerrain);
                }
            }

        }
        public override float PlantCommonalityFactorFor(ThingDef plant, PlanetTile tile)
        {
            return (plant.plant.wildTerrainTags?.Contains("WaterMarshy")==true) ? 10f : 1f;
        }


    }
}
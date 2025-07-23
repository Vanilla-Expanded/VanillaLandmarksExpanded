
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
    public class TileMutatorWorker_DryRiver : TileMutatorWorker_River
    {
     

        public TileMutatorWorker_DryRiver(TileMutatorDef def)
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

        public override void GeneratePostTerrain(Map map)
        {
            base.GeneratePostTerrain(map);
            foreach (IntVec3 cell in map.AllCells)
            {
                if (cell.GetTerrain(map).IsRiver)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_DryRiverBed);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.Riverbank)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_DryRiverBank);
                }
            }

        }

      
    }
}
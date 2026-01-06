
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;
using VEF.Maps;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_SulfuricRiver : TileMutatorWorker_River
    {


        public TileMutatorWorker_SulfuricRiver(TileMutatorDef def)
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
                if (cell.GetTerrain(map) == MapGenUtility.DeepMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_SulfuricWaterDeepMovingChestDeep);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_SulfuricWaterMovingShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.RiverbankTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_SaltySand);
                }
            }

        }
        public override void GenerateCriticalStructures(Map map)
        {
            int count;
            TileMutatorExtension extension = this.def.GetModExtension<TileMutatorExtension>();
            if (extension != null)
            {
                count = extension.thingToSpawnAmount.RandomInRange;
                int spawned = 0;
                foreach (IntVec3 cell in map.AllCells.InRandomOrder())
                {
                    if (extension.terrainValidation is null || (extension.terrainValidation != null && extension.terrainValidation.Contains(cell.GetTerrain(map))))
                    {
                        Thing thing = ThingMaker.MakeThing(extension.thingToSpawn, null);
                        GenSpawn.Spawn(thing, cell, map);
                        if (++spawned >= count)
                        {
                            break;
                        }

                    }


                }
            }


        }


    }
}
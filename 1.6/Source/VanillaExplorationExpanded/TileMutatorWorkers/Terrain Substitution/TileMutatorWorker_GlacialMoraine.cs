
using RimWorld;
using System.Linq;
using Verse;
using Verse.Noise;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_GlacialMoraine : TileMutatorWorker
    {
        private const float RidgedFreq = 0.03f;

        private const float RidgedLac = 2f;

        private const int RidgedOctaves = 2;

        private const float MudThreshold = 0.015f;

        private const float WaterThreshold = 0.35f;

        private ModuleBase wetlandNoise;

        public TileMutatorWorker_GlacialMoraine(TileMutatorDef def)
            : base(def)
        {
        }

        public override void Init(Map map)
        {
            if (ModsConfig.OdysseyActive)
            {
                wetlandNoise = new RidgedMultifractal(0.029999999329447746, 2.0, 2, Rand.Int, QualityMode.High);
                wetlandNoise = new Clamp(0.0, 1.0, wetlandNoise);
                NoiseDebugUI.StoreNoiseRender(wetlandNoise, "glacialmoraine");
            }
        }

        public override void GeneratePostTerrain(Map map)
        {
            foreach (IntVec3 allCell in map.AllCells)
            {
                if (allCell.GetEdifice(map) == null)
                {
                    TerrainDef terrain = allCell.GetTerrain(map);
                    if (!terrain.IsWater)
                    {
                        float value = wetlandNoise.GetValue(allCell);
                        if (value > 0.35f)
                        {
                            if (terrain.IsRock)
                            {
                                map.terrainGrid.SetTerrain(allCell, MapGenUtility.MudTerrainAt(allCell, map));
                            }
                            else
                            {
                                map.terrainGrid.SetTerrain(allCell, MapGenUtility.ShallowFreshWaterTerrainAt(allCell, map));
                            }
                        }
                        else if (value > 0.015f && MapGenUtility.ShouldGenerateBeachSand(allCell, map))
                        {
                            map.terrainGrid.SetTerrain(allCell, MapGenUtility.MudTerrainAt(allCell, map));
                        }
                    }
                   
                    if (terrain.categoryType != TerrainDef.TerrainCategoryType.Stone && !terrain.IsWater && terrain != MapGenUtility.BeachTerrainAt(allCell, map) && terrain != MapGenUtility.LakeshoreTerrainAt(allCell, map))
                    {
                        foreach (TerrainPatchMaker terrainPatchMaker in def.terrainPatchMakers)
                        {
                            if (!terrainPatchMaker.isPond)
                            {
                                TerrainDef terrainDef = terrainPatchMaker.TerrainAt(allCell, map, allCell.GetFertility(map));
                                if (terrainDef != null)
                                {
                                    map.terrainGrid.SetTerrain(allCell, terrainDef);
                                }
                            }
                        }
                    }
                }

               

            }
            foreach (TerrainPatchMaker terrainPatchMaker2 in def.terrainPatchMakers)
            {
                terrainPatchMaker2.Cleanup();
            }
        }
    }
}
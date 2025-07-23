using RimWorld;
using RimWorld.Planet;
using UnityEngine.Analytics;
using Verse;
using Verse.Noise;


namespace VanillaExplorationExpanded
{



    public class TileMutatorWorker_LoneIsland : TileMutatorWorker
    {
        protected ModuleBase coastNoise;

        protected const float CoastMacroNoiseFrequency = 0.006f;

        protected const float CoastMacroNoiseStrength = 30f;

        protected const float CoastNoiseFrequency = 0.015f;

        protected const float CoastNoiseStrength = 25f;

        protected virtual float MaxForDeepWater => 0.2f;

        protected virtual float MaxForShallowWater => 0.5f;

        protected virtual float MaxForSand => 0.6f;

        protected virtual FloatRange CoastOffset => new FloatRange(0.1f, 0.2f);

        public TileMutatorWorker_LoneIsland(TileMutatorDef def)
            : base(def)
        {
        }

        public override void Init(Map map)
        {
            float angle = GetCoastAngle(map.Tile);
            coastNoise = MapNoiseUtility.CreateFalloffRadius((float)map.Size.x * 0.6f, map.Center.ToVector2(), 1);

            ModuleBase water6 = new DistFromPoint((float)map.Size.x * 0.95f);
            water6 = new Clamp(MaxForDeepWater, 1.0, water6);
            water6 = new Scale(1, 1.0, 1.0, water6);
            water6 = new Rotate(0.0, angle, 0.0, water6);
            water6 = new Translate((float)(-map.Size.x) / 2f, 0.0, (float)(-map.Size.z) / 2f, water6);
            water6 = MapNoiseUtility.AddDisplacementNoise(water6, 0.015f, 35f, 4, map.Tile.tileId);
            ModuleBase island6 = new DistFromPoint((float)map.Size.x * 0.65f);
            island6 = new ScaleBias(-1.0, 1.0, island6);
            island6 = new Scale(1, 1.0, 1.0, island6);
            island6 = new Rotate(0.0, angle, 0.0, island6);
            island6 = new Translate((float)(-map.Size.x) / 2f, 0.0, (float)(-map.Size.z) / 2f, island6);
            island6 = MapNoiseUtility.AddDisplacementNoise(island6, 0.015f, 25f, 4, map.Tile.tileId);
            coastNoise = new SmoothMin(coastNoise, water6, 0.5);
            coastNoise = new Max(coastNoise, island6);
            NoiseDebugUI.StoreNoiseRender(coastNoise, "coast");
        }

        protected virtual float GetCoastAngle(PlanetTile tile)
        {
            return Find.World.CoastAngleAt(tile, BiomeDefOf.Ocean).GetValueOrDefault();
        }

        public override void GeneratePostElevationFertility(Map map)
        {
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            foreach (IntVec3 cell in map.AllCells)
            {
                float val = GetNoiseValue(cell);
                if (val < MaxForDeepWater)
                {
                    elevation[cell] = 0f;
                }
            }
        }

        public override void GeneratePostTerrain(Map map)
        {
            foreach (IntVec3 cell in map.AllCells)
            {
                TerrainDef terrain = CoastTerrainAt(cell, map);
                if (terrain != null && (cell.GetTerrain(map).categoryType != TerrainDef.TerrainCategoryType.Stone || (cell.GetEdifice(map) == null && terrain.IsWater)))
                {
                    map.terrainGrid.SetTerrain(cell, terrain);
                }
            }
        }

        protected virtual float GetNoiseValue(IntVec3 cell)
        {
            return coastNoise.GetValue(cell);
        }

        protected virtual TerrainDef DeepWaterTerrainAt(IntVec3 cell, Map map)
        {
            return MapGenUtility.DeepOceanWaterTerrainAt(cell, map);
        }

        protected virtual TerrainDef ShallowWaterTerrainAt(IntVec3 cell, Map map)
        {
            return MapGenUtility.ShallowOceanWaterTerrainAt(cell, map);
        }

        protected virtual TerrainDef BeachTerrainAt(IntVec3 cell, Map map)
        {
            return MapGenUtility.BeachTerrainAt(cell, map);
        }

        protected virtual TerrainDef CoastTerrainAt(IntVec3 cell, Map map)
        {
            if (coastNoise == null)
            {
                return null;
            }
            float val = GetNoiseValue(cell);
            if (val < MaxForDeepWater)
            {
                return DeepWaterTerrainAt(cell, map);
            }
            if (val < MaxForShallowWater)
            {
                return ShallowWaterTerrainAt(cell, map);
            }
            if (val < MaxForSand && MapGenUtility.ShouldGenerateBeachSand(cell, map))
            {
                return BeachTerrainAt(cell, map);
            }
            return null;
        }
    }

}
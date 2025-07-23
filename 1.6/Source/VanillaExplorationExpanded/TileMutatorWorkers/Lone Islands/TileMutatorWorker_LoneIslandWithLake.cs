
using RimWorld;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_LoneIslandWithLake : TileMutatorWorker_LoneIsland
    {

        protected ModuleBase innerLakeNoise;
        protected float LakeRadius => 0.2f;
        public static readonly FloatRange LakeSquashRange = new FloatRange(1f, 1.3f);
        protected virtual float LakeFalloffDecay => 1f;

        public TileMutatorWorker_LoneIslandWithLake(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GeneratePostElevationFertility(Map map)
        {
            base.GeneratePostElevationFertility(map);
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            foreach (IntVec3 cell in map.AllCells)
            {
                float val = (float)innerLakeNoise.GetValue(cell.x, 0.0, cell.z);
                if (val > 0.45f)
                {
                    elevation[cell] = 0f;
                }
            }
        }

        public override void Init(Map map)
        {
            base.Init(map);

            map.waterInfo.lakeCenter = map.Center;
            innerLakeNoise = new DistFromPoint((float)map.Size.x * LakeRadius);
            innerLakeNoise = new ScaleBias(-1.0, 1.0, innerLakeNoise);
            innerLakeNoise = new Scale(LakeSquashRange.RandomInRange, 1.0, 1.0, innerLakeNoise);
            innerLakeNoise = new Rotate(0.0, Rand.Range(0f, 360f), 0.0, innerLakeNoise);
            innerLakeNoise = new Translate(-map.waterInfo.lakeCenter.x, 0.0, -map.waterInfo.lakeCenter.z, innerLakeNoise);
            innerLakeNoise = new Clamp(0.0, 1.0, innerLakeNoise);
            innerLakeNoise = new Power(innerLakeNoise, new Const(LakeFalloffDecay));
            innerLakeNoise = MapNoiseUtility.AddDisplacementNoise(innerLakeNoise, 0.006f, 40f, 2, map.Tile.tileId);
            innerLakeNoise = MapNoiseUtility.AddDisplacementNoise(innerLakeNoise, 0.015f, 15f, 4, map.Tile.tileId);
            NoiseDebugUI.StoreNoiseRender(innerLakeNoise, "innerLakeNoise");
        }

      
        

        public override void GeneratePostTerrain(Map map)
        {
            base.GeneratePostTerrain(map);
            foreach (IntVec3 cell in map.AllCells)
            {
               
                if (!cell.GetTerrain(map).IsWater)
                {
                   
                    ProcessCell(cell, map);
                }
                
            }
        }

        public void ProcessCell(IntVec3 cell, Map map)
        {
            float val = (float)innerLakeNoise.GetValue(cell.x, 0.0, cell.z);
           

            if (val > 0.75f)
            {
                map.terrainGrid.SetTerrain(cell, MapGenUtility.DeepFreshWaterTerrainAt(cell, map));
            }
            else if (val > 0.5f)
            {
                map.terrainGrid.SetTerrain(cell, MapGenUtility.ShallowFreshWaterTerrainAt(cell, map));
            }
            else if (val > 0.45f && MapGenUtility.ShouldGenerateBeachSand(cell, map))
            {
                map.terrainGrid.SetTerrain(cell, MapGenUtility.LakeshoreTerrainAt(cell, map));
            }
        }

       
    }
}

using RimWorld;
using Verse;
using Verse.Noise;
using static RimWorld.BaseGen.SymbolStack;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_CoralReef : TileMutatorWorker_LoneIsland
    {

        protected ModuleBase innerLakeNoise;
        protected float LakeRadius => 0.5f;
        public static readonly FloatRange CoralRadius = new FloatRange(0.75f, 0.85f);
        protected virtual float LakeFalloffDecay => 1f;

        public TileMutatorWorker_CoralReef(TileMutatorDef def)
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
                if (cell.GetTerrain(map).IsWater&&cell.DistanceTo(map.Center) > map.Size.x/2 * CoralRadius.min && cell.DistanceTo(map.Center) < map.Size.x/2 * CoralRadius.max)
                {
                   
                    if (Rand.Chance(0.25f))
                    {
                        Thing thing = ThingMaker.MakeThing(InternalDefOf.VEE_Coral, null);

                        GenSpawn.Spawn(thing, cell, map);
                    }
                    
                }
                   

            }
        }

        public void ProcessCell(IntVec3 cell, Map map)
        {
            float val = (float)innerLakeNoise.GetValue(cell.x, 0.0, cell.z);


            if (val > 0.75f)
            {
                map.terrainGrid.SetTerrain(cell, MapGenUtility.DeepOceanWaterTerrainAt(cell, map));
            }
            else if (val > 0.5f)
            {
                map.terrainGrid.SetTerrain(cell, MapGenUtility.ShallowOceanWaterTerrainAt(cell, map));
            }
            else if (val > 0.45f && MapGenUtility.ShouldGenerateBeachSand(cell, map))
            {
                map.terrainGrid.SetTerrain(cell, MapGenUtility.BeachTerrainAt(cell, map));
            }
        }


    }
}
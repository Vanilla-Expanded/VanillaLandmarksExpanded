
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_ToxicCrater : TileMutatorWorker_Crater
    {

        protected ModuleBase innerLakeNoise;
        protected float LakeRadius => 0.4f;
        public static readonly FloatRange LakeSquashRange = new FloatRange(1f, 1.1f);
        protected virtual float LakeFalloffDecay => 1f;

        protected override bool generateAshTerrainInCenter => false;

        public override FloatRange CenterOffsetRange => new FloatRange(0f, 0f);


        public TileMutatorWorker_ToxicCrater(TileMutatorDef def)
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
                map.terrainGrid.SetTerrain(cell, TerrainDefOf.ToxicWaterDeep);
            }
            else if (val > 0.5f)
            {
                map.terrainGrid.SetTerrain(cell, TerrainDefOf.ToxicWaterShallow);
            }
            else if (val > 0.45f && MapGenUtility.ShouldGenerateBeachSand(cell, map))
            {
                map.terrainGrid.SetTerrain(cell, MapGenUtility.LakeshoreTerrainAt(cell, map));
            }


          
        }




    }
}
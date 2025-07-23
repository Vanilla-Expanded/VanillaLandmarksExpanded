
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_ResurgentCaldera : TileMutatorWorker_Crater
    {

        protected ModuleBase mountainNoise;

        private const float MountainRadius = 0.2f;

        private const float MountainExp = 0.2f;

        private const float MountainNoiseFrequency = 0.015f;

        private const float MountainNoiseStrength = 25f;

        private const float MountainThreshold = 0f;

        protected override bool generateAshTerrainInCenter => false;

        public override FloatRange CenterOffsetRange => new FloatRange(0f, 0f);


        public TileMutatorWorker_ResurgentCaldera(TileMutatorDef def)
            : base(def)
        {
        }

      

        public override void Init(Map map)
        {
            base.Init(map);

            Vector2 center = map.Center.ToVector2();

            mountainNoise = MapNoiseUtility.CreateFalloffRadius((float)map.Size.x * MountainRadius, center, MountainExp);
            mountainNoise = MapNoiseUtility.AddDisplacementNoise(mountainNoise, MountainNoiseFrequency, MountainNoiseStrength);
            NoiseDebugUI.StoreNoiseRender(mountainNoise, "mountainNoise");
            
        }


        public override void GeneratePostElevationFertility(Map map)
        {
            base.GeneratePostElevationFertility(map);
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            foreach (IntVec3 cell in map.AllCells)
            {
                float val = mountainNoise.GetValue(cell);
                if (val > 0f)
                {
                    elevation[cell] = val;
                }
            }
        }




    }
}
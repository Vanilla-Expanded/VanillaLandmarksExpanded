
using RimWorld;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_Ridge : TileMutatorWorker
    {
        private ModuleBase ridge;

        private const float ridgeSpan = 0.15f;

        private const float ridgeThreshold = 0.5f;

        private const float ridgeNoiseFreq = 0.015f;

        private const float ridgeNoiseStrength = 20f;

        public TileMutatorWorker_Ridge(TileMutatorDef def)
            : base(def)
        {
        }

        public override void Init(Map map)
        {
            base.Init(map);
            float direction = Rand.Chance(0.5f) ? 0f : 90f;
            ridge = new DistFromAxis((float)map.Size.x * ridgeSpan);
            ridge = new Rotate(0.0, direction, 0.0, ridge);
            ridge = new Translate((float)(-map.Size.x) / 2f, 0.0, (float)(-map.Size.z) / 2f, ridge);
            ridge = MapNoiseUtility.AddDisplacementNoise(ridge, ridgeNoiseFreq, ridgeNoiseStrength);
            NoiseDebugUI.StoreNoiseRender(ridge, "ridge");
        }

        public override void GeneratePostElevationFertility(Map map)
        {
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            foreach (IntVec3 c in map.AllCells)
            {
                if (ridge.GetValue(c) <= ridgeThreshold)
                {
                    elevation[c] = 1f;
                }
            }
        }

        
    }
}

using RimWorld;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_StoneForest : TileMutatorWorker
    {
        private ModuleBase blobs;
        private const float blobFreq = 0.15f;
        private const float blobThreshold = 0.2f;
        private const float warpFreq = 0.02f;
        private const float warpStrength = 18f;

        public TileMutatorWorker_StoneForest(TileMutatorDef def)
            : base(def)
        {
        }

        public override void Init(Map map)
        {
            base.Init(map);
          
            blobs = new Perlin(blobFreq, 2.0, 0.5, 4, Rand.Int, QualityMode.Medium
            );
          
            blobs = MapNoiseUtility.AddDisplacementNoise( blobs, warpFreq,  warpStrength
            );

            blobs = new Translate(  -map.Size.x / 2f, 0.0,  -map.Size.z / 2f, blobs
            );

            NoiseDebugUI.StoreNoiseRender(blobs, "stoneForest");
        }

        public override void GeneratePostElevationFertility(Map map)
        {
            MapGenFloatGrid elevation = MapGenerator.Elevation;

            foreach (IntVec3 c in map.AllCells)
            {
                float v = blobs.GetValue(c);

                if (v > blobThreshold)
                {
                    elevation[c] = 1f;
                }
            }
        }


    }
}
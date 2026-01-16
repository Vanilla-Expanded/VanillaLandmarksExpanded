
using RimWorld;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_DeepSnow : TileMutatorWorker
    {
        private ModuleBase blobs;
        private const float blobFreq = 0.05f;
        private const float blobThreshold = 0.6f;
        private const float warpFreq = 0.02f;
        private const float warpStrength = 18f;

        public TileMutatorWorker_DeepSnow(TileMutatorDef def)
            : base(def)
        {
        }

        public override void Init(Map map)
        {
            base.Init(map);

            blobs = new Perlin(blobFreq, 2.0, 0.5, 4, Rand.Int, QualityMode.Medium
            );

            blobs = MapNoiseUtility.AddDisplacementNoise(blobs, warpFreq, warpStrength
            );

            blobs = new Translate(-map.Size.x / 2f, 0.0, -map.Size.z / 2f, blobs
            );

            NoiseDebugUI.StoreNoiseRender(blobs, "snowblobs");
        }

        public override void GeneratePostTerrain(Map map)
        {

            foreach (IntVec3 c in map.AllCells)
            {
                float v = blobs.GetValue(c);

                if (v > blobThreshold)
                {
                    GenSpawn.Spawn(InternalDefOf.VEE_DenseSnow, c, map);
                }
            }
        }


    }
}
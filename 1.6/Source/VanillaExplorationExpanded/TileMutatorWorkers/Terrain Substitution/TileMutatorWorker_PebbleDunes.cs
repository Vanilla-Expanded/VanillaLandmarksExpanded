
using RimWorld;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{


    public class TileMutatorWorker_PebbleDunes : TileMutatorWorker
    {
        private ModuleBase pebblesNoise;

        private const float VoronoiFrequency = 0.003f;

        private const float VoronoiRandomnessX = 0.15f;
        private const float VoronoiRandomnessZ = 0.5f;
      
        private const float NoiseFrequency = 0.015f;

        private const float NoiseStrength = 35f;

        private const int NoiseOctaves = 2;

        private const float StretchFactor = 7f;
        private const float ScaleFactor = 1f;
        private const float Threshold = 0.2f;

        private const float DisplaceFrequency = 0.02f;
        private const float DisplaceMagnitude = 7.5f;

        public TileMutatorWorker_PebbleDunes(TileMutatorDef def)
            : base(def)
        {
        }

        public override void Init(Map map)
        {
            if (ModsConfig.OdysseyActive)
            {
                pebblesNoise = new Voronoi2D(VoronoiFrequency, Rand.Int, VoronoiRandomnessX, VoronoiRandomnessZ, staggered: true);
                pebblesNoise = MapNoiseUtility.AddDisplacementNoise(pebblesNoise, NoiseFrequency, NoiseStrength, NoiseOctaves);
                pebblesNoise = new Scale(1.0, 1.0, StretchFactor, pebblesNoise);
                pebblesNoise = new Scale(1.0, 1.0, ScaleFactor, pebblesNoise);
                ModuleBase lhs = new Perlin(DisplaceFrequency, 2.0, 0.5, 2, Rand.Int, QualityMode.Medium);
                lhs = new Multiply(lhs, new Const(DisplaceMagnitude));
                pebblesNoise = new Displace(pebblesNoise, new Const(0.0), new Const(0.0), lhs);
                pebblesNoise = new Rotate(0.0, Rand.Range(0f, 180f), 0.0, pebblesNoise);
            }
        }

        public override void GeneratePostTerrain(Map map)
        {
            foreach (IntVec3 allCell in map.AllCells)
            {
                if (!(pebblesNoise.GetValue(allCell) > 0.2f))
                {
                    map.terrainGrid.SetTerrain(allCell, InternalDefOf.VEE_PebbleDunes);
                }
            }
        }
    }
}
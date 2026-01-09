
using RimWorld;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{


public class TileMutatorWorker_PermafrostBasin : TileMutatorWorker
{
    private ModuleBase iceNoise;

    private const float VoronoiFrequency = 0.0025f;

    private const float VoronoiRandomnessX = 0.15f;

    private const float VoronoiRandomnessZ = 0.5f;

    private const float NoiseFrequency = 0.015f;

    private const float NoiseStrength = 20f;

    private const int NoiseOctaves = 2;

    private const float StretchFactor = 15f;

    private const float ScaleFactor = 1f;

    private const float Threshold = 0.2f;

    private const float DisplaceFrequency = 0.02f;

    private const float DisplaceMagnitude = 7.5f;

    public TileMutatorWorker_PermafrostBasin(TileMutatorDef def)
        : base(def)
    {
    }

    public override void Init(Map map)
    {
        if (ModsConfig.OdysseyActive)
        {
                iceNoise = new Voronoi2D(VoronoiFrequency, Rand.Int, VoronoiRandomnessX, VoronoiRandomnessZ, staggered: true);
                iceNoise = MapNoiseUtility.AddDisplacementNoise(iceNoise, NoiseFrequency, NoiseStrength, NoiseOctaves);
                iceNoise = new Scale(1.0, 1.0, StretchFactor, iceNoise);
                iceNoise = new Scale(1.0, 1.0, ScaleFactor, iceNoise);
            ModuleBase lhs = new Perlin(DisplaceFrequency, 2.0, 0.5, 2, Rand.Int, QualityMode.Medium);
            lhs = new Multiply(lhs, new Const(DisplaceMagnitude));
                iceNoise = new Displace(iceNoise, new Const(0.0), new Const(0.0), lhs);
                iceNoise = new Rotate(0.0, Rand.Range(0f, 180f), 0.0, iceNoise);
        }
    }

    public override void GeneratePostTerrain(Map map)
    {
        foreach (IntVec3 allCell in map.AllCells)
        {
            if (!(iceNoise.GetValue(allCell) > 0.2f))
            {
                map.terrainGrid.SetTerrain(allCell, TerrainDefOf.Ice);
            }
        }
    }
}
}
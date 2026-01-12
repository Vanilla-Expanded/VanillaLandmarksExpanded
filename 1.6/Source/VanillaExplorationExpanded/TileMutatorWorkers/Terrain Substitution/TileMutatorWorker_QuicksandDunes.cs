
using RimWorld;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{


public class TileMutatorWorker_QuicksandDunes : TileMutatorWorker
{
    private ModuleBase quicksandNoise;

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

    public TileMutatorWorker_QuicksandDunes(TileMutatorDef def)
        : base(def)
    {
    }

    public override void Init(Map map)
    {
        if (ModsConfig.OdysseyActive)
        {
                quicksandNoise = new Voronoi2D(VoronoiFrequency, Rand.Int, VoronoiRandomnessX, VoronoiRandomnessZ, staggered: true);
                quicksandNoise = MapNoiseUtility.AddDisplacementNoise(quicksandNoise, NoiseFrequency, NoiseStrength, NoiseOctaves);
                quicksandNoise = new Scale(1.0, 1.0, StretchFactor, quicksandNoise);
                quicksandNoise = new Scale(1.0, 1.0, ScaleFactor, quicksandNoise);
            ModuleBase lhs = new Perlin(DisplaceFrequency, 2.0, 0.5, 2, Rand.Int, QualityMode.Medium);
            lhs = new Multiply(lhs, new Const(DisplaceMagnitude));
                quicksandNoise = new Displace(quicksandNoise, new Const(0.0), new Const(0.0), lhs);
                quicksandNoise = new Rotate(0.0, Rand.Range(0f, 180f), 0.0, quicksandNoise);
        }
    }

    public override void GeneratePostTerrain(Map map)
    {
        foreach (IntVec3 allCell in map.AllCells)
        {
            if (quicksandNoise.GetValue(allCell) < 0.2f)
            {
                map.terrainGrid.SetTerrain(allCell, InternalDefOf.VEE_Quicksand);
            }else if (quicksandNoise.GetValue(allCell) < 0.25f)
                {
                    map.terrainGrid.SetTerrain(allCell, InternalDefOf.VEE_RedSoftSand);
                }
            }
    }
}
}
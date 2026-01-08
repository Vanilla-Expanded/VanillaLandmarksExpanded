
using RimWorld;
using VEF.Maps;
using Verse;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_SulfuricLake : TileMutatorWorker_Lake
    {
        public TileMutatorWorker_SulfuricLake(TileMutatorDef def)
            : base(def)
        {
        }

        protected override void ProcessCell(IntVec3 cell, Map map)
        {
            float valAt = GetValAt(cell, map);
            if (GenerateDeepWater && valAt > 0.75f)
            {
                map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_SulfuricWaterDeep);
            }
            else if (valAt > 0.5f)
            {
                map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_SulfuricWaterShallow);
            }
            else if (valAt > 0.45f && MapGenUtility.ShouldGenerateBeachSand(cell, map))
            {
                map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_SaltySand);
            }
        }
       
    }
}

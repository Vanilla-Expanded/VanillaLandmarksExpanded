
using RimWorld;
using Verse;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_DustBowl : TileMutatorWorker_Lake
    {
        protected override float LakeRadius => 0.75f;
       
        protected override bool GenerateDeepWater => false;

        public TileMutatorWorker_DustBowl(TileMutatorDef def)
            : base(def)
        {
        }

        protected override void ProcessCell(IntVec3 cell, Map map)
        {
            float val = GetValAt(cell, map);
            if (val > 0.5f)
            {
                map.terrainGrid.SetTerrain(cell, TerrainDefOf.Sand);
            }
            else if (val > 0.45f)
            {
                map.terrainGrid.SetTerrain(cell, TerrainDefOf.SoftSand);
            }
        }
    }
}

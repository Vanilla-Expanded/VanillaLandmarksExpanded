
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
        public override void GenerateCriticalStructures(Map map)
        {
            int count;
            TileMutatorExtension extension = this.def.GetModExtension<TileMutatorExtension>();
            if (extension != null)
            {
                count = extension.thingToSpawnAmount.RandomInRange;
                int spawned = 0;
                foreach (IntVec3 cell in map.AllCells.InRandomOrder())
                {
                    if (

                        (extension.terrainValidation is null || (extension.terrainValidation != null && extension.terrainValidation.Contains(cell.GetTerrain(map))))
                        &&
                        cell.GetTerrain(map) != InternalDefOf.VEE_SulfuricWaterDeep && cell.GetTerrain(map) != InternalDefOf.VEE_SulfuricWaterShallow
                        && cell.GetTerrain(map) != InternalDefOf.VEE_SaltySand
                        )
                    {
                        Thing thing = ThingMaker.MakeThing(extension.thingToSpawn, null);
                        GenSpawn.Spawn(thing, cell, map);
                        if (++spawned >= count)
                        {
                            break;
                        }

                    }


                }
            }


        }
    }
}

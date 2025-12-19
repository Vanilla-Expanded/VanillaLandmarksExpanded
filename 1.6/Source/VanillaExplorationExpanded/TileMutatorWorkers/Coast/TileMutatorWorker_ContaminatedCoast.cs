
using RimWorld;
using VEF.Maps;
using Verse;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_ContaminatedCoast : TileMutatorWorker_Coast
    {



        public TileMutatorWorker_ContaminatedCoast(TileMutatorDef def)
            : base(def)
        {
        }


        public override void GenerateCriticalStructures(Map map)
        {
            foreach (IntVec3 allCell in map.AllCells)
            {

                if (allCell.GetTerrain(map) == MapGenUtility.BeachTerrainAt(allCell, map))
                {
                    map.terrainGrid.SetTerrain(allCell, InternalDefOf.VEE_IrradiatedSoil);
                }
                if (allCell.GetTerrain(map) == MapGenUtility.DeepOceanWaterTerrainAt(allCell, map))
                {
                    map.terrainGrid.SetTerrain(allCell, InternalDefOf.VEE_IrradiatedOceanWaterDeep);
                }
                if (allCell.GetTerrain(map) == MapGenUtility.ShallowOceanWaterTerrainAt(allCell, map))
                {
                    map.terrainGrid.SetTerrain(allCell, InternalDefOf.VEE_IrradiatedWaterOceanShallow);
                }
            }

            int count;
            TileMutatorExtension extension = this.def.GetModExtension<TileMutatorExtension>();
            if (extension != null)
            {
                count = extension.thingToSpawnAmount.RandomInRange;
                int spawned = 0;
                foreach (IntVec3 cell in map.AllCells.InRandomOrder())
                {
                    if (extension.terrainValidation is null || (extension.terrainValidation != null && extension.terrainValidation.Contains(cell.GetTerrain(map))))
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

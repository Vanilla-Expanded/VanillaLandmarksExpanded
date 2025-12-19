
using RimWorld;
using VEF.Maps;
using Verse;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_ContaminatedReservoir : TileMutatorWorker_Lake
    {
        protected override float LakeRadius => 0.65f;



        public TileMutatorWorker_ContaminatedReservoir(TileMutatorDef def)
            : base(def)
        {
        }

        protected override void ProcessCell(IntVec3 cell, Map map)
        {
            float val = GetValAt(cell, map);
            if (val > 0.75f)
            {
                map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_IrradiatedWaterDeep);
            }
            else
            if (val > 0.5f)
            {
                map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_IrradiatedWaterShallow);
            }
            else if (val > 0.45f)
            {
                map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_IrradiatedSoil);
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

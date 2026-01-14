using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_JadeiteMountains : TileMutatorWorker
    {
        public TileMutatorWorker_JadeiteMountains(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GenerateCriticalStructures(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {

                List<Thing> thingsInCell = cell.GetThingList(map);

                List<Thing> thingsToDestroy = new List<Thing>();

                foreach(Thing thing in thingsInCell)
                {
                    if (thing.def.building?.smoothedThing != null)
                    {
                        thingsToDestroy.Add(thing);
                       
                    }
                
                }
                if (thingsToDestroy.Count > 0) {
                    thingsToDestroy[0].Destroy();
                    GenSpawn.Spawn(InternalDefOf.VEE_MineableJadeite, cell, map);

                }
                thingsToDestroy.Clear();

            }

        }
    }
}
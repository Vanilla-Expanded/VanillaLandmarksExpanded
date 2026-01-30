
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;
namespace VanillaExplorationExpanded
{
    public class GenStep_ScatterThingsClearPlants : GenStep_ScatterThings
    {


        protected override void ScatterAt(IntVec3 loc, Map map, GenStepParams parms, int stackCount = 1)
        {
            base.ScatterAt(loc, map, parms, stackCount);
           
            if (clearSpaceSize > 0)
            {
                foreach (IntVec3 item in GenRadial.RadialCellsAround(loc, clearSpaceSize,true))
                {
                    List<Thing> thingsHere = item.GetThingList(map);
                    List<Thing> plantsToDestroy = new List<Thing>();
                    if (thingsHere.Count > 0)
                    {
                        
                        List<Thing> plantsHere = thingsHere.Where(x => x.def.IsPlant && x.def != InternalDefOf.VEE_Plant_TreeAnima_Ancient).ToList();
                       
                        if (plantsHere.Count > 0)
                        {                        
                                plantsToDestroy.AddRange(plantsHere);                            
                        }
                    }
                    if (plantsToDestroy.Count > 0)
                    {
                        foreach(Thing plant in plantsToDestroy)
                        {
                            plant.Destroy();
                        }
                    }
                }
            }
            
        }

    }
}
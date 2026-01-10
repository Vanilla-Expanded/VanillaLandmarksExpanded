
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Analytics;
using Verse;
namespace VanillaExplorationExpanded
{
    public class GameCondition_TidalFlooding : GameCondition
    {

        public override void GameConditionTick()
        {
            base.GameConditionTick();

            if (Find.TickManager.TicksGame % 240000 == 0)
            {
                foreach (Map map in base.AffectedMaps)
                {
                    IncidentParms parms = StorytellerUtility.DefaultParmsNow(InternalDefOf.VEE_TidalFloodingIncident.category, map);
                    InternalDefOf.VEE_TidalFloodingIncident.Worker.TryExecute(parms);
                }
                   

            }
        }

    }
}

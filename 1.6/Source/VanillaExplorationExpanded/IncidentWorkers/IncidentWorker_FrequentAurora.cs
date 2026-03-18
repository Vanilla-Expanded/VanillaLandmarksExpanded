using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace VanillaExplorationExpanded
{
    public class IncidentWorker_FrequentAurora : IncidentWorker_MakeGameCondition
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            PlanetTile tile = parms.target.Tile;

            if (tile.Tile.Mutators?.Contains(InternalDefOf.VEE_FrequentAuroras) != true)
            {
                return base.CanFireNowSub(parms);
            }

            return false;

        }

    }
}

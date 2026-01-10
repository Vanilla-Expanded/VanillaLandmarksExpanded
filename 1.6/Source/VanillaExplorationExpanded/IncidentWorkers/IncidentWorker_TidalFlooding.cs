using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace VanillaExplorationExpanded
{
    public class IncidentWorker_TidalFlooding : IncidentWorker
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            GenSpawn.Spawn(InternalDefOf.VEE_TidalFlood, map.Center, map);
            return true;
        }

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            SurfaceTile surfaceTile = map.TileInfo as SurfaceTile;
            if (surfaceTile == null)
            {
                return false;
            }
            if (!surfaceTile.Mutators.Contains(InternalDefOf.VEE_RisingWaters))
            {
                return false;
            }
            if (!surfaceTile.IsCoastal)
            {
                return false;
            }
           
            
            return true;
        }
    }
}
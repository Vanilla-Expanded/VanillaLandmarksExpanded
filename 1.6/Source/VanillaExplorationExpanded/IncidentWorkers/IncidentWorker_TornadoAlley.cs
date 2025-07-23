
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace VanillaExplorationExpanded
{
    public class IncidentWorker_Tornado_MutatorLocked : IncidentWorker
    {
        private const int MinDistanceFromMapEdge = 30;

     

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            return map.TileInfo?.Mutators?.Contains(InternalDefOf.VEE_TornadoAlley) == true;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            CellRect cellRect = CellRect.WholeMap(map).ContractedBy(MinDistanceFromMapEdge);
            if (cellRect.IsEmpty)
            {
                cellRect = CellRect.WholeMap(map);
            }
            if (!CellFinder.TryFindRandomCellInsideWith(cellRect, (IntVec3 x) => CanSpawnTornadoAt(x, map), out var result))
            {
                return false;
            }
            Tornado tornado = (Tornado)GenSpawn.Spawn(InternalDefOf.Tornado, result, map);
            SendStandardLetter("VEE_LetterLabelTornado".Translate(), "VEE_LetterTornado".Translate(), LetterDefOf.ThreatSmall, parms, new TargetInfo(result, map));
            return true;
        }

        private bool CanSpawnTornadoAt(IntVec3 c, Map map)
        {
            if (c.Fogged(map))
            {
                return false;
            }
            int num = GenRadial.NumCellsInRadius(7f);
            for (int i = 0; i < num; i++)
            {
                IntVec3 c2 = c + GenRadial.RadialPattern[i];
                if (c2.InBounds(map) && AnyPawnOfPlayerFactionAt(c2, map))
                {
                    return false;
                }
            }
            return true;
        }

        private bool AnyPawnOfPlayerFactionAt(IntVec3 c, Map map)
        {
            List<Thing> thingList = c.GetThingList(map);
            for (int i = 0; i < thingList.Count; i++)
            {
                Pawn pawn = thingList[i] as Pawn;
                if (pawn != null && pawn.Faction == Faction.OfPlayer)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
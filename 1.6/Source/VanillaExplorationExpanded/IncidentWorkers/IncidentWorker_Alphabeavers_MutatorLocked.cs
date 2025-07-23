
using RimWorld;
using UnityEngine;
using Verse;
namespace VanillaExplorationExpanded
{
    public class IncidentWorker_Alphabeavers_MutatorLocked : IncidentWorker
    {
        private static readonly FloatRange CountPerColonistRange = new FloatRange(1f, 1.5f);

        private const int MinCount = 1;

        private const int MaxCount = 10;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }
            Map map = (Map)parms.target;
            if (map.TileInfo?.Mutators?.Contains(InternalDefOf.VEE_Alphabeavers)!=true)
            {
                return false;
            }
            if (!map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(PawnKindDefOf.Alphabeaver.race))
            {
                return false;
            }
            IntVec3 result;
            return RCellFinder.TryFindRandomPawnEntryCell(out result, map, CellFinder.EdgeRoadChance_Animal);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef beaverKind = PawnKindDefOf.Alphabeaver;
            if (!RCellFinder.TryFindRandomPawnEntryCell(out IntVec3 loc, map, CellFinder.EdgeRoadChance_Animal))
            {
                return false;
            }
            int numColonists = map.mapPawns.FreeColonistsCount;
            float beaversPerColonist = CountPerColonistRange.RandomInRange;
            float numBeaversRaw = (float)numColonists * beaversPerColonist;
            int numBeavers = Mathf.Clamp(GenMath.RoundRandom(numBeaversRaw), MinCount, MaxCount);
            for (int i = 0; i < numBeavers; i++)
            {
                IntVec3 spawnLoc = CellFinder.RandomClosewalkCellNear(loc, map, 10);
                Pawn animal = PawnGenerator.GeneratePawn(beaverKind);
                Pawn beaver = (Pawn)GenSpawn.Spawn(animal, spawnLoc, map);
                beaver.needs.food.CurLevelPercentage = 1f;
            }
            SendStandardLetter("LetterLabelBeaversArrived".Translate(), "BeaversArrived".Translate(), LetterDefOf.ThreatSmall, parms, new TargetInfo(loc, map));
            return true;
        }
    }
}
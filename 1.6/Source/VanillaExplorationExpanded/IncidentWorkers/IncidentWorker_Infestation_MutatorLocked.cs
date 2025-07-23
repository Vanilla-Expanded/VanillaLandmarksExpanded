
using RimWorld;
using UnityEngine;
using Verse;
namespace VanillaExplorationExpanded
{
    public class IncidentWorker_Infestation_MutatorLocked : IncidentWorker
    {
        public const float HivePoints = 220f;

        public static readonly SimpleCurve PointsFactorCurve = new SimpleCurve
    {
        new CurvePoint(0f, 0.7f),
        new CurvePoint(5000f, 0.45f)
    };

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 cell;
            if (map.TileInfo?.Mutators?.Contains(InternalDefOf.VEE_IncreasedInfestations) != true)
            {
                return false;
            }
            return base.CanFireNowSub(parms) && Faction.OfInsects != null && HiveUtility.TotalSpawnedHivesCount(map) < 30 && InfestationCellFinder.TryFindCell(out cell, map);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            parms.points *= PointsFactorCurve.Evaluate(parms.points);
            int hivesCount = Mathf.Max(GenMath.RoundRandom(parms.points / 220f), 1);
            Thing tunnel = InfestationUtility.SpawnTunnels(hivesCount, map, spawnAnywhereIfNoGoodCell: false, parms.infestationLocOverride.HasValue, null, parms.infestationLocOverride);
            SendStandardLetter(parms, tunnel);
            Find.TickManager.slower.SignalForceNormalSpeedShort();
            return true;
        }
    }
}
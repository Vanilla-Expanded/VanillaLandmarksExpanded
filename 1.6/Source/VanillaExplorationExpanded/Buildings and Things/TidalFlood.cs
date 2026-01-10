
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
namespace VanillaExplorationExpanded
{
    public class TidalFlood : Flood
    {
        private const int RecedeIntervalTicks = 1;

        private static readonly IntRange FloodedTicksRange = new IntRange(45000, 60000);

        private int remainFloodedTicks;

        protected override int MaxFloodDurationTicks => 60000;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref remainFloodedTicks, "floodedTicks", 0);
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {

            base.SpawnSetup(map, respawningAfterLoad);
            if (!respawningAfterLoad)
            {
                remainFloodedTicks = FloodedTicksRange.RandomInRange;
                CellFinder.TryFindRandomCell(map, (IntVec3 c) => c.GetTerrain(map).IsWater && !c.Fogged(map), out var result);
            }
        }

        protected override void Tick()
        {

            base.Tick();
           
            if (!base.Destroyed && remainFloodedTicks > 0 && Find.TickManager.TicksGame > spawnedTick + base.FloodingTicks + remainFloodedTicks)
            {

                Destroy();
            }
        }

        protected override IEnumerable<(IntVec3, int)> GetInitialCells(Map map)
        {

            List<IntVec3> list = new List<IntVec3>();
            foreach (IntVec3 allCell in map.AllCells)
            {
                if (allCell.GetTerrain(map).IsWater)
                {
                    list.Add(allCell);
                }
            }
            foreach (IntVec3 item in list.ToList())
            {
                bool flag = false;
                IntVec3[] cardinalDirections = GenAdj.CardinalDirections;
                foreach (IntVec3 intVec in cardinalDirections)
                {
                    IntVec3 c = item + intVec;
                    if (c.InBounds(base.Map) && !c.GetTerrain(map).IsWater)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    list.Remove(item);
                }
            }
            list.Shuffle();
            foreach (IntVec3 item2 in list)
            {
                yield return (item2, Flood.FloodWidthRange.RandomInRange);
            }
        }

        protected override void SpreadFlood(IntVec3 cell, TerrainDef sourceTerrain)
        {
            int num = estimatedFloodedTiles - floodedTileCount;
            base.Map.terrainGrid.SetTempTerrain(cell, InternalDefOf.VEE_TidalFloodwater);
            base.Map.tempTerrain.QueueRemoveTerrain(cell, spawnedTick + base.FloodingTicks + remainFloodedTicks + num);
        }
    }
}
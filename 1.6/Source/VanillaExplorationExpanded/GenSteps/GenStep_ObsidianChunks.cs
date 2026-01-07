using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.Noise;

namespace VanillaExplorationExpanded
{
    public class GenStep_ObsidianChunks : GenStep
    {
        private ModuleBase freqFactorNoise;

        private const float ThreshLooseRock = 0.55f;

        private const float PlaceProbabilityPerCell = 0.006f;

        private const float RubbleProbability = 0.5f;

        public override int SeedPart => 1898758716;

        public override void Generate(Map map, GenStepParams parms)
        {
            if (map.TileInfo.WaterCovered)
            {
                return;
            }
            freqFactorNoise = new Perlin(0.014999999664723873, 2.0, 0.5, 6, Rand.Range(0, 999999), QualityMode.Medium);
            freqFactorNoise = new ScaleBias(1.0, 1.0, freqFactorNoise);
            NoiseDebugUI.StoreNoiseRender(freqFactorNoise, "obsidian_chunks_freq_factor");
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            float num = PlaceProbabilityPerCell;

            foreach (IntVec3 allCell in map.AllCells)
            {
                float num2 = num * freqFactorNoise.GetValue(allCell);
                if ((elevation[allCell] < ThreshLooseRock || map.generatorDef.isUnderground) && Rand.Value < num2)
                {
                    GrowLowRockFormationFrom(allCell, map);
                }
            }
            freqFactorNoise = null;
        }

        private void GrowLowRockFormationFrom(IntVec3 root, Map map)
        {


            ThingDef thingDef = InternalDefOf.VEE_ObsidianChunk;

            Rot4 random = Rot4.Random;
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            IntVec3 intVec = root;
            while (true)
            {
                Rot4 random2 = Rot4.Random;
                if (random2 == random)
                {
                    continue;
                }
                intVec += random2.FacingCell;
                if (!intVec.InBounds(map) || intVec.GetEdifice(map) != null || intVec.GetFirstItem(map) != null || (!map.generatorDef.isUnderground && elevation[intVec] > 0.55f) || !intVec.GetAffordances(map).Contains(TerrainAffordanceDefOf.Heavy) || intVec.GetDoor(map) != null)
                {
                    break;
                }
                GenSpawn.Spawn(thingDef, intVec, map);
                IntVec3[] adjacentCellsAndInside = GenAdj.AdjacentCellsAndInside;
                foreach (IntVec3 intVec2 in adjacentCellsAndInside)
                {
                    if (!(Rand.Value < 0.5f))
                    {
                        continue;
                    }
                    IntVec3 c = intVec + intVec2;
                    if (!c.InBounds(map))
                    {
                        continue;
                    }

                }
            }
        }
    }
}
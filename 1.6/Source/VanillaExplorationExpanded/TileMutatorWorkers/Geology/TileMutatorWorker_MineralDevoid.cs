using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_MineralDevoid : TileMutatorWorker
    {
        private Perlin perlin;

        private const int SeedPart = 525214866;

        private List<ThingDef> cachedMineables;

        private List<ThingDef> Mineables
        {
            get
            {
                if (cachedMineables == null)
                {
                    cachedMineables = new List<ThingDef>();
                    foreach (ThingDef def in DefDatabase<ThingDef>.AllDefs)
                    {
                        if (def.building?.mineableThing != null && def.building.isResourceRock && !InternalDefOf.MineralRich.resourceBlacklist.Contains(def))
                        {
                            cachedMineables.Add(def);
                        }
                    }
                }
                return cachedMineables;
            }
        }

        private Perlin Perlin
        {
            get
            {
                if (perlin == null)
                {
                    perlin = new Perlin(0.40000000596046448, 0.5, 0.5, 4, normalized: true, invert: false, Gen.HashCombineInt(Find.World.info.Seed, 525214866), QualityMode.Medium);
                }
                return perlin;
            }
        }

        public TileMutatorWorker_MineralDevoid(TileMutatorDef def)
            : base(def)
        {
        }

        public override string GetLabel(PlanetTile tile)
        {
            return "VEE_MineralDevoid".Translate(NamedArgumentUtility.Named(GetMineableThingDefForTile(tile), "MINERAL"));
        }

        public override string GetDescription(PlanetTile tile)
        {
            return "VEE_MineralDevoidDescription".Translate(NamedArgumentUtility.Named(GetMineableThingDefForTile(tile), "MINERAL"));
        }

        public override void GeneratePostTerrain(Map map)
        {
            float blotchesPer10kCells = GenStep_RocksFromGrid.GetResourceBlotchesPer10KCellsForMap(map);
            ThingDef defToNOTScatter = GetMineableThingDefForTile(map.Tile);
            List<ThingDef> restOfTheMinerals = Mineables.Where(x => x != defToNOTScatter).ToList();

            foreach (ThingDef mineableToScatter in restOfTheMinerals)
            {
                float totalWeight = Mineables.Sum((ThingDef d) => d.building.mineableScatterCommonality);
                float weight = mineableToScatter.building.mineableScatterCommonality;
                float weightFactor = weight / totalWeight;
                blotchesPer10kCells *= weightFactor;
                int areaPerSpot = Mathf.RoundToInt(10000f / blotchesPer10kCells);
                int mapSize = map.Size.x;
                int actualCount = Mathf.RoundToInt((float)(mapSize * mapSize) / (float)areaPerSpot);
                if (actualCount < 2)
                {
                    blotchesPer10kCells = 10000f / ((float)(mapSize * mapSize) * 0.5f);
                }
                GenStep_ScatterLumpsMineable oreScatterer = new GenStep_ScatterLumpsMineable
                {
                    maxValue = float.MaxValue,
                    countPer10kCellsRange = new FloatRange(blotchesPer10kCells, blotchesPer10kCells),
                    forcedDefToScatter = mineableToScatter
                };
                for (int i = 0; i < 2; i++)
                {
                    oreScatterer.Generate(map, default(GenStepParams));
                }

            }

            foreach (IntVec3 cell in map.AllCells)
            {
                List<Thing> thingsHere = cell.GetThingList(map);
                if (thingsHere.Count > 0)
                {
                    List<Thing> correctThingsHere = thingsHere.Where(x => x.def == defToNOTScatter).ToList();
                    if (correctThingsHere.Count > 0)
                    {
                        Thing thingHere = correctThingsHere.First();


                        if (thingHere != null)
                        {
                            thingHere.Destroy();

                        }
                    }



                }
            }


        }

        private ThingDef GetMineableThingDefForTile(PlanetTile tile)
        {
            List<ThingDef> mineralDefs = Mineables;
            Vector3 center = Find.WorldGrid.GetTileCenter(tile);
            float noiseVal = Perlin.GetValue(center);
            int count = mineralDefs.Count;
            int index = (int)(noiseVal * (float)count);
            if (index >= count)
            {
                index = count - 1;
            }
            return mineralDefs[index];
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_BurnedForest : TileMutatorWorker
    {

        private ModuleBase burnedgroundNoise;

        private const float burnedgroundRadius = 0.3f;

        private const float burnedgroundExp = 0.2f;

        private const float burnedgroundNoiseFrequency = 0.015f;

        private const float burnedgroundNoiseStrength = 35f;

        private const float burnedgroundThreshold = 0f;

        private static readonly FloatRange CenterOffsetRange = new FloatRange(-0.1f, 0.1f);

        public TileMutatorWorker_BurnedForest(TileMutatorDef def)
            : base(def)
        {
        }

        public override void Init(Map map)
        {
            Vector2 offset = map.Center.ToVector2() + new Vector2((float)map.Size.x * CenterOffsetRange.RandomInRange, (float)map.Size.z * CenterOffsetRange.RandomInRange);
            float? num = Find.World.CoastAngleAt(map.Tile, BiomeDefOf.Ocean) ?? Find.World.CoastAngleAt(map.Tile, BiomeDefOf.Lake);
            if (num.HasValue)
            {
                offset = map.Center.ToVector2() + Vector2Utility.FromAngle(num.Value) * map.Size.x * CenterOffsetRange.max;
            }
            burnedgroundNoise = MapNoiseUtility.CreateFalloffRadius((float)map.Size.x * burnedgroundRadius, offset, burnedgroundExp);
            burnedgroundNoise = MapNoiseUtility.AddDisplacementNoise(burnedgroundNoise, burnedgroundNoiseFrequency, burnedgroundNoiseStrength);
            NoiseDebugUI.StoreNoiseRender(burnedgroundNoise, "burnedground");
        }




        public override void GeneratePostFog(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {
                float value = burnedgroundNoise.GetValue(cell);

                if (value > 0f)
                {

                    map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_BurntForestSoil);
                }


                List<Thing> thingsHere = cell.GetThingList(map);
                if (thingsHere.Count > 0)
                {
                    List<Thing> correctThingsHere = thingsHere.Where(x => x.def.plant?.IsTree == true && x.def!= ThingDefOf.Plant_TreeArchean).ToList();
                    if (correctThingsHere.Count > 0)
                    {
                        Thing thingHere = correctThingsHere.First();


                        if (thingHere != null)
                        {
                            Plant plant = thingHere as Plant;
                            plant.TrySpawnStump(PlantDestructionMode.Flame);
                            thingHere.Destroy();


                        }
                    }

                }else if(map.terrainGrid.TerrainAt(cell)== InternalDefOf.VEE_BurntForestSoil && Rand.Chance(0.05f))
                {
                    Thing thing = GenSpawn.Spawn(InternalDefOf.BurnedTree, cell,map);


                }




            }

        }


    }
}
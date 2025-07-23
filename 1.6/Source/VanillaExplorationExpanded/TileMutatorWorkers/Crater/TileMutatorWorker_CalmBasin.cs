
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Noise;


namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_CalmBasin : TileMutatorWorker
    {
        private ModuleBase outerRim;
        private ModuleBase opening;
        private ModuleBase innerSand;

        private const float CraterRadius = 0.4f;

        private const float CraterExponent = 0.2f;

        private const float CraterNoiseFrequency = 0.015f;

        private const float CraterNoiseStrength = 10f;

        private const float CraterThreshold = 0f;

        protected virtual bool generateAshTerrainInCenter => true;

        public virtual FloatRange CenterOffsetRange => new FloatRange(-0.2f, 0.2f);

        public TileMutatorWorker_CalmBasin(TileMutatorDef def)
            : base(def)
        {
        }

        public override void Init(Map map)
        {
            Vector2 center = map.Center.ToVector2() + new Vector2((float)map.Size.x * CenterOffsetRange.RandomInRange, (float)map.Size.z * CenterOffsetRange.RandomInRange);
            float? coastAngle = Find.World.CoastAngleAt(map.Tile, BiomeDefOf.Ocean) ?? Find.World.CoastAngleAt(map.Tile, BiomeDefOf.Lake);
            if (coastAngle.HasValue)
            {
                center = map.Center.ToVector2() + Vector2Utility.FromAngle(coastAngle.Value) * map.Size.x * CenterOffsetRange.max;
            }
            outerRim = MapNoiseUtility.CreateFalloffRadius((float)map.Size.x * CraterRadius, center, CraterExponent);
            outerRim = MapNoiseUtility.AddDisplacementNoise(outerRim, CraterNoiseFrequency, CraterNoiseStrength);

            opening = MapNoiseUtility.CreateFalloffRadius((float)map.Size.x * CraterRadius * 0.8f, center, CraterExponent);
            opening = MapNoiseUtility.AddDisplacementNoise(opening, CraterNoiseFrequency, CraterNoiseStrength);

            innerSand = MapNoiseUtility.CreateFalloffRadius((float)map.Size.x * CraterRadius * 0.4f, center, CraterExponent);
            innerSand = MapNoiseUtility.AddDisplacementNoise(innerSand, CraterNoiseFrequency, CraterNoiseStrength);
            NoiseDebugUI.StoreNoiseRender(outerRim, "outerRim");
            NoiseDebugUI.StoreNoiseRender(opening, "opening");
            NoiseDebugUI.StoreNoiseRender(innerSand, "innerSand");
        }

        public override void GeneratePostElevationFertility(Map map)
        {
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            foreach (IntVec3 cell in map.AllCells)
            {
                float val = outerRim.GetValue(cell);
                if (val > CraterThreshold)
                {
                    elevation[cell] = val;
                }

                float val2 = opening.GetValue(cell);
                if (val2 > 0.5f)
                {
                    elevation[cell] = 0;
                }
            }
            RoofCollapseCellsFinder.CheckAndRemoveCollpsingRoofs(map);
        }

        public override void GeneratePostTerrain(Map map)
        {
            if (generateAshTerrainInCenter)
            {
                foreach (IntVec3 cell in map.AllCells)
                {
                    if (!(innerSand.GetValue(cell) < 0.1f) && !cell.GetTerrain(map).IsWater)
                    {
                        map.terrainGrid.SetTerrain(cell, TerrainDefOf.SoftSand);
                    }
                }
            }

        }




    }
}

using RimWorld;
using UnityEngine;
using Verse;
using Verse.Noise;


namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_Crater : TileMutatorWorker
    {
        private ModuleBase outerRim;
        private ModuleBase opening;
        private ModuleBase innerAsh;

        private const float PlateauRadius = 0.4f;

        private const float PlateauExponent = 0.2f;

        private const float PlateauNoiseFrequency = 0.015f;

        private const float PlateauNoiseStrength = 10f;

        private const float PlateauThreshold = 0f;

        protected virtual bool generateAshTerrainInCenter => true;

        public virtual FloatRange CenterOffsetRange => new FloatRange(-0.2f, 0.2f);

        public TileMutatorWorker_Crater(TileMutatorDef def)
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
            outerRim = MapNoiseUtility.CreateFalloffRadius((float)map.Size.x * PlateauRadius, center, PlateauExponent);
            outerRim = MapNoiseUtility.AddDisplacementNoise(outerRim, PlateauNoiseFrequency, PlateauNoiseStrength);

            opening = MapNoiseUtility.CreateFalloffRadius((float)map.Size.x * PlateauRadius * 0.8f, center, PlateauExponent);
            opening = MapNoiseUtility.AddDisplacementNoise(opening, PlateauNoiseFrequency, PlateauNoiseStrength);

            innerAsh = MapNoiseUtility.CreateFalloffRadius((float)map.Size.x * PlateauRadius * 0.4f, center, PlateauExponent);
            innerAsh = MapNoiseUtility.AddDisplacementNoise(innerAsh, PlateauNoiseFrequency, PlateauNoiseStrength);
            NoiseDebugUI.StoreNoiseRender(outerRim, "outerRim");
            NoiseDebugUI.StoreNoiseRender(opening, "opening");
            NoiseDebugUI.StoreNoiseRender(innerAsh, "innerAsh");
        }

        public override void GeneratePostElevationFertility(Map map)
        {
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            foreach (IntVec3 cell in map.AllCells)
            {
                float val = outerRim.GetValue(cell);
                if (val > PlateauThreshold)
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
                    if (!(innerAsh.GetValue(cell) < 0.1f) && !cell.GetTerrain(map).IsWater)
                    {
                        map.terrainGrid.SetTerrain(cell, InternalDefOf.VEE_VolcanicAsh);
                    }
                }
            }
            
        }




    }
}
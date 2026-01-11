
using RimWorld;
using UnityEngine;
using VEF.Things;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_GreaterTerraformingScar : TileMutatorWorker
    {
        private ModuleBase scar;
       
        private const float scarRadius = 0.3f;

        private const float scarExp = 0.2f;

        private const float scarNoiseFrequency = 0.015f;

        private const float scarNoiseStrength = 35f;

        private const float scarThreshold = 0.75f;
        private const float regolithThreshold = 0.25f;

        public TileMutatorWorker_GreaterTerraformingScar(TileMutatorDef def)
            : base(def)
        {
        }

        public override void Init(Map map)
        {
            base.Init(map);
            Vector2 offset = map.Center.ToVector2() + new Vector2((float)map.Size.x , (float)map.Size.z );
            scar = MapNoiseUtility.CreateFalloffRadius((float)map.Size.x * scarRadius, offset , scarExp);
            scar = new Scale(1, 1, 0.15f, scar);
            scar = MapNoiseUtility.AddDisplacementNoise(scar, scarNoiseFrequency, scarNoiseStrength);
            NoiseDebugUI.StoreNoiseRender(scar, "scar");


        }

        public override void GeneratePostTerrain(Map map)
        {
            
            foreach (IntVec3 c in map.AllCells)
            {
                if (scar.GetValue(c) >= regolithThreshold)
                {
                    map.terrainGrid.SetTerrain(c, InternalDefOf.VEE_VolcanicAsh);
                }
                if (scar.GetValue(c) >= scarThreshold)
                {
                    map.terrainGrid.SetTerrain(c, TerrainDefOf.LavaDeep);
                }
            }
        }


    }
}
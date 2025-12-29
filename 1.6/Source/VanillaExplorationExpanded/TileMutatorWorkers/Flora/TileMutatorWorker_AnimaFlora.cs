
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using VEF.Genes;
using VEF.Maps;
using Verse;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_AnimaFlora : TileMutatorWorker_PlantsWithCommonality
    {

      

        public TileMutatorWorker_AnimaFlora(TileMutatorDef def)
            : base(def)
        {
        }
   
        public override float PlantCommonalityFactorFor(ThingDef plant, PlanetTile tile)
        {
            if (!this.def.plantKinds.Contains(plant)) { return 0; }
            return 1;
        }


    }
}
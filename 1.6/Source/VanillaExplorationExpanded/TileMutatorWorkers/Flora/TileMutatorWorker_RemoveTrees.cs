
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_RemoveTrees : TileMutatorWorker
    {
       
        public TileMutatorWorker_RemoveTrees(TileMutatorDef def)
            : base(def)
        {
        }


        public override float PlantCommonalityFactorFor(ThingDef plant, PlanetTile tile)
        {
            return (plant.plant.IsTree) ? 0f : 1f;
        }

       
    }
}
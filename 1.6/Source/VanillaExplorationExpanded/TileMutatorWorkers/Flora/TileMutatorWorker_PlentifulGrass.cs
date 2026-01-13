
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_PlentifulGrass : TileMutatorWorker
    {

        public TileMutatorWorker_PlentifulGrass(TileMutatorDef def)
            : base(def)
        {
        }


        public override float PlantCommonalityFactorFor(ThingDef plant, PlanetTile tile)
        {
            return (plant.defName.Contains("Grass")) ? 10f : 1f;
        }


    }
}
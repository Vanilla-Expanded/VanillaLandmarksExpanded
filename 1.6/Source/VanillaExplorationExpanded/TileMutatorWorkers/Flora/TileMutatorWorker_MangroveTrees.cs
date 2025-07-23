
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_MangroveTrees : TileMutatorWorker
    {
        private const float PlantCommonality = 5f;

        public TileMutatorWorker_MangroveTrees(TileMutatorDef def)
            : base(def)
        {
        }

        public override IEnumerable<BiomePlantRecord> AdditionalWildPlants(PlanetTile tile)
        {
            yield return new BiomePlantRecord
            {
                plant = GetPlantKind(tile),
                commonality = PlantCommonality
            };
        }

        private ThingDef GetPlantKind(PlanetTile tile)
        {
            Rand.PushState();
            Rand.Seed = tile.GetHashCode();
            ThingDef result = def.plantKinds.RandomElement();
            Rand.PopState();
            return result;
        }
    }
}
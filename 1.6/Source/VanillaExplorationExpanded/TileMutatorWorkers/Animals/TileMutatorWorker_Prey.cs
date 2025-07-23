
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_Prey : TileMutatorWorker
    {
        public virtual float AnimalCommonalityFactor => 10;

        public virtual float MinAnimalCommonality => 0f;

        private readonly List<PawnKindDef> tmpAnimals = new List<PawnKindDef>();

        public TileMutatorWorker_Prey(TileMutatorDef def)
            : base(def)
        {
        }


        public override float AnimalCommonalityFactorFor(PawnKindDef animal, PlanetTile tile)
        {
            return (GetAnimalKind(tile).Contains(animal)) ? AnimalCommonalityFactor : 1f;
        }

        public List<PawnKindDef> GetAnimalKind(PlanetTile tile)
        {
            if (tmpAnimals.Empty())
            {
                BiomeDef biome = Find.WorldGrid[tile].PrimaryBiome;
                foreach (PawnKindDef animal in biome.AllWildAnimals)
                {
                    if (!animal.RaceProps.predator && animal.RaceProps.canBePredatorPrey)
                    {
                        if (biome.CommonalityOfAnimal(animal) > MinAnimalCommonality)
                        {
                            tmpAnimals.Add(animal);
                        }
                        else if (tile.Tile.IsCoastal && biome.CommonalityOfCoastalAnimal(animal) > MinAnimalCommonality)
                        {
                            tmpAnimals.Add(animal);
                        }

                    }

                }
                if (tmpAnimals.Empty())
                {
                    Log.ErrorOnce($"TileMutatorWorker_Prey: Could not find animal in biome with commonality > {MinAnimalCommonality}, either increase some commonalities, or decrease animalDensity in the biomeDef below this mutators min of {def.animalDensityRange.min}", 123498);
                    return null;
                }

            }

            return tmpAnimals;
        }
    }


    public class TileMutatorWorker_ReducedPrey : TileMutatorWorker_Prey
    {
        public override float AnimalCommonalityFactor => 0.25f;


        public TileMutatorWorker_ReducedPrey(TileMutatorDef def)
            : base(def)
        {
        }


    }
}
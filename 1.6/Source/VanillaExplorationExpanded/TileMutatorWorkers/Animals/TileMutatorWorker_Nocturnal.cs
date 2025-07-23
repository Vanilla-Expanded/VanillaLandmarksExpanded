
using System.Collections.Generic;
using VEF.AnimalBehaviours;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_Nocturnal : TileMutatorWorker
    {
        public virtual float AnimalCommonalityFactor => 10;

        public virtual float MinAnimalCommonality => 0f;

        private readonly List<PawnKindDef> tmpAnimals = new List<PawnKindDef>();

        public TileMutatorWorker_Nocturnal(TileMutatorDef def)
            : base(def)
        {
        }


        public override float AnimalCommonalityFactorFor(PawnKindDef animal, PlanetTile tile)
        {
            return (GetAnimalKind(tile).Contains(animal)) ? AnimalCommonalityFactor : 1f;
        }

        public List<PawnKindDef> GetAnimalKind(PlanetTile tile)
        {
            if (tmpAnimals.Empty()) {
                BiomeDef biome = Find.WorldGrid[tile].PrimaryBiome;
                foreach (PawnKindDef animal in biome.AllWildAnimals)
                {
                    ExtendedRaceProperties extension = animal.race.GetModExtension<ExtendedRaceProperties>();

                    if(extension!=null && extension.bodyClock != BodyClock.Diurnal)
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
                    Log.ErrorOnce($"TileMutatorWorker_Nocturnal: Could not find animal in biome with commonality > {MinAnimalCommonality}, either increase some commonalities, or decrease animalDensity in the biomeDef below this mutators min of {def.animalDensityRange.min}", 123498);
                    return null;
                }

            }
       
            return tmpAnimals;
        }
    }


  
}

using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace VanillaExplorationExpanded
{
    public class TileMutatorWorker_Venomous : TileMutatorWorker
    {
        public virtual float AnimalCommonalityFactor => 10;

        public virtual float MinAnimalCommonality => 0f;

        private readonly List<PawnKindDef> tmpAnimals = new List<PawnKindDef>();

        public TileMutatorWorker_Venomous(TileMutatorDef def)
            : base(def)
        {
        }

        public static List<ToolCapacityDef> toolCapacities = new List<ToolCapacityDef>() { InternalDefOf.ToxicBite,InternalDefOf.ScratchToxic,InternalDefOf.AEXP_GilaVenomousBite,
            InternalDefOf.AEXP_RattleSnakeVenomousBite,InternalDefOf.AEXP_MegascorpionVenomousBite,InternalDefOf.AA_ToxicSting,InternalDefOf.AA_ToxicBite,InternalDefOf.AA_VeryToxicSting};


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
                    if (animal.race.tools.Where(x => x.capacities.Intersect(toolCapacities).Any()).Any())
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
                    Log.ErrorOnce($"TileMutatorWorker_Aerie: Could not find animal in biome with commonality > {MinAnimalCommonality}, either increase some commonalities, or decrease animalDensity in the biomeDef below this mutators min of {def.animalDensityRange.min}", 123498);
                    return null;
                }

            }
       
            return tmpAnimals;
        }
    }


  
}
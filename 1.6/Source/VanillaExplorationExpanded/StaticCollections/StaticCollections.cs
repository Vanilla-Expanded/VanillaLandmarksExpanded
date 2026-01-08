
using Verse;
using System;
using RimWorld;
using System.Collections.Generic;
using System.Linq;


namespace VanillaExplorationExpanded
{
    [StaticConstructorOnStartup]
    public static class StaticCollections
    {
        public static List<TerrainDef> sulfuricTerrains = new List<TerrainDef>();
      
        static StaticCollections()
        {
            sulfuricTerrains = DefDatabase<TerrainDef>.AllDefsListForReading.Where(x => x.defName.Contains("VEE_Sulfuric")).ToList();
            
        }

    }
}

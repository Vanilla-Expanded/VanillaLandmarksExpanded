using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using Verse.Noise;
namespace VanillaExplorationExpanded
{

    public class TileMutatorWorker_AlluvialFan : TileMutatorWorker_RiverDelta
    {


        public TileMutatorWorker_AlluvialFan(TileMutatorDef def)
            : base(def)
        {
        }

        public override bool IsValidTile(PlanetTile tile, PlanetLayer layer)
        {
            Tile tileInfo = Find.WorldGrid[tile];
            SurfaceTile surface = tileInfo as SurfaceTile;
            if (surface != null)
            {
                return surface?.Rivers?.Count > 0;
            }
            else return false;

        }

        public override string GetLabel(PlanetTile tile)
        {
            return def.label;
        }



    }
}
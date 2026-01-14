using Verse.Sound;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using System.Security.Cryptography;
using Verse.Noise;

namespace VanillaExplorationExpanded
{
    public class IngestionOutcomeDoer_AnimaPearls : IngestionOutcomeDoer
    {


        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
        {
            if (pawn.Map != null)
            {

                pawn.health.AddHediff(InternalDefOf.VEE_AnimaPearlsHediff);

            }
        }



    }
}
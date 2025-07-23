
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Analytics;
using Verse;

namespace VanillaExplorationExpanded
{
    public class GameCondition_RottenStench : GameCondition
    {
        public int tickCounter = 0;
        public const int tickCounterInterval = 6000;

        public override void GameConditionTick()
        {
            base.GameConditionTick();

            if (tickCounter-- < 0)
            {

                foreach (Map map in base.AffectedMaps)
                {
                    List<Pawn> pawns = map.mapPawns.AllPawns;
                    for (int i = 0; i < pawns.Count; i++)
                    {
                        if (!pawns[i].IsAnimal)
                        {
                            Hediff hediff = pawns[i].health.hediffSet.GetFirstHediffOfDef(InternalDefOf.VEE_RottenStench);
                            if (hediff is null)
                            {
                                pawns[i].health.AddHediff(InternalDefOf.VEE_RottenStench);
                            }
                            else hediff.TryGetComp<HediffComp_Disappears>().ticksToDisappear = 120000;

                        }

                    }
                }
                tickCounter = tickCounterInterval;


            }
        }

    }
}

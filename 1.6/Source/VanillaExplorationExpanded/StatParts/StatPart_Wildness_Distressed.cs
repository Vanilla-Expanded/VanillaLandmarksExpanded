using RimWorld;
using Verse;
namespace VanillaExplorationExpanded
{
    public class StatPart_Wildness_Distressed : StatPart
    {
        public float multiplier = 1.2f;

        public override void TransformValue(StatRequest req, ref float val)
        {
            if (ActiveFor(req.Thing))
            {
                val *= multiplier;
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (req.HasThing && ActiveFor(req.Thing))
            {
                return "StatsReport_MultiplierFor".Translate(InternalDefOf.VEE_DistressedWildlife.label) + (": x" + multiplier.ToStringPercent());
            }
            return null;
        }

        private bool ActiveFor(Thing t)
        {
            return t != null && t is Pawn && t.Map != null && t.MapHeld != null && t.Map.TileInfo.Mutators.Contains(InternalDefOf.VEE_DistressedWildlife);
        }
    }
}

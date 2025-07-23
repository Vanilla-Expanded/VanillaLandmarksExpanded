
using RimWorld;
using Verse;
namespace VanillaExplorationExpanded
{
    public class GameCondition_SkygazingSpot : GameCondition
    {
        public override float SkyGazeChanceFactor(Map map)
        {
          
            return 4f;
        }

        public override float SkyGazeJoyGainFactor(Map map)
        {
          
            return 2.5f;
        }
    }
}

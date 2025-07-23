
using RimWorld;
using Verse;
namespace VanillaExplorationExpanded
{
    public class GameCondition_NoWind : GameCondition
    {
       

        public override float WeatherCommonalityFactor(WeatherDef weather, Map map)
        {
            if (weather == InternalDefOf.Windy)
            {
                return 0f;
            }
            return 1f;
        }
    }
}

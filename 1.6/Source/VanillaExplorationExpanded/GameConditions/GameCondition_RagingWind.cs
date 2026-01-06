
using RimWorld;
using Verse;
namespace VanillaExplorationExpanded
{
    public class GameCondition_RagingWind : GameCondition
    {
        public override float MinWindSpeed()
        {
            return 2f;
        }

        public override float WeatherCommonalityFactor(WeatherDef weather, Map map)
        {
            if (weather == WeatherDefOf.Fog || weather == WeatherDefOf.FoggyRain)
            {
                return 0f;
            }
            return 1f;
        }
    }
}
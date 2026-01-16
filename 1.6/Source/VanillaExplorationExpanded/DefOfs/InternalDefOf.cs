using System;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace VanillaExplorationExpanded
{
	[DefOf]
	public static class InternalDefOf
	{
		static InternalDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
		}

		public static TerrainDef VEE_VolcanicAsh;
        public static TerrainDef VEE_Salt;
        public static TerrainDef VEE_SaltySand;
        public static TerrainDef VEE_DryRiverBed;
        public static TerrainDef VEE_DryRiverBank;
        public static TerrainDef VEE_SulfuricWaterDeep;
        public static TerrainDef VEE_SulfuricWaterShallow;
        public static TerrainDef VEE_SulfuricWaterDeepMovingChestDeep;
        public static TerrainDef VEE_SulfuricWaterMovingShallow;
        public static TerrainDef VEE_BurntForestSoil;
        public static TerrainDef VEE_IrradiatedWaterDeep;
        public static TerrainDef VEE_IrradiatedWaterShallow;
        public static TerrainDef VEE_IrradiatedSoil;
        public static TerrainDef VEE_IrradiatedWaterDeepMovingChestDeep;
        public static TerrainDef VEE_IrradiatedWaterMovingShallow;
        public static TerrainDef VEE_IrradiatedWaterOceanShallow;
        public static TerrainDef VEE_IrradiatedOceanWaterDeep;
        public static TerrainDef VEE_Quicksand;
        public static TerrainDef MarshyTerrain;
        public static TerrainDef VEE_SwampyWaterMovingChestDeep;
        public static TerrainDef VEE_SwampyWaterMovingShallow;
        public static TerrainDef VEE_StagnantRiverWater;
        public static TerrainDef VEE_AnimaSoil;
        public static TerrainDef VEE_AnimaSoilRich;
        public static TerrainDef VEE_AnimaCrackedSoil;
        public static TerrainDef VEE_AnimaWaterDeep;
        public static TerrainDef VEE_AnimaWaterOceanDeep;
        public static TerrainDef VEE_AnimaWaterMovingChestDeep;
        public static TerrainDef VEE_AnimaWaterShallow;
        public static TerrainDef VEE_AnimaWaterOceanShallow;
        public static TerrainDef VEE_AnimaWaterMovingShallow;
        public static TerrainDef VEE_AnimaSand;
        public static TerrainDef VEE_RedSand;
        public static TerrainDef VEE_RedSoftSand;
        public static TerrainDef VEE_VolcanicSand;
        public static TerrainDef VEE_SoftVolcanicSand;
        public static TerrainDef VEE_PebbleDunes;
        public static TerrainDef VEE_LittoralDuneSand;
        public static TerrainDef VEE_TidalFloodwater;

        public static ThingDef VEE_RotstinkMote;
		public static ThingDef VEE_ToxicMote;
		public static ThingDef VEE_SmokeMote;
		public static ThingDef VEE_DeadlifeMote;
        public static ThingDef Tornado;
        public static ThingDef VEE_Coral;
        public static ThingDef BurnedTree;
        [MayRequire("VanillaExpanded.VPsycastsE")]
        public static ThingDef VPE_EltexOre;
        public static ThingDef VEE_DriftwoodChunk;
        public static ThingDef VEE_JadeChunk;
        public static ThingDef VEE_ObsidianChunk;
        public static ThingDef VEE_TidalFlood;
        [MayRequireAnomaly]
        public static ThingDef VEE_InertPitGate;
        public static ThingDef VEE_MineableComponentSpacer;
        public static ThingDef VEE_MineableJadeite;
        public static ThingDef VEE_Filth_Seashells;
        public static ThingDef VEE_DenseSnow;

        public static GameConditionDef VEE_FertileRainsGameCondition;

        public static EffecterDef Impact_Toxic;

		public static ToolCapacityDef ToxicBite;
        public static ToolCapacityDef ScratchToxic;
        [MayRequire("VanillaExpanded.VanillaAnimalsExpanded")]
        public static ToolCapacityDef AEXP_GilaVenomousBite;
        [MayRequire("VanillaExpanded.VanillaAnimalsExpanded")]
        public static ToolCapacityDef AEXP_RattleSnakeVenomousBite;
        [MayRequire("VanillaExpanded.VanillaAnimalsExpanded")]
        public static ToolCapacityDef AEXP_MegascorpionVenomousBite;
        [MayRequire("sarg.alphaanimals")]
        public static ToolCapacityDef AA_ToxicSting;
        [MayRequire("sarg.alphaanimals")]
        public static ToolCapacityDef AA_ToxicBite;
        [MayRequire("sarg.alphaanimals")]
        public static ToolCapacityDef AA_VeryToxicSting;

        public static HediffDef VEE_ColossalFauna;
        public static HediffDef VEE_Microfauna;
        public static HediffDef VEE_RottenStench;
        public static HediffDef VEE_QuicksandHediff;
        public static HediffDef VEE_AnimaPearlsHediff;

        public static TileMutatorDef VEE_Alphabeavers;
        public static TileMutatorDef VEE_MigratoryHerds;
        public static TileMutatorDef VEE_FeralKinship;
        public static TileMutatorDef VEE_DistressedWildlife;
        public static TileMutatorDef VEE_AggressiveHerds;
        public static TileMutatorDef VEE_WanderingCompanions;
        public static TileMutatorDef VEE_IncreasedInfestations;
        public static TileMutatorDef VEE_TornadoAlley;
        public static TileMutatorDef VEE_NoWind;
        public static TileMutatorDef VEE_FertileRains;
        public static TileMutatorDef VEE_MoreSolarPower;
        public static TileMutatorDef VEE_LessSolarPower;
        public static TileMutatorDef VEE_FrequentAuroras;
        public static TileMutatorDef VEE_RisingWaters;

        public static WeatherDef Windy;

        public static IncidentDef VEE_TidalFloodingIncident;
    }
}

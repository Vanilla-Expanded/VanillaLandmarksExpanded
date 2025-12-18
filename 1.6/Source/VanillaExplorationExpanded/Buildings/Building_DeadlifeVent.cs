using System;
using Verse;
using Verse.Sound;
using RimWorld;


namespace VanillaExplorationExpanded
{
    public class Building_DeadlifeVent : Building
    {

        private DeadlifeSprayer steamSprayer;

        private Sustainer spraySustainer;

        private int spraySustainerStartTick = -999;


        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.steamSprayer = new DeadlifeSprayer(this);
            this.steamSprayer.startSprayCallback = new Action(this.StartSpray);
            this.steamSprayer.endSprayCallback = new Action(this.EndSpray);
        }

        private void StartSpray()
        {
            WeatherBuildupUtility.AddSnowRadial(this.OccupiedRect().RandomCell, base.Map, 4f, -0.06f);
            this.spraySustainer = SoundDefOf.GeyserSpray.TrySpawnSustainer(new TargetInfo(base.Position, base.Map, false));
            this.spraySustainerStartTick = Find.TickManager.TicksGame;
            GasUtility.AddDeadifeGas(Position, Map, Faction.OfEntities, 7500);


        }

        private void EndSpray()
        {
            if (this.spraySustainer != null)
            {
                this.spraySustainer.End();
                this.spraySustainer = null;
            }
        }

        protected override void Tick()
        {

            this.steamSprayer?.SteamSprayerTick();

            if (Find.TickManager.TicksGame > this.spraySustainerStartTick + 5000)
            {
                Log.Message("Toxic spray sustainer still playing after 1000 ticks. Force-ending.");
                this.spraySustainer?.End();
                this.spraySustainer = null;
            }
        }




    }
}

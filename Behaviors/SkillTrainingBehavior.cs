using Helpers;
using SkillTrainingMod.GameObjects;
using SkillTrainingMod.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace SkillTrainingMod.Behaviors
{
    public class SkillTrainingBehavior : CampaignBehaviorBase
    {
        public SkillTrainingState skillTrainingState;

        private static int SkillPointfor0to100 = 6;
        private static int GoldCostForSkill0to100 = 300 * SkillPointfor0to100;
        private static int SkillPointfor101to200 = 3;
        private static int GoldCostForSkill101to200 = 900 * SkillPointfor101to200;
        private static int SkillPointfor201to300 = 1;
        private static int GoldCostForSkill201to300 = 1800 * SkillPointfor201to300;
        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, OnSessionLaunched);
            CampaignEvents.TickEvent.AddNonSerializedListener(this, OnTickEvent);
            CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, OnDailyTickEvent);
        }

        private void OnDailyTickEvent()
        {
            var skillTrainingState = GetSkillTrainingState();
            var heroInTraingings = skillTrainingState.HerosInTrainging;

            if (heroInTraingings != null && heroInTraingings.Count > 0)
            {
                heroInTraingings.ForEach(hero =>
                {
                    int heroGold = Hero.MainHero.Gold;
                    var skills = hero.Skills;

                    var gameHero = Hero.FindFirst(x => x.Id.ToString() == hero.HeroId);
                    
                    foreach(var skill in skills)
                    {
                        var skillXp = gameHero.GetSkillValue(skill);

                        if (skillXp >= 0 && skillXp <= 100)
                        {
                            gameHero.AddSkillXp(skill, SkillPointfor0to100);
                            Hero.MainHero.ChangeHeroGold(-GoldCostForSkill0to100);
                        }
                        else if (skillXp >= 101 && skillXp <= 200)
                        {
                            gameHero.AddSkillXp(skill, SkillPointfor101to200);
                            Hero.MainHero.ChangeHeroGold(-GoldCostForSkill101to200);
                        }
                        else if (skillXp >= 101 && skillXp <= 200)
                        {
                            gameHero.AddSkillXp(skill, SkillPointfor201to300);
                            Hero.MainHero.ChangeHeroGold(-GoldCostForSkill201to300);
                        }
                    }
                });
            }
        }

        private void OnTickEvent(float obj)
        {
            var state = GameStateManager.Current.CreateState<SkillTrainingPanelState>(new object[]
                {
                    GetSkillTrainingState()
                });


            if (Input.IsKeyPressed(InputKey.A))
            {
                GameStateManager.Current.PushState(state);
            }
        }

        public SkillTrainingState GetSkillTrainingState()
        {
            if(skillTrainingState == null)
            {
                skillTrainingState = new SkillTrainingState();
            }

            return skillTrainingState;
        }

        private void OnSessionLaunched(CampaignGameStarter starter)
        {

        }
        public override void SyncData(IDataStore dataStore)
        {
            //dataStore.SyncData("SkillTrainingState", ref skillTrainingState);
        }

    }

}

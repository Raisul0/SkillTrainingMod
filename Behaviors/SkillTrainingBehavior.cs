using SkillTrainingMod.GameObjects;
using SkillTrainingMod.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
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

        private static float _personalSkillXpIncrement = 6;
        private static int _personalSkillGoldCost = 300 * (int)_personalSkillXpIncrement;
        private static float _partySkillXpIncrement = 3;
        private static int _partySkillGoldCost = 900 * (int)_partySkillXpIncrement;
        private static float _leaderSkillXpIncrement = 1;
        private static int _leaderSkillGoldCost = 1800 * (int)_leaderSkillXpIncrement;
        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, OnSessionLaunched);
            CampaignEvents.TickEvent.AddNonSerializedListener(this, OnTickEvent);
            CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, OnDailyTickEvent);
        }

        private void OnDailyTickEvent()
        {
            var boastedSkills = GetSkillTrainingState().BoastedSkills;
            if (boastedSkills != null  && boastedSkills.Count > 0)
            {
                boastedSkills.ForEach(skill => {

                    int heroGold = Hero.MainHero.Gold;
                    float xp = 0;
                    int goldCost = 0;

                    if (skill.IsPersonalSkill) { xp = _personalSkillXpIncrement; goldCost = _personalSkillGoldCost; }
                    else if (skill.IsPartySkill) { xp = _partySkillXpIncrement; goldCost = _partySkillGoldCost; }
                    else if (skill.IsLeaderSkill) {  xp = _leaderSkillXpIncrement;goldCost = _leaderSkillGoldCost; };

                    if(heroGold > goldCost)
                    {
                        Hero.MainHero.ChangeHeroGold(-goldCost);
                        Hero.MainHero.AddSkillXp(skill, xp);
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
            dataStore.SyncData("skillTrainingState", ref skillTrainingState);
        }

    }

}

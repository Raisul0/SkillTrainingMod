using Helpers;
using SkillTrainingMod.GameObjects;
using SkillTrainingMod.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
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
            var totalGoldCost = 0;
            var skillTrainingState = GetSkillTrainingState();
            var skillsInTraining = skillTrainingState.SkillsInTraining;

            if (skillsInTraining != null && skillsInTraining.Count > 0)
            {
                for(int i = 0; i < skillsInTraining.Count; i++)
                {
                    var sk = skillsInTraining[i];
                    int heroGold = Hero.MainHero.Gold;
                    var xpIncremeant = 0;
                    var goldCost = 0;

                    var skillXp = sk.Developer.Hero.GetSkillValue(sk.Skill);

                    if (skillXp >= 0 && skillXp <= 100)
                    {
                        xpIncremeant = SkillPointfor0to100;
                        goldCost = GoldCostForSkill0to100;
                    }
                    else if (skillXp >= 101 && skillXp <= 200)
                    {
                        xpIncremeant = SkillPointfor101to200;
                        goldCost = GoldCostForSkill101to200;
                    }
                    else if (skillXp >= 101 && skillXp <= 200)
                    {
                        xpIncremeant = SkillPointfor201to300;
                        goldCost = GoldCostForSkill201to300;
                    }

                    totalGoldCost += goldCost;

                    if (heroGold >= totalGoldCost)
                    {
                        IncreaseCharacterAndSkillXP((HeroDeveloper)sk.Developer.Hero.HeroDeveloper, sk.Skill, xpIncremeant);
                        //sk.Developer.Hero.HeroDeveloper.ChangeSkillLevel(sk.Skill,xpIncremeant);
                        //SkillLevelingManager.OnHeroHealedWhileWaiting(sk.Developer.Hero, xpIncremeant);
                    }
                    else
                    {
                        break;
                    }
                }

                Hero.MainHero.ChangeHeroGold(-totalGoldCost);

                TextObject textObject = new TextObject("{=dPD5zood}Daily Gold Cost For Skill Training: {CHANGE}{GOLD_ICON}", null);
                textObject.SetTextVariable("CHANGE", -totalGoldCost);
                textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
                string soundEventPath = (-totalGoldCost > 0) ? "event:/ui/notification/coins_positive" : ((-totalGoldCost == 0) ? string.Empty : "event:/ui/notification/coins_negative");
                InformationManager.DisplayMessage(new InformationMessage(textObject.ToString(), soundEventPath));

            }
        }

        private void IncreaseCharacterAndSkillXP(HeroDeveloper heroDeveloper,SkillObject skill,int changeAmount)
        {
            int skillValue = heroDeveloper.Hero.GetSkillValue(skill);
            int num = skillValue + changeAmount;
            float num2 = 0f;
            float propertyValue = heroDeveloper.GetPropertyValue(skill);
            num2 -= propertyValue - (float)Campaign.Current.Models.CharacterDevelopmentModel.GetXpRequiredForSkillLevel(skillValue);
            for (int i = skillValue + 1; i <= num; i++)
            {
                num2 += (float)(Campaign.Current.Models.CharacterDevelopmentModel.GetXpRequiredForSkillLevel(i) - Campaign.Current.Models.CharacterDevelopmentModel.GetXpRequiredForSkillLevel(i - 1));
            }

            heroDeveloper.AddSkillXp(skill, num2 + 1f, isAffectedByFocusFactor: false, true);
            SkillLevelingManager.OnHeroHealedWhileWaiting(heroDeveloper.Hero, (int)num2);

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

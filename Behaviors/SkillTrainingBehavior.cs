using Helpers;
using SkillTrainingMod.Extensions;
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
                    int heroGold = Hero.MainHero.Gold-SkillTrainingModState.MinHeroGold;
                    var xpIncremeant = 0;
                    var goldCost = 0;

                    var skillXp = sk.Developer.Hero.GetSkillValue(sk.Skill);

                    if (skillXp >= 0 && skillXp < 100)
                    {
                        xpIncremeant = SkillTrainingModState.SkillPointfor1to100;
                        if(skillXp+ xpIncremeant > 100)
                        {
                            xpIncremeant -= skillXp + xpIncremeant - 100;
                        }
                        goldCost = SkillTrainingModState.GoldCostForSkill1to100 * xpIncremeant;
                    }
                    else if (skillXp >= 100 && skillXp < 200)
                    {
                        xpIncremeant = SkillTrainingModState.SkillPointfor101to200;
                        if (skillXp + xpIncremeant > 200)
                        {
                            xpIncremeant -= skillXp + xpIncremeant - 200;
                        }
                        goldCost = SkillTrainingModState.GoldCostForSkill101to200 * xpIncremeant;
                    }
                    else if (skillXp >= 200 && skillXp < 300)
                    {
                        xpIncremeant = SkillTrainingModState.SkillPointfor201to300;
                        if (skillXp + xpIncremeant > 300)
                        {
                            xpIncremeant -= skillXp + xpIncremeant - 300;
                        }
                        goldCost = SkillTrainingModState.GoldCostForSkill201to300 * xpIncremeant;
                    }

                    if(skillXp + xpIncremeant > SkillTrainingModState.MaxSkillLevel)
                    {
                        InformationManager.DisplayMessage(new InformationMessage("Skill Training Level Will Exceed Allowed Level!"));
                        continue;
                    }

                    
                    if (heroGold >= goldCost)
                    {
                        totalGoldCost += goldCost;
                        IncreaseCharacterAndSkillXP((HeroDeveloper)sk.Developer.Hero.HeroDeveloper, sk.Skill, xpIncremeant);
                        MapNotificationForSkillTrainingGoldCost(sk.Skill.Name, goldCost);
                        Hero.MainHero.ChangeHeroGold(-goldCost);
                    }
                    else
                    {
                        skillTrainingState.RemoveSkill(sk);
                        InformationManager.DisplayMessage(new InformationMessage("Not Enough Gold!"));
                        continue;
                    }
                }
            }
        }

        private void MapNotificationForSkillTrainingGoldCost(TextObject skillName,int goldAmount)
        { 
            TextObject textObject = new TextObject("{=dPD5zood}Daily Gold Cost For Skill Training of " + skillName.ToString() + " : {CHANGE}{GOLD_ICON}", null);
            textObject.SetTextVariable("CHANGE", -goldAmount);
            textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
            string soundEventPath = (-goldAmount > 0) ? "event:/ui/notification/coins_positive" : ((-goldAmount == 0) ? string.Empty : "event:/ui/notification/coins_negative");
            InformationManager.DisplayMessage(new InformationMessage(textObject.ToString(), soundEventPath));
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

            heroDeveloper.AddSkillXp(skill, num2 + 1f, false, false);
            HeroDeveloperExtension.ModifyHeroXp(heroDeveloper, num2 + 1f);
            //SkillLevelingManager.OnHeroHealedWhileWaiting(heroDeveloper.Hero, (int)num2);
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

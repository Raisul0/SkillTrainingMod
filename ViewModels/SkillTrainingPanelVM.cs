using Messages.FromCustomBattleServer.ToCustomBattleServerManager;
using SkillTrainingMod.GameObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;

namespace SkillTrainingMod.ViewModels
{
    public class SkillTrainingPanelVM : ViewModel
    {
        public SkillTrainingPanelVM(SkillTrainingState skillTrainingState)
        {
            this.SkillTrainingState = skillTrainingState;
            IsVisible = false;
            this.SetCurrentHero( new CharacterVM(Hero.MainHero, new Action(this.OnPerkSelection)));
            this.SkillsText = GameTexts.FindText("str_skills", null).ToString();
            this.SkillFocusText = GameTexts.FindText("str_character_skill_focus", null).ToString();
            this.UnspentCharacterPointsHint = new HintViewModel(GameTexts.FindText("str_character_points_how_to_get", null), null);
            this.UnspentAttributePointsHint = new HintViewModel(GameTexts.FindText("str_attribute_points_how_to_get", null), null);
            this.CurrentCharacter.RefreshValues();
            this.SetSkills();
        }

        private void OnPerkSelection()
        {
            this.RefreshCharacterSelector();
        }

        // Token: 0x06001C2D RID: 7213 RVA: 0x0006610C File Offset: 0x0006430C
        private void RefreshCharacterSelector()
        {
            //List<string> list = new List<string>();
            //for (int i = 0; i < this._heroList.Count; i++)
            //{
            //    string text = this._heroList[i].HeroNameText;
            //    if (this._heroList[i].GetNumberOfUnselectedPerks() > 0)
            //    {
            //        text = GameTexts.FindText("str_STR1_space_STR2", null).SetTextVariable("STR1", text).SetTextVariable("STR2", "{=!}<img src=\"CharacterDeveloper\\UnselectedPerksIcon\" extend=\"2\">").ToString();
            //    }
            //    list.Add(text);
            //}
            //this.CharacterList.Refresh(list, this._heroIndex, new Action<SelectorVM<SelectorItemVM>>(this.OnCharacterSelection));
        }

        private void SetCurrentHero(CharacterVM currentHero)
        {
            //CharacterVM currentCharacter = this.CurrentCharacter;
            //SkillObject prevSkill;
            //if (currentCharacter == null)
            //{
            //    prevSkill = null;
            //}
            //else
            //{
            //    SkillVM skillVM = currentCharacter.Skills.FirstOrDefault((SkillVM s) => s.IsInspected);
            //    prevSkill = ((skillVM != null) ? skillVM.Skill : null);
            //}
            //CS$<> 8__locals2.prevSkill = prevSkill;
            this.CurrentCharacter = currentHero;
   //         if (CS$<> 8__locals1.prevSkill != null)
			//{
   //             CharacterVM currentCharacter2 = this.CurrentCharacter;
   //             if (currentCharacter2 == null)
   //             {
   //                 return;
   //             }
   //             currentCharacter2.SetCurrentSkill(this.CurrentCharacter.Skills.FirstOrDefault((SkillVM s) => s.Skill == CS$<> 8__locals1.prevSkill));
   //         }
        }

        public void SetSkills()
        {
            var skillTrainingSkills = new MBBindingList<SkillTrainingSkillVM>();
            var skills = this.CurrentCharacter.Skills;

            foreach (var skill in skills)
            {
                skillTrainingSkills.Add(new SkillTrainingSkillVM(ref SkillTrainingState, skill.Skill,this.CurrentCharacter,new Action<PerkVM>(this.OnSkillPerkSelection)));
            }
            this.Skills = skillTrainingSkills;
        }

        public void OnSkillPerkSelection(PerkVM perk)
        {

        }

        public void ExecuteOnSelected(SkillTrainingSkillVM selectedSkill)
        {

        }
        public void ExecuteCancel()
        {
            GameStateManager.Current.PopState();
        }

        public void ExecuteDone()
        {

            GameStateManager.Current.PopState();
        }


        [DataSourceProperty]
        public bool IsVisible
        {
            get
            {
                return this._isVisiable;
            }
            set
            {
                if (value != this._isVisiable)
                {
                    this._isVisiable = value;
                    base.OnPropertyChangedWithValue(value, "IsVisible");
                }
            }
        }

        [DataSourceProperty]
        public string SkillsText
        {
            get
            {
                return this._skillsText;
            }
            set
            {
                if (value != this._skillsText)
                {
                    this._skillsText = value;
                    base.OnPropertyChangedWithValue<string>(value, "SkillsText");
                }
            }
        }

        [DataSourceProperty]
        public string SkillFocusText
        {
            get
            {
                return this._skillFocusText;
            }
            set
            {
                if (value != this._skillFocusText)
                {
                    this._skillFocusText = value;
                    base.OnPropertyChangedWithValue<string>(value, "SkillFocusText");
                }
            }
        }

        [DataSourceProperty]
        public CharacterVM CurrentCharacter
        {
            get
            {
                return this._currentCharacter;
            }
            set
            {
                if (value != this._currentCharacter)
                {
                    this._currentCharacter = value;
                    this.CurrentCharacterNameText = this._currentCharacter.HeroNameText;
                    base.OnPropertyChangedWithValue<CharacterVM>(value, "CurrentCharacter");
                }
            }
        }

        [DataSourceProperty]
        public string CurrentCharacterNameText
        {
            get
            {
                return this._currentCharacterNameText;
            }
            set
            {
                if (value != this._currentCharacterNameText)
                {
                    this._currentCharacterNameText = value;
                    base.OnPropertyChangedWithValue<string>(value, "CurrentCharacterNameText");
                }
            }
        }



        [DataSourceProperty]
        public HintViewModel UnspentCharacterPointsHint
        {
            get
            {
                return this._unspentCharacterPointsHint;
            }
            set
            {
                if (value != this._unspentCharacterPointsHint)
                {
                    this._unspentCharacterPointsHint = value;
                    base.OnPropertyChangedWithValue<HintViewModel>(value, "UnspentCharacterPointsHint");
                }
            }
        }

        [DataSourceProperty]
        public HintViewModel UnspentAttributePointsHint
        {
            get
            {
                return this._unspentAttributePointsHint;
            }
            set
            {
                if (value != this._unspentAttributePointsHint)
                {
                    this._unspentAttributePointsHint = value;
                    base.OnPropertyChangedWithValue<HintViewModel>(value, "UnspentAttributePointsHint");
                }
            }
        }

        [DataSourceProperty]
        public MBBindingList<SkillTrainingSkillVM> Skills
        {
            get
            {
                return _skills;
            }
            set
            {
                if (value != _skills)
                {
                    _skills = value;
                    OnPropertyChangedWithValue(value, "Skills");
                }
            }
        }

        public SkillTrainingState SkillTrainingState;

        #region Private Properties 

        private bool _isVisiable;
        private string _skillsText;
        private string _skillFocusText;
        private CharacterVM _currentCharacter;
        private string _currentCharacterNameText;
        private HintViewModel _unspentCharacterPointsHint;
        private HintViewModel _unspentAttributePointsHint;
        private MBBindingList<SkillTrainingSkillVM> _skills;
        #endregion
    }
}

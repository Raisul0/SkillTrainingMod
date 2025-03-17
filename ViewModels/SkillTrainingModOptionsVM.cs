using SkillTrainingMod.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions;

namespace SkillTrainingMod.ViewModels
{
    public class SkillTrainingModOptionsVM : ViewModel
    {
        private readonly Action _onClose;

        private string _titleText;

        private string _doneText;

        private string _resetTutorialText;



        private SkillTrainingModOptionsControllerVM _optionsController;

        private MBBindingList<ViewModel> _options;

        [DataSourceProperty]
        public SkillTrainingModOptionsControllerVM OptionsController
        {
            get
            {
                return _optionsController;
            }
            set
            {
                if (value != _optionsController)
                {
                    _optionsController = value;
                    OnPropertyChangedWithValue(value, "OptionsController");
                }
            }
        }

        private SkillTrainingModNumericOptionsDataVM _maxSkillLevel { get; set; }

        [DataSourceProperty]
        public SkillTrainingModNumericOptionsDataVM MaxSkillLevel
        {
            get
            {
                return _maxSkillLevel;
            }
            set
            {
                if (value != _maxSkillLevel)
                {
                    _maxSkillLevel = value;
                    OnPropertyChangedWithValue(value, "MaxSkillLevel");
                }
            }
        }

        private SkillTrainingModNumericOptionsDataVM _goldCostFor1to100 { get; set; }

        [DataSourceProperty]
        public SkillTrainingModNumericOptionsDataVM GoldCostFor1to100
        {
            get
            {
                return _goldCostFor1to100;
            }
            set
            {
                if (value != _goldCostFor1to100)
                {
                    _goldCostFor1to100 = value;
                    OnPropertyChangedWithValue(value, "GoldCostFor1to100");
                }
            }
        }

        private SkillTrainingModNumericOptionsDataVM _goldCostFor101to200 { get; set; }

        [DataSourceProperty]
        public SkillTrainingModNumericOptionsDataVM GoldCostFor101to200
        {
            get
            {
                return _goldCostFor101to200;
            }
            set
            {
                if (value != _goldCostFor101to200)
                {
                    _goldCostFor101to200 = value;
                    OnPropertyChangedWithValue(value, "GoldCostFor101to200");
                }
            }
        }

        private SkillTrainingModNumericOptionsDataVM _goldCostFor201to300 { get; set; }

        [DataSourceProperty]
        public SkillTrainingModNumericOptionsDataVM GoldCostFor201to300
        {
            get
            {
                return _goldCostFor201to300;
            }
            set
            {
                if (value != _goldCostFor201to300)
                {
                    _goldCostFor201to300 = value;
                    OnPropertyChangedWithValue(value, "GoldCostFor201to300");
                }
            }
        }

        private SkillTrainingModNumericOptionsDataVM _dailyLevelUp1to100 { get; set; }

        [DataSourceProperty]
        public SkillTrainingModNumericOptionsDataVM DailyLevelUp1to100
        {
            get
            {
                return _dailyLevelUp1to100;
            }
            set
            {
                if (value != _dailyLevelUp1to100)
                {
                    _dailyLevelUp1to100 = value;
                    OnPropertyChangedWithValue(value, "DailyLevelUp1to100");
                }
            }
        }

        private SkillTrainingModNumericOptionsDataVM _dailyLevelUp101to200 { get; set; }

        [DataSourceProperty]
        public SkillTrainingModNumericOptionsDataVM DailyLevelUp101to200
        {
            get
            {
                return _dailyLevelUp101to200;
            }
            set
            {
                if (value != _dailyLevelUp101to200)
                {
                    _dailyLevelUp101to200 = value;
                    OnPropertyChangedWithValue(value, "DailyLevelUp101to200");
                }
            }
        }

        private SkillTrainingModNumericOptionsDataVM _dailyLevelUp201to300 { get; set; }

        [DataSourceProperty]
        public SkillTrainingModNumericOptionsDataVM DailyLevelUp201to300
        {
            get
            {
                return _dailyLevelUp201to300;
            }
            set
            {
                if (value != _dailyLevelUp201to300)
                {
                    _dailyLevelUp201to300 = value;
                    OnPropertyChangedWithValue(value, "DailyLevelUp201to300");
                }
            }
        }

        private SkillTrainingModNumericOptionsDataVM _heroMinGold { get; set; }

        [DataSourceProperty]
        public SkillTrainingModNumericOptionsDataVM HeroMinGold
        {
            get
            {
                return _heroMinGold;
            }
            set
            {
                if (value != _heroMinGold)
                {
                    _heroMinGold = value;
                    OnPropertyChangedWithValue(value, "HeroMinGold");
                }
            }
        }

        [DataSourceProperty]
        public string TitleText
        {
            get
            {
                return _titleText;
            }
            set
            {
                if (value != _titleText)
                {
                    _titleText = value;
                    OnPropertyChangedWithValue(value, "TitleText");
                }
            }
        }

        [DataSourceProperty]
        public string DoneText
        {
            get
            {
                return _doneText;
            }
            set
            {
                if (value != _doneText)
                {
                    _doneText = value;
                    OnPropertyChangedWithValue(value, "DoneText");
                }
            }
        }

        [DataSourceProperty]
        public MBBindingList<ViewModel> Options
        {
            get
            {
                return _options;
            }
            set
            {
                if (value != _options)
                {
                    _options = value;
                    OnPropertyChangedWithValue(value, "Options");
                }
            }
        }



        public SkillTrainingModOptionsVM(Action onClose)
        {
            _onClose = onClose;
            MBBindingList<ViewModel> mBBindingList = new MBBindingList<ViewModel>();

            this.MaxSkillLevel = new SkillTrainingModNumericOptionsDataVM(1,300,SkillTrainingModState.MaxSkillLevel, 1,"Max Skill Level",new TextObject("Description 1"));
            this.GoldCostFor1to100 = new SkillTrainingModNumericOptionsDataVM(100, 10000, SkillTrainingModState.GoldCostForSkill1to100, 5, "Gold Cost Per Level (1-100)", new TextObject("Description 2"));
            this.GoldCostFor101to200 = new SkillTrainingModNumericOptionsDataVM(100, 20000, SkillTrainingModState.GoldCostForSkill101to200, 5, "Gold Cost Per Level (101-200)", new TextObject("Description 3"));
            this.GoldCostFor201to300 = new SkillTrainingModNumericOptionsDataVM(100, 30000, SkillTrainingModState.GoldCostForSkill201to300, 5,"Gold Cost Per Level (201-300)", new TextObject("Description 4"));
            this.DailyLevelUp1to100 =   new SkillTrainingModNumericOptionsDataVM(1, 100, SkillTrainingModState.SkillPointfor1to100, 1, "Daily Level Up (1-100)",    new TextObject("Description 2"));
            this.DailyLevelUp101to200 = new SkillTrainingModNumericOptionsDataVM(1, 100, SkillTrainingModState.SkillPointfor101to200, 1, "Daily Level Up (101-200)",  new TextObject("Description 3"));
            this.DailyLevelUp201to300 = new SkillTrainingModNumericOptionsDataVM(1, 100, SkillTrainingModState.SkillPointfor201to300, 1, "Daily Level Up (201-300)",  new TextObject("Description 4"));
            this.HeroMinGold = new SkillTrainingModNumericOptionsDataVM(100, 10000, SkillTrainingModState.MinHeroGold, 10, "Minimum Hero Gold", new TextObject("Description 4"));
            //mBBindingList.Add(maxSkillLevel);
            //Options = mBBindingList;

            RefreshValues();
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            TitleText = new TextObject("{=PXT6aA4J}Mod Options").ToString();
            DoneText = GameTexts.FindText("str_done").ToString();
            //OptionsController.RefreshValues();
        }

        public void ExecuteDone()
        {
            _onClose?.Invoke();


            SkillTrainingModState.MaxSkillLevel = Convert.ToInt32(this.MaxSkillLevel.OptionValue);
            SkillTrainingModState.GoldCostForSkill1to100 = Convert.ToInt32(this.GoldCostFor1to100.OptionValue);
            SkillTrainingModState.GoldCostForSkill101to200 = Convert.ToInt32(this.GoldCostFor101to200.OptionValue);
            SkillTrainingModState.GoldCostForSkill201to300 = Convert.ToInt32(this.GoldCostFor201to300.OptionValue) ;
            SkillTrainingModState.SkillPointfor1to100 = Convert.ToInt32(this.DailyLevelUp1to100.OptionValue);
            SkillTrainingModState.SkillPointfor101to200 = Convert.ToInt32(this.DailyLevelUp101to200.OptionValue);
            SkillTrainingModState.SkillPointfor201to300 = Convert.ToInt32(this.DailyLevelUp201to300.OptionValue);
            SkillTrainingModState.MinHeroGold = Convert.ToInt32(this.HeroMinGold.OptionValue);


        }
    }
}

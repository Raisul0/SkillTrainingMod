using SkillTrainingMod.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Library;

namespace SkillTrainingMod.ViewModels
{
    public class SkillTrainingModOptionsControllerVM : ViewModel
    {
        private class CampaignOptionComparer : IComparer<SkillTrainingModOptionsItemVM>
        {
            public int Compare(SkillTrainingModOptionsItemVM x, SkillTrainingModOptionsItemVM y)
            {
                int priorityIndex = x.OptionData.GetPriorityIndex();
                int priorityIndex2 = y.OptionData.GetPriorityIndex();
                return priorityIndex.CompareTo(priorityIndex2);
            }
        }

        private const string _difficultyPresetsId = "DifficultyPresets";

        internal const int AutosaveDisableValue = -1;

        private SelectionCampaignOptionData _difficultyPreset;

        private Dictionary<string, SkillTrainingModOptionsItemVM> _optionItems;

        private bool _isUpdatingPresetData;

        private List<SkillTrainingModOptionsItemVM> _difficultyPresetRelatedOptions;

        private MBBindingList<SkillTrainingModOptionsItemVM> _options;

        [DataSourceProperty]
        public MBBindingList<SkillTrainingModOptionsItemVM> Options
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

        public SkillTrainingModOptionsControllerVM(MBBindingList<SkillTrainingModOptionsItemVM> options)
        {
            _optionItems = new Dictionary<string, SkillTrainingModOptionsItemVM>();
            Options = options;
            _difficultyPreset = Options.FirstOrDefault((SkillTrainingModOptionsItemVM x) => x.OptionData.GetIdentifier() == "DifficultyPresets")?.OptionData as SelectionCampaignOptionData;
            Options.Sort(new CampaignOptionComparer());
            for (int i = 0; i < Options.Count; i++)
            {
                _optionItems.Add(Options[i].OptionData.GetIdentifier(), Options[i]);
            }

            Options.ApplyActionOnAllItems(delegate (SkillTrainingModOptionsItemVM x)
            {
                x.RefreshDisabledStatus();
            });
            Options.ApplyActionOnAllItems(delegate (SkillTrainingModOptionsItemVM x)
            {
                x.SetOnValueChangedCallback(OnOptionChanged);
            });
            _difficultyPresetRelatedOptions = Options.Where((SkillTrainingModOptionsItemVM x) => x.OptionData.IsRelatedToDifficultyPreset()).ToList();
            UpdatePresetData(_difficultyPresetRelatedOptions.FirstOrDefault());
        }

        public override void OnFinalize()
        {
            base.OnFinalize();
            CampaignOptionsManager.ClearCachedOptions();
        }

        private void OnOptionChanged(SkillTrainingModOptionsItemVM optionVM)
        {
            UpdatePresetData(optionVM);
            Options.ApplyActionOnAllItems(delegate (SkillTrainingModOptionsItemVM x)
            {
                x.RefreshDisabledStatus();
            });
        }

        private void UpdatePresetData(SkillTrainingModOptionsItemVM changedOption)
        {
            if (_isUpdatingPresetData || changedOption == null || !_optionItems.TryGetValue(_difficultyPreset.GetIdentifier(), out var value))
            {
                return;
            }

            _isUpdatingPresetData = true;
            if (changedOption.OptionData == _difficultyPreset)
            {
                foreach (SkillTrainingModOptionsItemVM difficultyPresetRelatedOption in _difficultyPresetRelatedOptions)
                {
                    string identifier = difficultyPresetRelatedOption.OptionData.GetIdentifier();
                    CampaignOptionsDifficultyPresets preset = (CampaignOptionsDifficultyPresets)_difficultyPreset.GetValue();
                    float valueFromDifficultyPreset = difficultyPresetRelatedOption.OptionData.GetValueFromDifficultyPreset(preset);
                    if (_optionItems.TryGetValue(identifier, out var value2) && !value2.IsDisabled)
                    {
                        value2.SetValue(valueFromDifficultyPreset);
                    }
                }
            }
            //else if (_difficultyPresetRelatedOptions.Any((SkillTrainingModOptionsItemVM x) => x.OptionData.GetIdentifier() == changedOption.OptionData.GetIdentifier()))
            //{
            //    SkillTrainingModOptionsItemVM SkillTrainingModOptionsItemVM = _difficultyPresetRelatedOptions[0];
            //    CampaignOptionsDifficultyPresets campaignOptionsDifficultyPresets = FindOptionPresetForValue(SkillTrainingModOptionsItemVM.OptionData);
            //    bool flag = true;
            //    for (int i = 0; i < _difficultyPresetRelatedOptions.Count; i++)
            //    {
            //        if (FindOptionPresetForValue(_difficultyPresetRelatedOptions[i].OptionData) != campaignOptionsDifficultyPresets)
            //        {
            //            flag = false;
            //            break;
            //        }
            //    }

            //    value.SetValue(flag ? ((float)campaignOptionsDifficultyPresets) : 3f);
            //}

            _isUpdatingPresetData = false;
        }

        private CampaignOptionsDifficultyPresets FindOptionPresetForValue(IModOptionsData option)
        {
            float value = option.GetValue();
            if (option.GetValueFromDifficultyPreset(CampaignOptionsDifficultyPresets.Freebooter) == value)
            {
                return CampaignOptionsDifficultyPresets.Freebooter;
            }

            if (option.GetValueFromDifficultyPreset(CampaignOptionsDifficultyPresets.Warrior) == value)
            {
                return CampaignOptionsDifficultyPresets.Warrior;
            }

            if (option.GetValueFromDifficultyPreset(CampaignOptionsDifficultyPresets.Bannerlord) == value)
            {
                return CampaignOptionsDifficultyPresets.Bannerlord;
            }

            return CampaignOptionsDifficultyPresets.Custom;
        }
    }
}

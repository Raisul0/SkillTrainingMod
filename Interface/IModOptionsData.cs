using SkillTrainingMod.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ViewModelCollection;

namespace SkillTrainingMod.Interface
{
    public interface IModOptionsData
    {
        ModOptionDataType GetDataType();

        int GetPriorityIndex();

        bool IsRelatedToDifficultyPreset();

        float GetValueFromDifficultyPreset(CampaignOptionsDifficultyPresets preset);

        string GetIdentifier();

        CampaignOptionEnableState GetEnableState();

        string GetName();

        string GetDescription();

        float GetValue();

        void SetValue(float value);

        CampaignOptionDisableStatus GetIsDisabledWithReason();
    }
}

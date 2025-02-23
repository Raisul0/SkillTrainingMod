using SkillTrainingMod.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;

namespace SkillTrainingMod.ViewModels
{
    public class SkillTrainingSkillVM : SkillVM
    {
        public SkillTrainingSkillVM(ref SkillTrainingState skillTrainingState, SkillObject skill, CharacterVM developerVM, Action<PerkVM> onStartPerkSelection) : base(skill, developerVM, onStartPerkSelection)
        {
            _skillTrainingState = skillTrainingState;
            if(_skillTrainingState.IsSkillBoasted(this))
            {
                SetToActive();
            }
        }

        public void ExecuteOnSelected()
        {
            if (IsActive)
            {
                SetToInActive();
                _skillTrainingState.RemoveSkill(this);
            }
            else
            {
                SetToActive();
                _skillTrainingState.AddSkill(this);
            }
            
        }

        private void SetToActive()
        {
            this.SpriteName = "CharacterDeveloper\\cp_icon";
            IsActive = true;
        }

        private void SetToInActive()
        {
            this.SpriteName = "";
            IsActive = false;
        }

        [DataSourceProperty]
        public string SpriteName
        {
            get
            {
                return this._spriteName;
            }
            set
            {
                if (value != this._spriteName)
                {
                    this._spriteName = value;
                    base.OnPropertyChangedWithValue<string>(value, "SpriteName");
                }
            }
        }

        [DataSourceProperty]
        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if (value != this._isActive)
                {
                    this._isActive = value;
                    base.OnPropertyChangedWithValue(value, "IsActive");
                }
            }
        }

        #region Properties
        private SkillTrainingState _skillTrainingState;
        private string _spriteName;
        private bool _isActive;
        private SkillVM x;

        #endregion
    }
}

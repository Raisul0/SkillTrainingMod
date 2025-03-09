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
            Developer=developerVM;
            SpriteName = "Inventory\\icon_gold";

            Color = new Color(0, 0, 0).ToString();

            if (_skillTrainingState.IsSkillBoasted(this))
            {
                SetToActive();
            }

            ;
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
            Color = new Color(50, 50, 50).ToString();   
            IsActive = true;
        }

        private void SetToInActive()
        {
            Color = new Color(0, 0, 0).ToString();
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
        public string Color
        {
            get
            {
                return this._color;
            }
            set
            {
                if (value != this._color)
                {
                    this._color = value;
                    base.OnPropertyChangedWithValue<string>(value, "Color");
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

        public CharacterVM Developer;

        #region Properties
        private SkillTrainingState _skillTrainingState;
        private string _spriteName;
        private bool _isActive;
        private SkillVM x;
        private string _color;

        #endregion
    }
}

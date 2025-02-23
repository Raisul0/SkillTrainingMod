using SkillTrainingMod.Constants;
using SkillTrainingMod.GameObjects;
using SkillTrainingMod.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SkillTrainingMod.Views
{
    public class SkillTrainingPanelState : GameState
    {
        public SkillTrainingState skillTrainingState;

        public SkillTrainingPanelState() { }
        public SkillTrainingPanelState(SkillTrainingState skillTrainingState)
        {
            this.skillTrainingState = skillTrainingState;
        }
    }

    [GameStateScreen(typeof(SkillTrainingPanelState))]
    public class SkillTrainingPanelScreen : ScreenBase, IGameStateListener
    {

        SkillTrainingPanelState _skillTrainingPanelState;
        public SkillTrainingPanelScreen(SkillTrainingPanelState skillTrainingPanelState)
        {
            _skillTrainingPanelState = skillTrainingPanelState;
            _skillTrainingPanelState.RegisterListener(this);
        }


        GauntletLayer _layer;
        SkillTrainingPanelVM _dataSource;
        private SpriteCategory spriteCategory;

        void IGameStateListener.OnActivate()
        {
            _layer = new GauntletLayer(100, "GauntletLayer", true);
            _dataSource = new SkillTrainingPanelVM(_skillTrainingPanelState.skillTrainingState);
            this.LoadSpriteCategories();
            _layer.LoadMovie(SkillTrainingMovies.SkillTrainingPanel, _dataSource);
            _layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
            _layer.IsFocusLayer = true;
            ScreenManager.TrySetFocus(_layer);
            AddLayer(_layer);
        }

        private void LoadSpriteCategories()
        {
            SpriteData spriteData = UIResourceManager.SpriteData;
            TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
            ResourceDepot uiResourceDepot = UIResourceManager.UIResourceDepot;
            this.spriteCategory = spriteData.SpriteCategories["ui_characterdeveloper"];
            this.spriteCategory.Load(resourceContext, uiResourceDepot);
        }

        protected override void OnFrameTick(float dt)
        {
            base.OnFrameTick(dt);
            if (_layer.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.Escape))
            {
                _dataSource.ExecuteCancel();
            }
            if (_layer.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.Enter))
            {
                _dataSource.ExecuteDone();
            }
        }
        void IGameStateListener.OnDeactivate()
        {
            _layer.InputRestrictions.ResetInputRestrictions();
            _layer.IsFocusLayer = false;
            RemoveLayer(_layer);
            ScreenManager.TryLoseFocus(_layer);
            _dataSource = null;
        }

        void IGameStateListener.OnFinalize() { }
        void IGameStateListener.OnInitialize() { }
    }
}

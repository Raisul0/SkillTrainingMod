using SkillTrainingMod.Behaviors;
using SkillTrainingMod.Constants;
using SkillTrainingMod.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.TwoDimension;

namespace SkillTrainingMod.Views
{
    [DefaultView]
    public class SkillTrainingPanelView : MissionView
    {
        GauntletLayer _gauntletLayer;
        IGauntletMovie _movie;
        SkillTrainingPanelVM _dataSource;
        SkillTrainingBehavior SkillTrainingBehavior;
        int ViewOrderPriority = 99;
        public override void OnMissionScreenInitialize()
        {
            base.OnMissionScreenInitialize();
        }

        public SkillTrainingPanelView()
        {

        }

        public SkillTrainingPanelView(SkillTrainingPanelVM dataSource)
        {
            _dataSource = dataSource;
        }

        public override void OnBehaviorInitialize()
        {
            base.OnBehaviorInitialize();
            SkillTrainingBehavior = Campaign.Current.GetCampaignBehavior<SkillTrainingBehavior>();

            _gauntletLayer = new GauntletLayer(ViewOrderPriority);
            if (_dataSource == null) _dataSource = new SkillTrainingPanelVM(SkillTrainingBehavior.GetSkillTrainingState());
            _gauntletLayer = new GauntletLayer(ViewOrderPriority, "GauntletLayer", false);
        }

        private void LoadSpriteCategories()
        {
            SpriteData spriteData = UIResourceManager.SpriteData;
            TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
            ResourceDepot uiResourceDepot = UIResourceManager.UIResourceDepot;
            this.spriteCategory = spriteData.SpriteCategories["ui_characterdeveloper"];
            this.spriteCategory.Load(resourceContext, uiResourceDepot);
        }

        public override void OnMissionScreenTick(float dt)
        {
            if (Input.IsKeyPressed(TaleWorlds.InputSystem.InputKey.L))
            {
                ToggleUI();

            }
        }

        public override void OnMissionScreenFinalize()
        {
            base.OnMissionScreenFinalize();
            base.MissionScreen.RemoveLayer(_gauntletLayer);
            _movie = null;
            _gauntletLayer = null;
            _dataSource = null;
        }

        public void ToggleUI()
        {
            if (!_dataSource.IsVisible)
            {
                Show(false);
            }
            else
            {
                Hide();
            }
        }

        public void Show(bool showLootbox)
        {
            this.LoadSpriteCategories();
            _dataSource.IsVisible = true;
            _movie = _gauntletLayer.LoadMovie(SkillTrainingMovies.SkillTrainingPanel, _dataSource);
            MissionScreen.AddLayer(_gauntletLayer);
            _gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
        }

        public void Hide()
        {
            _dataSource.IsVisible = false;
            _gauntletLayer.ReleaseMovie(_movie);
            _gauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.Invalid);
        }

        #region Properties

        private SpriteCategory spriteCategory;
        #endregion
    }
}

using SandBox.View.Map;
using SkillTrainingMod.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SkillTrainingMod.Views
{
    public class SkillTrainingModOptionsView : MapView
    {
        // Token: 0x06000160 RID: 352 RVA: 0x0000B03C File Offset: 0x0000923C
        protected override void CreateLayout()
        {
            base.CreateLayout();
            this._dataSource = new SkillTrainingModOptionsVM(new Action(this.OnClose));
            base.Layer = new GauntletLayer(4401, "GauntletLayer", false)
            {
                IsFocusLayer = true
            };
            this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
            this.LoadSpriteCategories();
            this._layerAsGauntletLayer.LoadMovie("SkillTrainingModOptions", this._dataSource);
            base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
            base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);

            base.MapScreen.AddLayer(base.Layer);
            base.MapScreen.PauseAmbientSounds();
            ScreenManager.TrySetFocus(base.Layer);
        }

        // Token: 0x06000161 RID: 353 RVA: 0x0000B0FE File Offset: 0x000092FE
        private void OnClose()
        {
            var modOptionsView = MapScreen.GetMapView<SkillTrainingModOptionsView>();

            if (modOptionsView != null)
            {
                MapScreen.RemoveMapView(modOptionsView);
            }
        }

        private void LoadSpriteCategories()
        {
            SpriteData spriteData = UIResourceManager.SpriteData;
            TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
            ResourceDepot uiResourceDepot = UIResourceManager.UIResourceDepot;
            this.spriteCategory = spriteData.SpriteCategories["ui_group1"];
            this.spriteCategory.Load(resourceContext, uiResourceDepot);
        }

        // Token: 0x06000162 RID: 354 RVA: 0x0000B10A File Offset: 0x0000930A
        protected override void OnIdleTick(float dt)
        {
            base.OnFrameTick(dt);
            if (base.Layer.Input.IsHotKeyReleased("Exit"))
            {
                UISoundsHelper.PlayUISound("event:/ui/default");
                this._dataSource.ExecuteDone();
            }
        }

        // Token: 0x06000163 RID: 355 RVA: 0x0000B140 File Offset: 0x00009340
        protected override void OnFinalize()
        {
            base.OnFinalize();
            base.Layer.InputRestrictions.ResetInputRestrictions();
            base.MapScreen.RemoveLayer(base.Layer);
            base.MapScreen.RestartAmbientSounds();
            ScreenManager.TryLoseFocus(base.Layer);
            base.Layer = null;
            this._dataSource = null;
            this._layerAsGauntletLayer = null;
        }

        // Token: 0x040000AB RID: 171
        private SkillTrainingModOptionsVM _dataSource;

        // Token: 0x040000AC RID: 172
        private GauntletLayer _layerAsGauntletLayer;

    
        private SpriteCategory spriteCategory;

    }
}

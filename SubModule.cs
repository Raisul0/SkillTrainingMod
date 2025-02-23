using SkillTrainingMod.Behaviors;
using SkillTrainingMod.Views;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ScreenSystem;


namespace SkillTrainingMod
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

        }

        protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
        {
            base.InitializeGameStarter(game, starterObject);
            if (starterObject is CampaignGameStarter starter)
            {
                starter.AddBehavior(new SkillTrainingBehavior());
            }
            
        }

        public override void OnMissionBehaviorInitialize(Mission mission)
        {
            base.OnMissionBehaviorInitialize(mission);
            AddAdditionalMissionBehaviours(mission);
        }

        private void AddAdditionalMissionBehaviours(Mission mission)
        {
            mission.AddMissionBehavior(new SkillTrainingPanelView());
        }
    }
}
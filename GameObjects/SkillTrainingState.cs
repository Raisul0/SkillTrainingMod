using HarmonyLib;
using SkillTrainingMod.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;
using TaleWorlds.Core;
using TaleWorlds.SaveSystem;

namespace SkillTrainingMod.GameObjects
{
    public class SkillTrainingState
    {
        public SkillTrainingState() 
        {
            BoastedSkills = new List<SkillTrainingSkillVM>();
        }

        public void AddSkill(SkillTrainingSkillVM skill) 
        {
            BoastedSkills.Add(skill);


        }
        public void RemoveSkill(SkillTrainingSkillVM skill)
        {
            if(BoastedSkills.Any(x=>x.SkillId == skill.SkillId && x.Developer.HeroNameText == skill.Developer.HeroNameText))
            {
                var removeSkill = BoastedSkills.FirstOrDefault(x => x.SkillId == skill.SkillId && x.Developer.HeroNameText == skill.Developer.HeroNameText);
                BoastedSkills.Remove(removeSkill);
            }
        }

        public bool IsSkillBoasted(SkillTrainingSkillVM skill) => BoastedSkills.Any(x => x.SkillId == skill.SkillId && x.Developer.HeroNameText == skill.Developer.HeroNameText);


        [SaveableField(1)]
        public List<SkillTrainingSkillVM> BoastedSkills;
    }

    public class SkillTrainingStateSaveableTypeDefiner : SaveableTypeDefiner
    {
        public SkillTrainingStateSaveableTypeDefiner() : base(536_882_256) { }

        protected override void DefineClassTypes()
        {
            AddClassDefinition(typeof(SkillTrainingState), 1);
        }
        protected override void DefineContainerDefinitions()
        {
            ConstructContainerDefinition(typeof(Dictionary<string, SkillTrainingState>));
        }
    }
}

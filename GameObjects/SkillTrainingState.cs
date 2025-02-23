using HarmonyLib;
using SkillTrainingMod.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.SaveSystem;

namespace SkillTrainingMod.GameObjects
{
    public class SkillTrainingState
    {
        public SkillTrainingState() 
        {
            BoastedSkills = new List<SkillObject>();
        }

        public void AddSkill(SkillTrainingSkillVM skill) => BoastedSkills.Add(skill.Skill);
        public void RemoveSkill(SkillTrainingSkillVM skill) => BoastedSkills.Remove(skill.Skill);
        public bool IsSkillBoasted(SkillTrainingSkillVM skill) => BoastedSkills.Any(x => x == skill.Skill);

        [SaveableField(1)]
        public List<SkillObject> BoastedSkills;
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

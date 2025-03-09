using HarmonyLib;
using SkillTrainingMod.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;
using TaleWorlds.Core;
using TaleWorlds.SaveSystem;

namespace SkillTrainingMod.GameObjects
{
    public class SkillTrainingState
    {
        public SkillTrainingState() 
        {
            SkillsInTraining = new List<SkillTrainingSkillVM>();
        }

        public void AddSkill(SkillTrainingSkillVM skill) 
        {
            SkillsInTraining.Add(skill);
            //var heroTrainingExist = HerosInTrainging.FirstOrDefault(x => x.HeroId == skill.Developer.Hero.Id.ToString());
            //if (!string.IsNullOrEmpty(heroTrainingExist.HeroId))
            //{
            //    var skills = heroTrainingExist.Skills;
            //    if (skills != null)
            //    {
            //        heroTrainingExist.Skills.Add(skill.Skill);
            //    }
            //    else
            //    {
            //        heroTrainingExist.Skills = new List<SkillObject>
            //        {
            //            skill.Skill
            //        };
            //    }
            //}
            //else
            //{
            //    var heroInTraining = new HeroInTrainging();
            //    heroInTraining.HeroId = skill.Developer.Hero.Id.ToString();
            //    heroInTraining.Skills = new List<SkillObject>
            //    {
            //        skill.Skill
            //    };
            //    HerosInTrainging.Add(heroInTraining);
            //}
        }
        public void RemoveSkill(SkillTrainingSkillVM skill)
        {
            var skillInTrainingExists = SkillsInTraining.FirstOrDefault(x=>x.Developer.Hero.StringId == skill.Developer.Hero.StringId && x.Skill.StringId == skill.Skill.StringId);
            if(skillInTrainingExists != null)
            {
                SkillsInTraining.Remove(skillInTrainingExists);
            }
            //var heroTrainingExist = HerosInTrainging.FirstOrDefault(x => x.HeroId == skill.Developer.Hero.Id.ToString());
            //if(!string.IsNullOrEmpty(heroTrainingExist.HeroId))
            //{
            //    heroTrainingExist.Skills.Remove(skill.Skill);
            //}
        }

        public bool IsSkillBoasted(SkillTrainingSkillVM skill) 
        {
            var skillInTrainingExists = SkillsInTraining.Any(x => x.Developer.Hero.StringId == skill.Developer.Hero.StringId && x.Skill.StringId == skill.Skill.StringId);
            if(skillInTrainingExists)
            {
                return true;
            }
            //var heroTrainingExist = HerosInTrainging.FirstOrDefault(x => x.HeroId == skill.Developer.Hero.Id.ToString());
            //if(!string.IsNullOrEmpty(heroTrainingExist.HeroId))
            //{
            //    var skills = heroTrainingExist.Skills;
            //    if(skills.Any(x=> x == skill.Skill))
            //    {
            //        return true;
            //    }
            //}

            return false;
        }

        //[SaveableField(1)]
        public List<SkillTrainingSkillVM> SkillsInTraining;
    }

    //public class SkillTrainingStateSaveableTypeDefiner : SaveableTypeDefiner
    //{
    //    public SkillTrainingStateSaveableTypeDefiner() : base(888_888_888) { }

    //    protected override void DefineClassTypes()
    //    {
    //        AddClassDefinition(typeof(SkillTrainingState), 1);
    //        AddClassDefinition(typeof(HeroInTrainging), 2);
    //    }
    //    protected override void DefineContainerDefinitions()
    //    {
    //        ConstructContainerDefinition(typeof(SkillTrainingState));
    //    }
    //}
}

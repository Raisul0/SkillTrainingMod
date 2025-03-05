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
            HerosInTrainging = new List<HeroInTrainging>();
        }

        public void AddSkill(SkillTrainingSkillVM skill) 
        {
            var heroTrainingExist = HerosInTrainging.FirstOrDefault(x => x.HeroId == skill.Developer.Hero.Id.ToString());
            if (!string.IsNullOrEmpty(heroTrainingExist.HeroId))
            {
                var skills = heroTrainingExist.Skills;
                if (skills != null)
                {
                    heroTrainingExist.Skills.Add(skill.Skill);
                }
                else
                {
                    heroTrainingExist.Skills = new List<SkillObject>
                    {
                        skill.Skill
                    };
                }
            }
            else
            {
                var heroInTraining = new HeroInTrainging();
                heroInTraining.HeroId = skill.Developer.Hero.Id.ToString();
                heroInTraining.Skills = new List<SkillObject>
                {
                    skill.Skill
                };
                HerosInTrainging.Add(heroInTraining);
            }
        }
        public void RemoveSkill(SkillTrainingSkillVM skill)
        {
            var heroTrainingExist = HerosInTrainging.FirstOrDefault(x => x.HeroId == skill.Developer.Hero.Id.ToString());
            if(!string.IsNullOrEmpty(heroTrainingExist.HeroId))
            {
                heroTrainingExist.Skills.Remove(skill.Skill);
            }
        }

        public bool IsSkillBoasted(SkillTrainingSkillVM skill) 
        {
            var heroTrainingExist = HerosInTrainging.FirstOrDefault(x => x.HeroId == skill.Developer.Hero.Id.ToString());
            if(!string.IsNullOrEmpty(heroTrainingExist.HeroId))
            {
                var skills = heroTrainingExist.Skills;
                if(skills.Any(x=> x == skill.Skill))
                {
                    return true;
                }
            }

            return false;
        }

        //[SaveableField(1)]
        public List<HeroInTrainging> HerosInTrainging;
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

    public struct HeroInTrainging
    {
        //[SaveableField(1)]
        public string HeroId;
        //[SaveableField(2)]
        public List<SkillObject> Skills;
    }
}

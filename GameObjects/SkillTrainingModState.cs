using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTrainingMod.GameObjects
{
    public static class SkillTrainingModState
    {
        public static int MaxSkillLevel = 300;
        public static int GoldCostForSkill1to100 = 300;
        public static int GoldCostForSkill101to200 = 900;
        public static int GoldCostForSkill201to300 = 1800;
        public static int SkillPointfor1to100 = 6;
        public static int SkillPointfor101to200 = 3;
        public static int SkillPointfor201to300 = 1;
        public static int MinHeroGold = 1000;
    }
}

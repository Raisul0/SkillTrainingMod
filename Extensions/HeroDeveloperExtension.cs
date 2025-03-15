using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.MountAndBlade;

namespace SkillTrainingMod.Extensions
{
    public static class HeroDeveloperExtension
    {
        public static void ModifyHeroXp(HeroDeveloper instance, float xpChangeAmount)
        {
            FieldInfo fieldInfo = typeof(HeroDeveloper).GetField("_totalXp", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo != null)
            {
                var previousXp = instance.TotalXp;
                fieldInfo.SetValue(instance, previousXp+ (int)xpChangeAmount);
            }
        }
    }
}

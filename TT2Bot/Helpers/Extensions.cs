using System;
using System.Collections.Generic;
using System.Linq;
using TT2Bot.Models;

namespace TT2Bot.Helpers
{
    static class Extensions
    {
        public static string Join<T>(this IEnumerable<T> values, string separator, string finalSeparator = null)
        {
            finalSeparator = finalSeparator ?? separator;
            var firstElements = values.Take(Math.Max(values.Count() - 1, 0)).ToArray();
            var finalElement = values.Last();
            if (firstElements.Length == 0)
                return finalElement.ToString();
            return string.Join(separator, firstElements) + finalSeparator + finalElement.ToString();
        }

        public static string FormatValue(this BonusType bonusType, double value)
        {
            value = Math.Round(value, 5);
            switch (bonusType)
            {
                case BonusType.CritBoostSkillDuration:
                case BonusType.HandOfMidasSkillDuration:
                case BonusType.HelperBoostSkillDuration:
                case BonusType.ShadowCloneSkillDuration:
                case BonusType.TapBoostSkillDuration:
                    return $"+{(int)value}s";
                case BonusType.BurstDamageSkillMana:
                case BonusType.CritBoostSkillMana:
                case BonusType.HandOfMidasSkillMana:
                case BonusType.HelperBoostSkillMana:
                case BonusType.ShadowCloneSkillMana:
                case BonusType.TapBoostSkillMana:
                    return $"-{(int)value} mana";
                case BonusType.HelperUpgradeCost:
                    return $"-{(int)(value * 100)}%";
                case BonusType.DoubleFairyChance:
                case BonusType.CritChance:
                case BonusType.Goldx10Chance:
                case BonusType.ChestChance:
                    return string.Format("{0:0.##}%", value * 100);
                case BonusType.HSArtifactDamage:
                    return $"x{1 + value}";
                case BonusType.MeleeHelperDamage:
                case BonusType.SpellHelperDamage:
                case BonusType.RangedHelperDamage:
                case BonusType.AllHelperDamage:
                case BonusType.CritDamage:
                case BonusType.PetDamageMult:
                case BonusType.MonsterHP:
                case BonusType.TapDamage:
                case BonusType.AllDamage:
                case BonusType.GoldAll:
                case BonusType.ChestAmount:
                case BonusType.GoldBoss:
                case BonusType.GoldMonster:
                case BonusType.HelperBoostSkillAmount:
                case BonusType.ShadowCloneSkillAmount:
                case BonusType.HandOfMidasSkillAmount:
                case BonusType.TapBoostSkillAmount:
                case BonusType.BurstDamageSkillAmount:
                case BonusType.PrestigeRelic:
                case BonusType.HelmetBoost:
                case BonusType.SwordBoost:
                case BonusType.SlashBoost:
                case BonusType.ArmorBoost:
                case BonusType.AuraBoost:
                    return $"x{value + 1}";
                case BonusType.SplashDamage:
                case BonusType.ManaRegen:
                case BonusType.ManaPoolCap:
                    return $"+{value}";
                case BonusType.None:
                    return $"-";
                default:
                    return $"{(int)(value * 100)}%";

            }
        }
    }
}

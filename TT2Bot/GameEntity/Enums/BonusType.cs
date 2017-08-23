using System;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using static TT2Bot.TT2Localisation.Enums.BonusTypeText;

namespace TT2Bot.GameEntity.Enums
{
    public enum BonusType
    {
        None,
        AllDamage,
        AllHelperDamage,
        MeleeHelperDamage,
        RangedHelperDamage,
        SpellHelperDamage,
        CritDamage,
        ArmorBoost,
        ArtifactDamage,
        HSArtifactDamage,
        AuraBoost,
        BossTimer,
        BurstDamageSkillAmount,
        BurstDamageSkillMana,
        ChestAmount,
        ChestChance,
        ClanQTEDamage,
        ClanDamage,
        CritBoostSkillDuration,
        CritBoostSkillAmount,
        CritBoostSkillMana,
        CritChance,
        DoubleFairyChance,
        GoblinQTE,
        GoldAll,
        GoldBoss,
        GoldMonster,
        Goldx10Chance,
        HandOfMidasSkillDuration,
        HandOfMidasSkillAmount,
        HandOfMidasSkillMana,
        HelmetBoost,
        HelperBoostSkillDuration,
        HelperBoostSkillAmount,
        HelperBoostSkillMana,
        HelperUpgradeCost,
        HelperQTECount,
        InactiveAdvance,
        InactiveGold,
        ManaPoolCap,
        ManaRegen,
        ManaMonsterMana,
        ManaTapRegen,
        ThisHelperDamage,
        Memory,
        MonsterCountPerStage,
        MonsterHP,
        PetDamage,
        PetDamageMult,
        PetBossDamage,
        PetBossCount,
        PetQTEGold,
        PetOfflineDamage,
        PrestigeRelic,
        ShadowCloneSkillDuration,
        ShadowCloneSkillAmount,
        ShadowCloneSkillMana,
        SplashDamage,
        SlashBoost,
        SwordBoost,
        TapBoostSkillDuration,
        TapBoostSkillAmount,
        TapBoostSkillMana,
        TapDamage,
        TapDamageFromHelpers
    }

    public static class BonusTypeMethods
    {
        public static ILocalisable<string> ToLocalisable(this BonusType bonusType)
            => Localisable(bonusType);

        public static ILocalisable<string> ToLocalisable(this BonusType bonusType, double value)
        {
            value = Math.Round(value, 5);
            switch (bonusType)
            {
                case BonusType.CritBoostSkillDuration:
                case BonusType.HandOfMidasSkillDuration:
                case BonusType.HelperBoostSkillDuration:
                case BonusType.ShadowCloneSkillDuration:
                case BonusType.TapBoostSkillDuration:
                    return new LocalisedString(FORMATSECONDS, Math.Round(value));
                case BonusType.BurstDamageSkillMana:
                case BonusType.CritBoostSkillMana:
                case BonusType.HandOfMidasSkillMana:
                case BonusType.HelperBoostSkillMana:
                case BonusType.ShadowCloneSkillMana:
                case BonusType.TapBoostSkillMana:
                    return new LocalisedString(FORMATMANA, Math.Round(value));
                case BonusType.HelperUpgradeCost:
                    return new LocalisedString(FORMATDISCOUNT, value * 100);
                case BonusType.DoubleFairyChance:
                case BonusType.CritChance:
                case BonusType.Goldx10Chance:
                case BonusType.ChestChance:
                    return new LocalisedString(FORMATCHANCE, value * 100);
                case BonusType.HSArtifactDamage:
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
                    return new LocalisedString(FORMATMULTIPLY, value + 1);
                case BonusType.SplashDamage:
                case BonusType.ManaRegen:
                case BonusType.ManaPoolCap:
                    return new LocalisedString(FORMATADD, value);
                case BonusType.None:
                    return new LocalisedString(FORMATNONE, value);
                default:
                    return new LocalisedString(FORMATDEFAULT, value * 100);
            }
        }
    }
}

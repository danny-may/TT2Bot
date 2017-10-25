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
        BossHP,
        BossRelic,
        BossTimer,
        BurstDamageSkillAmount,
        ChestAmount,
        ChestChance,
        CritBoostSkillDuration,
        CritBoostSkillAmount,
        CritBoostSkillMana,
        CritChance,
        BurstDamageSkillMana,
        SuperCritDamage,
        FairySpawnChance,
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
        ManaMonsterAmount,
        ManaTapRegen,
        ThisHelperDamage,
        MonsterCountPerStage,
        MonsterHP,
        PetDamage,
        PetIndividualDamage,
        PetBossDamage,
        PetBossCount,
        PetQTEGold,
        InactivePetDamage,
        PrestigeRelic,
        ShadowCloneSkillDuration,
        ShadowCloneSkillAmount,
        ShadowCloneSkillMana,
        SplashDamage,
        SlashBoost,
        SwordBoost,
        SwordAttackDamage,
        TapBoostSkillDuration,
        TapBoostSkillAmount,
        TapBoostSkillMana,
        TapDamage,
        TapDamageFromHelpers,
        ClanShipDamage,
        MultiMonsters,
        SwordMasterDamage,
        ClanDamage,
        //2.2
        FairyGold,
        SplashGold,
        InactiveAllDamage,
        AllActiveSkillAmount,
        AllActiveSkillDuration,
        SwordMasterUpgradeCost
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
                case BonusType.AllActiveSkillDuration:
                    return new LocalisedString(FORMATSECONDS, Math.Round(value));
                case BonusType.BurstDamageSkillMana:
                case BonusType.CritBoostSkillMana:
                case BonusType.HandOfMidasSkillMana:
                case BonusType.HelperBoostSkillMana:
                case BonusType.ShadowCloneSkillMana:
                case BonusType.TapBoostSkillMana:
                    return new LocalisedString(FORMATMANA, Math.Round(value));
                case BonusType.HelperUpgradeCost:
                case BonusType.SwordMasterUpgradeCost:
                    return new LocalisedString(FORMATDISCOUNT, value * 100);
                case BonusType.FairySpawnChance:
                case BonusType.CritChance:
                case BonusType.Goldx10Chance:
                case BonusType.ChestChance:
                case BonusType.SplashDamage:
                    return new LocalisedString(FORMATCHANCE, value * 100);
                case BonusType.HSArtifactDamage:
                case BonusType.MeleeHelperDamage:
                case BonusType.SpellHelperDamage:
                case BonusType.RangedHelperDamage:
                case BonusType.AllHelperDamage:
                case BonusType.CritDamage:
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
                case BonusType.CritBoostSkillAmount:
                case BonusType.PrestigeRelic:
                case BonusType.HelmetBoost:
                case BonusType.SwordBoost:
                case BonusType.SlashBoost:
                case BonusType.ArmorBoost:
                case BonusType.AuraBoost:
                case BonusType.ClanShipDamage:
                case BonusType.AllActiveSkillAmount:
                case BonusType.InactiveAllDamage:
                case BonusType.InactiveGold:
                case BonusType.FairyGold:
                case BonusType.SplashGold:
                    return new LocalisedString(FORMATMULTIPLY, value + 1);
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
using System;
using TitanBot.Formatting;
using static TT2Bot.TT2Localisation.Enums.BonusTypeText;

namespace TT2Bot.Models
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
        public static LocalisedString ToLocalisable(this BonusType bonusType)
        {
            switch (bonusType)
            {
                case BonusType.AllDamage: return (LocalisedString)ALLDAMAGE;
                case BonusType.AllHelperDamage: return (LocalisedString)ALLHELPERDAMAGE;
                case BonusType.ArmorBoost: return (LocalisedString)ARMORBOOST;
                case BonusType.ArtifactDamage: return (LocalisedString)ARTIFACTDAMAGE;
                case BonusType.BurstDamageSkillAmount: return (LocalisedString)BURSTDAMAGESKILLAMOUNT;
                case BonusType.BurstDamageSkillMana: return (LocalisedString)BURSTDAMAGESKILLMANA;
                case BonusType.ChestAmount: return (LocalisedString)CHESTAMOUNT;
                case BonusType.ChestChance: return (LocalisedString)CHESTCHANCE;
                case BonusType.CritBoostSkillDuration: return (LocalisedString)CRITBOOSTSKILLDURATION;
                case BonusType.CritBoostSkillMana: return (LocalisedString)CRITBOOSTSKILLMANA;
                case BonusType.CritChance: return (LocalisedString)CRITCHANCE;
                case BonusType.DoubleFairyChance: return (LocalisedString)DOUBLEFAIRYCHANCE;
                case BonusType.GoldAll: return (LocalisedString)GOLDALL;
                case BonusType.GoldBoss: return (LocalisedString)GOLDBOSS;
                case BonusType.GoldMonster: return (LocalisedString)GOLDMONSTER;
                case BonusType.Goldx10Chance: return (LocalisedString)GOLDX10CHANCE;
                case BonusType.HandOfMidasSkillAmount: return (LocalisedString)HANDOFMIDASSKILLAMOUNT;
                case BonusType.HandOfMidasSkillDuration: return (LocalisedString)HANDOFMIDASSKILLDURATION;
                case BonusType.HandOfMidasSkillMana: return (LocalisedString)HANDOFMIDASSKILLMANA;
                case BonusType.HelmetBoost: return (LocalisedString)HELMETBOOST;
                case BonusType.HelperBoostSkillAmount: return (LocalisedString)HELPERBOOSTSKILLAMOUNT;
                case BonusType.HelperBoostSkillDuration: return (LocalisedString)HELPERBOOSTSKILLDURATION;
                case BonusType.HelperBoostSkillMana: return (LocalisedString)HELPERBOOSTSKILLMANA;
                case BonusType.HelperUpgradeCost: return (LocalisedString)HELPERUPGRADECOST;
                case BonusType.HSArtifactDamage: return (LocalisedString)HSARTIFACTDAMAGE;
                case BonusType.MeleeHelperDamage: return (LocalisedString)MELEEHELPERDAMAGE;
                case BonusType.None: return (LocalisedString)NONE;
                case BonusType.PetDamageMult: return (LocalisedString)PETDAMAGEMULT;
                case BonusType.PrestigeRelic: return (LocalisedString)PRESTIGERELIC;
                case BonusType.RangedHelperDamage: return (LocalisedString)RANGEDHELPERDAMAGE;
                case BonusType.ShadowCloneSkillAmount: return (LocalisedString)SHADOWCLONESKILLAMOUNT;
                case BonusType.ShadowCloneSkillDuration: return (LocalisedString)SHADOWCLONESKILLDURATION;
                case BonusType.ShadowCloneSkillMana: return (LocalisedString)SHADOWCLONESKILLMANA;
                case BonusType.SlashBoost: return (LocalisedString)SLASHBOOST;
                case BonusType.SpellHelperDamage: return (LocalisedString)SPELLHELPERDAMAGE;
                case BonusType.SwordBoost: return (LocalisedString)SWORDBOOST;
                case BonusType.TapBoostSkillAmount: return (LocalisedString)TAPBOOSTSKILLAMOUNT;
                case BonusType.TapBoostSkillDuration: return (LocalisedString)TAPBOOSTSKILLDURATION;
                case BonusType.TapBoostSkillMana: return (LocalisedString)TAPBOOSTSKILLMANA;
                case BonusType.TapDamage: return (LocalisedString)TAPDAMAGE;
                case BonusType.SplashDamage: return (LocalisedString)SPLASHDAMAGE;
                case BonusType.ManaRegen: return (LocalisedString)MANAREGEN;
                case BonusType.CritDamage: return (LocalisedString)CRITDAMAGE;
                case BonusType.ManaPoolCap: return (LocalisedString)MANAPOOLCAP;
                case BonusType.TapDamageFromHelpers: return (LocalisedString)TAPDAMAGEFROMHELPERS;
                case BonusType.MonsterHP: return (LocalisedString)MONSTERHP;
            }
            return (RawString)bonusType.ToString();
        }

        public static LocalisedString LocaliseValue(this BonusType bonusType, double value)
        {
            value = Math.Round(value, 5);
            switch (bonusType)
            {
                case BonusType.CritBoostSkillDuration:
                case BonusType.HandOfMidasSkillDuration:
                case BonusType.HelperBoostSkillDuration:
                case BonusType.ShadowCloneSkillDuration:
                case BonusType.TapBoostSkillDuration:
                    return new LocalisedString(FORMATSECONDS, (int)value);
                case BonusType.BurstDamageSkillMana:
                case BonusType.CritBoostSkillMana:
                case BonusType.HandOfMidasSkillMana:
                case BonusType.HelperBoostSkillMana:
                case BonusType.ShadowCloneSkillMana:
                case BonusType.TapBoostSkillMana:
                    return new LocalisedString(FORMATMANA, (int)value);
                case BonusType.HelperUpgradeCost:
                    return new LocalisedString(FORMATDISCOUNT, (int)(value * 100));
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
                    return new LocalisedString(FORMATDEFAULT, (int)(value * 100));
            }
        }
    }
}

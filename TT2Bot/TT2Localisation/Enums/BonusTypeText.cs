using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TitanBot.Formatting;
using TT2Bot.GameEntity.Enums;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Enums
        {
            public static class BonusTypeText
            {
                public const string BASE_PATH = Enums.BASE_PATH + "BONUSTYPE_";

                public static LocalisedString Localisable(BonusType bonus)
                    => new LocalisedString(BASE_PATH + bonus.ToString().ToUpper());

                public const string FORMATSECONDS = BASE_PATH + nameof(FORMATSECONDS);
                public const string FORMATMANA = BASE_PATH + nameof(FORMATMANA);
                public const string FORMATDISCOUNT = BASE_PATH + nameof(FORMATDISCOUNT);
                public const string FORMATCHANCE = BASE_PATH + nameof(FORMATCHANCE);
                public const string FORMATMULTIPLY = BASE_PATH + nameof(FORMATMULTIPLY);
                public const string FORMATADD = BASE_PATH + nameof(FORMATADD);
                public const string FORMATMINUS = BASE_PATH + nameof(FORMATMINUS);
                public const string FORMATNONE = BASE_PATH + nameof(FORMATNONE);
                public const string FORMATDEFAULT = BASE_PATH + nameof(FORMATDEFAULT);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<BonusType, string>
                    {
                        { BonusType.AllActiveSkillAmount, "All Active Skill Effect" },
                        { BonusType.AllActiveSkillCooldownRate, "All Active Skill Cooldown" },
                        { BonusType.AllActiveSkillDuration, "All Active Skill Duration" },
                        { BonusType.AllUpgradeCost, "All Upgrade Cost" },
                        { BonusType.AllEquipmentEffect, "All Equipment Boost" },
                        { BonusType.AllDamage, "All Damage" },
                        { BonusType.AllHelperDamage, "All Hero Damage" },
                        { BonusType.MeleeHelperDamage, "Melee Hero Damage" },
                        { BonusType.RangedHelperDamage, "Ranged Hero Damage" },
                        { BonusType.SpellHelperDamage, "Spell Hero Damage" },
                        { BonusType.CritDamage, "Critical Damage" },
                        { BonusType.ArmorBoost, "Armor Boost" },
                        { BonusType.AllPetGoldEffect, "Pet Gold Bonuses" },
                        { BonusType.AllPetDamageEffect, "Pet Damage Bonuses" },
                        { BonusType.ArtifactDamage, "All Artifact Damage" },
                        { BonusType.HSArtifactDamage, "All Artifact Damage" },
                        { BonusType.AuraBoost, "Aura Boost" },
                        { BonusType.BossHP, "Boss HP" },
                        { BonusType.BossRelic, "Boss Relic Amount" },
                        { BonusType.BossTimer, "Boss Timer Rate" },
                        { BonusType.BossTimerDuration, "Boss Timer Duration" },
                        { BonusType.BossDamage, "Boss Damage" },
                        { BonusType.BurstDamageSkillAmount, "Heavenly Strike Damage" },
                        { BonusType.ChestAmount, "Chesterson Gold" },
                        { BonusType.ChestChance, "Chesterson Chance" },
                        { BonusType.CompanionDamage, "Companion Damage" },
                        { BonusType.CritBoostSkillDuration, "Deadly Strike Duration" },
                        { BonusType.CritBoostSkillAmount, "Deadly Strike Damage" },
                        { BonusType.CritBoostSkillMana, "Deadly Strike Mana Cost" },
                        { BonusType.CritChance, "Critical Chance" },
                        { BonusType.BurstDamageSkillMana, "Heavenly Strike Mana Cost" },
                        { BonusType.SuperCritDamage, "Critical Strike Mastery" },
                        { BonusType.FairyGold, "Fairy Gold" },
                        { BonusType.FairySpawnChance, "Chance for Multiple Fairies" },
                        { BonusType.FlyingHelperDamage, "Flying Hero Damage" },
                        { BonusType.GoblinQTE, "Goblin Focus Tap" },
                        { BonusType.GoldAll, "All Gold" },
                        { BonusType.GoldBoss, "Boss Gold" },
                        { BonusType.GoldMonster, "Basic Titan Gold" },
                        { BonusType.Goldx10Chance, "10x Gold Chance" },
                        { BonusType.GroundHelperDamage, "Ground Hero Damage" },
                        { BonusType.HandOfMidasSkillDuration, "Hand Of Midas Duration" },
                        { BonusType.HandOfMidasSkillAmount, "Hand Of Midas Gold" },
                        { BonusType.HandOfMidasSkillMana, "Hand Of Midas Mana Cost" },
                        { BonusType.HelmetBoost, "Helmet Boost" },
                        { BonusType.HelperBoostSkillDuration, "War Cry Duration" },
                        { BonusType.HelperBoostSkillAmount, "War Cry Damage" },
                        { BonusType.HelperBoostSkillMana, "War Cry Mana Cost" },
                        { BonusType.HelperUpgradeCost, "Hero Upgrade Cost" },
                        { BonusType.HelperQTECount, "Ultra Orb Bounces" },
                        { BonusType.InactiveAdvance, "Max Inactive Stage" },
                        { BonusType.InactiveAllDamage, "Inactive Damage" },
                        { BonusType.InactiveGold, "Inactive Gold" },
                        { BonusType.ManaPoolCap, "Mana Capacity" },
                        { BonusType.ManaRegen, "Mana Regeneration" },
                        { BonusType.ManaMonsterAmount, "Manni Mana" },
                        { BonusType.ManaTapRegen, "Mana Per Tap" },
                        { BonusType.ThisHelperDamage, "This Heroes' Damage" },
                        { BonusType.MonsterCountPerStage, "Titan Count Per Stage" },
                        { BonusType.MonsterHP, "All Titan HP" },
                        { BonusType.PetDamage, "Pet Damage" },
                        { BonusType.PetIndividualDamage, "Pet Damage" },
                        { BonusType.PetBossDamage, "Pet Damage Per Zip" },
                        { BonusType.PetBossCount, "Boss Focus Tap Count" },
                        { BonusType.PetQTEGold, "Gold Per Heart of Midas" },
                        { BonusType.InactivePetDamage, "Inactive Pet Damage" },
                        { BonusType.PrestigeRelic, "Prestige Relic" },
                        { BonusType.ShadowCloneSkillDuration, "Shadow Clone Duration" },
                        { BonusType.ShadowCloneSkillAmount, "Shadow Clone Damage" },
                        { BonusType.ShadowCloneSkillMana, "Shadow Clone Mana Cost" },
                        { BonusType.SplashDamage, "Splash Damage" },
                        { BonusType.SplashGold, "Splash Gold" },
                        { BonusType.SlashBoost, "Slash Equipment Boost" },
                        { BonusType.SwordBoost, "Sword Equipment Boost" },
                        { BonusType.SwordAttackDamage, "Sword Attack Damage" },
                        { BonusType.SwordMasterUpgradeCost, "Sword Master Upgrade Cost" },
                        { BonusType.TapBoostSkillDuration, "Fire Sword Duration" },
                        { BonusType.TapBoostSkillAmount, "Fire Sword Damage" },
                        { BonusType.TapBoostSkillMana, "Fire Sword Mana Cost" },
                        { BonusType.TapDamage, "Tap Damage" },
                        { BonusType.TapDamageFromHelpers, "Tap Damage from Heroes" },
                        { BonusType.TitanDamage, "Non-Boss Damage" },
                        { BonusType.ClanShipDamage, "Clan Ship Damage" },
                        { BonusType.MultiMonsters, "Multiple Spawn Chance" },
                        { BonusType.SwordMasterDamage, "Sword Master Damage" },
                    }.ToDictionary(k => Localisable(k.Key).Key, v => v.Value)
                    .Concat(new Dictionary<string, string> {
                        { FORMATSECONDS, "+{0}s" },
                        { FORMATMANA, "-{0} mana" },
                        { FORMATDISCOUNT, "-{0}%" },
                        { FORMATCHANCE, "{0:0.##}%" },
                        { FORMATMULTIPLY, "x{0}" },
                        { FORMATADD, "+{0}" },
                        { FORMATMINUS, "-{0}" },
                        { FORMATNONE, "-" },
                        { FORMATDEFAULT, "{0}%" },
                    }).ToImmutableDictionary();
            }
        }
    }
}
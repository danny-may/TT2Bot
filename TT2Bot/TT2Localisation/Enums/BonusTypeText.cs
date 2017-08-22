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
                        { BonusType.AllDamage,"All Damage" },
                        { BonusType.AllHelperDamage,"All Hero Damage" },
                        { BonusType.ArmorBoost,"Armor Equipment Bonus" },
                        { BonusType.ArtifactDamage,"Artifact Damage" },
                        { BonusType.BurstDamageSkillAmount,"Heavenly Strike Effect" },
                        { BonusType.BurstDamageSkillMana,"Heavenly Strike Cost" },
                        { BonusType.ChestAmount,"Chesterson Amount" },
                        { BonusType.ChestChance,"Chesterson Chance" },
                        { BonusType.CritBoostSkillDuration,"Critical Strike Duration" },
                        { BonusType.CritBoostSkillMana,"Critical Strike Cost" },
                        { BonusType.CritChance,"Critical Chance" },
                        { BonusType.DoubleFairyChance,"Double Fairy Chance" },
                        { BonusType.GoldAll,"All Gold" },
                        { BonusType.GoldBoss,"Boss Gold" },
                        { BonusType.GoldMonster,"Titan Gold" },
                        { BonusType.Goldx10Chance,"x10 Gold Chance" },
                        { BonusType.HandOfMidasSkillAmount,"Hand of Midas Effect" },
                        { BonusType.HandOfMidasSkillDuration,"Hand of Midas Duration" },
                        { BonusType.HandOfMidasSkillMana,"Hand of Midas Cost" },
                        { BonusType.HelmetBoost,"Helmet Equipment Bonus" },
                        { BonusType.HelperBoostSkillAmount,"War Cry Effect" },
                        { BonusType.HelperBoostSkillDuration,"War Cry Duration" },
                        { BonusType.HelperBoostSkillMana,"War Cry Cost" },
                        { BonusType.HelperUpgradeCost,"Hero Cost" },
                        { BonusType.HSArtifactDamage,"All Artifact Damage" },
                        { BonusType.MeleeHelperDamage,"Melee Hero Damage" },
                        { BonusType.None,"None" },
                        { BonusType.PetDamageMult,"Pet Damage" },
                        { BonusType.PrestigeRelic,"Prestige Relics" },
                        { BonusType.RangedHelperDamage,"Ranged Hero Damage" },
                        { BonusType.ShadowCloneSkillAmount,"Shadow Clone Effect" },
                        { BonusType.ShadowCloneSkillDuration,"Shadow Clone Duration" },
                        { BonusType.ShadowCloneSkillMana,"Shadow Clone Cost" },
                        { BonusType.SlashBoost,"Slash Equipment Bonus" },
                        { BonusType.SpellHelperDamage,"Spell Hero Damage" },
                        { BonusType.SwordBoost,"Weapon Equipment Bonus" },
                        { BonusType.TapBoostSkillAmount,"Fire Sword Effect" },
                        { BonusType.TapBoostSkillDuration,"Fire Sword Duration" },
                        { BonusType.TapBoostSkillMana,"Fire Sword Cost" },
                        { BonusType.TapDamage,"Tap Damage" },
                        { BonusType.SplashDamage,"Splash Damage" },
                        { BonusType.ManaRegen,"Mana Regen" },
                        { BonusType.CritDamage,"Crit Damage" },
                        { BonusType.ManaPoolCap,"Mana Pool Cap" },
                        { BonusType.TapDamageFromHelpers,"% Tap Damage From Heroes" },
                        { BonusType.MonsterHP,"Titan HP" }
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

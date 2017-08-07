using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Enums
        {
            public static class BonusTypeText
            {
                private const string BASE_PATH = Enums.BASE_PATH + "BONUSTYPE_";

                public const string ALLDAMAGE = BASE_PATH + nameof(ALLDAMAGE);
                public const string ALLHELPERDAMAGE = BASE_PATH + nameof(ALLHELPERDAMAGE);
                public const string ARMORBOOST = BASE_PATH + nameof(ARMORBOOST);
                public const string ARTIFACTDAMAGE = BASE_PATH + nameof(ARTIFACTDAMAGE);
                public const string BURSTDAMAGESKILLAMOUNT = BASE_PATH + nameof(BURSTDAMAGESKILLAMOUNT);
                public const string BURSTDAMAGESKILLMANA = BASE_PATH + nameof(BURSTDAMAGESKILLMANA);
                public const string CHESTAMOUNT = BASE_PATH + nameof(CHESTAMOUNT);
                public const string CHESTCHANCE = BASE_PATH + nameof(CHESTCHANCE);
                public const string CRITBOOSTSKILLDURATION = BASE_PATH + nameof(CRITBOOSTSKILLDURATION);
                public const string CRITBOOSTSKILLMANA = BASE_PATH + nameof(CRITBOOSTSKILLMANA);
                public const string CRITCHANCE = BASE_PATH + nameof(CRITCHANCE);
                public const string DOUBLEFAIRYCHANCE = BASE_PATH + nameof(DOUBLEFAIRYCHANCE);
                public const string GOLDALL = BASE_PATH + nameof(GOLDALL);
                public const string GOLDBOSS = BASE_PATH + nameof(GOLDBOSS);
                public const string GOLDMONSTER = BASE_PATH + nameof(GOLDMONSTER);
                public const string GOLDX10CHANCE = BASE_PATH + nameof(GOLDX10CHANCE);
                public const string HANDOFMIDASSKILLAMOUNT = BASE_PATH + nameof(HANDOFMIDASSKILLAMOUNT);
                public const string HANDOFMIDASSKILLDURATION = BASE_PATH + nameof(HANDOFMIDASSKILLDURATION);
                public const string HANDOFMIDASSKILLMANA = BASE_PATH + nameof(HANDOFMIDASSKILLMANA);
                public const string HELMETBOOST = BASE_PATH + nameof(HELMETBOOST);
                public const string HELPERBOOSTSKILLAMOUNT = BASE_PATH + nameof(HELPERBOOSTSKILLAMOUNT);
                public const string HELPERBOOSTSKILLDURATION = BASE_PATH + nameof(HELPERBOOSTSKILLDURATION);
                public const string HELPERBOOSTSKILLMANA = BASE_PATH + nameof(HELPERBOOSTSKILLMANA);
                public const string HELPERUPGRADECOST = BASE_PATH + nameof(HELPERUPGRADECOST);
                public const string HSARTIFACTDAMAGE = BASE_PATH + nameof(HSARTIFACTDAMAGE);
                public const string MELEEHELPERDAMAGE = BASE_PATH + nameof(MELEEHELPERDAMAGE);
                public const string NONE = BASE_PATH + nameof(NONE);
                public const string PETDAMAGEMULT = BASE_PATH + nameof(PETDAMAGEMULT);
                public const string PRESTIGERELIC = BASE_PATH + nameof(PRESTIGERELIC);
                public const string RANGEDHELPERDAMAGE = BASE_PATH + nameof(RANGEDHELPERDAMAGE);
                public const string SHADOWCLONESKILLAMOUNT = BASE_PATH + nameof(SHADOWCLONESKILLAMOUNT);
                public const string SHADOWCLONESKILLDURATION = BASE_PATH + nameof(SHADOWCLONESKILLDURATION);
                public const string SHADOWCLONESKILLMANA = BASE_PATH + nameof(SHADOWCLONESKILLMANA);
                public const string SLASHBOOST = BASE_PATH + nameof(SLASHBOOST);
                public const string SPELLHELPERDAMAGE = BASE_PATH + nameof(SPELLHELPERDAMAGE);
                public const string SWORDBOOST = BASE_PATH + nameof(SWORDBOOST);
                public const string TAPBOOSTSKILLAMOUNT = BASE_PATH + nameof(TAPBOOSTSKILLAMOUNT);
                public const string TAPBOOSTSKILLDURATION = BASE_PATH + nameof(TAPBOOSTSKILLDURATION);
                public const string TAPBOOSTSKILLMANA = BASE_PATH + nameof(TAPBOOSTSKILLMANA);
                public const string TAPDAMAGE = BASE_PATH + nameof(TAPDAMAGE);
                public const string SPLASHDAMAGE = BASE_PATH + nameof(SPLASHDAMAGE);
                public const string MANAREGEN = BASE_PATH + nameof(MANAREGEN);
                public const string CRITDAMAGE = BASE_PATH + nameof(CRITDAMAGE);
                public const string MANAPOOLCAP = BASE_PATH + nameof(MANAPOOLCAP);
                public const string TAPDAMAGEFROMHELPERS = BASE_PATH + nameof(TAPDAMAGEFROMHELPERS);
                public const string MONSTERHP = BASE_PATH + nameof(MONSTERHP);
                public const string FORMATSECONDS = BASE_PATH + nameof(FORMATSECONDS);
                public const string FORMATMANA = BASE_PATH + nameof(FORMATMANA);
                public const string FORMATDISCOUNT = BASE_PATH + nameof(FORMATDISCOUNT);
                public const string FORMATCHANCE = BASE_PATH + nameof(FORMATCHANCE);
                public const string FORMATMULTIPLY = BASE_PATH + nameof(FORMATMULTIPLY);
                public const string FORMATADD = BASE_PATH + nameof(FORMATADD);
                public const string FORMATNONE = BASE_PATH + nameof(FORMATNONE);
                public const string FORMATDEFAULT = BASE_PATH + nameof(FORMATDEFAULT);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { ALLDAMAGE, "All Damage" },
                        { ALLHELPERDAMAGE, "All Hero Damage" },
                        { ARMORBOOST, "Armor Equipment Bonus" },
                        { ARTIFACTDAMAGE, "Artifact Damage" },
                        { BURSTDAMAGESKILLAMOUNT, "Heavenly Strike Effect" },
                        { BURSTDAMAGESKILLMANA, "Heavenly Strike Cost" },
                        { CHESTAMOUNT, "Chesterson Amount" },
                        { CHESTCHANCE, "Chesterson Chance" },
                        { CRITBOOSTSKILLDURATION, "Critical Strike Duration" },
                        { CRITBOOSTSKILLMANA, "Critical Strike Cost" },
                        { CRITCHANCE, "Critical Chance" },
                        { DOUBLEFAIRYCHANCE, "Double Fairy Chance" },
                        { GOLDALL, "All Gold" },
                        { GOLDBOSS, "Boss Gold" },
                        { GOLDMONSTER, "Titan Gold" },
                        { GOLDX10CHANCE, "x10 Gold Chance" },
                        { HANDOFMIDASSKILLAMOUNT, "Hand of Midas Effect" },
                        { HANDOFMIDASSKILLDURATION, "Hand of Midas Duration" },
                        { HANDOFMIDASSKILLMANA, "Hand of Midas Cost" },
                        { HELMETBOOST, "Helmet Equipment Bonus" },
                        { HELPERBOOSTSKILLAMOUNT, "War Cry Effect" },
                        { HELPERBOOSTSKILLDURATION, "War Cry Duration" },
                        { HELPERBOOSTSKILLMANA, "War Cry Cost" },
                        { HELPERUPGRADECOST, "Hero Cost" },
                        { HSARTIFACTDAMAGE, "All Artifact Damage" },
                        { MELEEHELPERDAMAGE, "Melee Hero Damage" },
                        { NONE, "None" },
                        { PETDAMAGEMULT, "Pet Damage" },
                        { PRESTIGERELIC, "Prestige Relics" },
                        { RANGEDHELPERDAMAGE, "Ranged Hero Damage" },
                        { SHADOWCLONESKILLAMOUNT, "Shadow Clone Effect" },
                        { SHADOWCLONESKILLDURATION, "Shadow Clone Duration" },
                        { SHADOWCLONESKILLMANA, "Shadow Clone Cost" },
                        { SLASHBOOST, "Slash Equipment Bonus" },
                        { SPELLHELPERDAMAGE, "Spell Hero Damage" },
                        { SWORDBOOST, "Weapon Equipment Bonus" },
                        { TAPBOOSTSKILLAMOUNT, "Fire Sword Effect" },
                        { TAPBOOSTSKILLDURATION, "Fire Sword Duration" },
                        { TAPBOOSTSKILLMANA, "Fire Sword Cost" },
                        { TAPDAMAGE, "Tap Damage" },
                        { SPLASHDAMAGE, "Splash Damage" },
                        { MANAREGEN, "Mana Regen" },
                        { CRITDAMAGE, "Crit Damage" },
                        { MANAPOOLCAP, "Mana Pool Cap" },
                        { TAPDAMAGEFROMHELPERS, "% Tap Damage From Heroes" },
                        { MONSTERHP, "Titan HP" },
                        { FORMATSECONDS, "+{0}s" },
                        { FORMATMANA, "-{0} mana" },
                        { FORMATDISCOUNT, "-{0}%" },
                        { FORMATCHANCE, "{0:0.##}%" },
                        { FORMATMULTIPLY, "x{0}" },
                        { FORMATADD, "+{0}" },
                        { FORMATNONE, "-" },
                        { FORMATDEFAULT, "{0}%" },
                    }.ToImmutableDictionary();
            }
        }
    }
}

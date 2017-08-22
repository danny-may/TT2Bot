using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Formatting;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Enums;
using TT2Bot.GameEntity.Enums.EntityId;
using TT2Bot.GameEntity.Localisation;
using TT2Bot.Models;

namespace TT2Bot.GameEntity.Entities
{
    class Skill : GameEntity<SkillId>
    {
        public static IReadOnlyDictionary<SkillCategory, ImmutableArray<SkillId>> SkillCategories { get; }
            = new Dictionary<SkillCategory, SkillId[]>
            {
                { SkillCategory.Left, new []{ SkillId.PetQTE, SkillId.BossDmgQTE, SkillId.PetGoldQTE, SkillId.HelperDmgQTE, SkillId.HelperCountQTE, SkillId.Fairy, SkillId.BossTimer, SkillId.ClanQTE } },
                { SkillCategory.Center, new []{ SkillId.OfflineGold, SkillId.MeleeHelperDmg, SkillId.SpellHelperDmg, SkillId.RangedHelperDmg, SkillId.PetDmg, SkillId.LessMonsters, SkillId.SplashDmg, SkillId.AutoAdvance, SkillId.MultiMonsters, SkillId.PetOfflineDmg } },
                { SkillCategory.Right, new []{ SkillId.BurstSkillBoost, SkillId.FireTapSkillBoost, SkillId.MPRegenBoost, SkillId.MPCapacityBoost, SkillId.HelperDmgSkillBoost, SkillId.MidasSkillBoost, SkillId.ManaStealSkillBoost, SkillId.CritSkillBoost, SkillId.CloneSkillBoost, SkillId.ManaMonster } }
            }.ToImmutableDictionary(k => k.Key, v => v.Value.ToImmutableArray());

        public static IReadOnlyDictionary<BonusType, ImmutableArray<string>> SudoBonusTypes { get; }
            = new Dictionary<BonusType, string[]>
            {

            }.ToImmutableDictionary(k => k.Key, v => v.Value.ToImmutableArray());

        public override LocalisedString Name => Localisation.GetName(Id);
        public override LocalisedString Abbreviations => Localisation.GetAbbreviation(Id);
        public string Note { get; }
        public SkillId RequirementKey { get; }
        public Skill Requirement => AllSkills.FirstOrDefault(s => s.Id == RequirementKey);
        private List<Skill> AllSkills { get; }
        public int StageRequirement { get; }
        public (int Cost, double Bonus)[] Levels { get; }
        public int TotalCost => Levels.Sum(l => l.Cost);
        public int UnlockCost => Requirement?.UnlockCost + 1 ?? 0;
        public double MaxBonus => Levels.Last().Bonus;
        public SkillCategory Category { get; }
        public BonusType SudoBonusType { get; }

        public Skill(SkillId id,
                     string note,
                     SkillId requirement,
                     List<Skill> allSkills,
                     int stageRequirement,
                     List<(int Cost, double Bonus)> levels,
                     string fileVersion,
                     Func<string, ValueTask<Bitmap>> imageGetter = null)
        {
            Id = id;
            RequirementKey = requirement;
            StageRequirement = stageRequirement;
            AllSkills = allSkills;
            Levels = levels?.ToArray() ?? new(int, double)[0];
            FileVersion = fileVersion;
            ImageGetter = imageGetter;

            MaxLevel = Levels.Length;
            Category = SkillCategories.FirstOrDefault(c => c.Value.Contains(Id)).Key;
        }

        public LocalisedString FormatBonus(double bonus)
        {
            return null;
        }

        public static class Localisation
        {
            public const string BASE_PATH = EntityLocalisation.BASE_PATH + "SKILL_";

            public const string TOSTRING = BASE_PATH + nameof(TOSTRING);
            public const string UNABLE_DOWNLOAD = BASE_PATH + nameof(UNABLE_DOWNLOAD);
            public const string MULTIPLE_MATCHES = BASE_PATH + nameof(MULTIPLE_MATCHES);
            public static LocalisedString GetName(SkillId skillId)
                => new LocalisedString(BASE_PATH + skillId.ToString().ToUpper());
            public static LocalisedString GetAbbreviation(SkillId skillId)
                => new LocalisedString(BASE_PATH + skillId.ToString().ToUpper() + "_ABBREV");

            private static IReadOnlyDictionary<SkillId, (string Name, string Abbreviations)> SkillNames { get; }
                = new Dictionary<SkillId, (string Name, string Abbreviations)> {
                        { SkillId.PetQTE, ("Pet: Lightning Burst", "LB" )},
                        { SkillId.BossDmgQTE, ("Pet: Flash Zip", "FZ" )},
                        { SkillId.PetGoldQTE, ("Pet: Heart of Midas", "HOM" )},
                        { SkillId.HelperDmgQTE, ("Ultra Heroes", "UH" )},
                        { SkillId.HelperCountQTE, ("Ultra Power", "UP" )},
                        { SkillId.Fairy, ("Fairyland", "FL,F" )},
                        { SkillId.BossTimer, ("Time Dilation", "TD" )},
                        { SkillId.ClanQTE, ("Summon Help", "SH" )},
                        { SkillId.OfflineGold, ("Gold Splitter", "GS" )},
                        { SkillId.MeleeHelperDmg, ("Stronger Arms", "SA" )},
                        { SkillId.SpellHelperDmg, ("Hyper Magic", "HYM,HM" )},
                        { SkillId.RangedHelperDmg, ("Pinpoint Accuracy", "PA" )},
                        { SkillId.PetDmg, ("Pet Evolution", "PE" )},
                        { SkillId.LessMonsters, ("Intimidating Presence", "IP" )},
                        { SkillId.SplashDmg, ("Extended Reach", "ER" )},
                        { SkillId.AutoAdvance, ("Silent March", "SM" )},
                        { SkillId.MultiMonsters, ("Ambush", "A,AM" )},
                        { SkillId.PetOfflineDmg, ("Pet: Stealth", "S,ST" )},
                        { SkillId.BurstSkillBoost, ("Magic Fusion", "MF" )},
                        { SkillId.FireTapSkillBoost, ("Flame Touch", "FT" )},
                        { SkillId.MPRegenBoost, ("Magic Well", "MW" )},
                        { SkillId.MPCapacityBoost, ("Mana Limit Break", "MLB,LB,MB" )},
                        { SkillId.HelperDmgSkillBoost, ("Heroic Might", "HEM" )},
                        { SkillId.MidasSkillBoost, ("Midas Ultimate", "MU" )},
                        { SkillId.ManaStealSkillBoost, ("Mana Siphon", "MS" )},
                        { SkillId.CritSkillBoost, ("Lightning Strike", "LS" )},
                        { SkillId.CloneSkillBoost, ("Shadow Clone", "SC" )},
                        { SkillId.ManaMonster, ("Manni Mana", "MM" )},
                    };

            public static IReadOnlyDictionary<string, string> Defaults { get; }
                = new Dictionary<string, string>
                {
                        { TOSTRING, "{0} ({1})" },
                        { UNABLE_DOWNLOAD, "I could not download any skill data. Please try again later." },
                        { MULTIPLE_MATCHES, "There were more than 1 skills that matched `{2}`. Try being more specific, or use `{0}{1}` for a list of all skills" }
                }.Concat(SkillNames.SelectMany(k => new Dictionary<string, string>
                {
                    { GetName(k.Key).Key, k.Value.Name },
                    { GetAbbreviation(k.Key).Key, k.Value.Abbreviations }
                })).ToImmutableDictionary();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Formatting;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Localisation;
using TT2Bot.Models;

namespace TT2Bot.GameEntity.Entities
{
    class Skill : GameEntity<string>
    {
        public static IReadOnlyDictionary<int, ArtifactTier> Tiers { get; }

        public override LocalisedString Name => Localisation.GetName(Id);
        public string Note { get; }
        public string RequirementKey { get; }
        public Skill Requirement => AllSkills.FirstOrDefault(s => s.Id == RequirementKey);
        private List<Skill> AllSkills { get; }
        public int StageRequirement { get; }
        public (int Cost, double Bonus)[] Levels { get; }
        public int TotalCost => Levels.Sum(l => l.Cost);
        public double MaxBonus => Levels.Last().Bonus;

        public Skill(string id,
                     string note,
                     string requirement,
                     List<Skill> allSkills,
                     int stageRequirement,
                     List<(int Cost, double Bonus)> levels,
                     string fileVersion,
                     Func<string, ValueTask<Bitmap>> imageGetter = null)
        {
            Id = id;
            RequirementKey = requirement;
            StageRequirement = StageRequirement;
            AllSkills = allSkills;
            Levels = levels?.ToArray() ?? new(int, double)[0];
            FileVersion = fileVersion;
            ImageGetter = imageGetter;

            MaxLevel = Levels.Length;
        }

        public static class Localisation
        {
            public const string BASE_PATH = EntityLocalisation.BASE_PATH + "SKILL_";

            public const string TOSTRING = BASE_PATH + nameof(TOSTRING);
            public const string UNABLE_DOWNLOAD = BASE_PATH + nameof(UNABLE_DOWNLOAD);
            public const string MULTIPLE_MATCHES = BASE_PATH + nameof(MULTIPLE_MATCHES);
            public static LocalisedString GetName(string skillId)
                => new LocalisedString(BASE_PATH + skillId.ToUpper());

            public static IReadOnlyDictionary<string, string> Defaults { get; }
                = new Dictionary<string, string>
                {
                        { TOSTRING, "{0} ({1})" },
                        { UNABLE_DOWNLOAD, "I could not download any skill data. Please try again later." },
                        { MULTIPLE_MATCHES, "There were more than 1 skills that matched `{0}`" },
                        { GetName("PetQTE").Key, "Pet: Lightning Burst" },
                        { GetName("BossDmgQTE").Key, "Pet: Flash Zip" },
                        { GetName("PetGoldQTE").Key, "Pet: Heart of Midas" },
                        { GetName("HelperDmgQTE").Key, "Ultra Heroes" },
                        { GetName("HelperCountQTE").Key, "Ultra Power" },
                        { GetName("Fairy").Key, "Fairyland" },
                        { GetName("BossTimer").Key, "Time Dilation" },
                        { GetName("ClanQTE").Key, "Summon Help" },
                        { GetName("GoblinQTE").Key, "Goblin Focus Tap" }, //Removed
                        { GetName("BossCountQTE").Key, null }, //Removed
                        { GetName("OfflineGold").Key, "Gold Splitter" },
                        { GetName("MeleeHelperDmg").Key, "Stronger Arms" },
                        { GetName("SpellHelperDmg").Key, "Hyper Magic" },
                        { GetName("RangedHelperDmg").Key, "Pinpoint Accuracy" },
                        { GetName("PetDmg").Key, "Pet Evolution" },
                        { GetName("LessMonsters").Key, "Intimidating Presence" },
                        { GetName("SplashDmg").Key, "Extended Reach" },
                        { GetName("AutoAdvance").Key, "Silent March" },
                        { GetName("MultiMonsters").Key, "Ambush" },
                        { GetName("PetOfflineDmg").Key, "Pet: Stealth" },
                        { GetName("GoldRateBoost").Key, "Raining Gold" }, //Removed
                        { GetName("BurstSkillBoost").Key, "Magic Fusion" },
                        { GetName("FireTapSkillBoost").Key, "Flame Touch" },
                        { GetName("MPRegenBoost").Key, "Magic Well" },
                        { GetName("MPCapacityBoost").Key, "Mana Limit Break" },
                        { GetName("HelperDmgSkillBoost").Key, "Heroic Might" },
                        { GetName("MidasSkillBoost").Key, "Midas Ultimate" },
                        { GetName("ManaStealSkillBoost").Key, "Mana Siphon" },
                        { GetName("CritSkillBoost").Key, "Lightning Strike" },
                        { GetName("CloneSkillBoost").Key, "Shadow Clone" },
                        { GetName("ManaMonster").Key, "Manni Mana" },
                }.ToImmutableDictionary();
        }
    }
}

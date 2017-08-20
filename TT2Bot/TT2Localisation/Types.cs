using System.Collections.Generic;
using System.Collections.Immutable;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Models;
using TT2Bot.Models.General;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static class Types
        {
            public static string ARTIFACT = typeof(Artifact).Name;
            public static string PET = typeof(Pet).Name;
            public static string EQUIPMENT = typeof(Equipment).Name;
            public static string HELPER = typeof(Helper).Name;
            public static string SKILL = typeof(Skill).Name;
            public static string PERCENTAGE = typeof(Percentage).Name;
            public static string BONUSTYPE = typeof(BonusType).Name;

            public static IReadOnlyDictionary<string, string> Defaults { get; }
                = new Dictionary<string, string>
                {
                    { ARTIFACT, "an artifact" },
                    { PET, "a pet" },
                    { EQUIPMENT, "an equipment" },
                    { HELPER, "a hero" },
                    { SKILL, "a skill" },
                    { PERCENTAGE, "a percentage" },
                    { BONUSTYPE, "A bonus type" }
                }.ToImmutableDictionary();
        }
    }
}

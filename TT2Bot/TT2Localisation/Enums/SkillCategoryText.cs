using System.Collections.Generic;
using System.Collections.Immutable;
using TitanBot.Formatting;
using TT2Bot.GameEntity.Enums;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Enums
        {
            public sealed class SkillCategoryText
            {
                public const string BASE_PATH = Enums.BASE_PATH + "SKILLCATEGORY_";

                public static LocalisedString Localisable(SkillCategory bonus)
                    => new LocalisedString(BASE_PATH + bonus.ToString().ToUpper());

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<SkillCategory, string>
                    {
                        { SkillCategory.None, "None" },
                        { SkillCategory.Left, "Active (Left)" },
                        { SkillCategory.Center, "Passive (Middle)" },
                        { SkillCategory.Right, "Ability (Right)" }
                    }.ToImmutableDictionary(k => Localisable(k.Key).Key, v => v.Value);
            }
        }
    }
}

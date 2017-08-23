using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Enums
        {
            public static class HelperTypeText
            {
                public const string BASE_PATH = Enums.BASE_PATH + "HELPERTYPE_";

                public const string SPELL = BASE_PATH + nameof(SPELL);
                public const string MELEE = BASE_PATH + nameof(MELEE);
                public const string RANGE = BASE_PATH + nameof(RANGE);
                public const string NONE = BASE_PATH + nameof(NONE);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { SPELL, "Spell" },
                        { MELEE, "Melee" },
                        { RANGE, "Range" },
                        { NONE, "None" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

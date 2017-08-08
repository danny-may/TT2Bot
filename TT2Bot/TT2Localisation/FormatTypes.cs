using System.Collections.Generic;
using System.Collections.Immutable;
using TitanBot.Formatting;
using TT2Bot.Overrides;
using static TitanBot.TBLocalisation.FormatType;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        class FormatTypes
        {
            public static IReadOnlyDictionary<string, string> GetDefaults { get; }
                = new Dictionary<string, string>
                {
                    { GetName(Formatter.Scientific).Key, "Scientific" },
                    { GetDescription(Formatter.Scientific).Key, "Gives you the scientific output in the same style as ingame" },
                    { GetName(FormatType.DEFAULT).Key, "Alphabet" },
                    { GetDescription(FormatType.DEFAULT).Key, "Gives you the alphabet output (aa, ab, ac...)" }
                }.ToImmutableDictionary();
        }
    }
}

using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class CommandText
        {
            public static class SuggestText
            {
                public const string BASE_PATH = CommandText.BASE_PATH + "SUGGEST_";

                public const string MISSING_CHANNEL = BASE_PATH + nameof(MISSING_CHANNEL);
                public const string SENT = BASE_PATH + nameof(SENT);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { MISSING_CHANNEL, "I could not find where I need to send the suggestion! Please try again later." },
                        { SENT, "Suggestion sent" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

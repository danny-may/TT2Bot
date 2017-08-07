using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Commands
        {
            public static class ClanStatsText
            {
                private const string BASE_PATH = Commands.BASE_PATH + "CLANSTATS_";

                public const string TITLE = BASE_PATH + nameof(TITLE);
                public const string FIELD_CQ = BASE_PATH + nameof(FIELD_CQ);
                public const string FIELD_BONUS_CURRENT = BASE_PATH + nameof(FIELD_BONUS_CURRENT);
                public const string FIELD_BONUS_NEXT = BASE_PATH + nameof(FIELD_BONUS_NEXT);
                public const string FIELD_HP = BASE_PATH + nameof(FIELD_HP);
                public const string FIELD_ADVSTART = BASE_PATH + nameof(FIELD_ADVSTART);
                public const string FIELD_ATTACKERS = BASE_PATH + nameof(FIELD_ATTACKERS);
                public const string FIELD_ATTACKERS_ROW = BASE_PATH + nameof(FIELD_ATTACKERS_ROW);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { TITLE, "Displaying stats for a level {0} clan" },
                        { FIELD_CQ, "Current CQ" },
                        { FIELD_BONUS_CURRENT, "Current Bonus" },
                        { FIELD_BONUS_NEXT, "Next Bonus" },
                        { FIELD_HP, "Next Titan Lord HP" },
                        { FIELD_ADVSTART, "Advance start" },
                        { FIELD_ATTACKERS, "Requirements per boss (assuming MS {0} + {0} taps)" },
                        { FIELD_ATTACKERS_ROW, "Attackers: {0} | Damage/person: {1} | Attacks: {2} | Diamonds: {3}" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

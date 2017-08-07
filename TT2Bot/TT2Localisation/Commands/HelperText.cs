using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Commands
        {
            public static class HelperText
            {
                private const string BASE_PATH = Commands.BASE_PATH + "HELPER_";

                public const string LIST_TITLE = BASE_PATH + nameof(LIST_TITLE);
                public const string LIST_DESCRIPTION = BASE_PATH + nameof(LIST_DESCRIPTION);
                public const string LIST_FOOTER = BASE_PATH + nameof(LIST_FOOTER);
                public const string LIST_FIELD_ALL = BASE_PATH + nameof(LIST_FIELD_ALL);
                public const string LIST_FIELD_GROUPED = BASE_PATH + nameof(LIST_FIELD_GROUPED);
                public const string COST = BASE_PATH + nameof(COST);
                public const string DPS = BASE_PATH + nameof(DPS);
                public const string SHOW_TITLE = BASE_PATH + nameof(SHOW_TITLE);
                public const string SHOW_FOOTER = BASE_PATH + nameof(SHOW_FOOTER);
                public const string SHOW_FIELD_ID = BASE_PATH + nameof(SHOW_FIELD_ID);
                public const string SHOW_FIELD_TYPE = BASE_PATH + nameof(SHOW_FIELD_TYPE);
                public const string SHOW_FIELD_BASECOST = BASE_PATH + nameof(SHOW_FIELD_BASECOST);
                public const string SHOW_FIELD_BASEDAMAGE = BASE_PATH + nameof(SHOW_FIELD_BASEDAMAGE);
                public const string SHOW_FIELD_COSTAT = BASE_PATH + nameof(SHOW_FIELD_COSTAT);
                public const string SHOW_FIELD_DAMAGEAT = BASE_PATH + nameof(SHOW_FIELD_DAMAGEAT);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { LIST_TITLE, "Hero listing" },
                        { LIST_DESCRIPTION, "All Heros" },
                        { LIST_FOOTER, "{0} Hero tool" },
                        { LIST_FIELD_ALL, "Current Heroes" },
                        { LIST_FIELD_GROUPED, "Current {0} Heroes" },
                        { COST, "{0} gold" },
                        { DPS, "{0} dps" },
                        { SHOW_TITLE, "Hero data for {0}" },
                        { SHOW_FOOTER, "{0} Hero tool | TT2 v{1}" },
                        { SHOW_FIELD_ID, "Hero Id" },
                        { SHOW_FIELD_TYPE, "Hero type" },
                        { SHOW_FIELD_BASECOST, "Base cost" },
                        { SHOW_FIELD_BASEDAMAGE, "Base damage" },
                        { SHOW_FIELD_COSTAT, "Cost at lvl {0}" },
                        { SHOW_FIELD_DAMAGEAT, "Cost at lvl {0}" }

                    }.ToImmutableDictionary();
            }
        }
    }
}

using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class CommandText
        {
            public static class EquipmentText
            {
                public const string BASE_PATH = CommandText.BASE_PATH + "EQUIPMENT_";

                public const string LIST_TITLE = BASE_PATH + nameof(LIST_TITLE);
                public const string LIST_DESCRIPTION = BASE_PATH + nameof(LIST_DESCRIPTION);
                public const string LIST_DESCRIPTION_NONE = BASE_PATH + nameof(LIST_DESCRIPTION_NONE);
                public const string LIST_FOOTER = BASE_PATH + nameof(LIST_FOOTER);
                public const string SHOW_TITLE = BASE_PATH + nameof(SHOW_TITLE);
                public const string SHOW_FOOTER = BASE_PATH + nameof(SHOW_FOOTER);
                public const string SHOW_FIELD_ID = BASE_PATH + nameof(SHOW_FIELD_ID);
                public const string SHOW_FIELD_CLASS = BASE_PATH + nameof(SHOW_FIELD_CLASS);
                public const string SHOW_FIELD_RARITY = BASE_PATH + nameof(SHOW_FIELD_RARITY);
                public const string SHOW_FIELD_SOURCE = BASE_PATH + nameof(SHOW_FIELD_SOURCE);
                public const string SHOW_FIELD_BONUSTYPE = BASE_PATH + nameof(SHOW_FIELD_BONUSTYPE);
                public const string SHOW_FIELD_BONUSBASE = BASE_PATH + nameof(SHOW_FIELD_BONUSBASE);
                public const string SHOW_FIELD_BONUSINCREASE = BASE_PATH + nameof(SHOW_FIELD_BONUSINCREASE);
                public const string SHOW_FIELD_NOTE_NOLEVEL = BASE_PATH + nameof(SHOW_FIELD_NOTE_NOLEVEL);
                public const string SHOW_FIELD_NOTE = BASE_PATH + nameof(SHOW_FIELD_NOTE);
                public const string SHOW_FIELD_BONUSAT = BASE_PATH + nameof(SHOW_FIELD_BONUSAT);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { LIST_TITLE, "Equipment listing" },
                        { LIST_DESCRIPTION, "All {0} Equipment" },
                        { LIST_FOOTER, "{0} Equipment tool" },
                        { LIST_DESCRIPTION_NONE, "Please use one of the following equipment types:\n{0}\n\n `{1}{2} list [type]`" },
                        { SHOW_TITLE, "Equipment data for {0}" },
                        { SHOW_FOOTER, "{0} Equipment tool | TT2 v{1}" },
                        { SHOW_FIELD_ID, "Equipment id" },
                        { SHOW_FIELD_CLASS, "Equipment type" },
                        { SHOW_FIELD_RARITY, "Rarity" },
                        { SHOW_FIELD_SOURCE, "Source" },
                        { SHOW_FIELD_BONUSTYPE, "Bonus type" },
                        { SHOW_FIELD_BONUSBASE, "Bonus base" },
                        { SHOW_FIELD_BONUSINCREASE, "Bonus increase" },
                        { SHOW_FIELD_BONUSAT, "Bonus at lv {0} (actual ~{1})" },
                        { SHOW_FIELD_NOTE_NOLEVEL, "*The level displayed by equipment ingame is actually 10x lower than the real level.*" },
                        { SHOW_FIELD_NOTE, "*The level displayed by equipment ingame is actually 10x lower than the real level and rounded.*" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

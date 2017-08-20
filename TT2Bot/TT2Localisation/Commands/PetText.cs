using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class CommandText
        {
            public static class PetText
            {
                public const string BASE_PATH = CommandText.BASE_PATH + "PET_";

                public const string SHOW_TITLE = BASE_PATH + nameof(SHOW_TITLE);
                public const string SHOW_FOOTER = BASE_PATH + nameof(SHOW_FOOTER);
                public const string SHOW_FIELD_ID = BASE_PATH + nameof(SHOW_FIELD_ID);
                public const string SHOW_FIELD_BASEDAMAGE = BASE_PATH + nameof(SHOW_FIELD_BASEDAMAGE);
                public const string SHOW_FIELD_LVLXUP = BASE_PATH + nameof(SHOW_FIELD_LVLXUP);
                public const string SHOW_FIELD_LVLXTOY = BASE_PATH + nameof(SHOW_FIELD_LVLXTOY);
                public const string SHOW_FIELD_BONUSTYPE = BASE_PATH + nameof(SHOW_FIELD_BONUSTYPE);
                public const string SHOW_FIELD_BONUSBASE = BASE_PATH + nameof(SHOW_FIELD_BONUSBASE);
                public const string SHOW_FIELD_BONUSINCREASE = BASE_PATH + nameof(SHOW_FIELD_BONUSINCREASE);
                public const string SHOW_FIELD_INACTIVEPERCENT = BASE_PATH + nameof(SHOW_FIELD_INACTIVEPERCENT);
                public const string SHOW_FIELD_DAMAGEAT = BASE_PATH + nameof(SHOW_FIELD_DAMAGEAT);
                public const string SHOW_FIELD_BONUSAT = BASE_PATH + nameof(SHOW_FIELD_BONUSAT);
                public const string SHOW_FIELD_INACTIVEDAMAGEAT = BASE_PATH + nameof(SHOW_FIELD_INACTIVEDAMAGEAT);
                public const string SHOW_FIELD_INACTIVEBONUSAT = BASE_PATH + nameof(SHOW_FIELD_INACTIVEBONUSAT);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { SHOW_TITLE, "Pet data for {0}" },
                        { SHOW_FOOTER, "{0} Pet tool | TT2 v{1}" },
                        { SHOW_FIELD_ID, "Pet id" },
                        { SHOW_FIELD_BASEDAMAGE, "Base damage" },
                        { SHOW_FIELD_LVLXUP, "lvl {0}+" },
                        { SHOW_FIELD_LVLXTOY, "lvl {0}-{1}" },
                        { SHOW_FIELD_BONUSTYPE, "Bonus type" },
                        { SHOW_FIELD_BONUSBASE, "Bonus base" },
                        { SHOW_FIELD_BONUSINCREASE, "Bonus increase" },
                        { SHOW_FIELD_INACTIVEPERCENT, "Inactive % at lvl {0}" },
                        { SHOW_FIELD_DAMAGEAT, "Damage at lvl {0}" },
                        { SHOW_FIELD_BONUSAT, "Bonus at lvl {0}" },
                        { SHOW_FIELD_INACTIVEDAMAGEAT, "Inactive Damage at lvl {0}" },
                        { SHOW_FIELD_INACTIVEBONUSAT, "Inactive bonus at lvl {0}" },
                    }.ToImmutableDictionary();
            }
        }
    }
}

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
            public static class PrestigeText
            {
                private const string BASE_PATH = Commands.BASE_PATH + "PESTIGE_";

                public const string DESCRIPTION_BOS = BASE_PATH + nameof(DESCRIPTION_BOS);
                public const string DESCRIPTION_CLAN = BASE_PATH + nameof(DESCRIPTION_CLAN);
                public const string DESCRIPTION_IP = BASE_PATH + nameof(DESCRIPTION_IP);
                public const string TITLE = BASE_PATH + nameof(TITLE);
                public const string FOOTER = BASE_PATH + nameof(FOOTER);
                public const string FIELD_STARTINGSTAGE = BASE_PATH + nameof(FIELD_STARTINGSTAGE);
                public const string FIELD_RELICS = BASE_PATH + nameof(FIELD_RELICS);
                public const string FIELD_ENEMIES = BASE_PATH + nameof(FIELD_ENEMIES);
                public const string FIELD_TIME = BASE_PATH + nameof(FIELD_TIME);
                public const string FIELD_RELICS_VALUE = BASE_PATH + nameof(FIELD_RELICS_VALUE);
                public const string FIELD_ENEMIES_VALUE = BASE_PATH + nameof(FIELD_ENEMIES_VALUE);
                public const string FIELD_TIME_VALUE = BASE_PATH + nameof(FIELD_TIME_VALUE);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { DESCRIPTION_BOS, "BoS: {0}" },
                        { DESCRIPTION_CLAN, "Clan: {0}" },
                        { DESCRIPTION_IP, "Ip: {0}" },
                        { TITLE, "Info when prestiging on stage **{0}**" },
                        { FOOTER, "Prestige Data" },
                        { FIELD_STARTINGSTAGE, "Starting stage" },
                        { FIELD_RELICS, "Relics" },
                        { FIELD_ENEMIES, "Enemies" },
                        { FIELD_TIME, "Time" },
                        { FIELD_RELICS_VALUE, "{0} + {1} = {2}" },
                        { FIELD_ENEMIES_VALUE, "{0} + {1} bosses" },
                        { FIELD_TIME_VALUE, "~{0}.\n~{1} with 4x splash" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

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
        public static partial class Help
        {
            public static class Desc
            {
                private const string BASE_PATH = Help.BASE_PATH + "DESCRIPTION_";

                public const string REPORT = BASE_PATH + nameof(REPORT);
                public const string SUGGEST = BASE_PATH + nameof(SUGGEST);
                public const string APPLY = BASE_PATH + nameof(APPLY);
                public const string EXCUSE = BASE_PATH + nameof(EXCUSE);
                public const string SUBMIT = BASE_PATH + nameof(SUBMIT);
                public const string TITANLORD = BASE_PATH + nameof(TITANLORD);
                public const string ARTIFACT = BASE_PATH + nameof(ARTIFACT);
                public const string CLAIM = BASE_PATH + nameof(CLAIM);
                public const string CLANSTATS = BASE_PATH + nameof(CLANSTATS);
                public const string EQUIPMENT = BASE_PATH + nameof(EQUIPMENT);
                public const string HELPER = BASE_PATH + nameof(HELPER);
                public const string HIGHSCORE = BASE_PATH + nameof(HIGHSCORE);
                public const string PETS = BASE_PATH + nameof(PETS);
                public const string PRESTIGE = BASE_PATH + nameof(PRESTIGE);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { REPORT, "Allows you to report a bug you have found in me!"},
                        { SUGGEST, "Allows you to make suggestions and feature requests for me!" },
                        { APPLY, "Allows a user to register their interest in joining the clan" },
                        { EXCUSE, "Missed the boss? Or did someone else? Use this to get a water-tight excuse whenever you need!" },
                        { SUBMIT, "Allows you to submit a bug, suggestion or question to GameHive!" },
                        { TITANLORD, "Used for Titan Lord timers and management" },
                        { ARTIFACT, "Displays data about any artifact" },
                        { CLAIM, "Used to tie your discord account to your ingame account. Will be used in the future for API access" },
                        { CLANSTATS, "Shows various information for any clan with given attributes" },
                        { EQUIPMENT, "Displays data about any equipment" },
                        { HELPER, "Displays data about any hero" },
                        { HIGHSCORE, "Shows data from the high score sheet, which can be found [here](https://docs.google.com/spreadsheets/d/13hsvWaYvp_QGFuQ0ukcgG-FlSAj2NyW8DOvPUG3YguY/pubhtml?gid=4642011cYS8TLGYU)\nAll credit to <@261814131282149377>, <@169180650203512832> and <@169915601496702977> for running the sheet!" },
                        { PETS, "Displays data about any pet" },
                        { PRESTIGE, "Shows you exactly what prestiging at a given point will mean for you" },
                    }.ToImmutableDictionary();
            }
        }
    }
}

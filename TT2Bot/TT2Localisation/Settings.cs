using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static class Settings
        {
            public const string BASE_PATH = "SETTINGS_";

            public const string STRING_TOOLONG = BASE_PATH + nameof(STRING_TOOLONG);
            public const string CQ_TOOLOW = BASE_PATH + nameof(CQ_TOOLOW);

            public static class Desc
            {
                public const string BASE_PATH = Settings.BASE_PATH + "DESCRIPTION_";

                public const string GUILD_TITANLORD = BASE_PATH + nameof(GUILD_TITANLORD);
                public const string GLOBAL_TT2 = BASE_PATH + nameof(GLOBAL_TT2);
                public const string GLOBAL_DATAFILE = BASE_PATH + nameof(GLOBAL_DATAFILE);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { GUILD_TITANLORD, "These are the settings surrounding the `t$titanlord` command" },
                        { GLOBAL_TT2, "These are the global settings for titanbot" },
                        { GLOBAL_DATAFILE, "These are the versions used for the data commands" }
                    }.ToImmutableDictionary();
            }

            public static class Notes
            {
                public const string BASE_PATH = Settings.BASE_PATH + "NOTES_";

                public const string GUILD_TITANLORD = BASE_PATH + nameof(GUILD_TITANLORD);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { GUILD_TITANLORD, "There are several format strings you can use to have live data in your message.\n" +
                                           "Use `%USER%` to include the user who started the timer\n" +
                                           "Use `%TIME%` to include how long until the titan lord is up\n" +
                                           "Use `%ROUND%` for the round number\n" +
                                           "Use `%CQ%` for the current CQ number\n" +
                                           "Use `%COMPLETE%` for the time the titan lord will be up (UTC time)\n" +
                                           "Alternatively `%COMPLETE+timezone%` can be used to define the timezone, e.g. `%COMPLETE+6%`, minus can also be used but timezone has to be a number from 0 to 12" }
                    }.ToImmutableDictionary();
            }

            public static IReadOnlyDictionary<string, string> Defaults { get; }
                = new Dictionary<string, string>
                {
                    { STRING_TOOLONG, "You cannot have more than 500 characters for this setting" },
                    { CQ_TOOLOW, "Your clan quest cannot be negative" }
                }.Concat(Desc.Defaults)
                 .Concat(Notes.Defaults)
                 .ToImmutableDictionary();
        }
    }
}

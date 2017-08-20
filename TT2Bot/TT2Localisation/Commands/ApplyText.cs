using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class CommandText
        {
            public static class ApplyText
            {
                public const string BASE_PATH = CommandText.BASE_PATH + "APPLY_";

                public const string APPLICATION_SUCCESSFUL_GLOBAL = BASE_PATH + nameof(APPLICATION_SUCCESSFUL_GLOBAL);
                public const string APPLICATION_SUCCESSFUL_GUILD = BASE_PATH + nameof(APPLICATION_SUCCESSFUL_GUILD);
                public const string APPLICATION_UPDATED = BASE_PATH + nameof(APPLICATION_UPDATED);
                public const string VIEW_NOTREGISTERED = BASE_PATH + nameof(VIEW_NOTREGISTERED);
                public const string VIEW_GLOBAL_NOTREGISTERED = BASE_PATH + nameof(VIEW_GLOBAL_NOTREGISTERED);
                public const string VIEW_GUILD_NOTREGISTERED = BASE_PATH + nameof(VIEW_GUILD_NOTREGISTERED);
                public const string VIEW_TITLE = BASE_PATH + nameof(VIEW_TITLE);
                public const string VIEW_FOOTER_GLOBAL = BASE_PATH + nameof(VIEW_FOOTER_GLOBAL);
                public const string VIEW_FOOTER_GUILD = BASE_PATH + nameof(VIEW_FOOTER_GUILD);
                public const string REMOVE_GLOBAL_NOTALLOWED = BASE_PATH + nameof(REMOVE_GLOBAL_NOTALLOWED);
                public const string REMOVE_GLOBAL_SUCCESS = BASE_PATH + nameof(REMOVE_GLOBAL_SUCCESS);
                public const string REMOVE_GUILD_SUCCESS = BASE_PATH + nameof(REMOVE_GUILD_SUCCESS);
                public const string REMOVE_OTHER_SUCCESS = BASE_PATH + nameof(REMOVE_OTHER_SUCCESS);
                public const string IGNORE_IGNORED = BASE_PATH + nameof(IGNORE_IGNORED);
                public const string IGNORE_UNIGNORED = BASE_PATH + nameof(IGNORE_UNIGNORED);
                public const string LIST_TABLEHEADERS = BASE_PATH + nameof(LIST_TABLEHEADERS);
                public const string LIST_ROW = BASE_PATH + nameof(LIST_ROW);
                public const string LIST_NONE = BASE_PATH + nameof(LIST_NONE);
                public const string LIST_FORMAT = BASE_PATH + nameof(LIST_FORMAT);
                public const string CLEAR_SUCCESS = BASE_PATH + nameof(CLEAR_SUCCESS);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { APPLICATION_SUCCESSFUL_GLOBAL, "Your global application has been successful. Recruiters from any TT2 guild will be able to see your application and might choose to recruit you." },
                        { APPLICATION_SUCCESSFUL_GUILD, "Your application has been successful. The clan recruiter will review your application and potentially get back to you." },
                        { APPLICATION_UPDATED, "Your application has been successfully updated" },
                        { VIEW_NOTREGISTERED, "You have not registered yet!" },
                        { VIEW_GLOBAL_NOTREGISTERED, "That user does not have a global registration yet!" },
                        { VIEW_GUILD_NOTREGISTERED, "That user does not have a registration here yet!" },
                        { VIEW_TITLE, "Application" },
                        { VIEW_FOOTER_GLOBAL, "Global application | Applied {0} | Updated {1}" },
                        { VIEW_FOOTER_GUILD, "Local application | Applied {0} | Updated {1}" },
                        { REMOVE_GLOBAL_NOTALLOWED, "You cannot remove another users global application. Try usng `{0}apply ignore <user>`." },
                        { REMOVE_GLOBAL_SUCCESS, "You have successfully removed your global application." },
                        { REMOVE_GUILD_SUCCESS, "You have successfully removed your application for this guild." },
                        { REMOVE_OTHER_SUCCESS, "You have successfully removed the application by {0} for this guild." },
                        { IGNORE_IGNORED, "That user will no longer be shown from the global listings. They will be for local ones however." },
                        { IGNORE_UNIGNORED, "That user will now be shown in global listings." },
                        { LIST_TABLEHEADERS, "#,User,MS,Imgs,Relics,CQ/wk,Taps/CQ,Last edit" },
                        { LIST_ROW, "#{0},{1} ({2}),[{3}],{4},#{5},{6},{7},{8} day(s) ago,{9}" },
                        { LIST_NONE, "You have no registered users!" },
                        { LIST_FORMAT, "```css\n{0}\n```" },
                        { CLEAR_SUCCESS, "Your guilds registrations have been wiped" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

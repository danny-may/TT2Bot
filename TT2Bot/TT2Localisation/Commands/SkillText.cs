using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class CommandText
        {
            public static class SkillText
            {
                public const string BASE_PATH = CommandText.BASE_PATH + "SKILL_";

                public const string LIST_TITLE = BASE_PATH + nameof(LIST_TITLE);
                public const string LIST_DESCRIPTION = BASE_PATH + nameof(LIST_DESCRIPTION);
                public const string LIST_FOOTER = BASE_PATH + nameof(LIST_FOOTER);
                public const string LIST_ROW_TITLE = BASE_PATH + nameof(LIST_ROW_TITLE);
                public const string SHOW_TITLE = BASE_PATH + nameof(SHOW_TITLE);
                public const string SHOW_FOOTER = BASE_PATH + nameof(SHOW_FOOTER);
                public const string SHOW_IDTITLE = BASE_PATH + nameof(SHOW_IDTITLE);
                public const string SHOW_CATEGORY = BASE_PATH + nameof(SHOW_CATEGORY);
                public const string SHOW_PARENT = BASE_PATH + nameof(SHOW_PARENT);
                public const string SHOW_UNLOCKAT = BASE_PATH + nameof(SHOW_UNLOCKAT);
                public const string SHOW_MAXLEVEL = BASE_PATH + nameof(SHOW_MAXLEVEL);
                public const string SHOW_LEVELS = BASE_PATH + nameof(SHOW_LEVELS);
                public const string SHOW_TABLE_LVLHEADER = BASE_PATH + nameof(SHOW_TABLE_LVLHEADER);
                public const string SHOW_TABLE_COSTHEADER = BASE_PATH + nameof(SHOW_TABLE_COSTHEADER);
                public const string SHOW_TABLE_COSTCUMULATIVEHEADER = BASE_PATH + nameof(SHOW_TABLE_COSTCUMULATIVEHEADER);
                public const string SHOW_TABLE_BONUSHEADER = BASE_PATH + nameof(SHOW_TABLE_BONUSHEADER);
                public const string SHOW_TABLE_EFFICENCYHEADER = BASE_PATH + nameof(SHOW_TABLE_EFFICENCYHEADER);
                public const string SHOW_UNLOCKCOST = BASE_PATH + nameof(SHOW_UNLOCKCOST);
                public const string NOPARENT = BASE_PATH + nameof(NOPARENT);
                public const string STAGE = BASE_PATH + nameof(STAGE);
                public const string SKILLPOINTS = BASE_PATH + nameof(SKILLPOINTS);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { LIST_TITLE, "Skill listing" },
                        { LIST_DESCRIPTION, "All Skills" },
                        { LIST_FOOTER, "{0} Skill tool" },
                        { SHOW_TITLE, "Skill data for {0}" },
                        { SHOW_FOOTER, "{0} Skill tool | TT2 v{1}" },
                        { SHOW_IDTITLE, "Skill id" },
                        { SHOW_CATEGORY, "Skill branch" },
                        { SHOW_PARENT, "Requires" },
                        { SHOW_UNLOCKAT, "Ulocked at" },
                        { SHOW_MAXLEVEL, "Max level" },
                        { SHOW_TABLE_LVLHEADER, "Level" },
                        { SHOW_TABLE_COSTHEADER, "SP" },
                        { SHOW_TABLE_COSTCUMULATIVEHEADER, "Total SP" },
                        { SHOW_TABLE_BONUSHEADER, "Bonus" },
                        { SHOW_TABLE_EFFICENCYHEADER, "Efficency" },
                        { SHOW_LEVELS, "Levels" },
                        { SHOW_UNLOCKCOST, "Pre-requisit cost" },
                        { NOPARENT, "None" },
                        { STAGE, "Stage {0}" },
                        { SKILLPOINTS, "{0} SP" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

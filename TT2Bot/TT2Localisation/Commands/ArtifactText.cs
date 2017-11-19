using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class CommandText
        {
            public static class ArtifactText
            {
                public const string BASE_PATH = CommandText.BASE_PATH + "ARTIFACT_";

                public const string LIST_TITLE = BASE_PATH + nameof(LIST_TITLE);
                public const string LIST_DESCRIPTION = BASE_PATH + nameof(LIST_DESCRIPTION);
                public const string LIST_FOOTER = BASE_PATH + nameof(LIST_FOOTER);
                public const string LIST_ROW_TITLE = BASE_PATH + nameof(LIST_ROW_TITLE);
                public const string SHOW_TITLE = BASE_PATH + nameof(SHOW_TITLE);
                public const string SHOW_FOOTER = BASE_PATH + nameof(SHOW_FOOTER);
                public const string SHOW_IDTITLE = BASE_PATH + nameof(SHOW_IDTITLE);
                public const string SHOW_TIERTITLE = BASE_PATH + nameof(SHOW_TIERTITLE);
                public const string SHOW_TEIRVALUE = BASE_PATH + nameof(SHOW_TEIRVALUE);
                public const string SHOW_MAXLEVELTITLE = BASE_PATH + nameof(SHOW_MAXLEVELTITLE);
                public const string SHOW_BONUSTYPE = BASE_PATH + nameof(SHOW_BONUSTYPE);
                public const string SHOW_BONUSPERLVL = BASE_PATH + nameof(SHOW_BONUSPERLVL);
                public const string SHOW_COSTCOEF = BASE_PATH + nameof(SHOW_COSTCOEF);
                public const string SHOW_COSTEXPO = BASE_PATH + nameof(SHOW_COSTEXPO);
                public const string SHOW_COSTOFLVL = BASE_PATH + nameof(SHOW_COSTOFLVL);
                public const string SHOW_COST = BASE_PATH + nameof(SHOW_COST);
                public const string SHOW_LVLRANGE = BASE_PATH + nameof(SHOW_LVLRANGE);
                public const string SHOW_EFFECTAT = BASE_PATH + nameof(SHOW_EFFECTAT);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { LIST_TITLE, "Artifact listing" },
                        { LIST_DESCRIPTION, "All Artifacts" },
                        { LIST_FOOTER, "{0} Artifact tool" },
                        { LIST_ROW_TITLE, "Tier {0}" },
                        { SHOW_TITLE, "Artifact data for {0}" },
                        { SHOW_FOOTER, "{0} Artifact tool | TT2 v{1}" },
                        { SHOW_IDTITLE, "Artifact id" },
                        { SHOW_TIERTITLE, "Tier" },
                        { SHOW_TEIRVALUE, "[{0}](https://redd.it/732nk1)" },
                        { SHOW_MAXLEVELTITLE, "Max level" },
                        { SHOW_BONUSTYPE, "Effect type"},
                        { SHOW_BONUSPERLVL, "Effect per Level" },
                        { SHOW_COSTCOEF, "Cost Coeficient" },
                        { SHOW_COSTEXPO, "Cost Exponent" },
                        { SHOW_COSTOFLVL, "Cost of lvl {0}" },
                        { SHOW_COST, "{0} relics" },
                        { SHOW_LVLRANGE, "Cost for {0} -> {1}" },
                        { SHOW_EFFECTAT, "Effect at lvl {0}" }
                    }.ToImmutableDictionary();
            }
        }
    }
}
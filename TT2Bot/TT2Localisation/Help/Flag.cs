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
            public static class Flag
            {
                private const string BASE_PATH = Help.BASE_PATH + "FLAG_";

                public const string APPLY_G = BASE_PATH + nameof(APPLY_G);
                public const string APPLY_I = BASE_PATH + nameof(APPLY_I);
                public const string APPLY_R = BASE_PATH + nameof(APPLY_R);
                public const string APPLY_A = BASE_PATH + nameof(APPLY_A);
                public const string APPLY_T = BASE_PATH + nameof(APPLY_T);
                public const string EXCUSE_I = BASE_PATH + nameof(EXCUSE_I);
                public const string SUBMIT_D = BASE_PATH + nameof(SUBMIT_D);
                public const string SUBMIT_I = BASE_PATH + nameof(SUBMIT_I);
                public const string SUBMIT_R = BASE_PATH + nameof(SUBMIT_R);
                public const string SUBMIT_Q = BASE_PATH + nameof(SUBMIT_Q);
                public const string CLANSTATS_S = BASE_PATH + nameof(CLANSTATS_S);
                public const string CLANSTATS_T = BASE_PATH + nameof(CLANSTATS_T);
                public const string CLANSTATS_A = BASE_PATH + nameof(CLANSTATS_A);
                public const string PRESTIGE_B = BASE_PATH + nameof(PRESTIGE_B);
                public const string PRESTIGE_C = BASE_PATH + nameof(PRESTIGE_C);
                public const string PRESTIGE_I = BASE_PATH + nameof(PRESTIGE_I);
                public const string HELPER_G = BASE_PATH + nameof(HELPER_G);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {

                        { APPLY_G, "Specifies that the application is a global application." },
                        { APPLY_I, "Specifies a list of images to use with your application" },
                        { APPLY_R, "Specifies how many relics you have earned" },
                        { APPLY_A, "Specifies how many attacks you aim to do per week" },
                        { APPLY_T, "Specifies how many taps you average per CQ" },
                        { EXCUSE_I, "Specifies an ID to use" },
                        { SUBMIT_D, "Describe the submission" },
                        { SUBMIT_I, "Links an image to the submission" },
                        { SUBMIT_R, "Links a reddit url to the submission" },
                        { SUBMIT_Q, "Specifies that there should be no DM sent to the submitter" },
                        { CLANSTATS_S, "Average max stage to use" },
                        { CLANSTATS_T, "Average taps to use" },
                        { CLANSTATS_A, "Number of attackers to use (array)" },
                        { PRESTIGE_B, "Uses the given BoS level" },
                        { PRESTIGE_C, "Uses the given clan level" },
                        { PRESTIGE_I, "Uses the given IP level" },
                        { HELPER_G, "Orders the heroes rather than grouping them" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

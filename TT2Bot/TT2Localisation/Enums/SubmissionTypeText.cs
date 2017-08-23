using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Enums
        {
            public static class SubmissionTypeText
            {
                public const string BASE_PATH = Enums.BASE_PATH + "SUBMISSIONTYPE_";

                public const string BUG = BASE_PATH + nameof(BUG);
                public const string SUGGESTION = BASE_PATH + nameof(SUGGESTION);
                public const string QUESTION = BASE_PATH + nameof(QUESTION);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { BUG, "Bug" },
                        { SUGGESTION, "Suggestion" },
                        { QUESTION, "Question" },
                    }.ToImmutableDictionary();
            }
        }
    }
}

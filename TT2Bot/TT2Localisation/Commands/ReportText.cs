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
            public static class ReportText
            {
                private const string BASE_PATH = Commands.BASE_PATH + "REPORT_";

                public const string MISSING_CHANNEL = BASE_PATH + nameof(MISSING_CHANNEL);
                public const string SENT = BASE_PATH + nameof(SENT);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { MISSING_CHANNEL, "I could not find where I need to send the bug report! Please try again later." },
                        { SENT, "Bug report sent" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

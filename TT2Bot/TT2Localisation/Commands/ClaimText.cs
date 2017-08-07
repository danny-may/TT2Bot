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
            public static class ClaimText
            {
                private const string BASE_PATH = Commands.BASE_PATH + "CLAIM_";

                public const string MISSING_SUPPORTCODE = BASE_PATH + nameof(MISSING_SUPPORTCODE);
                public const string INVALID_SUPPORTCODE = BASE_PATH + nameof(INVALID_SUPPORTCODE);
                public const string UNAVAILABLE_SUPPORTCODE = BASE_PATH + nameof(UNAVAILABLE_SUPPORTCODE);
                public const string SUPPORTCODE_OWNED = BASE_PATH + nameof(SUPPORTCODE_OWNED);
                public const string SUPPORTCODE_NEW = BASE_PATH + nameof(SUPPORTCODE_NEW);
                public const string SUPPORTCODE_UPDATED = BASE_PATH + nameof(SUPPORTCODE_UPDATED);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { MISSING_SUPPORTCODE, "You must supply a support code!" },
                        { INVALID_SUPPORTCODE, "That is an invalid support code" },
                        { UNAVAILABLE_SUPPORTCODE, "That support code is already claimed!" },
                        { SUPPORTCODE_OWNED, "You already have the support code `{0}` claimed!" },
                        { SUPPORTCODE_NEW, "You have claimed the support code `{0}`" },
                        { SUPPORTCODE_UPDATED, "You have claimed the support code `{0}` and given up ownership of {1}" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

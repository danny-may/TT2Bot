using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanBot.Commands;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Game
        {
            public static class Pet
            {
                private const string BASE_PATH = Game.BASE_PATH + "PET_";

                public const string TOSTRING = BASE_PATH + nameof(TOSTRING);
                public const string UNABLE_DOWNLOAD = BASE_PATH + nameof(UNABLE_DOWNLOAD);
                public const string MULTIPLE_MATCHES = BASE_PATH + nameof(MULTIPLE_MATCHES);
                public static LocalisedString GetName(int artifactId)
                    => new LocalisedString(BASE_PATH + artifactId.ToString());

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { TOSTRING, "{0} ({1})" },
                        { UNABLE_DOWNLOAD, "I could not download any pet data. Please try again later." },
                        { MULTIPLE_MATCHES, "There were more than 1 pets that matched `{0}`" },
                        { GetName(1).Key, "Nova" },
                        { GetName(2).Key, "Toto" },
                        { GetName(3).Key, "Cerberus" },
                        { GetName(4).Key, "Mousy" },
                        { GetName(5).Key, "Harker" },
                        { GetName(6).Key, "Bubbles" },
                        { GetName(7).Key, "Demos" },
                        { GetName(8).Key, "Tempest" },
                        { GetName(9).Key, "Basky" },
                        { GetName(10).Key, "Scraps" },
                        { GetName(11).Key,  "Zero" },
                        { GetName(12).Key,  "Polly" },
                        { GetName(13).Key,  "Hamy" },
                        { GetName(14).Key,  "Phobos" },
                        { GetName(15).Key,  "Fluffers" },
                        { GetName(16).Key,  "Kit" },

                    }.ToImmutableDictionary();
            }
        }
    }
}

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Commands
        {
            private const string BASE_PATH = "COMMANDS_";

            public const string DELAYMESSAGE_DATA = BASE_PATH + nameof(DELAYMESSAGE_DATA);
            public const string MAXSTAGE = BASE_PATH + nameof(MAXSTAGE);
            public const string RELICS = BASE_PATH + nameof(RELICS);
            public const string ATTACKSPERWEEK = BASE_PATH + nameof(ATTACKSPERWEEK);
            public const string TAPSPERCQ = BASE_PATH + nameof(TAPSPERCQ);
            public const string IMAGES = BASE_PATH + nameof(IMAGES);
            public const string NONE = BASE_PATH + nameof(NONE);

            public static IReadOnlyDictionary<string, string> Defaults { get; }
                = new Dictionary<string, string>
                {
                    { DELAYMESSAGE_DATA, "This might take a short while, theres a fair bit of data to download!" },
                    { MAXSTAGE, "Max Stage" },
                    { RELICS, "Relics" },
                    { ATTACKSPERWEEK, "CQ/Week" },
                    { TAPSPERCQ, "Taps/CQ" },
                    { IMAGES, "Images" },
                    { NONE, "None" }
                }.Concat(ReportText.Defaults)
                 .Concat(SuggestText.Defaults)
                 .Concat(ApplyText.Defaults)
                 .Concat(ExcuseText.Defaults)
                 .Concat(SubmitText.Defaults)
                 .Concat(TitanLordText.Defaults)
                 .Concat(ArtifactText.Defaults)
                 .Concat(EquipmentText.Defaults)
                 .Concat(ClaimText.Defaults)
                 .Concat(ClanStatsText.Defaults)
                 .Concat(PetText.Defaults)
                 .Concat(PrestigeText.Defaults)
                 .Concat(HelperText.Defaults)
                 .ToImmutableDictionary();
        }
    }
}

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Enums
        {
            private const string BASE_PATH = "ENUM_";
            public static IReadOnlyDictionary<string, string> Defaults { get; }
                = new Dictionary<string, string>().Concat(BonusTypeText.Defaults)
                                                  .Concat(EquipmentClassText.Defaults)
                                                  .Concat(EquipmentRarityText.Defaults)
                                                  .Concat(EquipmentSourceText.Defaults)
                                                  .Concat(HelperTypeText.Defaults)
                                                  .ToImmutableDictionary();
        }
    }
}

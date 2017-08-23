using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TT2Bot.GameEntity.Base;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static IReadOnlyDictionary<string, string> Defaults { get; }
            = new Dictionary<string, string>().Concat(Help.Defaults)
                                               .Concat(CommandText.Defaults)
                                               .Concat(GameEntityLocalisation.Defaults)
                                               .Concat(Enums.Defaults)
                                               .Concat(Types.Defaults)
                                               .Concat(FormatTypes.GetDefaults)
                                               .Concat(Settings.Defaults)
                                               .ToImmutableDictionary();


    }
}

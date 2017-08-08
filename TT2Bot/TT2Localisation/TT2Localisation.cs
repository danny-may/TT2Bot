using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;


namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static IReadOnlyDictionary<string, string> Defaults { get; }
            = new Dictionary<string, string>().Concat(Help.Defaults)
                                               .Concat(Commands.Defaults)
                                               .Concat(Game.Defaults)
                                               .Concat(Enums.Defaults)
                                               .Concat(Types.Defaults)
                                               .Concat(FormatTypes.GetDefaults)
                                               .Concat(Settings.Defaults)
                                               .ToImmutableDictionary();

        
    }
}

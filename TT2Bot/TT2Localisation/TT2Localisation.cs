using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanBot.Formatting;


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
                                               .ToImmutableDictionary();

        
    }
}

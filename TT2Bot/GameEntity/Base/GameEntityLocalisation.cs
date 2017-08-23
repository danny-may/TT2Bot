using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TT2Bot.GameEntity.Localisation;

namespace TT2Bot.GameEntity.Base
{
    public static class GameEntityLocalisation
    {
        public const string BASE_PATH = "GAMEENTITY_";

        public static IReadOnlyDictionary<string, string> Defaults { get; }
            = new Dictionary<string, string>().Concat(EntityLocalisation.Defaults)
                                              .ToImmutableDictionary();
    }
}

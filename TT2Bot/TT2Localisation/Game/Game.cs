using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Game
        {
            private const string BASE_PATH = "GAME_";

            public static IReadOnlyDictionary<string, string> Defaults { get; }
                = new Dictionary<string, string>().Concat(Artifact.Defaults)
                                                  .Concat(Equipment.Defaults)
                                                  .Concat(Pet.Defaults)
                                                  .Concat(Helper.Defaults)
                                                  .Concat(HelperSkill.Defaults)
                                                  .Concat(SkillTree.Defaults)
                                                  .ToImmutableDictionary();
        }
    }
}

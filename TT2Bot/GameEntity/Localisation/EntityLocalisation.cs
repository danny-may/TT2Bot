using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;

namespace TT2Bot.GameEntity.Localisation
{
    public static class EntityLocalisation
    {
        public const string BASE_PATH = GameEntityLocalisation.BASE_PATH + "ENTITIES_";

        public static IReadOnlyDictionary<string, string> Defaults { get; }
            = new Dictionary<string, string>()
            .Concat(Artifact.Localisation.Defaults)
            .Concat(Equipment.Localisation.Defaults)
            .Concat(Helper.Localisation.Defaults)
            .Concat(HelperSkill.Localisation.Defaults)
            .Concat(Pet.Localisation.Defaults)
            .Concat(Skill.Localisation.Defaults)
            .ToImmutableDictionary();
    }
}

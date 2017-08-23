using TitanBot.Formatting.Interfaces;
using static TT2Bot.TT2Localisation.Enums;

namespace TT2Bot.GameEntity.Enums
{
    public enum SkillCategory
    {
        None,
        Left,
        Center,
        Right
    }

    public static class SkillCategoryMethods
    {
        public static ILocalisable<string> ToLocalisable(this SkillCategory category)
            => SkillCategoryText.Localisable(category);
    }


}

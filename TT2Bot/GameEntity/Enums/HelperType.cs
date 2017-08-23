using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using static TT2Bot.TT2Localisation.Enums.HelperTypeText;

namespace TT2Bot.GameEntity.Enums
{
    public enum HelperType
    {
        None,
        Melee,
        Spell,
        Ranged
    }

    public static class HelperTypeMethods
    {
        public static ILocalisable<string> ToLocalisable(this HelperType helperType)
        {
            switch (helperType)
            {
                case HelperType.Spell: return (LocalisedString)SPELL;
                case HelperType.Melee: return (LocalisedString)MELEE;
                case HelperType.Ranged: return (LocalisedString)RANGE;
                case HelperType.None: return (LocalisedString)NONE;
            }
            return (RawString)helperType.ToString();
        }
    }
}

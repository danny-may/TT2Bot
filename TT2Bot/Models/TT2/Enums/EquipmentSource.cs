using TitanBot.Commands;
using static TT2Bot.TT2Localisation.Enums.EquipmentSourceText;

namespace TT2Bot.Models
{
    public enum EquipmentSource
    {
        Default,
        Valentines
    }

    public static class EquipmentSourceMethods
    {
        public static LocalisedString ToLocalisable(this EquipmentSource equipSource)
        {
            switch (equipSource)
            {
                case EquipmentSource.Default: return (LocalisedString)DEFAULT;
                case EquipmentSource.Valentines: return (LocalisedString)VALENTINES;
            }
            return (RawString)equipSource.ToString();
        }
    }
}

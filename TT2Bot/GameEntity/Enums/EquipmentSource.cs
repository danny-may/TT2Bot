using TitanBot.Formatting.Interfaces;
using static TT2Bot.TT2Localisation.Enums;

namespace TT2Bot.GameEntity.Enums
{
    public enum EquipmentSource
    {
        Default,
        Valentines,
        EquipmentSets
    }

    public static class EquipmentSourceMethods
    {
        public static ILocalisable<string> ToLocalisable(this EquipmentSource equipSource)
            => EquipmentSourceText.Localisable(equipSource);
    }
}
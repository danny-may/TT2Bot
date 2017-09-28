using TitanBot.Formatting.Interfaces;
using static TT2Bot.TT2Localisation.Enums;

namespace TT2Bot.GameEntity.Enums
{
    public enum EquipmentRarity
    {
        Removed = 0,
        Common = 1,
        Rare = 2,
        Legendary = 3,
        EquipmentSet = 4
    }

    public static class EquipmentRarityMethods
    {
        public static ILocalisable<string> ToLocalisable(this EquipmentRarity rarity)
            => EquipmentRarityText.Localisable(rarity);
    }
}
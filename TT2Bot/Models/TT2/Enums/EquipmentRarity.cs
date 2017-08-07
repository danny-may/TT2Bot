using TitanBot.Commands;
using static TT2Bot.TT2Localisation.Enums.EquipmentRarityText;

namespace TT2Bot.Models
{
    public enum EquipmentRarity
    {
        Removed = 0,
        Common = 1,
        Rare = 2,
        Legendary = 3,
    }

    public static class EquipmentRarityMethods
    {
        public static LocalisedString ToLocalisable(this EquipmentRarity equipRarity)
        {
            switch (equipRarity)
            {
                case EquipmentRarity.Removed: return (LocalisedString)REMOVED;
                case EquipmentRarity.Common: return (LocalisedString)COMMON;
                case EquipmentRarity.Rare: return (LocalisedString)RARE;
                case EquipmentRarity.Legendary: return (LocalisedString)LEGENDARY;
            }
            return (RawString)equipRarity.ToString();
        }
    }
}

using System.Collections.Generic;
using System.Collections.Immutable;
using TitanBot.Formatting;
using TT2Bot.GameEntity.Enums;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Enums
        {
            public static class EquipmentClassText
            {
                public const string BASE_PATH = Enums.BASE_PATH + "EQUIPMENTCLASS_";

                public static LocalisedString Localisable(EquipmentClass eClass)
                    => new LocalisedString(BASE_PATH + eClass.ToString().ToUpper());

                public static LocalisedString LocalisableAlt(EquipmentClass eClass)
                    => new LocalisedString(BASE_PATH + eClass.ToString().ToUpper() + "_ALT");

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { Localisable(EquipmentClass.None).Key, "Removed" },
                        { Localisable(EquipmentClass.Weapon).Key, "Weapon" },
                        { Localisable(EquipmentClass.Hat).Key, "Hat" },
                        { Localisable(EquipmentClass.Suit).Key, "Suit" },
                        { Localisable(EquipmentClass.Aura).Key, "Aura" },
                        { Localisable(EquipmentClass.Slash).Key, "Slash" },
                        { LocalisableAlt(EquipmentClass.None).Key, "" },
                        { LocalisableAlt(EquipmentClass.Weapon).Key, "Sword" },
                        { LocalisableAlt(EquipmentClass.Hat).Key, "Helmet" },
                        { LocalisableAlt(EquipmentClass.Suit).Key, "Chest,Body" },
                        { LocalisableAlt(EquipmentClass.Aura).Key, "" },
                        { LocalisableAlt(EquipmentClass.Slash).Key, "" }
                    }.ToImmutableDictionary();
            }

            public static class EquipmentRarityText
            {
                public const string BASE_PATH = Enums.BASE_PATH + "EQUIPMENTRARITY_";

                public static LocalisedString Localisable(EquipmentRarity rarity)
                    => new LocalisedString(BASE_PATH + rarity.ToString().ToUpper());

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { Localisable(EquipmentRarity.Removed).Key, "Removed" },
                        { Localisable(EquipmentRarity.Common).Key, "Common" },
                        { Localisable(EquipmentRarity.Rare).Key, "Rare" },
                        { Localisable(EquipmentRarity.Legendary).Key, "Legendary" }
                    }.ToImmutableDictionary();
            }

            public static class EquipmentSourceText
            {
                public const string BASE_PATH = Enums.BASE_PATH + "EQUIPMENTSOURCE_";

                public static LocalisedString Localisable(EquipmentSource source)
                    => new LocalisedString(BASE_PATH + source.ToString().ToUpper());

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { Localisable(EquipmentSource.Default).Key, "Default" },
                        { Localisable(EquipmentSource.Valentines).Key, "Valentines" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

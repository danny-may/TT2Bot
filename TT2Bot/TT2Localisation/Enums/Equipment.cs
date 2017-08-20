using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Enums
        {
            public static class EquipmentClassText
            {
                public const string BASE_PATH = Enums.BASE_PATH + "EQUIPMENTCLASS_";

                public const string NONE = BASE_PATH + nameof(NONE);
                public const string WEAPON = BASE_PATH + nameof(WEAPON);
                public const string HAT = BASE_PATH + nameof(HAT);
                public const string SUIT = BASE_PATH + nameof(SUIT);
                public const string AURA = BASE_PATH + nameof(AURA);
                public const string SLASH = BASE_PATH + nameof(SLASH);
                public const string WEAPON_ALT = BASE_PATH + nameof(WEAPON_ALT);
                public const string HAT_ALT = BASE_PATH + nameof(HAT_ALT);
                public const string SUIT_ALT = BASE_PATH + nameof(SUIT_ALT);
                public const string AURA_ALT = BASE_PATH + nameof(AURA_ALT);
                public const string SLASH_ALT = BASE_PATH + nameof(SLASH_ALT);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { NONE, "Removed" },
                        { WEAPON, "Weapon" },
                        { HAT, "Hat" },
                        { SUIT, "Suit" },
                        { AURA, "Aura" },
                        { SLASH, "Slash" },
                        { WEAPON_ALT, "Sword" },
                        { HAT_ALT, "Helmet" },
                        { SUIT_ALT, "Chest,Body" },
                        { AURA_ALT, null },
                        { SLASH_ALT, null }
                    }.ToImmutableDictionary();
            }

            public static class EquipmentRarityText
            {
                public const string BASE_PATH = Enums.BASE_PATH + "EQUIPMENTRARITY_";

                public const string REMOVED = BASE_PATH + nameof(REMOVED);
                public const string COMMON = BASE_PATH + nameof(COMMON);
                public const string RARE = BASE_PATH + nameof(RARE);
                public const string LEGENDARY = BASE_PATH + nameof(LEGENDARY);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { REMOVED, "Removed" },
                        { COMMON, "Common" },
                        { RARE, "Rare" },
                        { LEGENDARY, "Legendary" }
                    }.ToImmutableDictionary();
            }

            public static class EquipmentSourceText
            {
                public const string BASE_PATH = Enums.BASE_PATH + "EQUIPMENTSOURCE_";

                public const string DEFAULT = BASE_PATH + nameof(DEFAULT);
                public const string VALENTINES = BASE_PATH + nameof(VALENTINES);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { DEFAULT, "Default" },
                        { VALENTINES, "Valentines" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

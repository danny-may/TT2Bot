using System;
using System.Linq;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using static TT2Bot.TT2Localisation.Enums;

namespace TT2Bot.GameEntity.Enums
{
    public enum EquipmentClass
    {
        None = -1,
        Weapon,
        Hat,
        Suit,
        Aura,
        Slash
    }

    public static class EquipmentClassMethods
    {
        public static ILocalisable<string> ToLocalisable(this EquipmentClass equipClass)
            => EquipmentClassText.Localisable(equipClass);

        public static string[] GetNames(this EquipmentClass equipClass, ITextResourceCollection textResource)
        {
            var alt = EquipmentClassText.LocalisableAlt(equipClass).Localise(textResource);
            var main = equipClass.ToLocalisable().Localise(textResource);
            if (alt == null)
                return new string[] { main };
            else
                return new string[] { main }.Concat(alt.Split(',')).ToArray();
        }

        public static EquipmentClass Find(ITextResourceCollection textResource, string text)
        {
            foreach (var equipClass in Enum.GetValues(typeof(EquipmentClass)).Cast<EquipmentClass>())
                if (GetNames(equipClass, textResource).Any(n => n.ToLower() == text.ToLower()))
                    return equipClass;
            return EquipmentClass.None;
        }
    }
}

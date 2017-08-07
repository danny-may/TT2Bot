using System;
using System.Linq;
using TitanBot.Formatting;
using static TT2Bot.TT2Localisation.Enums.EquipmentClassText;

namespace TT2Bot.Models
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

    public static class EquipmentClasseMethods
    {
        public static LocalisedString ToLocalisable(this EquipmentClass equipClass)
        {
            switch (equipClass)
            {
                case EquipmentClass.None: return (LocalisedString)NONE;
                case EquipmentClass.Weapon: return (LocalisedString)WEAPON;
                case EquipmentClass.Hat: return (LocalisedString)HAT;
                case EquipmentClass.Suit: return (LocalisedString)SUIT;
                case EquipmentClass.Aura: return (LocalisedString)AURA;
                case EquipmentClass.Slash: return (LocalisedString)SLASH;
            }
            return (RawString)equipClass.ToString();
        }

        public static string[] GetNames(this EquipmentClass equipClass, ITextResourceCollection textResource)
        {
            string alt = null;
            switch (equipClass)
            {
                case EquipmentClass.None: return new string[0];
                case EquipmentClass.Weapon: alt = textResource.GetResource(WEAPON_ALT); break;
                case EquipmentClass.Hat: alt = textResource.GetResource(HAT_ALT); break;
                case EquipmentClass.Suit: alt = textResource.GetResource(SUIT_ALT); break;
                case EquipmentClass.Aura: alt = textResource.GetResource(AURA_ALT); break;
                case EquipmentClass.Slash: alt = textResource.GetResource(SLASH_ALT); break;
            }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatter;
using TitanBot.TypeReaders;
using TT2Bot.Models;

namespace TT2Bot.Overrides
{
    class Formatter : OutputFormatter
    {
        static Dictionary<BonusType, string> BonusTypeMap { get; } = LoadBonusTypes();
        static Dictionary<EquipmentClass, string> EquipmentClassMap { get; } = LoadEquipmentClasses();
        static Dictionary<HelperType, string> HelperTypeMap { get; } = LoadHelperTypes();

        static Dictionary<HelperType, string> LoadHelperTypes()
            => new Dictionary<HelperType, string>
            {
                { HelperType.Melee, "Melee" },
                { HelperType.Ranged, "Ranged" },
                { HelperType.Spell, "Spell" },
            };
        static Dictionary<EquipmentClass, string> LoadEquipmentClasses()
            => new Dictionary<EquipmentClass, string>{
                    { EquipmentClass.Aura, "Aura" },
                    { EquipmentClass.Hat, "Hat" },
                    { EquipmentClass.None, "None" },
                    { EquipmentClass.Slash, "Slash" },
                    { EquipmentClass.Suit, "Suit" },
                    { EquipmentClass.Weapon, "Weapon" },
                };
        static Dictionary<BonusType, string> LoadBonusTypes()
            => new Dictionary<BonusType, string>
            {
                {BonusType.AllDamage, "All Damage"},
                {BonusType.AllHelperDamage, "All Hero Damage"},
                {BonusType.ArmorBoost, "Armor Equipment Bonus"},
                {BonusType.ArtifactDamage, "Artifact Damage"},
                {BonusType.BurstDamageSkillAmount, "Heavenly Strike Effect"},
                {BonusType.BurstDamageSkillMana, "Heavenly Strike Cost"},
                {BonusType.ChestAmount, "Chesterson Amount"},
                {BonusType.ChestChance, "Chesterson Chance"},
                {BonusType.CritBoostSkillDuration, "Critical Strike Duration"},
                {BonusType.CritBoostSkillMana, "Critical Strike Cost"},
                {BonusType.CritChance, "Critical Chance"},
                {BonusType.DoubleFairyChance, "Double Fairy Chance"},
                {BonusType.GoldAll, "All Gold"},
                {BonusType.GoldBoss, "Boss Gold"},
                {BonusType.GoldMonster, "Titan Gold"},
                {BonusType.Goldx10Chance, "x10 Gold Chance"},
                {BonusType.HandOfMidasSkillAmount, "Hand of Midas Effect"},
                {BonusType.HandOfMidasSkillDuration, "Hand of Midas Duration"},
                {BonusType.HandOfMidasSkillMana, "Hand of Midas Cost"},
                {BonusType.HelmetBoost, "Helmet Equipment Bonus"},
                {BonusType.HelperBoostSkillAmount, "War Cry Effect"},
                {BonusType.HelperBoostSkillDuration, "War Cry Duration"},
                {BonusType.HelperBoostSkillMana, "War Cry Cost"},
                {BonusType.HelperUpgradeCost, "Hero Cost"},
                {BonusType.HSArtifactDamage, "All Artifact Damage"},
                {BonusType.MeleeHelperDamage, "Melee Hero Damage"},
                {BonusType.PetDamageMult, "Pet Damage"},
                {BonusType.PrestigeRelic, "Prestige Relics"},
                {BonusType.RangedHelperDamage, "Ranged Hero Damage"},
                {BonusType.ShadowCloneSkillAmount, "Shadow Clone Effect"},
                {BonusType.ShadowCloneSkillDuration, "Shadow Clone Duration"},
                {BonusType.ShadowCloneSkillMana, "Shadow Clone Cost"},
                {BonusType.SlashBoost, "Slash Equipment Bonus"},
                {BonusType.SpellHelperDamage, "Spell Hero Damage"},
                {BonusType.SwordBoost, "Weapon Equipment Bonus"},
                {BonusType.TapBoostSkillAmount, "Fire Sword Effect"},
                {BonusType.TapBoostSkillDuration, "Fire Sword Duration"},
                {BonusType.TapBoostSkillMana, "Fire Sword Cost"},
                {BonusType.TapDamage, "Tap Damage"},
                {BonusType.SplashDamage, "Splash Damage"},
                {BonusType.ManaRegen, "Mana Regen"},
                {BonusType.CritDamage, "Crit Damage"},
                {BonusType.ManaPoolCap, "Mana Pool Cap"},
                {BonusType.TapDamageFromHelpers, "% Tap Damage From Heroes"},
                {BonusType.MonsterHP, "Titan HP"}
            };

        public Formatter(ICommandContext context, ITypeReaderCollection typeReaders, bool altFormat) : base(context, typeReaders, altFormat)
        {
            Add<int>(Beautify, TryParse);
            Add<double>(Beautify, TryParse);

            Add<BonusType>(Beautify, TryParse);
            Add<EquipmentClass>(Beautify, TryParse);
            Add<HelperType>(Beautify, TryParse);
        }
        
        static string Format(string t)
            => new string(t.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLower();

        private string Beautify(int value)
            => Beautify((double)value);

        private bool TryParse(string text, out int value)
        {
            value = 0;
            if (!TryParse(text, out double parsed) || parsed > int.MaxValue)
                return false;
            value = (int)parsed;
            return true;
        }

        string alphabet = "abcdefghijklmnopqrstuvwxyz";

        private string Beautify(double value)
        {
            string sign = "";
            
            if (double.IsNaN(value))
                return "NaN";
            if (value == 0)
                return "0";
            if (value < 0)
            {
                sign = "-";
                value = -value;
            }
            if (double.IsInfinity(value))
                return sign+"∞";
            var postfixes = new string[] { "", "K", "M", "B", "T" };
            var magnitude = (int)Math.Floor(Math.Log10(value)) / 3;
            string postfix;

            if (magnitude > postfixes.Length - 1)
                postfix = alphabet[(magnitude - (postfixes.Length)) / 26].ToString() +
                          alphabet[(magnitude - (postfixes.Length)) % 26].ToString();
            else
                postfix = postfixes[magnitude];

            return sign+string.Format("{0:0.##} " + postfix, value / (Math.Pow(10, magnitude * 3)));
        }

        private bool TryParse(string text, out double value)
        {
            if (Regex.IsMatch(text, @"^\d+[kmbt]?$", RegexOptions.IgnoreCase))
            {
                value = 1;
                return true;
            }
            else if (Regex.IsMatch(text, $@"^\d+[{alphabet}]{{2}}$", RegexOptions.IgnoreCase))
            {
                value = 2;
                return true;
            }
            value = 0;
            return false;
        }

        private string Beautify(BonusType value)
        {
            if (BonusTypeMap.ContainsKey(value))
                return BonusTypeMap[value];
            return value.ToString();
        }

        private bool TryParse(string text, out BonusType value)
        {
            value = default(BonusType);
            var res = BonusTypeMap.Where(m => Format(m.Value) == Format(text));
            if (res.Count() != 1)
                return false;
            value = res.First().Key;
            return true;
        }

        private string Beautify(EquipmentClass value)
        {
            if (EquipmentClassMap.ContainsKey(value))
                return EquipmentClassMap[value];
            return value.ToString();
        }

        private bool TryParse(string text, out EquipmentClass value)
        {
            value = default(EquipmentClass);
            var res = EquipmentClassMap.Where(m => Format(m.Value) == Format(text));
            if (res.Count() != 1)
                return false;
            value = res.First().Key;
            return true;
        }

        private string Beautify(HelperType value)
        {
            if (HelperTypeMap.ContainsKey(value))
                return HelperTypeMap[value];
            return value.ToString();
        }

        private bool TryParse(string text, out HelperType value)
        {
            value = default(HelperType);
            var res = HelperTypeMap.Where(m => Format(m.Value) == Format(text));
            if (res.Count() != 1)
                return false;
            value = res.First().Key;
            return true;
        }
    }
}

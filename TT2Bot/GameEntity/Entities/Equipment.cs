using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Formatting;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Enums;
using TT2Bot.GameEntity.Enums.EntityId;
using TT2Bot.GameEntity.Localisation;
using TT2Bot.Models;

namespace TT2Bot.GameEntity.Entities
{
    class Equipment : GameEntity<EquipmentId>
    {
        public override LocalisedString Name => Localisation.GetName(Id);
        public override LocalisedString Abbreviations => Localisation.GetAbbreviation(Id);
        public EquipmentClass Class { get; }
        public BonusType BonusType { get; }
        public EquipmentRarity Rarity { get; }
        public double BonusBase { get; }
        public double BonusIncrease { get; }
        public EquipmentSource Source { get; }
        public override string ImageUrl => ImageUrls.TryGetValue(Id, out var url) ? url : _defaultImages[0];

        internal Equipment(EquipmentId id,
                           EquipmentClass eClass,
                           BonusType bonusType,
                           EquipmentRarity rarity,
                           double bonusBase,
                           double bonusIncrease,
                           EquipmentSource source,
                           string fileVersion,
                           Func<string, ValueTask<Bitmap>> imageGetter = null)
        {
            Id = id;
            Class = eClass;
            BonusType = bonusType;
            Rarity = rarity;
            BonusBase = bonusBase;
            BonusIncrease = bonusIncrease;
            Source = source;
            FileVersion = fileVersion;
            ImageGetter = imageGetter;
        }

        static Equipment()
        {
            ImageUrls = new Dictionary<EquipmentId, string>
            {
                { EquipmentId.Aura_Bats, Imgur("6JQGW3V") },
                { EquipmentId.Aura_Bird, Imgur("iIC7MeB") },
                { EquipmentId.Aura_BlackEnergy, Imgur("coFoXPO") },
                { EquipmentId.Aura_Bones, Imgur("Z7PytUd") },
                { EquipmentId.Aura_Bugs, Imgur("ntNAFxR") },
                { EquipmentId.Aura_Cards, Imgur("tBFW0zU") },
                { EquipmentId.Aura_Colors, Imgur("tpB3DKc") },
                { EquipmentId.Aura_Diamonds, Imgur("iilQvNH") },
                { EquipmentId.Aura_Embers, Imgur("kyKpotc") },
                { EquipmentId.Aura_Fire, Imgur("xzhcDlg") },
                { EquipmentId.Aura_GroundLightning, Imgur("di6vXmL") },
                { EquipmentId.Aura_Ice, Imgur("bK5vNwx") },
                { EquipmentId.Aura_Leaves, Imgur("oGEO2fa") },
                { EquipmentId.Aura_Lightning, Imgur("jbFiSs8") },
                { EquipmentId.Aura_Orbs, Imgur("73V8eiy") },
                { EquipmentId.Aura_Star, Imgur("FbFL1sJ") },
                { EquipmentId.Aura_Water, Imgur("LhT8Ljj") },
                { EquipmentId.Aura_Wind, Imgur("J61AR1Q") },
                { EquipmentId.Aura_Dizzy, Imgur("WJG8MJW") },
                { EquipmentId.Aura_Sparks, Imgur("YEN6i09") },
                { EquipmentId.Hat_Alien, Imgur("i8XIeqb") },
                { EquipmentId.Hat_Bag, Imgur("bbRCeSq") },
                { EquipmentId.Hat_BatGrey, Imgur("rjcCUwM") },
                { EquipmentId.Hat_BatRed, Imgur("PbvtRuE") },
                { EquipmentId.Hat_Cap, Imgur("S2shK39") },
                { EquipmentId.Hat_Cat, Imgur("HwKnbnI") },
                { EquipmentId.Hat_Chicken, Imgur("nf87Btz") },
                { EquipmentId.Hat_Football, Imgur("Ynx0btx") },
                { EquipmentId.Hat_HelmDevil, Imgur("tNc4yOa") },
                { EquipmentId.Hat_HelmHorned, Imgur("sfCcQ0H") },
                { EquipmentId.Hat_HelmTall, Imgur("iVON03I") },
                { EquipmentId.Hat_HelmWhite, Imgur("MwZHghp") },
                { EquipmentId.Hat_Ice, Imgur("KzpyO7H") },
                { EquipmentId.Hat_Indie, Imgur("UFMgJnU") },
                { EquipmentId.Hat_Ironman, Imgur("VZ2ubL2") },
                { EquipmentId.Hat_KickAss, Imgur("UeuRVEA") },
                { EquipmentId.Hat_Kid, Imgur("RQ71eNa") },
                { EquipmentId.Hat_Kitty, Imgur("Re6PnpZ") },
                { EquipmentId.Hat_Link, Imgur("hxb0ZbK") },
                { EquipmentId.Hat_Mega, Imgur("3ajJaNm") },
                { EquipmentId.Hat_Music, Imgur("7TKeUjt") },
                { EquipmentId.Hat_Ninja, Imgur("3JjGYku") },
                { EquipmentId.Hat_Pineapple, Imgur("TTwmbqd") },
                { EquipmentId.Hat_Poo, Imgur("2NdDUNZ") },
                { EquipmentId.Hat_PowerRanger, Imgur("5kr3Icd") },
                { EquipmentId.Hat_Robot, Imgur("3OvMXoX") },
                { EquipmentId.Hat_Soccer, Imgur("bgl0Fcu") },
                { EquipmentId.Hat_Top, Imgur("G57jlHn") },
                { EquipmentId.Hat_Transformer, Imgur("H5r8wa9") },
                { EquipmentId.Hat_Tribal, Imgur("aVN3Tix") },
                { EquipmentId.Hat_Wizard, Imgur("MmXcXJL") },
                { EquipmentId.Hat_Beanie, Imgur("J0M2hGl") },
                { EquipmentId.Hat_Detective, Imgur("J0M2hGl") },
                { EquipmentId.Hat_Ears, Imgur("J0M2hGl") },
                { EquipmentId.Hat_Halo, Imgur("J0M2hGl") },
                { EquipmentId.Hat_HeadPhones, Imgur("J0M2hGl") },
                { EquipmentId.Hat_MadHatter, Imgur("J0M2hGl") },
                { EquipmentId.Hat_RedTopHat, Imgur("J0M2hGl") },
                { EquipmentId.Hat_Ribbon, Imgur("J0M2hGl") },
                { EquipmentId.Hat_Santa, Imgur("J0M2hGl") },
                { EquipmentId.Hat_SmallTook, Imgur("J0M2hGl") },
                { EquipmentId.Hat_Took, Imgur("J0M2hGl") },
                { EquipmentId.Slash_Darkness, Imgur("myS7iUT") },
                { EquipmentId.Slash_Embers, Imgur("nq8Aq26") },
                { EquipmentId.Slash_EmbersBlue, Imgur("YvWfrqF") },
                { EquipmentId.Slash_Fire, Imgur("cATztqa") },
                { EquipmentId.Slash_FireBlack, Imgur("hlbXF8Y") },
                { EquipmentId.Slash_FireGray, Imgur("SPvqjt5") },
                { EquipmentId.Slash_FireOrange, Imgur("2y4R7m0") },
                { EquipmentId.Slash_Lava, Imgur("NI0nmjm") },
                { EquipmentId.Slash_Leaves, Imgur("YmfPd3H") },
                { EquipmentId.Slash_Lightning, Imgur("kWp2h7F") },
                { EquipmentId.Slash_Paint, Imgur("xgpUBxT") },
                { EquipmentId.Slash_Petals, Imgur("mOEIQam") },
                { EquipmentId.Slash_Rainbow, Imgur("mD91f1Y") },
                { EquipmentId.Slash_Smelly, Imgur("alOrEsp") },
                { EquipmentId.Slash_Stars, Imgur("Cb4b5GV") },
                { EquipmentId.Slash_StarsBlue, Imgur("rwMyYIo") },
                { EquipmentId.Slash_Water, Imgur("VMVVmyk") },
                { EquipmentId.Suit_ArmorFace, Imgur("dSOIAVN") },
                { EquipmentId.Suit_ArmorGreen, Imgur("HCulNSv") },
                { EquipmentId.Suit_ArmorHighTec, Imgur("qttmnrm") },
                { EquipmentId.Suit_ArmorIce, Imgur("GVk79M2") },
                { EquipmentId.Suit_ArmorOrange, Imgur("BxPamOw") },
                { EquipmentId.Suit_ArmorPurple, Imgur("nRn6ZkG") },
                { EquipmentId.Suit_ArmorRed, Imgur("3roDWVb") },
                { EquipmentId.Suit_ArmorRoman, Imgur("3IuMG7V") },
                { EquipmentId.Suit_ArmorRoyal, Imgur("WNgeKy6") },
                { EquipmentId.Suit_ArmorScale, Imgur("MD6BB8l") },
                { EquipmentId.Suit_ArmorWhite, Imgur("C8CPnxF") },
                { EquipmentId.Suit_Casual, Imgur("X3JY6NF") },
                { EquipmentId.Suit_Chicken, Imgur("H9ZJoZh") },
                { EquipmentId.Suit_Farmer, Imgur("gg7dNn1") },
                { EquipmentId.Suit_Ironman, Imgur("qn1WUvV") },
                { EquipmentId.Suit_KungFu, Imgur("a4c0za8") },
                { EquipmentId.Suit_Ninja, Imgur("mXucQ7T") },
                { EquipmentId.Suit_Robot, Imgur("T621x6N") },
                { EquipmentId.Suit_Snowman, Imgur("4N8qygD") },
                { EquipmentId.Suit_Wizard, Imgur("0FFKVox") },
                { EquipmentId.Weapon_Basic, Imgur("cvghMNg") },
                { EquipmentId.Weapon_Bat, Imgur("VZxcuFm") },
                { EquipmentId.Weapon_BattleAxe, Imgur("J3axNpt") },
                { EquipmentId.Weapon_Beam, Imgur("ZTPQFcG") },
                { EquipmentId.Weapon_Brute, Imgur("KFu0dV7") },
                { EquipmentId.Weapon_Buster, Imgur("nbFpycv") },
                { EquipmentId.Weapon_Carrot, Imgur("wVvI8Wo") },
                { EquipmentId.Weapon_Cleaver, Imgur("RuRKt2X") },
                { EquipmentId.Weapon_Club, Imgur("PDleVcS") },
                { EquipmentId.Weapon_Excalibur, Imgur("CSLTTu5") },
                { EquipmentId.Weapon_Fish, Imgur("vssvZuE") },
                { EquipmentId.Weapon_Gear, Imgur("ZZ7wDya") },
                { EquipmentId.Weapon_Glaive, Imgur("LALxySs") },
                { EquipmentId.Weapon_Halberd, Imgur("kWCa4TW") },
                { EquipmentId.Weapon_Hammer, Imgur("LIvAVnl") },
                { EquipmentId.Weapon_Holy, Imgur("PNc1aGL") },
                { EquipmentId.Weapon_Katana, Imgur("ER0jD17") },
                { EquipmentId.Weapon_Laser, Imgur("5ME9LEY") },
                { EquipmentId.Weapon_Ninja, Imgur("xzdPZwM") },
                { EquipmentId.Weapon_Pencil, Imgur("M7iR3xz") },
                { EquipmentId.Weapon_PitchFork, Imgur("A3nkxmX") },
                { EquipmentId.Weapon_Poison, Imgur("kXSqN3U") },
                { EquipmentId.Weapon_Saw, Imgur("JNYmyKi") },
                { EquipmentId.Weapon_Skull, Imgur("DD8ZgRD") },
                { EquipmentId.Weapon_Spear, Imgur("pixzDKe") },
                { EquipmentId.Weapon_Staff, Imgur("QXG7Tt9") },
                { EquipmentId.Weapon_Sythe, Imgur("d6uxNMl") },
                { EquipmentId.Weapon_WoodAxe, Imgur("VHLCk4w") },
                { EquipmentId.Weapon_ZigZag, Imgur("Nj5pcMX") },
                { EquipmentId.Aura_Valentines, Imgur("7WjHT6V") },
                { EquipmentId.Hat_Valentines, Imgur("KNAkgcU") },
                { EquipmentId.Weapon_Valentines, Imgur("LnE4Erc") }
            }.ToImmutableDictionary();
        }

        public double BonusOnLevel(double level)
        {
            return BonusBase + BonusIncrease * level;
        }

        //public override double MatchCertainty(ITextResourceCollection textResource, string text)
        //{
        //    var abbrevs = Abbreviations.Localise(textResource).ToLower().Split(',');
        //    return new List<double>
        //    {
        //        base.MatchCertainty(textResource, text),
        //        abbrevs.Any(a => a.ToLower() == text.ToLower()) ?  0.85 : 0,
        //        abbrevs.Any(a => a.ToLower().StartsWith(text.ToLower())) ?  0.65 : 0,
        //        abbrevs.Any(a => a.ToLower().Contains(text.ToLower())) ?  0.45 : 0,
        //        abbrevs.Any(a => a.ToLower().Without(" ") == text.ToLower().Without(" ")) ?  0.25 : 0,
        //        abbrevs.Any(a => a.ToLower().Without(" ").StartsWith(text.ToLower().Without(" "))) ?  0.05 : 0
        //    }.Max();
        //}

        private static List<string> _defaultImages = new List<string>
        {
            "http://imgur.com/J0M2hGl.png",
            "http://imgur.com/lUW3IPT.png",
            "http://imgur.com/j2SK2JH.png",
            "http://imgur.com/wFcmlES.png",
            "http://imgur.com/RJdU8Dj.png",
            "http://imgur.com/YC5doBb.png",
            "http://imgur.com/38v4GMV.png"
        };

        public static class Localisation
        {
            public const string BASE_PATH = EntityLocalisation.BASE_PATH + "EQUIPMENT_";

            public const string UNABLE_DOWNLOAD = BASE_PATH + nameof(UNABLE_DOWNLOAD);
            public const string MULTIPLE_MATCHES = BASE_PATH + nameof(MULTIPLE_MATCHES);
            public static LocalisedString GetName(EquipmentId equipmentName)
                => new LocalisedString(BASE_PATH + equipmentName.ToString().ToUpper());
            public static LocalisedString GetAbbreviation(EquipmentId equipmentName)
                => new LocalisedString(BASE_PATH + equipmentName.ToString().ToUpper() + "_ABBREV");

            private static IReadOnlyDictionary<EquipmentId, (string Name, string Abbreviation)> EquipmentNames { get; }
                = new Dictionary<EquipmentId, (string Name, string Abbreviation)>
                {
                    { EquipmentId.Aura_Bats, ("Bat Cave", "Aura_Bats") },
                    { EquipmentId.Aura_Bird, ("Birds", "Aura_Bird") },
                    { EquipmentId.Aura_BlackEnergy, ("Dark Energy", "Aura_BlackEnergy") },
                    { EquipmentId.Aura_Bones, ("Smells Fishy", "Aura_Bones") },
                    { EquipmentId.Aura_Bugs, ("Butterfly Effect", "Aura_Bugs") },
                    { EquipmentId.Aura_Cards, ("Magic Illusion", "Aura_Cards") },
                    { EquipmentId.Aura_Colors, ("Colour Pop", "Aura_Colors") },
                    { EquipmentId.Aura_Diamonds, ("Diamond Ring", "Aura_Diamonds") },
                    { EquipmentId.Aura_Embers, ("Falling Ember", "Aura_Embers") },
                    { EquipmentId.Aura_Fire, ("Flame Thrower", "Aura_Fire") },
                    { EquipmentId.Aura_GroundLightning, ("Lightning Charge", "Aura_GroundLightning") },
                    { EquipmentId.Aura_Ice, ("Frost Bite", "Aura_Ice") },
                    { EquipmentId.Aura_Leaves, ("Leaf Shield", "Aura_Leaves") },
                    { EquipmentId.Aura_Lightning, ("Magnetic Power", "Aura_Lightning") },
                    { EquipmentId.Aura_Orbs, ("Space Orbs", "Aura_Orbs") },
                    { EquipmentId.Aura_Star, ("Star Shine", "Aura_Star") },
                    { EquipmentId.Aura_Water, ("Whirlpool", "Aura_Water") },
                    { EquipmentId.Aura_Wind, ("Blue Wind", "Aura_Wind") },
                    { EquipmentId.Aura_Dizzy, ("Dizzy", "Aura_Dizzy") },
                    { EquipmentId.Aura_Sparks, ("Sparks", "Aura_Sparks") },
                    { EquipmentId.Hat_Alien, ("Alien Helmet", "Hat_Alien") },
                    { EquipmentId.Hat_Bag, ("Hat of Mystery", "Hat_Bag") },
                    { EquipmentId.Hat_BatGrey, ("Dark Knight", "Hat_BatGrey") },
                    { EquipmentId.Hat_BatRed, ("Crimson Knight", "Hat_BatRed") },
                    { EquipmentId.Hat_Cap, ("Everyday Cap", "Hat_Cap") },
                    { EquipmentId.Hat_Cat, ("Feline Helmet", "Hat_Cat") },
                    { EquipmentId.Hat_Chicken, ("Chicken Beanie", "Hat_Chicken") },
                    { EquipmentId.Hat_Football, ("Football Head", "Hat_Football") },
                    { EquipmentId.Hat_HelmDevil, ("Devil Helmet", "Hat_HelmDevil") },
                    { EquipmentId.Hat_HelmHorned, ("Horned Helmet", "Hat_HelmHorned") },
                    { EquipmentId.Hat_HelmTall, ("Magnificent Helmet", "Hat_HelmTall") },
                    { EquipmentId.Hat_HelmWhite, ("White Warrior Helmet", "Hat_HelmWhite") },
                    { EquipmentId.Hat_Ice, ("Ice Walker Helmet", "Hat_Ice") },
                    { EquipmentId.Hat_Indie, ("Fedora", "Hat_Indie") },
                    { EquipmentId.Hat_Ironman, ("Ironguy Helmet", "Hat_Ironman") },
                    { EquipmentId.Hat_KickAss, ("Castle Guard Mask", "Hat_KickAss") },
                    { EquipmentId.Hat_Kid, ("Whiz Kid's Cap", "Hat_Kid") },
                    { EquipmentId.Hat_Kitty, ("Softy Kitty Hood", "Hat_Kitty") },
                    { EquipmentId.Hat_Link, ("Phrygian Cap", "Hat_Link") },
                    { EquipmentId.Hat_Mega, ("Megaguy Helmet", "Hat_Mega") },
                    { EquipmentId.Hat_Music, ("Boom Box Head", "Hat_Music") },
                    { EquipmentId.Hat_Ninja, ("Ninja Mask", "Hat_Ninja") },
                    { EquipmentId.Hat_Pineapple, ("Pineapple Head", "Hat_Pineapple") },
                    { EquipmentId.Hat_Poo, ("Poop Head", "Hat_Poo") },
                    { EquipmentId.Hat_PowerRanger, ("Silver Mask", "Hat_PowerRanger") },
                    { EquipmentId.Hat_Robot, ("Cyclops Mask", "Hat_Robot") },
                    { EquipmentId.Hat_Soccer, ("Soccer Head", "Hat_Soccer") },
                    { EquipmentId.Hat_Top, ("Top Hat", "Hat_Top") },
                    { EquipmentId.Hat_Transformer, ("Optimus Helmet", "Hat_Transformer") },
                    { EquipmentId.Hat_Tribal, ("Tribal Helmet", "Hat_Tribal") },
                    { EquipmentId.Hat_Wizard, ("Wizard Hat", "Hat_Wizard") },
                    { EquipmentId.Hat_Beanie, ("Beanie", "Hat_Beanie") },
                    { EquipmentId.Hat_Detective, ("Detective Hat", "Hat_Detective") },
                    { EquipmentId.Hat_Ears, ("Ears", "Hat_Ears") },
                    { EquipmentId.Hat_Halo, ("Angel Halo", "Hat_Halo") },
                    { EquipmentId.Hat_HeadPhones, ("Headphones", "Hat_HeadPhones") },
                    { EquipmentId.Hat_MadHatter, ("Mad Hat", "Hat_MadHatter") },
                    { EquipmentId.Hat_RedTopHat, ("Red Hat", "Hat_RedTopHat") },
                    { EquipmentId.Hat_Ribbon, ("Ribbon", "Hat_Ribbon") },
                    { EquipmentId.Hat_Santa, ("Santa", "Hat_Santa") },
                    { EquipmentId.Hat_SmallTook, ("Small Toque", "Hat_SmallTook") },
                    { EquipmentId.Hat_Took, ("Toque", "Hat_Took") },
                    { EquipmentId.Slash_Darkness, ("Casting Shadows", "Slash_Darkness") },
                    { EquipmentId.Slash_Embers, ("Fiery Embers", "Slash_Embers") },
                    { EquipmentId.Slash_EmbersBlue, ("Frosted Embers", "Slash_EmbersBlue") },
                    { EquipmentId.Slash_Fire, ("Flames", "Slash_Fire") },
                    { EquipmentId.Slash_FireBlack, ("Crimson Flames", "Slash_FireBlack") },
                    { EquipmentId.Slash_FireGray, ("Gray Flames", "Slash_FireGray") },
                    { EquipmentId.Slash_FireOrange, ("Summoner Flames", "Slash_FireOrange") },
                    { EquipmentId.Slash_Lava, ("Hot Lava", "Slash_Lava") },
                    { EquipmentId.Slash_Leaves, ("Fallen Leaves", "Slash_Leaves") },
                    { EquipmentId.Slash_Lightning, ("Blue Lightning", "Slash_Lightning") },
                    { EquipmentId.Slash_Paint, ("Paint Streaks", "Slash_Paint") },
                    { EquipmentId.Slash_Petals, ("Sakura Petals", "Slash_Petals") },
                    { EquipmentId.Slash_Rainbow, ("Rainbow Road", "Slash_Rainbow") },
                    { EquipmentId.Slash_Smelly, ("Something Stinks", "Slash_Smelly") },
                    { EquipmentId.Slash_Stars, ("Starry Path", "Slash_Stars") },
                    { EquipmentId.Slash_StarsBlue, ("Glimmering Path", "Slash_StarsBlue") },
                    { EquipmentId.Slash_Water, ("Tidal Waves", "Slash_Water") },
                    { EquipmentId.Suit_ArmorFace, ("Unknown Face", "Suit_ArmorFace") },
                    { EquipmentId.Suit_ArmorGreen, ("Forest Fighter", "Suit_ArmorGreen") },
                    { EquipmentId.Suit_ArmorHighTec, ("Futuristic Gladiator", "Suit_ArmorHighTec") },
                    { EquipmentId.Suit_ArmorIce, ("Frosted Armor", "Suit_ArmorIce") },
                    { EquipmentId.Suit_ArmorOrange, ("Sunrise Armor", "Suit_ArmorOrange") },
                    { EquipmentId.Suit_ArmorPurple, ("Amethyst Armor", "Suit_ArmorPurple") },
                    { EquipmentId.Suit_ArmorRed, ("Crimson Warrior", "Suit_ArmorRed") },
                    { EquipmentId.Suit_ArmorRoman, ("Ancient Rome", "Suit_ArmorRoman") },
                    { EquipmentId.Suit_ArmorRoyal, ("Fit for Royals", "Suit_ArmorRoyal") },
                    { EquipmentId.Suit_ArmorScale, ("Scaly Suit", "Suit_ArmorScale") },
                    { EquipmentId.Suit_ArmorWhite, ("White Warrior", "Suit_ArmorWhite") },
                    { EquipmentId.Suit_Casual, ("Everyday Sweater", "Suit_Casual") },
                    { EquipmentId.Suit_Chicken, ("Chicken Breast", "Suit_Chicken") },
                    { EquipmentId.Suit_Farmer, ("Farmer Overalls", "Suit_Farmer") },
                    { EquipmentId.Suit_Ironman, ("Ironman Suit", "Suit_Ironman") },
                    { EquipmentId.Suit_KungFu, ("Kungfu Master", "Suit_KungFu") },
                    { EquipmentId.Suit_Ninja, ("Ninja Suit", "Suit_Ninja") },
                    { EquipmentId.Suit_Robot, ("Cyclops Body", "Suit_Robot") },
                    { EquipmentId.Suit_Snowman, ("Snowman", "Suit_Snowman") },
                    { EquipmentId.Suit_Wizard, ("Wizard Sweater", "Suit_Wizard") },
                    { EquipmentId.Weapon_Basic, ("Average Sword", "Weapon_Basic") },
                    { EquipmentId.Weapon_Bat, ("Dark Knight Sword", "Weapon_Bat") },
                    { EquipmentId.Weapon_BattleAxe, ("Axe of Ages", "Weapon_BattleAxe") },
                    { EquipmentId.Weapon_Beam, ("Lightsaber", "Weapon_Beam") },
                    { EquipmentId.Weapon_Brute, ("Crimson Sword", "Weapon_Brute") },
                    { EquipmentId.Weapon_Buster, ("Buster Blade", "Weapon_Buster") },
                    { EquipmentId.Weapon_Carrot, ("Carrot Stick", "Weapon_Carrot") },
                    { EquipmentId.Weapon_Cleaver, ("Conqueror's Cleaver", "Weapon_Cleaver") },
                    { EquipmentId.Weapon_Club, ("Club", "Weapon_Club") },
                    { EquipmentId.Weapon_Excalibur, ("Excalibur", "Weapon_Excalibur") },
                    { EquipmentId.Weapon_Fish, ("Fish Stick", "Weapon_Fish") },
                    { EquipmentId.Weapon_Gear, ("Geared Blade", "Weapon_Gear") },
                    { EquipmentId.Weapon_Glaive, ("Ancient Glaive", "Weapon_Glaive") },
                    { EquipmentId.Weapon_Halberd, ("Onyx Halberd", "Weapon_Halberd") },
                    { EquipmentId.Weapon_Hammer, ("Titansmasher", "Weapon_Hammer") },
                    { EquipmentId.Weapon_Holy, ("Knightfall Sword", "Weapon_Holy") },
                    { EquipmentId.Weapon_Katana, ("Reforged Katana", "Weapon_Katana") },
                    { EquipmentId.Weapon_Laser, ("Energy Sword", "Weapon_Laser") },
                    { EquipmentId.Weapon_Ninja, ("Ninja Dagger", "Weapon_Ninja") },
                    { EquipmentId.Weapon_Pencil, ("Blue Pencil", "Weapon_Pencil") },
                    { EquipmentId.Weapon_PitchFork, ("Pitchfork", "Weapon_PitchFork") },
                    { EquipmentId.Weapon_Poison, ("Poisonous Spellblade", "Weapon_Poison") },
                    { EquipmentId.Weapon_Saw, ("Sharpened Saw", "Weapon_Saw") },
                    { EquipmentId.Weapon_Skull, ("Skull-crusher Sword", "Weapon_Skull") },
                    { EquipmentId.Weapon_Spear, ("Swift Spear", "Weapon_Spear") },
                    { EquipmentId.Weapon_Staff, ("Twilight Staff", "Weapon_Staff") },
                    { EquipmentId.Weapon_Sythe, ("Death Sythe", "Weapon_Sythe") },
                    { EquipmentId.Weapon_WoodAxe, ("Lumberjack Axe", "Weapon_WoodAxe") },
                    { EquipmentId.Weapon_ZigZag, ("Crooked Blade", "Weapon_ZigZag") },
                    { EquipmentId.Aura_Valentines, ("Circle of Love", "Aura_Valentines") },
                    { EquipmentId.Hat_Valentines, ("Hat of Love", "Hat_Valentines") },
                    { EquipmentId.Weapon_Valentines, ("Heartbreaker", "Weapon_Valentines") },
                }.ToImmutableDictionary();

            public static IReadOnlyDictionary<string, string> Defaults { get; }
                = new Dictionary<string, string>
                {
                        { UNABLE_DOWNLOAD, "I could not download any equipment data. Please try again later." },
                        { MULTIPLE_MATCHES, "There were more than 1 equipment that matched `{2}`. Try being more specific, or use `{0}{1}` for a list of all equipment" },
                }.Concat(EquipmentNames.SelectMany(e => new Dictionary<string, string>
                {
                    { GetName(e.Key).Key, e.Value.Name },
                    { GetAbbreviation(e.Key).Key, e.Value.Abbreviation }
                })).ToImmutableDictionary();
        }
    }
}

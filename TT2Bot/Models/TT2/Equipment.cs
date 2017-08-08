using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Formatting;
using static TT2Bot.TT2Localisation;

namespace TT2Bot.Models
{
    class Equipment : GameEntity<string>
    {
        public static IReadOnlyDictionary<string, string> ImageUrls { get; }
            = new Dictionary<string, string>
            {
                { "AURA_BATS", Imgur("6JQGW3V") },
                { "AURA_BIRD", Imgur("iIC7MeB") },
                { "AURA_BLACKENERGY", Imgur("coFoXPO") },
                { "AURA_BONES", Imgur("Z7PytUd") },
                { "AURA_BUGS", Imgur("ntNAFxR") },
                { "AURA_CARDS", Imgur("tBFW0zU") },
                { "AURA_COLORS", Imgur("tpB3DKc") },
                { "AURA_DIAMONDS", Imgur("iilQvNH") },
                { "AURA_EMBERS", Imgur("kyKpotc") },
                { "AURA_FIRE", Imgur("xzhcDlg") },
                { "AURA_GROUNDLIGHTNING", Imgur("di6vXmL") },
                { "AURA_ICE", Imgur("bK5vNwx") },
                { "AURA_LEAVES", Imgur("oGEO2fa") },
                { "AURA_LIGHTNING", Imgur("jbFiSs8") },
                { "AURA_ORBS", Imgur("73V8eiy") },
                { "AURA_STAR", Imgur("FbFL1sJ") },
                { "AURA_WATER", Imgur("LhT8Ljj") },
                { "AURA_WIND", Imgur("J61AR1Q") },
                { "AURA_DIZZY", Imgur("WJG8MJW") },
                { "AURA_SPARKS", Imgur("YEN6i09") },
                { "HAT_ALIEN", Imgur("i8XIeqb") },
                { "HAT_BAG", Imgur("bbRCeSq") },
                { "HAT_BATGREY", Imgur("rjcCUwM") },
                { "HAT_BATRED", Imgur("PbvtRuE") },
                { "HAT_CAP", Imgur("S2shK39") },
                { "HAT_CAT", Imgur("HwKnbnI") },
                { "HAT_CHICKEN", Imgur("nf87Btz") },
                { "HAT_FOOTBALL", Imgur("Ynx0btx") },
                { "HAT_HELMDEVIL", Imgur("tNc4yOa") },
                { "HAT_HELMHORNED", Imgur("sfCcQ0H") },
                { "HAT_HELMTALL", Imgur("iVON03I") },
                { "HAT_HELMWHITE", Imgur("MwZHghp") },
                { "HAT_ICE", Imgur("KzpyO7H") },
                { "HAT_INDIE", Imgur("UFMgJnU") },
                { "HAT_IRONMAN", Imgur("VZ2ubL2") },
                { "HAT_KICKASS", Imgur("UeuRVEA") },
                { "HAT_KID", Imgur("RQ71eNa") },
                { "HAT_KITTY", Imgur("Re6PnpZ") },
                { "HAT_LINK", Imgur("hxb0ZbK") },
                { "HAT_MEGA", Imgur("3ajJaNm") },
                { "HAT_MUSIC", Imgur("7TKeUjt") },
                { "HAT_NINJA", Imgur("3JjGYku") },
                { "HAT_PINEAPPLE", Imgur("TTwmbqd") },
                { "HAT_POO", Imgur("2NdDUNZ") },
                { "HAT_POWERRANGER", Imgur("5kr3Icd") },
                { "HAT_ROBOT", Imgur("3OvMXoX") },
                { "HAT_SOCCER", Imgur("bgl0Fcu") },
                { "HAT_TOP", Imgur("G57jlHn") },
                { "HAT_TRANSFORMER", Imgur("H5r8wa9") },
                { "HAT_TRIBAL", Imgur("aVN3Tix") },
                { "HAT_WIZARD", Imgur("MmXcXJL") },
                { "HAT_BEANIE", Imgur("J0M2hGl") },
                { "HAT_DETECTIVE", Imgur("J0M2hGl") },
                { "HAT_EARS", Imgur("J0M2hGl") },
                { "HAT_HALO", Imgur("J0M2hGl") },
                { "HAT_HEADPHONES", Imgur("J0M2hGl") },
                { "HAT_MADHATTER", Imgur("J0M2hGl") },
                { "HAT_REDTOPHAT", Imgur("J0M2hGl") },
                { "HAT_RIBBON", Imgur("J0M2hGl") },
                { "HAT_SANTA", Imgur("J0M2hGl") },
                { "HAT_SMALLTOOK", Imgur("J0M2hGl") },
                { "HAT_TOOK", Imgur("J0M2hGl") },
                { "SLASH_DARKNESS", Imgur("myS7iUT") },
                { "SLASH_EMBERS", Imgur("nq8Aq26") },
                { "SLASH_EMBERSBLUE", Imgur("YvWfrqF") },
                { "SLASH_FIRE", Imgur("cATztqa") },
                { "SLASH_FIREBLACK", Imgur("hlbXF8Y") },
                { "SLASH_FIREGRAY", Imgur("SPvqjt5") },
                { "SLASH_FIREORANGE", Imgur("2y4R7m0") },
                { "SLASH_LAVA", Imgur("NI0nmjm") },
                { "SLASH_LEAVES", Imgur("YmfPd3H") },
                { "SLASH_LIGHTNING", Imgur("kWp2h7F") },
                { "SLASH_PAINT", Imgur("xgpUBxT") },
                { "SLASH_PETALS", Imgur("mOEIQam") },
                { "SLASH_RAINBOW", Imgur("mD91f1Y") },
                { "SLASH_SMELLY", Imgur("alOrEsp") },
                { "SLASH_STARS", Imgur("Cb4b5GV") },
                { "SLASH_STARSBLUE", Imgur("rwMyYIo") },
                { "SLASH_WATER", Imgur("VMVVmyk") },
                { "SUIT_ARMORFACE", Imgur("dSOIAVN") },
                { "SUIT_ARMORGREEN", Imgur("HCulNSv") },
                { "SUIT_ARMORHIGHTEC", Imgur("qttmnrm") },
                { "SUIT_ARMORICE", Imgur("GVk79M2") },
                { "SUIT_ARMORORANGE", Imgur("BxPamOw") },
                { "SUIT_ARMORPURPLE", Imgur("nRn6ZkG") },
                { "SUIT_ARMORRED", Imgur("3roDWVb") },
                { "SUIT_ARMORROMAN", Imgur("3IuMG7V") },
                { "SUIT_ARMORROYAL", Imgur("WNgeKy6") },
                { "SUIT_ARMORSCALE", Imgur("MD6BB8l") },
                { "SUIT_ARMORWHITE", Imgur("C8CPnxF") },
                { "SUIT_CASUAL", Imgur("X3JY6NF") },
                { "SUIT_CHICKEN", Imgur("H9ZJoZh") },
                { "SUIT_FARMER", Imgur("gg7dNn1") },
                { "SUIT_IRONMAN", Imgur("qn1WUvV") },
                { "SUIT_KUNGFU", Imgur("a4c0za8") },
                { "SUIT_NINJA", Imgur("mXucQ7T") },
                { "SUIT_ROBOT", Imgur("T621x6N") },
                { "SUIT_SNOWMAN", Imgur("4N8qygD") },
                { "SUIT_WIZARD", Imgur("0FFKVox") },
                { "WEAPON_BASIC", Imgur("cvghMNg") },
                { "WEAPON_BAT", Imgur("VZxcuFm") },
                { "WEAPON_BATTLEAXE", Imgur("J3axNpt") },
                { "WEAPON_BEAM", Imgur("ZTPQFcG") },
                { "WEAPON_BRUTE", Imgur("KFu0dV7") },
                { "WEAPON_BUSTER", Imgur("nbFpycv") },
                { "WEAPON_CARROT", Imgur("wVvI8Wo") },
                { "WEAPON_CLEAVER", Imgur("RuRKt2X") },
                { "WEAPON_CLUB", Imgur("PDleVcS") },
                { "WEAPON_EXCALIBUR", Imgur("CSLTTu5") },
                { "WEAPON_FISH", Imgur("vssvZuE") },
                { "WEAPON_GEAR", Imgur("ZZ7wDya") },
                { "WEAPON_GLAIVE", Imgur("LALxySs") },
                { "WEAPON_HALBERD", Imgur("kWCa4TW") },
                { "WEAPON_HAMMER", Imgur("LIvAVnl") },
                { "WEAPON_HOLY", Imgur("PNc1aGL") },
                { "WEAPON_KATANA", Imgur("ER0jD17") },
                { "WEAPON_LASER", Imgur("5ME9LEY") },
                { "WEAPON_NINJA", Imgur("xzdPZwM") },
                { "WEAPON_PENCIL", Imgur("M7iR3xz") },
                { "WEAPON_PITCHFORK", Imgur("A3nkxmX") },
                { "WEAPON_POISON", Imgur("kXSqN3U") },
                { "WEAPON_SAW", Imgur("JNYmyKi") },
                { "WEAPON_SKULL", Imgur("DD8ZgRD") },
                { "WEAPON_SPEAR", Imgur("pixzDKe") },
                { "WEAPON_STAFF", Imgur("QXG7Tt9") },
                { "WEAPON_SYTHE", Imgur("d6uxNMl") },
                { "WEAPON_WOODAXE", Imgur("VHLCk4w") },
                { "WEAPON_ZIGZAG", Imgur("Nj5pcMX") },
                { "AURA_VALENTINES", Imgur("7WjHT6V") },
                { "HAT_VALENTINES", Imgur("KNAkgcU") },
                { "WEAPON_VALENTINES", Imgur("LnE4Erc") }
            }.ToImmutableDictionary();

        public LocalisedString Abbreviation => Game.Equipment.GetAbbreviation(Id);
        public EquipmentClass Class { get; }
        public BonusType BonusType { get; }
        public EquipmentRarity Rarity { get; }
        public double BonusBase { get; }
        public double BonusIncrease { get; }
        public EquipmentSource Source { get; }
        public string ImageUrl => ImageUrls.TryGetValue(Id.ToUpper(), out var url) ? url : _defaultImages[0];
        public string FileVersion { get; }
        public Bitmap Image => _image.Value;
        public Lazy<Bitmap> _image { get; }

        internal Equipment(string id, 
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
            _image = new Lazy<Bitmap>(() => imageGetter?.Invoke(ImageUrl).Result);
        }

        public double BonusOnLevel(int level)
        {
            return BonusBase + BonusIncrease * level;
        }

        public override bool Matches(ITextResourceCollection textResource, string text)
        {
            var name = Name.Localise(textResource).ToLower();
            var abbrevs = Abbreviation.Localise(textResource).ToLower().Split(',');
            text = text.ToLower();
            var id = Id.ToLower();
            return id.StartsWith(text) || id.Contains($"_{text}") || base.Matches(textResource, text) || abbrevs.Any(a => a == text);
        }

        protected override LocalisedString GetName(string id)
            => Game.Equipment.GetName(id);

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
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using static TT2Bot.TT2Localisation;

namespace TT2Bot.Models
{
    class Equipment : GameEntity<string>
    {
        public static IReadOnlyDictionary<string, string> ImageUrls { get; }
        
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

        static Equipment()
        {
            ImageUrls = new Dictionary<string, string>
            {
                { "AURA_BATS", "http://i.imgur.com/6JQGW3V.png" },
                { "AURA_BIRD", "http://i.imgur.com/iIC7MeB.png" },
                { "AURA_BLACKENERGY", "http://i.imgur.com/coFoXPO.png" },
                { "AURA_BONES", "http://i.imgur.com/Z7PytUd.png" },
                { "AURA_BUGS", "http://i.imgur.com/ntNAFxR.png" },
                { "AURA_CARDS", "http://i.imgur.com/tBFW0zU.png" },
                { "AURA_COLORS", "http://i.imgur.com/tpB3DKc.png" },
                { "AURA_DIAMONDS", "http://i.imgur.com/iilQvNH.png" },
                { "AURA_EMBERS", "http://i.imgur.com/kyKpotc.png" },
                { "AURA_FIRE", "http://i.imgur.com/xzhcDlg.png" },
                { "AURA_GROUNDLIGHTNING", "http://i.imgur.com/di6vXmL.png" },
                { "AURA_ICE", "http://i.imgur.com/bK5vNwx.png" },
                { "AURA_LEAVES", "http://i.imgur.com/oGEO2fa.png" },
                { "AURA_LIGHTNING", "http://i.imgur.com/jbFiSs8.png" },
                { "AURA_ORBS", "http://i.imgur.com/73V8eiy.png" },
                { "AURA_STAR", "http://i.imgur.com/FbFL1sJ.png" },
                { "AURA_WATER", "http://i.imgur.com/LhT8Ljj.png" },
                { "AURA_WIND", "http://i.imgur.com/J61AR1Q.png" },
                { "AURA_DIZZY", "http://i.imgur.com/WJG8MJW.png" },
                { "AURA_SPARKS", "http://i.imgur.com/YEN6i09.png" },
                { "HAT_ALIEN", "http://i.imgur.com/i8XIeqb.png" },
                { "HAT_BAG", "http://i.imgur.com/bbRCeSq.png" },
                { "HAT_BATGREY", "http://i.imgur.com/rjcCUwM.png" },
                { "HAT_BATRED", "http://i.imgur.com/PbvtRuE.png" },
                { "HAT_CAP", "http://i.imgur.com/S2shK39.png" },
                { "HAT_CAT", "http://i.imgur.com/HwKnbnI.png" },
                { "HAT_CHICKEN", "http://i.imgur.com/nf87Btz.png" },
                { "HAT_FOOTBALL", "http://i.imgur.com/Ynx0btx.png" },
                { "HAT_HELMDEVIL", "http://i.imgur.com/tNc4yOa.png" },
                { "HAT_HELMHORNED", "http://i.imgur.com/sfCcQ0H.png" },
                { "HAT_HELMTALL", "http://i.imgur.com/iVON03I.png" },
                { "HAT_HELMWHITE", "http://i.imgur.com/MwZHghp.png" },
                { "HAT_ICE", "http://i.imgur.com/KzpyO7H.png" },
                { "HAT_INDIE", "http://i.imgur.com/UFMgJnU.png" },
                { "HAT_IRONMAN", "http://i.imgur.com/VZ2ubL2.png" },
                { "HAT_KICKASS", "http://i.imgur.com/UeuRVEA.png" },
                { "HAT_KID", "http://i.imgur.com/RQ71eNa.png" },
                { "HAT_KITTY", "http://i.imgur.com/Re6PnpZ.png" },
                { "HAT_LINK", "http://i.imgur.com/hxb0ZbK.png" },
                { "HAT_MEGA", "http://i.imgur.com/3ajJaNm.png" },
                { "HAT_MUSIC", "http://i.imgur.com/7TKeUjt.png" },
                { "HAT_NINJA", "http://i.imgur.com/3JjGYku.png" },
                { "HAT_PINEAPPLE", "http://i.imgur.com/TTwmbqd.png" },
                { "HAT_POO", "http://i.imgur.com/2NdDUNZ.png" },
                { "HAT_POWERRANGER", "http://i.imgur.com/5kr3Icd.png" },
                { "HAT_ROBOT", "http://i.imgur.com/3OvMXoX.png" },
                { "HAT_SOCCER", "http://i.imgur.com/bgl0Fcu.png" },
                { "HAT_TOP", "http://i.imgur.com/G57jlHn.png" },
                { "HAT_TRANSFORMER", "http://i.imgur.com/H5r8wa9.png" },
                { "HAT_TRIBAL", "http://i.imgur.com/aVN3Tix.png" },
                { "HAT_WIZARD", "http://i.imgur.com/MmXcXJL.png" },
                { "HAT_BEANIE", "http://i.imgur.com/J0M2hGl.png" },
                { "HAT_DETECTIVE", "http://i.imgur.com/J0M2hGl.png" },
                { "HAT_EARS", "http://i.imgur.com/J0M2hGl.png" },
                { "HAT_HALO", "http://i.imgur.com/J0M2hGl.png" },
                { "HAT_HEADPHONES", "http://i.imgur.com/J0M2hGl.png" },
                { "HAT_MADHATTER", "http://i.imgur.com/J0M2hGl.png" },
                { "HAT_REDTOPHAT", "http://i.imgur.com/J0M2hGl.png" },
                { "HAT_RIBBON", "http://i.imgur.com/J0M2hGl.png" },
                { "HAT_SANTA", "http://i.imgur.com/J0M2hGl.png" },
                { "HAT_SMALLTOOK", "http://i.imgur.com/J0M2hGl.png" },
                { "HAT_TOOK", "http://i.imgur.com/J0M2hGl.png" },
                { "SLASH_DARKNESS", "http://i.imgur.com/myS7iUT.png" },
                { "SLASH_EMBERS", "http://i.imgur.com/nq8Aq26.png" },
                { "SLASH_EMBERSBLUE", "http://i.imgur.com/YvWfrqF.png" },
                { "SLASH_FIRE", "http://i.imgur.com/cATztqa.png" },
                { "SLASH_FIREBLACK", "http://i.imgur.com/hlbXF8Y.png" },
                { "SLASH_FIREGRAY", "http://i.imgur.com/SPvqjt5.png" },
                { "SLASH_FIREORANGE", "http://i.imgur.com/2y4R7m0.png" },
                { "SLASH_LAVA", "http://i.imgur.com/NI0nmjm.png" },
                { "SLASH_LEAVES", "http://i.imgur.com/YmfPd3H.png" },
                { "SLASH_LIGHTNING", "http://i.imgur.com/kWp2h7F.png" },
                { "SLASH_PAINT", "http://i.imgur.com/xgpUBxT.png" },
                { "SLASH_PETALS", "http://i.imgur.com/mOEIQam.png" },
                { "SLASH_RAINBOW", "http://i.imgur.com/mD91f1Y.png" },
                { "SLASH_SMELLY", "http://i.imgur.com/alOrEsp.png" },
                { "SLASH_STARS", "http://i.imgur.com/Cb4b5GV.png" },
                { "SLASH_STARSBLUE", "http://i.imgur.com/rwMyYIo.png" },
                { "SLASH_WATER", "http://i.imgur.com/VMVVmyk.png" },
                { "SUIT_ARMORFACE", "http://i.imgur.com/dSOIAVN.png" },
                { "SUIT_ARMORGREEN", "http://i.imgur.com/HCulNSv.png" },
                { "SUIT_ARMORHIGHTEC", "http://i.imgur.com/qttmnrm.png" },
                { "SUIT_ARMORICE", "http://i.imgur.com/GVk79M2.png" },
                { "SUIT_ARMORORANGE", "http://i.imgur.com/BxPamOw.png" },
                { "SUIT_ARMORPURPLE", "http://i.imgur.com/nRn6ZkG.png" },
                { "SUIT_ARMORRED", "http://i.imgur.com/3roDWVb.png" },
                { "SUIT_ARMORROMAN", "http://i.imgur.com/3IuMG7V.png" },
                { "SUIT_ARMORROYAL", "http://i.imgur.com/WNgeKy6.png" },
                { "SUIT_ARMORSCALE", "http://i.imgur.com/MD6BB8l.png" },
                { "SUIT_ARMORWHITE", "http://i.imgur.com/C8CPnxF.png" },
                { "SUIT_CASUAL", "http://i.imgur.com/X3JY6NF.png" },
                { "SUIT_CHICKEN", "http://i.imgur.com/H9ZJoZh.png" },
                { "SUIT_FARMER", "http://i.imgur.com/gg7dNn1.png" },
                { "SUIT_IRONMAN", "http://i.imgur.com/qn1WUvV.png" },
                { "SUIT_KUNGFU", "http://i.imgur.com/a4c0za8.png" },
                { "SUIT_NINJA", "http://i.imgur.com/mXucQ7T.png" },
                { "SUIT_ROBOT", "http://i.imgur.com/T621x6N.png" },
                { "SUIT_SNOWMAN", "http://i.imgur.com/4N8qygD.png" },
                { "SUIT_WIZARD", "http://i.imgur.com/0FFKVox.png" },
                { "WEAPON_BASIC", "http://i.imgur.com/cvghMNg.png" },
                { "WEAPON_BAT", "http://i.imgur.com/VZxcuFm.png" },
                { "WEAPON_BATTLEAXE", "http://i.imgur.com/J3axNpt.png" },
                { "WEAPON_BEAM", "http://i.imgur.com/ZTPQFcG.png" },
                { "WEAPON_BRUTE", "http://i.imgur.com/KFu0dV7.png" },
                { "WEAPON_BUSTER", "http://i.imgur.com/nbFpycv.png" },
                { "WEAPON_CARROT", "http://i.imgur.com/wVvI8Wo.png" },
                { "WEAPON_CLEAVER", "http://i.imgur.com/RuRKt2X.png" },
                { "WEAPON_CLUB", "http://i.imgur.com/PDleVcS.png" },
                { "WEAPON_EXCALIBUR", "http://i.imgur.com/CSLTTu5.png" },
                { "WEAPON_FISH", "http://i.imgur.com/vssvZuE.png" },
                { "WEAPON_GEAR", "http://i.imgur.com/ZZ7wDya.png" },
                { "WEAPON_GLAIVE", "http://i.imgur.com/LALxySs.png" },
                { "WEAPON_HALBERD", "http://i.imgur.com/kWCa4TW.png" },
                { "WEAPON_HAMMER", "http://i.imgur.com/LIvAVnl.png" },
                { "WEAPON_HOLY", "http://i.imgur.com/PNc1aGL.png" },
                { "WEAPON_KATANA", "http://i.imgur.com/ER0jD17.png" },
                { "WEAPON_LASER", "http://i.imgur.com/5ME9LEY.png" },
                { "WEAPON_NINJA", "http://i.imgur.com/xzdPZwM.png" },
                { "WEAPON_PENCIL", "http://i.imgur.com/M7iR3xz.png" },
                { "WEAPON_PITCHFORK", "http://i.imgur.com/A3nkxmX.png" },
                { "WEAPON_POISON", "http://i.imgur.com/kXSqN3U.png" },
                { "WEAPON_SAW", "http://i.imgur.com/JNYmyKi.png" },
                { "WEAPON_SKULL", "http://i.imgur.com/DD8ZgRD.png" },
                { "WEAPON_SPEAR", "http://i.imgur.com/pixzDKe.png" },
                { "WEAPON_STAFF", "http://i.imgur.com/QXG7Tt9.png" },
                { "WEAPON_SYTHE", "http://i.imgur.com/d6uxNMl.png" },
                { "WEAPON_WOODAXE", "http://i.imgur.com/VHLCk4w.png" },
                { "WEAPON_ZIGZAG", "http://i.imgur.com/Nj5pcMX.png" },
                { "AURA_VALENTINES", "http://i.imgur.com/7WjHT6V.png" },
                { "HAT_VALENTINES", "http://i.imgur.com/KNAkgcU.png" },
                { "WEAPON_VALENTINES", "http://i.imgur.com/LnE4Erc.png" }
            }.ToImmutableDictionary();
        }

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

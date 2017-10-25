using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Formatting;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Enums;
using TT2Bot.GameEntity.Localisation;

namespace TT2Bot.GameEntity.Entities
{
    internal class Artifact : GameEntity<int>
    {
        public static IReadOnlyDictionary<ArtifactTier, ImmutableArray<int>> Tiers { get; }
            = new Dictionary<ArtifactTier, int[]>
            {
                { ArtifactTier.S, new [] { 22 } },
                { ArtifactTier.A, new [] { 1, 7, 9, 29, 32, 33, 34, 35, } },
                { ArtifactTier.B, new [] { 4, 6, 17, 20, 23, 25, 26, 28, 30, 31, 38, } },
                { ArtifactTier.C, new [] { 3, 8, 12, 14, 15, 21, 24, 36, 39, 41} },
                { ArtifactTier.D, new [] { 2, 5, 10, 11, 13, 16, 18, 19, 27, 37, } }
            }.ToImmutableDictionary(k => k.Key, v => v.Value.ToImmutableArray());

        public static IReadOnlyDictionary<int, string> ImageUrls { get; }
            = new Dictionary<int, string>
            {
                { 1,  Cockleshell("a4") },
                { 2,  Cockleshell("a38") },
                { 3,  Cockleshell("a22") },
                { 4,  Cockleshell("a20") },
                { 5,  Cockleshell("a24") },
                { 6,  Cockleshell("a34") },
                { 7,  Cockleshell("a2") },
                { 8,  Cockleshell("a33") },
                { 9,  Cockleshell("a3") },
                { 10, Cockleshell("a27") },
                { 11, Cockleshell("a36") },
                { 12, Cockleshell("a32") },
                { 13, Cockleshell("a30") },
                { 14, Cockleshell("a15") },
                { 15, Cockleshell("a14") },
                { 16, Cockleshell("a28") },
                { 17, Cockleshell("a18") },
                { 18, Cockleshell("a26") },
                { 19, Cockleshell("a25") },
                { 20, Cockleshell("a12") },
                { 21, Cockleshell("a13") },
                { 22, Cockleshell("a1") },
                { 23, Cockleshell("a19") },
                { 24, Cockleshell("a23") },
                { 25, Cockleshell("a17") },
                { 26, Cockleshell("a10") },
                { 27, Cockleshell("a31") },
                { 28, Cockleshell("a16") },
                { 29, Cockleshell("a37") },
                { 30, Imgur("ZGfXaNb") },
                { 31, Cockleshell("a11") },
                { 32, Cockleshell("a6") },
                { 33, Cockleshell("a8") },
                { 34, Cockleshell("a7") },
                { 35, Cockleshell("a9") },
                { 36, Cockleshell("a35") },
                { 37, Cockleshell("a29") },
                { 38, Cockleshell("a5") },
                { 39, Cockleshell("a21") },
                { 41, Imgur("VOO6luq") }
            }.ToImmutableDictionary();

        public override LocalisedString Name => Localisation.GetName(Id);
        public override LocalisedString Abbreviations => Localisation.GetAbbreviation(Id);
        //public string TT1 { get; }
        public BonusType BonusType { get; }
        public double EffectPerLevel { get; }
        public double EffectCoef { get; }
        public double DamageBonus { get; }
        public double CostCoef { get; }
        public double CostExpo { get; }
        public string Note { get; }
        public ArtifactTier Tier => Tiers.FirstOrDefault(t => t.Value.Contains(Id)).Key;
        public override string ImageUrl => ImageUrls.TryGetValue(Id, out var url) ? url : null;

        internal Artifact(int id,
                          int? maxLevel,
                          //string tt1,
                          BonusType bonusType,
                          double effectPerLevel,
                          double effectCoef,
                          double damageBonus,
                          double costCoef,
                          double costExpo,
                          string note,
                          string fileVersion,
                          Func<string, ValueTask<Bitmap>> imageGetter = null)
        {
            Id = id;
            MaxLevel = maxLevel;
            //TT1 = tt1;
            BonusType = bonusType;
            EffectPerLevel = effectPerLevel;
            EffectCoef = effectCoef;
            DamageBonus = damageBonus;
            CostCoef = costCoef;
            CostExpo = costExpo;
            Note = note;
            FileVersion = fileVersion;
            ImageGetter = imageGetter;
        }

        public double EffectAt(int level)
            => EffectPerLevel * level;

        public double DamageAt(int level)
            => DamageBonus * level;

        public long CostOfLevel(int level)
            => (long)Math.Ceiling(CostCoef * Math.Pow(level, CostExpo));

        public long CostToLevel(int finish)
            => CostToLevel(0, finish);

        public long CostToLevel(int start, int finish)
        {
            long total = 0;
            for (int i = start; i <= finish; i++)
            {
                total += CostOfLevel(i);
            }
            return total;
        }

        public int BudgetArtifact(double relics, int current)
        {
            var cost = CostOfLevel(current);
            if (cost > relics)
                return current - 1;
            return BudgetArtifact(relics - cost, current + 1);
        }

        public static class Localisation
        {
            public const string BASE_PATH = EntityLocalisation.BASE_PATH + "ARTIFACT_";

            public const string UNABLE_DOWNLOAD = BASE_PATH + nameof(UNABLE_DOWNLOAD);
            public const string MULTIPLE_MATCHES = BASE_PATH + nameof(MULTIPLE_MATCHES);
            public static LocalisedString GetName(int artifactId)
                => new LocalisedString(BASE_PATH + artifactId.ToString());
            public static LocalisedString GetAbbreviation(int artifactId)
                => new LocalisedString(BASE_PATH + "ABBREV_" + artifactId.ToString());

            public static IReadOnlyDictionary<string, string> Defaults { get; }
                = new Dictionary<string, string>
                {
                        { UNABLE_DOWNLOAD, "I could not download any artifact data. Please try again later." },
                        { MULTIPLE_MATCHES, "There were more than 1 artifacts that matched `{2}`. Try being more specific, or use `{0}{1}` for a list of all artifacts" },
                        { GetName(1).Key, "Heroic Shield" },              { GetAbbreviation(1).Key, "HSh" },
                        { GetName(2).Key, "Stone of the Valrunes " },     { GetAbbreviation(2).Key, "SotV,SoV,SV" },
                        { GetName(3).Key, "The Arcana Cloak " },          { GetAbbreviation(3).Key, "tAC,AC" },
                        { GetName(4).Key, "Axe of Muerte " },             { GetAbbreviation(4).Key, "AoM,AM" },
                        { GetName(5).Key, "Invader's Shield " },          { GetAbbreviation(5).Key, "IS" },
                        { GetName(6).Key, "Elixir of Eden " },            { GetAbbreviation(6).Key, "EoE,EE" },
                        { GetName(7).Key, "Parchment of Foresight " },    { GetAbbreviation(7).Key, "PoF,PF" },
                        { GetName(8).Key, "Hunter's Ointment " },         { GetAbbreviation(8).Key, "HO" },
                        { GetName(9).Key, "Laborer's Pendant" },          { GetAbbreviation(9).Key, "LP" },
                        { GetName(10).Key, "Bringer of Ragnarok " },      { GetAbbreviation(10).Key, "BoR,BR" },
                        { GetName(11).Key, "Titan's Mask" },              { GetAbbreviation(11).Key, "TM" },
                        { GetName(12).Key, "Swamp Gauntlet" },            { GetAbbreviation(12).Key, "SG" },
                        { GetName(13).Key, "Forbidden Scroll" },          { GetAbbreviation(13).Key, "FS" },
                        { GetName(14).Key, "Aegis " },                    { GetAbbreviation(14).Key, "AG,A" },
                        { GetName(15).Key, "Ring of Fealty" },            { GetAbbreviation(15).Key, "RoF,RF" },
                        { GetName(16).Key, "Glacial Axe" },               { GetAbbreviation(16).Key, "GA" },
                        { GetName(17).Key, "Helmet of Madness" },         { GetAbbreviation(17).Key, "HoM,HM" },
                        { GetName(18).Key, "Egg of Fortune " },           { GetAbbreviation(18).Key, "EoF,EF" },
                        { GetName(19).Key, "Chest of Contentment" },      { GetAbbreviation(19).Key, "CoC,CC" },
                        { GetName(20).Key, "Book of Prophecy " },         { GetAbbreviation(20).Key, "BoP,BP" },
                        { GetName(21).Key, "Divine Chalice" },            { GetAbbreviation(21).Key, "DC" },
                        { GetName(22).Key, "Book of Shadows " },          { GetAbbreviation(22).Key, "BoS,BS" },
                        { GetName(23).Key, "Titanium Plating" },          { GetAbbreviation(23).Key, "TP" },
                        { GetName(24).Key, "Staff of Radiance" },         { GetAbbreviation(24).Key, "SoR,SR" },
                        { GetName(25).Key, "Blade of Damocles " },        { GetAbbreviation(25).Key, "BoD,BD" },
                        { GetName(26).Key, "Heavenly Sword" },            { GetAbbreviation(26).Key, "HS" },
                        { GetName(27).Key, "Glove of Kuma " },            { GetAbbreviation(27).Key, "GoK,GK" },
                        { GetName(28).Key, "Amethyst Staff " },           { GetAbbreviation(28).Key, "AS" },
                        { GetName(29).Key, "Drunken Hammer" },            { GetAbbreviation(29).Key, "DH" },
                        { GetName(30).Key, "Influential Elixir" },        { GetAbbreviation(30).Key, "IE" },
                        { GetName(31).Key, "Divine Retribution" },        { GetAbbreviation(31).Key, "DR" },
                        { GetName(32).Key, "The Sword of Storms " },      { GetAbbreviation(32).Key, "TSS" },
                        { GetName(33).Key, "Furies Bow" },                { GetAbbreviation(33).Key, "FB" },
                        { GetName(34).Key, "Charm of the Ancient " },     { GetAbbreviation(34).Key, "CoA,CA" },
                        { GetName(35).Key, "Hero's Blade" },              { GetAbbreviation(35).Key, "HB" },
                        { GetName(36).Key, "Infinity Pendulum  " },       { GetAbbreviation(36).Key, "IP" },
                        { GetName(37).Key, "Oak Staff" },                 { GetAbbreviation(37).Key, "OS" },
                        { GetName(38).Key, "Fruit of Eden " },            { GetAbbreviation(38).Key, "FoE,FE" },
                        { GetName(39).Key, "Titan Spear " },              { GetAbbreviation(39).Key, "TS" },
                        { GetName(40).Key, "Ring of Calisto" },           { GetAbbreviation(40).Key, "RoC,RC" },
                        { GetName(41).Key, "Royal Toxin" },               { GetAbbreviation(41).Key, "RT" },
                        { GetName(42).Key, "Avian Feather" },             { GetAbbreviation(42).Key, "AF" },
                        { GetName(43).Key, "Zakinthos Coin" },            { GetAbbreviation(43).Key, "ZC" },
                        { GetName(44).Key, "Great Fay Medallion" },       { GetAbbreviation(44).Key, "GFM,FM" },
                        { GetName(45).Key, "Coins of Ebizu" },            { GetAbbreviation(45).Key, "CoE,CE" },
                        { GetName(46).Key, "Heart of Storms" },           { GetAbbreviation(46).Key, "HoS,HS" },
                        { GetName(47).Key, "Invader's Gjalarhorn" },      { GetAbbreviation(47).Key, "IG" },
                        { GetName(48).Key, "Phantom Timepiece" },         { GetAbbreviation(48).Key, "PT" },
                        { GetName(49).Key, "The Master's Sword" },        { GetAbbreviation(49).Key, "tMS,MS" },
                        { GetName(50).Key, "Ambrosia Elixir" },           { GetAbbreviation(50).Key, "AE" },
                        { GetName(51).Key, "Samosek Sword" },             { GetAbbreviation(51).Key, "SS" },
                }.ToImmutableDictionary();
        }
    }
}
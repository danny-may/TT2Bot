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
                { 1,  "iepmzFj" }, { 2,  "P7uOP5w" }, { 3,  "RZhGq6R" }, { 4,  "nQJNccr" }, { 5,  "W7irzH1" }, { 6,  "K4hLRun" },
                { 7,  "VwA8GZX" }, { 8,  "tJ4wB13" }, { 9,  "QnM8m9M" }, { 10, "oQ5HqtM" }, { 11, "pUGBP51" }, { 12, "cYmClnM" },
                { 13, "sihO05n" }, { 14, "hCZw37C" }, { 15, "vj6FZNr" }, { 16, "JnjNnVZ" }, { 17, "sUUuGwt" }, { 18, "j6gaxyy" },
                { 19, "ChZZhS6" }, { 20, "INpQ4cy" }, { 21, "84MiaJD" }, { 22, "hcjMeVU" }, { 23, "zxlTQyM" }, { 24, "coJ7mAt" },
                { 25, "dBDQPx3" }, { 26, "s4iJomt" }, { 27, "u9NNG0x" }, { 28, "RND5Q8k" }, { 29, "1G9I7ua" }, { 30, "QU4fGRL" },
                { 31, "XiyMxlm" }, { 32, "ugdLrb2" }, { 33, "AFjew0l" }, { 34, "BBnhI4U" }, { 35, "m0foaMQ" }, { 36, "apCLPlL" },
                { 37, "JEB9d1f" }, { 38, "NKjdgtt" }, { 39, "abpuZIr" }, { 40, "mKw6L0E" }, { 41, "Lj42MM9" }, { 42, "HqF5Cut" },
                { 43, "NCPbNV6" }, { 44, "Ih4VVf4" }, { 45, "ODamXKS" }, { 46, "XLCgvqe" }, { 47, "4aqLJgZ" }, { 48, "ydrx4Al" },
                { 49, "LGZAR8E" }, { 50, "rnGeVwj" }, { 51, "BAJwsuW" }
            }.ToImmutableDictionary();

        public override LocalisedString Name => Localisation.GetName(Id);
        public override LocalisedString Abbreviations => Localisation.GetAbbreviation(Id);
        public BonusType BonusType { get; }
        public double EffectPerLevel { get; }
        public double EffectGrowthMax { get; }
        public double EffectGrowthRate { get; }
        public double EffectGrowthExpo { get; }
        public double DamageBonus { get; }
        public double CostCoef { get; }
        public double CostExpo { get; }
        public string Note { get; }
        public ArtifactTier Tier => Tiers.FirstOrDefault(t => t.Value.Contains(Id)).Key;
        public override string ImageUrl => Imgur(ImageUrls.TryGetValue(Id, out var url) ? url : "jguBSeh");

        internal Artifact(int id,
                          int? maxLevel,
                          BonusType bonusType,
                          double effectPerLevel,
                          double growthMax,
                          double growthRate,
                          double growthExpo,
                          double damageBonus,
                          double costCoef,
                          double costExpo,
                          string note,
                          string fileVersion,
                          Func<string, ValueTask<Bitmap>> imageGetter = null)
        {
            Id = id;
            MaxLevel = maxLevel;
            BonusType = bonusType;
            EffectPerLevel = effectPerLevel;
            EffectGrowthMax = growthMax;
            EffectGrowthRate = growthRate;
            EffectGrowthExpo = growthExpo;
            DamageBonus = damageBonus;
            CostCoef = costCoef;
            CostExpo = costExpo;
            Note = note;
            FileVersion = fileVersion;
            ImageGetter = imageGetter;
        }

        public double EffectAt(int level)
            => EffectPerLevel * Math.Pow(level, Math.Pow(1 + (CostExpo - 1) * Math.Min(EffectGrowthRate * level, EffectGrowthMax), EffectGrowthExpo));

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
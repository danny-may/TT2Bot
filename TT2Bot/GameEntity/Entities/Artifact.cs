using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TitanBot.Formatting;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Enums;
using TT2Bot.GameEntity.Localisation;

namespace TT2Bot.GameEntity.Entities
{
    internal class Artifact : GameEntity<int>
    {
        public static string TierSourceUrl { get; } = "https://www.reddit.com/r/TapTitans2/comments/732nk1/artifact_tier_list_for_patch_20_will_update_with/";

        public static IReadOnlyDictionary<ArtifactTier, ImmutableArray<int>> Tiers { get; }
            = GetTiersFromReddit(TierSourceUrl + ".json");

        private static IReadOnlyDictionary<ArtifactTier, ImmutableArray<int>> GetTiersFromReddit(string address)
        {
            var rawData = new WebClient().DownloadString(address);
            var data = JArray.Parse(rawData).SelectToken("[0].data.children[0].data.selftext").Value<string>();

            var lines = data.Split('\n');

            var textDict = new Dictionary<char, List<string>>();
            var current = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.StartsWith("##"))
                {
                    current = new List<string>();
                    textDict.Add(line[2], current);
                }
                else if (line.StartsWith("* **"))
                    current.Add(line.Split('(')
                                    .Skip(1)
                                    .First()
                                    .Split(')')
                                    .First());
            }

            return textDict.ToDictionary(
                x => Enum.TryParse<ArtifactTier>(x.Key.ToString(), out var tier)
                        ? tier
                        : ArtifactTier.None,
                x => x.Value.Select(v => int.TryParse(v, out var id) ? id : -1)
                            .Where(i => i != -1)
                            .ToImmutableArray()

                ).Where(e => e.Key != ArtifactTier.None && e.Value.Count() > 0)
                 .ToImmutableDictionary();
        }

        //Forgive me, this is a quick and VERY DIRTY way to do the image urls
        //I wont do it like this in the rewrite
        public static IReadOnlyDictionary<int, string> ImageUrls { get; }
            = DownloadImgurAlbum("I537X",
                                 "596770a32e5c664",
                                 d => d.ToDictionary(e => int.Parse(e["description"].ToString()
                                                                                    .Split(' ')[0]),
                                                     e => e["id"].ToString()))
                    .ToImmutableDictionary();

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
        public override string ImageUrl => Imgur(ImageUrls.TryGetValue(Id, out var url) ? url : ImageUrls.First().Value);

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
                        { GetName(1).Key, "Heroic Shield" },                  { GetAbbreviation(1).Key, "HSh" },
                        { GetName(2).Key, "Stone of the Valrunes " },         { GetAbbreviation(2).Key, "SotV,SoV,SV" },
                        { GetName(3).Key, "The Arcana Cloak " },              { GetAbbreviation(3).Key, "tAC,AC" },
                        { GetName(4).Key, "Axe of Muerte " },                 { GetAbbreviation(4).Key, "AoM,AM" },
                        { GetName(5).Key, "Invader's Shield " },              { GetAbbreviation(5).Key, "IS" },
                        { GetName(6).Key, "Elixir of Eden " },                { GetAbbreviation(6).Key, "EoE,EE" },
                        { GetName(7).Key, "Parchment of Foresight " },        { GetAbbreviation(7).Key, "PoF,PF" },
                        { GetName(8).Key, "Hunter's Ointment " },             { GetAbbreviation(8).Key, "HO" },
                        { GetName(9).Key, "Laborer's Pendant" },              { GetAbbreviation(9).Key, "LP" },
                        { GetName(10).Key, "Bringer of Ragnarok " },          { GetAbbreviation(10).Key, "BoR,BR" },
                        { GetName(11).Key, "Titan's Mask" },                  { GetAbbreviation(11).Key, "TM" },
                        { GetName(12).Key, "Swamp Gauntlet" },                { GetAbbreviation(12).Key, "SG" },
                        { GetName(13).Key, "Forbidden Scroll" },              { GetAbbreviation(13).Key, "FS" },
                        { GetName(14).Key, "Aegis " },                        { GetAbbreviation(14).Key, "Ag" },
                        { GetName(15).Key, "Ring of Fealty" },                { GetAbbreviation(15).Key, "RoF,RF" },
                        { GetName(16).Key, "Glacial Axe" },                   { GetAbbreviation(16).Key, "GA" },
                        { GetName(17).Key, "Helmet of Madness" },             { GetAbbreviation(17).Key, "HoM,HM" },
                        { GetName(18).Key, "Egg of Fortune " },               { GetAbbreviation(18).Key, "EoF,EF" },
                        { GetName(19).Key, "Chest of Contentment" },          { GetAbbreviation(19).Key, "CoC,CC" },
                        { GetName(20).Key, "Book of Prophecy " },             { GetAbbreviation(20).Key, "BoP,BP" },
                        { GetName(21).Key, "Divine Chalice" },                { GetAbbreviation(21).Key, "DC" },
                        { GetName(22).Key, "Book of Shadows " },              { GetAbbreviation(22).Key, "BoS,BS" },
                        { GetName(23).Key, "Titanium Plating" },              { GetAbbreviation(23).Key, "TP" },
                        { GetName(24).Key, "Staff of Radiance" },             { GetAbbreviation(24).Key, "SoR,SR" },
                        { GetName(25).Key, "Blade of Damocles " },            { GetAbbreviation(25).Key, "BoD,BD" },
                        { GetName(26).Key, "Heavenly Sword" },                { GetAbbreviation(26).Key, "HSw" },
                        { GetName(27).Key, "Glove of Kuma " },                { GetAbbreviation(27).Key, "GoK" },
                        { GetName(28).Key, "Amethyst Staff " },               { GetAbbreviation(28).Key, "AS" },
                        { GetName(29).Key, "Drunken Hammer" },                { GetAbbreviation(29).Key, "DH" },
                        { GetName(30).Key, "Influential Elixir" },            { GetAbbreviation(30).Key, "IE" },
                        { GetName(31).Key, "Divine Retribution" },            { GetAbbreviation(31).Key, "DR" },
                        { GetName(32).Key, "The Sword of Storms " },          { GetAbbreviation(32).Key, "tSoS,tSS,SoS,SSt" },
                        { GetName(33).Key, "Furies Bow" },                    { GetAbbreviation(33).Key, "FB" },
                        { GetName(34).Key, "Charm of the Ancient " },         { GetAbbreviation(34).Key, "CotA,CoA,CA" },
                        { GetName(35).Key, "Hero's Blade" },                  { GetAbbreviation(35).Key, "HB" },
                        { GetName(36).Key, "Infinity Pendulum  " },           { GetAbbreviation(36).Key, "IP" },
                        { GetName(37).Key, "Oak Staff" },                     { GetAbbreviation(37).Key, "OS" },
                        { GetName(38).Key, "Fruit of Eden " },                { GetAbbreviation(38).Key, "FoE,FE" },
                        { GetName(39).Key, "Titan Spear " },                  { GetAbbreviation(39).Key, "TS" },
                        { GetName(40).Key, "Ring of Calisto" },               { GetAbbreviation(40).Key, "RoC,RC" },
                        { GetName(41).Key, "Royal Toxin" },                   { GetAbbreviation(41).Key, "RT" },
                        { GetName(42).Key, "Avian Feather" },                 { GetAbbreviation(42).Key, "AF" },
                        { GetName(43).Key, "Zakynthos Coin" },                { GetAbbreviation(43).Key, "ZC" },
                        { GetName(44).Key, "Great Fay Medallion" },           { GetAbbreviation(44).Key, "GFM" },
                        { GetName(45).Key, "Coins of Ebizu" },                { GetAbbreviation(45).Key, "CoE,CE" },
                        { GetName(46).Key, "Corrupted Rune Heart" },          { GetAbbreviation(46).Key, "CRH" },
                        { GetName(47).Key, "Invader's Gjallarhorn" },         { GetAbbreviation(47).Key, "IG" },
                        { GetName(48).Key, "Phantom Timepiece" },             { GetAbbreviation(48).Key, "PT" },
                        { GetName(49).Key, "The Master's Sword" },            { GetAbbreviation(49).Key, "tMS,MS" },
                        { GetName(50).Key, "Ambrosia Elixir" },               { GetAbbreviation(50).Key, "AE" },
                        { GetName(51).Key, "Samosek Sword" },                 { GetAbbreviation(51).Key, "SSw" },
                        { GetName(52).Key, "Heart of Storms" },               { GetAbbreviation(52).Key, "HoS,HSt" },
                        { GetName(53).Key, "Apollo Orb" },                    { GetAbbreviation(53).Key, "AO" },
                        { GetName(54).Key, "Essence of the Kitsune" },        { GetAbbreviation(54).Key, "EotK,EoK,EK" },
                        { GetName(55).Key, "Durendal Sword" },                { GetAbbreviation(55).Key, "DS" },
                        { GetName(56).Key, "Helheim Skull" },                 { GetAbbreviation(56).Key, "HSk" },
                        { GetName(57).Key, "Aram Spear" },                    { GetAbbreviation(57).Key, "AS" },
                        { GetName(58).Key, "Mystic Staff" },                  { GetAbbreviation(58).Key, "MS" },
                        { GetName(59).Key, "The Retaliator" },                { GetAbbreviation(59).Key, "tRe,Re" },
                        { GetName(60).Key, "Ward of the Darkness" },          { GetAbbreviation(60).Key, "WotD,WoD,WD" }
                }.ToImmutableDictionary();
        }
    }
}
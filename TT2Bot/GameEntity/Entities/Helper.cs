using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Formatting;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Localisation;
using TT2Bot.Helpers;
using TT2Bot.Models;

namespace TT2Bot.GameEntity.Entities
{
    class Helper : GameEntity<int>
    {
        private static float HelperInefficency = 0.023f;
        private static int HelperInefficencySlowdown = 34;
        private static float DmgScaleDown = 0.1f;

        public int Order { get; }
        public override LocalisedString Name => Localisation.GetName(Id);
        public LocalisedString ShortName => Localisation.GetShortName(Id);
        public HelperType HelperType { get; }
        public double BaseCost { get; }
        public double BaseDamage { get; }
        public double Efficency { get; }
        public IReadOnlyList<HelperSkill> Skills { get; }
        public bool IsInGame { get; }

        public Helper(int id,
                      int order,
                      HelperType type,
                      double baseCost,
                      List<HelperSkill> skills,
                      bool isInGame,
                      string version,
                      Func<string, ValueTask<Bitmap>> imageGetter = null)
        {
            Id = id;
            Order = order;
            HelperType = type;
            BaseCost = baseCost;
            Skills = skills.AsReadOnly();
            IsInGame = isInGame;
            FileVersion = version;
            ImageGetter = imageGetter;


            Efficency = Math.Pow(1f - HelperInefficency * Math.Min(Order, HelperInefficencySlowdown), Math.Min(Order, HelperInefficencySlowdown));
            BaseDamage = Efficency * DmgScaleDown * BaseCost;
        }

        static Helper()
        {
            ImageUrls = new Dictionary<int, string>
            {
                { 1,  Imgur("hvV0TIq") },
                { 2,  Imgur("vfwJHOr") },
                { 3,  Imgur("7H48V1S") },
                { 4,  Imgur("UPmWQQX") },
                { 5,  Imgur("UfQY9nV") },
                { 6,  Imgur("tU90lyc") },
                { 7,  Imgur("ueBKUm5") },
                { 8,  Imgur("clGI0Zw") },
                { 9,  Imgur("NiclzRy") },
                { 10, Imgur("8FStJtD") },
                { 11, Imgur("6wOUBi5") },
                { 12, Imgur("2d0FhBb") },
                { 13, Imgur("6yBu2Fb") },
                { 14, Imgur("QFwBnPD") },
                { 15, Imgur("iilQW67") },
                { 16, Imgur("GjOlXrF") },
                { 17, Imgur("x9NElV5") },
                { 18, Imgur("0qwsv5C") },
                { 19, Imgur("kOIuZBb") },
                { 20, Imgur("RWW5wSM") },
                { 21, Imgur("RXdMuTX") },
                { 22, Imgur("D9B5esA") },
                { 23, Imgur("MQa5xNz") },
                { 24, Imgur("ySyXFD2") },
                { 25, Imgur("n01QYYq") },
                { 26, Imgur("MukpKwc") },
                { 27, Imgur("JlgOAcU") },
                { 28, Imgur("P3cGrSJ") },
                { 29, Imgur("3GoOMYY") },
                { 30, Imgur("joFoONK") },
                { 31, Imgur("2gdViDG") },
                { 32, Imgur("rOkhWRa") },
                { 33, Imgur("DuKlEya") },
                { 34, Imgur("KZ8OwFN") },
                { 35, Imgur("EcCtJ9T") },
                { 36, Imgur("OMSXN8J") },
                { 37, Imgur("NGotVYA") },
            }.ToImmutableDictionary();
        }

        public double GetDps(int level)
            => BaseDamage * level;

        private static int[] _evolveLevels = new int[] { 1000, 2000, 3000 };

        public double GetCost(int level)
        {
            int evomult = 1;
            if (_evolveLevels.Contains(level))
                evomult = 1000;

            return Math.Ceiling(Math.Round(BaseCost * Math.Pow(1.082, level), 6)) * evomult;
        }

        public double GetCost(int level, int count)
        {
            return Enumerable.Range(level, count).Sum(l => GetCost(l));
        }

        public override bool Matches(ITextResourceCollection textResource, string text)
            => base.Matches(textResource, text.Without(",", " "));

        public static class Localisation
        {
            public const string BASE_PATH = EntityLocalisation.BASE_PATH + "HELPER_";

            public const string UNABLE_DOWNLOAD = BASE_PATH + nameof(UNABLE_DOWNLOAD);
            public const string MULTIPLE_MATCHES = BASE_PATH + nameof(MULTIPLE_MATCHES);
            public static LocalisedString GetName(int helperId)
                => new LocalisedString(BASE_PATH + helperId.ToString());
            public static LocalisedString GetShortName(int helperId)
                => new LocalisedString(BASE_PATH + helperId.ToString() + "_SHORT");

            public static IReadOnlyDictionary<string, string> Defaults { get; }
                = new Dictionary<string, string>
                {
                        { UNABLE_DOWNLOAD, "I could not download any hero data. Please try again later." },
                        { MULTIPLE_MATCHES, "There were more than 1 heroes that matched `{0}`" },
                        { GetName(1).Key, "Zato the Blind Staff Master" },    { GetShortName(1).Key, "Zato" },
                        { GetName(2).Key, "Lance, Knight of Cobalt Steel" },  { GetShortName(2).Key, "Lance" },
                        { GetName(3).Key, "Maddie, Shadow Thief" },           { GetShortName(3).Key, "Maddie" },
                        { GetName(4).Key, "Kronus, Bringer of Judgement" },   { GetShortName(4).Key, "Kronus" },
                        { GetName(5).Key, "Saje the Garden Keeper" },         { GetShortName(5).Key, "Saje" },
                        { GetName(6).Key, "Pingo of the Tori" },              { GetShortName(6).Key, "Pingo" },
                        { GetName(7).Key, "Aya the Lightning Violet" },       { GetShortName(7).Key, "Aya" },
                        { GetName(8).Key, "Gulbrand the Destroyer" },         { GetShortName(8).Key, "Gulbrand" },
                        { GetName(9).Key, "Rhys Mage of Order Evetga" },      { GetShortName(9).Key, "Rhys" },
                        { GetName(10).Key, "Kiki the Dragon Rider" },         { GetShortName(10).Key, "Kiki" },
                        { GetName(11).Key, "Lala Quickshot" },                { GetShortName(11).Key, "Lala" },
                        { GetName(12).Key, "Boomoh Doctor" },                 { GetShortName(12).Key, "Boomoh" },
                        { GetName(13).Key, "Wally Wat the Magician" },        { GetShortName(13).Key, "Wally" },
                        { GetName(14).Key, "Nohni the Spearit" },             { GetShortName(14).Key, "Nohni" },
                        { GetName(15).Key, "Kin the Puffy Beast" },           { GetShortName(15).Key, "Kin" },
                        { GetName(16).Key, "Zolom Blaster,  Space Hunter" },  { GetShortName(16).Key, "Zolom" },
                        { GetName(17).Key, "Princess Titania of Fay" },       { GetShortName(17).Key, "Titania" },
                        { GetName(18).Key, "Maya Muerta the Watcher" },       { GetShortName(18).Key, "Maya" },
                        { GetName(19).Key, "Jayce the Ruthless Cutter" },     { GetShortName(19).Key, "Jayce" },
                        { GetName(20).Key, "Cosette, Jewel of House Sabre" }, { GetShortName(20).Key, "Cosette" },
                        { GetName(21).Key, "Sophia, Champion of Swords" },    { GetShortName(21).Key, "Sophia" },
                        { GetName(22).Key, "Lil' Ursa" },                     { GetShortName(22).Key, "Ursa" },
                        { GetName(23).Key, "Dex-1000" },                      { GetShortName(23).Key, "Dex-1000" },
                        { GetName(24).Key, "Rosabella Bonnie Archer" },       { GetShortName(24).Key, "Rosabella" },
                        { GetName(25).Key, "Beany Sprout the 1st" },          { GetShortName(25).Key, "Beany" },
                        { GetName(26).Key, "Captain Davey Cannon" },          { GetShortName(26).Key, "Davey" },
                        { GetName(27).Key, "Sawyer the Wild Gunslinger" },    { GetShortName(27).Key, "Sawyer" },
                        { GetName(28).Key, "Miki the Graceful Dancer" },      { GetShortName(28).Key, "Miki" },
                        { GetName(29).Key, "The Great Pharaoh" },             { GetShortName(29).Key, "Pharoh" },
                        { GetName(30).Key, "The Great Madame Cass" },         { GetShortName(30).Key, "Cass" },
                        { GetName(31).Key, "Jazz Rockerfellow" },             { GetShortName(31).Key, "Jazz" },
                        { GetName(32).Key, "Lady Lucy the Night Caster" },    { GetShortName(32).Key, "Lucy" },
                        { GetName(33).Key, "Finn the Funny Guard" },          { GetShortName(33).Key, "Finn" },
                        { GetName(34).Key, "Maple the Autumn Guardian" },     { GetShortName(34).Key, "Maple" },
                        { GetName(35).Key, "Yzafa the Fearsome Bandit" },     { GetShortName(35).Key, "Yzafa" },
                        { GetName(36).Key, "Damon of the Darkness" },         { GetShortName(36).Key, "Damon" },
                        { GetName(37).Key, "Mina the Priestess of Light" },   { GetShortName(37).Key, "Mina" }
                }.ToImmutableDictionary();
        }
    }
}

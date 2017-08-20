using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Formatting;
using TT2Bot.Helpers;
using static TT2Bot.TT2Localisation;

namespace TT2Bot.Models.TT2
{
    class Helper : GameEntity<int>
    {
        private static float HelperInefficency = 0.023f;
        private static int HelperInefficencySlowdown = 34;
        private static float DmgScaleDown = 0.1f;

        public int Order { get; }
        public LocalisedString ShortName => Game.Helper.GetShortName(Id);
        public HelperType HelperType { get; }
        public double BaseCost { get; }
        public double BaseDamage { get; }
        public double Efficency { get; }
        public IReadOnlyList<HelperSkill> Skills { get; }
        public bool IsInGame { get; }
        public string ImageUrl => ImageUrls.TryGetValue(Id, out var url) ? url : null;
        public Bitmap Image => _image.Value;
        private Lazy<Bitmap> _image { get; }

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
            _image = new Lazy<Bitmap>(() => imageGetter?.Invoke(ImageUrl).Result);


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

        protected override LocalisedString GetName(int id)
            => Game.Helper.GetName(id);
    }
}

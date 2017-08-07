using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Formatting;
using TT2Bot.Helpers;
using static TT2Bot.TT2Localisation;

namespace TT2Bot.Models
{
    class Helper : GameEntity<int>
    {
        private static IReadOnlyDictionary<int, string> ImageUrls { get; }
        
        public int Order { get; }
        public LocalisedString ShortName => Game.Helper.GetShortName(Id);
        public HelperType HelperType { get; }
        public double BaseCost { get; }
        public IReadOnlyList<HelperSkill> Skills { get; }
        public bool IsInGame { get; }
        public string FileVersion { get; }
        public string ImageUrl => ImageUrls.TryGetValue(Id, out var url) ? url : null;
        public Bitmap Image => _image.Value;
        private Lazy<Bitmap> _image { get; }

        static Helper()
        {
            ImageUrls = Enumerable.Range(1, 37).ToImmutableDictionary(k => k, v => (string)null);
        }

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
        }

        public double GetDps(int level)
        {
            var efficiency = Math.Pow(1f - 0.023f * Math.Min(Order, 34), Math.Min(Order, 34));


            return BaseCost * 0.1f * efficiency * level * 1.0;
        }

        private static int[] _evolveLevels = new int[] { 1000, 2000, 3000 };

        public double GetCost(int level)
        {
            int evomult = 1;
            if (_evolveLevels.Contains(level))
                evomult = 1000;

            return Math.Ceiling(Math.Round(BaseCost * Math.Pow(1.082, (double)level), 6)) * evomult;
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

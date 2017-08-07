using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Formatting;
using TT2Bot.Models.General;
using static TT2Bot.TT2Localisation;

namespace TT2Bot.Models
{
    class Pet : GameEntity<int>
    {
        public static IReadOnlyDictionary<int, string> ImageUrls { get; }
        
        public double DamageBase { get; }
        public Dictionary<int, double> IncreaseRanges { get; }
        public BonusType BonusType { get; }
        public double BonusBase { get; }
        public double BonusIncrement { get; }
        public string FileVersion { get; }
        public string ImageUrl => ImageUrls.TryGetValue(Id, out var url) ? url : null;
        public Bitmap Image => _image.Value;
        public Lazy<Bitmap> _image { get; }

        static Pet()
        {
            ImageUrls = new Dictionary<int, string>
            {
                { 1,  "http://www.cockleshell.org/static/TT2/img/p9.png" },
                { 2,  "http://www.cockleshell.org/static/TT2/img/p7.png" },
                { 3,  "http://www.cockleshell.org/static/TT2/img/p2.png" },
                { 4,  "http://www.cockleshell.org/static/TT2/img/p5.png" },
                { 5,  "http://www.cockleshell.org/static/TT2/img/p1.png" },
                { 6,  "http://www.cockleshell.org/static/TT2/img/p12.png" },
                { 7,  "http://www.cockleshell.org/static/TT2/img/p8.png" },
                { 8,  "http://www.cockleshell.org/static/TT2/img/p6.png" },
                { 9,  "http://www.cockleshell.org/static/TT2/img/p3.png" },
                { 10,  "http://www.cockleshell.org/static/TT2/img/p4.png" },
                { 11,  "http://www.cockleshell.org/static/TT2/img/p0.png" },
                { 12,  "http://www.cockleshell.org/static/TT2/img/p13.png" },
                { 13,  "http://www.cockleshell.org/static/TT2/img/p10.png" },
                { 14,  "http://www.cockleshell.org/static/TT2/img/p11.png" },
                { 15,  "http://www.cockleshell.org/static/TT2/img/p15.png" },
                { 16,  "http://www.cockleshell.org/static/TT2/img/p14.png" }
            }.ToImmutableDictionary();
        }

        public Pet(int id, 
                    double damageBase, 
                    Dictionary<int, double> increaseRanges, 
                    BonusType bonusType, 
                    double bonusBase, 
                    double bonusIncrement, 
                    string version,
                    Func<string, ValueTask<Bitmap>> imageGetter = null)
        {
            Id = id;
            DamageBase = damageBase;
            IncreaseRanges = increaseRanges;
            BonusType = bonusType;
            BonusBase = bonusBase;
            BonusIncrement = bonusIncrement;
            FileVersion = FileVersion;
            _image = new Lazy<Bitmap>(() => imageGetter?.Invoke(ImageUrl).Result);
        }

        public double DamageOnLevel(int level)
        {
            return Enumerable.Range(1, level)
                             .Select(l => IncreaseRanges[IncreaseRanges.Keys.LastOrDefault(k => k <= l)])
                             .Sum() + DamageBase;
        }

        public double BonusOnLevel(int level)
        {
            return BonusBase + level * BonusIncrement;
        }

        public Percentage InactiveMultiplier(int level)
            => Math.Min(1d, (double)(level / 5) / 20);

        protected override LocalisedString GetName(int id)
            => Game.Pet.GetName(id);
    }
}

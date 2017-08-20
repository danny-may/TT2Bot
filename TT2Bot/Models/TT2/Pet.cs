using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Formatting;
using TT2Bot.Models.General;
using static TT2Bot.TT2Localisation;

namespace TT2Bot.Models.TT2
{
    class Pet : GameEntity<int>
    {
        public double DamageBase { get; }
        public Dictionary<int, double> IncreaseRanges { get; }
        public BonusType BonusType { get; }
        public double BonusBase { get; }
        public double BonusIncrement { get; }
        public string ImageUrl => ImageUrls.TryGetValue(Id, out var url) ? url : null;
        public Bitmap Image => _image.Value;
        private Lazy<Bitmap> _image { get; }

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

        static Pet()
        {
            ImageUrls = new Dictionary<int, string>
            {
                { 1,  Cockleshell("p9") },
                { 2,  Cockleshell("p7") },
                { 3,  Cockleshell("p2") },
                { 4,  Cockleshell("p5") },
                { 5,  Cockleshell("p1") },
                { 6,  Cockleshell("p12") },
                { 7,  Cockleshell("p8") },
                { 8,  Cockleshell("p6") },
                { 9,  Cockleshell("p3") },
                { 10,  Cockleshell("p4") },
                { 11,  Cockleshell("p0") },
                { 12,  Cockleshell("p13") },
                { 13,  Cockleshell("p10") },
                { 14,  Cockleshell("p11") },
                { 15,  Cockleshell("p15") },
                { 16,  Cockleshell("p14") }
            }.ToImmutableDictionary();
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

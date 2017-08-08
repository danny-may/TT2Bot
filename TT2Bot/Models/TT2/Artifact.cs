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
    class Artifact : GameEntity<int>
    {
        public static IReadOnlyDictionary<int, string> ImageUrls { get; }
            = new Dictionary<int, string>
            {
                { 1, Cockleshell("a4") },
                { 2, Cockleshell("a38") },
                { 3, Cockleshell("a22") },
                { 4, Cockleshell("a20") },
                { 5, Cockleshell("a24") },
                { 6, Cockleshell("a34") },
                { 7, Cockleshell("a2") },
                { 8, Cockleshell("a33") },
                { 9, Cockleshell("a3") },
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
                { 31, Cockleshell("a11") },
                { 32, Cockleshell("a6") },
                { 33, Cockleshell("a8") },
                { 34, Cockleshell("a7") },
                { 35, Cockleshell("a9") },
                { 36, Cockleshell("a35") },
                { 37, Cockleshell("a29") },
                { 38, Cockleshell("a5") },
                { 39, Cockleshell("a21") }
            }.ToImmutableDictionary();
        public static IReadOnlyDictionary<int, ArtifactTier> Tiers { get; }
            = new Dictionary<int, ArtifactTier>
            {
                { 1, ArtifactTier.B },
                { 2, ArtifactTier.D },
                { 3, ArtifactTier.A },
                { 4, ArtifactTier.B },
                { 5, ArtifactTier.D },
                { 6, ArtifactTier.E },
                { 7, ArtifactTier.A },
                { 8, ArtifactTier.E },
                { 9, ArtifactTier.B },
                { 10, ArtifactTier.E },
                { 11, ArtifactTier.E },
                { 12, ArtifactTier.E },
                { 13, ArtifactTier.D },
                { 14, ArtifactTier.A },
                { 15, ArtifactTier.B },
                { 16, ArtifactTier.E },
                { 17, ArtifactTier.B },
                { 18, ArtifactTier.D },
                { 19, ArtifactTier.D },
                { 20, ArtifactTier.C },
                { 21, ArtifactTier.C },
                { 22, ArtifactTier.S },
                { 23, ArtifactTier.C },
                { 24, ArtifactTier.C },
                { 25, ArtifactTier.B },
                { 26, ArtifactTier.B },
                { 27, ArtifactTier.D },
                { 28, ArtifactTier.B },
                { 29, ArtifactTier.E },
                { 31, ArtifactTier.B },
                { 32, ArtifactTier.A },
                { 33, ArtifactTier.A },
                { 34, ArtifactTier.A },
                { 35, ArtifactTier.A },
                { 36, ArtifactTier.E },
                { 37, ArtifactTier.E },
                { 38, ArtifactTier.A },
                { 39, ArtifactTier.B }
            }.ToImmutableDictionary();

        public int? MaxLevel { get; }
        public string TT1 { get; }
        public BonusType BonusType { get; }
        public double EffectPerLevel { get; }
        public double DamageBonus { get; }
        public double CostCoef { get; }
        public double CostExpo { get; }
        public string Note { get; }
        public LocalisedString Abbreviation => Game.Artifact.GetAbbreviation(Id);
        public string FileVersion { get; }
        public ArtifactTier Tier => Tiers.TryGetValue(Id, out var tier) ? tier : ArtifactTier.None;
        public string ImageUrl => ImageUrls.TryGetValue(Id, out var url) ? url : null;
        public Bitmap Image => _image.Value;
        public Lazy<Bitmap> _image { get; }
        
        internal Artifact(int id,
                          int? maxLevel,
                          string tt1,
                          BonusType bonusType,
                          double effectPerLevel,
                          double damageBonus,
                          double costCoef,
                          double costExpo,
                          string note,
                          string fileVersion,
                          Func<string, ValueTask<Bitmap>> imageGetter = null)
        {
            Id = id;
            MaxLevel = maxLevel;
            TT1 = tt1;
            BonusType = bonusType;
            EffectPerLevel = effectPerLevel;
            DamageBonus = damageBonus;
            CostCoef = costCoef;
            CostExpo = costExpo;
            Note = note;
            FileVersion = fileVersion;
            _image = new Lazy<Bitmap>(() => imageGetter?.Invoke(ImageUrl).Result);
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

        public override bool Matches(ITextResourceCollection textResource, string text)
        {
            var abbrevs = Abbreviation.Localise(textResource).ToLower().Split(',');
            return base.Matches(textResource, text) || abbrevs.Any(a => a == text.Without(" ").ToLower());
        }

        protected override LocalisedString GetName(int id)
            => Game.Artifact.GetName(id);
    }
}

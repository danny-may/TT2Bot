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
        public static IReadOnlyDictionary<int, ArtifactTier> Tiers { get; }

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

        static Artifact()
        {
            var imgurls = ImmutableDictionary.CreateBuilder<int, string>();
            var tiers = ImmutableDictionary.CreateBuilder<int, ArtifactTier>();
            (imgurls[1], tiers[1]) = ("http://www.cockleshell.org/static/TT2/img/a4.png", ArtifactTier.B);
            (imgurls[2], tiers[2]) = ("http://www.cockleshell.org/static/TT2/img/a38.png", ArtifactTier.D);
            (imgurls[3], tiers[3]) = ("http://www.cockleshell.org/static/TT2/img/a22.png", ArtifactTier.A);
            (imgurls[4], tiers[4]) = ("http://www.cockleshell.org/static/TT2/img/a20.png", ArtifactTier.B);
            (imgurls[5], tiers[5]) = ("http://www.cockleshell.org/static/TT2/img/a24.png", ArtifactTier.D);
            (imgurls[6], tiers[6]) = ("http://www.cockleshell.org/static/TT2/img/a34.png", ArtifactTier.E);
            (imgurls[7], tiers[7]) = ("http://www.cockleshell.org/static/TT2/img/a2.png", ArtifactTier.A);
            (imgurls[8], tiers[8]) = ("http://www.cockleshell.org/static/TT2/img/a33.png", ArtifactTier.E);
            (imgurls[9], tiers[9]) = ("http://www.cockleshell.org/static/TT2/img/a3.png", ArtifactTier.B);
            (imgurls[10], tiers[10]) = ("http://www.cockleshell.org/static/TT2/img/a27.png", ArtifactTier.E);
            (imgurls[11], tiers[11]) = ("http://www.cockleshell.org/static/TT2/img/a36.png", ArtifactTier.E);
            (imgurls[12], tiers[12]) = ("http://www.cockleshell.org/static/TT2/img/a32.png", ArtifactTier.E);
            (imgurls[13], tiers[13]) = ("http://www.cockleshell.org/static/TT2/img/a30.png", ArtifactTier.D);
            (imgurls[14], tiers[14]) = ("http://www.cockleshell.org/static/TT2/img/a15.png", ArtifactTier.A);
            (imgurls[15], tiers[15]) = ("http://www.cockleshell.org/static/TT2/img/a14.png", ArtifactTier.B);
            (imgurls[16], tiers[16]) = ("http://www.cockleshell.org/static/TT2/img/a28.png", ArtifactTier.E);
            (imgurls[17], tiers[17]) = ("http://www.cockleshell.org/static/TT2/img/a18.png", ArtifactTier.B);
            (imgurls[18], tiers[18]) = ("http://www.cockleshell.org/static/TT2/img/a26.png", ArtifactTier.D);
            (imgurls[19], tiers[19]) = ("http://www.cockleshell.org/static/TT2/img/a25.png", ArtifactTier.D);
            (imgurls[20], tiers[20]) = ("http://www.cockleshell.org/static/TT2/img/a12.png", ArtifactTier.C);
            (imgurls[21], tiers[21]) = ("http://www.cockleshell.org/static/TT2/img/a13.png", ArtifactTier.C);
            (imgurls[22], tiers[22]) = ("http://www.cockleshell.org/static/TT2/img/a1.png", ArtifactTier.S);
            (imgurls[23], tiers[23]) = ("http://www.cockleshell.org/static/TT2/img/a19.png", ArtifactTier.C);
            (imgurls[24], tiers[24]) = ("http://www.cockleshell.org/static/TT2/img/a23.png", ArtifactTier.C);
            (imgurls[25], tiers[25]) = ("http://www.cockleshell.org/static/TT2/img/a17.png", ArtifactTier.B);
            (imgurls[26], tiers[26]) = ("http://www.cockleshell.org/static/TT2/img/a10.png", ArtifactTier.B);
            (imgurls[27], tiers[27]) = ("http://www.cockleshell.org/static/TT2/img/a31.png", ArtifactTier.D);
            (imgurls[28], tiers[28]) = ("http://www.cockleshell.org/static/TT2/img/a16.png", ArtifactTier.B);
            (imgurls[29], tiers[29]) = ("http://www.cockleshell.org/static/TT2/img/a37.png", ArtifactTier.E);
            (imgurls[31], tiers[31]) = ("http://www.cockleshell.org/static/TT2/img/a11.png", ArtifactTier.B);
            (imgurls[32], tiers[32]) = ("http://www.cockleshell.org/static/TT2/img/a6.png", ArtifactTier.A);
            (imgurls[33], tiers[33]) = ("http://www.cockleshell.org/static/TT2/img/a8.png", ArtifactTier.A);
            (imgurls[34], tiers[34]) = ("http://www.cockleshell.org/static/TT2/img/a7.png", ArtifactTier.A);
            (imgurls[35], tiers[35]) = ("http://www.cockleshell.org/static/TT2/img/a9.png", ArtifactTier.A);
            (imgurls[36], tiers[36]) = ("http://www.cockleshell.org/static/TT2/img/a35.png", ArtifactTier.E);
            (imgurls[37], tiers[37]) = ("http://www.cockleshell.org/static/TT2/img/a29.png", ArtifactTier.E);
            (imgurls[38], tiers[38]) = ("http://www.cockleshell.org/static/TT2/img/a5.png", ArtifactTier.A);
            (imgurls[39], tiers[39]) = ("http://www.cockleshell.org/static/TT2/img/a21.png", ArtifactTier.B);
            ImageUrls = imgurls.ToImmutable();
            Tiers = tiers.ToImmutable();
        }

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

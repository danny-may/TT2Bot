using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Formatting;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Localisation;
using TT2Bot.Models;
using TT2Bot.Models.General;

namespace TT2Bot.GameEntity.Entities
{
    class Pet : GameEntity<int>
    {
        public override LocalisedString Name => Localisation.GetName(Id);
        public double DamageBase { get; }
        public Dictionary<int, double> IncreaseRanges { get; }
        public BonusType BonusType { get; }
        public double BonusBase { get; }
        public double BonusIncrement { get; }

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
            ImageGetter = imageGetter;
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

        public static class Localisation
        {
            public const string BASE_PATH = EntityLocalisation.BASE_PATH + "PET_";

            public const string TOSTRING = BASE_PATH + nameof(TOSTRING);
            public const string UNABLE_DOWNLOAD = BASE_PATH + nameof(UNABLE_DOWNLOAD);
            public const string MULTIPLE_MATCHES = BASE_PATH + nameof(MULTIPLE_MATCHES);
            public static LocalisedString GetName(int artifactId)
                => new LocalisedString(BASE_PATH + artifactId.ToString());

            public static IReadOnlyDictionary<string, string> Defaults { get; }
                = new Dictionary<string, string>
                {
                        { TOSTRING, "{0} ({1})" },
                        { UNABLE_DOWNLOAD, "I could not download any pet data. Please try again later." },
                        { MULTIPLE_MATCHES, "There were more than 1 pets that matched `{0}`" },
                        { GetName(1).Key, "Nova" },
                        { GetName(2).Key, "Toto" },
                        { GetName(3).Key, "Cerberus" },
                        { GetName(4).Key, "Mousy" },
                        { GetName(5).Key, "Harker" },
                        { GetName(6).Key, "Bubbles" },
                        { GetName(7).Key, "Demos" },
                        { GetName(8).Key, "Tempest" },
                        { GetName(9).Key, "Basky" },
                        { GetName(10).Key, "Scraps" },
                        { GetName(11).Key,  "Zero" },
                        { GetName(12).Key,  "Polly" },
                        { GetName(13).Key,  "Hamy" },
                        { GetName(14).Key,  "Phobos" },
                        { GetName(15).Key,  "Fluffers" },
                        { GetName(16).Key,  "Kit" },

                }.ToImmutableDictionary();
        }
    }
}

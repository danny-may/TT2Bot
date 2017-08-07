using System.Collections.Generic;
using System.Collections.Immutable;
using TitanBot.Formatting;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Game
        {
            public static class Artifact
            {
                private const string BASE_PATH = Game.BASE_PATH + "ARTIFACT_";
                
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
                        { MULTIPLE_MATCHES, "There were more than 1 artifacts that matched `{0}`" },
                        { GetName(1).Key,  "Heroic Shield"},          { GetAbbreviation(1).Key,  "HSH" },
                        { GetName(2).Key,  "Stone of the Valrunes"},  { GetAbbreviation(2).Key,  "SOV,SV" },
                        { GetName(3).Key,  "The Arcana Cloak"},       { GetAbbreviation(3).Key,  "TAC,AC" },
                        { GetName(4).Key,  "Axe of Muerte"},          { GetAbbreviation(4).Key,  "AOM,AM" },
                        { GetName(5).Key,  "Invader's Shield"},       { GetAbbreviation(5).Key,  "IS" },
                        { GetName(6).Key,  "Elixir of Eden"},         { GetAbbreviation(6).Key,  "EOE,EE" },
                        { GetName(7).Key,  "Parchment of Foresight"}, { GetAbbreviation(7).Key,  "POF,PF" },
                        { GetName(8).Key,  "Hunter's Ointment"},      { GetAbbreviation(8).Key,  "HO" },
                        { GetName(9).Key,  "Laborer's Pendant"},      { GetAbbreviation(9).Key,  "LP" },
                        { GetName(10).Key, "Bringer of Ragnarok"},    { GetAbbreviation(10).Key, "BOR,BR" },
                        { GetName(11).Key, "Titan's Mask"},           { GetAbbreviation(11).Key, "TM" },
                        { GetName(12).Key, "Swamp Gauntlet"},         { GetAbbreviation(12).Key, "SG" },
                        { GetName(13).Key, "Forbidden Scroll"},       { GetAbbreviation(13).Key, "FS" },
                        { GetName(14).Key, "Aegis"},                  { GetAbbreviation(14).Key, "AG" },
                        { GetName(15).Key, "Ring of Fealty"},         { GetAbbreviation(15).Key, "ROF,RF" },
                        { GetName(16).Key, "Glacial Axe"},            { GetAbbreviation(16).Key, "GA" },
                        { GetName(17).Key, "Hero's Blade"},           { GetAbbreviation(17).Key, "HB" },
                        { GetName(18).Key, "Egg of Fortune"},         { GetAbbreviation(18).Key, "EOF,EF" },
                        { GetName(19).Key, "Chest of Contentment"},   { GetAbbreviation(19).Key, "COC,CC" },
                        { GetName(20).Key, "Book of Prophecy"},       { GetAbbreviation(20).Key, "BOP,BP" },
                        { GetName(21).Key, "Divine Chalice"},         { GetAbbreviation(21).Key, "DC" },
                        { GetName(22).Key, "Book of Shadows"},        { GetAbbreviation(22).Key, "BOS,BS" },
                        { GetName(23).Key, "Helmet of Madness"},      { GetAbbreviation(23).Key, "HOM,HM" },
                        { GetName(24).Key, "Staff of Radiance"},      { GetAbbreviation(24).Key, "SOR,SR" },
                        { GetName(25).Key, "Lethe Water"},            { GetAbbreviation(25).Key, "LW" },
                        { GetName(26).Key, "Heavenly Sword"},         { GetAbbreviation(26).Key, "HSW" },
                        { GetName(27).Key, "Glove of Kuma"},          { GetAbbreviation(27).Key, "GOK,GK" },
                        { GetName(28).Key, "Amethyst Staff"},         { GetAbbreviation(28).Key, "AS" },
                        { GetName(29).Key, "Drunken Hammer"},         { GetAbbreviation(29).Key, "DH" },
                        { GetName(31).Key, "Divine Retribution"},     { GetAbbreviation(31).Key, "DR" },
                        { GetName(32).Key, "Fruit of Eden"},          { GetAbbreviation(32).Key, "FOE,FE" },
                        { GetName(33).Key, "The Sword of Storms"},    { GetAbbreviation(33).Key, "TSOS,TSS,SS" },
                        { GetName(34).Key, "Charm of the Ancient"},   { GetAbbreviation(34).Key, "COA,CA" },
                        { GetName(35).Key, "Blade of Damocles"},      { GetAbbreviation(35).Key, "BOD,BD" },
                        { GetName(36).Key, "Infinity Pendulum"},      { GetAbbreviation(36).Key, "IP" },
                        { GetName(37).Key, "Oak Staff"},              { GetAbbreviation(37).Key, "OS" },
                        { GetName(38).Key, "Furies Bow"},             { GetAbbreviation(38).Key, "FB" },
                        { GetName(39).Key, "Titan Spear"},            { GetAbbreviation(39).Key, "TS" },
                    }.ToImmutableDictionary();
            }
        }
    }
}

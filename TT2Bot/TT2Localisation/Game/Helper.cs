using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanBot.Commands;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Game
        {
            public static class Helper
            {
                private const string BASE_PATH = Game.BASE_PATH + "HELPER_";
                
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
                        { GetName(17).Key, "Princess Titania of Fay" },       { GetShortName(17).Key, "Princess" },
                        { GetName(18).Key, "Maya Muerta the Watcher" },       { GetShortName(18).Key, "Maya" },
                        { GetName(19).Key, "Jayce the Ruthless Cutter" },     { GetShortName(19).Key, "Jayce" },
                        { GetName(20).Key, "Cosette, Jewel of House Sabre" }, { GetShortName(20).Key, "Cosette" },
                        { GetName(21).Key, "Sophia, Champion of Swords" },    { GetShortName(21).Key, "Sophia" },
                        { GetName(22).Key, "Lil' Ursa" },                     { GetShortName(22).Key, "Ursa" },
                        { GetName(23).Key, "Dex-1000" },                      { GetShortName(23).Key, "Dex-1000" },
                        { GetName(24).Key, "Rosabella Bonnie Archer" },       { GetShortName(24).Key, "Rosabella" },
                        { GetName(25).Key, "Beany Sprout the 1st" },          { GetShortName(25).Key, "Beany" },
                        { GetName(26).Key, "Captain Davey Cannon" },          { GetShortName(26).Key, "Captain" },
                        { GetName(27).Key, "Sawyer the Wild Gunslinger" },    { GetShortName(27).Key, "Sawyer" },
                        { GetName(28).Key, "Miki the Graceful Dancer" },      { GetShortName(28).Key, "Miki" },
                        { GetName(29).Key, "The Great Pharaoh" },             { GetShortName(29).Key, "The" },
                        { GetName(30).Key, "The Great Madame Cass" },         { GetShortName(30).Key, "The" },
                        { GetName(31).Key, "Jazz Rockerfellow" },             { GetShortName(31).Key, "Jazz" },
                        { GetName(32).Key, "Lady Lucy the Night Caster" },    { GetShortName(32).Key, "Lady" },
                        { GetName(33).Key, "Finn the Funny Guard" },          { GetShortName(33).Key, "Finn" },
                        { GetName(34).Key, "Maple the Autumn Guardian" },     { GetShortName(34).Key, "Maple" },
                        { GetName(35).Key, "Yzafa the Fearsome Bandit" },     { GetShortName(35).Key, "Yzafa" },
                        { GetName(36).Key, "Damon of the Darkness" },         { GetShortName(36).Key, "Damon" },
                        { GetName(37).Key, "Mina the Priestess of Light" },   { GetShortName(37).Key, "Mina" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

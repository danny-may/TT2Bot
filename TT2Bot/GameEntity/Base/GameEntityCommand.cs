using TitanBot.Commands;
using TitanBot.Formatting;
using TT2Bot.Services;
using static TT2Bot.TT2Localisation.CommandText;

namespace TT2Bot.GameEntity.Base
{
    abstract class GameEntityCommand : Command
    {
        protected TT2DataService DataService { get; }
        protected override LocalisedString DelayMessage => (LocalisedString)DELAYMESSAGE_DATA;

        public GameEntityCommand(TT2DataService dataService)
        {
            DataService = dataService;
        }
    }
}

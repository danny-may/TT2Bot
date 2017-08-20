using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting;
using TT2Bot.Services;
using static TT2Bot.TT2Localisation.Commands;

namespace TT2Bot.Commands.Data
{
    class SkillTreeCommand : Command
    {
        private TT2DataService DataService { get; }
        protected override LocalisedString DelayMessage => (LocalisedString)DELAYMESSAGE_DATA;

        public SkillTreeCommand(TT2DataService dataService)
        {
            DataService = dataService;
        }

        [Call]
        async Task ListSkillsAsync()
        {
            var skills = await DataService.SkillTree.GetAll();

            await ReplyAsync(LocalisedString.Join("\n", skills as object[]));
        }
    }
}

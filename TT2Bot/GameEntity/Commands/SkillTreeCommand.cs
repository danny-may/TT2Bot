using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Services;
using static TT2Bot.TT2Localisation.CommandText;

namespace TT2Bot.GameEntity.Commands
{
    [Alias("Skill", "Skills"), Group("Data")]
    class SkillTreeCommand : Command
    {
        private TT2DataService DataService { get; }
        protected override LocalisedString DelayMessage => (LocalisedString)DELAYMESSAGE_DATA;

        public SkillTreeCommand(TT2DataService dataService)
        {
            DataService = dataService;
        }

        [Call, Alias("List")]
        async Task ListSkillsAsync()
        {
            var skills = await DataService.SkillTree.GetAll();

            await ReplyAsync(LocalisedString.Join("\n", skills as object[]));
        }

        [Call, Alias("Show", "View")]
        async Task ViewSkill([Dense]Skill skill)
        {
            await ReplyAsync(skill.Name);
        }
    }
}

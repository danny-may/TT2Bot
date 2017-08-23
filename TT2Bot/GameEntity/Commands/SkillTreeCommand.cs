using System.Threading.Tasks;
using TitanBot.Commands;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Embedables;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Services;

namespace TT2Bot.GameEntity.Commands
{
    [Alias("Skill", "Skills"), Group("Data")]
    class SkillTreeCommand : GameEntityCommand
    {
        public SkillTreeCommand(TT2DataService dataService) : base(dataService) { }

        [Call, Alias("List")]
        async Task ListSkillsAsync()
            => await ReplyAsync(new SkillTreeListEmbedable(Context, DataService.SkillTree));

        [Call, Alias("Show", "View")]
        async Task ViewSkill([Dense]Skill skill)
            => await ReplyAsync(new SkillTreeSingleEmbedable(Context, skill));
    }
}

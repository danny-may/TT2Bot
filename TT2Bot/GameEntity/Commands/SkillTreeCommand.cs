using System.Threading.Tasks;
using TitanBot.Commands;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Embedables;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Services;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.GameEntity.Commands
{
    //[Alias("Skill", "Skills"), Group("Data")]
    //[Description(Desc.SKILL)]
    //class SkillTreeCommand : GameEntityCommand
    //{
    //    public SkillTreeCommand(TT2DataService dataService) : base(dataService) { }

    //    [Call, Alias("List")]
    //    [Usage(Usage.SKILL)]
    //    async Task ListSkillsAsync()
    //        => await ReplyAsync(new SkillTreeListEmbedable(Context, DataService.SkillTree));

    //    [Call, Alias("Show", "View")]
    //    [Usage(Usage.SKILL_SHOW)]
    //    async Task ViewSkill([Dense]Skill skill)
    //        => await ReplyAsync(new SkillTreeSingleEmbedable(Context, skill));
    //}
}

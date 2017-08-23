using System.Threading.Tasks;
using TitanBot.Commands;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Embedables;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Services;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.GameEntity.Commands
{
    [Description(Desc.HELPER), Group("Data")]
    [Alias("Helper")]
    [Name("Hero")]
    class HelpersCommand : GameEntityCommand
    {
        public HelpersCommand(TT2DataService dataService) : base(dataService) { }

        [Call("List")]
        [Usage(Usage.HELPER_LIST)]

        async Task ListHelpersAsync([CallFlag('g', "group", Flag.HELPER_G)]bool shouldGroup = false)
            => await ReplyAsync(new HelperListEmbedable(Context, DataService.Helpers, shouldGroup));

        [Call]
        [Usage(Usage.HELPER)]
        async Task ShowHelperAsync([Dense]Helper helper, int? from = null, int? to = null)
            => await ReplyAsync(new HelperSingleEmbedable(Context, helper, from, to));
    }
}

using System.Threading.Tasks;
using TitanBot.Commands;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Embedables;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Services;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.GameEntity.Commands
{
    [Description(Desc.PETS), Group("Data")]
    [Alias("Pet")]
    class PetsCommand : GameEntityCommand
    {
        public PetsCommand(TT2DataService dataService) : base(dataService) { }

        [Call]
        [Usage(Usage.PET)]
        async Task ShowPetAsync([Dense]Pet pet, int? level = null)
            => await ReplyAsync(new PetSingleEmbedable(Context, pet, level));

        [Call("List")]
        [Usage(Usage.PET_LIST)]
        async Task ListPetsAsync()
            => await ReplyAsync(new PetListEmbedable(Context, DataService.Pets));
    }
}

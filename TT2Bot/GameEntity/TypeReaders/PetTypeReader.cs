using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Services;

namespace TT2Bot.GameEntity.TypeReaders
{
    class PetTypeReader : GameEntityTypeReader<Pet>
    {
        private TT2DataService DataService { get; }

        protected override GameEntityService<Pet> Service => DataService.Pets;
        protected override string UNABLE_DOWNLOAD => Pet.Localisation.UNABLE_DOWNLOAD;
        protected override string MULTIPLE_MATCHES => Pet.Localisation.MULTIPLE_MATCHES;

        public PetTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }
    }
}

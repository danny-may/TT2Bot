using TT2Bot.Models.TT2;
using TT2Bot.Services;
using TT2Bot.Services.ServiceAreas;

namespace TT2Bot.TypeReaders
{
    class PetTypeReader : GameEntityTypeReader<Pet, int>
    {
        private TT2DataService DataService { get; }

        protected override GameEntityService<Pet, int> Service => DataService.Pets;
        protected override string UNABLE_DOWNLOAD => TT2Localisation.Game.Pet.UNABLE_DOWNLOAD;
        protected override string MULTIPLE_MATCHES => TT2Localisation.Game.Pet.MULTIPLE_MATCHES;

        public PetTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }
    }
}

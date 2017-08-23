using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Services;

namespace TT2Bot.GameEntity.TypeReaders
{
    class HelperTypeReader : GameEntityTypeReader<Helper>
    {
        private TT2DataService DataService { get; }

        protected override GameEntityService<Helper> Service => DataService.Helpers;
        protected override string UNABLE_DOWNLOAD => Helper.Localisation.UNABLE_DOWNLOAD;
        protected override string MULTIPLE_MATCHES => Helper.Localisation.MULTIPLE_MATCHES;

        public HelperTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }
    }
}

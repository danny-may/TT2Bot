using TT2Bot.Models.TT2;
using TT2Bot.Services;
using TT2Bot.Services.ServiceAreas;

namespace TT2Bot.TypeReaders
{
    class HelperTypeReader : GameEntityTypeReader<Helper, int>
    {
        private TT2DataService DataService { get; }

        protected override GameEntityService<Helper, int> Service => DataService.Helpers;
        protected override string UNABLE_DOWNLOAD => TT2Localisation.Game.Helper.UNABLE_DOWNLOAD;
        protected override string MULTIPLE_MATCHES => TT2Localisation.Game.Helper.MULTIPLE_MATCHES;

        public HelperTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }
    }
}

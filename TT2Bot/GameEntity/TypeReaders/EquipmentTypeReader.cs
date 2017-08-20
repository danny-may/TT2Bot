using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Services;

namespace TT2Bot.GameEntity.TypeReaders
{
    class EquipmentTypeReader : GameEntityTypeReader<Equipment>
    {
        private TT2DataService DataService { get; }

        protected override GameEntityService<Equipment> Service => DataService.Equipment;
        protected override string UNABLE_DOWNLOAD => Equipment.Localisation.UNABLE_DOWNLOAD;
        protected override string MULTIPLE_MATCHES => Equipment.Localisation.MULTIPLE_MATCHES;

        public EquipmentTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }
    }
}

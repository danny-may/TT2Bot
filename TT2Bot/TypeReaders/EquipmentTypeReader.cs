using System;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Contexts;
using TitanBot.TypeReaders;
using TT2Bot.Models;
using TT2Bot.Services;
using TT2Bot.Services.ServiceAreas;

namespace TT2Bot.TypeReaders
{
    class EquipmentTypeReader : GameEntityTypeReader<Equipment, string>
    {
        private TT2DataService DataService { get; }

        protected override GameEntityService<Equipment, string> Service => DataService.Equipment;
        protected override string UNABLE_DOWNLOAD => TT2Localisation.Game.Equipment.UNABLE_DOWNLOAD;
        protected override string MULTIPLE_MATCHES => TT2Localisation.Game.Equipment.MULTIPLE_MATCHES;

        public EquipmentTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }
    }
}

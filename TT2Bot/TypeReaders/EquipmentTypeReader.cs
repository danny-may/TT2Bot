using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.TypeReaders;
using TT2Bot.Helpers;
using TT2Bot.Models;

namespace TT2Bot.TypeReaders
{
    class EquipmentTypeReader : TypeReader
    {
        private TT2DataService DataService { get; }

        public EquipmentTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }

        public override async Task<TypeReaderResponse> Read(ICommandContext context, string value)
        {
            var equip = Equipment.Find(value);
            if (equip == null)
                return TypeReaderResponse.FromError("TYPEREADER_UNABLETOREAD", value, typeof(Equipment));


            var equipment = await DataService.GetEquipment(equip);
            if (equipment == null)
                return TypeReaderResponse.FromError("Could not download data for equipment `{2}`", equip.Id.ToString(), typeof(Equipment));
            return TypeReaderResponse.FromSuccess(equipment);
        }
    }
}

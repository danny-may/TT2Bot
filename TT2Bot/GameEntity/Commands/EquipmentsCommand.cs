using System.Threading.Tasks;
using TitanBot.Commands;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Embedables;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Services;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.GameEntity.Commands
{
    [Description(Desc.EQUIPMENT), Group("Data")]
    [Alias("Equip", "Equips", "Equipment")]
    class EquipmentsCommand : GameEntityCommand
    {
        public EquipmentsCommand(TT2DataService dataService) : base(dataService) { }

        [Call("List")]
        [Usage(Usage.EQUIPMENT_LIST)]
        async Task ListEquipmentAsync([Dense]string equipClass = null)
            => await ReplyAsync(new EquipmentListEmbedable(Context, DataService.Equipment, equipClass));

        [Call]
        [Usage(Usage.EQUIPMENT)]
        async Task ShowEquipmentAsync([Dense]Equipment equipment, double? level = null)
            => await ReplyAsync(new EquipmentSingleEmbedable(Context, equipment, level));
    }
}

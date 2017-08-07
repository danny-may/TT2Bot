using Discord;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting.Interfaces;
using TT2Bot.Models;
using TT2Bot.Services;
using static TT2Bot.TT2Localisation.Commands;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Data
{
    [Description(Desc.EQUIPMENT)]
    [Alias("Equip", "Equips", "Equipment")]
    class EquipmentsCommand : Command
    {
        private TT2DataService DataService { get; }
        protected override string DelayMessage { get; } = DELAYMESSAGE_DATA;

        public EquipmentsCommand(TT2DataService dataService)
        {
            DataService = dataService;
        }

        [Call("List")]
        [Usage(Usage.EQUIPMENT_LIST)]
        async Task ListEquipmentAsync([Dense]string equipClass = null)
        {
            var builder = new LocalisedEmbedBuilder
            {
                Color = System.Drawing.Color.LightBlue.ToDiscord(),
                Footer = new LocalisedFooterBuilder().WithRawIconUrl(BotUser.GetAvatarUrl())
                                                     .WithText(EquipmentText.LIST_FOOTER, BotUser.Username),
                Timestamp = DateTime.Now
            }.WithTitle(EquipmentText.LIST_TITLE);

            IEnumerable<(LocalisedString Title, Equipment[] Values)> fields = null;
            List<Equipment> allEquip = (await DataService.Equipment.GetAll()).OrderBy(e => e.BonusBase).ThenBy(e => e.BonusIncrease).ToList();

            if (!string.IsNullOrWhiteSpace(equipClass))
            {
                switch (EquipmentClasseMethods.Find(TextResource, equipClass))
                {
                    case EquipmentClass.Aura:
                        fields = allEquip.Where(e => e.Class == EquipmentClass.Aura)
                                         .GroupBy(e => e.BonusType)
                                         .Select(g => (g.Key.ToLocalisable(), g.OrderBy(e => e.Rarity).ToArray()));
                        break;
                    case EquipmentClass.Weapon:
                        fields = allEquip.Where(e => e.Class == EquipmentClass.Weapon)
                                         .GroupBy(e => e.BonusType)
                                         .Select(g => (g.Key.ToLocalisable(), g.OrderBy(e => e.Rarity).ToArray()));
                        break;
                    case EquipmentClass.Hat:
                        fields = allEquip.Where(e => e.Class == EquipmentClass.Hat)
                                         .GroupBy(e => e.BonusType)
                                         .Select(g => (g.Key.ToLocalisable(), g.OrderBy(e => e.Rarity).ToArray()));
                        break;
                    case EquipmentClass.Slash:
                        fields = allEquip.Where(e => e.Class == EquipmentClass.Slash)
                                         .GroupBy(e => e.BonusType)
                                         .Select(g => (g.Key.ToLocalisable(), g.OrderBy(e => e.Rarity).ToArray()));
                        break;
                    case EquipmentClass.Suit:
                        fields = allEquip.Where(e => e.Class == EquipmentClass.Suit)
                                         .GroupBy(e => e.BonusType)
                                         .Select(g => (g.Key.ToLocalisable(), g.OrderBy(e => e.Rarity).ToArray()));
                        break;
                    case EquipmentClass.None when (equipClass.ToLower() == "removed"):
                        fields = allEquip.Where(e => e.Rarity == EquipmentRarity.Removed)
                                         .GroupBy(e => e.Class)
                                         .Select(g => ((LocalisedString)g.Key.ToString(), g.OrderBy(e => e.Localise(TextResource)).ToArray()));
                        break;
                    default:
                        fields = null;
                        break;
                }
            }
            if (fields == null)
                builder.WithDescription(EquipmentText.LIST_DESCRIPTION_NONE, LocalisedString.Join("\n", Enum.GetValues(typeof(EquipmentClass)).Cast<EquipmentClass>().Select(c => c.ToLocalisable()).ToArray()), Prefix, CommandName);
            else
            {
                builder.WithDescription(EquipmentText.LIST_DESCRIPTION, equipClass);
                foreach (var field in fields)
                    builder.AddInlineField(f => f.WithName(field.Title)
                           .WithValues("\n", field.Values));
            }

            await ReplyAsync(builder);
        }

        LocalisedEmbedBuilder GetBaseEmbed(Equipment equipment)
        {
            var builder = new LocalisedEmbedBuilder
            {
                Author = new LocalisedAuthorBuilder().WithName(EquipmentText.SHOW_TITLE, equipment.Name).WithRawIconUrl(equipment.ImageUrl),
                Footer = new LocalisedFooterBuilder().WithRawIconUrl(BotUser.GetAvatarUrl()).WithText(EquipmentText.SHOW_FOOTER, BotUser.Username, equipment.FileVersion),
                Timestamp = DateTime.Now,
                Color = equipment.Image.AverageColor(0.3f, 0.5f).ToDiscord(),
            }.WithRawThumbnailUrl(equipment.ImageUrl)
             .AddInlineField(f => f.WithName(EquipmentText.SHOW_FIELD_ID).WithRawValue(equipment.Id))
             .AddInlineField(f => f.WithName(EquipmentText.SHOW_FIELD_CLASS).WithValue(equipment.Class.ToLocalisable()))
             .AddInlineField(f => f.WithName(EquipmentText.SHOW_FIELD_RARITY).WithValue(equipment.Rarity.ToLocalisable()))
             .AddInlineField(f => f.WithName(EquipmentText.SHOW_FIELD_SOURCE).WithValue(equipment.Source.ToLocalisable()))
             .AddField(f => f.WithName(EquipmentText.SHOW_FIELD_BONUSTYPE).WithValue(equipment.BonusType.ToLocalisable()));

            return builder;
        }

        [Call]
        [Usage(Usage.EQUIPMENT)]
        async Task ShowEquipmentAsync([Dense] Equipment equipment, double? level = null)
        {
            var builder = GetBaseEmbed(equipment);

            if (level == null)
            {
                builder.AddInlineField(f => f.WithName(EquipmentText.SHOW_FIELD_BONUSBASE).WithValue(equipment.BonusType.LocaliseValue(equipment.BonusBase)));
                builder.AddInlineField(f => f.WithName(EquipmentText.SHOW_FIELD_BONUSINCREASE).WithValue(equipment.BonusType.LocaliseValue(equipment.BonusIncrease)));
                builder.AddField(f => f.WithName(TitanBot.TBLocalisation.NOTES).WithValue(EquipmentText.SHOW_FIELD_NOTE_NOLEVEL));
            }
            else
            {
                builder.AddField(f => f.WithName(EquipmentText.SHOW_FIELD_BONUSAT, level, level*10).WithValue(equipment.BonusType.LocaliseValue(equipment.BonusOnLevel((int)(10 * level)))));
                builder.AddField(f => f.WithName(TitanBot.TBLocalisation.NOTES).WithValue(EquipmentText.SHOW_FIELD_NOTE));
            }

            await ReplyAsync(builder);
        }
    }
}

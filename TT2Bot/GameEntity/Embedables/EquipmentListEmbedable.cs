using System;
using System.Collections.Generic;
using System.Linq;
using TitanBot.Contexts;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.Enums;
using TT2Bot.Models;
using static TT2Bot.TT2Localisation.CommandText;

namespace TT2Bot.GameEntity.Embedables
{
    internal class EquipmentListEmbedable : GameEntityListEmbedable<Equipment>
    {
        private string GroupName { get; }

        protected override ILocalisable<string> Description => new LocalisedString(EquipmentText.LIST_DESCRIPTION, GroupName);
        protected override ILocalisable<string> Footer => new LocalisedString(EquipmentText.LIST_FOOTER, BotUser.Username);
        protected override ILocalisable<string> Title => new LocalisedString(EquipmentText.LIST_TITLE);

        public EquipmentListEmbedable(ICommandContext context, GameEntityService<Equipment> entityService, string groupName) : base(context, entityService)
        {
            GroupName = groupName;
        }

        protected override IEnumerable<Equipment> AllEntities
            => base.AllEntities.OrderBy(e => e.AttributeBase).ThenBy(e => e.AttributeBaseInc).ToList();

        private Dictionary<ILocalisable<string>, Equipment[]> GroupEquipment()
        {
            if (string.IsNullOrWhiteSpace(GroupName))
                return null;

            var allEntities = AllEntities.ToArray();

            switch (EquipmentClassMethods.Find(Context.TextResource, GroupName))
            {
                case EquipmentClass.Aura:
                    return AllEntities.Where(e => e.Class == EquipmentClass.Aura)
                                         .GroupBy(e => e.BonusType)
                                         .ToDictionary(g => g.Key.ToLocalisable(), g => g.ToArray());
                case EquipmentClass.Weapon:
                    return AllEntities.Where(e => e.Class == EquipmentClass.Weapon)
                                         .GroupBy(e => e.BonusType)
                                         .ToDictionary(g => g.Key.ToLocalisable(), g => g.ToArray());
                case EquipmentClass.Hat:
                    return AllEntities.Where(e => e.Class == EquipmentClass.Hat)
                                         .GroupBy(e => e.BonusType)
                                         .ToDictionary(g => g.Key.ToLocalisable(), g => g.ToArray());
                case EquipmentClass.Slash:
                    return AllEntities.Where(e => e.Class == EquipmentClass.Slash)
                                         .GroupBy(e => e.BonusType)
                                         .ToDictionary(g => g.Key.ToLocalisable(), g => g.ToArray());
                case EquipmentClass.Suit:
                    return AllEntities.Where(e => e.Class == EquipmentClass.Suit)
                                         .GroupBy(e => e.BonusType)
                                         .ToDictionary(g => g.Key.ToLocalisable(), g => g.ToArray());
                case EquipmentClass.None when (GroupName.ToLower() == "removed"):
                    return AllEntities.Where(e => e.Rarity == EquipmentRarity.Removed)
                                         .GroupBy(e => e.Class)
                                         .ToDictionary(g => g.Key.ToLocalisable(), g => g.ToArray());
                default:
                    return null;
            }
        }

        protected override void AddFields(LocalisedEmbedBuilder builder)
        {
            var grouped = GroupEquipment();

            if (grouped == null)
                builder.WithDescription(EquipmentText.LIST_DESCRIPTION_NONE, LocalisedString.Join("\n", Enum.GetValues(typeof(EquipmentClass)).Cast<EquipmentClass>().Select(c => c.ToLocalisable()).ToArray()), Context.Prefix, Context.CommandText);
            else
                foreach (var group in GroupEquipment())
                    builder.AddInlineField(f => f.WithName(group.Key).WithValues("\n", group.Value));
        }

        protected override void AddFields(List<ILocalisable<string>> builder)
        {
            throw new NotImplementedException();
        }
    }
}
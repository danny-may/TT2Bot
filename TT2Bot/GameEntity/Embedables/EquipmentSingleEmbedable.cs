using System;
using System.Collections.Generic;
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
    class EquipmentSingleEmbedable : GameEntitySingleEmbedable<Equipment>
    {
        public EquipmentSingleEmbedable(ICommandContext context, Equipment entity, double? level) : base(context, entity, null, level) { }

        protected override ILocalisable<string> Title => new LocalisedString(EquipmentText.SHOW_TITLE, Entity.Name);
        protected override ILocalisable<string> Footer => new LocalisedString(EquipmentText.SHOW_FOOTER, BotUser.Username, Entity.FileVersion);

        protected override float MinBrightness => 0;
        protected override float MinSaturation => 0;

        protected override LocalisedEmbedBuilder GetBaseEmbed()
            => base.GetBaseEmbed().AddInlineField(f => f.WithName(EquipmentText.SHOW_FIELD_ID).WithRawValue(Entity.Id.ToString()))
                                  .AddInlineField(f => f.WithName(EquipmentText.SHOW_FIELD_CLASS).WithValue(Entity.Class.ToLocalisable()))
                                  .AddInlineField(f => f.WithName(EquipmentText.SHOW_FIELD_RARITY).WithValue(Entity.Rarity.ToLocalisable()))
                                  .AddInlineField(f => f.WithName(EquipmentText.SHOW_FIELD_SOURCE).WithValue(Entity.Source.ToLocalisable()))
                                  .AddField(f => f.WithName(EquipmentText.SHOW_FIELD_BONUSTYPE).WithValue(Entity.BonusType.ToLocalisable()));

        protected override void AddFields(LocalisedEmbedBuilder builder)
        {
            if (To is double level)
                builder.AddField(f => f.WithName(EquipmentText.SHOW_FIELD_BONUSAT, level, level * 10).WithValue(Entity.BonusType.ToLocalisable(Entity.BonusOnLevel(10 * level))))
                       .AddField(f => f.WithName(TitanBot.TBLocalisation.NOTES).WithValue(EquipmentText.SHOW_FIELD_NOTE));
            else
                builder.AddInlineField(f => f.WithName(EquipmentText.SHOW_FIELD_BONUSBASE).WithValue(Entity.BonusType.ToLocalisable(Entity.BonusBase)))
                       .AddInlineField(f => f.WithName(EquipmentText.SHOW_FIELD_BONUSINCREASE).WithValue(Entity.BonusType.ToLocalisable(Entity.BonusIncrease)))
                       .AddField(f => f.WithName(TitanBot.TBLocalisation.NOTES).WithValue(EquipmentText.SHOW_FIELD_NOTE_NOLEVEL));
        }

        protected override void AddFields(List<ILocalisable<string>> builder)
        {
            throw new NotImplementedException();
        }
    }
}

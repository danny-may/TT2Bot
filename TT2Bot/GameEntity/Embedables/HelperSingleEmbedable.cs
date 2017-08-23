using System;
using System.Collections.Generic;
using TitanBot.Contexts;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.Enums;
using static TT2Bot.TT2Localisation.CommandText;

namespace TT2Bot.GameEntity.Embedables
{
    class HelperSingleEmbedable : GameEntitySingleEmbedable<Helper>
    {
        protected override ILocalisable<string> Footer => new LocalisedString(HelperText.SHOW_FOOTER, BotUser.Username, Entity.FileVersion);
        protected override ILocalisable<string> Title => new LocalisedString(HelperText.SHOW_TITLE, Entity.Name);

        public HelperSingleEmbedable(ICommandContext context, Helper entity, int? from, int? to) : base(context, entity, from, to) { }

        protected override LocalisedEmbedBuilder GetBaseEmbed()
            => base.GetBaseEmbed().AddInlineField(f => f.WithName(HelperText.SHOW_FIELD_ID).WithValue(Entity.Id))
                                  .AddInlineField(f => f.WithName(HelperText.SHOW_FIELD_TYPE).WithValue(Entity.HelperType.ToLocalisable()));

        protected override void AddFields(LocalisedEmbedBuilder builder)
        {
            if (To != null)
            {
                var from = (int)(From ?? 0);
                var to = (int)To;
                builder.AddInlineField(f => f.WithName(HelperText.SHOW_FIELD_COSTAT, from).WithValue(HelperText.COST, Entity.GetCost(from + 1, 1)))
                       .AddInlineField(f => f.WithName(HelperText.SHOW_FIELD_DAMAGEAT, from).WithValue(HelperText.DPS, Entity.GetDps(from + 1)));
            }
            else
                builder.AddInlineField(f => f.WithName(HelperText.SHOW_FIELD_BASECOST).WithValue(HelperText.COST, Entity.GetCost(0)))
                       .AddInlineField(f => f.WithName(HelperText.SHOW_FIELD_BASEDAMAGE).WithValue(HelperText.DPS, Entity.BaseDamage));
        }

        protected override void AddFields(List<ILocalisable<string>> builder)
        {
            throw new NotImplementedException();
        }
    }
}

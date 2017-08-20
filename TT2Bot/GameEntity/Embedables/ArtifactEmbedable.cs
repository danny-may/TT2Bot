using System;
using System.Collections.Generic;
using TitanBot.Contexts;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Models;
using static TT2Bot.TT2Localisation.CommandText;

namespace TT2Bot.GameEntity.Embedables
{
    class ArtifactEmbedable : GameEntitySingleEmbedable<Artifact>
    {
        protected override ILocalisable<string> Footer => new LocalisedString(ArtifactText.LIST_FOOTER, BotUser.Username, Entity.FileVersion);
        protected override ILocalisable<string> Title => new LocalisedString(ArtifactText.SHOW_TITLE, Entity.Name);

        public ArtifactEmbedable(ICommandContext context, Artifact entity, int? from, int? to) : base(context, entity, from, to) { }

        protected override LocalisedEmbedBuilder GetBaseEmbed()
            => base.GetBaseEmbed().AddInlineField(f => f.WithName(ArtifactText.SHOW_IDTITLE).WithValue(Entity.Id))
                                  .AddInlineField(f => f.WithName(ArtifactText.SHOW_TIERTITLE).WithValue(ArtifactText.SHOW_TEIRVALUE, Entity.Tier))
                                  .AddField(f => f.WithName(ArtifactText.SHOW_MAXLEVELTITLE).WithValue(Entity.MaxLevel ?? double.PositiveInfinity));

        protected override void AddFields(LocalisedEmbedBuilder builder)
        {
            if (To != null)
            {
                var from = (int)(From ?? 1);
                var to = (int)To;
                builder.AddField(f => f.WithName(ArtifactText.SHOW_BONUSTYPE).WithValue(Entity.BonusType.ToLocalisable()))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_EFFECTAT, From ?? 1).WithValue(Entity.BonusType.LocaliseValue(Entity.EffectAt(from))))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_EFFECTAT, To).WithValue(Entity.BonusType.LocaliseValue(Entity.EffectAt(to))))
                       .AddField(f => f.WithName(ArtifactText.SHOW_BONUSTYPE).WithValue(BonusType.ArtifactDamage.ToLocalisable()))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_EFFECTAT, From ?? 1).WithValue(BonusType.ArtifactDamage.LocaliseValue(Entity.DamageAt(from))))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_EFFECTAT, To).WithValue(BonusType.ArtifactDamage.LocaliseValue(Entity.DamageAt(to))))
                       .AddField(f => f.WithName(ArtifactText.SHOW_LVLRANGE, From ?? 1, To).WithValue(ArtifactText.SHOW_COST, Entity.CostToLevel(from + 1, to)))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_COSTOFLVL, From ?? 1).WithValue(ArtifactText.SHOW_COST, Entity.CostOfLevel(from + 1)))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_COSTOFLVL, To).WithValue(ArtifactText.SHOW_COST, Entity.CostOfLevel(to)));
            }
            else
                builder.AddInlineField(f => f.WithName(ArtifactText.SHOW_BONUSTYPE).WithValue(Entity.BonusType.ToLocalisable()))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_BONUSPERLVL).WithValue(Entity.BonusType.LocaliseValue(Entity.EffectPerLevel)))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_BONUSTYPE).WithValue(BonusType.ArtifactDamage.ToLocalisable()))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_BONUSPERLVL).WithValue(BonusType.ArtifactDamage.LocaliseValue(Entity.DamageBonus)))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_COSTCOEF).WithValue(Entity.CostCoef))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_COSTEXPO).WithValue(Entity.CostExpo))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_COSTOFLVL, 1).WithValue(ArtifactText.SHOW_COST, Entity.CostOfLevel(2)));
        }

        protected override void AddFields(List<ILocalisable<string>> builder)
        {
            throw new NotImplementedException();
        }
    }
}

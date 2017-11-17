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
    internal class PetSingleEmbedable : GameEntitySingleEmbedable<Pet>
    {
        protected override ILocalisable<string> Footer => new LocalisedString(PetText.SHOW_FOOTER, BotUser.Username, Entity.FileVersion);
        protected override ILocalisable<string> Title => new LocalisedString(PetText.SHOW_TITLE, Entity.Name);

        public PetSingleEmbedable(ICommandContext context, Pet entity, int? level) : base(context, entity, null, level)
        {
        }

        protected override LocalisedEmbedBuilder GetBaseEmbed()
            => base.GetBaseEmbed().AddInlineField(f => f.WithName(PetText.SHOW_FIELD_ID).WithValue(Entity.Id));

        protected override void AddFields(LocalisedEmbedBuilder builder)
        {
            if (To == null)
            {
                builder.AddField(f => f.WithName(BonusType.PetDamage.ToLocalisable()).WithValue(BonusType.PetDamage.ToLocalisable(Entity.DamageBase)));
                var keysOrdered = Entity.IncreaseRanges.Keys.OrderBy(k => k).ToList();
                for (int i = 0; i < keysOrdered.Count(); i++)
                    if (i == keysOrdered.Count() - 1)
                        builder.AddInlineField(f => f.WithName(PetText.SHOW_FIELD_LVLXUP, keysOrdered[i])
                                                     .WithValue(BonusType.PetDamage.ToLocalisable(Entity.IncreaseRanges[keysOrdered[i]])));
                    else
                        builder.AddInlineField(f => f.WithName(PetText.SHOW_FIELD_LVLXTOY, keysOrdered[i], keysOrdered[i + 1] - 1)
                                                     .WithValue(BonusType.PetDamage.ToLocalisable(Entity.IncreaseRanges[keysOrdered[i]])));
                builder.AddField(f => f.WithName(PetText.SHOW_FIELD_BONUSTYPE).WithValue(Entity.BonusType.ToLocalisable()))
                       .AddInlineField(f => f.WithName(PetText.SHOW_FIELD_BONUSBASE).WithValue(Entity.BonusType.ToLocalisable(Entity.BonusBase)))
                       .AddInlineField(f => f.WithName(PetText.SHOW_FIELD_BONUSINCREASE).WithValue(Entity.BonusType.ToLocalisable(Entity.BonusIncrement)));
            }
            else
            {
                var actualLevel = (int)To;
                var dmg = Entity.DamageOnLevel(actualLevel);
                var bonus = Entity.BonusOnLevel(actualLevel);
                var mult = Entity.InactiveMultiplier(actualLevel);
                builder.AddField(f => f.WithName(PetText.SHOW_FIELD_BONUSTYPE).WithValue(Entity.BonusType.ToLocalisable()));
                builder.AddInlineField(f => f.WithName(PetText.SHOW_FIELD_DAMAGEAT, actualLevel).WithValue(BonusType.PetDamage.ToLocalisable(dmg)));
                builder.AddInlineField(f => f.WithName(PetText.SHOW_FIELD_BONUSAT, actualLevel).WithValue(Entity.BonusType.ToLocalisable(bonus)));
                if (mult < 1)
                {
                    builder.AddField(f => f.WithName(PetText.SHOW_FIELD_INACTIVEPERCENT, actualLevel).WithValue(mult));
                    builder.AddInlineField(f => f.WithName(PetText.SHOW_FIELD_INACTIVEDAMAGEAT, actualLevel).WithValue(BonusType.PetDamage.ToLocalisable(mult * dmg)));
                    builder.AddInlineField(f => f.WithName(PetText.SHOW_FIELD_INACTIVEBONUSAT, actualLevel).WithValue(Entity.BonusType.ToLocalisable(mult * bonus)));
                }
            }
        }

        protected override void AddFields(List<ILocalisable<string>> builder)
        {
            throw new NotImplementedException();
        }
    }
}
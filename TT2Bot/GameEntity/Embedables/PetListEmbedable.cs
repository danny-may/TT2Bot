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
    class PetListEmbedable : GameEntityListEmbedable<Pet>
    {
        protected override ILocalisable<string> Description => new LocalisedString(PetText.LIST_DESCRIPTION);
        protected override ILocalisable<string> Footer => new LocalisedString(PetText.LIST_FOOTER, BotUser.Username);
        protected override ILocalisable<string> Title => new LocalisedString(PetText.LIST_TITLE);

        public PetListEmbedable(ICommandContext context, GameEntityService<Pet> entityService) : base(context, entityService) { }

        private IEnumerable<IGrouping<BonusType, Pet>> GroupPets()
            => AllEntities.GroupBy(p => p.BonusType).OrderBy(g => g.Key);

        protected override void AddFields(LocalisedEmbedBuilder builder)
        {
            foreach (var group in GroupPets())
                builder.AddInlineField(f => f.WithName(group.Key.ToLocalisable()).WithValues("\n", group));
        }

        protected override void AddFields(List<ILocalisable<string>> builder)
        {
            throw new NotImplementedException();
        }
    }
}

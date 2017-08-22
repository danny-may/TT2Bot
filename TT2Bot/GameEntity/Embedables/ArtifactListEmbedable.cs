using System;
using System.Collections.Generic;
using System.Linq;
using TitanBot.Contexts;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.Enums;
using static TT2Bot.TT2Localisation.CommandText;

namespace TT2Bot.GameEntity.Embedables
{
    class ArtifactListEmbedable : GameEntityListEmbedable<Artifact>
    {
        protected override ILocalisable<string> Description => new LocalisedString(ArtifactText.LIST_DESCRIPTION);
        protected override ILocalisable<string> Footer => new LocalisedString(ArtifactText.LIST_FOOTER, BotUser.Username);
        protected override ILocalisable<string> Title => new LocalisedString(ArtifactText.LIST_TITLE);

        public ArtifactListEmbedable(ICommandContext context, GameEntityService<Artifact> entityService) : base(context, entityService) { }

        private IEnumerable<IGrouping<ArtifactTier, Artifact>> GroupArtifacts()
            => AllEntities.GroupBy(e => e.Tier).OrderBy(g => g.Key);

        protected override void AddFields(LocalisedEmbedBuilder builder)
        {
            foreach (var group in GroupArtifacts())
                builder.AddInlineField(f => f.WithName(ArtifactText.LIST_ROW_TITLE, group.Key).WithValues("\n", group));
        }

        protected override void AddFields(List<ILocalisable<string>> builder)
        {
            throw new NotImplementedException();
        }
    }
}

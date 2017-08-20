using System;
using System.Collections.Generic;
using System.Linq;
using TitanBot.Contexts;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Models;
using static TT2Bot.TT2Localisation.CommandText;

namespace TT2Bot.GameEntity.Embedables
{
    class HelperListEmbedable : GameEntityListEmbedable<Helper>
    {
        protected override ILocalisable<string> Description => new LocalisedString(HelperText.LIST_DESCRIPTION);
        protected override ILocalisable<string> Footer => new LocalisedString(HelperText.LIST_FOOTER, BotUser.Username);
        protected override ILocalisable<string> Title => new LocalisedString(HelperText.LIST_TITLE);

        private bool ShouldGroup { get; }

        public HelperListEmbedable(ICommandContext context, GameEntityService<Helper> entityService, bool shouldGroup) : base(context, entityService)
        {
            ShouldGroup = shouldGroup;
        }

        private IEnumerable<IGrouping<HelperType, Helper>> GroupHelpers()
            => AllEntities.GroupBy(h => h.HelperType);

        protected override void AddFields(LocalisedEmbedBuilder builder)
        {
            if (ShouldGroup)
                foreach (var group in GroupHelpers())
                    builder.AddInlineField(f => f.WithName(HelperText.LIST_FIELD_GROUPED, group.Key.ToLocalisable()).WithValue(tr => MakeTable(tr, group)));
            else
                builder.AddField(f => f.WithName(HelperText.LIST_FIELD_ALL).WithValue(tr => MakeTable(tr, AllEntities)));
        }

        private string MakeTable(ITextResourceCollection textResource, IEnumerable<Helper> helpers)
            => "```\n" + helpers.Select(h => new[]
                                             {
                                                 h.ShortName.Localise(textResource),
                                                 new LocalisedString(HelperText.COST, h.BaseCost).Localise(textResource)
                                             }).ToArray()
                                               .Tableify() + "\n```";

        protected override void AddFields(List<ILocalisable<string>> builder)
        {
            throw new NotImplementedException();
        }
    }
}

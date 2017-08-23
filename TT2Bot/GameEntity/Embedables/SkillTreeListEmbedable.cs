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
    class SkillTreeListEmbedable : GameEntityListEmbedable<Skill>
    {
        protected override ILocalisable<string> Description => new LocalisedString(SkillText.LIST_DESCRIPTION);
        protected override ILocalisable<string> Footer => new LocalisedString(SkillText.LIST_FOOTER, BotUser.Username);
        protected override ILocalisable<string> Title => new LocalisedString(SkillText.LIST_TITLE);

        public SkillTreeListEmbedable(ICommandContext context, GameEntityService<Skill> entityService) : base(context, entityService) { }

        private IEnumerable<IGrouping<SkillCategory, Skill>> GroupSkills()
            => AllEntities.Where(s => s.Category != SkillCategory.None).GroupBy(s => s.Category);

        protected override void AddFields(LocalisedEmbedBuilder builder)
        {
            foreach (var group in GroupSkills())
                builder.AddField(f => f.WithName(group.Key.ToLocalisable()).WithValues("\n", group));
        }

        protected override void AddFields(List<ILocalisable<string>> builder)
        {
            throw new NotImplementedException();
        }
    }
}

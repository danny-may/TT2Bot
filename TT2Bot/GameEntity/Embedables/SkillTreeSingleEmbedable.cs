using System;
using System.Collections.Generic;
using System.Linq;
using TitanBot.Contexts;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.Enums;
using TT2Bot.GameEntity.Enums.EntityId;
using TT2Bot.Models;
using static TT2Bot.TT2Localisation.CommandText;

namespace TT2Bot.GameEntity.Embedables
{
    class SkillTreeSingleEmbedable : GameEntitySingleEmbedable<Skill>
    {
        protected override ILocalisable<string> Footer => new LocalisedString(SkillText.SHOW_FOOTER, BotUser.Username, Entity.FileVersion);
        protected override ILocalisable<string> Title => new LocalisedString(SkillText.SHOW_TITLE, Entity.Name);

        public SkillTreeSingleEmbedable(ICommandContext context, Skill entity) : base(context, entity, null, null) { }

        protected override LocalisedEmbedBuilder GetBaseEmbed()
            => base.GetBaseEmbed().AddInlineField(f => f.WithName(SkillText.SHOW_IDTITLE).WithRawValue(Entity.Id.ToString()))
                                  .AddInlineField(f => f.WithName(SkillText.SHOW_CATEGORY).WithValue(Entity.Category.ToLocalisable()))
                                  .AddInlineField(f => f.WithName(SkillText.SHOW_PARENT).WithValue(Entity.Requirement as ILocalisable<string> ?? new LocalisedString(SkillText.NOPARENT)))
                                  .AddInlineField(f => f.WithName(SkillText.SHOW_UNLOCKAT).WithValue(SkillText.STAGE, Entity.StageRequirement))
                                  .AddInlineField(f => f.WithName(SkillText.SHOW_MAXLEVEL).WithValue(Entity.MaxLevel));

        protected override void AddFields(LocalisedEmbedBuilder builder)
        {
            builder.AddInlineField(f => f.WithName(SkillText.SHOW_UNLOCKCOST).WithValue(SkillText.SKILLPOINTS, Entity.UnlockCost))
                   .AddField(f => f.WithName(SkillText.SHOW_LEVELS).WithValue(tr =>
                   {
                       var rows = new List<string[]>
                       {
                           new []{
                                tr.GetResource(SkillText.SHOW_TABLE_LVLHEADER),
                                tr.GetResource(SkillText.SHOW_TABLE_COSTHEADER),
                                tr.GetResource(SkillText.SHOW_TABLE_COSTCUMULATIVEHEADER),
                                tr.GetResource(SkillText.SHOW_TABLE_BONUSHEADER),
                                tr.GetResource(SkillText.SHOW_TABLE_EFFICENCYHEADER)
                           }
                       };
                       int cCost = 0;
                       for (int i = 0; i < Entity.Levels.Length; i++)
                       {
                           cCost += Entity.Levels[i].Cost;
                           rows.Add(new ILocalisable<string>[]
                           {
                                new RawString((i+1).ToString()),
                                new RawString("{0}", Entity.Levels[i].Cost),
                                new RawString("{0}", cCost),
                                Entity.Id.AsLocalisableValue(Entity.Levels[i].Bonus),
                                new RawString("{0}%", i == 0 ? 100 : (100 * (Entity.Levels[i].Bonus - Entity.Levels[i-1].Bonus)/Entity.Levels[i-1].Bonus) / Entity.Levels[i].Cost)
                                }.Select(l => l.Localise(tr)).ToArray());
                       }

                       return $"```{rows.ToArray().Tableify()}```";
                   }));
        }

        protected override void AddFields(List<ILocalisable<string>> builder)
        {
            throw new NotImplementedException();
        }
    }
}

using Discord;
using System;
using System.Collections.Generic;
using TitanBot.Contexts;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;

namespace TT2Bot.GameEntity.Base
{
    abstract class GameEntityListEmbedable<TEntity> : GameEntityEmbedable where TEntity : GameEntity
    {
        protected GameEntityService<TEntity> EntityService { get; }

        protected virtual IEnumerable<TEntity> AllEntities => EntityService.GetAll().Result;

        protected virtual ILocalisable<string> Description { get; } = null;

        public GameEntityListEmbedable(ICommandContext context, GameEntityService<TEntity> entityService) : base(context)
        {
            EntityService = entityService;
        }

        protected override LocalisedEmbedBuilder GetBaseEmbed()
            => new LocalisedEmbedBuilder
            {
                Color = System.Drawing.Color.LightBlue.ToDiscord(),
                Footer = new LocalisedFooterBuilder().WithRawIconUrl(BotUser.GetAvatarUrl())
                                                     .WithText(Footer),
                Timestamp = DateTime.Now
            }.WithTitle(Title)
             .WithDescription(Description);

        protected override List<ILocalisable<string>> GetBaseString()
        {
            var builder = base.GetBaseString();
            builder.Add(Description);
            return builder;
        }
    }
}

using Discord;
using System;
using System.Drawing;
using TitanBot.Contexts;
using TitanBot.Formatting;

namespace TT2Bot.GameEntity.Base
{
    abstract class GameEntitySingleEmbedable<TEntity> : GameEntityEmbedable where TEntity : GameEntity
    {
        protected TEntity Entity { get; }

        protected double? From { get; }
        protected double? To { get; }

        protected virtual float MinBrightness { get; } = 0.3f;
        protected virtual float MinSaturation { get; } = 0.5f;

        public GameEntitySingleEmbedable(ICommandContext context, TEntity entity, double? from, double? to) : base(context)
        {
            Entity = entity;
            From = from?.Clamp(0, Entity.MaxLevel ?? int.MaxValue);
            To = to?.Clamp(0, Entity.MaxLevel ?? int.MaxValue);

            if (To == null || To < From)
                (From, To) = (To, From);
        }

        protected override LocalisedEmbedBuilder GetBaseEmbed()
        {
            var builder = base.GetBaseEmbed();
            builder.WithColor((Entity.Image?.AverageColor(MinBrightness, MinSaturation) ?? System.Drawing.Color.LightBlue).ToDiscord())
                   .WithRawThumbnailUrl(Entity.ImageUrl);
            builder.Author?.WithRawIconUrl(Entity.ImageUrl);
            return builder;
        }
    }
}

using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using TitanBot.Contexts;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using TitanBot.Replying;

namespace TT2Bot.GameEntity.Base
{
    abstract class GameEntityEmbedable : IEmbedable
    {
        protected ICommandContext Context { get; }
        protected IUser BotUser => Context.Client.CurrentUser;

        protected virtual ILocalisable<string> Title { get; } = null;
        protected virtual ILocalisable<string> Footer { get; } = null;

        public GameEntityEmbedable(ICommandContext context)
        {
            Context = context;
        }

        protected virtual LocalisedEmbedBuilder GetBaseEmbed()
            => new LocalisedEmbedBuilder
            {
                Footer = new LocalisedFooterBuilder().WithRawIconUrl(BotUser.GetAvatarUrl())
                                                     .WithText(Footer),
                Author = new LocalisedAuthorBuilder().WithName(Title),
                Timestamp = DateTime.Now
            };

        protected virtual List<ILocalisable<string>> GetBaseString()
            => new List<ILocalisable<string>>
            {
                Title,
            };

        protected virtual void AddFooter(List<ILocalisable<string>> builder)
        {
            builder.Add(Footer);
        }

        protected abstract void AddFields(LocalisedEmbedBuilder builder);
        protected abstract void AddFields(List<ILocalisable<string>> builder);

        public virtual ILocalisable<EmbedBuilder> GetEmbed()
        {
            var builder = GetBaseEmbed();

            AddFields(builder);

            return builder;
        }


        public virtual ILocalisable<string> GetString()
        {
            var builder = GetBaseString();

            AddFields(builder);
            AddFooter(builder);

            return LocalisedString.Join("\n", builder.Cast<object>().ToArray());
        }
    }
}

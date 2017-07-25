using Discord;
using System;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Util;
using TT2Bot.Models;

namespace TT2Bot.Commands.Bot
{
    [Description("Allows you to make suggestions and feature requests for me!")]
    class SuggestCommand : TT2Command
    {
        [Call]
        [Usage("Sends a suggestion to my home guild.")]
        public async Task SuggestAsync([Dense]string message)
        {
            var suggestionChannel = Client.GetChannel(TT2Global.BotSuggestChannel) as IMessageChannel;

            if (suggestionChannel == null)
            {
                await ReplyAsync("I could not find where I need to send the suggestion! Please try again later.", ReplyType.Error);
                return;
            }

            var builder = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    Name = $"{Author.Username}#{Author.Discriminator}",
                    IconUrl = Author.GetAvatarUrl()
                },
                Timestamp = DateTime.Now,
                Color = System.Drawing.Color.ForestGreen.ToDiscord()
            }
            .AddField("Suggestion", message)
            .AddInlineField(Guild?.Name ?? Author.Username, Guild?.Id ?? Author.Id)
            .AddInlineField(Channel.Name, Channel.Id);
            await Replier.Reply(suggestionChannel)
                         .WithEmbedable(Embedable.FromEmbed(builder))
                         .SendAsync();
            await ReplyAsync("Suggestion sent", ReplyType.Success);
        }
    }
}

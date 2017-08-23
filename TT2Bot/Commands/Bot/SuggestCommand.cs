using Discord;
using System;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Replying;
using static TT2Bot.TT2Localisation.CommandText;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Bot
{
    [Description(Desc.SUGGEST)]
    class SuggestCommand : TT2Command
    {
        [Call]
        [Usage(Usage.SUGGEST)]
        public async Task SuggestAsync([Dense]string message)
        {
            var suggestionChannel = Client.GetChannel(TT2Global.BotSuggestChannel) as IMessageChannel;

            if (suggestionChannel == null)
            {
                await ReplyAsync(SuggestText.MISSING_CHANNEL, ReplyType.Error);
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
            await Reply(suggestionChannel).WithEmbedable((Embedable)builder)
                                          .SendAsync();
            await ReplyAsync(SuggestText.SENT, ReplyType.Success);
        }
    }
}

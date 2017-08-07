using Discord;
using System;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Replying;
using static TT2Bot.TT2Localisation.Commands;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Bot
{
    [Description(Desc.REPORT)]
    class ReportCommand : TT2Command
    {
        [Call]
        [Usage(Usage.REPORT)]
        public async Task ReportAsync([Dense]string message)
        {
            var bugChannel = Client.GetChannel(TT2Global.BotBugChannel) as IMessageChannel;

            if (bugChannel == null)
            {
                await ReplyAsync(ReportText.MISSING_CHANNEL, ReplyType.Error);
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
                Color = System.Drawing.Color.IndianRed.ToDiscord()
            }
            .AddField("Bug report", message)
            .AddInlineField(Guild?.Name ?? Author.Username, Guild?.Id ?? Author.Id)
            .AddInlineField(Channel.Name, Channel.Id);
            await Reply(bugChannel).WithEmbedable(builder)
                                   .SendAsync();
            await ReplyAsync(ReportText.SENT, ReplyType.Success);
        }
    }
}

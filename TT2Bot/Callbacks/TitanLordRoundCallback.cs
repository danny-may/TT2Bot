using Discord;
using TitanBot.Contexts;
using TitanBot.Formatting;
using TitanBot.Scheduling;
using TT2Bot.Helpers;
using TT2Bot.Models;

namespace TT2Bot.Callbacks
{
    internal class TitanLordRoundCallback : ISchedulerCallback
    {
        public void Complete(ISchedulerContext context, bool wasCancelled)
        {
            if (!wasCancelled)
                Handle(context);
        }

        public bool Handle(ISchedulerContext context)
        {
            if (context.Guild == null)
                return false;

            var data = TitanLordTimerData.FromJson(context.Record.Data);
            var settings = context.GuildSettings.Get<TitanLordSettings>(data.GroupId);

            var messageChannel = context.Client.GetChannel(data.MessageChannelId) as IMessageChannel;

            if (settings.RoundPings && messageChannel != null && context.Author != null)
                context.Replier.Reply(messageChannel, context.Author).WithMessage((RawString)settings.RoundText.Contextualise(settings.CQ,
                                                                                                                   context.Record,
                                                                                                                   context.CycleTime,
                                                                                                                   context.GeneralGuildSetting.DateTimeFormat)).Send();

            return true;
        }
    }
}
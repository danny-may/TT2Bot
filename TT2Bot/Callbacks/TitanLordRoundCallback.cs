using Discord;
using Newtonsoft.Json;
using System;
using TitanBot.Contexts;
using TitanBot.Formatting;
using TitanBot.Scheduling;
using TT2Bot.Helpers;
using TT2Bot.Models;

namespace TT2Bot.Callbacks
{
    class TitanLordRoundCallback : ISchedulerCallback
    {
        public void Complete(ISchedulerContext context, bool wasCancelled)
        {
            if (!wasCancelled)
                Handle(context, context.Record.EndTime);
        }

        public void Handle(ISchedulerContext context, DateTime eventTime)
        {
            if (context.Guild == null)
                return;

            var data = JsonConvert.DeserializeObject<TitanLordTimerData>(context.Record.Data);
            var settings = context.GuildSettings.Get<TitanLordSettings>();

            var messageChannel = context.Client.GetChannel(data.MessageChannelId) as IMessageChannel;

            if (settings.RoundPings && messageChannel != null && context.Author != null)
                context.Replier.Reply(messageChannel, context.Author).WithMessage((RawString)settings.RoundText.Contextualise(settings.CQ,
                                                                                                                   context.Record,
                                                                                                                   eventTime,
                                                                                                                   context.GeneralGuildSetting.DateTimeFormat)).Send();
        }
    }
}

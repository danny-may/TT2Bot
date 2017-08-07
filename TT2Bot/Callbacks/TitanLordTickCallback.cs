using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using TitanBot.Commands;
using TitanBot.Contexts;
using TitanBot.Scheduling;
using TitanBot.Settings;
using TitanBot.Util;
using TT2Bot.Helpers;
using TT2Bot.Models;

namespace TT2Bot.Callbacks
{
    class TitanLordTickCallback : ISchedulerCallback
    {
        public void Handle(ISchedulerContext context, DateTime eventTime)
        {
            if (context.Guild == null)
                return;

            var data = JsonConvert.DeserializeObject<TitanLordTimerData>(context.Record.Data);
            var settings = context.GuildSettings.Get<TitanLordSettings>();

            var messageChannel = context.Client.GetChannel(data.MessageChannelId) as IMessageChannel;
            if (data.MessageId != 0)
            {
                var message = messageChannel?.GetMessageAsync(data.MessageId)?.Result as IUserMessage;
                context.Replier.Modify(message, context.Author).ChangeMessage(settings.TimerText.Contextualise(settings.CQ, 
                                                                                                               context.Record, 
                                                                                                               eventTime, 
                                                                                                               context.GeneralGuildSetting.DateTimeFormat)).Modify();
            }

            foreach (var ping in settings.PrePings)
            {
                var delta = (context.Record.EndTime - eventTime).Add(new TimeSpan(0, 0, -ping));
                if (delta < context.Record.Interval && delta >= new TimeSpan())
                    context.Replier.Reply(messageChannel, context.Author).WithMessage(settings.InXText.Contextualise(settings.CQ,
                                                                                                                     context.Record,
                                                                                                                     eventTime,
                                                                                                                     context.GeneralGuildSetting.DateTimeFormat)).Send();
            }
        }

        public void Complete(ISchedulerContext context, bool wasCancelled)
        {
            if (context.Guild == null)
                return;

            var data = JsonConvert.DeserializeObject<TitanLordTimerData>(context.Record.Data);
            var settings = context.GuildSettings.Get<TitanLordSettings>();

            var messageChannel = context.Client.GetChannel(data.MessageChannelId) as IMessageChannel;
            if (data.MessageId != 0)
            {
                var message = messageChannel?.GetMessageAsync(data.MessageId)?.Result as IUserMessage;
                message?.DeleteAsync().Wait();
            }

            if (!wasCancelled)
                context.Replier.Reply(messageChannel, context.Author).WithMessage(settings.NowText.Contextualise(settings.CQ, 
                                                                                                                 context.Record, 
                                                                                                                 context.Record.EndTime, 
                                                                                                                 context.GeneralGuildSetting.DateTimeFormat)).Send();
        }
    }
}

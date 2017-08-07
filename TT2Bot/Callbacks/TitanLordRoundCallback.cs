using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Contexts;
using TitanBot.Dependencies;
using TitanBot.Scheduling;
using TitanBot.Settings;
using TitanBot.Util;
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

            if (settings.RoundPings)
                context.Replier.Reply(messageChannel, context.Author).WithMessage(settings.RoundText.Contextualise(settings.CQ, 
                                                                                                                   context.Record, 
                                                                                                                   eventTime, 
                                                                                                                   context.GeneralGuildSetting.DateTimeFormat)).Send();
            }
    }
}

using Discord;
using System;
using System.Collections.Concurrent;
using TitanBot.Contexts;
using TitanBot.Formatting;
using TitanBot.Scheduling;
using TitanBot.Storage;
using TT2Bot.Helpers;
using TT2Bot.Models;
using TT2Bot.Models.Database;

namespace TT2Bot.Callbacks
{
    internal class TitanLordTickCallback : ISchedulerCallback
    {
        private static ConcurrentDictionary<ulong, DateTime> _lastCall = new ConcurrentDictionary<ulong, DateTime>();
        public static IDatabase Database { get; set; }

        public bool Handle(ISchedulerContext context)
        {
            if (_lastCall.TryGetValue(context.Record.Id, out var lastTime) && lastTime > DateTime.Now.AddSeconds(-5))
                return true;

            _lastCall.AddOrUpdate(context.Record.Id, DateTime.Now, (u, d) => DateTime.Now > d ? DateTime.Now : d);

            if (context.Guild == null)
                return false;

            var data = TitanLordTimerData.FromJson(context.Record.Data);
            var settings = context.GuildSettings.Get<TitanLordSettings>(data.GroupId);

            var actualTime = context.Record.EndTime.AddTicks(-((context.Record.EndTime - context.CycleTime + context.Delay).Ticks / context.Record.Interval.Ticks) * context.Record.Interval.Ticks);

            var messageChannel = context.Client.GetChannel(data.MessageChannelId) as IMessageChannel;

            if (messageChannel == null || context.Author == null)
                return false;

            if (data.MessageId != 0)
            {
                if (messageChannel?.GetMessageAsync(data.MessageId)?.Result is IUserMessage message)
                    context.Replier.Modify(message, context.Author).ChangeMessage((RawString)settings.TimerText.Contextualise(settings.CQ,
                                                                                                               context.Record,
                                                                                                               actualTime,
                                                                                                               context.GeneralGuildSetting.DateTimeFormat)).Modify();
            }

            foreach (var ping in settings.PrePings)
            {
                var pingTime = context.Record.EndTime.AddTicks(-ping * TimeSpan.TicksPerSecond);
                if (pingTime.Between(context.CycleTime, context.CycleTime + context.Delay + context.Record.Interval))
                {
                    context.Replier.Reply(messageChannel, context.Author).WithMessage((RawString)settings.InXText.Contextualise(settings.CQ,
                                                                                                                     context.Record,
                                                                                                                     pingTime,
                                                                                                                     context.GeneralGuildSetting.DateTimeFormat)).Send();
                }
            }

            return true;
        }

        public void Complete(ISchedulerContext context, bool wasCancelled)
        {
            if (context.Guild == null)
                return;

            var data = TitanLordTimerData.FromJson(context.Record.Data);
            var settings = context.GuildSettings.Get<TitanLordSettings>(data.GroupId);

            var messageChannel = context.Client.GetChannel(data.MessageChannelId) as IMessageChannel;
            if (messageChannel == null || context.Author == null)
                return;

            if (data.MessageId != 0)
            {
                var message = messageChannel?.GetMessageAsync(data.MessageId)?.Result as IUserMessage;
                message?.DeleteAsync().Wait();
            }

            if (!wasCancelled)
            {
                context.Replier.Reply(messageChannel, context.Author).WithMessage((RawString)settings.NowText.Contextualise(settings.CQ,
                                                                                                                 context.Record,
                                                                                                                 context.Record.EndTime,
                                                                                                                 context.GeneralGuildSetting.DateTimeFormat)).Send();
                var history = Database?.AddOrGet(context.Guild.Id, () => new TitanLordHistory()).Result;
                history?.SpawnTimes.Add(context.CycleTime);
                Database?.Upsert(history);
            }
        }
    }
}
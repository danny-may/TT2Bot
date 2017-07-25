﻿using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanBot.Scheduling;
using TitanBot.Settings;
using TitanBot.Util;
using TT2Bot.Helpers;
using TT2Bot.Models;

namespace TT2Bot.Callbacks
{
    class TitanLordTickCallback : ISchedulerCallback
    {
        ISettingManager SettingsManager { get; }
        DiscordSocketClient Client { get; }

        public TitanLordTickCallback(ISettingManager manager, DiscordSocketClient client)
        {
            SettingsManager = manager;
            Client = client;
        }

        public void Handle(ISchedulerRecord record, DateTime eventTime)
        {
            if (record.GuildId == null)
                return;

            var data = JsonConvert.DeserializeObject<TitanLordTimerData>(record.Data);
            var settingContext = SettingsManager.GetContext(record.GuildId.Value);
            var settings = settingContext.Get<TitanLordSettings>();
            var guildSettings = settingContext.Get<GeneralGuildSetting>();

            var messageChannel = Client.GetChannel(data.MessageChannelId) as IMessageChannel;
            if (data.MessageId != 0)
            {
                var message = messageChannel?.GetMessageAsync(data.MessageId)?.Result as IUserMessage;

                message?.ModifySafeAsync(m => m.Content = settings.TimerText.Contextualise(settings.CQ, record, eventTime, guildSettings.DateTimeFormat)).Wait();
            }

            foreach (var ping in settings.PrePings)
            {
                var delta = (record.EndTime - eventTime).Add(new TimeSpan(0, 0, -ping));
                if (delta < record.Interval && delta >= new TimeSpan())
                    messageChannel?.SendMessageSafeAsync(settings.InXText.Contextualise(settings.CQ, record, eventTime, guildSettings.DateTimeFormat)).Wait();
            }
        }

        public void Complete(ISchedulerRecord record, bool wasCancelled)
        {
            if (record.GuildId == null)
                return;

            var data = JsonConvert.DeserializeObject<TitanLordTimerData>(record.Data);
            var settingContext = SettingsManager.GetContext(record.GuildId.Value);
            var settings = settingContext.Get<TitanLordSettings>();
            var guildSettings = settingContext.Get<GeneralGuildSetting>();

            var messageChannel = Client.GetChannel(data.MessageChannelId) as IMessageChannel;
            if (data.MessageId != 0)
            {
                var message = messageChannel?.GetMessageAsync(data.MessageId)?.Result as IUserMessage;
                message?.DeleteAsync().Wait();
            }

            if (!wasCancelled)
                messageChannel?.SendMessageSafeAsync(settings.NowText.Contextualise(settings.CQ, record, record.EndTime, guildSettings.DateTimeFormat)).Wait();
        }
    }
}

using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TitanBot.Scheduling;
using TitanBot.Settings;
using TitanBot.Util;
using TT2Bot.Helpers;
using TT2Bot.Models;

namespace TT2Bot.Callbacks
{
    class TitanLordRoundCallback : ISchedulerCallback
    {
        ISettingsManager SettingsManager { get; }
        DiscordSocketClient Client { get; }

        public TitanLordRoundCallback(ISettingsManager manager, DiscordSocketClient client)
        {
            SettingsManager = manager;
            Client = client;
        }

        public void Complete(ISchedulerRecord record, bool wasCancelled)
        {
            if (!wasCancelled)
                Handle(record, record.EndTime);
        }

        public async void Handle(ISchedulerRecord record, DateTime eventTime)
        {
            if (record.GuildId == null)
                return;
            
            var data = JsonConvert.DeserializeObject<TitanLordTimerData>(record.Data);
            var settings = SettingsManager.GetGroup<TitanLordSettings>(record.GuildId.Value);
            var guildSettings = SettingsManager.GetGroup<GeneralSettings>(record.GuildId.Value);

            var messageChannel = Client.GetChannel(data.MessageChannelId) as IMessageChannel;

            if (settings.RoundPings)
                await (messageChannel?.SendMessageSafeAsync(settings.RoundText.Contextualise(settings.CQ, record, eventTime, guildSettings.DateTimeFormat)) ?? Task.CompletedTask);
        }
    }
}

﻿using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Scheduling;
using TitanBot.Settings;
using TitanBot.Util;
using TT2Bot.Callbacks;
using TT2Bot.Commands.Data;
using TT2Bot.Helpers;
using TT2Bot.Models;

namespace TT2Bot.Commands.Clan
{
    [Description("Used for Titan Lord timers and management")]
    [DefaultPermission(8)]
    [RequireContext(ContextType.Guild)]
    [Alias("TL", "Boss")]
    public class TitanLordCommand : Command
    {
        private TitanLordSettings TitanLordSettings => SettingsManager.GetGroup<TitanLordSettings>(Guild.Id);

        // Since TimeSpans are counting down from when they are created,
        // these arrays have been made from which the timespans will be
        // initialized from during runtime.
        private static readonly int[] BossUptime = { 24, 0, 0 };
        private static readonly int[] BossDelay = { 6, 0, 0};
        private static readonly int[] BossRound = { 1, 0, 0 };
        private static readonly int[] AttackTime = { 0, 0, 30 };
        private static readonly int[] UpdateDelay = { 0, 0, 10 };

        [Call("In")]
        [Usage("Sets a Titan Lord timer running for the given period.")]
        private Task TitanLordInAsync([Dense]TimeSpan time)
        {
            lock (GuildCommandLock)
                LockedTitanLordIn(time).Wait();

            return Task.CompletedTask;
        }

        [Call("Dead")]
        [Usage("Sets a Titan Lord timer running for 6 hours.")]
        private Task TitanLordDead()
            => TitanLordInAsync(BossDelay.ConvertToTimeSpam());

        private async Task LockedTitanLordIn(TimeSpan time)
        {
            if (time > BossDelay.ConvertToTimeSpam())
            {
                await ReplyAsync("You cannot set a timer for longer than 6 hours", ReplyType.Error);
                return;
            }

            (var ticks, var rounds) = CancelCurrent();

            var startTime = DateTime.Now.Add(time).Add(-BossDelay.ConvertToTimeSpam());

            var tlChannel = Client.GetChannel(TitanLordSettings.Channel ?? Channel.Id) as IMessageChannel;

            if (ticks.Length == 0)
            {
                var mostRecent = Scheduler.GetMostRecent<TitanLordTickCallback>(Guild.Id);
                if (mostRecent != null && mostRecent.EndTime > mostRecent.StartTime.Add(BossDelay.ConvertToTimeSpam()))
                    await ReplyAsync(tlChannel, "", embed: NewBoss(time));
            }

            var timer = await ReplyAsync(tlChannel, "Loading timer...\n_If this takes longer than 20s please let Titansmasher know_");

            if (TitanLordSettings.PinTimer)
            {
                try
                {
                    await timer.PinAsync();
                }
                catch { } // ... This needs to be logged
            }

            var data = new TitanLordTimerData
            {
                MessageChannelId = tlChannel.Id,
                MessageId = timer.Id
            };

            StartTimers(startTime, data);

            await ReplyAsync($"Set a timer running for {time}", ReplyType.Success);
        }

        [Call("Now")]
        [Usage("Alerts everyone that the Titan Lord is ready to be killed right now")]
        private async Task TitanLordNowAsync()
        {
            CancelCurrent();
            var startTime = DateTime.Now.Add(-BossDelay.ConvertToTimeSpam());

            var data = new TitanLordTimerData
            {
                MessageChannelId = Client.GetChannel(TitanLordSettings.Channel ?? Channel.Id).Id
            };

            StartTimers(startTime, data);

            await ReplyAsync("Ill let everyone know", ReplyType.Success);
        }

        [Call("When")]
        [Usage("Gets the time until the Titan Lord is ready to be killed")]
        private async Task TitanLordWhenAsync()
        {
            var current = Scheduler.GetMostRecent<TitanLordTickCallback>(Guild.Id);
            if (current == null || current.EndTime < DateTime.Now)
                await ReplyAsync($"There is no currently active Titan Lord timer running", ReplyType.Info);
            else
                await ReplyAsync($"There will be a Titan Lord in {Formatter.Beautify(current.EndTime - DateTime.Now)}", ReplyType.Info);
        }

        [Call("Info")]
        [Usage("Gets information about the clans current level")]
        private Task TitanLordInfoAsync()
            => ReplyAsync("", embed: ClanStatsCommand.StatsBuilder(Formatter, BotUser, TitanLordSettings.CQ, 4000, 500, new[] { 20, 30, 40, 50 }).Build()); // Magic numbers everywhere

        [Call("Stop")]
        [Usage("Stops any currently running timers.")]
        private async Task TitanLordStopAsync()
        {
            CancelCurrent();

            await ReplyAsync("All currently running Titan Lord timers have been stopped", ReplyType.Success);
        }

        private Embed NewBoss(TimeSpan time)
        {
            var settings = SettingsManager.GetGroup<TitanLordSettings>(Guild.Id);
            settings.CQ += 1;
            SettingsManager.SaveGroup(Guild.Id, settings);

            var bossHp = Calculator.TitanLordHp(settings.CQ);
            var clanBonus = Calculator.ClanBonus(settings.CQ);
            var advStart = Calculator.AdvanceStart(settings.CQ); // This isn't used. Was it removed?

            var latestTimer = Scheduler.GetMostRecent<TitanLordTickCallback>(Guild.Id);

            var builder = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    //IconUrl = Res.Emoji.Information_source,
                    Name = "Titan Lord data updated!"
                },
                ThumbnailUrl = "https://cdn.discordapp.com/attachments/275257967937454080/308047011289235456/emoji.png",
                Color = System.Drawing.Color.DarkOrange.ToDiscord(),
                Timestamp = DateTime.Now,
            }.AddField("New Clan Quest", TitanLordSettings.CQ)
             .AddField("New bonus", Formatter.Beautify(clanBonus))
             .AddField("Next Titan Lord HP", Formatter.Beautify(bossHp))
             .AddField("Time to kill", 
                Formatter.Beautify(DateTime.Now.Add(time).Add(-BossDelay.ConvertToTimeSpam()) - latestTimer.EndTime));

            return builder.Build();
        }

        private (ISchedulerRecord[] Ticks, ISchedulerRecord[] Rounds) CancelCurrent()
            => (Scheduler.Complete<TitanLordTickCallback>(Guild.Id, null), Scheduler.Complete<TitanLordRoundCallback>(Guild.Id, null));

        private (ulong TickTimer, ulong RoundTimer) StartTimers(DateTime from, TitanLordTimerData data)
            => (
                Scheduler.Queue<TitanLordTickCallback>(Author.Id, Guild.Id, from, 
                    UpdateDelay.ConvertToTimeSpam(), from.Add(BossDelay.ConvertToTimeSpam()), 
                    JsonConvert.SerializeObject(data)),
                Scheduler.Queue<TitanLordRoundCallback>(Author.Id, Guild.Id, 
                    from.Add(BossDelay.ConvertToTimeSpam() + BossRound.ConvertToTimeSpam() + AttackTime.ConvertToTimeSpam()),  // Uuurgh, I promise it makes sense though
                    BossRound.ConvertToTimeSpam() + AttackTime.ConvertToTimeSpam(),
                    from.Add(BossUptime.ConvertToTimeSpam() + BossDelay.ConvertToTimeSpam()),
                    JsonConvert.SerializeObject(data))
            );
    }
}

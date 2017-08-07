using Discord;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting;
using TitanBot.Replying;
using TitanBot.Scheduling;
using TT2Bot.Callbacks;
using TT2Bot.Commands.Data;
using TT2Bot.Helpers;
using TT2Bot.Models;
using static TT2Bot.TT2Localisation.Commands;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Clan
{
    [Description(Desc.TITANLORD)]
    [DefaultPermission(8)]
    [RequireContext(ContextType.Guild)]
    [Alias("TL", "Boss")]
    class TitanLordCommand : TT2Command
    {
        TitanLordSettings TitanLordSettings => GuildSettings.Get<TitanLordSettings>();

        // Since TimeSpans are counting down from when they are created,
        // these arrays have been made from which the timespans will be
        // initialized from during runtime.
        private static readonly TimeSpan BossUptime = new TimeSpan( 24, 0, 0 );
        private static readonly TimeSpan BossDelay = new TimeSpan( 6, 0, 0);
        private static readonly TimeSpan BossRound = new TimeSpan( 1, 0, 0 );
        private static readonly TimeSpan AttackTime = new TimeSpan( 0, 0, 30 );
        private static readonly TimeSpan UpdateDelay = new TimeSpan( 0, 0, 10 );

        [Call("In")]
        [Usage(Usage.TITANLORD_IN)]
        private Task TitanLordInAsync([Dense]TimeSpan time)
        {
            lock (GuildCommandLock)
                LockedTitanLordIn(time).Wait();

            return Task.CompletedTask;
        }

        [Call("Dead")]
        [Usage(Usage.TITANLORD_DEAD)]
        private Task TitanLordDead()
            => TitanLordInAsync(BossDelay);

        private async Task LockedTitanLordIn(TimeSpan time)
        {
            if (time > BossDelay)
            {
                await ReplyAsync(TitanLordText.TIMER_TOOLONG, ReplyType.Error);
                return;
            }

            (var ticks, var rounds) = CancelCurrent();

            var startTime = DateTime.Now.Add(time).Add(-BossDelay);

            var tlChannel = Client.GetChannel(TitanLordSettings.Channel ?? Channel.Id) as IMessageChannel;

            if (ticks.Length == 0)
            {
                var mostRecent = Scheduler.GetMostRecent<TitanLordTickCallback>(Guild.Id);
                if (mostRecent != null && mostRecent.EndTime > mostRecent.StartTime.Add(BossDelay))
                    await Reply(tlChannel).WithEmbedable(NewBoss(time)).SendAsync();
            }

            var timer = await Reply(tlChannel).WithMessage(TitanLordText.TIMER_LOADING)
                                              .SendAsync();

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

            await ReplyAsync(TitanLordText.TIMER_SET, ReplyType.Success, time);
        }

        [Call("Now")]
        [Usage(Usage.TITANLORD_NOW)]
        private async Task TitanLordNowAsync()
        {
            CancelCurrent();
            var startTime = DateTime.Now.Add(-BossDelay);

            var data = new TitanLordTimerData
            {
                MessageChannelId = Client.GetChannel(TitanLordSettings.Channel ?? Channel.Id).Id
            };

            StartTimers(startTime, data);

            await ReplyAsync(TitanLordText.NOW_SUCCESS, ReplyType.Success);
        }

        [Call("When")]
        [Usage(Usage.TITANLORD_WHEN)]
        private async Task TitanLordWhenAsync()
        {
            var current = Scheduler.GetMostRecent<TitanLordTickCallback>(Guild.Id);
            if (current == null || current.EndTime < DateTime.Now)
                await ReplyAsync(TitanLordText.WHEN_NORUNNING, ReplyType.Info);
            else
                await ReplyAsync(TitanLordText.WHEN_RUNNING, ReplyType.Info, current.EndTime - DateTime.Now);
        }

        [Call("Info")]
        [Usage(Usage.TITANLORD_INFO)]
        private async Task TitanLordInfoAsync()
            => await ReplyAsync(ClanStatsCommand.StatsBuilder(BotUser, TitanLordSettings.CQ, 4000, 500, new int[] { 20, 30, 40, 50 }));

        [Call("Stop")]
        [Usage(Usage.TITANLORD_STOP)]
        private async Task TitanLordStopAsync()
        {
            CancelCurrent();

            await ReplyAsync(TitanLordText.STOP_SUCCESS, ReplyType.Success);
        }

        private LocalisedEmbedBuilder NewBoss(TimeSpan time)
        {
            GuildSettings.Edit<TitanLordSettings>(s => s.CQ++);


            var bossHp = Calculator.TitanLordHp(TitanLordSettings.CQ);
            var clanBonus = Calculator.ClanBonus(TitanLordSettings.CQ);
            var advStart = Calculator.AdvanceStart(TitanLordSettings.CQ);

            var latestTimer = Scheduler.GetMostRecent<TitanLordTickCallback>(Guild.Id);

            var builder = new LocalisedEmbedBuilder
            {
                Color = System.Drawing.Color.DarkOrange.ToDiscord(),
                Timestamp = DateTime.Now,
            }.WithTitle(TitanLordText.NEWBOSS_EMBED_TITLE)
             .WithRawThumbnailUrl("https://cdn.discordapp.com/attachments/275257967937454080/308047011289235456/emoji.png")
             .AddField(f => f.WithName(TitanLordText.NEWBOSS_EMBED_CQ).WithValue(TitanLordSettings.CQ))
             .AddField(f => f.WithName(TitanLordText.NEWBOSS_EMBED_BONUS).WithValue(clanBonus))
             .AddField(f => f.WithName(TitanLordText.NEWBOSS_EMBED_HP).WithValue(bossHp))
             .AddField(f => f.WithName(TitanLordText.NEWBOSS_EMBED_TTK).WithValue(DateTime.Now.Add(time).Add(-BossDelay) - latestTimer.EndTime));

            return builder;
        }

        private (ISchedulerRecord[] Ticks, ISchedulerRecord[] Rounds) CancelCurrent()
            => (Scheduler.Complete<TitanLordTickCallback>(Guild.Id, null), Scheduler.Complete<TitanLordRoundCallback>(Guild.Id, null));

        private (ulong TickTimer, ulong RoundTimer) StartTimers(DateTime from, TitanLordTimerData data)
            => (
                Scheduler.Queue<TitanLordTickCallback>(Author.Id, Guild.Id, from, 
                    UpdateDelay, from.Add(BossDelay),
                    channel: Channel.Id,
                    message: Message.Id,
                    data: JsonConvert.SerializeObject(data)),
                Scheduler.Queue<TitanLordRoundCallback>(Author.Id, Guild.Id, 
                    from.Add(BossDelay + BossRound + AttackTime),  // Uuurgh, I promise it makes sense though
                    BossRound + AttackTime,
                    from.Add(BossUptime + BossDelay),
                    channel: Channel.Id,
                    message: Message.Id,
                    data: JsonConvert.SerializeObject(data))
            );
    }
}

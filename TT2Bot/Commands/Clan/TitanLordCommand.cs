using Discord;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using TitanBot.Replying;
using TitanBot.Scheduling;
using TT2Bot.Callbacks;
using TT2Bot.Commands.Data;
using TT2Bot.GameEntity.Enums;
using TT2Bot.Helpers;
using TT2Bot.Models;
using static TT2Bot.TT2Localisation.CommandText;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Clan
{
    [Description(Desc.TITANLORD)]
    [DefaultPermission(8)]
    [RequireContext(ContextType.Guild)]
    [Alias("TL", "Boss")]
    internal class TitanLordCommand : TT2Command
    {
        private TitanLordSettings TitanLordSettings(int group) => GuildSettings.Get<TitanLordSettings>(group);

        // Since TimeSpans are counting down from when they are created,
        // these arrays have been made from which the timespans will be
        // initialized from during runtime.
        private static readonly TimeSpan BossUptime = new TimeSpan(24, 0, 0);

        private static readonly TimeSpan BossDelay = new TimeSpan(6, 0, 0);
        private static readonly TimeSpan BossRound = new TimeSpan(1, 0, 0);
        private static readonly TimeSpan AttackTime = new TimeSpan(0, 0, 30);
        private static readonly TimeSpan UpdateDelay = new TimeSpan(0, 0, 10);

        [Call("In")]
        [Usage(Usage.TITANLORD_IN)]
        private Task TitanLordInAsync([Dense]TimeSpan time, [CallFlag('g', "Group", Flag.TITANLORD_G)]string group = null)
        {
            lock (GuildCommandLock)
                LockedTitanLordIn(time, group).Wait();

            return Task.CompletedTask;
        }

        [Call("Dead")]
        [Usage(Usage.TITANLORD_DEAD)]
        private Task TitanLordDead([CallFlag('g', "Group", Flag.TITANLORD_G)]string group = null)
            => TitanLordInAsync(BossDelay, group);

        private async Task LockedTitanLordIn(TimeSpan time, string group)
        {
            var groupId = GetGroup(group);

            if (time > BossDelay)
            {
                await ReplyAsync(TitanLordText.TIMER_TOOLONG, ReplyType.Error);
                return;
            }

            ISchedulerRecord[] ticks, rounds;
            try
            {
                (ticks, rounds) = CancelCurrent(groupId);
            }
            catch (Exception)
            {
                // Send error msg to user
                const string errorStr = "Unfortunately a critical, and thankfully very rare, bug has occured with regards to the use of the titanlord command in this discord server. " + 
                    "Unfortunately the only known fix for now is to wait it out. It can take around 24 hours for the bug to go away on its own. " + 
                    "If you have any questions feel free to join our developer server. If you find a way to reproduce the bug, we'd very much like to hear from you!";
                var builderUser = new EmbedBuilder
                {
                    Color = Color.Red,
                    Timestamp = DateTime.Now,
                    Title = "An error occured",
                    Description = errorStr
                };
                await ReplyAsync(builderUser);

                // Send error info to developer channel
                var channel = Client.GetChannel(347122777897041921) as IMessageChannel;
                var builder = new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder
                    {
                        Name = $"{Author?.Username}#{Author?.Discriminator}"
                    },
                    Timestamp = DateTime.Now
                }
                .AddField("Message", $"Failed to cancel current titanlord for **unknown** group with groupId {groupId}")
                .AddField("Guild", $"Failed in guild id {Guild?.Id} with name: {Guild?.Name}");
                
                await Reply(channel).WithEmbedable((Embedable)builder).SendAsync();
                return;
            }

            var startTime = DateTime.Now.Add(time).Add(-BossDelay);

            var tlChannel = Client.GetChannel(TitanLordSettings(groupId).Channel ?? Channel.Id) as IMessageChannel;

            if (ticks.Length == 1)
            {
                var mostRecent = Scheduler.GetMostRecent<TitanLordTickCallback>(Guild.Id, null, GroupCheck(groupId));
                if (mostRecent != null && mostRecent.CompleteTime >= mostRecent.EndTime)
                    await Reply(tlChannel).WithEmbedable(NewBoss(mostRecent, time, TitanLordSettings(groupId))).SendAsync();
            }

            var timer = await Reply(tlChannel).WithMessage((LocalisedString)TitanLordText.TIMER_LOADING)
                                              .SendAsync();

            if (TitanLordSettings(groupId).PinTimer)
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
                MessageId = timer.Id,
                GroupId = groupId
            };

            StartTimers(startTime, data);

            await ReplyAsync(TitanLordText.TIMER_SET, ReplyType.Success, time);
        }

        [Call("Now")]
        [Usage(Usage.TITANLORD_NOW)]
        private async Task TitanLordNowAsync([CallFlag('g', "Group", Flag.TITANLORD_G)]string group = null)
        {
            var groupId = GetGroup(group);

            CancelCurrent(groupId);
            var startTime = DateTime.Now.Add(-BossDelay);

            var data = new TitanLordTimerData
            {
                MessageChannelId = Client.GetChannel(TitanLordSettings(groupId).Channel ?? Channel.Id).Id,
                GroupId = groupId
            };

            StartTimers(startTime, data);

            await ReplyAsync(TitanLordText.NOW_SUCCESS, ReplyType.Success);
        }

        [Call("When")]
        [Usage(Usage.TITANLORD_WHEN)]
        private async Task TitanLordWhenAsync([CallFlag('g', "Group", Flag.TITANLORD_G)]string group = null)
        {
            var groupId = GetGroup(group);
            var current = Scheduler.GetMostRecent<TitanLordTickCallback>(Guild.Id, null, GroupCheck(groupId));
            if (current == null || current.EndTime < DateTime.Now)
                await ReplyAsync(TitanLordText.WHEN_NORUNNING, ReplyType.Info);
            else
                await ReplyAsync(TitanLordText.WHEN_RUNNING, ReplyType.Info, current.EndTime - DateTime.Now);
        }

        [Call("Info")]
        [Usage(Usage.TITANLORD_INFO)]
        private async Task TitanLordInfoAsync([CallFlag('g', "Group", Flag.TITANLORD_G)]string group = null)
            => await ReplyAsync(ClanStatsCommand.StatsBuilder(BotUser, TitanLordSettings(GetGroup(group)).CQ));

        [Call("Stop")]
        [Usage(Usage.TITANLORD_STOP)]
        private async Task TitanLordStopAsync([CallFlag('g', "Group", Flag.TITANLORD_G)]string group = null)
        {
            CancelCurrent(GetGroup(group));

            if (SettingsManager.GetGroups(Guild).Count == 0)
                await ReplyAsync(TitanLordText.STOP_SUCCESS, ReplyType.Success);
            else
                await ReplyAsync(TitanLordText.STOP_SUCCESS_GROUP, ReplyType.Success, group);
        }

        private Embedable NewBoss(ISchedulerRecord latestTimer, TimeSpan time, TitanLordSettings settings)
        {
            GuildSettings.Edit<TitanLordSettings>(s => s.CQ++);

            var bossHp = Calculator.TitanLordHp(settings.CQ);
            var clanBonus = Calculator.ClanBonus(settings.CQ);
            var advStart = Calculator.AdvanceStart(settings.CQ);

            var builder = new LocalisedEmbedBuilder
            {
                Color = System.Drawing.Color.DarkOrange.ToDiscord(),
                Timestamp = DateTime.Now,
            }.WithTitle(TitanLordText.NEWBOSS_EMBED_TITLE)
             .WithRawThumbnailUrl("https://cdn.discordapp.com/attachments/275257967937454080/308047011289235456/emoji.png")
             .AddField(f => f.WithName(TitanLordText.NEWBOSS_EMBED_CQ).WithValue(settings.CQ))
             .AddField(f => f.WithName(TitanLordText.NEWBOSS_EMBED_ADVANCEDSTART).WithValue(BonusType.ClanDamage.ToLocalisable(advStart)))
             .AddField(f => f.WithName(TitanLordText.NEWBOSS_EMBED_BONUS).WithValue(BonusType.ClanDamage.ToLocalisable(clanBonus)))
             .AddField(f => f.WithName(TitanLordText.NEWBOSS_EMBED_HP).WithValue(bossHp))
             .AddField(f => f.WithName(TitanLordText.NEWBOSS_EMBED_TTK).WithValue(DateTime.Now.Add(time).Add(-BossDelay) - latestTimer.EndTime));

            return builder;
        }

        private (ISchedulerRecord[] Ticks, ISchedulerRecord[] Rounds) CancelCurrent(int group)
            => (Scheduler.Cancel<TitanLordTickCallback>(Guild.Id, null, GroupCheck(group)),
                Scheduler.Cancel<TitanLordRoundCallback>(Guild.Id, null, GroupCheck(group)));

        private Func<string, bool> GroupCheck(int groupId)
            => s => TitanLordTimerData.FromJson(s).GroupId == groupId;

        private int GetGroup(string group)
        {
            if (group == null)
                return 0;
            var groups = SettingsManager.GetGroups(Guild);
            return groups.Select(g => new { Id = g.Key, Values = g.Value }).FirstOrDefault(g => g.Values.Contains(group.ToLower()))?.Id ?? 0;
        }

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
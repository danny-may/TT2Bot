using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting;
using TT2Bot.Helpers;
using TT2Bot.Models;
using static TT2Bot.TT2Localisation.Commands;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Data
{
    [Description(Desc.CLANSTATS)]
    class ClanStatsCommand : Command
    {
        internal static LocalisedEmbedBuilder StatsBuilder(SocketSelfUser me, int clanLevel, int? avgMS = null, int? tpCQ = null, int[] attackers = null)
        {
            attackers = (attackers?.Count() ?? 0) == 0 ? new int[] { 20, 30, 40, 50 } : attackers;
            var averageMS = avgMS ?? 4000;
            var tapsPerCq = tpCQ ?? 600;

            var absLevel = Math.Abs(clanLevel);

            var currentBonus = Calculator.ClanBonus(absLevel);
            var nextBonus = Calculator.ClanBonus(absLevel + 1);
            var nextTitanLordHp = Calculator.TitanLordHp(absLevel);
            var advanceStart = Calculator.AdvanceStart(absLevel);

            var builder = new LocalisedEmbedBuilder
            {
                Footer = new EmbedFooterBuilder
                {
                    IconUrl = me.GetAvatarUrl(),
                    Text = me.Username + "#" + me.Discriminator
                },
                Color = System.Drawing.Color.DarkOrange.ToDiscord(),
                Timestamp = DateTime.Now
            };
            builder.WithTitle(ClanStatsText.TITLE, clanLevel)
                   .AddInlineField(f => f.WithName(ClanStatsText.FIELD_CQ).WithValue(absLevel))
                   .AddInlineField(f => f.WithName(ClanStatsText.FIELD_BONUS_CURRENT).WithValue(BonusType.ClanDamage.LocaliseValue(currentBonus)))
                   .AddInlineField(f => f.WithName(ClanStatsText.FIELD_BONUS_NEXT).WithValue(BonusType.ClanDamage.LocaliseValue(nextBonus)))
                   .AddInlineField(f => f.WithName(ClanStatsText.FIELD_HP).WithValue(nextTitanLordHp))
                   .AddInlineField(f => f.WithName(ClanStatsText.FIELD_ADVSTART).WithValue(advanceStart));
            LocalisedString[] rows = attackers.Select(num =>
                                    {
                                        var dmgpp = nextTitanLordHp / num;
                                        var attacks = Calculator.AttacksNeeded(absLevel, num, averageMS, tapsPerCq);
                                        var dia = Calculator.TotalAttackCost(attacks);
                                        return new LocalisedString(ClanStatsText.FIELD_ATTACKERS_ROW, num, dmgpp, attacks, dia);
                                    }).ToArray();
            builder.AddField(f => f.WithName(ClanStatsText.FIELD_ATTACKERS, averageMS, tapsPerCq)
                                   .WithValues("\n", rows));

            return builder;
        }

        [Call]
        [Usage(Usage.CLANSTATS)]
        async Task ShowStatsAsync(int clanLevel,
            [CallFlag('s', "stage", Flag.CLANSTATS_S)] int? averageMs = null,
            [CallFlag('t', "taps", Flag.CLANSTATS_T)] int? tapsPerCQ = null,
            [CallFlag('a', "attackers", Flag.CLANSTATS_A)]int[] attackers = null)
        {
            await ReplyAsync(StatsBuilder(BotUser, clanLevel, averageMs, tapsPerCQ, attackers));
        }
    }
}

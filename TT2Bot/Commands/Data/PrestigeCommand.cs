using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting;
using TT2Bot.Helpers;
using static TT2Bot.TT2Localisation.Commands;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Data
{
    [Description(Desc.PRESTIGE)]
    class PrestigeCommand : Command
    {
        [Call]
        [Usage(Usage.PRESTIGE)]        
        async Task PrestigeStatsAsync(int stage,
            [CallFlag('b', "bos", Flag.PRESTIGE_B)] int bosLevel = -1,
            [CallFlag('c', "clan", Flag.PRESTIGE_C)] int clanLevel = -1,
            [CallFlag('i', "ip", Flag.PRESTIGE_I)] int ipLevel = -1)
        {

            ipLevel = Math.Min(20, ipLevel);
            var startingStage = (int)Math.Max(1, stage * Calculator.AdvanceStart(clanLevel.Clamp(0, int.MaxValue)));
            var totalRelics = Calculator.RelicsEarned(stage, bosLevel.Clamp(0, int.MaxValue));
            var baseRelics = Calculator.RelicsEarned(stage, 0);
            var enemiesToKill = Enumerable.Range(startingStage, stage - startingStage).Sum(s => Calculator.TitansOnStage(s, ipLevel.Clamp(0, int.MaxValue)));
            var timeTaken = Calculator.RunTime(startingStage, stage, ipLevel.Clamp(0, int.MaxValue), 1);
            var timeTakenSplash = Calculator.RunTime(startingStage, stage, ipLevel.Clamp(0, int.MaxValue), 4);

            List<LocalisedString> description = new List<LocalisedString>();
            if (bosLevel > 0)
                description.Add(new LocalisedString(PrestigeText.DESCRIPTION_BOS, bosLevel));
            if (clanLevel > 0)
                description.Add(new LocalisedString(PrestigeText.DESCRIPTION_CLAN, clanLevel));
            if (ipLevel > 0)
                description.Add(new LocalisedString(PrestigeText.DESCRIPTION_IP, ipLevel));

            var builder = new LocalisedEmbedBuilder
            {
                Description = LocalisedString.Join("\n", description.ToArray()),
                Color = System.Drawing.Color.Gold.ToDiscord(),
                Footer = new LocalisedFooterBuilder().WithText(PrestigeText.FOOTER).WithRawIconUrl(BotUser.GetAvatarUrl())
            }
            .WithTitle(PrestigeText.TITLE, stage)
            .AddInlineField(f => f.WithName(PrestigeText.FIELD_STARTINGSTAGE).WithValue(startingStage))
            .AddInlineField(f => f.WithName(PrestigeText.FIELD_RELICS).WithValue(PrestigeText.FIELD_RELICS_VALUE, baseRelics, totalRelics - baseRelics, totalRelics))
            .AddInlineField(f => f.WithName(PrestigeText.FIELD_ENEMIES).WithValue(PrestigeText.FIELD_ENEMIES_VALUE, enemiesToKill, stage - startingStage))
            .AddInlineField(f => f.WithName(PrestigeText.FIELD_TIME).WithValue(PrestigeText.FIELD_TIME_VALUE, timeTaken, timeTakenSplash));

            await ReplyAsync(builder);
        }
    }
}

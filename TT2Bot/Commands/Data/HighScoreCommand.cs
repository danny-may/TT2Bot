using System;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Commands;
using TT2Bot.Services;
using static TT2Bot.TT2Localisation.Commands;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Data
{
    [Description(Desc.HIGHSCORE)]
    [Alias("HS")]
    class HighScoreCommand : Command
    {
        private TT2DataService DataService { get; }
        protected override string DelayMessage { get; } = DELAYMESSAGE_DATA;

        public HighScoreCommand(TT2DataService dataService)
        {
            DataService = dataService;
        }

        [Call]
        [Usage(Usage.HIGHSCORE)]
        private async Task ShowSheetAsync(int? from = null, int? to = null)
        {
            var sheet = await DataService.GetHighScores();

            if (from == null && to == null)
            {
                from = 0;
                to = 30;
            }
            else if (to == null)
                to = from;

            var start = from.Value;
            var end = to.Value;

            if (start > end)
                (start, end) = (end, start);

            var places = Enumerable.Range(start, end - start + 1)
                                   .Select(i => sheet.Users.FirstOrDefault(u => u.Ranking == i))
                                   .Where(p => p != null)
                                   .ToList();

            if (places.Count == 0)
            {
                await ReplyAsync($"There were no users for the range {from} - {to}!");
                return;
            }

            var data = places.Select(p => new string[] { p.Ranking.ToString(), p.TotalRelics, p.UserName, p.ClanName }).ToArray();
            data = new string[][] { new string[] { "##", " Relics", " Username", " Clan" } }.Concat(data).ToArray();

            await ReplyAsync($"Here are the currently known users in rank {start} - {end}:\n```md\n{data.Tableify("[{0}]", "{0}  ")}```");

            return;
        }
    }
}

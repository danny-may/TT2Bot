using Discord;
using System;
using System.Threading.Tasks;
using TitanBot.Commands;
using TT2Bot.Models;

namespace TT2Bot.Commands.Games
{
    [Hidden]
    class EeveeCommand : Command
    {
        private float criticalChance = 0.01f;
        private float eeveeCriticalChance = 0.5f;

        [Call]
        async Task EeveeAsync()
        {
            var rand = new Random((int)(Message.Id % int.MaxValue));
            var rocks = rand.Next(5);
            var critChance = Author.Id == 255141315023601666 ? eeveeCriticalChance : criticalChance;
            var crit = rand.NextDouble() < critChance;
            var capture = rocks == 4;

            var message = await ReplyAsync("You throw a pokeball at the wild eevee!");
            await Task.Delay(500);

            if (rocks == 0)
            {
                await GotAway(message);
                return;
            }
            await Append(message, "\n\nIt rocks once...");
            if (rocks == 1)
            {
                await GotAway(message);
                return;
            }
            else if (crit)
            {
                var player = await Database.AddOrGet(Author.Id, () => new PlayerData { Id = Author.Id });
                player.Eevees++;
                player.CritEevees++;
                await Database.Upsert(player);
                await Append(message, $"\n\nCritical capture! You now have {player.Eevees} eevees caught and {player.CritEevees} critical captures!");
                return;
            }
            await Append(message, "\ntwice...");
            if (rocks == 2)
            {
                await GotAway(message);
                return;
            }
            await Append(message, "\nthree times...");
            if (!capture)
            {
                await GotAway(message);
                return;
            }
            else
            {
                var player = await Database.AddOrGet(Author.Id, () => new PlayerData { Id = Author.Id });
                player.Eevees++;
                await Database.Upsert(player);
                await Append(message, $"\n\nYou caught the wild eevee! You now have {player.Eevees} eevees caught!");
            }
        }

        private async Task GotAway(IUserMessage message)
        {
            await Append(message,"\n\nBut it got away...");
        }

        private async Task Append(IUserMessage message, string text)
        {
            await message.ModifyAsync(m => m.Content = message.Content + text);
            await Task.Delay(1500);
        }
    }
}

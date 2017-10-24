using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting;

namespace TT2Bot.Commands.General
{
    [Hidden]
    [RequireGuild(271733361863557129)]
    internal class PlebCommand : Command
    {
        [Call]
        private async Task PlebAsync()
            => await ReplyAsync(new RawString("Relajarse is a pleb >:D"));
    }
}
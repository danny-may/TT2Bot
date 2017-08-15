using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.DiscordHandlers;
using TitanBot.Logging;

namespace TT2Bot.Handlers
{
    class MessageHandler : DiscordHandlerBase
    {
        private readonly ICommandService CommandService;

        public MessageHandler(DiscordSocketClient client, ILogger logger, ICommandService commandService) : base(client, logger)
        {
            CommandService = commandService;

            Client.MessageReceived += MessageRecieved;
        }

        private async Task MessageRecieved(SocketMessage msg)
        {
            if (!(msg is SocketUserMessage message))
                return;
            for (int i = 0; i < message.Content.Length - 1; i++)
            {
                if (message.Content.Substring(i, 2) != "👋")
                    continue;
                var tone = "";
                if (message.Content.Length >= i + 4)
                {
                    tone = message.Content.Substring(i + 2, 2);
                    if (!"🏿🏼🏽🏾🏻".Contains(tone))
                        tone = "";
                }
                await message.AddReactionAsync(new Emoji("👋" + tone));
            }
        }
    }
}

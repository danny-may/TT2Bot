using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Replying;
using TT2Bot.Models;
using static TT2Bot.TT2Localisation.Commands;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Data
{
    [Description(Desc.CLAIM)]
    class ClaimCommand : Command
    {

        [Call]
        [Usage(Usage.CLAIM)]
        async Task ClaimCodeAsync(string supportCode)
        {
            if (supportCode == null)
            {
                await ReplyAsync(ClaimText.MISSING_SUPPORTCODE, ReplyType.Error);
                return;
            }

            var supportCodeOwned = await Database.FindOne<PlayerData>(p => p.PlayerCode == supportCode);
            if (supportCode.Length > 7)
                await ReplyAsync(ClaimText.INVALID_SUPPORTCODE, ReplyType.Error);
            else if (supportCodeOwned != null)
                await ReplyAsync(ClaimText.UNAVAILABLE_SUPPORTCODE, ReplyType.Error);
            else
            {
                var current = await Database.FindOne<PlayerData>(p => p.Id == Author.Id);
                if (current != null && supportCode.ToLower() == current.PlayerCode.ToLower())
                    await ReplyAsync(ClaimText.SUPPORTCODE_OWNED, ReplyType.Success);
                else
                {
                    var newUser = current ?? new PlayerData { Id = Author.Id };
                    newUser.PlayerCode = supportCode.ToLower();
                    await Database.Upsert(newUser);
                    if (current == null)
                        await ReplyAsync(ClaimText.SUPPORTCODE_NEW, ReplyType.Success, supportCode);
                    else
                        await ReplyAsync(ClaimText.SUPPORTCODE_UPDATED, ReplyType.Success, supportCode, current.PlayerCode);
                }
            }
        }
    }
}

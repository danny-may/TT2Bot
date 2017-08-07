using Discord;
using System;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting;
using TitanBot.Replying;
using TT2Bot.Models;
using static TT2Bot.TT2Localisation.Commands;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Clan
{
    [Description(Desc.EXCUSE)]
    class ExcuseCommand : TT2Command
    {
        [Call]
        [Usage(Usage.EXCUSE)]
        public async Task ExcuseUserAsync(IUser user = null,
            [CallFlag('i', "id", Flag.EXCUSE_I)]ulong? excuseId = null)
        {
            user = user ?? Author;

            if (user?.Id == BotUser.Id)
            {
                await ReplyAsync(ExcuseText.SELF);
                return;
            }

            var excuse = GetExcuse(excuseId, true) ?? GetExcuse(null, true);

            var submitter = Client.GetUser(excuse.CreatorId);

            var builder = new LocalisedEmbedBuilder
            {
                Author = LocalisedAuthorBuilder.FromUser(submitter),
                Footer = new LocalisedFooterBuilder().WithText(ExcuseText.ID, excuse.Id),
                Timestamp = DateTime.Now.AddSeconds(-new Random().Next(120, 3600)),
                Color = System.Drawing.Color.Green.ToDiscord(),
            }.WithRawDescription(excuse.ExcuseText);
            await ReplyAsync(ExcuseText.REASON, builder, user.Id);
        }

        [Call("Add")]
        [Usage(Usage.EXCUSE_ADD)]
        public async Task AddExcuseAsync([Dense]string text)
        {
            var excuse = new Models.Excuse
            {
                CreatorId = Author.Id,
                ExcuseText = text,
                SubmissionTime = DateTime.Now
            };
            await Database.Insert(excuse);

            await ReplyAsync(ExcuseText.ADD_SUCCESS, ReplyType.Success, excuse.Id);
        }

        [Call("Remove")]
        [Usage(Usage.EXCUSE_REMOVE)]
        public async Task RemoveExcuseAsync(ulong id)
        {
            var excuse = GetExcuse(id, false);

            if (excuse == null || excuse.Id == 0)
            {
                await ReplyAsync(ExcuseText.REMOVE_NOTFOUND, ReplyType.Error);
                return;
            }

            if (!Bot.Owners.Contains(Author.Id) && Author.Id != excuse.CreatorId)
            {
                await ReplyAsync(ExcuseText.REMOVE_NOTOWN, ReplyType.Error);
                return;
            }

            excuse.Removed = true;

            await Database.Upsert(excuse);

            await ReplyAsync(ExcuseText.REMOVE_SUCCESS, ReplyType.Success);
        }

        private Excuse GetExcuse(ulong? id, bool allowRandom)
        {
            if (id.HasValue)
                return Database.FindById<Excuse>(id.Value).Result;
            var all = Database.Find<Excuse>(e => true).Result.ToArray();
            if (all.Length == 0)
                return new Excuse { CreatorId = Author.Id, Id = 0, ExcuseText = "Im uninteresting and havent made any excuses yet", SubmissionTime = DateTime.MinValue };
            return all[new Random().Next(all.Length)];
        }
    }

}

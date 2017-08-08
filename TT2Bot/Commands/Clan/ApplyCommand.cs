using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using TitanBot.Models;
using TitanBot.Replying;
using TT2Bot.Models;
using static TT2Bot.TT2Localisation.Commands;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Clan
{
    [Description(Desc.APPLY)]
    [DefaultPermission(8)]
    [Alias("R", "Reg", "Register")]
    class ApplyCommand : TT2Command
    {
        [Call]
        [Usage(Usage.APPLY_REGISTER_GUILD)]
        [DefaultPermission(0)]
        [RequireContext(ContextType.Guild)]
        private async Task RegisterGuildAsync(int maxStage, [Dense]string message,
            [CallFlag('g', "global", Flag.APPLY_G)]bool isGlobal = false,
            [CallFlag('i', "images", Flag.APPLY_I)]Uri[] images = null,
            [CallFlag('r', "relics", Flag.APPLY_R)]double relics = -1,
            [CallFlag('a', "attacks", Flag.APPLY_A)]int attacks = -1,
            [CallFlag('t', "taps", Flag.APPLY_T)]int taps = -1)
        {
            if (isGlobal)
                await RegisterAsync(maxStage, message, null, images, relics, attacks, taps);
            else
                await RegisterAsync(maxStage, message, Guild.Id, images, relics, attacks, taps);
        }

        [Call]
        [Usage(Usage.APPLY_REGISTER_GLOBAL)]
        [RequireContext(ContextType.DM | ContextType.Group)]
        private Task RegisterGlobalAsync(int maxStage, [Dense]string message,
            [CallFlag('i', "images", Flag.APPLY_I)]Uri[] images = null,
            [CallFlag('r', "relics", Flag.APPLY_R)]double relics = -1,
            [CallFlag('a', "attacks", Flag.APPLY_A)]int attacks = -1,
            [CallFlag('t', "taps", Flag.APPLY_T)]int taps = -1)
            => RegisterAsync(maxStage, message, null, images, relics, attacks, taps);

        private async Task RegisterAsync(int maxStage, string message, ulong? guildId, Uri[] images, double relics, int attacks, int taps)
        {
            var current = GetRegistrations(guildId, Author.Id, !guildId.HasValue).OrderByDescending(r => r.EditTime).FirstOrDefault();
            var playerData = GetPlayers(Author.Id).FirstOrDefault();
            var isNew = current == null;

            current = current ?? new Registration
            {
                UserId = Author.Id,
                GuildId = guildId
            };
            playerData = playerData ?? new PlayerData
            {
                Id = Author.Id
            };

            current.EditTime = DateTime.Now;
            current.Message = message;
            current.Images = images;

            if (relics != -1)
                playerData.Relics = relics.Clamp(0, 2e15);
            if (attacks != -1)
                playerData.AttacksPerWeek = attacks.Clamp(0, 300);
            if (taps != -1)
                playerData.TapsPerCQ = taps.Clamp(0, 600);
            playerData.MaxStage = maxStage;
            
            await Database.Upsert(current);
            await Database.Upsert(playerData);

            if (isNew && guildId == null)
                await ReplyAsync(ApplyText.APPLICATION_SUCCESSFUL_GLOBAL, ReplyType.Success);
            else if (isNew)
                await ReplyAsync(ApplyText.APPLICATION_SUCCESSFUL_GUILD, ReplyType.Success);
            else
                await ReplyAsync(ApplyText.APPLICATION_UPDATED, ReplyType.Success);
        }

        [Call("View")]
        [Usage(Usage.APPLY_VIEW_GUILD)]
        [RequireContext(ContextType.Guild)]
        [DefaultPermission(0)]
        Task ViewGuildRegistrationAsync([CallFlag('g', "global", Flag.APPLY_G)]bool isGlobal = false)
            => ViewRegistrationAsync(Guild.Id, Author, isGlobal);

        [Call("View")]
        [Usage(Usage.APPLY_VIEW_GLOBAL)]
        [RequireContext(ContextType.DM | ContextType.Group)]
        Task ViewGlobalRegistrationAsync()
            => ViewRegistrationAsync(null, Author, true);

        [Call("View")]
        [Usage(Usage.APPLY_VIEW_USER)]
        [RequireContext(ContextType.Guild)]
        Task ViewGuildRegistrationAsync(IUser user,
            [CallFlag('g', "global", Flag.APPLY_G)]bool isGlobal = false)
            => ViewRegistrationAsync(Guild.Id, user, isGlobal);

        async Task ViewRegistrationAsync(ulong? guildId, IUser user, bool global)
        {
            Registration current = GetRegistrations(Guild.Id, Author.Id, global).OrderByDescending(r => r.EditTime).FirstOrDefault();
            PlayerData player = GetPlayers(Author.Id).FirstOrDefault();

            if (current == null)
            {
                if (user.Id == Author.Id)
                    await ReplyAsync(ApplyText.VIEW_NOTREGISTERED, ReplyType.Error);
                else if (global)
                    await ReplyAsync(ApplyText.VIEW_GLOBAL_NOTREGISTERED, ReplyType.Error);
                else
                    await ReplyAsync(ApplyText.VIEW_GUILD_NOTREGISTERED, ReplyType.Error);
                return;
            }

            var builder = new LocalisedEmbedBuilder
            {
                Author = LocalisedAuthorBuilder.FromUser(Author),
                Color = System.Drawing.Color.SkyBlue.ToDiscord(),
                Description = (RawString)current.Message,
                Footer = new LocalisedFooterBuilder().WithText(current.GuildId == null ? ApplyText.VIEW_FOOTER_GLOBAL : ApplyText.VIEW_FOOTER_GUILD, current.ApplyTime, current.EditTime)
            }.WithTitle(ApplyText.VIEW_TITLE)
             .AddInlineField(f => f.WithName(MAXSTAGE).WithValue(player.MaxStage))
             .AddInlineField(f => f.WithName(RELICS).WithValue(player.Relics))
             .AddInlineField(f => f.WithName(ATTACKSPERWEEK).WithValue(player.AttacksPerWeek))
             .AddInlineField(f => f.WithName(TAPSPERCQ).WithValue(player.TapsPerCQ));
            if (current.Images == null)
                builder.AddField(f => f.WithName(IMAGES).WithValue(NONE));
            else
                builder.AddField(f => f.WithName(IMAGES).WithValues("\n", current.Images.Select(i => i.AbsoluteUri)));

            await ReplyAsync(builder);
        }

        [Call("Cancel")]
        [Usage(Usage.APPLY_CANCEL_GUILD)]
        [RequireContext(ContextType.Guild)]
        [DefaultPermission(0)]
        Task RemoveGuildRegistrationAsync()
            => RemoveRegistrationAsync(Guild.Id, Author);

        [Call("Cancel")]
        [Usage(Usage.APPLY_CANCEL_GLOBAL)]
        [RequireContext(ContextType.DM | ContextType.Group)]
        Task RemoveGlobalRegistrationAsync()
            => RemoveRegistrationAsync(null, Author);

        [Call("Remove")]
        [Usage(Usage.APPLY_CANCEL_USER)]
        [RequireContext(ContextType.Guild)]
        Task RemoveGuildRegistrationAsync(IUser user)
            => RemoveRegistrationAsync(Guild.Id, user);

        async Task RemoveRegistrationAsync(ulong? guildID, IUser user)
        {
            if (guildID == null && user != Author)
            {
                await ReplyAsync(ApplyText.REMOVE_GLOBAL_NOTALLOWED, ReplyType.Error);
                return;
            }
            await Database.Delete<Registration>(r => r.UserId == user.Id && r.GuildId == guildID);

            if (user.Id == Author.Id)
                await ReplyAsync(guildID == null ? ApplyText.REMOVE_GLOBAL_SUCCESS : ApplyText.REMOVE_GUILD_SUCCESS, ReplyType.Success);
            else
                await ReplyAsync(ApplyText.REMOVE_OTHER_SUCCESS, ReplyType.Success, user.Username);
        }

        [Call("Ignore")]
        [Usage(Usage.APPLY_IGNORE)]
        [RequireContext(ContextType.Guild)]
        async Task IgnoreUser(IUser user, bool ignore = true)
        {
            GuildSettings.Edit<RegistrationSettings>(s =>
            {
                if (ignore)
                    s.IgnoreList.Add(user.Id);
                else
                    s.IgnoreList.Remove(user.Id);
            });

            if (ignore)
                await ReplyAsync(ApplyText.IGNORE_IGNORED, ReplyType.Success);
            else
                await ReplyAsync(ApplyText.IGNORE_UNIGNORED, ReplyType.Success);
        }

        [Call("List")]
        [Usage(Usage.APPLY_LIST)]
        [RequireContext(ContextType.Guild)]
        async Task ListRegistrationsAsync(int? start = null, int? end = null,
            [CallFlag('g', "global", Flag.APPLY_G)]bool isGlobal = false)
        {
            var from = start ?? 0;
            var to = end ?? from + 20;
            if (from > to)
                (from, to) = (to, from);
            to.Clamp(from, from + 30);

            var applications = GetRegistrations(Guild.Id, null, isGlobal);
            var players = GetPlayers(applications.Select(a => a.UserId).ToArray());

            var table = MakeTable(players, applications, new Range<int> { From = from, To = to });

            if (table.Length <= 1)
                await ReplyAsync(ApplyText.LIST_NONE, ReplyType.Error);
            else
                await ReplyAsync(ApplyText.LIST_FORMAT, new DynamicString(tr => table.Select(r => r.Localise(tr).Split(',')).ToArray().Tableify()));
        }

        private ILocalisable<string>[] MakeTable(IEnumerable<PlayerData> players, IEnumerable<Registration> applications, Range<int> range)
        {
            var paired = applications.Join(players, a => a.UserId, p => p.Id, (a, p) => (Application: a, Player: p));

            var ignore = GuildSettings.Get<RegistrationSettings>().IgnoreList;
            paired = paired.Where(a => !(ignore.Contains(a.Application.UserId) && a.Application.GuildId == null))
                                       .OrderByDescending(a => a.Player.MaxStage)
                                       .ThenByDescending(a => Math.Round(Math.Sqrt(a.Player.Relics), 0))
                                       .ThenByDescending(a => a.Player.AttacksPerWeek)
                                       .ThenBy(a => a.Application.EditTime)
                                       .Skip(range.From);

            var table = new List<LocalisedString> { };
            table.Add((LocalisedString)ApplyText.LIST_TABLEHEADERS);
            var pos = range.From + 1;
            foreach (var app in paired)
            {
                var user = Client.GetUser(app.Application.UserId);
                if (user == null)
                    continue;
                table.Add(new LocalisedString(ApplyText.LIST_ROW, pos++.ToString(),
                                                                  user,
                                                                  user.Id,
                                                                  app.Player.MaxStage,
                                                                  app.Application.Images?.Length ?? 0,
                                                                  app.Player.Relics,
                                                                  app.Player.AttacksPerWeek,
                                                                  app.Player.TapsPerCQ,
                                                                  (DateTime.Now - app.Application.EditTime).Days,
                                                                  app.Application.GuildId == null ? "-g" : ""));
                if (table.Count == range.To - range.From)
                    break;
            }

            return table.ToArray();
        }

        [Call("Clear")]
        [Usage(Usage.APPLY_CLEAR)]
        [RequireContext(ContextType.Guild)]
        async Task ClearRegistrationsAsync()
        {
            await Database.Delete<Registration>(r => r.GuildId == Guild.Id);
            await ReplyAsync(ApplyText.CLEAR_SUCCESS, ReplyType.Success);
        }

        private IEnumerable<Registration> GetRegistrations(ulong? guildId, ulong? userId, bool includeGlobal)
        {
            if (userId == null && includeGlobal)
                return Database.Find<Registration>(r => r.GuildId == null || r.GuildId == guildId).Result;
            else if (userId == null)
                return Database.Find<Registration>(r => r.GuildId == guildId).Result;
            else if (includeGlobal)
                return Database.Find<Registration>(r => (r.GuildId == null || r.GuildId == guildId) && r.UserId == userId.Value).Result;
            else
                return Database.Find<Registration>(r => r.GuildId == guildId && r.UserId == userId.Value).Result;
        }

        private IEnumerable<PlayerData> GetPlayers(params ulong[] userids)
            => Database.FindById<PlayerData>(userids).Result;

        private void RemoveRegistration(ulong? guildid, ulong? userid)
        {
            Database.Delete<Registration>(r => r.GuildId == guildid && (!userid.HasValue || r.UserId == userid.Value)).Wait();
        }
    }
}

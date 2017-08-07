using Discord;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Util;
using TT2Bot.Helpers;
using TT2Bot.Models;
using TT2Bot.Services;
using static TT2Bot.TT2Localisation.Commands;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Data
{
    [Description(Desc.HELPER)]
    [RequireOwner]
    [Alias("Helper")]
    [Name("Hero")]
    class HelpersCommand : Command
    {
        private TT2DataService DataService { get; }
        protected override string DelayMessage { get; } = DELAYMESSAGE_DATA;

        public HelpersCommand(TT2DataService dataService)
        {
            DataService = dataService;
        }

        [Call("List")]
        [Usage(Usage.HELPER_LIST)]
        
        async Task ListHelpersAsync([CallFlag('g', "group", Flag.HELPER_G)]bool shouldGroup = false)
        {
            var helpers = (await DataService.Helpers.GetAll())?.Where(h => h.IsInGame).OrderBy(h => h.Order);

            var builder = new LocalisedEmbedBuilder
            {
                Color = System.Drawing.Color.LightBlue.ToDiscord(),
                Footer = new LocalisedFooterBuilder().WithText(HelperText.LIST_FOOTER, BotUser.Username).WithRawIconUrl(BotUser.GetAvatarUrl()),
                Timestamp = DateTime.Now
            }.WithTitle(HelperText.LIST_TITLE)
             .WithDescription(HelperText.LIST_DESCRIPTION);

            if (shouldGroup)
                builder.AddField(f => f.WithName(HelperText.LIST_FIELD_ALL).WithRawValue(MakeTable(helpers)));
            else
                foreach (var group in helpers.GroupBy(h => h.HelperType))
                {
                    builder.AddField(f => f.WithName(HelperText.LIST_FIELD_GROUPED, group.Key.ToLocalisable())
                                           .WithRawValue(MakeTable(group)));
                }

            await ReplyAsync(builder);
        }

        private string MakeTable(IEnumerable<Helper> helpers)
            => "```\n" + helpers.Select(h => new[]
                                             {
                                                 h.ShortName.Localise(TextResource),
                                                 new LocalisedString(HelperText.COST, h.BaseCost).Localise(TextResource)
                                             }).ToArray()
                                               .Tableify() + "\n```";

        LocalisedEmbedBuilder GetBaseEmbed(Helper helper)
        {
            var builder = new LocalisedEmbedBuilder
            {
                Author = new LocalisedAuthorBuilder().WithName(HelperText.SHOW_TITLE, helper.Name).WithRawIconUrl(helper.ImageUrl),
                Footer = new LocalisedFooterBuilder().WithText(HelperText.SHOW_FOOTER, BotUser.Username, helper.FileVersion)
                                                     .WithRawIconUrl(BotUser.GetAvatarUrl()),
                Timestamp = DateTime.Now,
                Color = helper.Image.AverageColor(0.3f, 0.5f).ToDiscord(),
            }.WithRawThumbnailUrl(helper.ImageUrl);

            builder.AddInlineField(f => f.WithName(HelperText.SHOW_FIELD_ID).WithValue(helper.Id));
            builder.AddInlineField(f => f.WithName(HelperText.SHOW_FIELD_TYPE).WithValue(helper.HelperType.ToLocalisable()));

            return builder;
        }

        [Call]
        [Usage(Usage.HELPER)]
        async Task ShowHelperAsync([Dense]Helper helper, int? level = null)
        {
            var builder = GetBaseEmbed(helper);

            if (level == null)
            {
                builder.AddInlineField(f => f.WithName(HelperText.SHOW_FIELD_BASECOST).WithValue(HelperText.COST, helper.GetCost(0)));
                builder.AddInlineField(f => f.WithName(HelperText.SHOW_FIELD_BASEDAMAGE).WithValue(HelperText.DPS, helper.GetDps(1)));
            }
            else
            {
                builder.AddInlineField(f => f.WithName(HelperText.SHOW_FIELD_COSTAT, level).WithValue(HelperText.COST, helper.GetCost(0, level ?? 1)));
                builder.AddInlineField(f => f.WithName(HelperText.SHOW_FIELD_DAMAGEAT, level).WithValue(HelperText.DPS, helper.GetDps(level ?? 1)));
            }

            await ReplyAsync(builder);
        }
    }
}

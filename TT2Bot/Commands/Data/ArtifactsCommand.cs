﻿using Discord;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting;
using TT2Bot.Models;
using TT2Bot.Services;
using static TT2Bot.TT2Localisation.Commands;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Data
{
    [Description(Desc.ARTIFACT)]
    [Alias("Art", "Arts", "Artifact")]
    class ArtifactsCommand : Command
    {
        private TT2DataService DataService { get; }
        protected override string DelayMessage => DELAYMESSAGE_DATA;

        public ArtifactsCommand(TT2DataService dataService)
        {
            DataService = dataService;
        }

        [Call("List")]
        [Usage(Usage.ARTIFACT_LIST)]
        async Task ListArtifactsAsync()
        {
            var artifacts = await DataService.Artifacts.GetAll();

            var builder = new LocalisedEmbedBuilder
            {
                Color = System.Drawing.Color.LightBlue.ToDiscord(),
                Footer = new LocalisedFooterBuilder().WithRawIconUrl(BotUser.GetAvatarUrl())
                                                     .WithText(ArtifactText.LIST_FOOTER),
                Timestamp = DateTime.Now
            }.WithTitle(ArtifactText.LIST_TITLE)
             .WithDescription(ArtifactText.LIST_DESCRIPTION);

            foreach (var tier in artifacts.GroupBy(a => a.Tier).OrderBy(t => t.Key))
            {
                builder.AddInlineField(f => f.WithName(ArtifactText.LIST_ROW_TITLE, tier.Key).WithValues("\n", tier));
            }

            await ReplyAsync(builder);
        }

        LocalisedEmbedBuilder GetBaseEmbed(Artifact artifact)
        {
            var builder = new LocalisedEmbedBuilder
            {
                Title = (ArtifactText.SHOW_TITLE, artifact.Name),
                Footer = new LocalisedFooterBuilder().WithText(ArtifactText.SHOW_FOOTER, Author.Username, artifact.FileVersion)
                                                     .WithRawIconUrl(BotUser.GetAvatarUrl()),
                Timestamp = DateTime.Now,
                Color = artifact.Image?.AverageColor(0.3f, 0.5f).ToDiscord(),
            }.WithRawThumbnailUrl(artifact.ImageUrl);

            builder.AddInlineField(f => f.WithName(ArtifactText.SHOW_IDTITLE).WithValue(artifact.Id))
                   .AddInlineField(f => f.WithName(ArtifactText.SHOW_TIERTITLE).WithValue(ArtifactText.SHOW_TEIRVALUE, artifact.Tier))
                   .AddField(f => f.WithName(ArtifactText.SHOW_MAXLEVELTITLE).WithValue(artifact.MaxLevel ?? double.PositiveInfinity));

            return builder;
        }

        [Call("Budget")]
        [Usage(Usage.ARTIFACT_BUDGET)]
        Task GetBudgetAsync([Dense]Artifact artifact, double relics, int currentLevel = 0)
        {
            relics = relics.Clamp(0, double.MaxValue);
            currentLevel = currentLevel.Clamp(0, artifact.MaxLevel ?? int.MaxValue);
            return ShowArtifactAsync(artifact, currentLevel, artifact.BudgetArtifact(relics, currentLevel ));
        }

        [Call]
        [Usage(Usage.ARTIFACT)]
        async Task ShowArtifactAsync([Dense]Artifact artifact, int? from = null, int? to = null)
        {
            var builder = GetBaseEmbed(artifact);

            if (from == null)
            {
                builder.AddInlineField(f => f.WithName(ArtifactText.SHOW_BONUSTYPE).WithValue(artifact.BonusType.ToLocalisable()))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_BONUSPERLVL).WithValue(artifact.BonusType.LocaliseValue(artifact.EffectPerLevel)))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_BONUSTYPE).WithValue(BonusType.ArtifactDamage.ToLocalisable()))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_BONUSPERLVL).WithValue(BonusType.ArtifactDamage.LocaliseValue(artifact.DamageBonus)))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_COSTCOEF).WithValue(artifact.CostCoef))
                       .AddInlineField(f => f.WithName(ArtifactText.SHOW_COSTEXPO).WithValue(artifact.CostExpo))
                       .AddInlineField((ArtifactText.SHOW_COSTOFLVL, 1), (ArtifactText.SHOW_COST, artifact.CostOfLevel(2)));
            }
            else
            {
                var startLevel = Math.Min(from ?? 0, to ?? 1).Clamp(0, artifact.MaxLevel ?? int.MaxValue);
                var endLevel = Math.Max(from ?? 0, to ?? 1).Clamp(0, artifact.MaxLevel ?? int.MaxValue);

                builder.AddField((LocalisedString)ArtifactText.SHOW_BONUSTYPE, artifact.BonusType.ToLocalisable())
                       .AddInlineField((ArtifactText.SHOW_EFFECTAT, startLevel), artifact.BonusType.LocaliseValue(artifact.EffectAt(startLevel)))
                       .AddInlineField((ArtifactText.SHOW_EFFECTAT, endLevel), artifact.BonusType.LocaliseValue(artifact.EffectAt(endLevel)))
                       .AddField((LocalisedString)ArtifactText.SHOW_BONUSTYPE, BonusType.ArtifactDamage.ToLocalisable())
                       .AddInlineField((ArtifactText.SHOW_EFFECTAT, startLevel), BonusType.ArtifactDamage.LocaliseValue(artifact.DamageAt(startLevel)))
                       .AddInlineField((ArtifactText.SHOW_EFFECTAT, endLevel), BonusType.ArtifactDamage.LocaliseValue(artifact.DamageAt(endLevel)))
                       .AddField((ArtifactText.SHOW_LVLRANGE, startLevel, endLevel), (ArtifactText.SHOW_COST, artifact.CostToLevel(startLevel + 1, endLevel)))
                       .AddInlineField((ArtifactText.SHOW_COSTOFLVL, startLevel), (ArtifactText.SHOW_COST, artifact.CostOfLevel(startLevel + 1)))
                       .AddInlineField((ArtifactText.SHOW_COSTOFLVL, endLevel), (ArtifactText.SHOW_COST, artifact.CostOfLevel(endLevel)));
            }
            
            await ReplyAsync(builder);
        }
    }
}

using Discord;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Formatting;
using TT2Bot.Models;
using TT2Bot.Models.TT2;
using TT2Bot.Services;
using static TT2Bot.TT2Localisation.Commands;
using static TT2Bot.TT2Localisation.Help;

namespace TT2Bot.Commands.Data
{
    [Description(Desc.PETS)]
    [Alias("Pet")]
    class PetsCommand : Command
    {
        private TT2DataService DataService { get; }
        protected override LocalisedString DelayMessage => (LocalisedString)DELAYMESSAGE_DATA;

        public PetsCommand(TT2DataService dataService)
        {
            DataService = dataService;
        }

        LocalisedEmbedBuilder GetBaseEmbed(Pet pet)
        {
            var builder = new LocalisedEmbedBuilder
            {
                Author = new LocalisedAuthorBuilder().WithName(PetText.SHOW_TITLE, pet.Name).WithRawIconUrl(pet.ImageUrl),
                Footer = new LocalisedFooterBuilder().WithText(PetText.SHOW_FOOTER, BotUser.Username, pet.FileVersion).WithRawIconUrl(BotUser.GetAvatarUrl()),
                Timestamp = DateTime.Now,
                Color = pet.Image.AverageColor(0.3f, 0.5f).ToDiscord(),
            }.WithRawThumbnailUrl(pet.ImageUrl)
             .AddInlineField(f => f.WithName(PetText.SHOW_FIELD_ID).WithValue(pet.Id));

            return builder;
        }

        [Call]
        [Usage(Usage.PET)]
        async Task ShowPetAsync(Pet pet, int? level = null)
        {

            var builder = GetBaseEmbed(pet);

            if (level == null)
            {
                builder.AddField(f => f.WithName(PetText.SHOW_FIELD_BASEDAMAGE).WithValue(BonusType.PetDamage.LocaliseValue(pet.DamageBase)));
                var keysOrdered = pet.IncreaseRanges.Keys.OrderBy(k => k).ToList();
                for (int i = 0; i < keysOrdered.Count(); i++)
                {
                    if (i == keysOrdered.Count() - 1)
                        builder.AddInlineField(f => f.WithName(PetText.SHOW_FIELD_LVLXUP, keysOrdered[i])
                                                     .WithValue(BonusType.PetDamage.LocaliseValue(pet.IncreaseRanges[keysOrdered[i]])));
                    else
                        builder.AddInlineField(f => f.WithName(PetText.SHOW_FIELD_LVLXTOY, keysOrdered[i], keysOrdered[i + 1] - 1)
                                                     .WithValue(BonusType.PetDamage.LocaliseValue(pet.IncreaseRanges[keysOrdered[i]])));
                }
                builder.AddField(f => f.WithName(PetText.SHOW_FIELD_BONUSTYPE).WithValue(pet.BonusType.ToLocalisable()));
                builder.AddInlineField(f => f.WithName(PetText.SHOW_FIELD_BONUSBASE).WithValue(pet.BonusType.LocaliseValue(pet.BonusBase)));
                builder.AddInlineField(f => f.WithName(PetText.SHOW_FIELD_BONUSINCREASE).WithValue(pet.BonusType.LocaliseValue(pet.BonusIncrement)));
            }
            else
            {
                var actualLevel = level ?? 1;
                var dmg = pet.DamageOnLevel(actualLevel);
                var bonus = pet.BonusOnLevel(actualLevel);
                builder.AddField(f => f.WithName(PetText.SHOW_FIELD_BONUSTYPE).WithValue(pet.BonusType.ToLocalisable()));
                builder.AddInlineField(f => f.WithValue(PetText.SHOW_FIELD_DAMAGEAT, actualLevel).WithValue(BonusType.PetDamage.LocaliseValue(dmg)));
                builder.AddInlineField(f => f.WithValue(PetText.SHOW_FIELD_BONUSAT, actualLevel).WithValue(pet.BonusType.LocaliseValue(bonus)));
                if (pet.InactiveMultiplier(actualLevel) < 1)
                {
                    var mult = pet.InactiveMultiplier(actualLevel);
                    builder.AddField(f => f.WithName(PetText.SHOW_FIELD_INACTIVEPERCENT, actualLevel).WithValue(mult));
                    builder.AddInlineField(f => f.WithName(PetText.SHOW_FIELD_INACTIVEDAMAGEAT, actualLevel).WithValue(BonusType.PetDamage.LocaliseValue(mult * dmg)));
                    builder.AddInlineField(f => f.WithName(PetText.SHOW_FIELD_INACTIVEBONUSAT, actualLevel).WithValue(pet.BonusType.LocaliseValue(mult * bonus)));
                }
            }

            await ReplyAsync(builder);
        }

        [Call("List")]
        [Usage(Usage.PET_LIST)]
        async Task ListPetsAsync()
        {
            var pets = await DataService.Pets.GetAll();

            var builder = new LocalisedEmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    IconUrl = BotUser.GetAvatarUrl(),
                    Name = "Pet listing"
                },
                Color = System.Drawing.Color.LightBlue.ToDiscord(),
                Description = (RawString)"All Pets",
                Footer = new EmbedFooterBuilder
                {
                    IconUrl = BotUser.GetAvatarUrl(),
                    Text = $"{BotUser.Username} Pet tool"
                },
                Timestamp = DateTime.Now
            };

            foreach (var bonus in pets.GroupBy(p => p.BonusType).OrderBy(t => t.Key))
                builder.AddInlineField(f => f.WithName(bonus.Key.ToLocalisable()).WithValues("\n", bonus));

            await ReplyAsync(builder);
        }
    }
}

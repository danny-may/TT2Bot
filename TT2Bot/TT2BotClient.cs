using Conversion;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TitanBot;
using TitanBot.Dependencies;
using TitanBot.Formatter;
using TitanBot.Logging;
using TT2Bot.Helpers;
using TT2Bot.Models;
using TT2Bot.Overrides;
using TT2Bot.TypeReaders;

namespace TT2Bot
{
    public class TT2BotClient
    {
        private readonly BotClient _client;

        public Task UntilOffline => _client.UntilOffline;

        public TT2BotClient()
        {
            _client = new BotClient(MapDependencies);
            _client.Install(Assembly.GetExecutingAssembly());

            //This is just for migrating from the old titanbot database structure
            //Console.WriteLine("Do you want to convert from an old database? Press enter if not, or type in the location of the database if you do:");
            //var location = Console.ReadLine();
            //if (!string.IsNullOrWhiteSpace(location))
            //    new Converter(location, Client).Convert();

            RegisterSettings();
            RegisterTypeReaders();
            PopulateMapper();
        }

        private static void MapDependencies(IDependencyFactory factory)
        {
            factory.Map<ILogger, ConsoleLogger>();
            factory.Map<OutputFormatter, Formatter>();
        }

        private void PopulateMapper()
        {
            _client.DependencyFactory.GetOrStore<TT2DataService>();
        }

        private void RegisterSettings()
        {
            string StrLengthValidator(string s) => s.Length < 500 ? null : "You cannot have more than 500 characters for this setting";
            _client.SettingsManager
                .Register<TitanLordSettings>().WithName("TitanLord")
                .WithDescription("These are the settings surrounding the `t$titanlord` command")
                .WithNotes("There are several format strings you can use to have live data in your message.\n" +
                            "Use `%USER%` to include the user who started the timer\n" +
                            "Use `%TIME%` to include how long until the titan lord is up\n" +
                            "Use `%ROUND%` for the round number\n" +
                            "Use `%CQ%` for the current CQ number\n" +
                            "Use `%COMPLETE%` for the time the titan lord will be up (UTC time)\n" + 
                            "Alternatively `%COMPLETE+timezone%` can be used to define the timezone, e.g. `%COMPLETE+6%`, minus can also be used but timezone has to be a number from 0 to 12\n" +
                            "One important note is that the timezone has to be the number used during winter. For example, during summer England is using UTC+1, but here you would simply use +0")
                .AddSetting(s => s.TimerText, validator: StrLengthValidator)
                .AddSetting(s => s.InXText, validator: StrLengthValidator)
                .AddSetting(s => s.NowText, validator: StrLengthValidator)
                .AddSetting(s => s.RoundText, validator: StrLengthValidator)
                .AddSetting(s => s.PinTimer)
                .AddSetting(s => s.RoundPings)
                .AddSetting(s => s.PrePings, (TimeSpan[] t) => t.Select(v => (int)v.TotalSeconds).ToArray(), viewer: v => string.Join(", ", v.Select(t => new TimeSpan(0, 0, t))))
                .AddSetting("ClanQuest", s => s.CQ, validator: v => v > 0 ? null : "Your clan quest cannot be negative")
                .AddSetting("TimerChannel", s => s.Channel, (IMessageChannel c) => c?.Id, viewer: v => v == null ? null : $"<#{v}>")
                .Finalise();

            _client.SettingsManager
                .RegisterGlobal<TT2GlobalSettings>().WithName("TT2")
                .WithDescription("These are the global settings for titanbot")
                .AddSetting(s => s.BotBugChannel, (IMessageChannel c) => c.Id, viewer: v => $"<#{v}>")
                .AddSetting(s => s.BotSuggestChannel, (IMessageChannel c) => c.Id, viewer: v => $"<#{v}>")
                .AddSetting(s => s.GHFeedbackChannel, (IMessageChannel c) => c.Id, viewer: v => $"<#{v}>")
                .AddSetting(s => s.ImageRegex)
                .AddSetting(s => s.DefaultVersion)
                .Finalise();

            _client.SettingsManager
                .RegisterGlobal((m, id) => 
                    m.GetCustomGlobal<TT2GlobalSettings.DataFileVersions>(),
                        (m, id, o) =>
                        {
                            var parent = m.GetCustomGlobal<TT2GlobalSettings>();
                            parent.FileVersions = o;
                            m.SaveCustomGlobal(parent);
                        }
                    )
                .WithName("FileVersions")
                .WithDescription("These are the versions used for the data commands")
                .AddSetting(s => s.Artifact)
                .AddSetting(s => s.Equipment)
                .AddSetting(s => s.Helper)
                .AddSetting(s => s.HelperSkill)
                .AddSetting(s => s.Pet)
                .Finalise();
        }

        private void RegisterTypeReaders()
        {
            _client.TypeReaders.AddTypeReader<Artifact>(new ArtifactTypeReader());
            _client.TypeReaders.AddTypeReader<Pet>(new PetTypeReader());
            _client.TypeReaders.AddTypeReader<Equipment>(new EquipmentTypeReader());
            _client.TypeReaders.AddTypeReader<Helper>(new HelperTypeReader());
        }

        public async Task StartAsync(Func<string, string> getToken)
        {
            await _client.StartAsync(getToken);
        }

        public Task StopAsync()
            => _client.StopAsync();
    }
}

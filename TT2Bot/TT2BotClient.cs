using Discord;
using System;
using System.Reflection;
using System.Threading.Tasks;
using TitanBot;
using TitanBot.Dependencies;
using TitanBot.Formatting;
using TitanBot.Logging;
using TitanBot.Settings;
using TitanBot.TypeReaders;
using TT2Bot.Helpers;
using TT2Bot.Models;
using TT2Bot.Overrides;
using TT2Bot.Services;
using TT2Bot.TypeReaders;

namespace TT2Bot
{
    public class TT2BotClient
    {
        BotClient _client { get; }

        public Task UntilOffline => _client.UntilOffline;

        public TT2BotClient()
        {
            _client = new BotClient(MapDependencies);
            _client.TextResourceManager.RequireKeys(TT2Localisation.Defaults);
            _client.InstallHandlers(Assembly.GetExecutingAssembly());
            _client.CommandService.Install(_client.DefaultCommands);
            _client.CommandService.Install(Assembly.GetExecutingAssembly());

            RegisterSettings(_client.SettingsManager);
            RegisterTypeReaders(_client.TypeReaders, _client.DependencyFactory);
        }

        private void MapDependencies(IDependencyFactory factory)
        {
            factory.Map<ILogger, ConsoleLogger>();
            factory.Map<ValueFormatter, Formatter>();
        }

        private void RegisterSettings(ISettingManager settingManager)
        {
            Func<string, string> strLengthValidator = ((string s) => s.Length < 500 ? null : "You cannot have more than 500 characters for this setting");
            settingManager.GetEditorCollection<TitanLordSettings>(SettingScope.Guild)
                          .WithName("TitanLord")
                          .WithDescription("These are the settings surrounding the `t$titanlord` command")
                          .WithNotes("There are several format strings you can use to have live data in your message.\n" +
                                     "Use `%USER%` to include the user who started the timer\n" +
                                     "Use `%TIME%` to include how long until the titan lord is up\n" +
                                     "Use `%ROUND%` for the round number\n" +
                                     "Use `%CQ%` for the current CQ number\n" +
                                     "Use `%COMPLETE%` for the time the titan lord will be up (UTC time)\n" +
                                     "Alternatively `%COMPLETE+timezone%` can be used to define the timezone, e.g. `%COMPLETE+6%`, minus can also be used but timezone has to be a number from 0 to 12")
                          .AddSetting(s => s.TimerText, b => b.SetValidator(strLengthValidator))
                          .AddSetting(s => s.InXText, b => b.SetValidator(strLengthValidator))
                          .AddSetting(s => s.NowText, b => b.SetValidator(strLengthValidator))
                          .AddSetting(s => s.RoundText, b => b.SetValidator(strLengthValidator))
                          .AddSetting(s => s.PinTimer)
                          .AddSetting(s => s.RoundPings)
                          .AddSetting<int, TimeSpan>(s => s.PrePings, b => (int)b.TotalSeconds, b => b.SetViewer(i => new TimeSpan(0, 0, i).ToString()))
                          .AddSetting(s => s.CQ, b => b.SetName("ClanQuest").SetValidator(v => v > 0 ? null : "Your clan quest cannot be negative"))
                          .AddSetting<IMessageChannel>(s => s.Channel, b => b.SetName("TimerChannel"));

            settingManager.GetEditorCollection<TT2GlobalSettings>(SettingScope.Global)
                          .WithName("TT2")
                          .WithDescription("These are the global settings for titanbot")
                          .AddSetting<IMessageChannel>(s => s.BotBugChannel)
                          .AddSetting<IMessageChannel>(s => s.BotSuggestChannel)
                          .AddSetting<IMessageChannel>(s => s.GHFeedbackChannel)
                          .AddSetting(s => s.ImageRegex)
                          .AddSetting(s => s.DefaultVersion);

            settingManager.GetEditorCollection<TT2GlobalSettings.DataFileVersions>(SettingScope.Global)
                          .WithName("FileVersions")
                          .WithDescription("These are the versions used for the data commands")
                          .AddSetting(s => s.Artifact)
                          .AddSetting(s => s.Equipment)
                          .AddSetting(s => s.Helper)
                          .AddSetting(s => s.HelperSkill)
                          .AddSetting(s => s.Pet);
        }

        private void RegisterTypeReaders(ITypeReaderCollection typeReaders, IDependencyFactory factory)
        {
            var dataService = factory.GetOrStore<TT2DataService>();
            typeReaders.AddTypeReader<Artifact>(new ArtifactTypeReader(dataService));
            typeReaders.AddTypeReader<Pet>(new PetTypeReader(dataService));
            typeReaders.AddTypeReader<Equipment>(new EquipmentTypeReader(dataService));
            typeReaders.AddTypeReader<Helper>(new HelperTypeReader(dataService));
        }

        public async Task StartAsync(Func<string, string> getToken)
        {
            await _client.StartAsync(getToken);
        }

        public Task StopAsync()
            => _client.StopAsync();
    }
}

using Discord;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TitanBot;
using TitanBot.Dependencies;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using TitanBot.Logging;
using TitanBot.Replying;
using TitanBot.Settings;
using TitanBot.TypeReaders;
using TT2Bot.Models;
using TT2Bot.Models.TT2;
using TT2Bot.Overrides;
using TT2Bot.Services;
using TT2Bot.TypeReaders;
using static TT2Bot.TT2Localisation.Settings;

namespace TT2Bot
{
    public class TT2BotClient
    {
        BotClient _client { get; }

        public Task UntilOffline => _client.UntilOffline;

        public TT2BotClient()
        {
            _client = new BotClient(MapDependencies);
            _client.TextResourceManager.RegisterKeys(TT2Localisation.Defaults);
            _client.InstallHandlers(Assembly.GetExecutingAssembly());
            _client.CommandService.Install(_client.DefaultCommands);
            _client.CommandService.Install(Assembly.GetExecutingAssembly());

            RegisterSettings(_client.SettingsManager);
            RegisterTypeReaders(_client.TypeReaders, _client.DependencyFactory);

            _client.SettingsManager.Migrate(new Dictionary<string, Type>
            {
                { "TitanBot.Settings.GeneralSettings", typeof(GeneralGuildSetting) },
                { typeof(TitanLordSettings).ToString(), typeof(TitanLordSettings) },
                { typeof(RegistrationSettings).ToString(), typeof(RegistrationSettings) }

            });
        }

        private void MapDependencies(IDependencyFactory factory)
        {
            factory.Map<ILogger, ConsoleLogger>();
            factory.Map<ValueFormatter, Formatter>();
        }

        private void RegisterSettings(ISettingManager settingManager)
        {
            Func<string, ILocalisable<string>> strLengthValidator = ((string s) => s.Length < 500 ? null : new LocalisedString(STRING_TOOLONG, ReplyType.Error));
            settingManager.GetEditorCollection<TitanLordSettings>(SettingScope.Guild)
                          .WithName("TitanLord")
                          .WithDescription(Desc.GUILD_TITANLORD)
                          .WithNotes(Notes.GUILD_TITANLORD)
                          .AddSetting(s => s.TimerText, b => b.SetValidator(strLengthValidator))
                          .AddSetting(s => s.InXText, b => b.SetValidator(strLengthValidator))
                          .AddSetting(s => s.NowText, b => b.SetValidator(strLengthValidator))
                          .AddSetting(s => s.RoundText, b => b.SetValidator(strLengthValidator))
                          .AddSetting(s => s.PinTimer)
                          .AddSetting(s => s.RoundPings)
                          .AddSetting<int, TimeSpan>(s => s.PrePings, b => (int)b.TotalSeconds, b => b.SetViewer(i => new RawString("{0}", new TimeSpan(0, 0, i))))
                          .AddSetting(s => s.CQ, b => b.SetName("ClanQuest")
                                                       .SetAlias("CQ")
                                                       .SetValidator(v => v > 0 ? null : new LocalisedString(CQ_TOOLOW, ReplyType.Error)))
                          .AddSetting<IMessageChannel>(s => s.Channel, b => b.SetName("TimerChannel"));

            settingManager.GetEditorCollection<TT2GlobalSettings>(SettingScope.Global)
                          .WithName("TT2")
                          .WithDescription(Desc.GLOBAL_TT2)
                          .AddSetting<IMessageChannel>(s => s.BotBugChannel)
                          .AddSetting<IMessageChannel>(s => s.BotSuggestChannel)
                          .AddSetting<IMessageChannel>(s => s.GHFeedbackChannel)
                          .AddSetting(s => s.ImageRegex)
                          .AddSetting(s => s.DefaultVersion);

            settingManager.GetEditorCollection<TT2GlobalSettings.DataFileVersions>(SettingScope.Global)
                          .WithName("FileVersions")
                          .WithDescription(Desc.GLOBAL_DATAFILE)
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

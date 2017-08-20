using Csv;
using System;
using System.Text;
using System.Threading.Tasks;
using TitanBot.Downloader;
using TitanBot.Settings;
using TT2Bot.Models;
using TT2Bot.Models.TT2;
using TT2Bot.Services.ServiceAreas;

namespace TT2Bot.Services
{
    class TT2DataService
    {
        private IDownloader WebClient { get; }
        private ISettingManager Settings { get; }
        public static readonly string GHStatic = "https://s3.amazonaws.com/tt2-static/info_files/";
        public static readonly string HighScoreSheet = "https://docs.google.com/spreadsheets/d/13hsvWaYvp_QGFuQ0ukcgG-FlSAj2NyW8DOvPUG3YguY/export?format=csv&id=13hsvWaYvp_QGFuQ0ukcgG-FlSAj2NyW8DOvPUG3YguY&gid=4642011";

        public GameEntityService<Artifact, int> Artifacts { get; }
        public GameEntityService<Equipment, string> Equipment { get; }
        public GameEntityService<Pet, int> Pets { get; }
        public GameEntityService<HelperSkill, int> HelperSkills { get; }
        public GameEntityService<Helper, int> Helpers { get; }
        public GameEntityService<Skill, string> SkillTree { get; }

        public TT2DataService(IDownloader client, ISettingManager settings)
        {
            WebClient = client;
            Settings = settings;

            Artifacts = new ArtifactService(GlobalSettings, WebClient);
            Equipment = new EquipmentService(GlobalSettings, WebClient);
            Pets = new PetService(GlobalSettings, WebClient);
            HelperSkills = new HelperSkillService(GlobalSettings, WebClient);
            Helpers = new HelperService(GlobalSettings, WebClient, HelperSkills);
            SkillTree = new SkillTreeService(GlobalSettings, WebClient);
        }

        private TT2GlobalSettings GlobalSettings()
            => Settings.GetContext(Settings.Global).Get<TT2GlobalSettings>();

        public async ValueTask<HighScoreSheet> GetHighScores()
        {
            var data = await WebClient.GetString(new Uri(HighScoreSheet), Encoding.UTF8);

            var sheet = new HighScoreSheet(CsvReader.ReadFromText(data, new CsvOptions
            {
                HeaderMode = HeaderMode.HeaderAbsent
            }), Settings.GetContext(Settings.Global).Get<HighScoreSettings>());

            return sheet;
        }
    }
}

using Csv;
using System;
using System.Linq;
using TitanBot.Downloader;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Models;

namespace TT2Bot.GameEntity.Services
{
    class HelperService : GameEntityService<Helper>
    {
        protected override string FilePath => "/HelperInfo.csv";
        protected override string FileVersion => Settings.FileVersions.Helper;

        private GameEntityService<HelperSkill> HelperSkills;

        public HelperService(Func<TT2GlobalSettings> settings, IDownloader webClient, GameEntityService<HelperSkill> helperSkills)
            : base(settings, webClient)
        {
            HelperSkills = helperSkills;
        }

        protected override Helper Build(ICsvLine serverData, string version)
        {
            int.TryParse(serverData[0].Replace("H", ""), out int helperId);
            int.TryParse(serverData[1], out int order);
            Enum.TryParse(serverData[2], out HelperType type);
            double.TryParse(serverData[3], out double baseCost);
            int.TryParse(serverData[4], out int isInGame);

            var skills = HelperSkills.GetAll().Result.Where(h => h.HelperId == helperId).ToList();

            return new Helper(helperId, order, type, baseCost, skills, isInGame > 0, version, GetImage);
        }
    }
}

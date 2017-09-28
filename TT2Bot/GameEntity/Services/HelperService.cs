using System;
using System.Linq;
using TitanBot.Downloader;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.Enums;
using TT2Bot.Helpers;
using TT2Bot.Models;
using TT2Bot.Models.General;

namespace TT2Bot.GameEntity.Services
{
    internal class HelperService : GameEntityService<Helper>
    {
        protected override string FilePath => "/HelperInfo.csv";
        protected override string FileVersion => Settings.FileVersions.Helper;

        private GameEntityService<HelperSkill> HelperSkills;

        public HelperService(Func<TT2GlobalSettings> settings, IDownloader webClient, GameEntityService<HelperSkill> helperSkills)
            : base(settings, webClient)
        {
            HelperSkills = helperSkills;
        }

        protected override Helper Build(Iterable<string> serverData, string version)
        {
            int.TryParse(serverData.Next().Without("H"), out int helperId);
            int.TryParse(serverData.Next(), out int order);
            Enum.TryParse(serverData.Next(), out HelperType type);
            double.TryParse(serverData.Next(), out double baseCost);
            int.TryParse(serverData.Next(), out int isInGame);

            var skills = HelperSkills.GetAll().Result.Where(h => h.HelperId == helperId).ToList();

            return new Helper(helperId, order, type, baseCost, skills, isInGame > 0, version, GetImage);
        }
    }
}
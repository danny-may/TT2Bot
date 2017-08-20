using Csv;
using System;
using System.Collections.Generic;
using TitanBot.Downloader;
using TT2Bot.Models;
using TT2Bot.Models.TT2;

namespace TT2Bot.Services.ServiceAreas
{
    class HelperSkillService : GameEntityService<HelperSkill, int>
    {
        protected override string FilePath => "/HelperSkillInfo.csv";
        protected override string FileVersion => Settings.FileVersions.HelperSkill;

        public HelperSkillService(Func<TT2GlobalSettings> settings, IDownloader webClient) : base(settings, webClient) { }

        private Dictionary<int, string> Names { get; } = new Dictionary<int, string>();

        protected override HelperSkill Build(ICsvLine serverData, string version)
        {
            int.TryParse(serverData[0], out int skillId);
            int.TryParse(serverData[1].Replace("H", ""), out int helperId);
            var name = serverData[2];
            Enum.TryParse(serverData[3], out BonusType type);
            double.TryParse(serverData[4], out double magnitude);
            int.TryParse(serverData[5], out int requirement);

            Names.Add(skillId, name);

            return new HelperSkill(skillId, helperId, type, magnitude, requirement, version);
        }
    }
}

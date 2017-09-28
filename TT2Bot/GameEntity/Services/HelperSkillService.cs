using Csv;
using System;
using System.Collections.Generic;
using TitanBot.Downloader;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.Enums;
using TT2Bot.Helpers;
using TT2Bot.Models;
using TT2Bot.Models.General;

namespace TT2Bot.GameEntity.Services
{
    internal class HelperSkillService : GameEntityService<HelperSkill>
    {
        protected override string FilePath => "/HelperSkillInfo.csv";
        protected override string FileVersion => Settings.FileVersions.HelperSkill;

        public HelperSkillService(Func<TT2GlobalSettings> settings, IDownloader webClient) : base(settings, webClient)
        {
        }

        private Dictionary<int, string> Names { get; } = new Dictionary<int, string>();

        protected override HelperSkill Build(Iterable<string> serverData, string version)
        {
            int.TryParse(serverData.Next(), out int skillId);
            int.TryParse(serverData.Next().Without("H"), out int helperId);
            var name = serverData.Next();
            Enum.TryParse(serverData.Next(), out BonusType type);
            double.TryParse(serverData.Next(), out double magnitude);
            int.TryParse(serverData.Next(), out int requirement);

            Names.Add(skillId, name);

            return new HelperSkill(skillId, helperId, type, magnitude, requirement, version);
        }
    }
}
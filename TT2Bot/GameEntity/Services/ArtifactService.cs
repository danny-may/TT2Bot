using Csv;
using System;
using TitanBot.Downloader;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Models;

namespace TT2Bot.GameEntity.Services
{
    class ArtifactService : GameEntityService<Artifact>
    {
        protected override string FilePath => "/ArtifactInfo.csv";
        protected override string FileVersion => Settings.FileVersions.Artifact;

        public ArtifactService(Func<TT2GlobalSettings> settings, IDownloader webClient) : base(settings, webClient) { }

        protected override Artifact Build(ICsvLine serverData, string version)
        {
            int.TryParse(serverData[0].Substring(8), out int id);
            int.TryParse(serverData[1], out int maxLevel);
            string tt1 = serverData[2];
            Enum.TryParse(serverData[3], out BonusType bonusType);
            double.TryParse(serverData[4], out double effectPerLevel);
            double.TryParse(serverData[5], out double damageBonus);
            double.TryParse(serverData[6], out double costCoef);
            double.TryParse(serverData[7], out double costExpo);
            string note = serverData[8];
            string name = serverData[9];

            return new Artifact(id,
                                maxLevel == 0 ? (int?)null : maxLevel,
                                tt1,
                                bonusType,
                                effectPerLevel,
                                damageBonus,
                                costCoef,
                                costExpo,
                                note,
                                version,
                                GetImage);
        }
    }
}

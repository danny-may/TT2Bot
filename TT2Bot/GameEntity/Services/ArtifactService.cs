using System;
using TitanBot.Downloader;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.Enums;
using TT2Bot.Helpers;
using TT2Bot.Models;
using TT2Bot.Models.General;

namespace TT2Bot.GameEntity.Services
{
    internal class ArtifactService : GameEntityService<Artifact>
    {
        protected override string FilePath => "/ArtifactInfo.csv";
        protected override string FileVersion => Settings.FileVersions.Artifact;

        public ArtifactService(Func<TT2GlobalSettings> settings, IDownloader webClient) : base(settings, webClient)
        {
        }

        protected override Artifact Build(Iterable<string> serverData, string version)
        {
            int.TryParse(serverData.Next().Without("Artifact"), out int id);
            int.TryParse(serverData.Next(), out int maxLevel);
            //string tt1 = serverData.Next();
            var bonustext = serverData.Next();
            Enum.TryParse(bonustext, out BonusType bonusType);
            double.TryParse(serverData.Next(), out double effectPerLevel);
            double.TryParse(serverData.Next(), out double growthMax);
            double.TryParse(serverData.Next(), out double growthRate);
            double.TryParse(serverData.Next(), out double growthExpo);
            double.TryParse(serverData.Next(), out double damageBonus);
            double.TryParse(serverData.Next(), out double costCoef);
            double.TryParse(serverData.Next(), out double costExpo);
            string note = serverData.Next();
            string name = serverData.Next();

            return new Artifact(id,
                                maxLevel == 0 ? (int?)null : maxLevel,
                                //tt1,
                                bonusType,
                                effectPerLevel,
                                growthMax,
                                growthRate,
                                growthExpo,
                                damageBonus,
                                costCoef,
                                costExpo,
                                note,
                                version,
                                GetImage);
        }
    }
}
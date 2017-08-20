using Csv;
using System;
using System.Collections.Generic;
using TitanBot.Downloader;
using TT2Bot.Models;
using TT2Bot.Models.TT2;

namespace TT2Bot.Services.ServiceAreas
{
    class SkillTreeService : GameEntityService<Skill, string>
    {
        protected override string FilePath => "/SkillTreeInfo.csv";
        protected override string FileVersion => Settings.FileVersions.Pet;

        public SkillTreeService(Func<TT2GlobalSettings> settings, IDownloader webClient) : base(settings, webClient) { }

        protected override Skill Build(ICsvLine serverData, string version)
        {
            var id = serverData[0];
            var note = serverData[1];
            var req = serverData[2];
            int.TryParse(serverData[3], out var stageReq);

            var levels = new List<(int, double)>();

            for (int i = 0; i < 21; i++)
            {
                var hasCost = int.TryParse(serverData[4 + i], out var cost);
                var hasBonus = double.TryParse(serverData[25 + i], out var bonus);
                if (hasBonus)
                    levels.Add((cost, bonus));
                else
                    break;
            }

            return new Skill(id, note, req, CachedObjects, stageReq, levels, version, GetImage);
        }
    }
}
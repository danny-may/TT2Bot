using Csv;
using System;
using System.Collections.Generic;
using TitanBot.Downloader;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.Enums.EntityId;
using TT2Bot.Models;

namespace TT2Bot.Services.ServiceAreas
{
    class SkillTreeService : GameEntityService<Skill>
    {
        protected override string FilePath => "/SkillTreeInfo.csv";
        protected override string FileVersion => Settings.FileVersions.Pet;

        public SkillTreeService(Func<TT2GlobalSettings> settings, IDownloader webClient) : base(settings, webClient) { }

        protected override Skill Build(ICsvLine serverData, string version)
        {
            Enum.TryParse(serverData[0], out SkillId id);
            var note = serverData[1];
            Enum.TryParse(serverData[2], out SkillId req);
            int.TryParse(serverData[3], out var stageReq);

            var levels = new List<(int, double)>();

            for (int i = 0; i < 20; i++)
            {
                var hasCost = int.TryParse(serverData[4 + i], out var cost);
                var hasBonus = double.TryParse(serverData[26 + i], out var bonus);
                if (hasBonus)
                    levels.Add((cost, bonus));
                else
                    break;
            }

            return new Skill(id, note, req, CachedObjects, stageReq, levels, version, GetImage);
        }
    }
}
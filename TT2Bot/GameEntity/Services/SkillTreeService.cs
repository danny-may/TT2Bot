using System;
using System.Collections.Generic;
using TitanBot.Downloader;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.Enums.EntityId;
using TT2Bot.Models;
using TT2Bot.Models.General;

namespace TT2Bot.Services.ServiceAreas
{
    internal class SkillTreeService : GameEntityService<Skill>
    {
        protected override string FilePath => "/SkillTreeInfo.csv";
        protected override string FileVersion => Settings.FileVersions.Pet;

        public SkillTreeService(Func<TT2GlobalSettings> settings, IDownloader webClient) : base(settings, webClient)
        {
        }

        protected override Skill Build(Iterable<string> serverData, string version)
        {
            Enum.TryParse(serverData.Next(), out SkillId id);
            var note = serverData.Next();
            Enum.TryParse(serverData.Next(), out SkillId req);
            int.TryParse(serverData.Next(), out var stageReq);

            var levels = new List<(int, double)>();

            //for (int i = 0; i < 20; i++)
            //{
            //    var hasCost = int.TryParse(serverData[4 + i], out var cost);
            //    var hasBonus = double.TryParse(serverData[26 + i], out var bonus);
            //    if (hasBonus)
            //        levels.Add((cost, bonus));
            //    else
            //        break;
            //}

            return new Skill(id, note, req, CachedObjects, stageReq, levels, version, GetImage);
        }
    }
}
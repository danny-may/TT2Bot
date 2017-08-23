using Csv;
using System;
using System.Collections.Generic;
using TitanBot.Downloader;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.Enums;
using TT2Bot.Helpers;
using TT2Bot.Models;

namespace TT2Bot.Services.ServiceAreas
{
    class PetService : GameEntityService<Pet>
    {
        protected override string FilePath => "/PetInfo.csv";
        protected override string FileVersion => Settings.FileVersions.Pet;

        public PetService(Func<TT2GlobalSettings> settings, IDownloader webClient) : base(settings, webClient) { }

        protected override Pet Build(ICsvLine serverData, string version)
        {
            var incrementRange = new Dictionary<int, double> { };

            int.TryParse(serverData[0].Without("Pet"), out int id);
            double.TryParse(serverData[1], out double damageBase);
            double.TryParse(serverData[2], out double inc1to40);
            double.TryParse(serverData[3], out double inc41to80);
            double.TryParse(serverData[4], out double inc80on);
            Enum.TryParse(serverData[5], out BonusType bonusType);
            double.TryParse(serverData[6], out double bonusBase);
            double.TryParse(serverData[7], out double bonusIncrement);

            incrementRange.Add(1, inc1to40);
            incrementRange.Add(41, inc41to80);
            incrementRange.Add(81, inc80on);

            return new Pet(id, damageBase, incrementRange, bonusType, bonusBase, bonusIncrement, version, GetImage);
        }
    }
}
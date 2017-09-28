using System;
using System.Collections.Generic;
using TitanBot.Downloader;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.Enums;
using TT2Bot.Helpers;
using TT2Bot.Models;
using TT2Bot.Models.General;

namespace TT2Bot.Services.ServiceAreas
{
    internal class PetService : GameEntityService<Pet>
    {
        protected override string FilePath => "/PetInfo.csv";
        protected override string FileVersion => Settings.FileVersions.Pet;

        public PetService(Func<TT2GlobalSettings> settings, IDownloader webClient) : base(settings, webClient)
        {
        }

        protected override Pet Build(Iterable<string> serverData, string version)
        {
            var incrementRange = new Dictionary<int, double> { };

            int.TryParse(serverData.Next().Without("Pet"), out int id);
            double.TryParse(serverData.Next(), out double damageBase);
            double.TryParse(serverData.Next(), out double inc1to40);
            double.TryParse(serverData.Next(), out double inc41to80);
            double.TryParse(serverData.Next(), out double inc80on);
            Enum.TryParse(serverData.Next(), out BonusType bonusType);
            double.TryParse(serverData.Next(), out double bonusBase);
            double.TryParse(serverData.Next(), out double bonusIncrement);

            incrementRange.Add(1, inc1to40);
            incrementRange.Add(41, inc41to80);
            incrementRange.Add(81, inc80on);

            return new Pet(id, damageBase, incrementRange, bonusType, bonusBase, bonusIncrement, version, GetImage);
        }
    }
}
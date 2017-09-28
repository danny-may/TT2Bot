using System;
using TitanBot.Downloader;
using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.Enums;
using TT2Bot.GameEntity.Enums.EntityId;
using TT2Bot.Models;
using TT2Bot.Models.General;

namespace TT2Bot.GameEntity.Services
{
    internal class EquipmentService : GameEntityService<Equipment>
    {
        protected override string FilePath => "/EquipmentInfo.csv";
        protected override string FileVersion => Settings.FileVersions.Equipment;

        public EquipmentService(Func<TT2GlobalSettings> settings, IDownloader webClient) : base(settings, webClient)
        {
        }

        protected override Equipment Build(Iterable<string> serverData, string version)
        {
            Enum.TryParse(serverData.Next(), out EquipmentId id);
            Enum.TryParse(serverData.Next(), out EquipmentClass eClass);
            Enum.TryParse(serverData.Next(), out BonusType bonusType);
            Enum.TryParse(serverData.Next(), out EquipmentRarity rarity);
            double.TryParse(serverData.Next(), out double attributeBase);
            double.TryParse(serverData.Next(), out double attributeBaseInc);
            double.TryParse(serverData.Next(), out double attributeExp1);
            double.TryParse(serverData.Next(), out double attributeExp2);
            double.TryParse(serverData.Next(), out double attributeExpBase);
            Enum.TryParse(serverData.Next(), out EquipmentSource source);
            double.TryParse(serverData.Next(), out double _1163_1);
            double.TryParse(serverData.Next(), out double _1163_2);
            double.TryParse(serverData.Next(), out double _2095_1);
            double.TryParse(serverData.Next(), out double _2095_2);
            double.TryParse(serverData.Next(), out double _4450_1);
            double.TryParse(serverData.Next(), out double _4450_2);

            return new Equipment(id,
                                 eClass,
                                 bonusType,
                                 rarity,
                                 attributeBase,
                                 attributeBaseInc,
                                 attributeExp1,
                                 attributeExp2,
                                 attributeExpBase,
                                 source,
                                 new[] { _1163_1, _1163_2 },
                                 new[] { _2095_1, _2095_2 },
                                 new[] { _4450_1, _4450_2 },
                                 version,
                                 GetImage);
        }
    }
}
using System;
using TitanBot.Commands;
using static TT2Bot.TT2Localisation;

namespace TT2Bot.Models
{
    class HelperSkill : GameEntity<int>
    {
        public int HelperId { get; }
        public BonusType BonusType { get; }
        public double Magnitude { get; }
        public int RequiredLevel { get; }
        public string FileVersion { get; }

        public HelperSkill(int skillId, int helperId, BonusType bonusType, double magnitude, int requiredLevel, string fileVersion)
        {
            Id = skillId;
            HelperId = helperId;
            BonusType = bonusType;
            Magnitude = magnitude;
            RequiredLevel = requiredLevel;
            FileVersion = fileVersion;
        }

        protected override LocalisedString GetName(int id)
            => Game.HelperSkill.GetName(id);
    }
}

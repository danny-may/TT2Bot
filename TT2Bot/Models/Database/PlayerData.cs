using TitanBot.Storage;

namespace TT2Bot.Models
{
    class PlayerData : IDbRecord
    {
        public ulong Id { get; set; }
        public string PlayerCode { get; set; }
        public double Relics { get; set; }
        public int TapsPerCQ { get; set; }
        public int AttacksPerWeek { get; set; }
        public int MaxStage { get; set; }
        public bool CanGHSubmit { get; set; } = true;
        public bool CanAddExcuse { get; set; } = true;
        public bool CanBotSubmit { get; set; } = true;
        public int Eevees { get; set; }
        public int CritEevees { get; set; }
    }
}

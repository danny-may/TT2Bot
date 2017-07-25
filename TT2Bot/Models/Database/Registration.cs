using System;
using TitanBot.Storage;

namespace TT2Bot.Models
{
    class Registration : IDbRecord
    {
        public ulong Id { get; set; }
        public ulong? GuildId { get; set; }
        public ulong UserId { get; set; }
        public string Message { get; set; } = null;
        public Uri[] Images { get; set; }
        public DateTime ApplyTime { get; set; } = DateTime.Now;
        public DateTime EditTime { get; set; } = DateTime.Now;
    }
}

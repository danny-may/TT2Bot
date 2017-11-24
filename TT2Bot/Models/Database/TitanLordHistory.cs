using System;
using System.Collections.Generic;
using TitanBot.Storage;

namespace TT2Bot.Models.Database
{
    public class TitanLordHistory : IDbRecord
    {
        public ulong Id { get; set; }
        public List<DateTime> SpawnTimes { get; set; } = new List<DateTime>();
    }
}
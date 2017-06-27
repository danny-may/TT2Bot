using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanBot.Storage;

namespace TT2Bot.Models
{
    class Excuse : IDbRecord
    {
        public ulong Id { get; set; }
        public ulong CreatorId { get; set; }
        public string ExcuseText { get; set; }
        public DateTime SubmissionTime { get; set; }
        public bool Removed { get; set; }
    }
}

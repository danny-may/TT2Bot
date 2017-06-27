using LiteDB;
using System;

namespace Conversion.OldModels
{
    class Excuse
    {
        [BsonId]
        public int ExcuseNo { get; set; }
        public ulong CreatorId { get; set; }
        public string ExcuseText { get; set; }
        public DateTime SubmissionTime { get; set; }
    }
}

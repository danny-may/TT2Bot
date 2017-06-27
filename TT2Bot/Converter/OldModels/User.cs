using LiteDB;

namespace Conversion.OldModels
{
    class User
    {
        [BsonId]
        public ulong DiscordId { get; set; }
        public string SupportCode { get; set; }
        public bool CanSubmit { get; set; } = true;
    }
}

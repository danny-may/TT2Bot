using LiteDB;

namespace Conversion.OldModels
{
    class CmdPerm
    {
        [BsonId]
        public int entryId { get; set; }
        public ulong guildId { get; set; }
        public string commandname { get; set; }
        public ulong[] roleIds { get; set; }
        public ulong[] blackListed { get; set; }
        public ulong? permissionId { get; set; }
    }
}

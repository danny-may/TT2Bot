using Newtonsoft.Json.Linq;

namespace TT2Bot.Models
{
    class TitanLordTimerData
    {
        public ulong MessageId { get; set; }
        public ulong MessageChannelId { get; set; }
        public int GroupId { get; set; }

        public static TitanLordTimerData FromJson(string text)
        {
            var json = JObject.Parse(text);

            ulong mId = 0;
            ulong mCId = 0;
            int gId = 0;
            if (json.TryGetValue("MessageId", out var mIdToken))
                mId = (ulong)mIdToken;
            if (json.TryGetValue("MessageChannelId", out var mCIdToken))
                mCId = (ulong)mCIdToken;
            if (json.TryGetValue("GroupId", out var gIdToken))
                gId = (int)gIdToken;

            return new TitanLordTimerData
            {
                MessageId = mId,
                MessageChannelId = mCId,
                GroupId = gId
            };
        }
    }
}

using Newtonsoft.Json;
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
            try
            {
                return JsonConvert.DeserializeObject<TitanLordTimerData>(text);
            }
            catch
            {
                return JsonConvert.DeserializeObject<TitanLordTimerData>(JsonConvert.DeserializeObject<string>(text));
            }
        }
    }
}

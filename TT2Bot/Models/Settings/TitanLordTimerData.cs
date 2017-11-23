using Newtonsoft.Json;

namespace TT2Bot.Models
{
    internal class TitanLordTimerData
    {
        public ulong MessageId { get; set; }
        public ulong MessageChannelId { get; set; }
        public int GroupId { get; set; }

        public static TitanLordTimerData FromJson(string text)
        {
            try
            {
                return JsonConvert.DeserializeObject<TitanLordTimerData>(JsonConvert.DeserializeObject<string>(text));
            }
            catch
            {
                return JsonConvert.DeserializeObject<TitanLordTimerData>(text);
            }
        }
    }
}
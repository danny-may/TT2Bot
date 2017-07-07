using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TitanBot.Scheduling;
using TT2Bot.Models;

namespace TT2Bot.Helpers
{
    public static class TitanLordExtensions
    {
        public static string Contextualise(this string message, int cq, ISchedulerRecord timer, DateTime eventTime, string dateTimeFormat)
        {
            var CQ = cq;
            var user = timer.UserId;
            var remaining = 0;
            var completesAt = timer.StartTime;
            var round = (int)(2 + (eventTime - timer.StartTime).TotalSeconds / timer.Interval.TotalSeconds);

            // Handle %COMPLETE% message
            message = ReplaceUniversalTime(message, completesAt.ToUniversalTime(), dateTimeFormat);

            return message.Replace("%CQ%", CQ.ToString())
                .Replace("%USER%", $"<@{user}>")
                .Replace("%TIME%", remaining.ToString())
                .Replace("%ROUND%", round.ToString());
        }

        public static string ReplaceUniversalTime(this string msg, DateTime universalEventTime, string dateTimeFormat)
        {
            var regex = new Regex(@"(%COMPLETE)(?<time>[\+\-](\d|1[0-2]))?%");

            // Contains a valid %COMPLETE% format?
            var match = regex.Match(msg);
            if (!match.Success)
                return msg;

            var time = match.Groups["time"].ToString();
            if (!int.TryParse(time, out int val))
                return msg;

            var newTime = universalEventTime.AddHours(val);
            var strToReplace = newTime.ToString(dateTimeFormat) + $" (UTC{time})"; // TODO: Add setting property for display type
            return regex.Replace(msg, strToReplace);
        }
    }
}

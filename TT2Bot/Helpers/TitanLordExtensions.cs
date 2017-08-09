using System;
using System.Text.RegularExpressions;
using TitanBot.Scheduling;

namespace TT2Bot.Helpers
{
    public static class TitanLordExtensions
    {
        public static string Contextualise(this string message, int cq, ISchedulerRecord timer, DateTime eventTime, string dateTimeFormat)
        {
            var user = timer.UserId;
            var remaining = timer.EndTime - eventTime;
            var round = (int)(2 + (eventTime - timer.StartTime).TotalSeconds / timer.Interval.TotalSeconds);

            // Handle %COMPLETE% message
            message = ReplaceUniversalTime(message, timer.EndTime, dateTimeFormat);

            return message.Replace("%CQ%", cq.ToString())
                .Replace("%USER%", $"<@{user}>")
                .Replace("%TIME%", remaining.ToString())
                .Replace("%ROUND%", round.ToString());
        }

        public static string ReplaceUniversalTime(this string msg, DateTime eventTime, string dateTimeFormat)
        {
            var regex = new Regex(@"(%COMPLETE)(?<time>[\+\-](\d|1[0-2]))?%");

            // Contains a valid format?
            var matches = regex.Matches(msg);
            if (matches.Count <= 0)
                return msg;

            var timeZone = TimeZone.CurrentTimeZone;
            var timeZoneOffset = timeZone.GetUtcOffset(eventTime);  // There might be a better way of getting UTC time of a DateTime
            var utcEventTime = eventTime.Add(-timeZoneOffset);                      // but have not found one, and this will make it independant of
                                                                                    // where this code is run from.

            // Take Daylight Savings into account
            if (timeZone.IsDaylightSavingTime(eventTime))
            {
                utcEventTime = utcEventTime.AddHours(1);
            }

            // Replace for all matches of %COMPLETE%
            foreach (Match match in matches)
            {
                var time = match.Groups["time"].ToString();
                if (!int.TryParse(time, out int hour))
                {
                    msg = msg.Replace(match.ToString(), $"{utcEventTime.ToString(dateTimeFormat)}");
                    continue;
                }

                var newTime = utcEventTime.AddHours(hour);
                msg = msg.Replace(match.ToString(), $"{newTime.ToString(dateTimeFormat)} (UTC{time})");
            }

            return msg;
        }
    }
}

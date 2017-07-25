using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanBot.Scheduling;
using TT2Bot.Models;

namespace TT2Bot.Helpers
{
    public static class TimerExtensions
    {
        // Convers the IEnumerable to a TimeSpan in the form {hours, minutes, seconds}
        public static TimeSpan ConvertToTimeSpam(this IEnumerable<int> args)
        {
            var array = args as int[] ?? args.ToArray();
            if (array.Length != 3)
                throw new ArgumentException("Converting to TimeSpan only takes 3 arguments");
            
            return new TimeSpan(array[0], array[1], array[2]);
        }
    }
}

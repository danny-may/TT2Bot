using System;
using TitanBot.Logging;

namespace TT2Bot
{
    class ConsoleLogger : Logger
    {
        protected override LogSeverity LogLevel => LogSeverity.Critical | LogSeverity.Error | LogSeverity.Info | LogSeverity.Verbose;

        protected override void WriteLog(ILoggable entry)
        {
            if (!ShouldLog(entry.Severity))
                return;
            Console.Out.WriteLineAsync(entry.ToString());
            base.WriteLog(entry);
        }
    }
}

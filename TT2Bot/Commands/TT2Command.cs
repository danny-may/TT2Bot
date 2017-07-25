using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanBot.Commands;
using TT2Bot.Models;

namespace TT2Bot.Commands
{
    abstract class TT2Command : Command
    {
        protected TT2GlobalSettings TT2Global => GlobalSettings.Get<TT2GlobalSettings>();
    }
}

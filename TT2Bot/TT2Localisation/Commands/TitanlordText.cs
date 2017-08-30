using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class CommandText
        {
            public static class TitanLordText
            {
                public const string BASE_PATH = CommandText.BASE_PATH + "TITANLORD_";

                public const string TIMER_TOOLONG = BASE_PATH + nameof(TIMER_TOOLONG);
                public const string TIMER_LOADING = BASE_PATH + nameof(TIMER_LOADING);
                public const string TIMER_SET = BASE_PATH + nameof(TIMER_SET);
                public const string NOW_SUCCESS = BASE_PATH + nameof(NOW_SUCCESS);
                public const string WHEN_NORUNNING = BASE_PATH + nameof(WHEN_NORUNNING);
                public const string WHEN_RUNNING = BASE_PATH + nameof(WHEN_RUNNING);
                public const string STOP_SUCCESS = BASE_PATH + nameof(STOP_SUCCESS);
                public const string STOP_SUCCESS_GROUP = BASE_PATH + nameof(STOP_SUCCESS_GROUP);
                public const string NEWBOSS_EMBED_TITLE = BASE_PATH + nameof(NEWBOSS_EMBED_TITLE);
                public const string NEWBOSS_EMBED_CQ = BASE_PATH + nameof(NEWBOSS_EMBED_CQ);
                public const string NEWBOSS_EMBED_BONUS = BASE_PATH + nameof(NEWBOSS_EMBED_BONUS);
                public const string NEWBOSS_EMBED_HP = BASE_PATH + nameof(NEWBOSS_EMBED_HP);
                public const string NEWBOSS_EMBED_TTK = BASE_PATH + nameof(NEWBOSS_EMBED_TTK);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { TIMER_TOOLONG, "You cannot set a timer for longer than 6 hours" },
                        { TIMER_LOADING, "Loading timer...\n_If this takes longer than 20s please let Titansmasher know_" },
                        { TIMER_SET, "Set a timer running for {0}" },
                        { NOW_SUCCESS,"Ill let everyone know" },
                        { WHEN_NORUNNING, "There is no currently active Titan Lord timer running" },
                        { WHEN_RUNNING, "There will be a Titan Lord in {0}" },
                        { STOP_SUCCESS, "All currently running Titan Lord timers have been stopped" },
                        { STOP_SUCCESS_GROUP, "The Titan Lord timer for {0} has been stopped" },
                        { NEWBOSS_EMBED_TITLE, "Titan Lord data updated!" },
                        { NEWBOSS_EMBED_CQ,"New Clan Quest" },
                        { NEWBOSS_EMBED_BONUS, "New bonus" },
                        { NEWBOSS_EMBED_HP, "Next Titan Lord HP" },
                        { NEWBOSS_EMBED_TTK, "Time to kill" }
                    }.ToImmutableDictionary();
            }
        }
    }
}
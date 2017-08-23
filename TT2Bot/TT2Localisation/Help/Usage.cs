using System.Collections.Generic;
using System.Collections.Immutable;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Help
        {
            public static class Usage
            {
                public const string BASE_PATH = Help.BASE_PATH + "USAGE_";

                public const string REPORT = BASE_PATH + nameof(REPORT);
                public const string SUGGEST = BASE_PATH + nameof(SUGGEST);
                public const string APPLY_REGISTER_GUILD = BASE_PATH + nameof(APPLY_REGISTER_GUILD);
                public const string APPLY_REGISTER_GLOBAL = BASE_PATH + nameof(APPLY_REGISTER_GLOBAL);
                public const string APPLY_VIEW_GUILD = BASE_PATH + nameof(APPLY_VIEW_GUILD);
                public const string APPLY_VIEW_GLOBAL = BASE_PATH + nameof(APPLY_VIEW_GLOBAL);
                public const string APPLY_VIEW_USER = BASE_PATH + nameof(APPLY_VIEW_USER);
                public const string APPLY_CANCEL_GUILD = BASE_PATH + nameof(APPLY_CANCEL_GUILD);
                public const string APPLY_CANCEL_GLOBAL = BASE_PATH + nameof(APPLY_CANCEL_GLOBAL);
                public const string APPLY_CANCEL_USER = BASE_PATH + nameof(APPLY_CANCEL_USER);
                public const string APPLY_IGNORE = BASE_PATH + nameof(APPLY_IGNORE);
                public const string APPLY_LIST = BASE_PATH + nameof(APPLY_LIST);
                public const string APPLY_CLEAR = BASE_PATH + nameof(APPLY_CLEAR);
                public const string EXCUSE = BASE_PATH + nameof(EXCUSE);
                public const string EXCUSE_ADD = BASE_PATH + nameof(EXCUSE_ADD);
                public const string EXCUSE_REMOVE = BASE_PATH + nameof(EXCUSE_REMOVE);
                public const string SUBMIT_BUG = BASE_PATH + nameof(SUBMIT_BUG);
                public const string SUBMIT_SUGGESTION = BASE_PATH + nameof(SUBMIT_SUGGESTION);
                public const string SUBMIT_QUESTION = BASE_PATH + nameof(SUBMIT_QUESTION);
                public const string SUBMIT_BLOCK = BASE_PATH + nameof(SUBMIT_BLOCK);
                public const string SUBMIT_UNBLOCK = BASE_PATH + nameof(SUBMIT_UNBLOCK);
                public const string SUBMIT_REPLY = BASE_PATH + nameof(SUBMIT_REPLY);
                public const string SUBMIT_LIST = BASE_PATH + nameof(SUBMIT_LIST);
                public const string SUBMIT_SHOW = BASE_PATH + nameof(SUBMIT_SHOW);
                public const string TITANLORD_IN = BASE_PATH + nameof(TITANLORD_IN);
                public const string TITANLORD_DEAD = BASE_PATH + nameof(TITANLORD_DEAD);
                public const string TITANLORD_NOW = BASE_PATH + nameof(TITANLORD_NOW);
                public const string TITANLORD_WHEN = BASE_PATH + nameof(TITANLORD_WHEN);
                public const string TITANLORD_INFO = BASE_PATH + nameof(TITANLORD_INFO);
                public const string TITANLORD_STOP = BASE_PATH + nameof(TITANLORD_STOP);
                public const string ARTIFACT_LIST = BASE_PATH + nameof(ARTIFACT_LIST);
                public const string ARTIFACT_BUDGET = BASE_PATH + nameof(ARTIFACT_BUDGET);
                public const string ARTIFACT = BASE_PATH + nameof(ARTIFACT);
                public const string CLAIM = BASE_PATH + nameof(CLAIM);
                public const string CLANSTATS = BASE_PATH + nameof(CLANSTATS);
                public const string EQUIPMENT = BASE_PATH + nameof(EQUIPMENT);
                public const string EQUIPMENT_LIST = BASE_PATH + nameof(EQUIPMENT_LIST);
                public const string HELPER_LIST = BASE_PATH + nameof(HELPER_LIST);
                public const string HELPER = BASE_PATH + nameof(HELPER);
                public const string HIGHSCORE = BASE_PATH + nameof(HIGHSCORE);
                public const string PET = BASE_PATH + nameof(PET);
                public const string PET_LIST = BASE_PATH + nameof(PET_LIST);
                public const string PRESTIGE = BASE_PATH + nameof(PRESTIGE);
                public const string SKILL = BASE_PATH + nameof(SKILL);
                public const string SKILL_SHOW = BASE_PATH + nameof(SKILL_SHOW);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                = new Dictionary<string, string>
                {
                    { REPORT, "Sends a bug report to my home guild." },
                    { SUGGEST, "Sends a suggestion to my home guild." },
                    { APPLY_REGISTER_GUILD, "Creates your application for this clan" },
                    { APPLY_REGISTER_GLOBAL, "Creates a global application" },
                    { APPLY_VIEW_GUILD, "Views your registration for this guild" },
                    { APPLY_VIEW_GLOBAL, "Views your global registration" },
                    { APPLY_VIEW_USER, "View the registration for the given user" },
                    { APPLY_CANCEL_GUILD, "Cancels your registration for this guild" },
                    { APPLY_CANCEL_GLOBAL, "Cancels your global registration" },
                    { APPLY_CANCEL_USER, "Removes the registration for the given user" },
                    { APPLY_IGNORE, "Specifies if a users global registrations should be ignored. Defaults to yes" },
                    { APPLY_LIST, "Lists all applications for this guild" },
                    { APPLY_CLEAR, "Completely clears your guilds application list" },
                    { EXCUSE, "Gets an excuse for why that person (or yourself) didnt attack the boss" },
                    { EXCUSE_ADD, "Adds an excuse to the pool of available excuses" },
                    { EXCUSE_REMOVE, "Removes an excuse you made by ID" },
                    { SUBMIT_BUG, "Submits a bug to the GH team" },
                    { SUBMIT_SUGGESTION, "Submits a suggestion to the GH team" },
                    { SUBMIT_QUESTION, "Submits a question to the GH team" },
                    { SUBMIT_BLOCK, "Blocks a user from being able to use the submit command" },
                    { SUBMIT_UNBLOCK, "Unblocks a user, allowing them to use the submit command" },
                    { SUBMIT_REPLY, "Replies to a given submission" },
                    { SUBMIT_LIST, "Lists all unanswered submissions" },
                    { SUBMIT_SHOW, "Pulls the text for any submission" },
                    { TITANLORD_IN, "Sets a Titan Lord timer running for the given period." },
                    { TITANLORD_DEAD, "Sets a Titan Lord timer running for 6 hours." },
                    { TITANLORD_NOW, "Alerts everyone that the Titan Lord is ready to be killed right now" },
                    { TITANLORD_WHEN, "Gets the time until the Titan Lord is ready to be killed" },
                    { TITANLORD_INFO, "Gets information about the clans current level" },
                    { TITANLORD_STOP, "Stops any currently running timers." },
                    { ARTIFACT_LIST, "Lists all artifacts available." },
                    { ARTIFACT_BUDGET, "Shows you what the maximum level you can get an artifact to is with a given relic budget" },
                    { ARTIFACT, "Shows stats for a given artifact on the given levels." },
                    { CLAIM, "Claims a support code as your own." },
                    { CLANSTATS, "Shows data about a clan with the given level" },
                    { EQUIPMENT_LIST, "Lists all equipment for the given type" },
                    { EQUIPMENT, "Shows stats for a given equipment on the given level." },
                    { HELPER_LIST, "Lists all heros available" },
                    { HELPER, "Shows stats for a given hero on the given level" },
                    { HIGHSCORE, "Shows the people in the specified range. Defaults to 1-30" },
                    { PET, "Shows stats for a given pet on the given level" },
                    { PET_LIST, "Lists all pets available" },
                    { PRESTIGE, "Shows various stats about prestiging on the given stage" },
                    { SKILL, "Lists all available skills currently in the game" },
                    { SKILL_SHOW, "Shows you all the relevant information about the given skill" }
                }.ToImmutableDictionary();
            }
        }
    }
}

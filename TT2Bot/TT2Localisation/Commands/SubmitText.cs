using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT2Bot
{
    public static partial class TT2Localisation
    {
        public static partial class Commands
        {
            public static class SubmitText
            {
                private const string BASE_PATH = Commands.BASE_PATH + "SUBMIT_";
                
                public const string BLOCKED = BASE_PATH + nameof(BLOCKED);
                public const string MISSING_CHANNEL = BASE_PATH + nameof(MISSING_CHANNEL);
                public const string MISSING_DESCRIPTION = BASE_PATH + nameof(MISSING_DESCRIPTION);
                public const string IMAGE_TOOMANY = BASE_PATH + nameof(IMAGE_TOOMANY);
                public const string IMAGE_INVALID = BASE_PATH + nameof(IMAGE_INVALID);
                public const string REDDIT_INVALID = BASE_PATH + nameof(REDDIT_INVALID);
                public const string SUCCESS = BASE_PATH + nameof(SUCCESS);
                public const string BLOCK_SUCCESS = BASE_PATH + nameof(BLOCK_SUCCESS);
                public const string UNBLOCK_SUCCESS = BASE_PATH + nameof(UNBLOCK_SUCCESS);
                public const string REPLY_MISSINGID = BASE_PATH + nameof(REPLY_MISSINGID);
                public const string REPLY_ALERT = BASE_PATH + nameof(REPLY_ALERT);
                public const string REPLY_SUCCESS = BASE_PATH + nameof(REPLY_SUCCESS);
                public const string LIST_NONE = BASE_PATH + nameof(LIST_NONE);
                public const string LIST_HEADERS = BASE_PATH + nameof(LIST_HEADERS);
                public const string LIST_ROW = BASE_PATH + nameof(LIST_ROW);
                public const string LIST_TABLEFORMAT = BASE_PATH + nameof(LIST_TABLEFORMAT);
                public const string SHOW_DMSUCCESS = BASE_PATH + nameof(SHOW_DMSUCCESS);

                public static IReadOnlyDictionary<string, string> Defaults { get; }
                    = new Dictionary<string, string>
                    {
                        { BLOCKED, "You have been blocked from using this command. Please contact one of the admins on the /r/TapTitans2 discord if you think this is a mistake." },
                        { MISSING_CHANNEL, "I could not locate where to send the {0}! Please try again later." },
                        { MISSING_DESCRIPTION, "Please make sure you provide a description using the `-d` flag!" },
                        { IMAGE_TOOMANY, "You cannot specify an image flag if you attach an image!" },
                        { IMAGE_INVALID, "The image you supplied is not a valid image!" },
                        { REDDIT_INVALID, "The URL you gave was not a valid reddit URL" },
                        { SUCCESS, "Your {0} has been successfully sent!" },
                        { BLOCK_SUCCESS, "Successfully blocked {0} from using the submit command." },
                        { UNBLOCK_SUCCESS, "Successfully unblocked {0} from using the submit command." },
                        { REPLY_MISSINGID,"There is no submission by that ID" },
                        { REPLY_ALERT, "Your recent {0} titled `{1}` has just been replied to:\n\n{2}\n - {3}" },
                        { REPLY_SUCCESS,"Reply has been accepted!" },
                        { LIST_NONE, "There are no unanswered submissions!" },
                        { LIST_HEADERS, "id,Title,Type,Submitter" },
                        { LIST_ROW, "{0},{1},[{2}],{3} ({4})" },
                        { LIST_TABLEFORMAT, "```css\n{0}```" },
                        { SHOW_DMSUCCESS, "Ill DM you the submission!" }
                    }.ToImmutableDictionary();
            }
        }
    }
}

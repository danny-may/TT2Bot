using System;
using TitanBot.Formatting;
using TitanBot.Storage;
using static TT2Bot.TT2Localisation.Enums.SubmissionTypeText;

namespace TT2Bot.Models
{
    class TT2Submission : IDbRecord
    {
        public ulong Id { get; set; }
        public ulong Submitter { get; set; }
        public ulong? Answerer { get; set; }
        public SubmissionType Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Response { get; set; }
        public Uri ImageUrl { get; set; }
        public Uri Reddit { get; set; }
        public ulong? Message { get; set; }
        public DateTime SubmissionTime { get; set; }
        public DateTime? ReplyTime { get; set; }

        public enum SubmissionType
        {
            Bug = 0,
            Suggestion = 1,
            Question = 2
        }
    }

    static class SubmissionTypeMethods
    {
        public static LocalisedString ToLocalisable(this TT2Submission.SubmissionType type)
        {
            switch (type)
            {
                case TT2Submission.SubmissionType.Bug: return (LocalisedString)BUG;
                case TT2Submission.SubmissionType.Question: return (LocalisedString)QUESTION;
                case TT2Submission.SubmissionType.Suggestion: return (LocalisedString)SUGGESTION;
            }
            return (RawString)type.ToString();
        }
    }
}

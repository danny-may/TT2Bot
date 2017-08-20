using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TitanBot;
using TitanBot.Commands;
using TitanBot.Formatting;
using TitanBot.Replying;
using TT2Bot.Models;
using static TT2Bot.TT2Localisation.CommandText;
using static TT2Bot.TT2Localisation.Help;

#pragma warning disable 1587
/// <summary>
/// Old command used for the official server.
/// Archived because of lack of usage.
/// </summary>
#pragma warning restore 1587
namespace TT2Bot.Commands.Clan
{
    [DoNotInstall]
    [Description(Desc.SUBMIT)]
    [RequireGuild(169160979744161793, 312312366391885824)]
    class SubmitCommand : TT2Command
    {
        private IMessageChannel SubmissionChannel => (Client.GetChannel(TT2Global.GHFeedbackChannel) as IMessageChannel);

        Embedable GetSubmissionMessage(TT2Submission submission)
        {
            var color = System.Drawing.Color.Pink.ToDiscord();
            switch (submission.Type)
            {
                case TT2Submission.SubmissionType.Bug:
                    color = System.Drawing.Color.Red.ToDiscord();
                    break;
                case TT2Submission.SubmissionType.Question:
                    color = System.Drawing.Color.SkyBlue.ToDiscord();
                    break;
                case TT2Submission.SubmissionType.Suggestion:
                    color = System.Drawing.Color.Green.ToDiscord();
                    break;
            }

            IUser submitter = Client.GetUser(submission.Submitter);
            IUser replier = null;
            if (submission.Answerer != null)
                replier = Client.GetUser(submission.Answerer.Value);

            var builder = new EmbedBuilder
            {
                Color = color,
                Title = $"{submission.Type} #{submission.Id}: {submission.Title}",
                Description = submission.Description,
                Timestamp = submission.SubmissionTime
            };

            if (submitter != null)
                builder.WithAuthor(submitter);
            if (submission.Reddit != null)
                builder.AddField("Reddit link", submission.Reddit);
            if (submission.ImageUrl != null)
                builder.AddField("Image", submission.ImageUrl);
            if (string .IsNullOrWhiteSpace(submission.Response))
                builder.AddField("GH reply", "No reply yet!");
            else
                builder.AddField("GH reply", submission.Response);

            if (replier != null)
                builder.WithFooter(new EmbedFooterBuilder
                {
                    IconUrl = replier.GetAvatarUrl(),
                    Text = $"{replier} | Replied {submission.ReplyTime}"
                });

            return builder;
        }

        async Task SubmitAsync(string title, TT2Submission.SubmissionType type, string description, Uri image, Uri reddit)
        {
            var canSubmit = (await Database.FindById<PlayerData>(Author.Id))?.CanGHSubmit ?? true;
            if (!canSubmit)
                await ReplyAsync(SubmitText.BLOCKED, ReplyType.Error, type);
            else if (SubmissionChannel == null)
                await ReplyAsync(SubmitText.MISSING_CHANNEL, ReplyType.Error);
            else if (string.IsNullOrWhiteSpace(description))
                await ReplyAsync(SubmitText.MISSING_DESCRIPTION);
            else
            {
                var urlString = Message.Attachments.FirstOrDefault(a => Regex.IsMatch(a.Url, TT2Global.ImageRegex))?.Url;
                Uri imageUrl = null;
                if (urlString != null)
                    if (image != null)
                    {
                        await ReplyAsync(SubmitText.IMAGE_TOOMANY, ReplyType.Error);
                        return;
                    }
                    else
                        imageUrl = new Uri(urlString);
                else if (image != null && !Regex.IsMatch(image.AbsoluteUri, TT2Global.ImageRegex))
                {
                    await ReplyAsync(SubmitText.IMAGE_INVALID, ReplyType.Error);
                    return;
                }
                else if (image != null)
                    imageUrl = image;


                Uri redditUrl = null;
                if (reddit != null && !Regex.IsMatch(reddit.AbsoluteUri, @"^https?:\/\/(www.)?redd(.it|it.com)\/.*$"))
                {
                    await ReplyAsync(SubmitText.REDDIT_INVALID, ReplyType.Error);
                    return;
                }

                var submission = new TT2Submission
                {
                    Description = description,
                    Title = title,
                    ImageUrl = imageUrl,
                    Reddit = redditUrl,
                    Submitter = Author.Id,
                    Type = type,
                    SubmissionTime = DateTime.Now
                };

                await Database.Insert(submission);

                var message = await Reply(SubmissionChannel).WithEmbedable(GetSubmissionMessage(submission))
                                                            .SendAsync();

                submission.Message = message.Id;
                await Database.Upsert(submission);

                await ReplyAsync(SubmitText.SUCCESS, ReplyType.Success, type.ToLocalisable());
            }
        }

        async Task BlockUserAsync(IUser target, bool block)
        {
            var user = await Database.FindById<PlayerData>(target.Id) ?? new PlayerData
            {
                Id = target.Id
            };

            user.CanGHSubmit = !block;

            await Database.Upsert(user);

            await ReplyAsync(block ? SubmitText.BLOCK_SUCCESS : SubmitText.UNBLOCK_SUCCESS, ReplyType.Success, target.Username);
        }

        [Call("Bug")]
        [Usage(Usage.SUBMIT_BUG)]
        Task SubmitBugAsync([Dense]string title,
            [CallFlag('d', "description", Flag.SUBMIT_D)]string description = null,
            [CallFlag('i', "image", Flag.SUBMIT_I)]Uri image = null,
            [CallFlag('r', "reddit", Flag.SUBMIT_R)]Uri reddit = null)
            => SubmitAsync(title, TT2Submission.SubmissionType.Bug, description, image, reddit);

        [Call("Suggestion")]
        [Usage(Usage.SUBMIT_SUGGESTION)]
        Task SubmitFeatureAsync([Dense]string title,
            [CallFlag('d', "description", Flag.SUBMIT_D)]string description = null,
            [CallFlag('i', "image", Flag.SUBMIT_I)]Uri image = null,
            [CallFlag('r', "reddit", Flag.SUBMIT_R)]Uri reddit = null)
            => SubmitAsync(title, TT2Submission.SubmissionType.Suggestion, description, image, reddit);

        [Call("Question")]
        [Usage(Usage.SUBMIT_QUESTION)]
        Task SubmitQuestionAsync([Dense]string title,
            [CallFlag('d', "description", Flag.SUBMIT_D)]string description = null,
            [CallFlag('i', "image", Flag.SUBMIT_I)]Uri image = null,
            [CallFlag('r', "reddit", Flag.SUBMIT_R)]Uri reddit = null)
            => SubmitAsync(title, TT2Submission.SubmissionType.Question, description, image, reddit);

        [Call("Block")]
        [Usage(Usage.SUBMIT_BLOCK)]
        [DefaultPermission(8)]
        Task BlockUserAsync([Dense]IUser user)
            => BlockUserAsync(user, true);

        [Call("Unblock")]
        [Usage(Usage.SUBMIT_UNBLOCK)]
        [DefaultPermission(8)]
        Task UnblockUserAsync([Dense]IUser user)
            => BlockUserAsync(user, false);

        [Call("Reply")]
        [Usage(Usage.SUBMIT_REPLY)]
        [DefaultPermission(8)]
        async Task ReplySubmissionAsync(ulong id, [Dense]string reply,
            [CallFlag('q', "quiet", Flag.SUBMIT_Q)]bool quiet = false)
        {
            var submission = await Database.FindById<TT2Submission>(id);
            if (submission == null)
            {
                await ReplyAsync(SubmitText.REPLY_MISSINGID, ReplyType.Error);
                return;
            }

            var alreadyReplied = submission.Response != null;

            submission.ReplyTime = DateTime.Now;
            submission.Response = reply;
            submission.Answerer = Author.Id;

            await Database.Upsert(submission);

            if (submission.Message != null && await SubmissionChannel.GetMessageAsync(submission.Message.Value) is IUserMessage message)
                await Modify(message).ChangeEmbedable(GetSubmissionMessage(submission)).ModifyAsync();

            var submitter = Client.GetUser(submission.Submitter);
            if (submitter != null && !alreadyReplied && !quiet)
                await Reply(await submitter.GetOrCreateDMChannelAsync(), submitter).WithMessage(new LocalisedString(SubmitText.REPLY_ALERT, ReplyType.Info, submission.Type, submission.Title, reply, Author))
                                                                        .SendAsync();

            await ReplyAsync(SubmitText.REPLY_SUCCESS, ReplyType.Success);
        }

        [Call("List")]
        [Usage(Usage.SUBMIT_LIST)]
        [DefaultPermission(8)]
        async Task ListAsync(TT2Submission.SubmissionType[] type = null)
        {
            if (type == null)
                type = new TT2Submission.SubmissionType[]
                {
                    TT2Submission.SubmissionType.Bug,
                    TT2Submission.SubmissionType.Question,
                    TT2Submission.SubmissionType.Suggestion
                };

            var unanswered = await Database.Find<TT2Submission>(s => s.Response == null);
            unanswered = unanswered.Where(s => type.Contains(s.Type));

            if (unanswered.Count() == 0)
            {
                await ReplyAsync(SubmitText.LIST_NONE, ReplyType.Info);
                return;
            }

            var table = new List<string[]> { TextResource.GetResource(SubmitText.LIST_HEADERS).Split(',') };
            foreach (var submission in unanswered)
            {
                var user = Client.GetUser(submission.Submitter);
                table.Add(TextResource.Format(SubmitText.LIST_ROW,
                                              submission.Id.ToString(),
                                              submission.Title.Substring(0, 50),
                                              $"[{submission.Type}]",
                                              user?.Username ?? TextResource.GetResource(TBLocalisation.UNKNOWNUSER),
                                              user?.Id ?? submission.Id).Split(',')
                );
            }

            await ReplyAsync(SubmitText.LIST_TABLEFORMAT, table.ToArray().Tableify());
        }

        [Call("Show")]
        [Usage(Usage.SUBMIT_SHOW)]
        [DefaultPermission(8)]
        async Task ShowAsync(ulong id)
        {
            var submission = await Database.FindById<TT2Submission>(id);
            if (submission == null)
                await ReplyAsync(SubmitText.REPLY_MISSINGID, ReplyType.Error);
            else
            {
                await ReplyAsync(SubmitText.SHOW_DMSUCCESS, ReplyType.Success);
                await Reply(await Author.GetOrCreateDMChannelAsync()).WithEmbedable(GetSubmissionMessage(submission))
                                                                     .SendAsync();
            }
        }
    }
}

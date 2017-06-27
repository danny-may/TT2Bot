using Conversion.OldModels;
using LiteDB;
using System;
using System.Linq;
using TitanBot;
using TitanBot.Storage;
using TitanBot.Scheduling;
using TitanBot.Settings;
using TT2Bot.Models;
using TT2Bot.Callbacks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Conversion
{
    class Converter
    {
        LiteDatabase Database { get; }
        IDatabase BotDb { get; }
        ISettingsManager Settings { get; }
        IScheduler Scheduler { get; }
        public Converter(string dbLocation, BotClient client)
        {
            Database = new LiteDatabase(dbLocation);
            BotDb = client.Database;
            Settings = client.SettingsManager;
            Scheduler = client.Scheduler;
        }

        public void Convert()
        {
            ConvertSettings();
            ConvertTimers();
            ConvertExcuses();
            ConvertCmdPerms();
            ConvertRegistrations();
            ConvertSubmissions();
            ConvertUsers();
        }

        private void ConvertUsers()
        {
            var oldUsers = Database.GetCollection<User>().FindAll().ToArray();
            var newUsers = oldUsers.Select(u => new PlayerData
            {
                PlayerCode = u.SupportCode,
                CanAddExcuse = true,
                CanGHSubmit = u.CanSubmit,
                CanBotSubmit = true
            });
            BotDb.Upsert(newUsers).Wait();
        }

        private void ConvertSubmissions()
        {
            var oldSubmissions = Database.GetCollection<OldModels.TT2Submission>().FindAll().ToArray();
            var newSubmissions = oldSubmissions.Select(s => new TT2Bot.Models.TT2Submission
            {
                Answerer = s.Answerer,
                Description = s.Description,
                Id = (ulong)s.Id,
                ImageUrl = s.ImageUrl,
                Message = s.Message,
                Reddit = s.Reddit,
                ReplyTime = s.ReplyTime,
                Response = s.Response,
                SubmissionTime = s.SubmissionTime,
                Submitter = s.Submitter,
                Title = s.Title,
                Type = (TT2Bot.Models.TT2Submission.SubmissionType)s.Type
            });
            BotDb.Upsert(newSubmissions).Wait();
        }

        private void ConvertRegistrations()
        {
            var oldRegistrations = Database.GetCollection<OldModels.Registration>().FindAll().ToArray();
            var users = oldRegistrations.GroupBy(r => r.UserId).Select(u => new PlayerData
            {
                AttacksPerWeek = u.Max(r => r.CQPerWeek),
                Id = u.Key,
                MaxStage = u.Max(r => r.MaxStage),
                Relics = u.Max(r => r.Relics),
                TapsPerCQ = u.Max(r => r.Taps)
            });
            var registrations = oldRegistrations.Select(r => new TT2Bot.Models.Registration
            {
                ApplyTime = r.ApplyTime,
                EditTime = r.EditTime,
                GuildId = r.GuildId,
                Images = r.Images,
                Message = r.Message,
                UserId = r.UserId
            });

            BotDb.Upsert(users).Wait();
            BotDb.Upsert(registrations).Wait();
        }

        private void ConvertCmdPerms()
        {
            var oldPerms = Database.GetCollection<CmdPerm>().FindAll().ToArray();
            var newPerms = oldPerms.Select(p => new TitanBot.Storage.Tables.CallPermission
            {
                Blacklisted = p.blackListed,
                CallName = p.commandname,
                GuildId = p.guildId,
                Permission = p.permissionId,
                Roles = p.roleIds
            });
            BotDb.Upsert(newPerms).Wait();
        }

        private void ConvertExcuses()
        {
            var oldexcuses = Database.GetCollection<OldModels.Excuse>().FindAll().ToArray();
            var newExcuses = oldexcuses.Select(e => new TT2Bot.Models.Excuse
            {
                CreatorId = e.CreatorId,
                ExcuseText = e.ExcuseText,
                Id = (ulong)e.ExcuseNo,
                Removed = false,
                SubmissionTime = e.SubmissionTime
            });
            BotDb.Upsert(newExcuses).Wait();
        }

        public void ConvertTimers()
        {
            var oldTimers = Database.GetCollection<Timer>().FindAll().ToArray();
            var restart = Scheduler.IsRunning;
            if (Scheduler.IsRunning)
                Scheduler.StopAsync().Wait();
            foreach (var timer in oldTimers)
            {
                if (!timer.Active)
                    continue;

                var data = JsonConvert.SerializeObject(new TitanLordTimerData
                {
                    MessageChannelId = timer.CustArgs["timerMessageId"]?.ToObject<ulong>() ?? 0,
                    MessageId = timer.CustArgs["timerMessageChannelId"]?.ToObject<ulong>() ?? 0,
                });

                switch (timer.Callback.ToString())
                {
                    case "TitanLordTick":
                        Scheduler.Queue<TitanLordTickCallback>(timer.UserId, timer.GuildId, timer.From, new TimeSpan(0, 0, timer.SecondInterval), timer.To, data);
                        break;
                    case "TitanLordNow":
                        continue;
                    case "TitanLordRound":
                        Scheduler.Queue<TitanLordRoundCallback>(timer.UserId, timer.GuildId, timer.From, new TimeSpan(0, 0, timer.SecondInterval), timer.To, data);
                        break;
                    default:
                        continue;
                }
            }

            if (restart)
                Scheduler.StartAsync().Wait();
        }

        public void ConvertSettings()
        {
            var oldSettings = Database.GetCollection<Guild>().FindAll().ToArray();
            foreach (var settings in oldSettings)
            {
                Settings.SaveGroup(settings.GuildId, new RegistrationSettings
                {
                    IgnoreList = settings.RegisterIgnore.ToList()
                });
                Settings.SaveGroup(settings.GuildId, new GeneralSettings
                {
                    BlackListed = settings.BlackListed,
                    PermOverride = settings.PermOverride,
                    Prefix = settings.Prefix
                });
                Settings.SaveGroup(settings.GuildId, new TitanLordSettings
                {
                    Channel = settings.TitanLord.Channel,
                    CQ = settings.TitanLord.CQ,
                    InXText = settings.TitanLord.InXText,
                    NowText = settings.TitanLord.NowText,
                    PinTimer = settings.TitanLord.PinTimer,
                    PrePings = settings.TitanLord.PrePings,
                    RoundPings = settings.TitanLord.RoundPings,
                    RoundText = settings.TitanLord.RoundText,
                    TimerText = settings.TitanLord.TimerText
                });
            }
        }
    }
}

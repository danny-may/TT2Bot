﻿using System;
using System.Linq;
using TT2Bot.Models.General;

namespace TT2Bot.Helpers
{
    public static class Calculator
    {
        private static int[] BossAttackCosts { get; } = new int[] { 0, 5, 25, 50, 75, 100, 125, 150 };

        public static double TitanLordHp(int clanQuest)
            => Math.Pow(10, 5) * (15 + 9 * (Math.Pow(Math.Min(clanQuest - 1, 650), 0.9) * Math.Pow(Math.Max(((double)clanQuest - 1) / 650, 1), 1.4)));

        public static double ClanBonus(int clanQuest)
            => Math.Pow(1.1, Math.Min(clanQuest, 200)) *
               Math.Pow(1.05, Math.Max(clanQuest - 200, 0));

        public static int AttackCost(int attackNo)
            => BossAttackCosts[Math.Min(attackNo, BossAttackCosts.Length - 1)];

        public static int TotalAttackCost(int attacks)
            => Enumerable.Range(0, attacks).Sum(v => AttackCost(v));

        public static Percentage AdvanceStart(int clanQuest)
            => Math.Min(0.5, Math.Min((double)clanQuest, 200) / 1000 + Math.Max((double)clanQuest - 200, 0) / 2000);

        public static int AttacksNeeded(int clanLevel, int attackers, int maxStage, int tapsPerAttack)
            => (int)Math.Ceiling((TitanLordHp(clanLevel) / attackers) / (Math.Max(50, maxStage) * tapsPerAttack + Math.Max(50, maxStage) * 90 * 2));

        public static int RelicsEarned(int stage, int bosLevel)
            => (int)Math.Floor((1 + bosLevel * 0.05) * Math.Pow((((double)stage - 75) / 14), 1.75));

        public static int TitansOnStage(int stage, int ipLevel)
            => Math.Max(1, (stage / 1000) * 4 + 10 - ipLevel);

        public static TimeSpan RunTime(int from, int to, int ipLevel, int splashAmount)
            => new TimeSpan(0, 0, (int)Math.Ceiling((to - from) + Enumerable.Range(from, to - from).Sum(s => TitansOnStage(s, ipLevel) / splashAmount + TitansOnStage(s, ipLevel) % splashAmount) * 0.4));
    }
}

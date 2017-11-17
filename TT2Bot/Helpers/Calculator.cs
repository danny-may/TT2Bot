using System;
using System.Linq;
using TT2Bot.Models.General;

namespace TT2Bot.Helpers
{
    public static class Calculator
    {
        private static int[] BossAttackCosts { get; } = { 0, 5, 25, 50, 75, 100, 125, 150 };

        public static double TitanLordHp(int clanQuest)
            => Math.Pow(10, 5) * Math.Pow(clanQuest, Math.Pow(clanQuest, 0.028));

        public static double ClanBonus(int clanQuest)
            => Math.Pow(1.0233, clanQuest) + Math.Pow(clanQuest, 1.05);

        public static int AttackCost(int attackNo)
            => BossAttackCosts[Math.Min(attackNo, BossAttackCosts.Length - 1)];

        public static int TotalAttackCost(int attacks)
            => Enumerable.Range(0, attacks).Sum(AttackCost);

        public static Percentage AdvanceStart(int clanQuest)
            => Math.Min(0.003 * Math.Pow(Math.Log(clanQuest + 4), 2.692), 0.9);

        public static int AttacksNeeded(int clanLevel, int attackers, int maxStage, int tapsPerAttack)
            => (int)Math.Ceiling((TitanLordHp(clanLevel) / attackers) / (Math.Max(50, maxStage) * tapsPerAttack + Math.Max(50, maxStage) * 90 * 2));

        public static int RelicsEarned(int stage, int bosLevel)
        {
            var baseRelics = (int) Math.Max(0.0, Math.Round(
                3 * Math.Pow(1.21, Math.Pow(stage, 0.48))
                + 1.5 * (stage - 110)
                + Math.Pow(1.002, Math.Pow(stage, 1.005 * (Math.Pow(stage, 1.1) * 5e-7 + 1)))
            ));
            var bosEffect = 1.0 + 0.05 * Math.Pow(bosLevel, 1 + 1.5 * Math.Min(2.5e-5 * bosLevel, 0.015));
            return (int) (baseRelics * bosEffect);
        }

        public static int TitansOnStage(int stage, int ipLevel)
            => Math.Max(1, (stage / 500) * 2 + 10 - ipLevel);

        public static TimeSpan RunTime(int from, int to, int ipLevel, int splashAmount)
            => new TimeSpan(0, 0, (int)Math.Ceiling((to - from) + Enumerable.Range(from, to - from).Sum(s => TitansOnStage(s, ipLevel) / splashAmount + TitansOnStage(s, ipLevel) % splashAmount) * 0.4));
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT2Bot.Models.TT2
{
    class ImprovementCollection
    {
        private Dictionary<int, Improvement> Improvements { get; } = new Dictionary<int, Improvement>();

        public ImprovementCollection(Dictionary<int, double> values)
        {
            var mult = 1d;
            foreach (var val in values)
            {
                mult *= val.Value;
                Improvements[val.Key] = new Improvement(val.Key, mult);
            }
        }

        public Improvement Get(int level)
        {
            if (!Improvements.ContainsKey(level))
                Improvements[level] = new Improvement(level, Improvements.LastOrDefault(i => i.Key < level).Value.Multiplier);
            return Improvements[level];
        }

        public Improvement this[int level]
            => Get(level);

        public struct Improvement
        {
            public int Level { get; }
            public double Multiplier { get; }

            internal Improvement(int level, double muliplier)
            {
                Level = level;
                Multiplier = muliplier;
            }
        }
    }
}

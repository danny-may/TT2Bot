using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT2Bot.Models.General
{
    public struct Percentage
    {
        private decimal Value { get; }

        public Percentage(decimal value)
            => Value = value;

        public override string ToString()
        {
            return $"{Value}%";
        }

        public static Percentage operator +(Percentage v1, Percentage v2)
            => new Percentage(v1.Value + v2.Value / 100);
        public static Percentage operator -(Percentage v1, Percentage v2)
            => new Percentage(v1.Value - v2.Value / 100);
        public static Percentage operator /(Percentage v1, Percentage v2)
            => new Percentage(v1.Value / v2.Value / 100);
        public static Percentage operator *(Percentage v1, Percentage v2)
            => new Percentage(v1.Value * v2.Value / 100);

        public static implicit operator decimal(Percentage value)
            => value.Value / 100;
        public static implicit operator double(Percentage value)
            => (double)value.Value / 100;
        public static implicit operator float(Percentage value)
            => (float)value.Value / 100;
        public static implicit operator int(Percentage value)
            => (int)value.Value / 100;
        public static implicit operator Percentage(decimal value)
            => new Percentage(value * 100);
        public static implicit operator Percentage(double value)
            => new Percentage((decimal)value * 100);
        public static implicit operator Percentage(float value)
            => new Percentage((decimal)value * 100);
        public static implicit operator Percentage(int value)
            => new Percentage((decimal)value * 100);
    }
}

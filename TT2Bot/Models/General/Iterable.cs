using System;
using System.Collections;
using System.Collections.Generic;

namespace TT2Bot.Models.General
{
    public class Iterable<T> : IEnumerator<T>
    {
        private IEnumerator<T> _enumerator;

        public Iterable(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
        }

        public Iterable(IEnumerable<T> enumerable) : this(enumerable.GetEnumerator())
        {
        }

        public void Skip(int count)
        {
            for (int i = 0; i < count; i++)
                MoveNext();
        }

        public T Next()
        {
            if (MoveNext())
                return Current;
            throw new IndexOutOfRangeException();
        }

        public T Current => _enumerator.Current;

        object IEnumerator.Current => _enumerator.Current;

        public void Dispose()
        {
            _enumerator.Dispose();
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator.Reset();
        }
    }
}
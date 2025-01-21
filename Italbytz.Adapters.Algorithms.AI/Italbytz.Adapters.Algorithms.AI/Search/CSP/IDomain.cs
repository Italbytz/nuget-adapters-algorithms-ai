using System;
using System.Collections.Generic;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP;

public interface IDomain<TVal> : IEnumerable<TVal>, IEquatable<IDomain<TVal>>
{
    public TVal[] Values { get; }

    public IList<TVal> AsList => new List<TVal>(Values);

    public int Count => Values.Length;

    public TVal this[int index] => Values[index];

    public bool IsEmpty => Values.Length == 0;
}
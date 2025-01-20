using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP;

public class Domain<TVal> : IDomain<TVal>
{
    public TVal[] Values { get; }
    
    public Domain(IEnumerable<TVal> values)
    {
        this.Values = values.ToArray();
    }

    public Domain(params TVal[] values)
    {
        this.Values = values;
    } 
    
    
    
    public TVal this[int index] => Values[index];
    
    public bool Contains(TVal value) => Values.Contains(value);
    
    public bool IsEmpty => Values.Length == 0;
    
    public TVal Get(int index) => Values[index];
    
    public IEnumerator<TVal> GetEnumerator()
    {
        return ((IEnumerable<TVal>)Values).GetEnumerator();
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public bool Equals(IDomain<TVal>? other)
    {
        return other != null && Values.Equals(other.Values);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Domain<TVal>)obj);
    }

    public override string ToString()
    {
        return string.Join(", ", Values);
    }

    public override int GetHashCode()
    {
        return Values.GetHashCode();
    }
    
    
}
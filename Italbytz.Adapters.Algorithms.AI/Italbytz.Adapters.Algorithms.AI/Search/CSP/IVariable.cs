using System;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP;

public interface IVariable : IEquatable<IVariable>
{
    public string Name { get;  }
}
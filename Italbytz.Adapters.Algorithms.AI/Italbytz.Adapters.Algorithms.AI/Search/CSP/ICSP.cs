using System.Collections.Generic;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP;

public interface ICSP<TVar,TVal> where TVar : IVariable
{
    IEnumerable<IConstraint<TVar,TVal>> Constraints { get;  }
    IEnumerable<TVar> Variables { get;  }
    IEnumerable<IDomain<TVal>> Domains { get; }
}
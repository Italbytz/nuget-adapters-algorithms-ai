using System.Collections.Generic;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP;

public class CSP<TVar, TVal> : ICSP<TVar, TVal> where TVar : IVariable
{
    public IEnumerable<IConstraint<TVar, TVal>> Constraints { get; }
    public IEnumerable<TVar> Variables { get; }
    public IEnumerable<IDomain<TVal>> Domains { get; }
}
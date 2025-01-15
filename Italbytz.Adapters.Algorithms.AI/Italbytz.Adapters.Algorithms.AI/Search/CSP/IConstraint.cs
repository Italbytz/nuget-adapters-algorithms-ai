using System.Collections.Generic;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP;

public interface IConstraint<TVar,TVal> where TVar : IVariable
{
    public IEnumerable<TVar> Scope { get; }
    public bool IsSatisfiedWith(IAssignment<TVar,TVal> assignment);
}
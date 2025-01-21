using Italbytz.Ports.Algorithms.AI.Search.CSP;
using Italbytz.Ports.Algorithms.AI.Search.CSP.Solver;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP.Solver;

public abstract class AbstractCspSolver<TVar, TVal> : ICspSolver<TVar, TVal>
    where TVar : IVariable
{
    public abstract IAssignment<TVar, TVal>? Solve(ICSP<TVar, TVal> csp);
}
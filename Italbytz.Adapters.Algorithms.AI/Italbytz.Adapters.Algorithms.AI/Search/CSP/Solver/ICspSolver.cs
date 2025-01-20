namespace Italbytz.Adapters.Algorithms.AI.Search.CSP.Solver;

public interface ICspSolver<TVar,TVal> where TVar : IVariable
{
    public IAssignment<TVar,TVal>? Solve(ICSP<TVar,TVal> csp);
}
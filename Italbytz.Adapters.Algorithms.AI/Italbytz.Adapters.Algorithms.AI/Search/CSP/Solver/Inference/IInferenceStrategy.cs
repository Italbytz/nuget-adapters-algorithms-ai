namespace Italbytz.Adapters.Algorithms.AI.Search.CSP.Solver.Inference;

public interface IInferenceStrategy<TVar,TVal> where TVar : IVariable
{
    public IInferenceLog<TVar, TVal> Apply(ICSP<TVar, TVal> csp);
    public IInferenceLog<TVar, TVal> Apply(ICSP<TVar, TVal> csp, IAssignment<TVar, TVal> assignment, TVar var);
}
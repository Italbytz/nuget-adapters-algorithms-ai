namespace Italbytz.Adapters.Algorithms.AI.Search.CSP.Solver.Inference;

public interface IInferenceLog<TVar,TVal> where TVar : IVariable
{
    public bool IsEmpty { get; }
    public bool InconsistencyFound { get; }
    public void Undo(ICSP<TVar, TVal> csp);
    
}
using System.Collections.Generic;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP.Examples;

public class NotEqualConstraint<TVar, TVal> : IConstraint<TVar, TVal> where TVar : IVariable
{
    private TVar _var1;
    private TVar _var2;
    
    public NotEqualConstraint(TVar var1, TVar var2)
    {
        _var1 = var1;
        _var2 = var2;
        Scope = new List<TVar> { var1, var2 };
    }

    public IList<TVar> Scope { get; }
    public bool IsSatisfiedWith(IAssignment<TVar, TVal> assignment)
    {
        var value1 = assignment.GetValue(_var1);
        var value2 = assignment.GetValue(_var2);
        return value1 == null || value2 == null || !value1.Equals(value2);
    }
}
using System;
using System.Collections.Generic;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP;

public interface IAssignment<TVar,TVal> : ICloneable
    where TVar : IVariable 
    
{
    public IEnumerable<TVar> Variables { get; }
    public TVal? GetValue(TVar variable);

    public void Add(TVar variable, TVal value);

    public void Remove(TVar variable);

    public bool Contains(TVar variable);

    public bool IsConsistent(IEnumerable<IConstraint<TVar, TVal>> constraints);

    public bool IsComplete(IEnumerable<TVar> variables);

    public bool IsSolution(ICSP<TVar, TVal> csp);
}
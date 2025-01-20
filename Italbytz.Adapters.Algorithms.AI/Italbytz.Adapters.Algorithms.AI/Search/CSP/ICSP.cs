using System;
using System.Collections.Generic;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP;

public interface ICSP<TVar,TVal> where TVar : IVariable
{
    IList<IConstraint<TVar,TVal>> Constraints { get;  }
    IList<TVar> Variables { get;  }
    IList<IDomain<TVal>> Domains { get; set; }

    public void AddConstraint(IConstraint<TVar, TVal> constraint);
    
    public void AddVariable(TVar var);
    
    public IDomain<TVal> GetDomain(TVar var);
    
    public IList<IConstraint<TVar, TVal>> GetConstraints(TVar var);
    
    public TVar GetNeighbor(TVar var, IConstraint<TVar, TVal> constraint);
    
    public bool RemoveConstraint(IConstraint<TVar, TVal> constraint);
    
    public void SetDomain(TVar var, IDomain<TVal> domain);

    public ICSP<TVar, TVal> CopyDomains();
    public bool RemoveValueFromDomain(TVar variable, TVal value);
}
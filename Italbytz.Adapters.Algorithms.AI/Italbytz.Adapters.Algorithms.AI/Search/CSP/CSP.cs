using System.Collections.Generic;
using System.Linq;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP;

public class CSP<TVar, TVal> : ICSP<TVar, TVal> where TVar : IVariable
{
    public IList<IConstraint<TVar, TVal>> Constraints { get; }
    public IList<TVar> Variables { get; }
    public IList<IDomain<TVal>> Domains { get; set; }

    private readonly Dictionary<IVariable, IList<IConstraint<TVar, TVal>>> cnet;
    
    public CSP(IList<TVar> variables, IList<IDomain<TVal>> domains, IList<IConstraint<TVar, TVal>> constraints)
    {
        Variables = variables;
        Domains = domains;
        Constraints = constraints;
        cnet = new Dictionary<IVariable, IList<IConstraint<TVar, TVal>>>();
    }
    
    public CSP(IList<TVar> variables) : this(variables, new List<IDomain<TVal>>(), new List<IConstraint<TVar, TVal>>())
    {
        variables.ToList().ForEach(var => AddVariable(var));
    }

    protected CSP() : this(new List<TVar>())
    {
    }

    public void AddVariable(TVar var)
    {
        if (cnet.ContainsKey(var)) throw new System.ArgumentException("Variable already exists in CSP");
        Variables.Add(var);
        Domains.Add(new Domain<TVal>(new List<TVal>()));
        cnet.Add(var, new List<IConstraint<TVar, TVal>>());
        
    }

    public void AddConstraint(IConstraint<TVar, TVal> constraint)
    {
        Constraints.Add(constraint);
        constraint.Scope.ToList().ForEach(var => cnet[var].Add(constraint));
    }
    
    public bool RemoveConstraint(IConstraint<TVar, TVal> constraint)
    {
        if (!Constraints.Remove(constraint)) return false;
        constraint.Scope.ToList().ForEach(var => cnet[var].Remove(constraint));
        return true;
    }

    public void SetDomain(TVar var, IDomain<TVal> domain)
    {
        Domains[Variables.IndexOf(var)] = domain;
    }

    public IList<IConstraint<TVar, TVal>> GetConstraints(TVar var)
    {
        return cnet[var];
    }
    
    public TVar GetNeighbor(TVar var, IConstraint<TVar, TVal> constraint)
    {
        if (constraint.Scope.Count != 2) throw new System.ArgumentException("Constraint must involve exactly two variables");
        return constraint.Scope.First(v => !v.Equals(var));
    }
    
    public IDomain<TVal> GetDomain(TVar var)
    {
        return Domains[Variables.IndexOf(var)];
    }

    public ICSP<TVar, TVal> CopyDomains()
    {
        var copy = (ICSP<TVar, TVal>)this.MemberwiseClone(); 
        copy.Domains = new List<IDomain<TVal>>(Domains);
        return copy;
    }

    public bool RemoveValueFromDomain(TVar variable, TVal value)
    {
        var currentDomain = GetDomain(variable);
        var values = currentDomain.Where(val => !val.Equals(value)).ToList();
        if (values.Count == currentDomain.Count) return false;
        SetDomain(variable, new Domain<TVal>(values));
        return true;
    }




}
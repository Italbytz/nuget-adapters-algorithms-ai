// The original version of this file is part of <see href="https://github.com/aimacode/aima-java"/> which is released under
// MIT License
// Copyright (c) 2015 aima-java contributors

using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP;

public class Assignment<TVar,TVal> : IAssignment<TVar,TVal> where TVar : IVariable
{
    private Dictionary<TVar,TVal> variableToValueMap = new Dictionary<TVar,TVal>();
    
    public IEnumerable<TVar> Variables => new List<TVar>(variableToValueMap.Keys);
    
    public TVal? GetValue(TVar variable)
    {
        return !variableToValueMap.TryGetValue(variable, out var value) ? default(TVal) : value;
    }
    
    public void Add(TVar variable, TVal value)
    {
        variableToValueMap.Add(variable, value);
    }
    
    public void Remove(TVar variable)
    {
        variableToValueMap.Remove(variable);
    }
    
    public bool Contains(TVar variable)
    {
        return variableToValueMap.ContainsKey(variable);
    }
    
    public bool IsConsistent(IEnumerable<IConstraint<TVar,TVal>> constraints)
    {
        return constraints.All(constraint => constraint.IsSatisfiedWith(this));
    }
    
    public bool IsComplete(IEnumerable<TVar> variables)
    {
        return variables.All(Contains);
    }
    
    public bool IsSolution(ICSP<TVar,TVal> csp)
    {
        return IsConsistent(csp.Constraints) && IsComplete(csp.Variables);
    }
    
    
    public object Clone()
    {
        var clonedAssignment = new Assignment<TVar,TVal>();
        foreach (var variable in variableToValueMap.Keys)
        {
            clonedAssignment.Add(variable, variableToValueMap[variable]);
        }
        return clonedAssignment;
    }
    
    public override string ToString()
    {
        return variableToValueMap.Keys.Aggregate("", (current, variable) => current + (variable + " = " + variableToValueMap[variable] + "\n"));
    }
    
    
    
}
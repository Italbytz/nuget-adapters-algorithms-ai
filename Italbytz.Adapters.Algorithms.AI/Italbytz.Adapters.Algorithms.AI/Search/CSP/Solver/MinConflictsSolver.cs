using System;
using System.Linq;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP.Solver;

// ToDo: This is Copilot generated code, please review it before using it in production. It differs from the AIMA Java implementation.
public class MinConflictsSolver<TVar, TVal> : AbstractCspSolver<TVar, TVal> where TVar : IVariable
{
    private readonly int _maxSteps;

    public MinConflictsSolver(int maxSteps)
    {
        _maxSteps = maxSteps;
    }

    public override IAssignment<TVar, TVal>? Solve(ICSP<TVar, TVal> csp)
    {
        var current = GenerateRandomAssignment(csp);
        for (var i = 0; i < _maxSteps; i++)
        {
            if (current.IsSolution(csp))
            {
                return current;
            }
            var variable = SelectVariableToChange(csp, current);
            var value = SelectValueForVariable(csp, variable, current);
            current.Add(variable, value);
        }
        return null;
    }

    private TVal SelectValueForVariable(ICSP<TVar, TVal> csp, TVar variable, IAssignment<TVar, TVal> current)
    {
        var domain = csp.GetDomain(variable);
        var minConflicts = int.MaxValue;
        TVal? bestValue = default(TVal);
        foreach (var value in domain)
        {
            current.Add(variable, value);
            var conflicts = csp.Constraints.Count(constraint => !constraint.IsSatisfiedWith(current));
            if (conflicts < minConflicts)
            {
                minConflicts = conflicts;
                bestValue = value;
            }
            current.Remove(variable);
        }
        return bestValue;
    }

    private TVar SelectVariableToChange(ICSP<TVar,TVal> csp, IAssignment<TVar, TVal> assignment)
    {
        var variables = csp.Variables.Where(variable => !assignment.Contains(variable));
        var variable = variables.ElementAt(new Random().Next(variables.Count()));
        return variable;
    }

    private IAssignment<TVar,TVal> GenerateRandomAssignment(ICSP<TVar,TVal> csp)
    {
        IAssignment<TVar,TVal> assignment = new Assignment<TVar,TVal>();
        foreach (var variable in csp.Variables)
        {
            assignment.Add(variable, csp.GetDomain(variable).ElementAt(new Random().Next(csp.GetDomain(variable).Count)));
        }
        return assignment;
    }
}

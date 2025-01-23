using System.Collections.Generic;
using Italbytz.Ports.Algorithms.AI.Logic.Fol.Parsing.Ast;

namespace Italbytz.Adapters.Algorithms.AI.Logic.Fol.Parsing.Ast;

public class Predicate : IPredicate
{
    public Predicate(string predicateName, IList<ITerm> terms)
    {
        SymbolicName = predicateName;
        Args = new List<ITerm>();
        foreach (var term in terms) Args.Add(term);
    }

    public string SymbolicName { get; }

    public IList<ITerm> Args { get; }

    public override string ToString()
    {
        return $"{SymbolicName}({string.Join(",", Args)})";
    }
}
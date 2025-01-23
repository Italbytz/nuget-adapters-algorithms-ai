using System.Collections.Generic;
using Italbytz.Adapters.Algorithms.AI.Logic.Fol.Parsing.Ast;

namespace Italbytz.Adapters.Algorithms.AI.Logic.Planning;

public class HighLevelAction : ActionSchema
{
    public HighLevelAction(string name, List<ITerm> variables,
        string precondition, string effect,
        List<List<IActionSchema>> refinements) : base(name, variables,
        precondition, effect)
    {
        Refinements = refinements;
    }

    public List<List<IActionSchema>> Refinements { get; }
}
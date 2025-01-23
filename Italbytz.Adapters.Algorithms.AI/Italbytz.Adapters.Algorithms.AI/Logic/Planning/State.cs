using System.Collections.Generic;
using System.Linq;
using Italbytz.Adapters.Algorithms.AI.Logic.Fol.Kb.Data;

namespace Italbytz.Adapters.Algorithms.AI.Logic.Planning;

public class State : IState
{
    public State(IList<ILiteral> fluents)
    {
        Fluents = fluents;
        Fluents = Fluents.OrderBy(f => f.ToString()).ToList();
    }

    public State(string fluents) : this(Utils.Parse(fluents))
    {
    }

    public IList<ILiteral> Fluents { get; }

    public IState Result(List<IActionSchema> actions)
    {
        var state = this;

        return actions.Aggregate(state,
            (current, action) => current.Result(action));
    }

    private State Result(IActionSchema action)
    {
        if (IsApplicable(action))
        {
            var result = new List<ILiteral>(Fluents);
            foreach (var effect in action.Effect)
                if (effect.PositiveLiteral)
                    result.Add(effect);
                else
                    result.Remove(effect.GetComplementaryLiteral());
            return new State(result.OrderBy(f => f.ToString()).ToList());
        }

        return this;
    }

    public bool IsApplicable(IActionSchema action)
    {
        return action.Precondition.All(literal => literal.PositiveLiteral
            ? Fluents.Contains(literal)
            : !Fluents.Contains(literal.GetComplementaryLiteral()));
    }
}
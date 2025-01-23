using System.Collections.Generic;
using Italbytz.Adapters.Algorithms.AI.Logic.Fol.Kb.Data;

namespace Italbytz.Adapters.Algorithms.AI.Logic.Planning;

public interface IPlanningProblem
{
    public IList<ILiteral> Goal { get; }
    public IState InitialState { get; }
    IEnumerable<IActionSchema> GetPropositionalisedActions();
}
using Italbytz.Ports.Algorithms.AI.Search.GP;
using Italbytz.Ports.Algorithms.AI.Search.GP.StoppingCriterion;

namespace Italbytz.Adapters.Algorithms.AI.Search.GP.StoppingCriterion;

/// <inheritdoc cref="IStoppingCriterion" />
public class GenerationStoppingCriterion(IGeneticProgram gp)
    : IStoppingCriterion
{
    public int Limit { get; set; }

    /// <inheritdoc />
    public bool IsMet()
    {
        return gp.Generation >= Limit;
    }
}
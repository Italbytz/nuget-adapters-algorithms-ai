using Italbytz.AI.Search.EA;
using Italbytz.AI.Search.EA.StoppingCriterion;

namespace Italbytz.AI.Search.GP.StoppingCriterion;

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
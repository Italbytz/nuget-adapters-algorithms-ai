using Italbytz.AI.Search.EA.Control;
using Italbytz.AI.Search.GP.StoppingCriterion;

namespace Italbytz.AI.Search.EA.StoppingCriterion;

/// <inheritdoc cref="IStoppingCriterion" />
public class GenerationStoppingCriterion(EvolutionaryAlgorithm schedule)
    : IStoppingCriterion
{
    public int Limit { get; set; } = 100;

    /// <inheritdoc />
    public bool IsMet()
    {
        return schedule.Generation >= Limit;
    }
}
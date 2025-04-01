using Italbytz.Adapters.Algorithms.AI.Search.GP.Individuals;
using Italbytz.Ports.Algorithms.AI.Search.GP.Individuals;
using Italbytz.Ports.Algorithms.AI.Search.GP.Selection;

namespace Italbytz.Adapters.Algorithms.AI.Search.GP.Selection;

/// <inheritdoc cref="ISelection" />
public class UniformSelection : ISelection
{
    /// <inheritdoc />
    public int Size { get; set; }

    /// <inheritdoc />
    public IIndividualList Process(IIndividualList individuals)
    {
        var result = new Population();
        var population = individuals;
        for (var i = 0; i < Size; i++)
            result.Add(population.GetRandomIndividual());
        return result;
    }
}
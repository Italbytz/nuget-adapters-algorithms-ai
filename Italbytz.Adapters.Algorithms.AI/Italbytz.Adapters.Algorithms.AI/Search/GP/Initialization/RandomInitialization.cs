using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.GP.Initialization;

/// <inheritdoc cref="IInitialization" />
public class RandomInitialization(IGeneticProgram gp) : IInitialization
{
    public int Size { get; set; }

    /// <inheritdoc />
    public IIndividualList Process(IIndividualList individuals)
    {
        var result = new Population();
        var searchSpace = gp.SearchSpace;
        for (var i = 0; i < Size; i++)
            result
                .Add(new Individual(searchSpace.GetRandomGenotype(),
                    null));
        return result;
    }
}
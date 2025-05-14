using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.GP.Initialization;

/// <inheritdoc cref="IInitialization" />
public class CompleteInitialization(IGeneticProgram gp) : IInitialization
{
    public int Size { get; set; }

    /// <inheritdoc />
    public IIndividualList Process(IIndividualList individuals)
    {
        var searchSpace = gp.SearchSpace;
        var population = searchSpace.GetAStartingPopulation();
        return population;
    }
}
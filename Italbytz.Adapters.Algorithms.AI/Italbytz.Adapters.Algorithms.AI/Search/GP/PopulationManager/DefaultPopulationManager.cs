using Italbytz.AI.Search.GP.Individuals;
using Italbytz.AI.Search.GP.Initialization;

namespace Italbytz.AI.Search.GP.PopulationManager;

/// <inheritdoc cref="IPopulationManager" />
public class DefaultPopulationManager : IPopulationManager
{
    /// <inheritdoc />
    public required IIndividualList Population { get; set; }

    /// <inheritdoc />
    public void InitPopulation(IInitialization initialization)
    {
        Population = initialization.Process(null);
    }
}
using Italbytz.Ports.Algorithms.AI.Search.GP.Individuals;
using Italbytz.Ports.Algorithms.AI.Search.GP.Initialization;
using Italbytz.Ports.Algorithms.AI.Search.GP.PopulationManager;

namespace Italbytz.Adapters.Algorithms.AI.Search.GP.PopulationManager;

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
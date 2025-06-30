using Italbytz.AI.Search.EA.Individuals;
using Italbytz.AI.Search.EA.Initialization;
using Italbytz.AI.Search.EA.PopulationManager;

namespace Italbytz.AI.Search.GP.PopulationManager;

/// <inheritdoc cref="IPopulationManager" />
public class DefaultPopulationManager : IPopulationManager
{
    /// <inheritdoc />
    public IIndividualList? Population { get; set; }

    /// <inheritdoc />
    public void InitPopulation(IInitialization initialization)
    {
        Population = initialization.Process(null, null).Result;
    }

    public string GetPopulationInfo()
    {
        return Population?.ToString() ?? "Population not initialized.";
    }
}
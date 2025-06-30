using System.Threading.Tasks;
using Italbytz.AI.Search.EA;
using Italbytz.AI.Search.EA.Fitness;
using Italbytz.AI.Search.EA.Individuals;
using Italbytz.AI.Search.EA.Initialization;

namespace Italbytz.AI.Search.GP.Initialization;

/// <inheritdoc cref="IInitialization" />
public class CompleteInitialization(IGeneticProgram gp) : IInitialization
{
    public int Size { get; set; }

    /// <inheritdoc />
    public Task<IIndividualList>? Process(Task<IIndividualList> individuals,
        IFitnessFunction fitnessFunction)
    {
        var searchSpace = gp.SearchSpace;
        var population = searchSpace.GetAStartingPopulation();
        return Task.FromResult(population);
    }
}
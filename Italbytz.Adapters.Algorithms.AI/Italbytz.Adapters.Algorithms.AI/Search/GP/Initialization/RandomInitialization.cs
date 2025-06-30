using System.Threading.Tasks;
using Italbytz.AI.Search.EA;
using Italbytz.AI.Search.EA.Fitness;
using Italbytz.AI.Search.EA.Individuals;
using Italbytz.AI.Search.EA.Initialization;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.GP.Initialization;

/// <inheritdoc cref="IInitialization" />
public class RandomInitialization(IGeneticProgram gp) : IInitialization
{
    public int Size { get; set; }

    /// <inheritdoc />
    public Task<IIndividualList>? Process(Task<IIndividualList> individuals,
        IFitnessFunction fitnessFunction)
    {
        var result = new Population();
        var searchSpace = gp.SearchSpace;
        for (var i = 0; i < Size; i++)
            result
                .Add(new Individual(searchSpace.GetRandomGenotype(),
                    null));
        return Task.FromResult<IIndividualList>(result);
    }
}
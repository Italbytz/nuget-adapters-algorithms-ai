using System.Threading.Tasks;
using Italbytz.AI.Search.EA.Control;
using Italbytz.AI.Search.EA.Fitness;
using Italbytz.AI.Search.EA.Individuals;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Initialization;

public class RandomInitialization(EvolutionaryAlgorithm schedule)
    : IInitialization
{
    public int Size { get; set; } = 1;

    public Task<IIndividualList>? Process(Task<IIndividualList> individuals,
        IFitnessFunction fitnessFunction)
    {
        var result = new Population();
        var searchSpace = schedule.SearchSpace;
        for (var i = 0; i < Size; i++)
            result
                .Add(new Individual(searchSpace.GetRandomGenotype(),
                    null));
        return Task.FromResult<IIndividualList>(result);
    }
}
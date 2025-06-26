using Italbytz.AI.Search.EA.Control;
using Italbytz.AI.Search.GP.Individuals;
using Italbytz.AI.Search.GP.Initialization;

namespace Italbytz.AI.Search.EA.Initialization;

public class RandomInitialization(Schedule schedule) : IInitialization
{
    public int Size { get; set; } = 1;

    public IIndividualList Process(IIndividualList individuals)
    {
        var result = new Population();
        var searchSpace = schedule.SearchSpace;
        for (var i = 0; i < Size; i++)
            result
                .Add(new Individual(searchSpace.GetRandomGenotype(),
                    null));
        return result;
    }
}
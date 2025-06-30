using System.Threading.Tasks;
using Italbytz.AI.Search.EA.Fitness;
using Italbytz.AI.Search.EA.Individuals;
using Italbytz.AI.Search.EA.Selection;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.GP.Selection;

/// <inheritdoc cref="ISelection" />
public class UniformSelection : ISelection
{
    /// <inheritdoc />
    public int Size { get; set; }

    /// <inheritdoc />
    public Task<IIndividualList>? Process(Task<IIndividualList> individuals,
        IFitnessFunction fitnessFunction)
    {
        var result = new Population();
        var population = individuals.Result;
        for (var i = 0; i < Size; i++)
            result.Add(population.GetRandomIndividual());
        return Task.FromResult<IIndividualList>(result);
    }
}
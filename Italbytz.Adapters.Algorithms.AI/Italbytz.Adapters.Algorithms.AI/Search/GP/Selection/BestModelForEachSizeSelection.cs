using System;
using System.Linq;
using System.Threading.Tasks;
using Italbytz.AI.Search.EA.Fitness;
using Italbytz.AI.Search.EA.Individuals;
using Italbytz.AI.Search.EA.Selection;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.GP.Selection;

/// <inheritdoc cref="ISelection" />
public class BestModelForEachSizeSelection : ISelection
{
    /// <inheritdoc />
    public int Size { get; set; }

    /// <inheritdoc />
    public Task<IIndividualList>? Process(Task<IIndividualList> individuals,
        IFitnessFunction fitnessFunction)
    {
        var population = new Population();
        var individualList = individuals.Result.ToList();

        var groupedIndividuals =
            individualList.GroupBy(individual => individual.Size);
        foreach (var group in groupedIndividuals)
        {
            var bestIndividual = group.First();
            var bestFitness = 0;
            foreach (var individual in group)
            {
                var fitness =
                    (individual.LatestKnownFitness ?? Array.Empty<double>())
                    .Aggregate(0, (current, fitval) => (int)(current + fitval));
                if (fitness <= bestFitness) continue;
                bestFitness = fitness;
                bestIndividual = individual;
            }

            population.Add(bestIndividual);
        }

        return Task.FromResult<IIndividualList>(population);
    }
}
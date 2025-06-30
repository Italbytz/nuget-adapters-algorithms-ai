using System.Linq;
using System.Threading.Tasks;
using Italbytz.AI.Search.EA.Fitness;
using Italbytz.AI.Search.EA.Individuals;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Operator.Selection;

public class CutSelection : GraphOperator
{
    public override int MaxParents { get; } = int.MaxValue;
    public int NoOfIndividualsToSelect { get; } = 1;

    public override Task<IIndividualList> Operate(
        Task<IIndividualList> individuals, IFitnessFunction fitnessFunction)
    {
        var individualList = individuals.Result;
        var newPopulation = new Population();
        // Update LatestKnownFitness
        foreach (var individual in individualList)
            individual.LatestKnownFitness ??=
                fitnessFunction.Evaluate(individual, null);
        // Take the NoOfIndividualsToSelect best individuals according to LatestKnownFitness from individualList
        var bestIndividuals = individualList
            .OrderByDescending(i => i.LatestKnownFitness.Sum())
            .Take(NoOfIndividualsToSelect);
        foreach (var individual in bestIndividuals)
            newPopulation.Add(individual);
        return Task.FromResult<IIndividualList>(newPopulation);
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Italbytz.AI.Search.EA.Graph;
using Italbytz.AI.Search.GP.Fitness;
using Italbytz.AI.Search.GP.Individuals;
using Italbytz.AI.Search.GP.Initialization;
using Italbytz.AI.Search.GP.PopulationManager;
using Italbytz.AI.Search.GP.SearchSpace;
using Italbytz.AI.Search.GP.StoppingCriterion;

namespace Italbytz.AI.Search.EA.Control;

public class EvolutionaryAlgorithm
{
    public required IFitnessFunction FitnessFunction { get; set; }
    public required ISearchSpace SearchSpace { get; set; }
    public IInitialization? Initialization { get; set; }

    public IPopulationManager PopulationManager { get; set; } =
        new DefaultPopulationManager();

    public IIndividualList Population => PopulationManager.Population;

    public IStoppingCriterion[] StoppingCriteria { get; set; }
    public int Generation { get; set; }
    public required OperatorGraph AlgorithmGraph { get; set; }

    public async Task Run()
    {
        AlgorithmGraph.Check();
        AlgorithmGraph.FitnessFunction = FitnessFunction;
        Generation = 0;
        PopulationManager.InitPopulation(Initialization);
        var stop = false;
        while (!stop)
        {
            Console.Out.WriteLine(((DefaultPopulationManager)PopulationManager)
                .GetPopulationInfo());
            stop = StoppingCriteria.Any(sc => sc.IsMet());
            if (stop) continue;
            var newPopulation = await AlgorithmGraph.Process(Population);
            Generation++;
            foreach (var individual in newPopulation)
                individual.Generation = Generation;
            PopulationManager.Population = newPopulation;
        }
    }
}
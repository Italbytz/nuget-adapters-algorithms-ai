using Italbytz.AI.Search.EA.Control;
using Italbytz.AI.Search.EA.Fitness;
using Italbytz.AI.Search.EA.Graph.Common;
using Italbytz.AI.Search.EA.Initialization;
using Italbytz.AI.Search.EA.Searchspace;
using GenerationStoppingCriterion =
    Italbytz.AI.Search.EA.StoppingCriterion.GenerationStoppingCriterion;

namespace Italbytz.AI.Tests.Unit.Search.EA;

public class OnePlusOneEaGraphTests
{
    [Test]
    public async Task TestOnePlusOneEA()
    {
        var onePlusOneEA = new EvolutionaryAlgorithm
        {
            FitnessFunction = new OneMax(),
            SearchSpace = new BitString(),
            AlgorithmGraph = new OnePlusOneEAGraph()
        };
        onePlusOneEA.Initialization = new RandomInitialization(onePlusOneEA);
        onePlusOneEA.StoppingCriteria =
        [
            new GenerationStoppingCriterion(onePlusOneEA)
        ];
        await onePlusOneEA.Run();
    }
}
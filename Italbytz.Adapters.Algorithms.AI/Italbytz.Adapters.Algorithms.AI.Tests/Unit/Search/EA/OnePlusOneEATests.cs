using Italbytz.AI.Search.EA.Control;
using Italbytz.AI.Search.EA.Fitness;
using Italbytz.AI.Search.EA.Graph.Common;
using Italbytz.AI.Search.EA.Initialization;
using Italbytz.AI.Search.EA.Searchspace;
using GenerationStoppingCriterion =
    Italbytz.AI.Search.EA.StoppingCriterion.GenerationStoppingCriterion;

namespace Italbytz.AI.Tests.Unit.Search.EA;

public class OnePlusOneEATests
{
    [Test]
    public void TestOnePlusOneEA()
    {
        var schedule = new Schedule
        {
            FitnessFunction = new OneMax(),
            SearchSpace = new BitString(),
            AlgorithmGraph = new OnePlusOneEA()
        };
        schedule.Initialization = new RandomInitialization(schedule);
        schedule.StoppingCriteria =
        [
            new GenerationStoppingCriterion(schedule)
        ];
        schedule.Run();
    }
}
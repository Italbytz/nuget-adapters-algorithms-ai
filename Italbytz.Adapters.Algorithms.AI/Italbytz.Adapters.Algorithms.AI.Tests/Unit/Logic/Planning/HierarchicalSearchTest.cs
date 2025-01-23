using Italbytz.Adapters.Algorithms.AI.Logic.Planning;

namespace Italbytz.Adapters.Algorithms.AI.Tests.Unit.Logic;

public class HierarchicalSearchTest
{
    [Test]
    public void TestHierarchicalSearch()
    {
        var algo = new HierarchicalSearchAlgorithm();
        var taxiAction = new ActionSchema("Taxi", null,
            "At(Home)",
            "~At(Home)^At(SFO)");
        var result = algo.HierarchicalSearch(
            PlanningProblemFactory.GoHomeToSfoProblem());
        Assert.That(result, Is.Not.Null);
        foreach (var action in result)
            Assert.That(action, Is.EqualTo(taxiAction));
    }
}
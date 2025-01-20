using Italbytz.Adapters.Algorithms.AI.Search.CSP;
using Italbytz.Adapters.Algorithms.AI.Search.CSP.Examples;
using Italbytz.Adapters.Algorithms.AI.Search.CSP.Solver;

namespace Italbytz.Adapters.Algorithms.AI.Tests.Unit.Search.CSP;

public class MapCSPTest
{
    private ICSP<Variable, string> csp;
    
    [SetUp]
    public void SetUp()
    {
        csp = new MapCSP();
    }
    
    [Test]
    public void TestBackTrackingSearch()
    {
        var solver = new FlexibleBacktrackingSolver<Variable, string>();
        var assignment = solver.Solve(csp);
        Assert.That(assignment, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(assignment.GetValue(MapCSP.WA), Is.EqualTo(MapCSP.BLUE));
            Assert.That(assignment.GetValue(MapCSP.NT), Is.EqualTo(MapCSP.RED));
            Assert.That(assignment.GetValue(MapCSP.SA), Is.EqualTo(MapCSP.GREEN));
            Assert.That(assignment.GetValue(MapCSP.Q), Is.EqualTo(MapCSP.BLUE));
            Assert.That(assignment.GetValue(MapCSP.NSW), Is.EqualTo(MapCSP.RED));
            Assert.That(assignment.GetValue(MapCSP.V), Is.EqualTo(MapCSP.BLUE));
            Assert.That(assignment.GetValue(MapCSP.T), Is.EqualTo(MapCSP.RED));
        });

    }
}
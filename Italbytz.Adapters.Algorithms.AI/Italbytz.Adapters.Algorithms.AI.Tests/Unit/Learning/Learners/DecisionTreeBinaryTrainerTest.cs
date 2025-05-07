using Italbytz.Adapters.Algorithms.Tests.Unit.Learning.Learners;
using Microsoft.ML;

namespace Italbytz.Adapters.Algorithms.AI.Tests.Learning.Learners;

[TestFixture]
public class DecisionTreeBinaryTrainerTest
{
    [SetUp]
    public void Setup()
    {
        var mlContext = new MLContext();
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory,
            "Data", "restaurant_categories.csv");
        _data = mlContext.Data.LoadFromTextFile<RestaurantModelInput>(
            path,
            ',', true);
    }

    private IDataView _data;

    [Test]
    public void METHOD()
    {
    }
}
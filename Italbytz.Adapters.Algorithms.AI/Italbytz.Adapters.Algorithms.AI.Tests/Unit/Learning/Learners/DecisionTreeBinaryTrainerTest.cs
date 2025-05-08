using Italbytz.Adapters.Algorithms.AI.Learning.Learners;
using Italbytz.Adapters.Algorithms.Tests.Unit.Learning.Learners;
using Italbytz.ML;
using Microsoft.ML;
using Microsoft.ML.Data;

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

    private readonly LookupMap<uint>[] _lookupData =
    [
        new(0),
        new(1)
    ];

    [Test]
    public void TestInducedTreeClassifiesDataSetCorrectly()
    {
        var mlContext = ThreadSafeMLContext.LocalMLContext;
        var lookupIdvMap = mlContext.Data.LoadFromEnumerable(_lookupData);
        var trainer = new DecisionTreeBinaryTrainer();
        var pipeline = GetPipeline(trainer, lookupIdvMap);
        var model = pipeline.Fit(_data);
        var transformedData = model.Transform(_data);
        var metrics = mlContext.BinaryClassification
            .Evaluate(transformedData);
        Assert.Multiple(() =>
        {
            Assert.That(metrics.Accuracy, Is.EqualTo(1));
            Assert.That(metrics.AreaUnderRocCurve, Is.EqualTo(1));
            Assert.That(metrics.F1Score, Is.EqualTo(1));
            Assert.That(metrics.LogLoss, Is.EqualTo(0));
            Assert.That(metrics.LogLossReduction, Is.EqualTo(1));
            Assert.That(metrics.PositivePrecision, Is.EqualTo(1));
            Assert.That(metrics.PositiveRecall, Is.EqualTo(1));
            Assert.That(metrics.NegativePrecision, Is.EqualTo(1));
            Assert.That(metrics.NegativeRecall, Is.EqualTo(1));
        });
    }

    protected EstimatorChain<ITransformer> GetPipeline(
        IEstimator<ITransformer> trainer, IDataView lookupIdvMap)
    {
        var mlContext = ThreadSafeMLContext.LocalMLContext;
        var pipeline = mlContext.Transforms.ReplaceMissingValues(new[]
            {
                new InputOutputColumnPair(@"alternate", @"alternate"),
                new InputOutputColumnPair(@"bar", @"bar"),
                new InputOutputColumnPair(@"fri/sat", @"fri/sat"),
                new InputOutputColumnPair(@"hungry", @"hungry"),
                new InputOutputColumnPair(@"patrons", @"patrons"),
                new InputOutputColumnPair(@"price", @"price"),
                new InputOutputColumnPair(@"raining", @"raining"),
                new InputOutputColumnPair(@"reservation", @"reservation"),
                new InputOutputColumnPair(@"type", @"type"),
                new InputOutputColumnPair(@"wait_estimate", @"wait_estimate")
            })
            .Append(mlContext.Transforms.Concatenate(@"Features", @"alternate",
                @"bar", @"fri/sat", @"hungry", @"patrons", @"price", @"raining",
                @"reservation", @"type", @"wait_estimate"))
            .Append(mlContext.Transforms.Conversion.MapValueToKey(@"Label",
                @"will_wait", keyData: lookupIdvMap))
            .Append(trainer);

        return pipeline;
    }
}
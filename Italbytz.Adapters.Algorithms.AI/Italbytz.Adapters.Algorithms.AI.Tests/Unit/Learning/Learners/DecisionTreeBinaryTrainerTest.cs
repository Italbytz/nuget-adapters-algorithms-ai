using Italbytz.AI.Learning.Learners;
using Italbytz.AI.Tests.Data;
using Italbytz.ML;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Italbytz.AI.Tests.Unit.Learning.Learners;

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

    [Test]
    public void TestCustomMapper()
    {
        var mlContext = new MLContext();

        var samples = new List<InputData>
        {
            new() { Age = 16 },
            new() { Age = 35 },
            new() { Age = 60 },
            new() { Age = 28 }
        };

        var data = mlContext.Data.LoadFromEnumerable(samples);

        void Mapping(InputData input, CustomMappingOutput output)
        {
            output.AgeName = input.Age switch
            {
                < 18 => "Child",
                < 55 => "Man",
                _ => "Grandpa"
            };
        }

        var pipeline =
            mlContext.Transforms.CustomMapping(
                (Action<InputData, CustomMappingOutput>)Mapping, null);

        var transformer = pipeline.Fit(data);
        var transformedData = transformer.Transform(data);

        var dataEnumerable =
            mlContext.Data.CreateEnumerable<TransformedData>(transformedData,
                false);

        var dataArray = dataEnumerable.ToArray();

        Assert.That(dataArray, Has.Length.EqualTo(4));
        Assert.Multiple(() =>
        {
            Assert.That(dataArray[0].AgeName, Is.EqualTo("Child"));
            Assert.That(dataArray[1].AgeName, Is.EqualTo("Man"));
            Assert.That(dataArray[2].AgeName, Is.EqualTo("Grandpa"));
            Assert.That(dataArray[3].AgeName, Is.EqualTo("Man"));
        });
    }
}

internal class InputData
{
    public int Age { get; set; }
}

internal class CustomMappingOutput
{
    public string AgeName { get; set; }
}

internal class TransformedData
{
    public int Age { get; set; }

    public string AgeName { get; set; }
}
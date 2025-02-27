using Italbytz.Adapters.Algorithms.AI.Learning.Learners;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Italbytz.Adapters.Algorithms.Tests.Unit.Learning.Learners;

public class DecisionTreeBinaryTrainerTest
{
    private IDataView _data;

    [SetUp]
    public void Setup()
    {
        var mlContext = new MLContext();
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory,
            "Data", "restaurant_recoded.csv");
        _data = mlContext.Data.LoadFromTextFile<ModelInput>(
            path,
            ',', true);
    }

    [Test]
    public void TestCustomRestaurantMapper()
    {
        var mlContext = new MLContext();

        var samples = new List<ModelInput>
        {
            new()
            {
                Patrons = @"Some",
                Price = @"$$$",
                Type = @"French",
                Wait_estimate = @"0-10"
            }
        };

        var data = mlContext.Data.LoadFromEnumerable(samples);

        var pipeline =
            mlContext.Transforms.CustomMapping(
                (Action<ModelInput, ModelMapping>)Mapping, null);

        var transformer = pipeline.Fit(data);
        var transformedData = transformer.Transform(data);

        var dataEnumerable =
            mlContext.Data.CreateEnumerable<ModelOutput>(transformedData,
                false);

        var dataArray = dataEnumerable.ToArray();
        Assert.That(dataArray, Has.Length.EqualTo(1));
        Assert.That(dataArray[0].PredictedLabel, Is.EqualTo(1));
    }

    private void Mapping(ModelInput input, ModelMapping output)
    {
        output.Features = new float[11];
        output.PredictedLabel = 1;
        output.Score = new float[2];
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

    [Test]
    public void
        TestInducedTreeClassifiesDataSetCorrectly()
    {
        var mlContext = new MLContext();
        var trainer = new DecisionTreeBinaryTrainer<ModelInput>("will_wait");
        var transformer = trainer.Fit(_data);
        var transformedData = transformer.Transform(_data);

        var dataEnumerable =
            mlContext.Data.CreateEnumerable<ModelOutput>(transformedData,
                false);

        var dataArray = dataEnumerable.ToArray();
    }

    /// <summary>
    ///     model input class for Restaurant.
    /// </summary>

    #region model input class

    public class ModelInput
    {
        [LoadColumn(0)]
        [ColumnName(@"alternate")]
        public bool Alternate { get; set; }

        [LoadColumn(1)] [ColumnName(@"bar")] public bool Bar { get; set; }

        [LoadColumn(2)]
        [ColumnName(@"fri/sat")]
        public bool Fri_sat { get; set; }

        [LoadColumn(3)]
        [ColumnName(@"hungry")]
        public bool Hungry { get; set; }

        [LoadColumn(4)]
        [ColumnName(@"patrons")]
        public string Patrons { get; set; }

        [LoadColumn(5)] [ColumnName(@"price")] public string Price { get; set; }

        [LoadColumn(6)]
        [ColumnName(@"raining")]
        public bool Raining { get; set; }

        [LoadColumn(7)]
        [ColumnName(@"reservation")]
        public bool Reservation { get; set; }

        [LoadColumn(8)] [ColumnName(@"type")] public string Type { get; set; }

        [LoadColumn(9)]
        [ColumnName(@"wait_estimate")]
        public string Wait_estimate { get; set; }

        [LoadColumn(10)]
        [ColumnName(@"will_wait")]
        public float Will_wait { get; set; }
    }

    #endregion

    /// <summary>
    ///     model output class for Restaurant.
    /// </summary>

    #region model output class

    public class ModelOutput
    {
        [ColumnName(@"alternate")] public bool Alternate { get; set; }
        [ColumnName(@"bar")] public bool Bar { get; set; }
        [ColumnName(@"fri/sat")] public bool Fri_sat { get; set; }
        [ColumnName(@"hungry")] public bool Hungry { get; set; }
        [ColumnName(@"patrons")] public string Patrons { get; set; }
        [ColumnName(@"price")] public string Price { get; set; }
        [ColumnName(@"raining")] public bool Raining { get; set; }
        [ColumnName(@"reservation")] public bool Reservation { get; set; }
        [ColumnName(@"type")] public string Type { get; set; }
        [ColumnName(@"wait_estimate")] public string Wait_estimate { get; set; }
        [ColumnName(@"will_wait")] public float Will_wait { get; set; }
        [ColumnName(@"Features")] public float[] Features { get; set; }

        [ColumnName(@"PredictedLabel")]
        public float PredictedLabel { get; set; }

        [ColumnName(@"Score")] public float[] Score { get; set; }
    }

    #endregion

    /// <summary>
    ///     model mapping class for Restaurant.
    /// </summary>

    #region model mapping class

    public class ModelMapping
    {
        [ColumnName(@"Features")] public float[] Features { get; set; }

        [ColumnName(@"PredictedLabel")]
        public float PredictedLabel { get; set; }

        [ColumnName(@"Score")] public float[] Score { get; set; }
    }

    #endregion
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
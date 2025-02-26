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

        Action<InputData, CustomMappingOutput> mapping =
            (input, output) =>
            {
                output.AgeName = input.Age switch
                {
                    < 18 => "Child",
                    < 55 => "Man",
                    _ => "Grandpa"
                };
            };

        var pipeline = mlContext.Transforms.CustomMapping(mapping, null);

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
        var trainer = new DecisionTreeBinaryTrainer("will_wait");
        var mlModel = trainer.Fit(_data);
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
using System;
using System.Reflection;
using Italbytz.Adapters.Algorithms.AI.Learning.Framework;
using Italbytz.Adapters.Algorithms.AI.Util.ML;
using Italbytz.Ports.Algorithms.AI.Learning;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Learners;

public class DecisionTreeBinaryTrainer<TModelInput> : IEstimator<ITransformer>
    where TModelInput : class, new()
{
    private readonly ILearner _learner;
    private readonly string _target;

    public DecisionTreeBinaryTrainer(string target)
    {
        _target = target;
        _learner = new DecisionTreeLearner();
    }

    public ITransformer Fit(IDataView input)
    {
        var mlContext = new MLContext();
        var dataSet = input.AsDataSet(_target);
        _learner.Train(dataSet);

        var pipeline =
            mlContext.Transforms.CustomMapping(
                (Action<TModelInput, ClassificationMapping>)Mapping, null);
        return pipeline.Fit(input);
    }

    public SchemaShape GetOutputSchema(SchemaShape inputSchema)
    {
        throw new NotImplementedException();
    }

    private void Mapping(TModelInput input, ClassificationMapping output)
    {
        var example = ToExample(input);
        var prediction = _learner.Predict(example);
        output.Features = new float[11];
        output.PredictedLabel = prediction.Equals(Util.Util.Yes) ? 1 : 0;
        output.Score = new float[2];
    }

    private IExample ToExample(TModelInput input)
    {
        var type = typeof(TModelInput);
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(input);
            var name = property.Name;
            var att =
                property.GetCustomAttributes(typeof(ColumnNameAttribute), true);
            if (att.Length > 0)
            {
                var props = typeof(ColumnNameAttribute).GetProperties(
                    BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.Public);
                name = typeof(ColumnNameAttribute).GetProperty("Name",
                        BindingFlags.Instance |
                        BindingFlags.NonPublic |
                        BindingFlags.Public)
                    ?.GetValue(att[0])
                    ?.ToString();
            }


            Console.WriteLine($"{name} = {value}");
        }

        Console.WriteLine(input);
        return null;
        return new Example(null, null);
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using Italbytz.Adapters.Algorithms.AI.Learning.Framework;
using Italbytz.Adapters.Algorithms.AI.Util.ML;
using Italbytz.ML;
using Italbytz.Ports.Algorithms.AI.Learning;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Learners;

public class DecisionTreeBinaryLegacyTrainer<TModelInput> : IEstimator<ITransformer>
    where TModelInput : class, new()
{
    private readonly ILearner _learner;
    private readonly string _target;
    private IDataSetSpecification _spec;

    public DecisionTreeBinaryLegacyTrainer(string target)
    {
        _target = target;
        _learner = new DecisionTreeLearner();
    }

    public ITransformer Fit(IDataView input)
    {
        var mlContext = new MLContext();
        _spec = input.GetDataSetSpecification(_target);
        var dataSet = input.AsDataSet(_target, _spec);
        _learner.Train(dataSet);

        var pipeline =
            mlContext.Transforms.CustomMapping(
                (Action<TModelInput, BinaryClassificationOutputSchema>)Mapping,
                null);
        return pipeline.Fit(input);
    }

    public SchemaShape GetOutputSchema(SchemaShape inputSchema)
    {
        throw new NotImplementedException();
    }

    private void Mapping(TModelInput input,
        BinaryClassificationOutputSchema output)
    {
        var example = ToExample(input);
        var prediction = _learner.Predict(example);
        output.PredictedLabel = (uint)(prediction.Equals("1") ? 1 : 0);
        output.Score = prediction.Equals("1") ? 1f : 0f;
        output.Probability = prediction.Equals("1") ? 1f : 0f;
    }

    private IExample ToExample(TModelInput input)
    {
        var type = typeof(TModelInput);
        var properties = type.GetProperties();

        Dictionary<string, IAttribute> attributes = new();

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

            switch (value)
            {
                case null:
                    continue;
                case bool b:
                    value = b ? Util.Util.Yes : Util.Util.No;
                    break;
            }

            attributes.Add(name,
                new StringAttribute(value.ToString(),
                    _spec.GetAttributeSpecFor(name)));
        }

        return new Example(attributes, attributes[_target]);
    }
}
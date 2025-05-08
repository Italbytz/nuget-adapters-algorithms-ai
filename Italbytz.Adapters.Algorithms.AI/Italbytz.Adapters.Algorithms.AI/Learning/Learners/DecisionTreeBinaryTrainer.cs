using System;
using System.Collections.Generic;
using Italbytz.Adapters.Algorithms.AI.Learning.Framework;
using Italbytz.ML;
using Italbytz.Ports.Algorithms.AI.Learning;
using Microsoft.ML;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Learners;

/// <inheritdoc />
public class DecisionTreeBinaryTrainer : IEstimator<ITransformer>
{
    private readonly ILearner _learner;

    public DecisionTreeBinaryTrainer()
    {
        _learner = new DecisionTreeLearner();
    }

    /// <inheritdoc />
    public SchemaShape GetOutputSchema(SchemaShape inputSchema)
    {
        //throw new NotImplementedException();
        return inputSchema;
    }

    /// <inheritdoc />
    public ITransformer Fit(IDataView input)
    {
        var dataExcerpt = input.GetDataExcerpt();
        var spec =
            GetDataSetSpecification(dataExcerpt);
        var dataSet = GetDataSet(dataExcerpt, spec);
        _learner.Train(dataSet);
        throw new NotImplementedException();
    }

    private IDataSet GetDataSet(IDataExcerpt dataExcerpt,
        IDataSetSpecification spec)
    {
        var features = dataExcerpt.Features;
        var featureNames = dataExcerpt.FeatureNames;
        var labels = dataExcerpt.Labels;
        var dataSet = new DataSet(spec);
        // Iterate through rows
        var rowIndex = 0;
        foreach (var feature in features)
        {
            Dictionary<string, IAttribute> attributes = new();
            // Iterate through columns
            var columnIndex = 0;
            foreach (var featureName in featureNames)
            {
                var columnSpecification =
                    spec.GetAttributeSpecFor(featureName);
                var value = feature[columnIndex];
                attributes.Add(featureName,
                    new NumericAttribute(value, columnSpecification));
                columnIndex++;
            }

            var targetAttribute = new NumericAttribute(labels[rowIndex],
                spec.GetAttributeSpecFor(DefaultColumnNames.Label));
            attributes.Add(DefaultColumnNames.Label, targetAttribute);
            var example = new Example(attributes, targetAttribute);
            dataSet.Examples.Add(example);
            rowIndex++;
        }

        return dataSet;
    }

    private IDataSetSpecification GetDataSetSpecification(
        IDataExcerpt dataExcerpt)
    {
        var featureNames = dataExcerpt.FeatureNames;
        var dss = new DataSetSpecification();
        foreach (var featureName in featureNames)
            dss.DefineNumericAttribute(featureName);
        dss.DefineNumericAttribute(DefaultColumnNames.Label);
        return dss;
    }
}
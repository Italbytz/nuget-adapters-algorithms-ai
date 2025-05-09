using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        var mlContext = ThreadSafeMLContext.LocalMLContext;
        var mapping = new DecisionTreeMapping(_learner, null, null);
        return mlContext.Transforms
            .CustomMapping(
                mapping
                    .GetMapping<BinaryClassificationInputSchema,
                        BinaryClassificationOutputSchema>(), null)
            .GetOutputSchema(inputSchema);
    }

    /// <inheritdoc />
    public ITransformer Fit(IDataView input)
    {
        var dataExcerpt = input.GetDataExcerpt();
        var spec =
            GetDataSetSpecification(dataExcerpt);
        var dataSet = GetDataSet(dataExcerpt, spec);
        _learner.Train(dataSet);
        var mlContext = ThreadSafeMLContext.LocalMLContext;
        var mapping = new DecisionTreeMapping(_learner, dataExcerpt, spec);
        return mlContext.Transforms
            .CustomMapping(
                mapping
                    .GetMapping<BinaryClassificationInputSchema,
                        BinaryClassificationOutputSchema>(), null).Fit(input);
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
                    new StringAttribute(
                        value.ToString(CultureInfo.InvariantCulture),
                        columnSpecification));
                columnIndex++;
            }

            var targetAttribute = new StringAttribute(
                labels[rowIndex].ToString(),
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
            dss.DefineStringAttribute(featureName,
                dataExcerpt.GetUniqueFeatureValues(featureName)
                    .Select(v => v.ToString(CultureInfo.InvariantCulture))
                    .ToArray());
        dss.DefineStringAttribute(DefaultColumnNames.Label,
            dataExcerpt.UniqueLabelValues
                .Select(v => v.ToString(CultureInfo.InvariantCulture))
                .ToArray());
        return dss;
    }
}
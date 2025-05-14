using Italbytz.AI.Util.ML;
using Italbytz.ML;
using Microsoft.ML;

namespace Italbytz.AI.Learning.Learners;

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
            dataExcerpt.GetDataSetSpecification();
        var dataSet = dataExcerpt.GetDataSet(spec);
        _learner.Train(dataSet);
        var mlContext = ThreadSafeMLContext.LocalMLContext;
        var mapping = new DecisionTreeMapping(_learner, dataExcerpt, spec);
        return mlContext.Transforms
            .CustomMapping(
                mapping
                    .GetMapping<BinaryClassificationInputSchema,
                        BinaryClassificationOutputSchema>(), null).Fit(input);
    }
}
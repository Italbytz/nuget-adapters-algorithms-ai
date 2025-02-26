using System;
using Italbytz.Adapters.Algorithms.AI.Util.ML;
using Microsoft.ML;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Learners;

public class DecisionTreeBinaryTrainer : IEstimator<ITransformer>
{
    private readonly string _target;

    public DecisionTreeBinaryTrainer(string target)
    {
        _target = target;
    }

    public ITransformer Fit(IDataView input)
    {
        var dataSet = input.AsDataSet(_target);
        var learner = new DecisionTreeLearner();
        learner.Train(dataSet);
        var result = learner.Test(dataSet);
        return null;
    }

    public SchemaShape GetOutputSchema(SchemaShape inputSchema)
    {
        throw new NotImplementedException();
    }
}
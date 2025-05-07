using System;
using Microsoft.ML;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Learners;

public class DecisionTreeBinaryTrainer<TTransformer> : IEstimator<TTransformer>
    where TTransformer : ITransformer
{
    public TTransformer Fit(IDataView input)
    {
        throw new NotImplementedException();
    }

    public SchemaShape GetOutputSchema(SchemaShape inputSchema)
    {
        throw new NotImplementedException();
    }
}
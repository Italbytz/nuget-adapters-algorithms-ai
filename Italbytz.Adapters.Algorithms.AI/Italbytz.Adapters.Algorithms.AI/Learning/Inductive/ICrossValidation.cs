using Italbytz.Ports.Algorithms.AI.Learning;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Inductive;

public interface ICrossValidation
{
    public IParameterizedLearner CrossValidationWrapper(
        IParameterizedLearner learner, int k,
        IDataSet examples);
}
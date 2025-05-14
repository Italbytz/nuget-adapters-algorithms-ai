using Italbytz.Ports.Algorithms.AI.Learning;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Inductive;

public interface IParameterizedLearner : ILearner
{
    public int ParameterSize { get; set; }
    public void Train(int size, IDataSet dataSet);
}
using System.Linq;
using Italbytz.Ports.Algorithms.AI.Learning;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Learners;

public class MajorityLearner : ILearner
{
    private string _result;

    public void Train(IDataSet ds)
    {
        var targets = ds.Examples.Select(e => e.TargetValue()).ToList();
        _result = Util.Util.Mode(targets)!;
    }

    public string[] Predict(IDataSet ds)
    {
        throw new System.NotImplementedException();
    }

    public string Predict(IExample e)
    {
        return _result;
    }

    public int[] Test(IDataSet ds)
    {
        var results = new[] { 0, 0 };

        foreach (var e in ds.Examples)
            if (e.TargetValue().Equals(_result))
                results[0] = results[0] + 1;
            else
                results[1] = results[1] + 1;

        return results;
    }
}
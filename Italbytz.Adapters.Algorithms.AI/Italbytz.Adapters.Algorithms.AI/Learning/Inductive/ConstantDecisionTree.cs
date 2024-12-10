using Italbytz.Ports.Algorithms.AI.Learning;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Inductive;

public class ConstantDecisionTree : DecisionTree
{
    public ConstantDecisionTree(string value)
    {
        Value = value;
    }

    public string Value { get; set; }

    public override object Predict(IExample e)
    {
        return Value;
    }
}
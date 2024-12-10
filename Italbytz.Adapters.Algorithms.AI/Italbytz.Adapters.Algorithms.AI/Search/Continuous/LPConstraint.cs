using Italbytz.Ports.Algorithms.AI.Search.Continuous;

namespace Italbytz.Adapters.Algorithms.AI.Search.Continuous;

public class LPConstraint : ILPConstraint
{
    public double[] Coefficients { get; set; }
    public ConstraintType ConstraintType { get; set; }
    public double RHS { get; set; }
}
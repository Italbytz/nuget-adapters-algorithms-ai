using System.Collections.Generic;
using Italbytz.Ports.Algorithms.AI.Search.Continuous;

namespace Italbytz.Adapters.Algorithms.AI.Search.Continuous;

public class LPModel : ILPModel
{
    public bool Maximization { get; set; }
    public double[] ObjectiveFunction { get; set; }
    public (double, double)[] Bounds { get; set; }
    public List<ILPConstraint> Constraints { get; set; }
    public bool[] IntegerVariables { get; set; }
}
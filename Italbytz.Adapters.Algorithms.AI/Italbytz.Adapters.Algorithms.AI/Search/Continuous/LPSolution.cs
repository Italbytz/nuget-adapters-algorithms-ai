using Italbytz.Ports.Algorithms.AI.Search.Continuous;

namespace Italbytz.Adapters.Algorithms.AI.Search.Continuous
{
    public class LPSolution : ILPSolution
    {
        public double Objective { get; set; }
        public double[] Solution { get; set; }
    }
}
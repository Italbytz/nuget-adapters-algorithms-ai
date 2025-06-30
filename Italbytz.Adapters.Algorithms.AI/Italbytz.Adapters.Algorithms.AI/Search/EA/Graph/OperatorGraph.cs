using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Italbytz.AI.Search.EA.Fitness;
using Italbytz.AI.Search.EA.Individuals;
using Italbytz.AI.Search.EA.Operator;

namespace Italbytz.AI.Search.EA.Graph;

public class OperatorGraph
{
    protected Finish Finish { get; set; }
    protected Start Start { get; set; }
    public IFitnessFunction FitnessFunction { get; set; }

    public Task<IIndividualList> Process(IIndividualList individuals)
    {
        return Start.Process(Task.FromResult(individuals), FitnessFunction);
    }

    public void Check()
    {
        List<IGraphOperator> nodes =
        [
            Start
        ];
        while (nodes.Count > 0)
        {
            var node = nodes[0];
            nodes.RemoveAt(0);
            node.Check();
            foreach (var child in node.Children.Where(child =>
                         !nodes.Contains(child))) nodes.Add(child);
        }
    }
}
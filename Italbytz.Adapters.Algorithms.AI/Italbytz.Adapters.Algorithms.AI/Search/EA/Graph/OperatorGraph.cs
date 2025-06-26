using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Italbytz.AI.Search.EA.Operator;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Graph;

public class OperatorGraph
{
    protected Finish Finish { get; set; }
    protected Start Start { get; set; }

    public Task<IIndividualList> Process(IIndividualList individuals)
    {
        return Start.Process(Task.FromResult(individuals));
        /*List<IGraphOperator> nodes =
        [
            Start
        ];
        while (nodes.Count > 0)
        {
            var node = nodes[0];
            nodes.RemoveAt(0);
            foreach (var child in node.Children.Where(child =>
                         !nodes.Contains(child)))
            {
            }
        }

        return null;*/
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
using Italbytz.AI.Search.EA.Mutation;
using Italbytz.AI.Search.EA.Operator;
using Italbytz.AI.Search.EA.Operator.Selection;

namespace Italbytz.AI.Search.EA.Graph.Common;

public class OnePlusOneEA : OperatorGraph
{
    public OnePlusOneEA()
    {
        Start = new Start();
        Finish = new Finish();
        var mutation = new StandardMutation();
        var selection = new CutSelection();
        Start.AddChildren(mutation, selection);
        mutation.AddChildren(selection);
        selection.AddChildren(Finish);
    }
}
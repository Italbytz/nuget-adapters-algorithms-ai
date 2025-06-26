using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Operator;

public abstract class SplitOperator : GraphOperator
{
    public override async Task<IIndividualList> Process(
        Task<IIndividualList> individuals)
    {
        List<Task<IIndividualList>> processes = [];
        processes.AddRange(
            Children.Select(parent => parent.Process(individuals)));
        await Task.WhenAll(processes);
        return null;
    }

    /*public override void Check()
    {
        if (Children.Count == 0)
            throw new InvalidOperationException(
                "SplitOperator must have at least one child operator.");
        if (Parents.Count > 1)
            throw new InvalidOperationException(
                "SplitOperator cannot have more than one parent.");
    }*/
}
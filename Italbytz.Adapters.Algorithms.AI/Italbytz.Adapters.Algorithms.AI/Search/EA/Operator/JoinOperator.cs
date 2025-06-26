using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Operator;

public abstract class JoinOperator : GraphOperator
{
    public override async Task<IIndividualList> Process(
        Task<IIndividualList> individuals)
    {
        List<Task<IIndividualList>> processes = [];
        processes.AddRange(
            Parents.Select(parent => parent.Process(individuals)));
        await Task.WhenAll(processes);
        return null;
    }

    /*public override void Check()
    {
        if (Children.Count > 1)
            throw new InvalidOperationException(
                "JoinOperator cannot have more than one child.");
        if (Parents.Count < 1)
            throw new InvalidOperationException(
                "JoinOperator must at least have one parent.");
    }*/
}
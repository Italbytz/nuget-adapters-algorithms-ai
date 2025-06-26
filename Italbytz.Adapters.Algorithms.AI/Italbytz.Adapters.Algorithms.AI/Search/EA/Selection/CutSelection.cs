using System.Collections.Generic;
using System.Threading.Tasks;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Operator.Selection;

public class CutSelection : GraphOperator
{
    public List<Task<IIndividualList>> ParentTasks = [];
    public override bool NeedsJoin { get; }

    public override Task<IIndividualList> Process(
        Task<IIndividualList> individuals)
    {
        ParentTasks.Add(individuals);
        if (ParentTasks.Count < Parents.Count) return null;
        Task.WhenAll(ParentTasks).Wait();
        var individualList = ParentTasks[0].Result;
        // ToDo
        Task<IIndividualList> result = null;
        foreach (var child in Children) result = child.Process(individuals);
        return result;
    }
}
using System.Threading.Tasks;
using Italbytz.AI.Search.EA.Operator;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Mutation;

public class StandardMutation : GraphOperator
{
    public override Task<IIndividualList> Process(
        Task<IIndividualList> individuals)
    {
        var individualList = individuals.Result;
        // ToDo: Implement mutation logic here
        Task<IIndividualList> result = null;
        foreach (var child in Children) result = child.Process(individuals);
        return result;
    }
}
using System.Threading.Tasks;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Operator;

public class Start : GraphOperator
{
    public override int MaxParents { get; } = 0;


    public override Task<IIndividualList> Process(
        Task<IIndividualList> individuals)
    {
        Task<IIndividualList> result = null;
        foreach (var child in Children) result = child.Process(individuals);

        return result;
    }
}
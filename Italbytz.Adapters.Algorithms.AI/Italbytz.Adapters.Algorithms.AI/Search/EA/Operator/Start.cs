using System.Threading.Tasks;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Operator;

public class Start : GraphOperator
{
    public override bool MaySplit { get; } = true;

    public override Task<IIndividualList> Process(
        Task<IIndividualList> individuals)
    {
        Task<IIndividualList> result = null;
        foreach (var child in Children) result = child.Process(individuals);

        return result;
    }
}
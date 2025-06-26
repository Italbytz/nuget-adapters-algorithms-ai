using System.Threading.Tasks;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Operator;

public class Finish : GraphOperator
{
    public override int MaxChildren { get; } = 0;


    public override Task<IIndividualList> Process(
        Task<IIndividualList> individuals)
    {
        return individuals;
    }
}
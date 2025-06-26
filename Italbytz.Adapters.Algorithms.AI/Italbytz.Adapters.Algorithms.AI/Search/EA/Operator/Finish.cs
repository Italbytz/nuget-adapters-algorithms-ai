using System.Threading.Tasks;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Operator;

public class Finish : GraphOperator
{
    public override bool NeedsJoin { get; } = true;

//    public Task<IIndividualList> FinalIndividuals { get; set; } = new(null);

    public override Task<IIndividualList> Process(
        Task<IIndividualList> individuals)
    {
        return individuals;
    }
}
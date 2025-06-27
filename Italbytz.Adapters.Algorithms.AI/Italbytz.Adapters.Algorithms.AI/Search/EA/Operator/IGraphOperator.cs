using System.Collections.Generic;
using System.Threading.Tasks;
using Italbytz.AI.Search.GP.Fitness;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Operator;

public interface IGraphOperator
{
    public int MaxParents { get; }
    public int MaxChildren { get; }
    public List<IGraphOperator> Children { get; }
    public List<IGraphOperator> Parents { get; }

    public void Check();

    public void AddChildren(params IGraphOperator[] children);

    public Task<IIndividualList>? Process(Task<IIndividualList> individuals,
        IFitnessFunction fitnessFunction);
}
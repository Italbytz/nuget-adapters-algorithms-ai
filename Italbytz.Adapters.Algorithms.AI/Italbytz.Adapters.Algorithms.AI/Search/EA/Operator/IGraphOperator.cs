using System.Collections.Generic;
using System.Threading.Tasks;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Operator;

public interface IGraphOperator
{
    public bool MaySplit { get; }
    public bool NeedsJoin { get; }
    public List<IGraphOperator> Children { get; }
    public List<IGraphOperator> Parents { get; }

    public void Check();

    public void AddChildren(params IGraphOperator[] children);

    public Task<IIndividualList> Process(Task<IIndividualList> individuals);
}
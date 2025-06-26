using System.Collections.Generic;
using System.Threading.Tasks;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Operator;

public abstract class GraphOperator : IGraphOperator
{
    public abstract Task<IIndividualList> Process(
        Task<IIndividualList> individuals);

    public virtual bool MaySplit { get; } = false;
    public virtual bool NeedsJoin { get; } = false;
    public List<IGraphOperator> Children { get; } = [];
    public List<IGraphOperator> Parents { get; } = [];

    public void AddChildren(params IGraphOperator[] children)
    {
        foreach (var child in children)
        {
            Children.Add(child);
            child.Parents.Add(this);
        }
    }

    public void Check()
    {
        //ToDo;
    }
}
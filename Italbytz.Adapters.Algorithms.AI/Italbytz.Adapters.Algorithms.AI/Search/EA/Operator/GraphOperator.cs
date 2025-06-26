using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Operator;

public abstract class GraphOperator : IGraphOperator
{
    public virtual int MaxParents { get; } = 1;
    public virtual int MaxChildren { get; } = 1;

    public abstract Task<IIndividualList> Process(
        Task<IIndividualList> individuals);

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
        if (Children.Count > MaxChildren)
            throw new InvalidOperationException(
                $"Operator cannot have more than {MaxChildren} children.");
        if (Parents.Count > MaxParents)
            throw new InvalidOperationException(
                $"Operator cannot have more than {MaxParents} parents.");
    }
}
using System.Collections.Generic;
using Italbytz.AI.Search.GP;

namespace Italbytz.AI.Search.EA.Operator;

public interface IGraphOperator : IOperator
{
    public List<IGraphOperator> Children { get; }
}
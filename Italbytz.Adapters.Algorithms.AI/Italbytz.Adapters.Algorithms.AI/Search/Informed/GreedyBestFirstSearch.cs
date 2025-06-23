using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Italbytz.Adapters.Algorithms.AI.Search.Framework;
using Italbytz.Adapters.Algorithms.AI.Search.Framework.QSearch;
using Italbytz.Ports.Algorithms.AI.Search;
using Italbytz.Ports.Algorithms.AI.Search.Informed;

namespace Italbytz.Adapters.Algorithms.AI.Search.Informed
{
    public class GreedyBestFirstSearch<TState, TAction> :
       QueueBasedSearch<TState, TAction>, IInformed<TState, TAction>
    {
        public GreedyBestFirstSearch(QueueSearch<TState, TAction> impl,
            Func<INode<TState, TAction>, double> evalFn) : base(impl,
            QueueFactory.CreatePriorityQueue(evalFn))
        {
        }

        public Func<INode<TState, TAction>, double>? HeuristicFn { get; set; }
    }
}

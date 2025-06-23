using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Italbytz.Adapters.Algorithms.AI.Search.Framework;
using Italbytz.Adapters.Algorithms.AI.Search.Framework.QSearch;
using Italbytz.Adapters.Algorithms.AI.Util.Datastructure;

namespace Italbytz.Adapters.Algorithms.AI.Search.Uninformed
{
    public class BreadthFirstSearch<TState, TAction> : QueueBasedSearch<TState, TAction>
    {
        public BreadthFirstSearch() : this(new TreeSearch<TState, TAction>())
        {
        }

        private BreadthFirstSearch(QueueSearch<TState, TAction> impl) : base(
            impl,
            QueueFactory.CreatePriorityQueue<TState, TAction>(node =>
                node.PathCost))

        {
        }
    }
}

// The original version of this file is part of <see href="https://github.com/aimacode/aima-java"/> which is released under
// MIT License
// Copyright (c) 2015 aima-java contributors

using System;
using Italbytz.Adapters.Algorithms.AI.Search.Framework;
using Italbytz.Adapters.Algorithms.AI.Search.Framework.QSearch;
using Italbytz.Ports.Algorithms.AI.Search;
using Italbytz.Ports.Algorithms.AI.Search.Informed;

namespace Italbytz.Adapters.Algorithms.AI.Search.Informed
{
    public class BestFirstSearch<TState, TAction> :
        QueueBasedSearch<TState, TAction>, IInformed<TState, TAction>
    {
        protected BestFirstSearch(QueueSearch<TState, TAction> impl,
            Func<INode<TState, TAction>, double> evalFn) : base(impl,
            QueueFactory.CreatePriorityQueue(evalFn))
        {
        }

        public Func<INode<TState, TAction>, double>? HeuristicFn { get; set; }
    }
}
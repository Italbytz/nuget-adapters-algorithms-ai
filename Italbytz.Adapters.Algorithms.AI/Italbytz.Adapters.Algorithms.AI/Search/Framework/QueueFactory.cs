// The original version of this file is part of <see href="https://github.com/aimacode/aima-java"/> which is released under
// MIT License
// Copyright (c) 2015 aima-java contributors

using System;
using Italbytz.Adapters.Algorithms.AI.Util.Datastructure;
using Italbytz.Ports.Algorithms.AI.Search;

namespace Italbytz.Adapters.Algorithms.AI.Search.Framework
{
    public static class QueueFactory
    {
        public static NodePriorityQueue<TState, TAction>
            CreatePriorityQueue<TState, TAction>(
                Func<INode<TState, TAction>, double> priorityFn) =>
            new(priorityFn, 11);
    }
}
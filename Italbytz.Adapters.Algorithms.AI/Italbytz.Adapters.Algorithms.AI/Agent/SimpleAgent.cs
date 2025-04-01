// The original version of this file is part of <see href="https://github.com/aimacode/aima-java"/> which is released under
// MIT License
// Copyright (c) 2015 aima-java contributors

using Italbytz.Adapters.Algorithms.AI.Util;
using Italbytz.Ports.Algorithms.AI.Agent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Italbytz.Adapters.Algorithms.AI.Agent;

/// <inheritdoc cref="IAgent{TPercept,TAction}" />
public class SimpleAgent<TPercept, TAction> : IAgent<TPercept, TAction>
{
    protected SimpleAgent(ILoggerFactory loggerFactory)
    {
        LoggingExtensions.InitLoggers(loggerFactory);
    }

    protected SimpleAgent() : this(NullLoggerFactory.Instance)
    {
    }

    public bool Alive { get; } = true;

    public virtual TAction? Act(TPercept? percept)
    {
        return default;
    }
}
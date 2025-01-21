// The original version of this file is part of <see href="https://github.com/aimacode/aima-java"/> which is released under
// MIT License
// Copyright (c) 2015 aima-java contributors

using Italbytz.Ports.Algorithms.AI.Search.CSP;

namespace Italbytz.Adapters.Algorithms.AI.Search.CSP;

public class Variable : IVariable
{
    public Variable(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public bool Equals(IVariable? other)
    {
        return other != null && Name.Equals(other.Name);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Variable)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
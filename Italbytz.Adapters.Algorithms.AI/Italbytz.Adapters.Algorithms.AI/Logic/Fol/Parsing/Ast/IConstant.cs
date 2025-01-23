using System;

namespace Italbytz.Adapters.Algorithms.AI.Logic.Fol.Parsing.Ast;

public interface IConstant : ITerm, IEquatable<IConstant>
{
    public string Value { get; }
}
using System;

namespace Italbytz.Adapters.Algorithms.AI.Logic.Fol.Parsing.Ast;

public interface IVariable : ITerm, IEquatable<IVariable>
{
    int Indexical { get; }
}
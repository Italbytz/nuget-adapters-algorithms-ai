using Italbytz.Adapters.Algorithms.AI.Logic.Common;

namespace Italbytz.Adapters.Algorithms.AI.Logic.Fol.Parsing.Ast;

public interface IFolNode : IParseTreeNode
{
    public string SymbolicName { get; }
}
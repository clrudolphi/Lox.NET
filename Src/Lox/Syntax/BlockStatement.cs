using System.Collections.Generic;

namespace Lox
{
    public sealed class BlockStatement : SyntaxNode, IVisitable
    {
        public List<SyntaxNode> Statements { get; }
        public override SyntaxKind Kind => SyntaxKind.BlockStatement;

        public BlockStatement(List<SyntaxNode> statements)
        {
            Statements = statements;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            throw new System.NotImplementedException();
        }

        public void Accept(ISyntaxNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public T Accept<T>(ISyntaxNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}

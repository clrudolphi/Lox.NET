using System.Collections.Generic;
using static Lox.Functional;

namespace Lox
{
    public sealed class LiteralExpression : SyntaxNode, IVisitable
    {
        public Option<object> Value { get; }

        public LiteralExpression(object? value)
        {
            Value = value ?? None;
        }

        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;

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

using System.Collections.Generic;

namespace Lox
{
    public class AssignmentExpression : SyntaxNode, IVisitable
    {
        public Token Name { get; }
        public SyntaxNode Value { get; }

        public override SyntaxKind Kind => SyntaxKind.AssignmentExpression;

        public AssignmentExpression(Token name, SyntaxNode value)
        {
            Name = name;
            Value = value;
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

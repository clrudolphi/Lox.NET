using System.Collections.Generic;

namespace Lox
{
    public class GetExpression : SyntaxNode, IVisitable
    {
        public Token Name {get;}
        public SyntaxNode Object {get;}

        public override SyntaxKind Kind => SyntaxKind.GetExpression;

        public GetExpression(SyntaxNode expression, Token name)
        {
            Name = name;
            Object = expression;
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

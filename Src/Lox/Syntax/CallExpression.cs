using System.Collections.Generic;

namespace Lox
{
    public class CallExpression : SyntaxNode, IVisitable
    {
        public SyntaxNode Callee {get;}
        public Token Paren {get;}

        public List<SyntaxNode> Arguments {get;}

        public override SyntaxKind Kind => SyntaxKind.CallExpression;

        public CallExpression(SyntaxNode callee, Token paren, List<SyntaxNode> arguments)
        {
            Callee  = callee;
            Paren = paren;
            Arguments = arguments;
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

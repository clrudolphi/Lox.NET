using System.Collections.Generic;

namespace Lox
{
    public class SuperExpression : SyntaxNode, IVisitable
    {
        public Token Keyword {get;}
        public Token Method {get;}

        public override SyntaxKind Kind => SyntaxKind.SuperExpression;

        public SuperExpression(Token keyword, Token method)
        {
            Keyword = keyword;
            Method = method;
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

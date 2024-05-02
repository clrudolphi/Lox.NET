using System.Collections.Generic;

namespace Lox
{
    public class ThisExpression : SyntaxNode, IVisitable
    {
        public Token Keyword {get;}
        public override SyntaxKind Kind => SyntaxKind.ThisExpression;

        public ThisExpression(Token keyword)
        {
            Keyword = keyword;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            throw new System.NotImplementedException();
        }

        public void Accept(ISyntaxNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

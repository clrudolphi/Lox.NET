using System.Collections.Generic;

namespace Lox
{
    public class ReturnStatement : SyntaxNode, IVisitable
    {
        public Token Keyword {get;}
        public SyntaxNode Value {get;}
        public override SyntaxKind Kind => SyntaxKind.ReturnStatement;

        public ReturnStatement(Token keyword, SyntaxNode value)
        {
            Keyword = keyword;
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
    }
}

using System.Collections.Generic;

namespace Lox
{
    public class ExpressionStatement : SyntaxNode, IVisitable
    {
        public SyntaxNode Expression { get; }
        public override SyntaxKind Kind => SyntaxKind.ExpressionStatement;

        public ExpressionStatement(SyntaxNode expression)
        {
            Expression = expression;
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

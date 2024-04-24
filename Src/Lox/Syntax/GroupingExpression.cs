using System.Collections.Generic;

namespace Lox
{
    public sealed class GroupingExpression : SyntaxNode, IVisitable
    {
        public SyntaxNode Expression { get; }

        public GroupingExpression(SyntaxNode expression)
        {
            Expression = expression;
        }

        public override SyntaxKind Kind => SyntaxKind.GroupingExpression;

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

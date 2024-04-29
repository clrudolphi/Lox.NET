using System;
using System.Collections.Generic;
using System.Linq;

namespace Lox
{
    public class WhileStatement : SyntaxNode, IVisitable
    {
        
        public SyntaxNode Condition {get;}
        public SyntaxNode Body {get;}

        public override SyntaxKind Kind => SyntaxKind.WhileStatement;

        public WhileStatement(SyntaxNode condition, SyntaxNode body)
        {
            Condition = condition;
            Body = body;
        }
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Array.Empty<SyntaxNode>().ToArray().AsEnumerable();
        }

        public void Accept(ISyntaxNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

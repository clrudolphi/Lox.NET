using System;
using System.Collections.Generic;
using System.Linq;

namespace Lox
{
    public class VariableDeclarationStatement : SyntaxNode, IVisitable
    {
        public Token Name { get; }
        public SyntaxNode Initializer { get; }

        public override SyntaxKind Kind => SyntaxKind.VariableDeclarationStatement;

        public VariableDeclarationStatement(Token name, SyntaxNode initializer)
        {
            Name = name;
            Initializer = initializer;
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

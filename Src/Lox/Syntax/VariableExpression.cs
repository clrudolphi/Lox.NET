﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Lox
{
    public class VariableExpression : SyntaxNode, IVisitable
    {
        public Token Name { get; }

        public override SyntaxKind Kind => SyntaxKind.VariableExpression;

        public VariableExpression(Token name)
        {
            Name = name;
        }


        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Array.Empty<SyntaxNode>().ToArray().AsEnumerable();
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

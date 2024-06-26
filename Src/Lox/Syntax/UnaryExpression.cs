﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Lox
{
    public sealed class UnaryExpression : SyntaxNode, IVisitable
    {
        public SyntaxNode Right { get; }
        public Token Operator { get; }

        public UnaryExpression(Token oper, SyntaxNode right)
        {
            Operator = oper;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;

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


        //public IEnumerable<SyntaxNode> GetNodes()
        //{
        //    yield return Right;
        //    yield return this;
        //}
    }
}

using System.Collections.Generic;

namespace Lox
{
    public abstract class ExpressionSyntax : SyntaxNode
    {


    }


    public class BinaryExpression : SyntaxNode, IVisitable
    {
        public SyntaxNode Left { get; }
        public SyntaxNode Right { get; }
        public Token Operator { get; }

        public BinaryExpression(SyntaxNode left, Token oper, SyntaxNode right)
        {
            Left = left;
            Operator = oper;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return Operator;
            yield return Right;
        }

        public void Accept(ISyntaxNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

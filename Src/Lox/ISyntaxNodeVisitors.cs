namespace Lox
{
    public interface ISyntaxNodeVisitor
    {
        void Visit(BinaryExpression node);
        void Visit(ExpressionStatement node);
        void Visit(GroupingExpression node);
        void Visit(LiteralExpression node);
        void Visit(PrintStatement node);
        void Visit(UnaryExpression node);
        void Visit(ReturnStatement node);
        void Visit(VariableDeclarationStatement node);
        void Visit(VariableExpression node);
        void Visit(BlockStatement node);
        void Visit(AssignmentExpression node);
    }


    public interface IVisitable
    {
        void Accept(ISyntaxNodeVisitor visitor);
    }
}
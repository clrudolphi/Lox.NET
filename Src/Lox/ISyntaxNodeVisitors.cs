namespace Lox
{
    public interface ISyntaxNodeVisitor
    {
        void Visit(BlockStatement node);
        void Visit(ExpressionStatement node);
        void Visit(PrintStatement node);
        void Visit(ReturnStatement node);
        void Visit(VariableDeclarationStatement node);
        void Visit(IfStatement node);
        void Visit(FunctionStatement node);
        void Visit(BinaryExpression node);
        void Visit(UnaryExpression node);
        void Visit(VariableExpression node);
        void Visit(GroupingExpression node);
        void Visit(AssignmentExpression node);
        void Visit(LiteralExpression node);
        void Visit(CallExpression node);
    }


    public interface IVisitable
    {
        void Accept(ISyntaxNodeVisitor visitor);
    }
}
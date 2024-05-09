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
        void Visit(WhileStatement node);
        void Visit(FunctionStatement node);
        void Visit(ClassStatement node);
        void Visit(BinaryExpression node);
        void Visit(UnaryExpression node);
        void Visit(VariableExpression node);
        void Visit(GroupingExpression node);
        void Visit(AssignmentExpression node);
        void Visit(LiteralExpression node);
        void Visit(CallExpression node);
        void Visit(SetExpression node);
        void Visit(GetExpression node);
        void Visit(ThisExpression node);
    }

    public interface ISyntaxNodeVisitor<T>
    {
        T Visit(BlockStatement node);
        T Visit(ExpressionStatement node);
        T Visit(PrintStatement node);
        T Visit(ReturnStatement node);
        T Visit(VariableDeclarationStatement node);
        T Visit(IfStatement node);
        T Visit(WhileStatement node);
        T Visit(FunctionStatement node);
        T Visit(ClassStatement node);
        T Visit(BinaryExpression node);
        T Visit(UnaryExpression node);
        T Visit(VariableExpression node);
        T Visit(GroupingExpression node);
        T Visit(AssignmentExpression node);
        T Visit(LiteralExpression node);
        T Visit(CallExpression node);
        T Visit(SetExpression node);
        T Visit(GetExpression node);
        T Visit(ThisExpression node);
    }

    public interface IVisitable
    {
        void Accept(ISyntaxNodeVisitor visitor);
        T Accept<T>(ISyntaxNodeVisitor<T> visitor);
    }
}
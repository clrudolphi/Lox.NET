using System;
using System.Collections.Generic;

namespace Lox
{
    public sealed class Resolver
    {
        private enum FunctionType
        {
            None,
            Function,
            Method,
            Initializer,
        }

        private enum ClassType
        {
            None,
            Class,

            SubClass,
        }
        private readonly Evaluator _evaluator;
        private readonly List<Dictionary<string, bool>> _scopes = new List<Dictionary<string, bool>>();

        private FunctionType _currentFunction = FunctionType.None;
        private ClassType _currentClass = ClassType.None;

        private readonly List<Error> _errors = new List<Error>();

        public IEnumerable<Error> GetErrors()
        {
            return _errors;
        }

        private void Error(Token token, string message)
        {
            if (token.Kind == SyntaxKind.Eof)
            {
                _errors.Add(new Error(ErrorType.SyntaxError, token.Line, " at end", message));
            }
            else
            {
                _errors.Add(new Error(ErrorType.SyntaxError, token.Line, $" at '{token.Lexeme}'", message));
            }
        }

        public Resolver(Evaluator evaluator)
        {
            _evaluator = evaluator;
        }
        public void Resolve(List<SyntaxNode> expressions)
        {
            foreach (SyntaxNode expression in expressions)
            {
                Resolve(expression);
            }
        }


        private void Resolve(SyntaxNode expression)
        {
            switch (expression.Kind)
            {
                case SyntaxKind.BlockStatement:
                    ResolveBlockStatement((BlockStatement)expression);
                    break;
                case SyntaxKind.VariableDeclarationStatement:
                    ResolveVariableDeclarationStatement((VariableDeclarationStatement)expression);
                    break;
                case SyntaxKind.VariableExpression:
                    ResolveVariableExpression((VariableExpression)expression);
                    break;
                case SyntaxKind.AssignmentExpression:
                    ResolveAssignmentExpression((AssignmentExpression)expression);
                    break;
                case SyntaxKind.FunctionStatement:
                    ResolveFunctionStatement((FunctionStatement)expression);
                    break;
                case SyntaxKind.ExpressionStatement:
                    Resolve(((ExpressionStatement)expression).Expression);
                    break;
                case SyntaxKind.IfStatement:
                    ResolveIfStatement((IfStatement)expression);
                    break;
                case SyntaxKind.PrintStatement:
                    Resolve(((PrintStatement)expression).Expression);
                    break;
                case SyntaxKind.ReturnStatement:
                    ResolveReturnStatement((ReturnStatement)expression);
                    break;
                case SyntaxKind.WhileStatement:
                    ResolveWhileStatement((WhileStatement)expression);
                    break;
                case SyntaxKind.BinaryExpression:
                    ResolveBinaryExpression((BinaryExpression)expression);
                    break;
                case SyntaxKind.CallExpression:
                    ResolveCallExpression((CallExpression)expression);
                    break;
                case SyntaxKind.GroupingExpression:
                    Resolve(((GroupingExpression)expression).Expression);
                    break;
                case SyntaxKind.LiteralExpression:
                    break;
                case SyntaxKind.UnaryExpression:
                    ResolveUnaryExpression((UnaryExpression)expression);
                    break;
                case SyntaxKind.ClassStatement:
                    ResolveClassStatement((ClassStatement)expression);
                    break;
                case SyntaxKind.GetExpression:
                    Resolve(((GetExpression)expression).Object);
                    break;
                case SyntaxKind.SetExpression:
                    ResolveSetExpression((SetExpression)expression);
                    break;
                case SyntaxKind.ThisExpression:
                    ResolveThisExpresssion((ThisExpression)expression);
                    break;
                case SyntaxKind.SuperExpression:
                    ResolveSuperExpression((SuperExpression)expression);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private void BeginScope()
        {
            _scopes.Add(new Dictionary<string, bool>());
        }

        private void EndScope()
        {
            _scopes.RemoveAt(_scopes.Count - 1);
        }

        private void Declare(Token name)
        {
            if (_scopes.Count == 0)
            {
                return;
            }

            Dictionary<string, bool> scope = _scopes[_scopes.Count - 1];
            if (scope.ContainsKey(name.Lexeme))
            {
                Error(name, "Already a variable with this name in this scope.");
            }

            scope[name.Lexeme] = false;
        }

        private void Define(Token name)
        {
            if (_scopes.Count == 0)
            {
                return;
            }

            Dictionary<string, bool> scope = _scopes[_scopes.Count - 1];
            scope[name.Lexeme] = true;
        }

        private void ResolveSuperExpression(SuperExpression expr)
        {
            if (_currentClass == ClassType.None)
            {
                Error(expr.Keyword, "Can't use 'super' outside of a class.");
            }
            else if (_currentClass != ClassType.SubClass)
            {
                Error(expr.Keyword, "Can't use 'super' in a class with no superclass.");
            }
            ResolveLocal(expr, expr.Keyword);
        }
        private void ResolveThisExpresssion(ThisExpression expr)
        {
            if (_currentClass == ClassType.None)
            {
                Error(expr.Keyword, "Can't use 'this' outside of a class.");
            }
            ResolveLocal(expr, expr.Keyword);
        }
        private void ResolveSetExpression(SetExpression expr)
        {
            Resolve(expr.Value);
            Resolve(expr.Object);
        }

        private void ResolveBlockStatement(BlockStatement expr)
        {
            BeginScope();
            Resolve(expr.Statements);
            EndScope();
        }

        private void ResolveVariableDeclarationStatement(VariableDeclarationStatement expr)
        {
            Declare(expr.Name);
            if (expr.Initializer != null)
            {
                Resolve(expr.Initializer);
            }

            Define(expr.Name);
        }

        private void ResolveVariableExpression(VariableExpression expr)
        {
            if (_scopes.Count != 0)
            {
                if (_scopes[_scopes.Count - 1].TryGetValue(expr.Name.Lexeme, out bool value) && value == false)
                {
                    Error(expr.Name, "Can't read local variable in its own initializer.");
                }
            }
            ResolveLocal(expr, expr.Name);
        }

        private void ResolveLocal(SyntaxNode expr, Token name)
        {
            for (int i = _scopes.Count - 1; i >= 0; i--)
            {
                if (_scopes[i].ContainsKey(name.Lexeme))
                {
                    _evaluator.Resolve(expr, _scopes.Count - 1 - i);
                    break;
                }
            }
        }

        private void ResolveAssignmentExpression(AssignmentExpression expr)
        {
            Resolve(expr.Value);
            ResolveLocal(expr, expr.Name);
        }

        private void ResolveFunctionStatement(FunctionStatement expr)
        {
            Declare(expr.Name);
            Define(expr.Name);
            ResolveFunction(expr, FunctionType.Function);
        }

        private void ResolveFunction(FunctionStatement expr, FunctionType type)
        {
            FunctionType enclosingFunction = _currentFunction;
            _currentFunction = type;

            BeginScope();
            foreach (Token param in expr.Parameters)
            {
                Declare(param);
                Define(param);
            }
            Resolve(expr.Body);
            EndScope();

            _currentFunction = enclosingFunction;
        }

        private void ResolveIfStatement(IfStatement expr)
        {
            Resolve(expr.Condition);
            Resolve(expr.ThenBranch);
            if (expr.ElseBranch != null)
            {
                Resolve(expr.ElseBranch);
            }
        }

        private void ResolveReturnStatement(ReturnStatement expr)
        {
            if (_currentFunction == FunctionType.None)
            {
                Error(expr.Keyword, "Can't return from top-level code.");
            }

            if (expr.Value != null)
            {
                if (_currentFunction == FunctionType.Initializer)
                {
                    Error(expr.Keyword, "Can't return a value from an initializer.");
                }

                Resolve(expr.Value);
            }
        }

        private void ResolveWhileStatement(WhileStatement expr)
        {
            Resolve(expr.Condition);
            Resolve(expr.Body);
        }

        private void ResolveBinaryExpression(BinaryExpression expr)
        {
            Resolve(expr.Left);
            Resolve(expr.Right);
        }

        private void ResolveCallExpression(CallExpression expr)
        {
            Resolve(expr.Callee);
            foreach (SyntaxNode arg in expr.Arguments)
            {
                Resolve(arg);
            }
        }

        private void ResolveClassStatement(ClassStatement expr)
        {
            ClassType enclosingClass = _currentClass;
            _currentClass = ClassType.Class;
            Declare(expr.Name);
            Define(expr.Name);

            if (expr.SuperClass != null && expr.Name.Lexeme.Equals(expr.SuperClass.Name.Lexeme))
            {
                Error(expr.SuperClass.Name, "A class cannot inherit from itself.");
            }

            if (expr.SuperClass != null)
            {
                _currentClass = ClassType.SubClass;
                Resolve(expr.SuperClass);
            }
            if (expr.SuperClass != null)
            {
                BeginScope();
                _scopes[_scopes.Count - 1].Add("super", true);
            }

            BeginScope();
            _scopes[_scopes.Count - 1].Add("this", true);

            foreach (FunctionStatement method in expr.Methods)
            {
                FunctionType declaration = FunctionType.Method;
                if (method.Name.Lexeme.Equals("init"))
                {
                    declaration = FunctionType.Initializer;
                }

                ResolveFunction(method, declaration);
            }


            EndScope();

            if (expr.SuperClass != null)
            {
                EndScope();
            }

            _currentClass = enclosingClass;
        }

        private void ResolveUnaryExpression(UnaryExpression expr)
        {
            Resolve(expr.Right);
        }
    }
}

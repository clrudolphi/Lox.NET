using System.Collections.Generic;
using System.Linq;

namespace Lox
{
    public sealed class SyntaxTree
    {
        public SyntaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, Token endOfFileToken)
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOfFileToken = endOfFileToken;
        }

        public IReadOnlyList<string> Diagnostics { get; }
        public ExpressionSyntax Root { get; }
        public Token EndOfFileToken { get; }

        //public static SyntaxTree Parse(string text)
        //{
        //    Parser parser = new Parser(text);
        //    return parser.Parse() /*new Parser(text).Parse()*/;
        //}
    }
}

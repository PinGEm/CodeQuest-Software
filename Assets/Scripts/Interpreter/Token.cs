using System.Collections;
using System.Collections.Generic;

namespace CYoureSharpPackage
{
    class Token // Stores data regarding the token
    {
        public readonly TokenType type;
        public readonly string lexeme;
        public readonly object literal;
        public readonly int line;

        public Token(TokenType type, string lexeme, object literal, int line)
        {
            this.type = type;
            this.lexeme = lexeme;
            this.literal = literal;
            this.line = line;
        }

        public string toString()
        {
            return type + "" + lexeme + "" + literal;
        }
    }
}
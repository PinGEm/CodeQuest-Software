using System.Collections;
using System.Collections.Generic;

namespace CYoureSharpPackage
{
    enum TokenType // Defines what the keyword is
    {
        // Literals
        IDENTIFIER, NUMBER,

        // Types & symbols
        INT, EQUAL, SEMICOLON, LEFT_PAREN, RIGHT_PAREN, LEFT_BRACE,
        RIGHT_BRACE, COMMA,

        // Commands
        UP, DOWN, LEFT, RIGHT, SWAP, SELECT,

        // Functions : we'll see if we can implement this
        FUNC,

        // End of input
        EOF
    }
}
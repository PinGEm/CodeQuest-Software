using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CYoureSharpPackage
{
    class Parser
    {
        private class ParseError : Exception
        {
            public ParseError(string message) : base(message) { }
        }

        private readonly List<Token> tokens;
        private int current = 0;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public Expr Parse()
        {
            try
            {
                return Expression();
            }
            catch (ParseError error)
            {
                return null;
            }
        }

        private Expr Expression()
        {
            return ParseCommands();
        }


        private Expr ParseCommands()
        {
            Token token = Advance();

            switch (token.type)
            {
                case TokenType.UP:
                case TokenType.DOWN:
                case TokenType.LEFT:
                case TokenType.RIGHT:
                    return new Expr.MoveExpr(token.lexeme);
                case TokenType.SWAP:
                    {
                        Token firstVar = Advance();
                        Token secondVar = Advance();
                        return new Expr.SwapExpr(firstVar.lexeme, secondVar.lexeme);
                    }
                case TokenType.SELECT:
                    {
                        Token firstVar_Select = Advance();
                        Token secondVar_Select = Advance();
                        return new Expr.SelectExpr(firstVar_Select.lexeme, secondVar_Select.lexeme);
                    }
                case TokenType.FUNC:
                    {
                        Token funcName = Advance();
                        return new Expr.FuncCallExpr(funcName.lexeme);
                    }
                default:
                    throw Error(Peek(), $"Unexpected Token: {token.lexeme}");
            }
        }

        private Token Advance()
        {
            if (!isAtEnd())
            {
                current++;
            }

            return Previous();
        }

        private bool isAtEnd()
        {
            return Peek().type == TokenType.EOF;
        }

        private Token Peek()
        {
            return tokens[current];
        }

        private Token Previous()
        {
            return tokens[current - 1];
        }

        private ParseError Error(Token token, string message)
        {
            if (token.type == TokenType.EOF)
            {
                CYoureSharp.ExternalReport(token.line, " at end", message);
            }
            else
            {
                CYoureSharp.ExternalReport(token.line, " at '" + token.lexeme + "'", message);
            }

            return new ParseError(message);
        }
    }
}
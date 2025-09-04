using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.ExceptionServices;

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
                        firstVar = Advance(); // TEMPORARY FOR NOW
                        Token secondVar = Advance();
                        secondVar = Advance(); // TEMPORARY FOR NOW

                        if (firstVar.type != TokenType.NUMBER)
                        {
                            throw Error(firstVar, $"Unexpected Token: {token.lexeme}");
                        }

                        if (secondVar.type != TokenType.NUMBER)
                        {
                            throw Error(secondVar, $"Unexpected Token: {token.lexeme}");
                        }

                        return new Expr.SwapExpr(firstVar.lexeme, secondVar.lexeme);
                    }
                case TokenType.SELECT:
                    {
                        Token firstVar_Select = Advance();
                        firstVar_Select = Advance(); // TEMPORARY FOR NOW
                        Token secondVar_Select = Advance();
                        secondVar_Select = Advance(); // TEMPORARY FOR NOW
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
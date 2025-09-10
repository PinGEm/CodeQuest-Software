using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace CYoureSharpPackage
{
    class Scanner
    {
        private readonly string source;
        private readonly List<Token> tokens = new List<Token>();

        private int start = 0;
        private int current = 0;
        private int line = 1;

        private static readonly Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>()
        {
            {"func", TokenType.FUNC},
            {"up", TokenType.UP},
            {"down", TokenType.DOWN},
            {"left", TokenType.LEFT},
            {"right", TokenType.RIGHT},
            {"swap", TokenType.SWAP},
            {"select", TokenType.SELECT}
        };

        public Scanner(string source, int line)
        {
            this.source = source;
            this.line = line;
        }

        public List<Token> ScanTokens()
        {
            while (!IsAtEnd())
            {
                start = current;
                ScanToken();
            }

            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }

        void ScanToken()
        {
            char c = Advance();
            switch (c)
            {
                case '(':
                    AddToken(TokenType.LEFT_PAREN);
                    break;
                case ')':
                    AddToken(TokenType.RIGHT_PAREN);
                    break;
                case '{':
                    AddToken(TokenType.LEFT_BRACE);
                    break;
                case '}':
                    AddToken(TokenType.RIGHT_BRACE);
                    break;
                case ',':
                    AddToken(TokenType.COMMA);
                    break;
                /*case ';':
                    AddToken(TokenType.SEMICOLON);
                    break; */
                    // IGNORING WHITE SPACE
                case ' ':
                case '\n':
                case '\r':
                case '\t':
                case '\'':
                case '\"':
                case '\b':
                case '\v':
                    // IGNORING WHITE SPACE
                    break;
                default:
                    if (IsDigit(c))
                    {
                        Number();
                    }
                    else if (isAlpha(c))
                    {
                        Identifier();
                    }
                    else
                    {
                        CYoureSharp.Error(line, $"Unexpected Character: '{c}'");
                    }
                    break;
            }
        }

        private void Identifier()
        {
            while (isAlphaNumeric(Peek()))
            {
                Advance();
            }

            string text = source.Substring(start, current - start);

            if (keywords.TryGetValue(text, out TokenType type))
            {
                AddToken(type);
            }
            else
            {
                AddToken(TokenType.IDENTIFIER);
            }
        }

        private void Number()
        {
            while (IsDigit(Peek()))
            {
                Advance();
            }

            AddToken(TokenType.NUMBER, int.Parse(source.Substring(start, current - start)));
        }

        private char Peek()
        {
            if (IsAtEnd()) return '\0';
            return source[current];
        }

        private bool isAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z') || 
                    c == '_';
        }

        private bool isAlphaNumeric(char c)
        {
            return isAlpha(c) || IsDigit(c);
        }

        bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool IsAtEnd()
        {
            return current >= source.Length;
        }

        char Advance()
        {
            return source[current++]; // consumes the next character in the source file and returns it. Goes to the next character.
        }

        void AddToken(TokenType type, object literal = null)
        {
            // output, grabs the text of the current lexeme and creates a new token.
            string text = source.Substring(start, current - start);
            tokens.Add(new Token(type, text, literal, line));
        }
    }
}
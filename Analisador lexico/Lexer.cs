using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Analisador_lexico
{
    public class Lexer
    {
        private static readonly Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>
        {
            { "int", TokenType.Keyword },
            { "if", TokenType.Keyword },
            { "else", TokenType.Keyword },
            // Adicione outras palavras-chave conforme necessário
        };

        private static readonly Regex IdentifierRegex = new Regex(@"^[a-zA-Z_][a-zA-Z0-9_]*");
        private static readonly Regex LiteralRegex = new Regex(@"^\d+");
        private static readonly Regex OperatorRegex = new Regex(@"^[+\-*/=]");
        private static readonly Regex SeparatorRegex = new Regex(@"^[;{}()]");

        public List<Token> Analyze(string code)
        {
            var tokens = new List<Token>();
            int line = 1, column = 1;
            int i = 0;

            while (i < code.Length)
            {
                char currentChar = code[i];

                if (char.IsWhiteSpace(currentChar))
                {
                    if (currentChar == '\n')
                    {
                        line++;
                        column = 1;
                    }
                    else
                    {
                        column++;
                    }
                    i++;
                    continue;
                }

                string remainingCode = code.Substring(i);

                // Palavras-chave e identificadores
                if (IdentifierRegex.IsMatch(remainingCode))
                {
                    Match match = IdentifierRegex.Match(remainingCode);
                    string value = match.Value;
                    tokens.Add(new Token
                    {
                        Type = Keywords.ContainsKey(value) ? Keywords[value] : TokenType.Identifier,
                        Value = value,
                        Line = line,
                        Column = column
                    });
                    i += value.Length;
                    column += value.Length;
                    continue;
                }

                // Literais
                if (LiteralRegex.IsMatch(remainingCode))
                {
                    Match match = LiteralRegex.Match(remainingCode);
                    string value = match.Value;
                    tokens.Add(new Token
                    {
                        Type = TokenType.Literal,
                        Value = value,
                        Line = line,
                        Column = column
                    });
                    i += value.Length;
                    column += value.Length;
                    continue;
                }

                // Operadores
                if (OperatorRegex.IsMatch(remainingCode))
                {
                    Match match = OperatorRegex.Match(remainingCode);
                    string value = match.Value;
                    tokens.Add(new Token
                    {
                        Type = TokenType.Operator,
                        Value = value,
                        Line = line,
                        Column = column
                    });
                    i += value.Length;
                    column += value.Length;
                    continue;
                }

                // Separadores
                if (SeparatorRegex.IsMatch(remainingCode))
                {
                    Match match = SeparatorRegex.Match(remainingCode);
                    string value = match.Value;
                    tokens.Add(new Token
                    {
                        Type = TokenType.Separator,
                        Value = value,
                        Line = line,
                        Column = column
                    });
                    i += value.Length;
                    column += value.Length;
                    continue;
                }

                // Se nenhum token foi reconhecido, marque como desconhecido
                tokens.Add(new Token
                {
                    Type = TokenType.Unknown,
                    Value = currentChar.ToString(),
                    Line = line,
                    Column = column
                });

                i++;
                column++;
            }

            return tokens;
        }

    }
}

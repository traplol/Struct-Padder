using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace StructPadder
{
    class Tokenizer
    {
        public static List<Token> Tokenize(string text)
        {
            var tokens = new List<Token>();

            int idx = 0;
            int line = 1;
            while (idx < text.Length)
            {
                Token.TokenDefinition matchedDefinition = null;
                int matchLength = 0;

                foreach (var rule in Token.Rules)
                {
                    var match = rule.Regex.Match(text, idx);
                    if (match.Success && match.Index == idx)
                    {
                        matchedDefinition = rule;
                        matchLength = match.Length;
                        break;
                    }
                }
                if (matchedDefinition == null)
                {
                    throw new FormatException(string.Format("Unrecognized symbol '{0}' (line {1}).", text[idx], line));
                }
                if (matchedDefinition == Token.EndOfLineRule)
                {
                    line++;
                }
                var str = text.Substring(idx, matchLength);
                if (!matchedDefinition.IsIgnored)
                {
                    tokens.Add(new Token(line, matchedDefinition.TokenType, str));
                }

                idx += matchLength;
            }

            return tokens;
        }
    }
}

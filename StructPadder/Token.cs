using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace StructPadder
{
    internal class Token
    {
        public enum TokenTypes
        {
            BadToken,

            Ident,
            IntNum,
            Star,
            Equals,
            Colon,
            Semicolon,
            LSqBrack,
            RSqBrack,
            LCurly,
            RCurly,
            KSigned,
            KUnsigned,
            KPtrSize,
            KStructOrClass,
        }

        public TokenTypes Type { get; }
        public string StringValue { get; }
        public object Value { get; }
        public int Line { get; }

        public Token(int line, TokenTypes type, string stringValue)
        {
            Line = line;
            StringValue = stringValue;
            Type = type;
            Value = GetTokenValue(Type, stringValue);
        }

        public static TokenDefinition EndOfLineRule = new TokenDefinition(TokenTypes.BadToken, new Regex("\r\n|\n"),
            true);
        public static readonly TokenDefinition[] Rules =
        {
            new TokenDefinition(TokenTypes.BadToken, new Regex("[ \t]+"), true), 
            EndOfLineRule,
            new TokenDefinition(TokenTypes.BadToken, new Regex(@"//.*"), true), 
            new TokenDefinition(TokenTypes.BadToken, new Regex(@"/\*.*\*/"), true), 

            new TokenDefinition(TokenTypes.Star, new Regex(@"\*")), 
            new TokenDefinition(TokenTypes.Equals, new Regex(@"=")), 
            new TokenDefinition(TokenTypes.Colon, new Regex(@":")), 
            new TokenDefinition(TokenTypes.Semicolon, new Regex(@";")), 

            new TokenDefinition(TokenTypes.LSqBrack, new Regex(@"\[")), 
            new TokenDefinition(TokenTypes.RSqBrack, new Regex(@"\]")), 
            new TokenDefinition(TokenTypes.LCurly, new Regex(@"\{")), 
            new TokenDefinition(TokenTypes.RCurly, new Regex(@"\}")), 

            new TokenDefinition(TokenTypes.KSigned, new Regex(@"signed")), 
            new TokenDefinition(TokenTypes.KUnsigned, new Regex(@"unsigned")), 
            new TokenDefinition(TokenTypes.KPtrSize, new Regex(@"ptrsize")), 
            new TokenDefinition(TokenTypes.KStructOrClass, new Regex(@"struct|class")), 

            new TokenDefinition(TokenTypes.Ident, new Regex(@"[a-zA-Z_]+[a-zA-Z0-9_]*")),
            new TokenDefinition(TokenTypes.IntNum, new Regex(@"(0[xX][0-9a-fA-F]+)|([0-9]+)")), 

        };

        public static object GetTokenValue(TokenTypes type, string stringValue)
        {
            switch (type)
            {
                case TokenTypes.IntNum:
                    return GetIntNum(stringValue);

                case TokenTypes.Ident:
                case TokenTypes.Star:
                case TokenTypes.Equals:
                case TokenTypes.Colon:
                case TokenTypes.Semicolon:
                case TokenTypes.LSqBrack:
                case TokenTypes.RSqBrack:
                case TokenTypes.LCurly:
                case TokenTypes.RCurly:
                case TokenTypes.KSigned:
                case TokenTypes.KUnsigned:
                case TokenTypes.KPtrSize:
                case TokenTypes.KStructOrClass:
                    return stringValue;
            }
            return null;
        }

        private static int GetIntNum(string intNum)
        {
            int value;

            if (int.TryParse(intNum, NumberStyles.Integer, CultureInfo.CurrentCulture, out value))
            {
                return value;
            }
            return Convert.ToInt32(intNum, 16);
        }

        public class TokenDefinition
        {
            public TokenTypes TokenType { get; }
            public Regex Regex { get; }
            public bool IsIgnored { get; }

            public TokenDefinition(TokenTypes tokenType, Regex regex, bool isIgnored=false)
            {
                TokenType = tokenType;
                Regex = regex;
                IsIgnored = isIgnored;
            }
        }
    }
}

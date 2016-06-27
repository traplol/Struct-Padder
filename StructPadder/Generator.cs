﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructPadder
{
    class Generator
    {
        public static int PointerSize = 4;

        public static List<Struct> Generate(List<Token> tokens)
        {
            var structs = new List<Struct>();

            try
            {
                int i = 0;
                while (i < tokens.Count)
                {
                    var s = ParseToken(tokens, ref i);
                    if (s != null)
                    {
                        structs.Add(s);
                        MemberTypeTable.AddUserType(s);
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                // BAD SHIT HAPPENED!
            }
            return structs;
        }

        private static Token Expect(Token.TokenTypes type, List<Token> tokens, ref int idx)
        {
            var tk = tokens[idx];
            if (tk.Type == type)
            {
                idx++;
                return tk;
            }
            throw new ArgumentException(string.Format("Unexpected token: '{0}' at line:{1}", tk.Type, tk.Line));
        }

        private static Token Accept(Token.TokenTypes type, List<Token> tokens, ref int idx)
        {
            var tk = tokens[idx];
            if (tk.Type == type)
            {
                idx++;
                return tk;
            }
            return null;
        }

        private static Struct ParseToken(List<Token> tokens, ref int idx)
        {
            var tk = tokens[idx];
            switch (tk.Type)
            {
                case Token.TokenTypes.KStructOrClass:
                    return ParseStruct(tokens, ref idx);
                case Token.TokenTypes.KPtrSize:
                    return ParsePtrSize(tokens, ref idx);
            }
            throw new ArgumentException(string.Format("Unexpected token: '{0}' at line:{1}", tk.Type, tk.Line));
        }

        private static Struct ParseStruct(List<Token> tokens, ref int idx)
        {
            Expect(Token.TokenTypes.KStructOrClass, tokens, ref idx);
            var ident = Expect(Token.TokenTypes.Ident, tokens, ref idx);
            Expect(Token.TokenTypes.LCurly, tokens, ref idx);

            var s = new Struct(ident.StringValue);

            Member m;
            while ((m = ParseMember(tokens, ref idx)) != null)
            {
                s.AddMember(m);
            }

            Expect(Token.TokenTypes.RCurly, tokens, ref idx);
            Expect(Token.TokenTypes.Semicolon, tokens, ref idx);
            return s;
        }

        private static Member ParseMember(List<Token> tokens, ref int idx)
        {
            var type = Accept(Token.TokenTypes.Ident, tokens, ref idx);
            if (type != null)
            {
                int numStars = 0;
                while (Accept(Token.TokenTypes.Star, tokens, ref idx) != null)
                {
                    numStars++;
                }

                var ident = Expect(Token.TokenTypes.Ident, tokens, ref idx);
                if (Accept(Token.TokenTypes.LSqBrack, tokens, ref idx) != null)
                {
                    var numElems = Expect(Token.TokenTypes.IntNum, tokens, ref idx);
                    Expect(Token.TokenTypes.RSqBrack, tokens, ref idx);

                    if (Accept(Token.TokenTypes.Colon, tokens, ref idx) != null)
                    {
                        var offset = Expect(Token.TokenTypes.IntNum, tokens, ref idx);
                        Expect(Token.TokenTypes.Semicolon, tokens, ref idx);
                        return Member.CreateArray(type.StringValue, ident.StringValue, (int)offset.Value, numStars, (int)numElems.Value);
                    }

                    Expect(Token.TokenTypes.Semicolon, tokens, ref idx);

                    return Member.CreateArrayRelative(type.StringValue, ident.StringValue, numStars, (int)numElems.Value);
                }
                else
                {
                    if (Accept(Token.TokenTypes.Colon, tokens, ref idx) != null)
                    {
                        var offset = Expect(Token.TokenTypes.IntNum, tokens, ref idx);
                        Expect(Token.TokenTypes.Semicolon, tokens, ref idx);
                        if (numStars > 0)
                        {
                            return Member.CreatePointer(type.StringValue, ident.StringValue, (int)offset.Value, numStars);
                        }
                        return Member.CreateValue(type.StringValue, ident.StringValue, (int)offset.Value);
                    }
                    Expect(Token.TokenTypes.Semicolon, tokens, ref idx);
                    if (numStars > 0)
                    {
                        return Member.CreatePointerRelative(type.StringValue, ident.StringValue, numStars);
                    }
                    return Member.CreateValueRelative(type.StringValue, ident.StringValue);
                }
            }
            return null;
        }

        private static Struct ParsePtrSize(List<Token> tokens, ref int idx)
        {
            Expect(Token.TokenTypes.KPtrSize, tokens, ref idx);
            Expect(Token.TokenTypes.Equals, tokens, ref idx);
            var tk = Expect(Token.TokenTypes.IntNum, tokens, ref idx);
            PointerSize = (int)tk.Value;
            Expect(Token.TokenTypes.Semicolon, tokens, ref idx);
            return null;
        }
    }
}
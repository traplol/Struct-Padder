using System;
using System.Text;

namespace StructPadder
{
    class Member
    {
        public MemberType Type { get; }
        public string Name { get; }
        public int Offset { get; set; }
        public int NumStars { get; }
        public int NumElements { get; }
        public bool IsRelative { get; }


        public int Size => GetSize();

        private Member(MemberType type, string name, int numStars, int numElements)
        {
            Type = type;
            Name = name;
            Offset = 0;
            IsRelative = true;
            NumStars = numStars;
            NumElements = numElements;
        }
        private Member(MemberType type, string name, int offset, int numStars, int numElements)
        {
            Type = type;
            Name = name;
            Offset = offset;
            IsRelative = false;
            NumStars = numStars;
            NumElements = numElements;
        }

        public static Member CreateArrayRelative(string typeName, string name, int numStars, int numElements)
        {
            var type = MemberTypeTable.GetMemberType(typeName);
            return CreateArrayRelative(type, name, numStars, numElements);
        }

        public static Member CreateArrayRelative(MemberType type, string name, int numStars, int numElements)
        {
            if (numElements <= 0)
            {
                throw new ArgumentException("numElements", "numElements cannot be <= 0!");
            }
            return new Member(type, name, numStars, numElements);
        }

        public static Member CreateArray(string typeName, string name, int offset, int numStars, int numElements)
        {
            var type = MemberTypeTable.GetMemberType(typeName);
            return CreateArray(type, name, offset, numStars, numElements);
        }
        public static Member CreateArray(MemberType type, string name, int offset, int numStars, int numElements)
        {
            if (numElements <= 0)
            {
                throw new ArgumentException("numElements", "numElements cannot be <= 0!");
            }
            return new Member(type, name, offset, numStars, numElements);
        }

        public static Member CreatePointerRelative(string typeName, string name, int numStars)
        {
            var type = MemberTypeTable.GetMemberType(typeName);
            return CreatePointerRelative(type, name, numStars);
        }
        public static Member CreatePointerRelative(MemberType type, string name, int numStars)
        {
            return new Member(type, name, numStars, 1);
        }

        public static Member CreatePointer(string typeName, string name, int offset, int numStars)
        {
            var type = MemberTypeTable.GetMemberType(typeName);
            return CreatePointer(type, name, offset, numStars);
        }
        public static Member CreatePointer(MemberType type, string name, int offset, int numStars)
        {
            return new Member(type, name, offset, numStars, 1);
        }

        public static Member CreateValueRelative(string typeName, string name)
        {
            var type = MemberTypeTable.GetMemberType(typeName);
            return CreateValueRelative(type, name);
        }
        public static Member CreateValueRelative(MemberType type, string name)
        {
            return new Member(type, name, 0, 1);
        }

        public static Member CreateValue(string typeName, string name, int offset)
        {
            var type = MemberTypeTable.GetMemberType(typeName);
            return CreateValue(type, name, offset);
        }
        public static Member CreateValue(MemberType type, string name, int offset)
        {
            return new Member(type, name, offset, 0, 1);
        }

        private int GetSize()
        {
            if (NumStars > 0)
            {
                return Generator.PointerSize;
            }
            return NumElements*Type.Size;
        }

        public override string ToString()
        {
            var stars = new StringBuilder();
            for (int i = 0; i < NumStars; ++i)
            {
                stars.Append("*");
            }
            if (NumElements > 1)
            {
                return string.Format("{0} {1}{2}[0x{3:X}]", Type.Name, stars, Name, NumElements);
            }
            return string.Format("{0} {1}{2}", Type.Name, stars, Name);
        }
    }
}

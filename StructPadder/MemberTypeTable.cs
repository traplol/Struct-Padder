using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructPadder
{
    class MemberTypeTable
    {
        private static readonly Dictionary<string, MemberType> TypeTable = new Dictionary<string, MemberType>();

        public static void AddBuiltinType(string name, int size)
        {
            if (!TypeTable.ContainsKey(name))
            {
                TypeTable.Add(name, new BuiltinMemberType(name, size));
            }
            else
            {
                throw new ArgumentException(string.Format("'{0}' already defined with size {1}", name, size));
            }
        }

        public static void AddUserType(Struct type)
        {
            TypeTable[type.Name] = type;
        }

        public static MemberType GetMemberType(string name)
        {
            return TypeTable.ContainsKey(name) ? TypeTable[name] : null;
        }

        public static int GetMemberTypeSize(string name)
        {
            return TypeTable.ContainsKey(name) ? TypeTable[name].Size : 0;
        }
    }
}

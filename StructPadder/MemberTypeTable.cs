using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructPadder
{
    class MemberTypeTable
    {
        private static readonly Dictionary<string, MemberType> BuiltinTypes = new Dictionary<string, MemberType>();
        private static readonly Dictionary<string, MemberType> UserTypes = new Dictionary<string, MemberType>();

        public static void AddBuiltinType(string name, int size)
        {
            if (!BuiltinTypes.ContainsKey(name))
            {
                BuiltinTypes.Add(name, new PrimitiveType(name, size));
            }
            else
            {
                throw new ArgumentException(string.Format("'{0}' already defined with size {1}", name, size));
            }
        }

        public static void ClearUserTypes()
        {
            UserTypes.Clear();
        }

        public static void AddUserType(Struct type)
        {
            if (!UserTypes.ContainsKey(type.Name) && !BuiltinTypes.ContainsKey(type.Name))
            {
                UserTypes.Add(type.Name, type);
            }
            else
            {
                var definedType = GetMemberType(type.Name);
                throw new ArgumentException(string.Format("'{0}' already defined with size {1}", definedType.Name, definedType.Size));
            }
        }

        public static void AddUserType(string name, int size)
        {
            if (!UserTypes.ContainsKey(name) && !BuiltinTypes.ContainsKey(name))
            {
                UserTypes.Add(name, new PrimitiveType(name, size));
            }
            else
            {
                var definedType = GetMemberType(name);
                throw new ArgumentException(string.Format("'{0}' already defined with size {1}", definedType.Name, definedType.Size));
            }
        }

        public static MemberType GetMemberType(string name)
        {
            if (UserTypes.ContainsKey(name))
            {
                return UserTypes[name];
            }
            return BuiltinTypes.ContainsKey(name) ? BuiltinTypes[name] : null;
        }

        public static int GetMemberTypeSize(string name)
        {
            if (UserTypes.ContainsKey(name))
            {
                return UserTypes[name].Size;
            }
            return BuiltinTypes.ContainsKey(name) ? BuiltinTypes[name].Size : 0;
        }
    }
}

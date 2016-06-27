using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructPadder
{
    class BuiltinMemberType : MemberType
    {
        public override sealed int Size { get; set; }

        public BuiltinMemberType(string name, int size)
            :base(name)
        {
            Size = size;
        }
    }
}

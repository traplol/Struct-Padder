using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructPadder
{
    class PrimitiveType : MemberType
    {
        public override sealed int Size { get; set; }

        public PrimitiveType(string name, int size)
            :base(name)
        {
            Size = size;
        }
    }
}

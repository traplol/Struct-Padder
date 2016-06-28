using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StructPadder
{
    class Struct : MemberType
    {
        private readonly List<Member> _realMembers;

        public override sealed int Size => CalculateSize();

        public Struct(string name)
            : base(name)
        {
            _realMembers = new List<Member>();
        }

        public void AddMember(Member member)
        {
            _realMembers.Add(member);
        }

        private List<Member> GetPaddedMembersList()
        {
            _realMembers.Sort((m1, m2) => m1.Offset.CompareTo(m2.Offset));
            var paddedList = new List<Member>();

            int offset = 0;
            foreach (var m in _realMembers)
            {
                if (m.Type == null)
                {
                    throw new Exception(string.Format("type not defined at line:{0}!", m.LineNum));
                }
                if (m.Offset - offset < 0)
                {
                    throw new Exception(string.Format("Struct alignment wrong at line:{0}!", m.LineNum));
                }
                if (m.Offset - offset  > 0)
                {
                    var size = m.Offset - offset;
                    paddedList.Add(Member.CreateArray(
                        "char",
                        string.Format("_pad_0x{0:X4}", offset),
                        -1,
                        offset,
                        0,
                        size));
                    offset += size;
                }
                paddedList.Add(m);
                offset += m.Size;
            }

            return paddedList;
        }

        private int CalculateSize()
        {
            var paddedList = GetPaddedMembersList();
            return paddedList.Sum(m => m.Size);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("struct {0} {{", Name));

            foreach (var member in GetPaddedMembersList())
            {
                sb.AppendLine(string.Format("\t{0}; \t// offs: 0x{1:X}", member, member.Offset));
            }

            sb.AppendLine(string.Format("}}; // size: 0x{0:X}", Size));
            return sb.ToString();
        }
    }
}

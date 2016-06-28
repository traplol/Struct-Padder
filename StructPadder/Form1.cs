using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StructPadder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inputTb.Font = new Font(FontFamily.GenericMonospace, 12);
            inputTb.AcceptsTab = true;
            outputTb.Font = new Font(FontFamily.GenericMonospace, 12);

            MemberTypeTable.AddBuiltinType("char", 1);
            MemberTypeTable.AddBuiltinType("int8", 1);
            MemberTypeTable.AddBuiltinType("int16", 2);
            MemberTypeTable.AddBuiltinType("int32", 4);
            MemberTypeTable.AddBuiltinType("int64", 8);

            MemberTypeTable.AddBuiltinType("float", 4);
            MemberTypeTable.AddBuiltinType("double", 8);

            MemberTypeTable.AddBuiltinType("D3DXVECTOR2", 2 * MemberTypeTable.GetMemberTypeSize("float"));
            MemberTypeTable.AddBuiltinType("D3DXVECTOR3", 3 * MemberTypeTable.GetMemberTypeSize("float"));
            MemberTypeTable.AddBuiltinType("D3DXMATRIX", 4 * 4 * MemberTypeTable.GetMemberTypeSize("float"));


            var s = new Struct("Player");
            s.AddMember(Member.CreateValue("int32", "Health", 0x10));
            s.AddMember(Member.CreateValue("int32", "Mana", 0x14));
            s.AddMember(Member.CreateValue("D3DXVECTOR3", "Position1", 0x150));
            s.AddMember(Member.CreateValueRelative("D3DXVECTOR3", "Position2"));
            outputTb.Text = s.ToString();
        }

        private void inputTb_TextChanged(object sender, EventArgs e)
        {
            outputTb.Clear();
            List<Token> tokens;
            try
            {
                tokens = Tokenizer.Tokenize(inputTb.Text);
            }
            catch (FormatException ex)
            {
                outputTb.Text = ex.Message;
                return;
            }

            try
            {
                MemberTypeTable.ClearUserTypes();
                var structs = Generator.Generate(tokens);
                foreach (var s in structs)
                {
                    outputTb.AppendText(s.ToString());
                    outputTb.AppendText("\r\n");
                }
            }
            catch (Exception ex)
            {
                outputTb.Text = ex.Message;
            }
        }
    }
}

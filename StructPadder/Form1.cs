using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace StructPadder
{
    public partial class Form1 : Form
    {

        private string _currentOpenFilePath;
        private string _currentSaveFilePath;

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
            MemberTypeTable.AddBuiltinType("__int8", 1);
            MemberTypeTable.AddBuiltinType("__int16", 2);
            MemberTypeTable.AddBuiltinType("__int32", 4);
            MemberTypeTable.AddBuiltinType("__int64", 8);

            MemberTypeTable.AddBuiltinType("float", 4);
            MemberTypeTable.AddBuiltinType("double", 8);

            MemberTypeTable.AddBuiltinType("D3DXVECTOR2", 2 * MemberTypeTable.GetMemberTypeSize("float"));
            MemberTypeTable.AddBuiltinType("D3DXVECTOR3", 3 * MemberTypeTable.GetMemberTypeSize("float"));
            MemberTypeTable.AddBuiltinType("D3DXMATRIX", 4 * 4 * MemberTypeTable.GetMemberTypeSize("float"));

            MemberTypeTable.AliasBuiltinType("int8", "__int8");
            MemberTypeTable.AliasBuiltinType("int16", "__int16");
            MemberTypeTable.AliasBuiltinType("int32", "__int32");
            MemberTypeTable.AliasBuiltinType("int64", "__int64");

            MemberTypeTable.AliasBuiltinType("BYTE", "__int8");
            MemberTypeTable.AliasBuiltinType("WORD", "__int16");
            MemberTypeTable.AliasBuiltinType("DWORD", "__int32");
            MemberTypeTable.AliasBuiltinType("QWORD", "__int64");

            const string example =
@"struct Player {
    int32 Health : 0x10;
    int32 Mana : 0x14;
    D3DXVECTOR3 Position1 : 0x150;
    D3DXVECTOR3 Position2;
};
";
            inputTb.Text = example;
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _currentOpenFilePath = openFileDialog.InitialDirectory + openFileDialog.FileName;
                try
                {
                    inputTb.Text = File.ReadAllText(_currentOpenFilePath);
                }
                catch (Exception ex)
                {
                    outputTb.Text = ex.Message;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_currentSaveFilePath))
            {
                saveAsToolStripMenuItem_Click(sender, e);
                return;
            }

            SaveOutput(_currentSaveFilePath);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _currentSaveFilePath = saveFileDialog.InitialDirectory + saveFileDialog.FileName;
                SaveOutput(_currentSaveFilePath);
            }
        }

        private void SaveOutput(string path)
        {
            try
            {
                File.WriteAllText(path, outputTb.Text);
            }
            catch (Exception ex)
            {
                outputTb.Text = ex.Message;
            }
        }
    }
}

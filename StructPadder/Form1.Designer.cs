namespace StructPadder
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputTb = new System.Windows.Forms.TextBox();
            this.outputTb = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputTb
            // 
            this.inputTb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputTb.Location = new System.Drawing.Point(0, 0);
            this.inputTb.Multiline = true;
            this.inputTb.Name = "inputTb";
            this.inputTb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.inputTb.Size = new System.Drawing.Size(511, 430);
            this.inputTb.TabIndex = 0;
            this.inputTb.TextChanged += new System.EventHandler(this.inputTb_TextChanged);
            // 
            // outputTb
            // 
            this.outputTb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTb.Location = new System.Drawing.Point(0, 0);
            this.outputTb.Multiline = true;
            this.outputTb.Name = "outputTb";
            this.outputTb.ReadOnly = true;
            this.outputTb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTb.Size = new System.Drawing.Size(523, 430);
            this.outputTb.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.inputTb);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.outputTb);
            this.splitContainer1.Size = new System.Drawing.Size(1038, 430);
            this.splitContainer1.SplitterDistance = 511;
            this.splitContainer1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 430);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Struct Padder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox inputTb;
        private System.Windows.Forms.TextBox outputTb;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}


namespace DebugTool
{
    partial class frmAttenuator
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClearTxt = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.fbtnReadNum = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btnClearTxt);
            this.groupBox3.Controls.Add(this.richTextBox1);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.fbtnReadNum);
            this.groupBox3.Location = new System.Drawing.Point(40, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(709, 381);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "读数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "请输入读数次数：";
            // 
            // btnClearTxt
            // 
            this.btnClearTxt.Location = new System.Drawing.Point(192, 112);
            this.btnClearTxt.Name = "btnClearTxt";
            this.btnClearTxt.Size = new System.Drawing.Size(75, 23);
            this.btnClearTxt.TabIndex = 10;
            this.btnClearTxt.Text = "清空信息";
            this.btnClearTxt.UseVisualStyleBackColor = true;
            this.btnClearTxt.Click += new System.EventHandler(this.BtnClearTxt_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(361, 49);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(342, 326);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // textBox2
            // 
            this.textBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox2.Location = new System.Drawing.Point(151, 72);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(116, 21);
            this.textBox2.TabIndex = 7;
            this.textBox2.MouseEnter += new System.EventHandler(this.TextBox2_MouseEnter);
            this.textBox2.MouseLeave += new System.EventHandler(this.TextBox2_MouseLeave);
            // 
            // fbtnReadNum
            // 
            this.fbtnReadNum.Location = new System.Drawing.Point(46, 112);
            this.fbtnReadNum.Name = "fbtnReadNum";
            this.fbtnReadNum.Size = new System.Drawing.Size(80, 23);
            this.fbtnReadNum.TabIndex = 8;
            this.fbtnReadNum.Text = "清洗盘读数";
            this.fbtnReadNum.UseVisualStyleBackColor = true;
            this.fbtnReadNum.Click += new System.EventHandler(this.FbtnReadNum_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(674, 411);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // frmAttenuator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAttenuator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAttenuator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAttenuator_FormClosed);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClearTxt;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button fbtnReadNum;
        private System.Windows.Forms.Button btnExit;
    }
}
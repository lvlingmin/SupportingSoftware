namespace DebugTool
{
    partial class frmPhoton
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
            this.btnClearTxt = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.fbtnReadNum = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.fbtnWashStepOrder = new System.Windows.Forms.Button();
            this.txtRotateNum = new System.Windows.Forms.TextBox();
            this.fbtnWashTrayRotate = new System.Windows.Forms.Button();
            this.fbtnWashTrayReset = new System.Windows.Forms.Button();
            this.fbtnWashReset = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fbtnWashSub = new System.Windows.Forms.Button();
            this.fbtnWashAdd = new System.Windows.Forms.Button();
            this.fbtnWashSave = new System.Windows.Forms.Button();
            this.txtWashIncream = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.cmbWashElecMachine = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.cmbWashPara = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClearTxt);
            this.groupBox3.Controls.Add(this.richTextBox1);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.fbtnReadNum);
            this.groupBox3.Location = new System.Drawing.Point(427, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(338, 403);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "读数";
            // 
            // btnClearTxt
            // 
            this.btnClearTxt.Location = new System.Drawing.Point(245, 39);
            this.btnClearTxt.Name = "btnClearTxt";
            this.btnClearTxt.Size = new System.Drawing.Size(75, 23);
            this.btnClearTxt.TabIndex = 10;
            this.btnClearTxt.Text = "清空信息";
            this.btnClearTxt.UseVisualStyleBackColor = true;
            this.btnClearTxt.Click += new System.EventHandler(this.BtnClearTxt_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(33, 81);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(287, 298);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // textBox2
            // 
            this.textBox2.ForeColor = System.Drawing.Color.LightGray;
            this.textBox2.Location = new System.Drawing.Point(33, 41);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(116, 21);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = "请输入读数次数";
            this.textBox2.MouseEnter += new System.EventHandler(this.TextBox2_MouseEnter);
            this.textBox2.MouseLeave += new System.EventHandler(this.TextBox2_MouseLeave);
            // 
            // fbtnReadNum
            // 
            this.fbtnReadNum.Location = new System.Drawing.Point(159, 39);
            this.fbtnReadNum.Name = "fbtnReadNum";
            this.fbtnReadNum.Size = new System.Drawing.Size(80, 23);
            this.fbtnReadNum.TabIndex = 8;
            this.fbtnReadNum.Text = "清洗盘读数";
            this.fbtnReadNum.UseVisualStyleBackColor = true;
            this.fbtnReadNum.Click += new System.EventHandler(this.FbtnReadNum_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.fbtnWashStepOrder);
            this.groupBox2.Controls.Add(this.txtRotateNum);
            this.groupBox2.Controls.Add(this.fbtnWashTrayRotate);
            this.groupBox2.Controls.Add(this.fbtnWashTrayReset);
            this.groupBox2.Controls.Add(this.fbtnWashReset);
            this.groupBox2.Location = new System.Drawing.Point(36, 234);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 185);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "快捷操作";
            // 
            // textBox3
            // 
            this.textBox3.ForeColor = System.Drawing.Color.LightGray;
            this.textBox3.Location = new System.Drawing.Point(22, 140);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(165, 21);
            this.textBox3.TabIndex = 7;
            this.textBox3.Text = "请输入清洗盘单步指令";
            this.textBox3.MouseEnter += new System.EventHandler(this.TextBox3_MouseEnter);
            this.textBox3.MouseLeave += new System.EventHandler(this.TextBox3_MouseLeave);
            // 
            // fbtnWashStepOrder
            // 
            this.fbtnWashStepOrder.Location = new System.Drawing.Point(193, 138);
            this.fbtnWashStepOrder.Name = "fbtnWashStepOrder";
            this.fbtnWashStepOrder.Size = new System.Drawing.Size(99, 23);
            this.fbtnWashStepOrder.TabIndex = 8;
            this.fbtnWashStepOrder.Text = "清洗盘单步指令";
            this.fbtnWashStepOrder.UseVisualStyleBackColor = true;
            this.fbtnWashStepOrder.Click += new System.EventHandler(this.FbtnWashStepOrder_Click);
            // 
            // txtRotateNum
            // 
            this.txtRotateNum.ForeColor = System.Drawing.Color.LightGray;
            this.txtRotateNum.Location = new System.Drawing.Point(22, 89);
            this.txtRotateNum.Name = "txtRotateNum";
            this.txtRotateNum.Size = new System.Drawing.Size(164, 21);
            this.txtRotateNum.TabIndex = 2;
            this.txtRotateNum.Text = "请输入旋转孔位步数参数";
            this.txtRotateNum.MouseEnter += new System.EventHandler(this.TxtRotateNum_MouseEnter);
            this.txtRotateNum.MouseLeave += new System.EventHandler(this.TxtRotateNum_MouseLeave);
            // 
            // fbtnWashTrayRotate
            // 
            this.fbtnWashTrayRotate.Location = new System.Drawing.Point(192, 87);
            this.fbtnWashTrayRotate.Name = "fbtnWashTrayRotate";
            this.fbtnWashTrayRotate.Size = new System.Drawing.Size(100, 23);
            this.fbtnWashTrayRotate.TabIndex = 4;
            this.fbtnWashTrayRotate.Text = "清洗盘旋转";
            this.fbtnWashTrayRotate.UseVisualStyleBackColor = true;
            this.fbtnWashTrayRotate.Click += new System.EventHandler(this.FbtnWashTrayRotate_Click);
            // 
            // fbtnWashTrayReset
            // 
            this.fbtnWashTrayReset.Location = new System.Drawing.Point(192, 39);
            this.fbtnWashTrayReset.Name = "fbtnWashTrayReset";
            this.fbtnWashTrayReset.Size = new System.Drawing.Size(100, 23);
            this.fbtnWashTrayReset.TabIndex = 3;
            this.fbtnWashTrayReset.Text = "清洗盘复位";
            this.fbtnWashTrayReset.UseVisualStyleBackColor = true;
            this.fbtnWashTrayReset.Click += new System.EventHandler(this.FbtnWashTrayReset_Click);
            // 
            // fbtnWashReset
            // 
            this.fbtnWashReset.Enabled = false;
            this.fbtnWashReset.Location = new System.Drawing.Point(22, 39);
            this.fbtnWashReset.Name = "fbtnWashReset";
            this.fbtnWashReset.Size = new System.Drawing.Size(100, 23);
            this.fbtnWashReset.TabIndex = 2;
            this.fbtnWashReset.Text = "清洗盘模块复位";
            this.fbtnWashReset.UseVisualStyleBackColor = true;
            this.fbtnWashReset.Click += new System.EventHandler(this.FbtnWashReset_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fbtnWashSub);
            this.groupBox1.Controls.Add(this.fbtnWashAdd);
            this.groupBox1.Controls.Add(this.fbtnWashSave);
            this.groupBox1.Controls.Add(this.txtWashIncream);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label50);
            this.groupBox1.Controls.Add(this.cmbWashElecMachine);
            this.groupBox1.Controls.Add(this.label51);
            this.groupBox1.Controls.Add(this.cmbWashPara);
            this.groupBox1.Location = new System.Drawing.Point(36, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 200);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数设置";
            // 
            // fbtnWashSub
            // 
            this.fbtnWashSub.Location = new System.Drawing.Point(277, 106);
            this.fbtnWashSub.Name = "fbtnWashSub";
            this.fbtnWashSub.Size = new System.Drawing.Size(44, 23);
            this.fbtnWashSub.TabIndex = 79;
            this.fbtnWashSub.Text = "-";
            this.fbtnWashSub.UseVisualStyleBackColor = true;
            this.fbtnWashSub.Click += new System.EventHandler(this.FbtnWashSub_Click);
            // 
            // fbtnWashAdd
            // 
            this.fbtnWashAdd.Location = new System.Drawing.Point(217, 106);
            this.fbtnWashAdd.Name = "fbtnWashAdd";
            this.fbtnWashAdd.Size = new System.Drawing.Size(44, 23);
            this.fbtnWashAdd.TabIndex = 78;
            this.fbtnWashAdd.Text = "+";
            this.fbtnWashAdd.UseVisualStyleBackColor = true;
            this.fbtnWashAdd.Click += new System.EventHandler(this.FbtnWashAdd_Click);
            // 
            // fbtnWashSave
            // 
            this.fbtnWashSave.Location = new System.Drawing.Point(22, 150);
            this.fbtnWashSave.Name = "fbtnWashSave";
            this.fbtnWashSave.Size = new System.Drawing.Size(100, 23);
            this.fbtnWashSave.TabIndex = 77;
            this.fbtnWashSave.Text = "保存参数";
            this.fbtnWashSave.UseVisualStyleBackColor = true;
            this.fbtnWashSave.Click += new System.EventHandler(this.FbtnWashSave_Click);
            // 
            // txtWashIncream
            // 
            this.txtWashIncream.Location = new System.Drawing.Point(87, 108);
            this.txtWashIncream.Name = "txtWashIncream";
            this.txtWashIncream.Size = new System.Drawing.Size(100, 21);
            this.txtWashIncream.TabIndex = 76;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 75;
            this.label1.Text = "参数名：";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label50.Location = new System.Drawing.Point(20, 111);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(41, 12);
            this.label50.TabIndex = 74;
            this.label50.Text = "增量：";
            // 
            // cmbWashElecMachine
            // 
            this.cmbWashElecMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWashElecMachine.FormattingEnabled = true;
            this.cmbWashElecMachine.Items.AddRange(new object[] {
            "清洗盘电机",
            "Z轴电机",
            "压杯电机"});
            this.cmbWashElecMachine.Location = new System.Drawing.Point(87, 78);
            this.cmbWashElecMachine.Name = "cmbWashElecMachine";
            this.cmbWashElecMachine.Size = new System.Drawing.Size(160, 20);
            this.cmbWashElecMachine.TabIndex = 72;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label51.Location = new System.Drawing.Point(20, 81);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(41, 12);
            this.label51.TabIndex = 73;
            this.label51.Text = "电机：";
            // 
            // cmbWashPara
            // 
            this.cmbWashPara.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWashPara.FormattingEnabled = true;
            this.cmbWashPara.Items.AddRange(new object[] {
            "压杯开始位置,Z轴夹管最低位",
            "压杯最低位置,Z轴夹管开始位",
            "清洗盘初始位置"});
            this.cmbWashPara.Location = new System.Drawing.Point(87, 47);
            this.cmbWashPara.Name = "cmbWashPara";
            this.cmbWashPara.Size = new System.Drawing.Size(234, 20);
            this.cmbWashPara.TabIndex = 70;
            this.cmbWashPara.SelectedIndexChanged += new System.EventHandler(this.CmbWashPara_SelectedIndexChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(690, 425);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // frmPhoton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 456);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPhoton";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计数工装";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmWash_FormClosed);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button fbtnReadNum;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button fbtnWashStepOrder;
        private System.Windows.Forms.TextBox txtRotateNum;
        private System.Windows.Forms.Button fbtnWashTrayRotate;
        private System.Windows.Forms.Button fbtnWashTrayReset;
        private System.Windows.Forms.Button fbtnWashReset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button fbtnWashSub;
        private System.Windows.Forms.Button fbtnWashAdd;
        private System.Windows.Forms.Button fbtnWashSave;
        private System.Windows.Forms.TextBox txtWashIncream;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.ComboBox cmbWashElecMachine;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.ComboBox cmbWashPara;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClearTxt;
    }
}
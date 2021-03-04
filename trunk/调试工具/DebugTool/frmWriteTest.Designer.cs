namespace DebugTool
{
    partial class frmWriteTest
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
            this.btnWriteIn = new System.Windows.Forms.Button();
            this.txtResultShow = new System.Windows.Forms.RichTextBox();
            this.num1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnWriteIn
            // 
            this.btnWriteIn.Location = new System.Drawing.Point(115, 223);
            this.btnWriteIn.Name = "btnWriteIn";
            this.btnWriteIn.Size = new System.Drawing.Size(108, 48);
            this.btnWriteIn.TabIndex = 0;
            this.btnWriteIn.Text = "执行";
            this.btnWriteIn.UseVisualStyleBackColor = true;
            this.btnWriteIn.Click += new System.EventHandler(this.btnWriteIn_Click);
            // 
            // txtResultShow
            // 
            this.txtResultShow.Location = new System.Drawing.Point(386, 61);
            this.txtResultShow.Name = "txtResultShow";
            this.txtResultShow.Size = new System.Drawing.Size(327, 210);
            this.txtResultShow.TabIndex = 1;
            this.txtResultShow.Text = "";
            // 
            // num1
            // 
            this.num1.Location = new System.Drawing.Point(126, 154);
            this.num1.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.num1.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(129, 21);
            this.num1.TabIndex = 2;
            this.num1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "参数：";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(638, 355);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "模块：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "功能：";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "通信板\t\t",
            "清洗板\t\t",
            "计数器板\t",
            "理杯机板\t",
            "加样板\t\t",
            "液位探测板\t",
            "加热板\t\t",
            "制冷板\t\t",
            "温育板"});
            this.comboBox1.Location = new System.Drawing.Point(126, 61);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(129, 20);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(126, 106);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(129, 20);
            this.comboBox2.TabIndex = 8;
            // 
            // frmWriteTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.num1);
            this.Controls.Add(this.txtResultShow);
            this.Controls.Add(this.btnWriteIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmWriteTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "泵阀&E2测试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmWriteTest_FormClosed);
            this.Load += new System.EventHandler(this.frmWriteTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWriteIn;
        private System.Windows.Forms.RichTextBox txtResultShow;
        private System.Windows.Forms.NumericUpDown num1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}
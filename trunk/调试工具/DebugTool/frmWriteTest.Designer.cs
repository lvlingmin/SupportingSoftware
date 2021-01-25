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
            ((System.ComponentModel.ISupportInitialize)(this.num1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnWriteIn
            // 
            this.btnWriteIn.Location = new System.Drawing.Point(91, 114);
            this.btnWriteIn.Name = "btnWriteIn";
            this.btnWriteIn.Size = new System.Drawing.Size(108, 48);
            this.btnWriteIn.TabIndex = 0;
            this.btnWriteIn.Text = "写入";
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
            this.num1.Location = new System.Drawing.Point(136, 62);
            this.num1.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.num1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(84, 21);
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
            this.label1.Location = new System.Drawing.Point(89, 64);
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
            // frmWriteTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.num1);
            this.Controls.Add(this.txtResultShow);
            this.Controls.Add(this.btnWriteIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmWriteTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通讯板写入测试";
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
    }
}
namespace DebugTool
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnIAP = new System.Windows.Forms.Button();
            this.btnAttenuator = new System.Windows.Forms.Button();
            this.btnLinearityTest = new System.Windows.Forms.Button();
            this.btnWriteTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(69, 47);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(122, 62);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "光子计数工装测试";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(335, 261);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(122, 62);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnIAP
            // 
            this.btnIAP.Location = new System.Drawing.Point(234, 47);
            this.btnIAP.Name = "btnIAP";
            this.btnIAP.Size = new System.Drawing.Size(122, 62);
            this.btnIAP.TabIndex = 4;
            this.btnIAP.Text = "IAP";
            this.btnIAP.UseVisualStyleBackColor = true;
            this.btnIAP.Click += new System.EventHandler(this.BtnIAP_Click);
            // 
            // btnAttenuator
            // 
            this.btnAttenuator.Location = new System.Drawing.Point(398, 47);
            this.btnAttenuator.Name = "btnAttenuator";
            this.btnAttenuator.Size = new System.Drawing.Size(122, 62);
            this.btnAttenuator.TabIndex = 5;
            this.btnAttenuator.Text = "衰减片工装测试";
            this.btnAttenuator.UseVisualStyleBackColor = true;
            this.btnAttenuator.Click += new System.EventHandler(this.BtnAttenuator_Click);
            // 
            // btnLinearityTest
            // 
            this.btnLinearityTest.Location = new System.Drawing.Point(69, 133);
            this.btnLinearityTest.Name = "btnLinearityTest";
            this.btnLinearityTest.Size = new System.Drawing.Size(122, 62);
            this.btnLinearityTest.TabIndex = 6;
            this.btnLinearityTest.Text = "计数器线性测试";
            this.btnLinearityTest.UseVisualStyleBackColor = true;
            this.btnLinearityTest.Click += new System.EventHandler(this.btnLinearityTest_Click);
            // 
            // btnWriteTest
            // 
            this.btnWriteTest.Location = new System.Drawing.Point(234, 133);
            this.btnWriteTest.Name = "btnWriteTest";
            this.btnWriteTest.Size = new System.Drawing.Size(122, 62);
            this.btnWriteTest.TabIndex = 7;
            this.btnWriteTest.Text = "泵阀&&E2测试";
            this.btnWriteTest.UseVisualStyleBackColor = true;
            this.btnWriteTest.Click += new System.EventHandler(this.btnWriteTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnWriteTest);
            this.Controls.Add(this.btnLinearityTest);
            this.Controls.Add(this.btnAttenuator);
            this.Controls.Add(this.btnIAP);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "调试工具";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnIAP;
        private System.Windows.Forms.Button btnAttenuator;
        private System.Windows.Forms.Button btnLinearityTest;
        private System.Windows.Forms.Button btnWriteTest;
    }
}


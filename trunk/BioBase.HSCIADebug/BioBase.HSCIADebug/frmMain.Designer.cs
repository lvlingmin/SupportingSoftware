namespace BioBase.HSCIADebug
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlPublic = new System.Windows.Forms.Panel();
            this.pnlbarUP = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlbarDown = new System.Windows.Forms.Panel();
            this.btnExit = new BioBase.HSCIADebug.CustomControl.FunctionButton(this.components);
            this.fbtnGroupTest = new BioBase.HSCIADebug.CustomControl.FunctionButton(this.components);
            this.functionButton3 = new BioBase.HSCIADebug.CustomControl.FunctionButton(this.components);
            this.btnPerformance = new BioBase.HSCIADebug.CustomControl.FunctionButton(this.components);
            this.functionButton1 = new BioBase.HSCIADebug.CustomControl.FunctionButton(this.components);
            this.pnlSidebar.SuspendLayout();
            this.pnlPublic.SuspendLayout();
            this.pnlbarUP.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.Controls.Add(this.btnExit);
            this.pnlSidebar.Controls.Add(this.fbtnGroupTest);
            this.pnlSidebar.Controls.Add(this.functionButton3);
            this.pnlSidebar.Controls.Add(this.btnPerformance);
            this.pnlSidebar.Controls.Add(this.functionButton1);
            this.pnlSidebar.Location = new System.Drawing.Point(865, 21);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(143, 485);
            this.pnlSidebar.TabIndex = 0;
            // 
            // pnlPublic
            // 
            this.pnlPublic.BackColor = System.Drawing.Color.LightBlue;
            this.pnlPublic.Controls.Add(this.pnlSidebar);
            this.pnlPublic.Location = new System.Drawing.Point(0, 132);
            this.pnlPublic.Name = "pnlPublic";
            this.pnlPublic.Size = new System.Drawing.Size(1032, 556);
            this.pnlPublic.TabIndex = 23;
            // 
            // pnlbarUP
            // 
            this.pnlbarUP.BackColor = System.Drawing.Color.Transparent;
            this.pnlbarUP.BackgroundImage = global::BioBase.HSCIADebug.Properties.Resources.其他界面按钮2;
            this.pnlbarUP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlbarUP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlbarUP.Controls.Add(this.label1);
            this.pnlbarUP.Location = new System.Drawing.Point(0, 0);
            this.pnlbarUP.Name = "pnlbarUP";
            this.pnlbarUP.Size = new System.Drawing.Size(1019, 132);
            this.pnlbarUP.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(210, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(604, 64);
            this.label1.TabIndex = 0;
            this.label1.Text = "全自动免疫分析仪监测工装";
            // 
            // pnlbarDown
            // 
            this.pnlbarDown.BackColor = System.Drawing.Color.LightBlue;
            this.pnlbarDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlbarDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlbarDown.Location = new System.Drawing.Point(0, 677);
            this.pnlbarDown.Name = "pnlbarDown";
            this.pnlbarDown.Size = new System.Drawing.Size(1008, 50);
            this.pnlbarDown.TabIndex = 20;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = global::BioBase.HSCIADebug.Properties.Resources.主界面按钮_;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.EnabledSet = true;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(14, 422);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(119, 58);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "退出系统";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // fbtnGroupTest
            // 
            this.fbtnGroupTest.BackgroundImage = global::BioBase.HSCIADebug.Properties.Resources.主界面按钮_;
            this.fbtnGroupTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fbtnGroupTest.EnabledSet = true;
            this.fbtnGroupTest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.fbtnGroupTest.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.fbtnGroupTest.Location = new System.Drawing.Point(14, 323);
            this.fbtnGroupTest.Name = "fbtnGroupTest";
            this.fbtnGroupTest.Size = new System.Drawing.Size(119, 58);
            this.fbtnGroupTest.TabIndex = 4;
            this.fbtnGroupTest.Text = "组合测试";
            this.fbtnGroupTest.UseVisualStyleBackColor = false;
            this.fbtnGroupTest.Click += new System.EventHandler(this.fbtnGroupTest_Click);
            // 
            // functionButton3
            // 
            this.functionButton3.BackgroundImage = global::BioBase.HSCIADebug.Properties.Resources.主界面按钮_;
            this.functionButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.functionButton3.EnabledSet = true;
            this.functionButton3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.functionButton3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.functionButton3.Location = new System.Drawing.Point(14, 224);
            this.functionButton3.Name = "functionButton3";
            this.functionButton3.Size = new System.Drawing.Size(119, 58);
            this.functionButton3.TabIndex = 3;
            this.functionButton3.Text = "仪器初始化";
            this.functionButton3.UseVisualStyleBackColor = false;
            this.functionButton3.Click += new System.EventHandler(this.functionButton3_Click);
            // 
            // btnPerformance
            // 
            this.btnPerformance.BackgroundImage = global::BioBase.HSCIADebug.Properties.Resources.主界面按钮_;
            this.btnPerformance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPerformance.EnabledSet = true;
            this.btnPerformance.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPerformance.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnPerformance.Location = new System.Drawing.Point(14, 125);
            this.btnPerformance.Name = "btnPerformance";
            this.btnPerformance.Size = new System.Drawing.Size(119, 58);
            this.btnPerformance.TabIndex = 2;
            this.btnPerformance.Text = "性能监测";
            this.btnPerformance.UseVisualStyleBackColor = true;
            this.btnPerformance.Click += new System.EventHandler(this.btnPerformance_Click);
            // 
            // functionButton1
            // 
            this.functionButton1.BackgroundImage = global::BioBase.HSCIADebug.Properties.Resources.主界面按钮_;
            this.functionButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.functionButton1.EnabledSet = true;
            this.functionButton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.functionButton1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.functionButton1.Location = new System.Drawing.Point(14, 26);
            this.functionButton1.Name = "functionButton1";
            this.functionButton1.Size = new System.Drawing.Size(119, 58);
            this.functionButton1.TabIndex = 1;
            this.functionButton1.Text = "功能监测";
            this.functionButton1.UseVisualStyleBackColor = false;
            this.functionButton1.Click += new System.EventHandler(this.functionButton1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 727);
            this.Controls.Add(this.pnlbarDown);
            this.Controls.Add(this.pnlbarUP);
            this.Controls.Add(this.pnlPublic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BIOBASE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlPublic.ResumeLayout(false);
            this.pnlbarUP.ResumeLayout(false);
            this.pnlbarUP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlPublic;
        private System.Windows.Forms.Panel pnlbarUP;
        private CustomControl.FunctionButton btnExit;
        private System.Windows.Forms.Label label1;
        private CustomControl.FunctionButton fbtnGroupTest;
        private CustomControl.FunctionButton functionButton3;
        private CustomControl.FunctionButton btnPerformance;
        private CustomControl.FunctionButton functionButton1;
        private System.Windows.Forms.Panel pnlbarDown;
    }
}
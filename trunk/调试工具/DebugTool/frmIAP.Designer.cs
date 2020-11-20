namespace DebugTool
{
    partial class frmIAP
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
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.cmbSendDelay = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadProgram = new System.Windows.Forms.Button();
            this.btnSelectBin = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.cmbZhenID = new System.Windows.Forms.ComboBox();
            this.lblDescribe = new System.Windows.Forms.Label();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.pgbLoad = new System.Windows.Forms.ProgressBar();
            this.label74 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox21.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.cmbSendDelay);
            this.groupBox21.Controls.Add(this.label1);
            this.groupBox21.Controls.Add(this.btnLoadProgram);
            this.groupBox21.Controls.Add(this.btnSelectBin);
            this.groupBox21.Controls.Add(this.txtFilePath);
            this.groupBox21.Controls.Add(this.cmbZhenID);
            this.groupBox21.Controls.Add(this.lblDescribe);
            this.groupBox21.Controls.Add(this.lblPercentage);
            this.groupBox21.Controls.Add(this.pgbLoad);
            this.groupBox21.Controls.Add(this.label74);
            this.groupBox21.Controls.Add(this.label70);
            this.groupBox21.Controls.Add(this.label29);
            this.groupBox21.Controls.Add(this.label30);
            this.groupBox21.Location = new System.Drawing.Point(175, 108);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(450, 235);
            this.groupBox21.TabIndex = 7;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "烧录程序";
            // 
            // cmbSendDelay
            // 
            this.cmbSendDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSendDelay.FormattingEnabled = true;
            this.cmbSendDelay.Items.AddRange(new object[] {
            "10",
            "15",
            "20"});
            this.cmbSendDelay.Location = new System.Drawing.Point(369, 27);
            this.cmbSendDelay.Name = "cmbSendDelay";
            this.cmbSendDelay.Size = new System.Drawing.Size(42, 20);
            this.cmbSendDelay.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(274, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "传输延迟(ms)：";
            // 
            // btnLoadProgram
            // 
            this.btnLoadProgram.Location = new System.Drawing.Point(185, 188);
            this.btnLoadProgram.Name = "btnLoadProgram";
            this.btnLoadProgram.Size = new System.Drawing.Size(75, 23);
            this.btnLoadProgram.TabIndex = 17;
            this.btnLoadProgram.Text = "烧录程序";
            this.btnLoadProgram.UseVisualStyleBackColor = true;
            this.btnLoadProgram.Click += new System.EventHandler(this.BtnLoadProgram_Click);
            // 
            // btnSelectBin
            // 
            this.btnSelectBin.Location = new System.Drawing.Point(369, 79);
            this.btnSelectBin.Name = "btnSelectBin";
            this.btnSelectBin.Size = new System.Drawing.Size(75, 23);
            this.btnSelectBin.TabIndex = 16;
            this.btnSelectBin.Text = "...";
            this.btnSelectBin.UseVisualStyleBackColor = true;
            this.btnSelectBin.Click += new System.EventHandler(this.Button1_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(81, 79);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(282, 21);
            this.txtFilePath.TabIndex = 15;
            // 
            // cmbZhenID
            // 
            this.cmbZhenID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZhenID.FormattingEnabled = true;
            this.cmbZhenID.Items.AddRange(new object[] {
            "计数器模组",
            "通讯模块",
            "理杯机模组",
            "加样机模块",
            "清洗模组",
            "报警模组",
            "制冷模组",
            "温育模块"});
            this.cmbZhenID.Location = new System.Drawing.Point(81, 27);
            this.cmbZhenID.Name = "cmbZhenID";
            this.cmbZhenID.Size = new System.Drawing.Size(128, 20);
            this.cmbZhenID.TabIndex = 14;
            // 
            // lblDescribe
            // 
            this.lblDescribe.AutoSize = true;
            this.lblDescribe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDescribe.Location = new System.Drawing.Point(84, 157);
            this.lblDescribe.Name = "lblDescribe";
            this.lblDescribe.Size = new System.Drawing.Size(23, 12);
            this.lblDescribe.TabIndex = 10;
            this.lblDescribe.Text = "...";
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPercentage.Location = new System.Drawing.Point(393, 135);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(17, 12);
            this.lblPercentage.TabIndex = 9;
            this.lblPercentage.Text = "0%";
            // 
            // pgbLoad
            // 
            this.pgbLoad.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pgbLoad.Location = new System.Drawing.Point(81, 131);
            this.pgbLoad.Name = "pgbLoad";
            this.pgbLoad.Size = new System.Drawing.Size(306, 23);
            this.pgbLoad.TabIndex = 7;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label74.Location = new System.Drawing.Point(10, 135);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(65, 12);
            this.label74.TabIndex = 3;
            this.label74.Text = "烧录进度：";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label70.Location = new System.Drawing.Point(9, 82);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(65, 12);
            this.label70.TabIndex = 2;
            this.label70.Text = "文件路径：";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label29.Location = new System.Drawing.Point(9, 30);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(65, 12);
            this.label29.TabIndex = 1;
            this.label29.Text = "电控板位：";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label30.Location = new System.Drawing.Point(12, 34);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(0, 12);
            this.label30.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(360, 349);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // frmIAP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox21);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmIAP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "程序下载";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmIAP_FormClosed);
            this.Load += new System.EventHandler(this.FrmIAP_Load);
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.Button btnSelectBin;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.ComboBox cmbZhenID;
        private System.Windows.Forms.Label lblDescribe;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.ProgressBar pgbLoad;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btnLoadProgram;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cmbSendDelay;
        private System.Windows.Forms.Label label1;
    }
}
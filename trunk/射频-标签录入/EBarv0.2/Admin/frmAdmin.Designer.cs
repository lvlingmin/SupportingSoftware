namespace EBarv0._2.Administrator
{
    partial class frmAdmin
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
            this.btnItemImport = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUserList = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnItemImport
            // 
            this.btnItemImport.Location = new System.Drawing.Point(30, 35);
            this.btnItemImport.Name = "btnItemImport";
            this.btnItemImport.Size = new System.Drawing.Size(115, 50);
            this.btnItemImport.TabIndex = 0;
            this.btnItemImport.Text = "项目导入";
            this.btnItemImport.UseVisualStyleBackColor = true;
            this.btnItemImport.Click += new System.EventHandler(this.btnItemImport_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(167, 35);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(115, 50);
            this.btnInfo.TabIndex = 1;
            this.btnInfo.Text = "信息生成";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(660, 380);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUserList
            // 
            this.btnUserList.Location = new System.Drawing.Point(304, 35);
            this.btnUserList.Name = "btnUserList";
            this.btnUserList.Size = new System.Drawing.Size(115, 50);
            this.btnUserList.TabIndex = 2;
            this.btnUserList.Text = "用户";
            this.btnUserList.UseVisualStyleBackColor = true;
            this.btnUserList.Click += new System.EventHandler(this.btnUserList_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnItemImport);
            this.groupBox1.Controls.Add(this.btnUserList);
            this.groupBox1.Controls.Add(this.btnInfo);
            this.groupBox1.Location = new System.Drawing.Point(54, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 122);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExit);
            this.Name = "frmAdmin";
            this.Text = "管理界面";
            this.Load += new System.EventHandler(this.frmAdmin_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnItemImport;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnUserList;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
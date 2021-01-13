namespace DebugTool
{
    partial class frmLinearityTest
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvValue = new System.Windows.Forms.DataGridView();
            this.data0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtRepeatNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb3 = new System.Windows.Forms.RadioButton();
            this.rb4 = new System.Windows.Forms.RadioButton();
            this.rb5 = new System.Windows.Forms.RadioButton();
            this.rb6 = new System.Windows.Forms.RadioButton();
            this.rb7 = new System.Windows.Forms.RadioButton();
            this.rb8 = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCoefficient = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValue)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCoefficient);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.dgvValue);
            this.groupBox1.Controls.Add(this.txtRepeatNum);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnRead);
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(48, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(702, 362);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(530, 59);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(622, 59);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvValue
            // 
            this.dgvValue.AllowUserToAddRows = false;
            this.dgvValue.AllowUserToDeleteRows = false;
            this.dgvValue.AllowUserToResizeRows = false;
            this.dgvValue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.data0,
            this.data1,
            this.data2,
            this.data3,
            this.data4,
            this.data5,
            this.data6,
            this.data7,
            this.data8});
            this.dgvValue.Location = new System.Drawing.Point(6, 94);
            this.dgvValue.Name = "dgvValue";
            this.dgvValue.ReadOnly = true;
            this.dgvValue.RowHeadersVisible = false;
            this.dgvValue.RowTemplate.Height = 23;
            this.dgvValue.Size = new System.Drawing.Size(690, 262);
            this.dgvValue.TabIndex = 7;
            this.dgvValue.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvValue_CellPainting);
            // 
            // data0
            // 
            this.data0.DataPropertyName = "data0";
            this.data0.HeaderText = "序号";
            this.data0.Name = "data0";
            this.data0.ReadOnly = true;
            // 
            // data1
            // 
            this.data1.DataPropertyName = "data1";
            this.data1.HeaderText = "暗室读数";
            this.data1.Name = "data1";
            this.data1.ReadOnly = true;
            // 
            // data2
            // 
            this.data2.DataPropertyName = "data2";
            this.data2.HeaderText = "稳定光源";
            this.data2.Name = "data2";
            this.data2.ReadOnly = true;
            // 
            // data3
            // 
            this.data3.DataPropertyName = "data3";
            this.data3.HeaderText = "衰减50%";
            this.data3.Name = "data3";
            this.data3.ReadOnly = true;
            // 
            // data4
            // 
            this.data4.DataPropertyName = "data4";
            this.data4.HeaderText = "衰减25%";
            this.data4.Name = "data4";
            this.data4.ReadOnly = true;
            // 
            // data5
            // 
            this.data5.DataPropertyName = "data5";
            this.data5.HeaderText = "衰减10%";
            this.data5.Name = "data5";
            this.data5.ReadOnly = true;
            // 
            // data6
            // 
            this.data6.DataPropertyName = "data6";
            this.data6.HeaderText = "衰减5%";
            this.data6.Name = "data6";
            this.data6.ReadOnly = true;
            // 
            // data7
            // 
            this.data7.DataPropertyName = "data7";
            this.data7.HeaderText = "衰减2.5%";
            this.data7.Name = "data7";
            this.data7.ReadOnly = true;
            // 
            // data8
            // 
            this.data8.DataPropertyName = "data8";
            this.data8.HeaderText = "衰减1%";
            this.data8.Name = "data8";
            this.data8.ReadOnly = true;
            // 
            // txtRepeatNum
            // 
            this.txtRepeatNum.Location = new System.Drawing.Point(76, 61);
            this.txtRepeatNum.Name = "txtRepeatNum";
            this.txtRepeatNum.Size = new System.Drawing.Size(82, 21);
            this.txtRepeatNum.TabIndex = 6;
            this.txtRepeatNum.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "读数次数：";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(263, 59);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 4;
            this.btnRead.Text = "开始读数";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.rb1);
            this.flowLayoutPanel1.Controls.Add(this.rb2);
            this.flowLayoutPanel1.Controls.Add(this.rb3);
            this.flowLayoutPanel1.Controls.Add(this.rb4);
            this.flowLayoutPanel1.Controls.Add(this.rb5);
            this.flowLayoutPanel1.Controls.Add(this.rb6);
            this.flowLayoutPanel1.Controls.Add(this.rb7);
            this.flowLayoutPanel1.Controls.Add(this.rb8);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(20, 20);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(553, 26);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Checked = true;
            this.rb1.Location = new System.Drawing.Point(3, 3);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(47, 16);
            this.rb1.TabIndex = 7;
            this.rb1.TabStop = true;
            this.rb1.Text = "暗室";
            this.rb1.UseVisualStyleBackColor = true;
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(56, 3);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(71, 16);
            this.rb2.TabIndex = 0;
            this.rb2.Text = "稳定光源";
            this.rb2.UseVisualStyleBackColor = true;
            // 
            // rb3
            // 
            this.rb3.AutoSize = true;
            this.rb3.Location = new System.Drawing.Point(133, 3);
            this.rb3.Name = "rb3";
            this.rb3.Size = new System.Drawing.Size(65, 16);
            this.rb3.TabIndex = 1;
            this.rb3.Text = "衰减50%";
            this.rb3.UseVisualStyleBackColor = true;
            // 
            // rb4
            // 
            this.rb4.AutoSize = true;
            this.rb4.Location = new System.Drawing.Point(204, 3);
            this.rb4.Name = "rb4";
            this.rb4.Size = new System.Drawing.Size(65, 16);
            this.rb4.TabIndex = 2;
            this.rb4.Text = "衰减25%";
            this.rb4.UseVisualStyleBackColor = true;
            // 
            // rb5
            // 
            this.rb5.AutoSize = true;
            this.rb5.Location = new System.Drawing.Point(275, 3);
            this.rb5.Name = "rb5";
            this.rb5.Size = new System.Drawing.Size(65, 16);
            this.rb5.TabIndex = 3;
            this.rb5.Text = "衰减10%";
            this.rb5.UseVisualStyleBackColor = true;
            // 
            // rb6
            // 
            this.rb6.AutoSize = true;
            this.rb6.Location = new System.Drawing.Point(346, 3);
            this.rb6.Name = "rb6";
            this.rb6.Size = new System.Drawing.Size(59, 16);
            this.rb6.TabIndex = 4;
            this.rb6.Text = "衰减5%";
            this.rb6.UseVisualStyleBackColor = true;
            // 
            // rb7
            // 
            this.rb7.AutoSize = true;
            this.rb7.Location = new System.Drawing.Point(411, 3);
            this.rb7.Name = "rb7";
            this.rb7.Size = new System.Drawing.Size(71, 16);
            this.rb7.TabIndex = 5;
            this.rb7.Text = "衰减2.5%";
            this.rb7.UseVisualStyleBackColor = true;
            // 
            // rb8
            // 
            this.rb8.AutoSize = true;
            this.rb8.Location = new System.Drawing.Point(488, 3);
            this.rb8.Name = "rb8";
            this.rb8.Size = new System.Drawing.Size(59, 16);
            this.rb8.TabIndex = 6;
            this.rb8.Text = "衰减1%";
            this.rb8.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(713, 415);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCoefficient
            // 
            this.btnCoefficient.Location = new System.Drawing.Point(173, 59);
            this.btnCoefficient.Name = "btnCoefficient";
            this.btnCoefficient.Size = new System.Drawing.Size(75, 23);
            this.btnCoefficient.TabIndex = 10;
            this.btnCoefficient.Text = "计算系数";
            this.btnCoefficient.UseVisualStyleBackColor = true;
            this.btnCoefficient.Click += new System.EventHandler(this.btnCoefficient_Click);
            // 
            // frmLinearityTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLinearityTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "光子计数器线性测试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLinearityTest_FormClosed);
            this.Load += new System.EventHandler(this.frmLinearityTest_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValue)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.TextBox txtRepeatNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.RadioButton rb3;
        private System.Windows.Forms.RadioButton rb4;
        private System.Windows.Forms.RadioButton rb5;
        private System.Windows.Forms.RadioButton rb6;
        private System.Windows.Forms.RadioButton rb7;
        private System.Windows.Forms.RadioButton rb8;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvValue;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn data0;
        private System.Windows.Forms.DataGridViewTextBoxColumn data1;
        private System.Windows.Forms.DataGridViewTextBoxColumn data2;
        private System.Windows.Forms.DataGridViewTextBoxColumn data3;
        private System.Windows.Forms.DataGridViewTextBoxColumn data4;
        private System.Windows.Forms.DataGridViewTextBoxColumn data5;
        private System.Windows.Forms.DataGridViewTextBoxColumn data6;
        private System.Windows.Forms.DataGridViewTextBoxColumn data7;
        private System.Windows.Forms.DataGridViewTextBoxColumn data8;
        private System.Windows.Forms.Button btnCoefficient;
    }
}
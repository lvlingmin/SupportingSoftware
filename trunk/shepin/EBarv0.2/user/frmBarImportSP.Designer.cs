namespace EBarv0._2.user
{
    partial class frmBarImportSP
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTestNum = new System.Windows.Forms.TextBox();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.txtProductionDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvQc = new System.Windows.Forms.DataGridView();
            this.qcNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qcItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qcTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qcX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qcSd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnback = new System.Windows.Forms.Button();
            this.cmbReagentName = new System.Windows.Forms.ComboBox();
            this.btnQc1 = new EBarv0._2.CustomControl.defineButton(this.components);
            this.btnQc2 = new EBarv0._2.CustomControl.defineButton(this.components);
            this.btnStartBar = new EBarv0._2.CustomControl.defineButton(this.components);
            this.btnCloseBar = new EBarv0._2.CustomControl.defineButton(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.IapProBar = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQc)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目名称：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTestNum);
            this.groupBox1.Controls.Add(this.txtBarCode);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNum);
            this.groupBox1.Controls.Add(this.txtProductionDate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtItemName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(18, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 220);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "条码信息";
            // 
            // txtTestNum
            // 
            this.txtTestNum.Enabled = false;
            this.txtTestNum.Location = new System.Drawing.Point(129, 127);
            this.txtTestNum.Name = "txtTestNum";
            this.txtTestNum.Size = new System.Drawing.Size(150, 21);
            this.txtTestNum.TabIndex = 9;
            // 
            // txtBarCode
            // 
            this.txtBarCode.Enabled = false;
            this.txtBarCode.Location = new System.Drawing.Point(129, 37);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(150, 21);
            this.txtBarCode.TabIndex = 7;
            this.txtBarCode.TextChanged += new System.EventHandler(this.txtBarCode_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "测数规格：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "条码编号：";
            // 
            // txtNum
            // 
            this.txtNum.Enabled = false;
            this.txtNum.Location = new System.Drawing.Point(129, 157);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(150, 21);
            this.txtNum.TabIndex = 5;
            // 
            // txtProductionDate
            // 
            this.txtProductionDate.Enabled = false;
            this.txtProductionDate.Location = new System.Drawing.Point(129, 97);
            this.txtProductionDate.Name = "txtProductionDate";
            this.txtProductionDate.Size = new System.Drawing.Size(150, 21);
            this.txtProductionDate.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "流水编号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "生产时间：";
            // 
            // txtItemName
            // 
            this.txtItemName.Enabled = false;
            this.txtItemName.Location = new System.Drawing.Point(129, 67);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(150, 21);
            this.txtItemName.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvQc);
            this.groupBox2.Location = new System.Drawing.Point(18, 260);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(445, 102);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "质控";
            // 
            // dgvQc
            // 
            this.dgvQc.AllowUserToAddRows = false;
            this.dgvQc.AllowUserToResizeColumns = false;
            this.dgvQc.AllowUserToResizeRows = false;
            this.dgvQc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.qcNum,
            this.qcItem,
            this.qcTime,
            this.qcX,
            this.qcSd});
            this.dgvQc.Location = new System.Drawing.Point(18, 20);
            this.dgvQc.Name = "dgvQc";
            this.dgvQc.RowHeadersVisible = false;
            this.dgvQc.RowTemplate.Height = 23;
            this.dgvQc.Size = new System.Drawing.Size(408, 68);
            this.dgvQc.TabIndex = 0;
            // 
            // qcNum
            // 
            this.qcNum.DataPropertyName = "qcNum";
            this.qcNum.HeaderText = "质控品";
            this.qcNum.Name = "qcNum";
            this.qcNum.ReadOnly = true;
            this.qcNum.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // qcItem
            // 
            this.qcItem.DataPropertyName = "qcItem";
            this.qcItem.HeaderText = "项目名称";
            this.qcItem.Name = "qcItem";
            this.qcItem.ReadOnly = true;
            this.qcItem.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // qcTime
            // 
            this.qcTime.DataPropertyName = "qcTime";
            this.qcTime.HeaderText = "生产时间";
            this.qcTime.Name = "qcTime";
            this.qcTime.ReadOnly = true;
            this.qcTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // qcX
            // 
            this.qcX.DataPropertyName = "qcX";
            this.qcX.HeaderText = "靶值";
            this.qcX.Name = "qcX";
            this.qcX.ReadOnly = true;
            this.qcX.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // qcSd
            // 
            this.qcSd.DataPropertyName = "qcSd";
            this.qcSd.HeaderText = "标准差";
            this.qcSd.Name = "qcSd";
            this.qcSd.ReadOnly = true;
            this.qcSd.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(572, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "项目名称：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(20, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(488, 368);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "扫描信息";
            // 
            // btnback
            // 
            this.btnback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnback.Location = new System.Drawing.Point(696, 384);
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(75, 23);
            this.btnback.TabIndex = 42;
            this.btnback.Text = "返回";
            this.btnback.UseVisualStyleBackColor = true;
            this.btnback.Click += new System.EventHandler(this.btnback_Click);
            // 
            // cmbReagentName
            // 
            this.cmbReagentName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReagentName.FormattingEnabled = true;
            this.cmbReagentName.Location = new System.Drawing.Point(631, 115);
            this.cmbReagentName.Name = "cmbReagentName";
            this.cmbReagentName.Size = new System.Drawing.Size(121, 20);
            this.cmbReagentName.TabIndex = 43;
            // 
            // btnQc1
            // 
            this.btnQc1.Location = new System.Drawing.Point(574, 143);
            this.btnQc1.Name = "btnQc1";
            this.btnQc1.Size = new System.Drawing.Size(75, 23);
            this.btnQc1.TabIndex = 46;
            this.btnQc1.Text = "登记质控一";
            this.btnQc1.UseVisualStyleBackColor = true;
            this.btnQc1.Click += new System.EventHandler(this.btnQc1_Click);
            // 
            // btnQc2
            // 
            this.btnQc2.Location = new System.Drawing.Point(665, 143);
            this.btnQc2.Name = "btnQc2";
            this.btnQc2.Size = new System.Drawing.Size(75, 23);
            this.btnQc2.TabIndex = 47;
            this.btnQc2.Text = "登记质控二";
            this.btnQc2.UseVisualStyleBackColor = true;
            this.btnQc2.Click += new System.EventHandler(this.btnQc2_Click);
            // 
            // btnStartBar
            // 
            this.btnStartBar.Location = new System.Drawing.Point(574, 191);
            this.btnStartBar.Name = "btnStartBar";
            this.btnStartBar.Size = new System.Drawing.Size(75, 23);
            this.btnStartBar.TabIndex = 48;
            this.btnStartBar.Text = "开启扫码";
            this.btnStartBar.UseVisualStyleBackColor = true;
            this.btnStartBar.Click += new System.EventHandler(this.btnStartBar_Click);
            // 
            // btnCloseBar
            // 
            this.btnCloseBar.Enabled = false;
            this.btnCloseBar.Location = new System.Drawing.Point(665, 190);
            this.btnCloseBar.Name = "btnCloseBar";
            this.btnCloseBar.Size = new System.Drawing.Size(75, 23);
            this.btnCloseBar.TabIndex = 49;
            this.btnCloseBar.Text = "关闭扫码";
            this.btnCloseBar.UseVisualStyleBackColor = true;
            this.btnCloseBar.Click += new System.EventHandler(this.btnCloseBar_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IapProBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 430);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 50;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // IapProBar
            // 
            this.IapProBar.Name = "IapProBar";
            this.IapProBar.Size = new System.Drawing.Size(100, 16);
            this.IapProBar.ToolTipText = "射频录入进度";
            this.IapProBar.Visible = false;
            // 
            // frmBarImportSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 452);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCloseBar);
            this.Controls.Add(this.btnStartBar);
            this.Controls.Add(this.btnQc2);
            this.Controls.Add(this.btnQc1);
            this.Controls.Add(this.cmbReagentName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnback);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBarImportSP";
            this.Text = "射频-条码录入";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBarImportSP_FormClosed);
            this.Load += new System.EventHandler(this.frmBarImportSP_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQc)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvQc;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnback;
        private System.Windows.Forms.ComboBox cmbReagentName;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.TextBox txtProductionDate;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTestNum;
        private System.Windows.Forms.Label label6;
        private CustomControl.defineButton btnQc1;
        private CustomControl.defineButton btnQc2;
        private CustomControl.defineButton btnStartBar;
        private CustomControl.defineButton btnCloseBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn qcNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn qcItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn qcTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn qcX;
        private System.Windows.Forms.DataGridViewTextBoxColumn qcSd;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar IapProBar;
    }
}
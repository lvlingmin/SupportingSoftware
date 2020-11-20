namespace EBarv0._2
{
    partial class FrmLoadItem
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.back = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnLoadItem = new System.Windows.Forms.Button();
            this.dgvItemList = new System.Windows.Forms.DataGridView();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvItemStd = new System.Windows.Forms.DataGridView();
            this.StnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvItemStep = new System.Windows.Forms.DataGridView();
            this.NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StepName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StepPara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParaUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemStd)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemStep)).BeginInit();
            this.SuspendLayout();
            // 
            // back
            // 
            this.back.Location = new System.Drawing.Point(885, 530);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(75, 23);
            this.back.TabIndex = 9;
            this.back.Text = "返回";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.Back_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(987, 512);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnLoadItem);
            this.groupBox4.Controls.Add(this.dgvItemList);
            this.groupBox4.Location = new System.Drawing.Point(22, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(256, 480);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "导入项目信息";
            // 
            // btnLoadItem
            // 
            this.btnLoadItem.Location = new System.Drawing.Point(69, 41);
            this.btnLoadItem.Name = "btnLoadItem";
            this.btnLoadItem.Size = new System.Drawing.Size(111, 37);
            this.btnLoadItem.TabIndex = 0;
            this.btnLoadItem.Text = "导入项目信息";
            this.btnLoadItem.UseVisualStyleBackColor = true;
            this.btnLoadItem.Click += new System.EventHandler(this.btnLoadItem_Click);
            // 
            // dgvItemList
            // 
            this.dgvItemList.AllowUserToAddRows = false;
            this.dgvItemList.AllowUserToDeleteRows = false;
            this.dgvItemList.AllowUserToResizeRows = false;
            this.dgvItemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colShortName});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvItemList.Location = new System.Drawing.Point(17, 96);
            this.dgvItemList.MultiSelect = false;
            this.dgvItemList.Name = "dgvItemList";
            this.dgvItemList.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvItemList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvItemList.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvItemList.RowTemplate.Height = 23;
            this.dgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemList.Size = new System.Drawing.Size(224, 378);
            this.dgvItemList.TabIndex = 24;
            // 
            // colNo
            // 
            this.colNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNo.DataPropertyName = "NO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colNo.FillWeight = 50F;
            this.colNo.HeaderText = "NO.";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colShortName
            // 
            this.colShortName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colShortName.DataPropertyName = "ItemShortName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colShortName.DefaultCellStyle = dataGridViewCellStyle3;
            this.colShortName.HeaderText = "项目简称";
            this.colShortName.Name = "colShortName";
            this.colShortName.ReadOnly = true;
            this.colShortName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvItemStd);
            this.groupBox3.Location = new System.Drawing.Point(646, 267);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(324, 233);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "定标信息";
            // 
            // dgvItemStd
            // 
            this.dgvItemStd.AllowUserToAddRows = false;
            this.dgvItemStd.AllowUserToDeleteRows = false;
            this.dgvItemStd.AllowUserToResizeRows = false;
            this.dgvItemStd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemStd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvItemStd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemStd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StnNo,
            this.StdName,
            this.conc});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemStd.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvItemStd.Location = new System.Drawing.Point(14, 20);
            this.dgvItemStd.MultiSelect = false;
            this.dgvItemStd.Name = "dgvItemStd";
            this.dgvItemStd.ReadOnly = true;
            this.dgvItemStd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvItemStd.RowTemplate.Height = 23;
            this.dgvItemStd.Size = new System.Drawing.Size(294, 207);
            this.dgvItemStd.TabIndex = 1;
            // 
            // StnNo
            // 
            this.StnNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StnNo.DataPropertyName = "NO";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StnNo.DefaultCellStyle = dataGridViewCellStyle8;
            this.StnNo.FillWeight = 50F;
            this.StnNo.HeaderText = "No.";
            this.StnNo.Name = "StnNo";
            this.StnNo.ReadOnly = true;
            this.StnNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StdName
            // 
            this.StdName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StdName.DataPropertyName = "StdName";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StdName.DefaultCellStyle = dataGridViewCellStyle9;
            this.StdName.FillWeight = 120F;
            this.StdName.HeaderText = "标准品名";
            this.StdName.Name = "StdName";
            this.StdName.ReadOnly = true;
            this.StdName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // conc
            // 
            this.conc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.conc.DataPropertyName = "StdConc";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.conc.DefaultCellStyle = dataGridViewCellStyle10;
            this.conc.FillWeight = 80F;
            this.conc.HeaderText = "浓度";
            this.conc.Name = "conc";
            this.conc.ReadOnly = true;
            this.conc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvItemStep);
            this.groupBox2.Location = new System.Drawing.Point(301, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 357);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "项目步骤";
            // 
            // dgvItemStep
            // 
            this.dgvItemStep.AllowUserToAddRows = false;
            this.dgvItemStep.AllowUserToDeleteRows = false;
            this.dgvItemStep.AllowUserToResizeRows = false;
            this.dgvItemStep.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItemStep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemStep.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NO,
            this.StepName,
            this.StepPara,
            this.ParaUnit});
            this.dgvItemStep.Location = new System.Drawing.Point(15, 20);
            this.dgvItemStep.MultiSelect = false;
            this.dgvItemStep.Name = "dgvItemStep";
            this.dgvItemStep.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvItemStep.RowTemplate.Height = 23;
            this.dgvItemStep.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemStep.Size = new System.Drawing.Size(293, 331);
            this.dgvItemStep.TabIndex = 0;
            // 
            // NO
            // 
            this.NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NO.DataPropertyName = "NO";
            this.NO.FillWeight = 50F;
            this.NO.HeaderText = "NO.";
            this.NO.Name = "NO";
            this.NO.ReadOnly = true;
            this.NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StepName
            // 
            this.StepName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StepName.DataPropertyName = "StepName";
            this.StepName.FillWeight = 120F;
            this.StepName.HeaderText = "步骤名";
            this.StepName.Name = "StepName";
            this.StepName.ReadOnly = true;
            this.StepName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StepPara
            // 
            this.StepPara.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StepPara.DataPropertyName = "Para";
            this.StepPara.FillWeight = 80F;
            this.StepPara.HeaderText = "参数";
            this.StepPara.Name = "StepPara";
            this.StepPara.ReadOnly = true;
            this.StepPara.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ParaUnit
            // 
            this.ParaUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParaUnit.DataPropertyName = "Unit";
            this.ParaUnit.FillWeight = 80F;
            this.ParaUnit.HeaderText = "单位";
            this.ParaUnit.Name = "ParaUnit";
            this.ParaUnit.ReadOnly = true;
            this.ParaUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmLoadItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 565);
            this.ControlBox = false;
            this.Controls.Add(this.back);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmLoadItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "项目信息导入";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLoadItem_FormClosed);
            this.Load += new System.EventHandler(this.FrmLoadItem_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemStd)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemStep)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnLoadItem;
        private System.Windows.Forms.DataGridView dgvItemList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShortName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvItemStd;
        private System.Windows.Forms.DataGridViewTextBoxColumn StnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn conc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvItemStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn StepName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StepPara;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParaUnit;
    }
}
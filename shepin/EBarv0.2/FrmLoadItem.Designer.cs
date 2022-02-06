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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.btnUnLoadItem = new System.Windows.Forms.Button();
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
            this.back.Location = new System.Drawing.Point(668, 418);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(731, 408);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnUnLoadItem);
            this.groupBox4.Controls.Add(this.btnLoadItem);
            this.groupBox4.Controls.Add(this.dgvItemList);
            this.groupBox4.Location = new System.Drawing.Point(13, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(232, 373);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "导入项目信息";
            // 
            // btnLoadItem
            // 
            this.btnLoadItem.Location = new System.Drawing.Point(16, 20);
            this.btnLoadItem.Name = "btnLoadItem";
            this.btnLoadItem.Size = new System.Drawing.Size(97, 33);
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
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colShortName});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvItemList.Location = new System.Drawing.Point(16, 61);
            this.dgvItemList.MultiSelect = false;
            this.dgvItemList.Name = "dgvItemList";
            this.dgvItemList.ReadOnly = true;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvItemList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvItemList.RowsDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvItemList.RowTemplate.Height = 23;
            this.dgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemList.Size = new System.Drawing.Size(200, 300);
            this.dgvItemList.TabIndex = 24;
            // 
            // colNo
            // 
            this.colNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNo.DataPropertyName = "NO";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colNo.DefaultCellStyle = dataGridViewCellStyle13;
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
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colShortName.DefaultCellStyle = dataGridViewCellStyle14;
            this.colShortName.HeaderText = "项目简称";
            this.colShortName.Name = "colShortName";
            this.colShortName.ReadOnly = true;
            this.colShortName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvItemStd);
            this.groupBox3.Location = new System.Drawing.Point(475, 160);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(217, 233);
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
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemStd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvItemStd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemStd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StnNo,
            this.StdName,
            this.conc});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemStd.DefaultCellStyle = dataGridViewCellStyle22;
            this.dgvItemStd.Location = new System.Drawing.Point(9, 21);
            this.dgvItemStd.MultiSelect = false;
            this.dgvItemStd.Name = "dgvItemStd";
            this.dgvItemStd.ReadOnly = true;
            this.dgvItemStd.RowHeadersVisible = false;
            this.dgvItemStd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvItemStd.RowTemplate.Height = 23;
            this.dgvItemStd.Size = new System.Drawing.Size(200, 200);
            this.dgvItemStd.TabIndex = 1;
            // 
            // StnNo
            // 
            this.StnNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StnNo.DataPropertyName = "NO";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StnNo.DefaultCellStyle = dataGridViewCellStyle19;
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
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StdName.DefaultCellStyle = dataGridViewCellStyle20;
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
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.conc.DefaultCellStyle = dataGridViewCellStyle21;
            this.conc.FillWeight = 80F;
            this.conc.HeaderText = "浓度";
            this.conc.Name = "conc";
            this.conc.ReadOnly = true;
            this.conc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvItemStep);
            this.groupBox2.Location = new System.Drawing.Point(251, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(218, 326);
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
            this.dgvItemStep.Location = new System.Drawing.Point(9, 14);
            this.dgvItemStep.MultiSelect = false;
            this.dgvItemStep.Name = "dgvItemStep";
            this.dgvItemStep.RowHeadersVisible = false;
            this.dgvItemStep.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvItemStep.RowTemplate.Height = 23;
            this.dgvItemStep.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemStep.Size = new System.Drawing.Size(200, 300);
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
            // btnUnLoadItem
            // 
            this.btnUnLoadItem.Location = new System.Drawing.Point(119, 20);
            this.btnUnLoadItem.Name = "btnUnLoadItem";
            this.btnUnLoadItem.Size = new System.Drawing.Size(97, 33);
            this.btnUnLoadItem.TabIndex = 28;
            this.btnUnLoadItem.Text = "删除项目信息";
            this.btnUnLoadItem.UseVisualStyleBackColor = true;
            this.btnUnLoadItem.Click += new System.EventHandler(this.btnUnLoadItem_Click);
            // 
            // FrmLoadItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 449);
            this.Controls.Add(this.back);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmLoadItem";
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
        private System.Windows.Forms.Button btnUnLoadItem;
    }
}
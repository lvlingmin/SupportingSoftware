namespace EBarv0._2.user
{
    partial class frmSpImport
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
            this.btnImportFile = new System.Windows.Forms.Button();
            this.btnInNext = new System.Windows.Forms.Button();
            this.dgvSp = new System.Windows.Forms.DataGridView();
            this.btnback = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSp)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImportFile
            // 
            this.btnImportFile.Location = new System.Drawing.Point(12, 60);
            this.btnImportFile.Name = "btnImportFile";
            this.btnImportFile.Size = new System.Drawing.Size(75, 23);
            this.btnImportFile.TabIndex = 0;
            this.btnImportFile.Text = "导入文件";
            this.btnImportFile.UseVisualStyleBackColor = true;
            this.btnImportFile.Click += new System.EventHandler(this.btnImportFile_Click);
            // 
            // btnInNext
            // 
            this.btnInNext.Location = new System.Drawing.Point(361, 365);
            this.btnInNext.Name = "btnInNext";
            this.btnInNext.Size = new System.Drawing.Size(75, 23);
            this.btnInNext.TabIndex = 1;
            this.btnInNext.Text = "开始录入";
            this.btnInNext.UseVisualStyleBackColor = true;
            this.btnInNext.Click += new System.EventHandler(this.btnInNext_Click);
            // 
            // dgvSp
            // 
            this.dgvSp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.data1,
            this.data2,
            this.data3,
            this.data4,
            this.data5,
            this.data6,
            this.data7,
            this.data8,
            this.data9,
            this.data10,
            this.data11});
            this.dgvSp.Location = new System.Drawing.Point(12, 102);
            this.dgvSp.Name = "dgvSp";
            this.dgvSp.ReadOnly = true;
            this.dgvSp.RowHeadersVisible = false;
            this.dgvSp.RowTemplate.Height = 23;
            this.dgvSp.Size = new System.Drawing.Size(776, 150);
            this.dgvSp.TabIndex = 2;
            // 
            // btnback
            // 
            this.btnback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnback.Location = new System.Drawing.Point(713, 390);
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(75, 23);
            this.btnback.TabIndex = 41;
            this.btnback.Text = "返回";
            this.btnback.UseVisualStyleBackColor = true;
            this.btnback.Click += new System.EventHandler(this.btnback_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 22F);
            this.label6.Location = new System.Drawing.Point(339, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 30);
            this.label6.TabIndex = 44;
            this.label6.Text = "剩余：0";
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // data1
            // 
            this.data1.DataPropertyName = "data1";
            this.data1.HeaderText = "data1";
            this.data1.Name = "data1";
            this.data1.ReadOnly = true;
            // 
            // data2
            // 
            this.data2.DataPropertyName = "data2";
            this.data2.HeaderText = "data2";
            this.data2.Name = "data2";
            this.data2.ReadOnly = true;
            // 
            // data3
            // 
            this.data3.DataPropertyName = "data3";
            this.data3.HeaderText = "data3";
            this.data3.Name = "data3";
            this.data3.ReadOnly = true;
            // 
            // data4
            // 
            this.data4.DataPropertyName = "data4";
            this.data4.HeaderText = "data4";
            this.data4.Name = "data4";
            this.data4.ReadOnly = true;
            // 
            // data5
            // 
            this.data5.DataPropertyName = "data5";
            this.data5.HeaderText = "data5";
            this.data5.Name = "data5";
            this.data5.ReadOnly = true;
            // 
            // data6
            // 
            this.data6.DataPropertyName = "data6";
            this.data6.HeaderText = "data6";
            this.data6.Name = "data6";
            this.data6.ReadOnly = true;
            // 
            // data7
            // 
            this.data7.DataPropertyName = "data7";
            this.data7.HeaderText = "data7";
            this.data7.Name = "data7";
            this.data7.ReadOnly = true;
            // 
            // data8
            // 
            this.data8.DataPropertyName = "data8";
            this.data8.HeaderText = "data8";
            this.data8.Name = "data8";
            this.data8.ReadOnly = true;
            // 
            // data9
            // 
            this.data9.DataPropertyName = "data9";
            this.data9.HeaderText = "data9";
            this.data9.Name = "data9";
            this.data9.ReadOnly = true;
            // 
            // data10
            // 
            this.data10.DataPropertyName = "data10";
            this.data10.HeaderText = "data10";
            this.data10.Name = "data10";
            this.data10.ReadOnly = true;
            // 
            // data11
            // 
            this.data11.DataPropertyName = "data11";
            this.data11.HeaderText = "data11";
            this.data11.Name = "data11";
            this.data11.ReadOnly = true;
            // 
            // frmSpImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnback);
            this.Controls.Add(this.dgvSp);
            this.Controls.Add(this.btnInNext);
            this.Controls.Add(this.btnImportFile);
            this.Name = "frmSpImport";
            this.Text = "射频录入";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSpImport_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImportFile;
        private System.Windows.Forms.Button btnInNext;
        private System.Windows.Forms.DataGridView dgvSp;
        private System.Windows.Forms.Button btnback;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn data1;
        private System.Windows.Forms.DataGridViewTextBoxColumn data2;
        private System.Windows.Forms.DataGridViewTextBoxColumn data3;
        private System.Windows.Forms.DataGridViewTextBoxColumn data4;
        private System.Windows.Forms.DataGridViewTextBoxColumn data5;
        private System.Windows.Forms.DataGridViewTextBoxColumn data6;
        private System.Windows.Forms.DataGridViewTextBoxColumn data7;
        private System.Windows.Forms.DataGridViewTextBoxColumn data8;
        private System.Windows.Forms.DataGridViewTextBoxColumn data9;
        private System.Windows.Forms.DataGridViewTextBoxColumn data10;
        private System.Windows.Forms.DataGridViewTextBoxColumn data11;
    }
}
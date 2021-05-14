namespace EBarv0._2
{
    partial class frmBarFromExcel
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
            this.btnImportAndExcel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCreate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImportAndExcel
            // 
            this.btnImportAndExcel.Location = new System.Drawing.Point(54, 28);
            this.btnImportAndExcel.Name = "btnImportAndExcel";
            this.btnImportAndExcel.Size = new System.Drawing.Size(95, 30);
            this.btnImportAndExcel.TabIndex = 0;
            this.btnImportAndExcel.Text = "Excel导入";
            this.btnImportAndExcel.UseVisualStyleBackColor = true;
            this.btnImportAndExcel.Click += new System.EventHandler(this.btnImportAndExcel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.name,
            this.batch,
            this.productDate,
            this.vol,
            this.numStart,
            this.createNum});
            this.dataGridView1.Location = new System.Drawing.Point(54, 75);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(634, 275);
            this.dataGridView1.TabIndex = 1;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "序号";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 40;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // batch
            // 
            this.batch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.batch.DataPropertyName = "batch";
            this.batch.HeaderText = "批号";
            this.batch.Name = "batch";
            this.batch.ReadOnly = true;
            // 
            // productDate
            // 
            this.productDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.productDate.DataPropertyName = "productDate";
            this.productDate.HeaderText = "生产时间";
            this.productDate.Name = "productDate";
            this.productDate.ReadOnly = true;
            // 
            // vol
            // 
            this.vol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.vol.DataPropertyName = "vol";
            this.vol.HeaderText = "规格";
            this.vol.Name = "vol";
            this.vol.ReadOnly = true;
            // 
            // numStart
            // 
            this.numStart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.numStart.DataPropertyName = "numStart";
            this.numStart.HeaderText = "开始编号";
            this.numStart.Name = "numStart";
            this.numStart.ReadOnly = true;
            // 
            // createNum
            // 
            this.createNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.createNum.DataPropertyName = "createNum";
            this.createNum.HeaderText = "生成数量";
            this.createNum.Name = "createNum";
            this.createNum.ReadOnly = true;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(593, 28);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(95, 30);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "生成";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // frmBarFromExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 421);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnImportAndExcel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBarFromExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "条码生成&Excel";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBarFromExcel_FormClosed);
            this.Load += new System.EventHandler(this.frmBarFromExcel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImportAndExcel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn productDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn vol;
        private System.Windows.Forms.DataGridViewTextBoxColumn numStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn createNum;
        private System.Windows.Forms.Button btnCreate;
    }
}
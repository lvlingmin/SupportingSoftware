namespace EBarv0._2
{
    partial class FrmProject2
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnGener = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbProject = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvProdure = new System.Windows.Forms.DataGridView();
            this.back = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdure)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.btnGener);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbProject);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dgvProdure);
            this.groupBox2.Location = new System.Drawing.Point(21, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(827, 487);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "生成实验流程条码";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(592, 21);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 46;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(486, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 23);
            this.button2.TabIndex = 45;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // btnGener
            // 
            this.btnGener.Location = new System.Drawing.Point(380, 21);
            this.btnGener.Name = "btnGener";
            this.btnGener.Size = new System.Drawing.Size(71, 23);
            this.btnGener.TabIndex = 44;
            this.btnGener.Text = "生成";
            this.btnGener.UseVisualStyleBackColor = true;
            this.btnGener.Click += new System.EventHandler(this.BtnGener_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DGV1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(380, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(406, 434);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // DGV1
            // 
            this.DGV1.AllowUserToAddRows = false;
            this.DGV1.AllowUserToDeleteRows = false;
            this.DGV1.AllowUserToResizeColumns = false;
            this.DGV1.AllowUserToResizeRows = false;
            this.DGV1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.DGV1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Location = new System.Drawing.Point(17, 46);
            this.DGV1.Name = "DGV1";
            this.DGV1.ReadOnly = true;
            this.DGV1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.DGV1.RowTemplate.Height = 23;
            this.DGV1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGV1.Size = new System.Drawing.Size(373, 131);
            this.DGV1.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(13, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.TabIndex = 41;
            this.label3.Text = "条码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(13, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 20);
            this.label4.TabIndex = 40;
            this.label4.Text = "编码";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(17, 214);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 203);
            this.panel1.TabIndex = 39;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "实验项目";
            // 
            // cmbProject
            // 
            this.cmbProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProject.FormattingEnabled = true;
            this.cmbProject.Location = new System.Drawing.Point(98, 18);
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Size = new System.Drawing.Size(165, 20);
            this.cmbProject.TabIndex = 40;
            this.cmbProject.SelectedIndexChanged += new System.EventHandler(this.CmbProject_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 39;
            this.label2.Text = "实验步骤 ：";
            // 
            // dgvProdure
            // 
            this.dgvProdure.AllowUserToAddRows = false;
            this.dgvProdure.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProdure.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProdure.Location = new System.Drawing.Point(41, 77);
            this.dgvProdure.MultiSelect = false;
            this.dgvProdure.Name = "dgvProdure";
            this.dgvProdure.ReadOnly = true;
            this.dgvProdure.RowTemplate.Height = 23;
            this.dgvProdure.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProdure.Size = new System.Drawing.Size(222, 402);
            this.dgvProdure.TabIndex = 38;
            // 
            // back
            // 
            this.back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.back.Location = new System.Drawing.Point(773, 505);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(75, 23);
            this.back.TabIndex = 39;
            this.back.Text = "返回";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.Back_Click);
            // 
            // FrmProject2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 531);
            this.ControlBox = false;
            this.Controls.Add(this.back);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmProject2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "项目-流程";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmProject2_FormClosed);
            this.Load += new System.EventHandler(this.FrmProject2_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdure)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbProject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvProdure;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnGener;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button btnReset;
    }
}
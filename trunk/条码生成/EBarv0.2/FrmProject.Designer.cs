namespace EBarv0._2
{
    partial class FrmProject
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.sample = new System.Windows.Forms.Button();
            this.dgvProdure = new System.Windows.Forms.DataGridView();
            this.R1 = new System.Windows.Forms.Button();
            this.R2 = new System.Windows.Forms.Button();
            this.R3 = new System.Windows.Forms.Button();
            this.H = new System.Windows.Forms.Button();
            this.B = new System.Windows.Forms.Button();
            this.R4 = new System.Windows.Forms.Button();
            this.W = new System.Windows.Forms.Button();
            this.T = new System.Windows.Forms.Button();
            this.D = new System.Windows.Forms.Button();
            this.up = new System.Windows.Forms.Button();
            this.down = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.delete = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.btnGener = new System.Windows.Forms.Button();
            this.cmbProject = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.back = new System.Windows.Forms.Button();
            this.GRBAddProdure = new System.Windows.Forms.GroupBox();
            this.GRBLocaOrData = new System.Windows.Forms.GroupBox();
            this.GRB_ischangge = new System.Windows.Forms.GroupBox();
            this.rbtnAddProject = new System.Windows.Forms.RadioButton();
            this.rbtnOnlyPara = new System.Windows.Forms.RadioButton();
            this.rbtnProAndPara = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            this.GRBAddProdure.SuspendLayout();
            this.GRBLocaOrData.SuspendLayout();
            this.GRB_ischangge.SuspendLayout();
            this.SuspendLayout();
            // 
            // sample
            // 
            this.sample.Location = new System.Drawing.Point(19, 54);
            this.sample.Name = "sample";
            this.sample.Size = new System.Drawing.Size(75, 23);
            this.sample.TabIndex = 1;
            this.sample.Text = "样本";
            this.sample.UseVisualStyleBackColor = true;
            this.sample.Click += new System.EventHandler(this.sample_Click);
            // 
            // dgvProdure
            // 
            this.dgvProdure.AllowUserToAddRows = false;
            this.dgvProdure.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvProdure.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProdure.Location = new System.Drawing.Point(243, 239);
            this.dgvProdure.MultiSelect = false;
            this.dgvProdure.Name = "dgvProdure";
            this.dgvProdure.RowTemplate.Height = 23;
            this.dgvProdure.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProdure.Size = new System.Drawing.Size(193, 419);
            this.dgvProdure.TabIndex = 3;
            this.dgvProdure.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // R1
            // 
            this.R1.Location = new System.Drawing.Point(19, 102);
            this.R1.Name = "R1";
            this.R1.Size = new System.Drawing.Size(75, 23);
            this.R1.TabIndex = 4;
            this.R1.Text = "试剂R1";
            this.R1.UseVisualStyleBackColor = true;
            this.R1.Click += new System.EventHandler(this.R1_Click);
            // 
            // R2
            // 
            this.R2.Location = new System.Drawing.Point(117, 102);
            this.R2.Name = "R2";
            this.R2.Size = new System.Drawing.Size(75, 23);
            this.R2.TabIndex = 5;
            this.R2.Text = "试剂R2";
            this.R2.UseVisualStyleBackColor = true;
            this.R2.Click += new System.EventHandler(this.R2_Click);
            // 
            // R3
            // 
            this.R3.Location = new System.Drawing.Point(19, 142);
            this.R3.Name = "R3";
            this.R3.Size = new System.Drawing.Size(75, 23);
            this.R3.TabIndex = 6;
            this.R3.Text = "试剂R3";
            this.R3.UseVisualStyleBackColor = true;
            this.R3.Click += new System.EventHandler(this.R3_Click);
            // 
            // H
            // 
            this.H.Location = new System.Drawing.Point(19, 184);
            this.H.Name = "H";
            this.H.Size = new System.Drawing.Size(75, 23);
            this.H.TabIndex = 8;
            this.H.Text = "温育";
            this.H.UseVisualStyleBackColor = true;
            this.H.Click += new System.EventHandler(this.H_Click);
            // 
            // B
            // 
            this.B.Location = new System.Drawing.Point(19, 236);
            this.B.Name = "B";
            this.B.Size = new System.Drawing.Size(75, 23);
            this.B.TabIndex = 9;
            this.B.Text = "加磁珠";
            this.B.UseVisualStyleBackColor = true;
            this.B.Click += new System.EventHandler(this.B_Click);
            // 
            // R4
            // 
            this.R4.Location = new System.Drawing.Point(117, 142);
            this.R4.Name = "R4";
            this.R4.Size = new System.Drawing.Size(75, 23);
            this.R4.TabIndex = 7;
            this.R4.Text = "试剂R4";
            this.R4.UseVisualStyleBackColor = true;
            this.R4.Click += new System.EventHandler(this.R4_Click);
            // 
            // W
            // 
            this.W.Location = new System.Drawing.Point(19, 283);
            this.W.Name = "W";
            this.W.Size = new System.Drawing.Size(75, 23);
            this.W.TabIndex = 10;
            this.W.Text = "清洗";
            this.W.UseVisualStyleBackColor = true;
            this.W.Click += new System.EventHandler(this.W_Click);
            // 
            // T
            // 
            this.T.Location = new System.Drawing.Point(19, 333);
            this.T.Name = "T";
            this.T.Size = new System.Drawing.Size(75, 23);
            this.T.TabIndex = 11;
            this.T.Text = "加底物";
            this.T.UseVisualStyleBackColor = true;
            this.T.Click += new System.EventHandler(this.T_Click);
            // 
            // D
            // 
            this.D.Location = new System.Drawing.Point(19, 380);
            this.D.Name = "D";
            this.D.Size = new System.Drawing.Size(75, 23);
            this.D.TabIndex = 12;
            this.D.Text = "读数";
            this.D.UseVisualStyleBackColor = true;
            this.D.Click += new System.EventHandler(this.D_Click);
            // 
            // up
            // 
            this.up.Location = new System.Drawing.Point(15, 30);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(75, 23);
            this.up.TabIndex = 13;
            this.up.Text = "上移";
            this.up.UseVisualStyleBackColor = true;
            this.up.Click += new System.EventHandler(this.up_Click);
            // 
            // down
            // 
            this.down.Location = new System.Drawing.Point(15, 78);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(75, 23);
            this.down.TabIndex = 14;
            this.down.Text = "下移";
            this.down.UseVisualStyleBackColor = true;
            this.down.Click += new System.EventHandler(this.down_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "实验步骤 ：";
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(15, 130);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(75, 23);
            this.delete.TabIndex = 16;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(494, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "条码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(494, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "编码";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(484, 280);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 203);
            this.panel1.TabIndex = 17;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(669, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnGener
            // 
            this.btnGener.Location = new System.Drawing.Point(537, 51);
            this.btnGener.Name = "btnGener";
            this.btnGener.Size = new System.Drawing.Size(71, 23);
            this.btnGener.TabIndex = 21;
            this.btnGener.Text = "生成";
            this.btnGener.UseVisualStyleBackColor = true;
            this.btnGener.Click += new System.EventHandler(this.btnGener_Click);
            // 
            // cmbProject
            // 
            this.cmbProject.FormattingEnabled = true;
            this.cmbProject.Location = new System.Drawing.Point(300, 48);
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Size = new System.Drawing.Size(121, 20);
            this.cmbProject.TabIndex = 24;
            this.cmbProject.SelectedIndexChanged += new System.EventHandler(this.cmbProject_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(241, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "实验项目";
            // 
            // DGV1
            // 
            this.DGV1.AllowUserToAddRows = false;
            this.DGV1.AllowUserToDeleteRows = false;
            this.DGV1.AllowUserToResizeColumns = false;
            this.DGV1.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.DGV1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Location = new System.Drawing.Point(484, 112);
            this.DGV1.Name = "DGV1";
            this.DGV1.ReadOnly = true;
            this.DGV1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.DGV1.RowTemplate.Height = 23;
            this.DGV1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGV1.Size = new System.Drawing.Size(373, 131);
            this.DGV1.TabIndex = 26;
            // 
            // back
            // 
            this.back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.back.Location = new System.Drawing.Point(782, 635);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(75, 23);
            this.back.TabIndex = 27;
            this.back.Text = "返回";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // GRBAddProdure
            // 
            this.GRBAddProdure.Controls.Add(this.sample);
            this.GRBAddProdure.Controls.Add(this.R1);
            this.GRBAddProdure.Controls.Add(this.R2);
            this.GRBAddProdure.Controls.Add(this.R3);
            this.GRBAddProdure.Controls.Add(this.R4);
            this.GRBAddProdure.Controls.Add(this.H);
            this.GRBAddProdure.Controls.Add(this.B);
            this.GRBAddProdure.Controls.Add(this.W);
            this.GRBAddProdure.Controls.Add(this.T);
            this.GRBAddProdure.Controls.Add(this.D);
            this.GRBAddProdure.Location = new System.Drawing.Point(17, 71);
            this.GRBAddProdure.Name = "GRBAddProdure";
            this.GRBAddProdure.Size = new System.Drawing.Size(200, 416);
            this.GRBAddProdure.TabIndex = 28;
            this.GRBAddProdure.TabStop = false;
            this.GRBAddProdure.Text = "添加或修改实验流程";
            // 
            // GRBLocaOrData
            // 
            this.GRBLocaOrData.Controls.Add(this.up);
            this.GRBLocaOrData.Controls.Add(this.down);
            this.GRBLocaOrData.Controls.Add(this.delete);
            this.GRBLocaOrData.Location = new System.Drawing.Point(108, 493);
            this.GRBLocaOrData.Name = "GRBLocaOrData";
            this.GRBLocaOrData.Size = new System.Drawing.Size(120, 165);
            this.GRBLocaOrData.TabIndex = 29;
            this.GRBLocaOrData.TabStop = false;
            this.GRBLocaOrData.Text = "位置或数据调整";
            // 
            // GRB_ischangge
            // 
            this.GRB_ischangge.Controls.Add(this.rbtnAddProject);
            this.GRB_ischangge.Controls.Add(this.rbtnOnlyPara);
            this.GRB_ischangge.Controls.Add(this.rbtnProAndPara);
            this.GRB_ischangge.Location = new System.Drawing.Point(243, 101);
            this.GRB_ischangge.Name = "GRB_ischangge";
            this.GRB_ischangge.Size = new System.Drawing.Size(188, 110);
            this.GRB_ischangge.TabIndex = 30;
            this.GRB_ischangge.TabStop = false;
            // 
            // rbtnAddProject
            // 
            this.rbtnAddProject.AutoSize = true;
            this.rbtnAddProject.Enabled = false;
            this.rbtnAddProject.Location = new System.Drawing.Point(6, 87);
            this.rbtnAddProject.Name = "rbtnAddProject";
            this.rbtnAddProject.Size = new System.Drawing.Size(95, 16);
            this.rbtnAddProject.TabIndex = 2;
            this.rbtnAddProject.TabStop = true;
            this.rbtnAddProject.Text = "增加实验项目";
            this.rbtnAddProject.UseVisualStyleBackColor = true;
            this.rbtnAddProject.CheckedChanged += new System.EventHandler(this.rbtnAddProject_CheckedChanged);
            // 
            // rbtnOnlyPara
            // 
            this.rbtnOnlyPara.AutoSize = true;
            this.rbtnOnlyPara.Location = new System.Drawing.Point(6, 20);
            this.rbtnOnlyPara.Name = "rbtnOnlyPara";
            this.rbtnOnlyPara.Size = new System.Drawing.Size(83, 16);
            this.rbtnOnlyPara.TabIndex = 1;
            this.rbtnOnlyPara.TabStop = true;
            this.rbtnOnlyPara.Text = "仅修改参数";
            this.rbtnOnlyPara.UseVisualStyleBackColor = true;
            this.rbtnOnlyPara.CheckedChanged += new System.EventHandler(this.rbtnOnlyPara_CheckedChanged);
            // 
            // rbtnProAndPara
            // 
            this.rbtnProAndPara.AutoSize = true;
            this.rbtnProAndPara.Enabled = false;
            this.rbtnProAndPara.Location = new System.Drawing.Point(7, 56);
            this.rbtnProAndPara.Name = "rbtnProAndPara";
            this.rbtnProAndPara.Size = new System.Drawing.Size(107, 16);
            this.rbtnProAndPara.TabIndex = 0;
            this.rbtnProAndPara.TabStop = true;
            this.rbtnProAndPara.Text = "修改流程与参数";
            this.rbtnProAndPara.UseVisualStyleBackColor = true;
            this.rbtnProAndPara.CheckedChanged += new System.EventHandler(this.rbtnProAndPara_CheckedChanged);
            // 
            // FrmProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 692);
            this.ControlBox = false;
            this.Controls.Add(this.GRB_ischangge);
            this.Controls.Add(this.GRBLocaOrData);
            this.Controls.Add(this.GRBAddProdure);
            this.Controls.Add(this.back);
            this.Controls.Add(this.DGV1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbProject);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnGener);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvProdure);
            this.Name = "FrmProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "项目流程";
            this.Load += new System.EventHandler(this.FrmProject_Load);
            this.BindingContextChanged += new System.EventHandler(this.r);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            this.GRBAddProdure.ResumeLayout(false);
            this.GRBLocaOrData.ResumeLayout(false);
            this.GRB_ischangge.ResumeLayout(false);
            this.GRB_ischangge.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sample;
        private System.Windows.Forms.DataGridView dgvProdure;
        private System.Windows.Forms.Button R1;
        private System.Windows.Forms.Button R2;
        private System.Windows.Forms.Button R3;
        private System.Windows.Forms.Button H;
        private System.Windows.Forms.Button B;
        private System.Windows.Forms.Button R4;
        private System.Windows.Forms.Button W;
        private System.Windows.Forms.Button T;
        private System.Windows.Forms.Button D;
        private System.Windows.Forms.Button up;
        private System.Windows.Forms.Button down;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnGener;
        private System.Windows.Forms.ComboBox cmbProject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.GroupBox GRBAddProdure;
        private System.Windows.Forms.GroupBox GRBLocaOrData;
        private System.Windows.Forms.GroupBox GRB_ischangge;
        private System.Windows.Forms.RadioButton rbtnAddProject;
        private System.Windows.Forms.RadioButton rbtnOnlyPara;
        private System.Windows.Forms.RadioButton rbtnProAndPara;
    }
}
namespace EBarv0._2.Administrator
{
    partial class frmInfo
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
            this.batchTime = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnQcReset = new System.Windows.Forms.Button();
            this.btnQC2 = new System.Windows.Forms.Button();
            this.btnQC1 = new System.Windows.Forms.Button();
            this.dgvScaling = new System.Windows.Forms.DataGridView();
            this.no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num2 = new System.Windows.Forms.NumericUpDown();
            this.num1 = new System.Windows.Forms.NumericUpDown();
            this.reagentName = new System.Windows.Forms.ComboBox();
            this.prodectTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.back = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.rbtnRg = new System.Windows.Forms.RadioButton();
            this.rbtnDilute = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScaling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnDilute);
            this.groupBox1.Controls.Add(this.rbtnRg);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.batchTime);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnQcReset);
            this.groupBox1.Controls.Add(this.btnQC2);
            this.groupBox1.Controls.Add(this.btnQC1);
            this.groupBox1.Controls.Add(this.dgvScaling);
            this.groupBox1.Controls.Add(this.num2);
            this.groupBox1.Controls.Add(this.num1);
            this.groupBox1.Controls.Add(this.reagentName);
            this.groupBox1.Controls.Add(this.prodectTime);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 382);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息选定";
            // 
            // batchTime
            // 
            this.batchTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.batchTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.batchTime.Location = new System.Drawing.Point(116, 73);
            this.batchTime.Name = "batchTime";
            this.batchTime.Size = new System.Drawing.Size(238, 26);
            this.batchTime.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(17, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 31;
            this.label4.Text = "批号";
            // 
            // btnQcReset
            // 
            this.btnQcReset.Location = new System.Drawing.Point(279, 265);
            this.btnQcReset.Name = "btnQcReset";
            this.btnQcReset.Size = new System.Drawing.Size(75, 23);
            this.btnQcReset.TabIndex = 30;
            this.btnQcReset.Text = "清空质控";
            this.btnQcReset.UseVisualStyleBackColor = true;
            this.btnQcReset.Click += new System.EventHandler(this.btnQcReset_Click);
            // 
            // btnQC2
            // 
            this.btnQC2.Location = new System.Drawing.Point(151, 265);
            this.btnQC2.Name = "btnQC2";
            this.btnQC2.Size = new System.Drawing.Size(85, 23);
            this.btnQC2.TabIndex = 29;
            this.btnQC2.Text = "质控2-高浓度";
            this.btnQC2.UseVisualStyleBackColor = true;
            this.btnQC2.Click += new System.EventHandler(this.btnQC2_Click);
            // 
            // btnQC1
            // 
            this.btnQC1.Location = new System.Drawing.Point(20, 265);
            this.btnQC1.Name = "btnQC1";
            this.btnQC1.Size = new System.Drawing.Size(85, 23);
            this.btnQC1.TabIndex = 28;
            this.btnQC1.Text = "质控1-低浓度";
            this.btnQC1.UseVisualStyleBackColor = true;
            this.btnQC1.Click += new System.EventHandler(this.btnQC1_Click);
            // 
            // dgvScaling
            // 
            this.dgvScaling.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvScaling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScaling.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no,
            this.conc,
            this.value});
            this.dgvScaling.Location = new System.Drawing.Point(20, 320);
            this.dgvScaling.Name = "dgvScaling";
            this.dgvScaling.RowTemplate.Height = 23;
            this.dgvScaling.Size = new System.Drawing.Size(369, 66);
            this.dgvScaling.TabIndex = 24;
            this.dgvScaling.Visible = false;
            // 
            // no
            // 
            this.no.HeaderText = "序号";
            this.no.Name = "no";
            // 
            // conc
            // 
            this.conc.HeaderText = "浓度";
            this.conc.Name = "conc";
            // 
            // value
            // 
            this.value.HeaderText = "发光值";
            this.value.Name = "value";
            // 
            // num2
            // 
            this.num2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.num2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num2.Location = new System.Drawing.Point(268, 211);
            this.num2.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(86, 26);
            this.num2.TabIndex = 18;
            this.num2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // num1
            // 
            this.num1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.num1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num1.Location = new System.Drawing.Point(117, 211);
            this.num1.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(85, 26);
            this.num1.TabIndex = 17;
            this.num1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // reagentName
            // 
            this.reagentName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reagentName.BackColor = System.Drawing.SystemColors.Menu;
            this.reagentName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.reagentName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reagentName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.reagentName.FormattingEnabled = true;
            this.reagentName.IntegralHeight = false;
            this.reagentName.Items.AddRange(new object[] {
            "试剂A",
            "试剂B"});
            this.reagentName.Location = new System.Drawing.Point(116, 161);
            this.reagentName.MaxDropDownItems = 16;
            this.reagentName.Name = "reagentName";
            this.reagentName.Size = new System.Drawing.Size(238, 24);
            this.reagentName.TabIndex = 7;
            this.reagentName.SelectedIndexChanged += new System.EventHandler(this.reagentName_SelectedIndexChanged);
            // 
            // prodectTime
            // 
            this.prodectTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prodectTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prodectTime.Location = new System.Drawing.Point(116, 118);
            this.prodectTime.Name = "prodectTime";
            this.prodectTime.Size = new System.Drawing.Size(238, 26);
            this.prodectTime.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(225, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "到";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(17, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "批内流水号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(17, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(17, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "生产时间";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(546, 315);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(116, 36);
            this.btnGenerate.TabIndex = 43;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 18F);
            this.label14.Location = new System.Drawing.Point(581, 167);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 24);
            this.label14.TabIndex = 50;
            this.label14.Text = "|";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 18F);
            this.label13.Location = new System.Drawing.Point(502, 211);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(166, 24);
            this.label13.TabIndex = 49;
            this.label13.Text = "质控2：未配备";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 18F);
            this.label12.Location = new System.Drawing.Point(502, 119);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(166, 24);
            this.label12.TabIndex = 48;
            this.label12.Text = "质控1：未配备";
            // 
            // back
            // 
            this.back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.back.Location = new System.Drawing.Point(713, 400);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(75, 23);
            this.back.TabIndex = 51;
            this.back.Text = "返回";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(17, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 33;
            this.label6.Text = "试剂类型：";
            // 
            // rbtnRg
            // 
            this.rbtnRg.AutoSize = true;
            this.rbtnRg.Checked = true;
            this.rbtnRg.Location = new System.Drawing.Point(117, 33);
            this.rbtnRg.Name = "rbtnRg";
            this.rbtnRg.Size = new System.Drawing.Size(47, 16);
            this.rbtnRg.TabIndex = 34;
            this.rbtnRg.TabStop = true;
            this.rbtnRg.Text = "试剂";
            this.rbtnRg.UseVisualStyleBackColor = true;
            this.rbtnRg.CheckedChanged += new System.EventHandler(this.rbtnRg_CheckedChanged);
            // 
            // rbtnDilute
            // 
            this.rbtnDilute.AutoSize = true;
            this.rbtnDilute.Location = new System.Drawing.Point(239, 31);
            this.rbtnDilute.Name = "rbtnDilute";
            this.rbtnDilute.Size = new System.Drawing.Size(59, 16);
            this.rbtnDilute.TabIndex = 35;
            this.rbtnDilute.TabStop = true;
            this.rbtnDilute.Text = "稀释液";
            this.rbtnDilute.UseVisualStyleBackColor = true;
            this.rbtnDilute.CheckedChanged += new System.EventHandler(this.rbtnDilute_CheckedChanged);
            // 
            // frmInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.back);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmInfo";
            this.Text = "射频信息生成";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInfo_FormClosed);
            this.Load += new System.EventHandler(this.frmInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScaling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvScaling;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn conc;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.NumericUpDown num2;
        private System.Windows.Forms.NumericUpDown num1;
        private System.Windows.Forms.ComboBox reagentName;
        private System.Windows.Forms.DateTimePicker prodectTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQC2;
        private System.Windows.Forms.Button btnQC1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnQcReset;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.DateTimePicker batchTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbtnDilute;
        private System.Windows.Forms.RadioButton rbtnRg;
        private System.Windows.Forms.Label label6;
    }
}
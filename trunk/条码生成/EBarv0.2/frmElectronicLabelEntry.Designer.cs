namespace EBarv0._2
{
    partial class frmElectronicLabelEntry
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnInput = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbReadyQC = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cb1_2s = new System.Windows.Forms.CheckBox();
            this.cb1_3s = new System.Windows.Forms.CheckBox();
            this.cb2_2s = new System.Windows.Forms.CheckBox();
            this.cb4_1s = new System.Windows.Forms.CheckBox();
            this.cb10x = new System.Windows.Forms.CheckBox();
            this.flpLevel = new System.Windows.Forms.FlowLayoutPanel();
            this.rbLow = new System.Windows.Forms.RadioButton();
            this.rbMid = new System.Windows.Forms.RadioButton();
            this.rbHigh = new System.Windows.Forms.RadioButton();
            this.txtSD = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScaling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flpLevel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(473, 309);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(116, 36);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(648, 309);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(116, 36);
            this.btnInput.TabIndex = 1;
            this.btnInput.Text = "录入下一个";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // back
            // 
            this.back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.back.Location = new System.Drawing.Point(784, 405);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(75, 23);
            this.back.TabIndex = 40;
            this.back.Text = "返回";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbReadyQC);
            this.groupBox1.Controls.Add(this.dgvScaling);
            this.groupBox1.Controls.Add(this.num2);
            this.groupBox1.Controls.Add(this.num1);
            this.groupBox1.Controls.Add(this.reagentName);
            this.groupBox1.Controls.Add(this.prodectTime);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 416);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息选定";
            // 
            // dgvScaling
            // 
            this.dgvScaling.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvScaling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScaling.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no,
            this.conc,
            this.value});
            this.dgvScaling.Location = new System.Drawing.Point(36, 185);
            this.dgvScaling.Name = "dgvScaling";
            this.dgvScaling.RowTemplate.Height = 23;
            this.dgvScaling.Size = new System.Drawing.Size(369, 212);
            this.dgvScaling.TabIndex = 24;
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
            this.num2.Location = new System.Drawing.Point(284, 102);
            this.num2.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(121, 26);
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
            this.num1.Location = new System.Drawing.Point(133, 102);
            this.num1.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(120, 26);
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
            this.reagentName.Location = new System.Drawing.Point(132, 69);
            this.reagentName.MaxDropDownItems = 16;
            this.reagentName.Name = "reagentName";
            this.reagentName.Size = new System.Drawing.Size(273, 24);
            this.reagentName.TabIndex = 7;
            this.reagentName.SelectedIndexChanged += new System.EventHandler(this.reagentName_SelectedIndexChanged);
            // 
            // prodectTime
            // 
            this.prodectTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prodectTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prodectTime.Location = new System.Drawing.Point(132, 28);
            this.prodectTime.Name = "prodectTime";
            this.prodectTime.Size = new System.Drawing.Size(273, 26);
            this.prodectTime.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(259, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "到";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(33, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "批内流水号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(33, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "试剂品名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(33, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "生产时间";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 22F);
            this.label6.Location = new System.Drawing.Point(468, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 30);
            this.label6.TabIndex = 42;
            this.label6.Text = "剩余：0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 22F);
            this.label7.Location = new System.Drawing.Point(468, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(298, 30);
            this.label7.TabIndex = 43;
            this.label7.Text = "下一个批内流水号：0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel1);
            this.groupBox2.Controls.Add(this.flpLevel);
            this.groupBox2.Controls.Add(this.txtSD);
            this.groupBox2.Controls.Add(this.txtX);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(473, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(394, 179);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "质控-配套";
            // 
            // cbReadyQC
            // 
            this.cbReadyQC.AutoSize = true;
            this.cbReadyQC.Location = new System.Drawing.Point(36, 151);
            this.cbReadyQC.Name = "cbReadyQC";
            this.cbReadyQC.Size = new System.Drawing.Size(120, 16);
            this.cbReadyQC.TabIndex = 25;
            this.cbReadyQC.Text = "配备试剂专用质控";
            this.cbReadyQC.UseVisualStyleBackColor = true;
            this.cbReadyQC.CheckedChanged += new System.EventHandler(this.cbReadyQC_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cb1_2s);
            this.flowLayoutPanel1.Controls.Add(this.cb1_3s);
            this.flowLayoutPanel1.Controls.Add(this.cb2_2s);
            this.flowLayoutPanel1.Controls.Add(this.cb4_1s);
            this.flowLayoutPanel1.Controls.Add(this.cb10x);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(99, 105);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(272, 63);
            this.flowLayoutPanel1.TabIndex = 32;
            // 
            // cb1_2s
            // 
            this.cb1_2s.AutoSize = true;
            this.cb1_2s.Checked = true;
            this.cb1_2s.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb1_2s.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb1_2s.Location = new System.Drawing.Point(3, 3);
            this.cb1_2s.Name = "cb1_2s";
            this.cb1_2s.Size = new System.Drawing.Size(59, 20);
            this.cb1_2s.TabIndex = 21;
            this.cb1_2s.Text = "1-2s";
            this.cb1_2s.UseVisualStyleBackColor = true;
            // 
            // cb1_3s
            // 
            this.cb1_3s.AutoSize = true;
            this.cb1_3s.Checked = true;
            this.cb1_3s.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb1_3s.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb1_3s.Location = new System.Drawing.Point(68, 3);
            this.cb1_3s.Name = "cb1_3s";
            this.cb1_3s.Size = new System.Drawing.Size(59, 20);
            this.cb1_3s.TabIndex = 22;
            this.cb1_3s.Text = "1-3s";
            this.cb1_3s.UseVisualStyleBackColor = true;
            // 
            // cb2_2s
            // 
            this.cb2_2s.AutoSize = true;
            this.cb2_2s.Checked = true;
            this.cb2_2s.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb2_2s.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb2_2s.Location = new System.Drawing.Point(133, 3);
            this.cb2_2s.Name = "cb2_2s";
            this.cb2_2s.Size = new System.Drawing.Size(59, 20);
            this.cb2_2s.TabIndex = 23;
            this.cb2_2s.Text = "2-2s";
            this.cb2_2s.UseVisualStyleBackColor = true;
            // 
            // cb4_1s
            // 
            this.cb4_1s.AutoSize = true;
            this.cb4_1s.Checked = true;
            this.cb4_1s.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb4_1s.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb4_1s.Location = new System.Drawing.Point(198, 3);
            this.cb4_1s.Name = "cb4_1s";
            this.cb4_1s.Size = new System.Drawing.Size(59, 20);
            this.cb4_1s.TabIndex = 24;
            this.cb4_1s.Text = "4-1s";
            this.cb4_1s.UseVisualStyleBackColor = true;
            // 
            // cb10x
            // 
            this.cb10x.AutoSize = true;
            this.cb10x.Checked = true;
            this.cb10x.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb10x.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb10x.Location = new System.Drawing.Point(3, 29);
            this.cb10x.Name = "cb10x";
            this.cb10x.Size = new System.Drawing.Size(51, 20);
            this.cb10x.TabIndex = 25;
            this.cb10x.Text = "10x";
            this.cb10x.UseVisualStyleBackColor = true;
            // 
            // flpLevel
            // 
            this.flpLevel.Controls.Add(this.rbLow);
            this.flpLevel.Controls.Add(this.rbMid);
            this.flpLevel.Controls.Add(this.rbHigh);
            this.flpLevel.Location = new System.Drawing.Point(99, 80);
            this.flpLevel.Name = "flpLevel";
            this.flpLevel.Size = new System.Drawing.Size(272, 23);
            this.flpLevel.TabIndex = 31;
            // 
            // rbLow
            // 
            this.rbLow.AutoSize = true;
            this.rbLow.Checked = true;
            this.rbLow.Location = new System.Drawing.Point(3, 3);
            this.rbLow.Name = "rbLow";
            this.rbLow.Size = new System.Drawing.Size(59, 16);
            this.rbLow.TabIndex = 12;
            this.rbLow.TabStop = true;
            this.rbLow.Text = "低水平";
            this.rbLow.UseVisualStyleBackColor = true;
            // 
            // rbMid
            // 
            this.rbMid.AutoSize = true;
            this.rbMid.Location = new System.Drawing.Point(68, 3);
            this.rbMid.Name = "rbMid";
            this.rbMid.Size = new System.Drawing.Size(59, 16);
            this.rbMid.TabIndex = 13;
            this.rbMid.Text = "中水平";
            this.rbMid.UseVisualStyleBackColor = true;
            // 
            // rbHigh
            // 
            this.rbHigh.AutoSize = true;
            this.rbHigh.Location = new System.Drawing.Point(133, 3);
            this.rbHigh.Name = "rbHigh";
            this.rbHigh.Size = new System.Drawing.Size(59, 16);
            this.rbHigh.TabIndex = 14;
            this.rbHigh.Text = "高水平";
            this.rbHigh.UseVisualStyleBackColor = true;
            // 
            // txtSD
            // 
            this.txtSD.Location = new System.Drawing.Point(99, 47);
            this.txtSD.Name = "txtSD";
            this.txtSD.Size = new System.Drawing.Size(272, 21);
            this.txtSD.TabIndex = 30;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(99, 20);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(272, 21);
            this.txtX.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(12, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 29;
            this.label4.Text = "质控规则";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F);
            this.label8.Location = new System.Drawing.Point(12, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 28;
            this.label8.Text = "质控类别";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F);
            this.label9.Location = new System.Drawing.Point(12, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 27;
            this.label9.Text = "质控标准差";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F);
            this.label10.Location = new System.Drawing.Point(12, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 26;
            this.label10.Text = "质控靶值";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker1.Location = new System.Drawing.Point(66, -30);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(360, 26);
            this.dateTimePicker1.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F);
            this.label11.Location = new System.Drawing.Point(-33, -23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 23;
            this.label11.Text = "生产时间";
            // 
            // frmElectronicLabelEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 450);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.back);
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.btnGenerate);
            this.Name = "frmElectronicLabelEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电子标签录入";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmElectronicLabelEntry_FormClosed);
            this.Load += new System.EventHandler(this.frmElectronicLabelEntry_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScaling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flpLevel.ResumeLayout(false);
            this.flpLevel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown num2;
        private System.Windows.Forms.NumericUpDown num1;
        private System.Windows.Forms.ComboBox reagentName;
        private System.Windows.Forms.DateTimePicker prodectTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvScaling;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn conc;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbReadyQC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox cb1_2s;
        private System.Windows.Forms.CheckBox cb1_3s;
        private System.Windows.Forms.CheckBox cb2_2s;
        private System.Windows.Forms.CheckBox cb4_1s;
        private System.Windows.Forms.CheckBox cb10x;
        private System.Windows.Forms.FlowLayoutPanel flpLevel;
        private System.Windows.Forms.RadioButton rbLow;
        private System.Windows.Forms.RadioButton rbMid;
        private System.Windows.Forms.RadioButton rbHigh;
        private System.Windows.Forms.TextBox txtSD;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label11;
    }
}
namespace EBarv0._2
{
    partial class FrmScaling
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
            this.labelA = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grbScaling = new System.Windows.Forms.GroupBox();
            this.textBoxA = new System.Windows.Forms.TextBox();
            this.textBoxG = new System.Windows.Forms.TextBox();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.textBoxC = new System.Windows.Forms.TextBox();
            this.textBoxD = new System.Windows.Forms.TextBox();
            this.textBoxE = new System.Windows.Forms.TextBox();
            this.textBoxF = new System.Windows.Forms.TextBox();
            this.gBoxScaling = new System.Windows.Forms.GroupBox();
            this.rbtn7Point = new System.Windows.Forms.RadioButton();
            this.rbtn6Point = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelF = new System.Windows.Forms.Label();
            this.labelE = new System.Windows.Forms.Label();
            this.labelD = new System.Windows.Forms.Label();
            this.labelC = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGener = new System.Windows.Forms.Button();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grbScaling.SuspendLayout();
            this.gBoxScaling.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelA
            // 
            this.labelA.AutoSize = true;
            this.labelA.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelA.Location = new System.Drawing.Point(43, 125);
            this.labelA.Name = "labelA";
            this.labelA.Size = new System.Drawing.Size(62, 17);
            this.labelA.TabIndex = 9;
            this.labelA.Text = "1.标准点A";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.grbScaling);
            this.groupBox1.Controls.Add(this.gBoxScaling);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelF);
            this.groupBox1.Controls.Add(this.labelE);
            this.groupBox1.Controls.Add(this.labelD);
            this.groupBox1.Controls.Add(this.labelC);
            this.groupBox1.Controls.Add(this.labelB);
            this.groupBox1.Controls.Add(this.labelA);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 489);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标准曲线定标点与校准点浓度";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "浓度：";
            // 
            // grbScaling
            // 
            this.grbScaling.Controls.Add(this.textBoxA);
            this.grbScaling.Controls.Add(this.textBoxG);
            this.grbScaling.Controls.Add(this.textBoxB);
            this.grbScaling.Controls.Add(this.textBoxC);
            this.grbScaling.Controls.Add(this.textBoxD);
            this.grbScaling.Controls.Add(this.textBoxE);
            this.grbScaling.Controls.Add(this.textBoxF);
            this.grbScaling.Location = new System.Drawing.Point(154, 105);
            this.grbScaling.Name = "grbScaling";
            this.grbScaling.Size = new System.Drawing.Size(145, 381);
            this.grbScaling.TabIndex = 18;
            this.grbScaling.TabStop = false;
            this.grbScaling.Paint += new System.Windows.Forms.PaintEventHandler(this.GrbScaling_Paint);
            // 
            // textBoxA
            // 
            this.textBoxA.Location = new System.Drawing.Point(17, 20);
            this.textBoxA.Name = "textBoxA";
            this.textBoxA.Size = new System.Drawing.Size(100, 21);
            this.textBoxA.TabIndex = 15;
            // 
            // textBoxG
            // 
            this.textBoxG.Location = new System.Drawing.Point(17, 351);
            this.textBoxG.Name = "textBoxG";
            this.textBoxG.Size = new System.Drawing.Size(100, 21);
            this.textBoxG.TabIndex = 24;
            // 
            // textBoxB
            // 
            this.textBoxB.Location = new System.Drawing.Point(17, 75);
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(100, 21);
            this.textBoxB.TabIndex = 16;
            // 
            // textBoxC
            // 
            this.textBoxC.Location = new System.Drawing.Point(17, 134);
            this.textBoxC.Name = "textBoxC";
            this.textBoxC.Size = new System.Drawing.Size(100, 21);
            this.textBoxC.TabIndex = 17;
            // 
            // textBoxD
            // 
            this.textBoxD.Location = new System.Drawing.Point(17, 197);
            this.textBoxD.Name = "textBoxD";
            this.textBoxD.Size = new System.Drawing.Size(100, 21);
            this.textBoxD.TabIndex = 18;
            // 
            // textBoxE
            // 
            this.textBoxE.Location = new System.Drawing.Point(17, 246);
            this.textBoxE.Name = "textBoxE";
            this.textBoxE.Size = new System.Drawing.Size(100, 21);
            this.textBoxE.TabIndex = 19;
            // 
            // textBoxF
            // 
            this.textBoxF.Location = new System.Drawing.Point(17, 301);
            this.textBoxF.Name = "textBoxF";
            this.textBoxF.Size = new System.Drawing.Size(100, 21);
            this.textBoxF.TabIndex = 20;
            // 
            // gBoxScaling
            // 
            this.gBoxScaling.Controls.Add(this.rbtn7Point);
            this.gBoxScaling.Controls.Add(this.rbtn6Point);
            this.gBoxScaling.Location = new System.Drawing.Point(44, 20);
            this.gBoxScaling.Name = "gBoxScaling";
            this.gBoxScaling.Size = new System.Drawing.Size(219, 56);
            this.gBoxScaling.TabIndex = 25;
            this.gBoxScaling.TabStop = false;
            this.gBoxScaling.Text = "定标点选择";
            // 
            // rbtn7Point
            // 
            this.rbtn7Point.AutoSize = true;
            this.rbtn7Point.Location = new System.Drawing.Point(128, 25);
            this.rbtn7Point.Name = "rbtn7Point";
            this.rbtn7Point.Size = new System.Drawing.Size(71, 16);
            this.rbtn7Point.TabIndex = 1;
            this.rbtn7Point.TabStop = true;
            this.rbtn7Point.Text = "七点定标";
            this.rbtn7Point.UseVisualStyleBackColor = true;
            this.rbtn7Point.CheckedChanged += new System.EventHandler(this.Rbtn7Point_CheckedChanged);
            // 
            // rbtn6Point
            // 
            this.rbtn6Point.AutoSize = true;
            this.rbtn6Point.Location = new System.Drawing.Point(19, 25);
            this.rbtn6Point.Name = "rbtn6Point";
            this.rbtn6Point.Size = new System.Drawing.Size(71, 16);
            this.rbtn6Point.TabIndex = 0;
            this.rbtn6Point.TabStop = true;
            this.rbtn6Point.Text = "六点定标";
            this.rbtn6Point.UseVisualStyleBackColor = true;
            this.rbtn6Point.CheckedChanged += new System.EventHandler(this.Rbtn6Point_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(43, 456);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "7.标准点G";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "类别：";
            // 
            // labelF
            // 
            this.labelF.AutoSize = true;
            this.labelF.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelF.Location = new System.Drawing.Point(43, 406);
            this.labelF.Name = "labelF";
            this.labelF.Size = new System.Drawing.Size(60, 17);
            this.labelF.TabIndex = 14;
            this.labelF.Text = "6.标准点F";
            // 
            // labelE
            // 
            this.labelE.AutoSize = true;
            this.labelE.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelE.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelE.Location = new System.Drawing.Point(43, 351);
            this.labelE.Name = "labelE";
            this.labelE.Size = new System.Drawing.Size(61, 17);
            this.labelE.TabIndex = 13;
            this.labelE.Text = "5.标准点E";
            // 
            // labelD
            // 
            this.labelD.AutoSize = true;
            this.labelD.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelD.Location = new System.Drawing.Point(41, 302);
            this.labelD.Name = "labelD";
            this.labelD.Size = new System.Drawing.Size(63, 17);
            this.labelD.TabIndex = 12;
            this.labelD.Text = "4.标准点D";
            // 
            // labelC
            // 
            this.labelC.AutoSize = true;
            this.labelC.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelC.Location = new System.Drawing.Point(43, 239);
            this.labelC.Name = "labelC";
            this.labelC.Size = new System.Drawing.Size(62, 17);
            this.labelC.TabIndex = 11;
            this.labelC.Text = "3.标准点C";
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelB.Location = new System.Drawing.Point(43, 180);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(62, 17);
            this.labelB.TabIndex = 10;
            this.labelB.Text = "2.标准点B";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(760, 507);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "返回";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(419, 250);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(416, 251);
            this.panel1.TabIndex = 12;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnGener
            // 
            this.btnGener.Location = new System.Drawing.Point(419, 12);
            this.btnGener.Name = "btnGener";
            this.btnGener.Size = new System.Drawing.Size(75, 23);
            this.btnGener.TabIndex = 13;
            this.btnGener.Text = "生成";
            this.btnGener.UseVisualStyleBackColor = true;
            this.btnGener.Click += new System.EventHandler(this.button3_Click);
            // 
            // DGV1
            // 
            this.DGV1.AllowUserToAddRows = false;
            this.DGV1.AllowUserToDeleteRows = false;
            this.DGV1.AllowUserToResizeColumns = false;
            this.DGV1.AllowUserToResizeRows = false;
            this.DGV1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGV1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.DGV1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Location = new System.Drawing.Point(419, 61);
            this.DGV1.Name = "DGV1";
            this.DGV1.ReadOnly = true;
            this.DGV1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.DGV1.RowTemplate.Height = 23;
            this.DGV1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGV1.Size = new System.Drawing.Size(416, 152);
            this.DGV1.TabIndex = 14;
            this.DGV1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(415, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "编码";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(415, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "条码";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(527, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(633, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 18;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // FrmScaling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 537);
            this.ControlBox = false;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DGV1);
            this.Controls.Add(this.btnGener);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmScaling";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "浓度-定标";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmScaling_FormClosed);
            this.Load += new System.EventHandler(this.FrmScaling_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbScaling.ResumeLayout(false);
            this.grbScaling.PerformLayout();
            this.gBoxScaling.ResumeLayout(false);
            this.gBoxScaling.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelA;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelE;
        private System.Windows.Forms.Label labelD;
        private System.Windows.Forms.Label labelC;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Label labelF;
        private System.Windows.Forms.TextBox textBoxA;
        private System.Windows.Forms.TextBox textBoxF;
        private System.Windows.Forms.TextBox textBoxE;
        private System.Windows.Forms.TextBox textBoxD;
        private System.Windows.Forms.TextBox textBoxC;
        private System.Windows.Forms.TextBox textBoxB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGener;
        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gBoxScaling;
        private System.Windows.Forms.RadioButton rbtn7Point;
        private System.Windows.Forms.RadioButton rbtn6Point;
        private System.Windows.Forms.TextBox textBoxG;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grbScaling;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label4;
    }
}
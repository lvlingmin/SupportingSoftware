namespace fourLogistic.Forms
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rbtnConcToPmt0 = new System.Windows.Forms.RadioButton();
            this.rbtnPmtToConc0 = new System.Windows.Forms.RadioButton();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.concentration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Responsevalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countConc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnFit = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ShowCurvePnl = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.rbtnConcToPmt = new System.Windows.Forms.RadioButton();
            this.rbtnPmtToConc = new System.Windows.Forms.RadioButton();
            this.grbPara = new System.Windows.Forms.GroupBox();
            this.rtbPara = new System.Windows.Forms.RichTextBox();
            this.cmbY = new System.Windows.Forms.ComboBox();
            this.cmbX = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFit = new System.Windows.Forms.ComboBox();
            this.lblFit = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnCopy2 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grbPara.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1405, 682);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rbtnConcToPmt0);
            this.tabPage1.Controls.Add(this.rbtnPmtToConc0);
            this.tabPage1.Controls.Add(this.zedGraphControl1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnPaste);
            this.tabPage1.Controls.Add(this.btnCopy);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.btnFit);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1397, 656);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "夹心";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rbtnConcToPmt0
            // 
            this.rbtnConcToPmt0.AutoSize = true;
            this.rbtnConcToPmt0.Location = new System.Drawing.Point(252, 3);
            this.rbtnConcToPmt0.Name = "rbtnConcToPmt0";
            this.rbtnConcToPmt0.Size = new System.Drawing.Size(125, 16);
            this.rbtnConcToPmt0.TabIndex = 77;
            this.rbtnConcToPmt0.TabStop = true;
            this.rbtnConcToPmt0.Text = "发光值  <--  浓度";
            this.rbtnConcToPmt0.UseVisualStyleBackColor = true;
            // 
            // rbtnPmtToConc0
            // 
            this.rbtnPmtToConc0.AutoSize = true;
            this.rbtnPmtToConc0.Checked = true;
            this.rbtnPmtToConc0.Location = new System.Drawing.Point(6, 6);
            this.rbtnPmtToConc0.Name = "rbtnPmtToConc0";
            this.rbtnPmtToConc0.Size = new System.Drawing.Size(125, 16);
            this.rbtnPmtToConc0.TabIndex = 76;
            this.rbtnPmtToConc0.TabStop = true;
            this.rbtnPmtToConc0.Text = "发光值  -->  浓度";
            this.rbtnPmtToConc0.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.ForeColor = System.Drawing.Color.Black;
            this.zedGraphControl1.Location = new System.Drawing.Point(622, 280);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(8, 12, 8, 12);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.PanModifierKeys = System.Windows.Forms.Keys.None;
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(591, 315);
            this.zedGraphControl1.TabIndex = 75;
            this.zedGraphControl1.ZoomStepFraction = 0D;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.concentration,
            this.Responsevalue,
            this.countConc});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dataGridView1.Location = new System.Drawing.Point(6, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(371, 622);
            this.dataGridView1.TabIndex = 67;
            // 
            // concentration
            // 
            this.concentration.DataPropertyName = "concentration";
            this.concentration.FillWeight = 1F;
            this.concentration.HeaderText = "浓度";
            this.concentration.Name = "concentration";
            // 
            // Responsevalue
            // 
            this.Responsevalue.DataPropertyName = "Responsevalue";
            this.Responsevalue.FillWeight = 1F;
            this.Responsevalue.HeaderText = "发光值";
            this.Responsevalue.Name = "Responsevalue";
            // 
            // countConc
            // 
            this.countConc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.countConc.FillWeight = 1F;
            this.countConc.HeaderText = "计算浓度";
            this.countConc.Name = "countConc";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(467, 268);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 30);
            this.button2.TabIndex = 74;
            this.button2.Text = "全部清除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(427, 366);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(184, 204);
            this.richTextBox1.TabIndex = 73;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(660, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 27);
            this.label1.TabIndex = 72;
            this.label1.Text = "暂时只用来做夹心法";
            this.label1.Visible = false;
            // 
            // btnPaste
            // 
            this.btnPaste.Location = new System.Drawing.Point(467, 201);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(64, 30);
            this.btnPaste.TabIndex = 71;
            this.btnPaste.Text = "粘贴";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(467, 136);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(64, 30);
            this.btnCopy.TabIndex = 70;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(467, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 30);
            this.button1.TabIndex = 69;
            this.button1.Text = "选中清除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnFit
            // 
            this.btnFit.Location = new System.Drawing.Point(467, 22);
            this.btnFit.Name = "btnFit";
            this.btnFit.Size = new System.Drawing.Size(64, 30);
            this.btnFit.TabIndex = 68;
            this.btnFit.Text = "生成";
            this.btnFit.UseVisualStyleBackColor = true;
            this.btnFit.Click += new System.EventHandler(this.btnFit_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.rbtnConcToPmt);
            this.tabPage2.Controls.Add(this.rbtnPmtToConc);
            this.tabPage2.Controls.Add(this.grbPara);
            this.tabPage2.Controls.Add(this.cmbY);
            this.tabPage2.Controls.Add(this.cmbX);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cmbFit);
            this.tabPage2.Controls.Add(this.lblFit);
            this.tabPage2.Controls.Add(this.btnClear);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.btnCopy2);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1397, 656);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "竞争";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.groupBox1.Controls.Add(this.ShowCurvePnl);
            this.groupBox1.Location = new System.Drawing.Point(734, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 532);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "曲线";
            // 
            // ShowCurvePnl
            // 
            this.ShowCurvePnl.Location = new System.Drawing.Point(28, 26);
            this.ShowCurvePnl.Name = "ShowCurvePnl";
            this.ShowCurvePnl.Size = new System.Drawing.Size(560, 490);
            this.ShowCurvePnl.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(450, 168);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 30);
            this.button3.TabIndex = 75;
            this.button3.Text = "选中清除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button1_Click2);
            // 
            // rbtnConcToPmt
            // 
            this.rbtnConcToPmt.AutoSize = true;
            this.rbtnConcToPmt.Location = new System.Drawing.Point(252, 3);
            this.rbtnConcToPmt.Name = "rbtnConcToPmt";
            this.rbtnConcToPmt.Size = new System.Drawing.Size(125, 16);
            this.rbtnConcToPmt.TabIndex = 74;
            this.rbtnConcToPmt.TabStop = true;
            this.rbtnConcToPmt.Text = "发光值  <--  浓度";
            this.rbtnConcToPmt.UseVisualStyleBackColor = true;
            // 
            // rbtnPmtToConc
            // 
            this.rbtnPmtToConc.AutoSize = true;
            this.rbtnPmtToConc.Location = new System.Drawing.Point(6, 6);
            this.rbtnPmtToConc.Name = "rbtnPmtToConc";
            this.rbtnPmtToConc.Size = new System.Drawing.Size(125, 16);
            this.rbtnPmtToConc.TabIndex = 73;
            this.rbtnPmtToConc.TabStop = true;
            this.rbtnPmtToConc.Text = "发光值  -->  浓度";
            this.rbtnPmtToConc.UseVisualStyleBackColor = true;
            // 
            // grbPara
            // 
            this.grbPara.BackColor = System.Drawing.Color.White;
            this.grbPara.Controls.Add(this.rtbPara);
            this.grbPara.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grbPara.Location = new System.Drawing.Point(430, 357);
            this.grbPara.Name = "grbPara";
            this.grbPara.Size = new System.Drawing.Size(280, 224);
            this.grbPara.TabIndex = 64;
            this.grbPara.TabStop = false;
            this.grbPara.Text = "方程参数";
            // 
            // rtbPara
            // 
            this.rtbPara.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbPara.Location = new System.Drawing.Point(10, 21);
            this.rtbPara.Name = "rtbPara";
            this.rtbPara.Size = new System.Drawing.Size(264, 197);
            this.rtbPara.TabIndex = 0;
            this.rtbPara.Text = "";
            // 
            // cmbY
            // 
            this.cmbY.Enabled = false;
            this.cmbY.FormattingEnabled = true;
            this.cmbY.Items.AddRange(new object[] {
            "不转换",
            "对数(2为底)",
            "对数(10为底)",
            "对数(e为底)",
            "平方",
            "平方根",
            "指数"});
            this.cmbY.Location = new System.Drawing.Point(619, 268);
            this.cmbY.Name = "cmbY";
            this.cmbY.Size = new System.Drawing.Size(109, 20);
            this.cmbY.TabIndex = 72;
            this.cmbY.Visible = false;
            // 
            // cmbX
            // 
            this.cmbX.Enabled = false;
            this.cmbX.FormattingEnabled = true;
            this.cmbX.Items.AddRange(new object[] {
            "不转换",
            "对数(2为底)",
            "对数(10为底)",
            "对数(e为底)",
            "平方",
            "平方根",
            "指数"});
            this.cmbX.Location = new System.Drawing.Point(619, 225);
            this.cmbX.Name = "cmbX";
            this.cmbX.Size = new System.Drawing.Size(109, 20);
            this.cmbX.TabIndex = 71;
            this.cmbX.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(556, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 70;
            this.label4.Text = "Y数据列";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(556, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 69;
            this.label3.Text = "X数据列";
            this.label3.Visible = false;
            // 
            // cmbFit
            // 
            this.cmbFit.Enabled = false;
            this.cmbFit.FormattingEnabled = true;
            this.cmbFit.Items.AddRange(new object[] {
            "线性回归",
            "点对点",
            "二次多项式",
            "三次多项式",
            "Log-Logit",
            "四参数Logistic曲线拟合",
            "五参数Logistic曲线拟合",
            "四参数Logistic曲线拟合(竞争法)"});
            this.cmbFit.Location = new System.Drawing.Point(517, 142);
            this.cmbFit.Name = "cmbFit";
            this.cmbFit.Size = new System.Drawing.Size(211, 20);
            this.cmbFit.TabIndex = 68;
            this.cmbFit.Visible = false;
            // 
            // lblFit
            // 
            this.lblFit.AutoSize = true;
            this.lblFit.Location = new System.Drawing.Point(448, 6);
            this.lblFit.Name = "lblFit";
            this.lblFit.Size = new System.Drawing.Size(95, 12);
            this.lblFit.TabIndex = 67;
            this.lblFit.Text = "回归/拟合模型：";
            this.lblFit.Visible = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(451, 82);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 66;
            this.btnClear.Text = "全部清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click2);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(450, 279);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 32);
            this.button4.TabIndex = 65;
            this.button4.Text = "粘贴";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnCopy2
            // 
            this.btnCopy2.Location = new System.Drawing.Point(450, 225);
            this.btnCopy2.Name = "btnCopy2";
            this.btnCopy2.Size = new System.Drawing.Size(100, 31);
            this.btnCopy2.TabIndex = 63;
            this.btnCopy2.Text = "复制";
            this.btnCopy2.UseVisualStyleBackColor = true;
            this.btnCopy2.Click += new System.EventHandler(this.btnCopy2_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(451, 33);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(100, 30);
            this.button6.TabIndex = 62;
            this.button6.Text = "生成";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btnFit_Click2);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView2.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dataGridView2.Location = new System.Drawing.Point(6, 28);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView2.Size = new System.Drawing.Size(371, 622);
            this.dataGridView2.TabIndex = 61;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "concentration";
            this.dataGridViewTextBoxColumn1.FillWeight = 76.14214F;
            this.dataGridViewTextBoxColumn1.HeaderText = "浓度";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Responsevalue";
            this.dataGridViewTextBoxColumn2.FillWeight = 106.8817F;
            this.dataGridViewTextBoxColumn2.HeaderText = "发光值";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "计算浓度";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1429, 706);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "博科诊断拟合软件_Version_20201022";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.grbPara.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn concentration;
        private System.Windows.Forms.DataGridViewTextBoxColumn Responsevalue;
        private System.Windows.Forms.DataGridViewTextBoxColumn countConc;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnFit;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RadioButton rbtnConcToPmt;
        private System.Windows.Forms.RadioButton rbtnPmtToConc;
        private System.Windows.Forms.GroupBox grbPara;
        private System.Windows.Forms.RichTextBox rtbPara;
        private System.Windows.Forms.ComboBox cmbY;
        private System.Windows.Forms.ComboBox cmbX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFit;
        private System.Windows.Forms.Label lblFit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnCopy2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel ShowCurvePnl;
        private System.Windows.Forms.RadioButton rbtnConcToPmt0;
        private System.Windows.Forms.RadioButton rbtnPmtToConc0;
    }
}
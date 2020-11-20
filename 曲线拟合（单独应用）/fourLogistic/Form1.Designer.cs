namespace fourLogistic
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabValueInput = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
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
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnFit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.concentration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Responsevalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countConc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MenuCurve = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.ShowCurvePnl = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.rtbTest = new System.Windows.Forms.RichTextBox();
            this.tbTest = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabValueInput.SuspendLayout();
            this.grbPara.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.MenuCurve.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabValueInput);
            this.tabControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(13, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(732, 626);
            this.tabControl1.TabIndex = 0;
            // 
            // tabValueInput
            // 
            this.tabValueInput.BackColor = System.Drawing.SystemColors.Window;
            this.tabValueInput.Controls.Add(this.button1);
            this.tabValueInput.Controls.Add(this.rbtnConcToPmt);
            this.tabValueInput.Controls.Add(this.rbtnPmtToConc);
            this.tabValueInput.Controls.Add(this.grbPara);
            this.tabValueInput.Controls.Add(this.cmbY);
            this.tabValueInput.Controls.Add(this.cmbX);
            this.tabValueInput.Controls.Add(this.label4);
            this.tabValueInput.Controls.Add(this.label3);
            this.tabValueInput.Controls.Add(this.cmbFit);
            this.tabValueInput.Controls.Add(this.lblFit);
            this.tabValueInput.Controls.Add(this.btnClear);
            this.tabValueInput.Controls.Add(this.btnPaste);
            this.tabValueInput.Controls.Add(this.btnCopy);
            this.tabValueInput.Controls.Add(this.btnFit);
            this.tabValueInput.Controls.Add(this.dataGridView1);
            this.tabValueInput.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabValueInput.Location = new System.Drawing.Point(4, 26);
            this.tabValueInput.Name = "tabValueInput";
            this.tabValueInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabValueInput.Size = new System.Drawing.Size(724, 596);
            this.tabValueInput.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(157, 295);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 60;
            this.button1.Text = "选中清除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click2);
            // 
            // rbtnConcToPmt
            // 
            this.rbtnConcToPmt.AutoSize = true;
            this.rbtnConcToPmt.Location = new System.Drawing.Point(574, 13);
            this.rbtnConcToPmt.Name = "rbtnConcToPmt";
            this.rbtnConcToPmt.Size = new System.Drawing.Size(125, 16);
            this.rbtnConcToPmt.TabIndex = 59;
            this.rbtnConcToPmt.TabStop = true;
            this.rbtnConcToPmt.Text = "发光值  <--  浓度";
            this.rbtnConcToPmt.UseVisualStyleBackColor = true;
            // 
            // rbtnPmtToConc
            // 
            this.rbtnPmtToConc.AutoSize = true;
            this.rbtnPmtToConc.Location = new System.Drawing.Point(292, 15);
            this.rbtnPmtToConc.Name = "rbtnPmtToConc";
            this.rbtnPmtToConc.Size = new System.Drawing.Size(125, 16);
            this.rbtnPmtToConc.TabIndex = 58;
            this.rbtnPmtToConc.TabStop = true;
            this.rbtnPmtToConc.Text = "发光值  -->  浓度";
            this.rbtnPmtToConc.UseVisualStyleBackColor = true;
            // 
            // grbPara
            // 
            this.grbPara.BackColor = System.Drawing.Color.White;
            this.grbPara.Controls.Add(this.rtbPara);
            this.grbPara.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grbPara.Location = new System.Drawing.Point(6, 366);
            this.grbPara.Name = "grbPara";
            this.grbPara.Size = new System.Drawing.Size(280, 224);
            this.grbPara.TabIndex = 3;
            this.grbPara.TabStop = false;
            this.grbPara.Text = "方程参数";
            this.grbPara.Enter += new System.EventHandler(this.GrbPara_Enter);
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
            this.cmbY.Location = new System.Drawing.Point(77, 154);
            this.cmbY.Name = "cmbY";
            this.cmbY.Size = new System.Drawing.Size(109, 20);
            this.cmbY.TabIndex = 57;
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
            this.cmbX.Location = new System.Drawing.Point(77, 101);
            this.cmbX.Name = "cmbX";
            this.cmbX.Size = new System.Drawing.Size(109, 20);
            this.cmbX.TabIndex = 56;
            this.cmbX.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(24, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 55;
            this.label4.Text = "Y数据列";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(24, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 54;
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
            this.cmbFit.Location = new System.Drawing.Point(27, 52);
            this.cmbFit.Name = "cmbFit";
            this.cmbFit.Size = new System.Drawing.Size(211, 20);
            this.cmbFit.TabIndex = 6;
            // 
            // lblFit
            // 
            this.lblFit.AutoSize = true;
            this.lblFit.Location = new System.Drawing.Point(24, 15);
            this.lblFit.Name = "lblFit";
            this.lblFit.Size = new System.Drawing.Size(95, 12);
            this.lblFit.TabIndex = 5;
            this.lblFit.Text = "回归/拟合模型：";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(157, 218);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "全部清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click2);
            // 
            // btnPaste
            // 
            this.btnPaste.Location = new System.Drawing.Point(27, 305);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(48, 20);
            this.btnPaste.TabIndex = 3;
            this.btnPaste.Text = "粘贴";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Visible = false;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(27, 267);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(48, 20);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Visible = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnFit
            // 
            this.btnFit.Location = new System.Drawing.Point(27, 218);
            this.btnFit.Name = "btnFit";
            this.btnFit.Size = new System.Drawing.Size(100, 30);
            this.btnFit.TabIndex = 1;
            this.btnFit.Text = "生成";
            this.btnFit.UseVisualStyleBackColor = true;
            this.btnFit.Click += new System.EventHandler(this.btnFit_Click2);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.concentration,
            this.Responsevalue,
            this.countConc});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dataGridView1.Location = new System.Drawing.Point(292, 52);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(407, 532);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataGridView1_PreviewKeyDown);
            // 
            // concentration
            // 
            this.concentration.DataPropertyName = "concentration";
            this.concentration.FillWeight = 76.14214F;
            this.concentration.HeaderText = "浓度";
            this.concentration.Name = "concentration";
            // 
            // Responsevalue
            // 
            this.Responsevalue.DataPropertyName = "Responsevalue";
            this.Responsevalue.FillWeight = 106.8817F;
            this.Responsevalue.HeaderText = "发光值";
            this.Responsevalue.Name = "Responsevalue";
            // 
            // countConc
            // 
            this.countConc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.countConc.HeaderText = "计算浓度";
            this.countConc.Name = "countConc";
            // 
            // MenuCurve
            // 
            this.MenuCurve.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemSave});
            this.MenuCurve.Name = "MenuCurve";
            this.MenuCurve.Size = new System.Drawing.Size(125, 26);
            // 
            // itemSave
            // 
            this.itemSave.Name = "itemSave";
            this.itemSave.Size = new System.Drawing.Size(124, 22);
            this.itemSave.Text = "保存图像";
            this.itemSave.Click += new System.EventHandler(this.itemSave_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // ShowCurvePnl
            // 
            this.ShowCurvePnl.Location = new System.Drawing.Point(28, 26);
            this.ShowCurvePnl.Name = "ShowCurvePnl";
            this.ShowCurvePnl.Size = new System.Drawing.Size(500, 500);
            this.ShowCurvePnl.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.groupBox1.Controls.Add(this.ShowCurvePnl);
            this.groupBox1.Location = new System.Drawing.Point(759, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(556, 544);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "曲线";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(57, 676);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // rtbTest
            // 
            this.rtbTest.Location = new System.Drawing.Point(355, 644);
            this.rtbTest.Name = "rtbTest";
            this.rtbTest.Size = new System.Drawing.Size(429, 96);
            this.rtbTest.TabIndex = 6;
            this.rtbTest.Text = "";
            this.rtbTest.Visible = false;
            // 
            // tbTest
            // 
            this.tbTest.Location = new System.Drawing.Point(174, 676);
            this.tbTest.Name = "tbTest";
            this.tbTest.Size = new System.Drawing.Size(129, 21);
            this.tbTest.TabIndex = 7;
            this.tbTest.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1327, 763);
            this.Controls.Add(this.tbTest);
            this.Controls.Add(this.rtbTest);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "博科诊断拟合曲线";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabValueInput.ResumeLayout(false);
            this.tabValueInput.PerformLayout();
            this.grbPara.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.MenuCurve.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabValueInput;
        private System.Windows.Forms.Button btnFit;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ContextMenuStrip MenuCurve;
        private System.Windows.Forms.ToolStripMenuItem itemSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblFit;
        private System.Windows.Forms.ComboBox cmbY;
        private System.Windows.Forms.ComboBox cmbX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Panel ShowCurvePnl;
        private System.Windows.Forms.GroupBox grbPara;
        private System.Windows.Forms.RichTextBox rtbPara;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn concentration;
        private System.Windows.Forms.DataGridViewTextBoxColumn Responsevalue;
        private System.Windows.Forms.DataGridViewTextBoxColumn countConc;
        private System.Windows.Forms.ComboBox cmbFit;
        private System.Windows.Forms.RadioButton rbtnConcToPmt;
        private System.Windows.Forms.RadioButton rbtnPmtToConc;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.RichTextBox rtbTest;
        private System.Windows.Forms.TextBox tbTest;
        private System.Windows.Forms.Button button1;
    }
}


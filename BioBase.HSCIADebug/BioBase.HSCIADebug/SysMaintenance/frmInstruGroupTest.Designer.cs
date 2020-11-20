namespace BioBase.HSCIADebug.SysMaintenance
{
    partial class frmInstruGroupTest
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numIPos = new System.Windows.Forms.NumericUpDown();
            this.btnEnd = new BioBase.HSCIADebug.CustomControl.FunctionButton(this.components);
            this.btnStart = new BioBase.HSCIADebug.CustomControl.FunctionButton(this.components);
            this.numTubeCount = new System.Windows.Forms.NumericUpDown();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.numDwPourin = new System.Windows.Forms.NumericUpDown();
            this.cbCleanTray = new System.Windows.Forms.CheckBox();
            this.ChkSubPourIn = new System.Windows.Forms.CheckBox();
            this.chkClearTray = new System.Windows.Forms.CheckBox();
            this.chkDelay = new System.Windows.Forms.CheckBox();
            this.ChkClearWash = new System.Windows.Forms.CheckBox();
            this.ChkLossTube = new System.Windows.Forms.CheckBox();
            this.ChkAddTube = new System.Windows.Forms.CheckBox();
            this.ChkRead = new System.Windows.Forms.CheckBox();
            this.chkWash = new System.Windows.Forms.CheckBox();
            this.ChkAddSub = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.numReadCount = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnReturn = new BioBase.HSCIADebug.CustomControl.FunctionButton(this.components);
            this.functionButton1 = new BioBase.HSCIADebug.CustomControl.FunctionButton(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numRepeat = new System.Windows.Forms.NumericUpDown();
            this.btnPourInto = new BioBase.HSCIADebug.CustomControl.FunctionButton(this.components);
            this.cmbSelectAct = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkNewTube = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTubeCount)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDwPourin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadCount)).BeginInit();
            this.pnlSidebar.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRepeat)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.numIPos);
            this.groupBox5.Controls.Add(this.btnEnd);
            this.groupBox5.Controls.Add(this.btnStart);
            this.groupBox5.Controls.Add(this.numTubeCount);
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.numReadCount);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(31, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(360, 452);
            this.groupBox5.TabIndex = 77;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "整机测试";
            // 
            // numIPos
            // 
            this.numIPos.Font = new System.Drawing.Font("宋体", 11.25F);
            this.numIPos.Location = new System.Drawing.Point(90, 16);
            this.numIPos.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numIPos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIPos.Name = "numIPos";
            this.numIPos.Size = new System.Drawing.Size(82, 25);
            this.numIPos.TabIndex = 79;
            this.numIPos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnEnd
            // 
            this.btnEnd.BackgroundImage = global::BioBase.HSCIADebug.Properties.Resources.主界面按钮_;
            this.btnEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEnd.EnabledSet = true;
            this.btnEnd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEnd.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnEnd.Location = new System.Drawing.Point(184, 416);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(86, 24);
            this.btnEnd.TabIndex = 78;
            this.btnEnd.Text = "终止";
            this.btnEnd.UseVisualStyleBackColor = false;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackgroundImage = global::BioBase.HSCIADebug.Properties.Resources.主界面按钮_;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStart.EnabledSet = true;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnStart.Location = new System.Drawing.Point(36, 416);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(86, 24);
            this.btnStart.TabIndex = 77;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // numTubeCount
            // 
            this.numTubeCount.Font = new System.Drawing.Font("宋体", 11.25F);
            this.numTubeCount.Location = new System.Drawing.Point(90, 43);
            this.numTubeCount.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numTubeCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTubeCount.Name = "numTubeCount";
            this.numTubeCount.Size = new System.Drawing.Size(82, 25);
            this.numTubeCount.TabIndex = 60;
            this.numTubeCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.numDwPourin);
            this.groupBox7.Controls.Add(this.cbCleanTray);
            this.groupBox7.Controls.Add(this.ChkSubPourIn);
            this.groupBox7.Controls.Add(this.chkClearTray);
            this.groupBox7.Controls.Add(this.chkDelay);
            this.groupBox7.Controls.Add(this.ChkClearWash);
            this.groupBox7.Controls.Add(this.ChkLossTube);
            this.groupBox7.Controls.Add(this.ChkAddTube);
            this.groupBox7.Controls.Add(this.ChkRead);
            this.groupBox7.Controls.Add(this.chkWash);
            this.groupBox7.Controls.Add(this.ChkAddSub);
            this.groupBox7.Font = new System.Drawing.Font("宋体", 11.25F);
            this.groupBox7.Location = new System.Drawing.Point(22, 98);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(287, 314);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "流程";
            // 
            // numDwPourin
            // 
            this.numDwPourin.Location = new System.Drawing.Point(95, 87);
            this.numDwPourin.Name = "numDwPourin";
            this.numDwPourin.Size = new System.Drawing.Size(48, 25);
            this.numDwPourin.TabIndex = 76;
            this.numDwPourin.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // cbCleanTray
            // 
            this.cbCleanTray.AutoSize = true;
            this.cbCleanTray.Checked = true;
            this.cbCleanTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCleanTray.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbCleanTray.Location = new System.Drawing.Point(14, 288);
            this.cbCleanTray.Name = "cbCleanTray";
            this.cbCleanTray.Size = new System.Drawing.Size(101, 19);
            this.cbCleanTray.TabIndex = 70;
            this.cbCleanTray.Text = "清洗盘清洗";
            this.cbCleanTray.UseVisualStyleBackColor = true;
            // 
            // ChkSubPourIn
            // 
            this.ChkSubPourIn.AutoSize = true;
            this.ChkSubPourIn.Checked = true;
            this.ChkSubPourIn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSubPourIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChkSubPourIn.Location = new System.Drawing.Point(14, 90);
            this.ChkSubPourIn.Name = "ChkSubPourIn";
            this.ChkSubPourIn.Size = new System.Drawing.Size(86, 19);
            this.ChkSubPourIn.TabIndex = 69;
            this.ChkSubPourIn.Text = "底物灌注";
            this.ChkSubPourIn.UseVisualStyleBackColor = true;
            // 
            // chkClearTray
            // 
            this.chkClearTray.AutoSize = true;
            this.chkClearTray.Checked = true;
            this.chkClearTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClearTray.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkClearTray.Location = new System.Drawing.Point(14, 24);
            this.chkClearTray.Name = "chkClearTray";
            this.chkClearTray.Size = new System.Drawing.Size(101, 19);
            this.chkClearTray.TabIndex = 68;
            this.chkClearTray.Text = "清空温育盘";
            this.chkClearTray.UseVisualStyleBackColor = true;
            // 
            // chkDelay
            // 
            this.chkDelay.AutoSize = true;
            this.chkDelay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.chkDelay.ForeColor = System.Drawing.Color.Black;
            this.chkDelay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkDelay.Location = new System.Drawing.Point(142, 25);
            this.chkDelay.Name = "chkDelay";
            this.chkDelay.Size = new System.Drawing.Size(76, 16);
            this.chkDelay.TabIndex = 67;
            this.chkDelay.Text = "旋转延迟";
            this.chkDelay.UseVisualStyleBackColor = true;
            // 
            // ChkClearWash
            // 
            this.ChkClearWash.AutoSize = true;
            this.ChkClearWash.Checked = true;
            this.ChkClearWash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkClearWash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChkClearWash.Location = new System.Drawing.Point(14, 57);
            this.ChkClearWash.Name = "ChkClearWash";
            this.ChkClearWash.Size = new System.Drawing.Size(101, 19);
            this.ChkClearWash.TabIndex = 66;
            this.ChkClearWash.Text = "清空清洗盘";
            this.ChkClearWash.UseVisualStyleBackColor = true;
            // 
            // ChkLossTube
            // 
            this.ChkLossTube.AutoSize = true;
            this.ChkLossTube.Checked = true;
            this.ChkLossTube.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkLossTube.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChkLossTube.Location = new System.Drawing.Point(14, 255);
            this.ChkLossTube.Name = "ChkLossTube";
            this.ChkLossTube.Size = new System.Drawing.Size(56, 19);
            this.ChkLossTube.TabIndex = 4;
            this.ChkLossTube.Text = "扔管";
            this.ChkLossTube.UseVisualStyleBackColor = true;
            // 
            // ChkAddTube
            // 
            this.ChkAddTube.AutoSize = true;
            this.ChkAddTube.Checked = true;
            this.ChkAddTube.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkAddTube.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChkAddTube.Location = new System.Drawing.Point(14, 123);
            this.ChkAddTube.Name = "ChkAddTube";
            this.ChkAddTube.Size = new System.Drawing.Size(71, 19);
            this.ChkAddTube.TabIndex = 0;
            this.ChkAddTube.Text = "夹新管";
            this.ChkAddTube.UseVisualStyleBackColor = true;
            // 
            // ChkRead
            // 
            this.ChkRead.AutoSize = true;
            this.ChkRead.Checked = true;
            this.ChkRead.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkRead.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChkRead.Location = new System.Drawing.Point(14, 222);
            this.ChkRead.Name = "ChkRead";
            this.ChkRead.Size = new System.Drawing.Size(56, 19);
            this.ChkRead.TabIndex = 3;
            this.ChkRead.Text = "读数";
            this.ChkRead.UseVisualStyleBackColor = true;
            // 
            // chkWash
            // 
            this.chkWash.AutoSize = true;
            this.chkWash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkWash.Location = new System.Drawing.Point(14, 156);
            this.chkWash.Name = "chkWash";
            this.chkWash.Size = new System.Drawing.Size(56, 19);
            this.chkWash.TabIndex = 1;
            this.chkWash.Text = "清洗";
            this.chkWash.UseVisualStyleBackColor = true;
            // 
            // ChkAddSub
            // 
            this.ChkAddSub.AutoSize = true;
            this.ChkAddSub.Checked = true;
            this.ChkAddSub.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkAddSub.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChkAddSub.Location = new System.Drawing.Point(14, 189);
            this.ChkAddSub.Name = "ChkAddSub";
            this.ChkAddSub.Size = new System.Drawing.Size(56, 19);
            this.ChkAddSub.TabIndex = 2;
            this.ChkAddSub.Text = "底物";
            this.ChkAddSub.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 11.25F);
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(22, 21);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(67, 15);
            this.label20.TabIndex = 6;
            this.label20.Text = "起始孔位";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 11.25F);
            this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label21.Location = new System.Drawing.Point(22, 48);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(52, 15);
            this.label21.TabIndex = 8;
            this.label21.Text = "管数量";
            // 
            // numReadCount
            // 
            this.numReadCount.Font = new System.Drawing.Font("宋体", 11.25F);
            this.numReadCount.Location = new System.Drawing.Point(90, 71);
            this.numReadCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numReadCount.Name = "numReadCount";
            this.numReadCount.Size = new System.Drawing.Size(48, 25);
            this.numReadCount.TabIndex = 58;
            this.numReadCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 11.25F);
            this.label22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label22.Location = new System.Drawing.Point(22, 75);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(67, 15);
            this.label22.TabIndex = 59;
            this.label22.Text = "重复读数";
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.Controls.Add(this.btnReturn);
            this.pnlSidebar.Controls.Add(this.functionButton1);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSidebar.Location = new System.Drawing.Point(873, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(151, 547);
            this.pnlSidebar.TabIndex = 78;
            // 
            // btnReturn
            // 
            this.btnReturn.BackgroundImage = global::BioBase.HSCIADebug.Properties.Resources.主界面按钮_;
            this.btnReturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReturn.EnabledSet = true;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReturn.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnReturn.Location = new System.Drawing.Point(11, 387);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(113, 54);
            this.btnReturn.TabIndex = 5;
            this.btnReturn.Text = "返回";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // functionButton1
            // 
            this.functionButton1.BackgroundImage = global::BioBase.HSCIADebug.Properties.Resources.灰显1;
            this.functionButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.functionButton1.Enabled = false;
            this.functionButton1.EnabledSet = true;
            this.functionButton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.functionButton1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.functionButton1.Location = new System.Drawing.Point(11, 93);
            this.functionButton1.Name = "functionButton1";
            this.functionButton1.Size = new System.Drawing.Size(113, 54);
            this.functionButton1.TabIndex = 1;
            this.functionButton1.Text = "组合测试";
            this.functionButton1.UseVisualStyleBackColor = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.numRepeat);
            this.groupBox6.Controls.Add(this.btnPourInto);
            this.groupBox6.Controls.Add(this.cmbSelectAct);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.checkNewTube);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.Location = new System.Drawing.Point(438, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(379, 141);
            this.groupBox6.TabIndex = 79;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "清洗灌注";
            // 
            // numRepeat
            // 
            this.numRepeat.Font = new System.Drawing.Font("宋体", 11.25F);
            this.numRepeat.Location = new System.Drawing.Point(105, 56);
            this.numRepeat.Name = "numRepeat";
            this.numRepeat.Size = new System.Drawing.Size(48, 25);
            this.numRepeat.TabIndex = 66;
            this.numRepeat.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnPourInto
            // 
            this.btnPourInto.BackgroundImage = global::BioBase.HSCIADebug.Properties.Resources.主界面按钮_;
            this.btnPourInto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPourInto.EnabledSet = true;
            this.btnPourInto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPourInto.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnPourInto.Location = new System.Drawing.Point(158, 98);
            this.btnPourInto.Name = "btnPourInto";
            this.btnPourInto.Size = new System.Drawing.Size(86, 24);
            this.btnPourInto.TabIndex = 79;
            this.btnPourInto.Text = "执行";
            this.btnPourInto.UseVisualStyleBackColor = false;
            this.btnPourInto.Click += new System.EventHandler(this.btnPourInto_Click);
            // 
            // cmbSelectAct
            // 
            this.cmbSelectAct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectAct.Font = new System.Drawing.Font("宋体", 11.25F);
            this.cmbSelectAct.FormattingEnabled = true;
            this.cmbSelectAct.Items.AddRange(new object[] {
            "磁珠清洗液灌注",
            "加样针清洗液灌注",
            "加试剂针清洗液灌注",
            "底物灌注",
            "循环灌注"});
            this.cmbSelectAct.Location = new System.Drawing.Point(105, 20);
            this.cmbSelectAct.Name = "cmbSelectAct";
            this.cmbSelectAct.Size = new System.Drawing.Size(83, 23);
            this.cmbSelectAct.TabIndex = 66;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 11.25F);
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(35, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 67;
            this.label8.Text = "重复次数";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11.25F);
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(35, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 77;
            this.label7.Text = "功能选择";
            // 
            // checkNewTube
            // 
            this.checkNewTube.AutoSize = true;
            this.checkNewTube.Checked = true;
            this.checkNewTube.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkNewTube.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.checkNewTube.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkNewTube.Location = new System.Drawing.Point(213, 23);
            this.checkNewTube.Name = "checkNewTube";
            this.checkNewTube.Size = new System.Drawing.Size(63, 16);
            this.checkNewTube.TabIndex = 71;
            this.checkNewTube.Text = "夹新管";
            this.checkNewTube.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label23.Location = new System.Drawing.Point(438, 161);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(65, 12);
            this.label23.TabIndex = 81;
            this.label23.Text = "提示信息：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(438, 182);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(379, 282);
            this.textBox1.TabIndex = 80;
            // 
            // frmInstruGroupTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 547);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.groupBox5);
            this.Name = "frmInstruGroupTest";
            this.Text = "frmInstruGroupTest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInstruGroupTest_Load);
            this.SizeChanged += new System.EventHandler(this.frmInstruGroupTest_SizeChanged);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTubeCount)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDwPourin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadCount)).EndInit();
            this.pnlSidebar.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRepeat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numTubeCount;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown numDwPourin;
        private System.Windows.Forms.CheckBox cbCleanTray;
        private System.Windows.Forms.CheckBox ChkSubPourIn;
        private System.Windows.Forms.CheckBox chkClearTray;
        private System.Windows.Forms.CheckBox chkDelay;
        private System.Windows.Forms.CheckBox ChkClearWash;
        private System.Windows.Forms.CheckBox ChkLossTube;
        private System.Windows.Forms.CheckBox ChkAddTube;
        private System.Windows.Forms.CheckBox ChkRead;
        private System.Windows.Forms.CheckBox chkWash;
        private System.Windows.Forms.CheckBox ChkAddSub;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown numReadCount;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel pnlSidebar;
        private CustomControl.FunctionButton btnReturn;
        private CustomControl.FunctionButton functionButton1;
        private CustomControl.FunctionButton btnEnd;
        private CustomControl.FunctionButton btnStart;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown numRepeat;
        private CustomControl.FunctionButton btnPourInto;
        private System.Windows.Forms.ComboBox cmbSelectAct;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkNewTube;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numIPos;
    }
}
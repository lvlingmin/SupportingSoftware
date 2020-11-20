using EBarv0._2.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EBarv0._2
{
    public partial class FrmProject2 : frmParent
    {
        List<string> Slist = new List<string>();
        DataTable dt = new DataTable();
        DataTable dtStep;
        DataTable dtTemp = new DataTable();
        DataTable dtProject = new DataTable();
        bool closedFlag = false;
        public FrmProject2()
        {
            InitializeComponent();
        }

        private void FrmProject2_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//禁止最大化
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;//禁止进行拖拽
            
            //流程展示增加头
            dtStep = new DataTable();
            dtStep.Columns.Add("步骤", typeof(string));
            dtStep.Columns.Add("参数", typeof(string));
            dtStep.Columns.Add("单位", typeof(string));
            dgvProdure.DataSource = dtStep;
            //设置只读
            dgvProdure.Columns[0].ReadOnly = true;
            dgvProdure.Columns[2].ReadOnly = true;

            //提取数据项目信息，并加载到combobox中
            DbHelperOleDb db = new DbHelperOleDb(0);
            string sql = "select ProjectNumber ,ShortName,ProjectProcedure from tbProject";
            dt = DbHelperOleDb.Query(0,sql).Tables[0];
            this.cmbProject.SelectedIndexChanged -= new System.EventHandler(this.CmbProject_SelectedIndexChanged);
            cmbProject.DataSource = dt;
            cmbProject.DisplayMember = "ShortName";
            cmbProject.ValueMember = "ProjectNumber";
            this.cmbProject.SelectedIndexChanged += new System.EventHandler(this.CmbProject_SelectedIndexChanged);
            cmbProject.SelectedIndex = -1;

            //设置dt显示格式
            dtProject.Columns.Add("原始编号", typeof(string));
            dtProject.Columns.Add("密文", typeof(string));

            this.DGV1.DataSource = dtProject;
            this.DGV1.Columns[0].Width = 180;
            this.DGV1.Columns[1].Width = 180;
            this.DGV1.RowHeadersVisible = false;

        }


        private void CmbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtStep.Rows.Count > 0)
                dtStep.Rows.Clear();
            if (cmbProject.SelectedIndex == -1)
                return;
            DbHelperOleDb db = new DbHelperOleDb(0);
            string sql = "select ProjectProcedure from tbProject where ShortName = '" + cmbProject.Text.Trim() + "'";

            string[] Procedure;
            try
            {
                DataTable dtProcedure = DbHelperOleDb.Query(0,sql).Tables[0];
                Procedure = dtProcedure.Rows[0][0].ToString().Split(';');
                //数据库存在当前项目
                if (Procedure.Length > 0)
                {
                    foreach (var proce in Procedure)
                    {
                        string[] steps = proce.Split('-');
                        dtStep.Rows.Add(steps[0], steps[1], steps[2]);
                    }
                }
                else
                { }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据库导出项目信息失败，请查看数据库项目流程格式是否正确\n" + ex.Message);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            closedFlag = true;
            FrmMain.f0.Show();
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dtProject.Rows.Count < 1 || dgvProdure.Rows.Count < 1)
            {
                MessageBox.Show("请生成编码后再进行保存。","温馨提示");
                return;
            }
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择保存文件夹";
            DialogResult flag = dialog.ShowDialog();
            if (flag == DialogResult.OK || flag == DialogResult.Yes)
            {
                string fPath = dialog.SelectedPath;
                //保存一维码
                Utils.instance.saveImage(dtProject, fPath, 2);
            }
            ////保存一维码
            //Utils.instance.saveImage(dtProject, "项目");
        }

        private void BtnGener_Click(object sender, EventArgs e)
        {
            if (dgvProdure.Rows.Count < 1)
            {
                MessageBox.Show("请先选择项目。", "温馨提示");
                return;
            }

            Utils.instance.clearShowData(panel1, dtProject);
            Slist.Clear();

            #region
            //数据库中查询项目编号
            DbHelperOleDb db = new DbHelperOleDb(0);
            string sql = "select ProjectNumber from tbProject where ShortName = '" + cmbProject.Text.Trim() + "'";
            string projectNumber = DbHelperOleDb.Query(0,sql).Tables[0].Rows[0][0].ToString();
            while (projectNumber.Length < 3)
            {
                projectNumber = projectNumber.Insert(0, "0");
            }
            Slist.Add("2");
            Slist.Add(projectNumber);

            #region 流程
            //f表示不修改实验流程，t表示修改实验流程
            Slist.Add("f");
            #region 生成过程
            //遍历dgvProdure数据，并将数据存储生成实验流程
            for (int i = 0; i < dgvProdure.Rows.Count; i++)
            {
                string tempStep = dgvProdure.Rows[i].Cells[0].Value.ToString();
                string temPara = dgvProdure.Rows[i].Cells[1].Value.ToString();
                int tempIntPara;
                string tempResult = "";
                string unit = dgvProdure.Rows[i].Cells[2].Value.ToString(); ;
                //有关每个步骤的具体占据位置和长度已经在文档中具体说明
                switch (tempStep)
                {
                    case "S":
                        tempIntPara = int.Parse(temPara);
                        tempIntPara = tempIntPara / 5;
                        tempResult = tempIntPara.ToString();
                        while (tempResult.Length < 2)
                        {
                            tempResult = tempResult.Insert(0, "0");
                        }
                        Slist.Add(tempResult);
                        break;
                    case "R1":
                        tempIntPara = int.Parse(temPara);
                        tempIntPara = tempIntPara / 10;
                        tempResult = tempIntPara.ToString();
                        while (tempResult.Length < 2)
                        {
                            tempResult = tempResult.Insert(0, "0");
                        }
                        Slist.Add(tempResult);
                        break;
                    case "R2":
                        tempIntPara = int.Parse(temPara);
                        tempIntPara = tempIntPara / 10;
                        tempResult = tempIntPara.ToString();
                        while (tempResult.Length < 2)
                        {
                            tempResult = tempResult.Insert(0, "0");
                        }
                        Slist.Add(tempResult);
                        break;
                    case "R3":
                        tempIntPara = int.Parse(temPara);
                        tempIntPara = tempIntPara / 10;
                        tempResult = tempIntPara.ToString();
                        while (tempResult.Length < 2)
                        {
                            tempResult = tempResult.Insert(0, "0");
                        }
                        Slist.Add(tempResult);
                        break;
                    case "R4":
                        tempIntPara = int.Parse(temPara);
                        tempIntPara = tempIntPara / 10;
                        tempResult = tempIntPara.ToString();
                        while (tempResult.Length < 2)
                        {
                            tempResult = tempResult.Insert(0, "0");
                        }
                        Slist.Add(tempResult);
                        break;
                    case "H":
                        if (unit == "min")
                        {
                            tempIntPara = int.Parse(temPara);
                            tempIntPara *= 60;
                            tempResult = Convert.ToString(tempIntPara, 16);
                            while (tempResult.Length < 3)
                            {
                                tempResult = tempResult.Insert(0, "0");
                            }
                        }
                        else if (unit == "s")
                        {
                            tempIntPara = int.Parse(temPara);
                            tempResult = Convert.ToString(tempIntPara, 16);
                            while (tempResult.Length < 3)
                            {
                                tempResult = tempResult.Insert(0, "0");
                            }
                        }
                        else
                        {
                            MessageBox.Show("只允许单位为min（分钟）和s（秒）");
                            return;
                        }
                        Slist.Add(tempResult);
                        //tempIntPara = int.Parse(temPara);
                        //tempIntPara = tempIntPara / 100;
                        //tempResult = tempIntPara.ToString();
                        //while (tempResult.Length < 2)
                        //{
                        //    tempResult = tempResult.Insert(0, "0");
                        //}
                        //Slist.Add(tempResult);
                        break;
                    case "B":
                        tempIntPara = int.Parse(temPara);
                        tempIntPara = tempIntPara / 10;
                        tempResult = tempIntPara.ToString();
                        Slist.Add(tempResult);
                        break;
                    case "W":
                        tempIntPara = int.Parse(temPara);
                        tempIntPara = tempIntPara / 100;
                        tempResult = tempIntPara.ToString();
                        Slist.Add(tempResult);
                        break;
                    case "T":
                        tempIntPara = int.Parse(temPara);
                        tempIntPara = tempIntPara / 100;
                        tempResult = tempIntPara.ToString();
                        Slist.Add(tempResult);
                        break;
                    case "D":
                        tempIntPara = int.Parse(temPara);
                        tempIntPara = tempIntPara / 10;
                        tempResult = tempIntPara.ToString();
                        Slist.Add(tempResult);
                        break;
                }
            }
            #endregion

            #endregion
            StringBuilder sb = new StringBuilder();
            foreach (var para in Slist)
            {
                sb.Append(para);
            }
            #endregion
            //MessageBox.Show(sb.ToString());
            dtProject.Rows.Add(sb.ToString(), Utils.instance.ToEncryption(sb.ToString()));//dt 0列存储明文 1列存储加密
            Utils.instance.ToMadePictureFromdt(panel1, dtProject);
            //MessageBox.Show("1:"+Utils.instance.ToDecryption(Utils.instance.ToEncryption(sb.ToString())));
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            dtProject.Clear();
            panel1.Controls.Clear();
            dtStep.Clear();
            cmbProject.SelectedIndex = -1;
        }

        private void FrmProject2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!closedFlag)
            {
                FrmMain.f0.Close();
                System.Environment.Exit(0);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZXing.Common;
using ZXing;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using System.Reflection;
using EBarv0._2.DBUtility;

namespace EBarv0._2
{
    public partial class FrmProject : Form
    {
        List<string> Slist = new List<string>();
        
        public FrmProject()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        DataTable dtStep;
        DataTable dtTemp = new DataTable();
        DataTable dtProject = new DataTable();
        private void FrmProject_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//禁止最大化
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;//禁止进行拖拽
            //初始化设置增加步骤及上下移按钮不可用
            foreach(var ctr in GRBAddProdure.Controls)
            {
                ((Button)ctr).Enabled = false;
            }
            foreach (var ctr in GRBLocaOrData.Controls) 
            {
                ((Button)ctr).Enabled = false;
            }
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
            this.cmbProject.SelectedIndexChanged -= new System.EventHandler(this.cmbProject_SelectedIndexChanged);        
            cmbProject.DataSource = dt;
            cmbProject.DisplayMember = "ShortName";
            cmbProject.ValueMember = "ProjectNumber";
            this.cmbProject.SelectedIndexChanged += new System.EventHandler(this.cmbProject_SelectedIndexChanged);  
      
            //默认选中仅修改参数
            rbtnOnlyPara.Checked = true;

            //设置dt显示格式
            dtProject.Columns.Add("原始编号", typeof(string));
            dtProject.Columns.Add("密文", typeof(string));

            this.DGV1.DataSource = dtProject;
            this.DGV1.Columns[0].Width = 180;
            this.DGV1.Columns[1].Width = 180;
            this.DGV1.RowHeadersVisible = false;

            //增加实验暂时不可用
            this.rbtnAddProject.Enabled = false;
        }

        private void sample_Click(object sender, EventArgs e)
        {
            dtStep.Rows.Add("S","","ml");
        }

        private void R1_Click(object sender, EventArgs e)
        {
            dtStep.Rows.Add("R1", "1", "ml");
        }

        private void R2_Click(object sender, EventArgs e)
        {
            dtStep.Rows.Add("R2", "", "ml");
        }

        private void R3_Click(object sender, EventArgs e)
        {
            dtStep.Rows.Add("R3", "", "ml");
        }

        private void R4_Click(object sender, EventArgs e)
        {
            dtStep.Rows.Add("R4", "", "ml");
        }

        private void H_Click(object sender, EventArgs e)
        {
            dtStep.Rows.Add("H", "", "min");
        }

        private void B_Click(object sender, EventArgs e)
        {
            dtStep.Rows.Add( "B", "", "ml");
        }

        private void W_Click(object sender, EventArgs e)
        {
            dtStep.Rows.Add( "W", "", "ml");
        }

        private void T_Click(object sender, EventArgs e)
        {
            dtStep.Rows.Add( "T", "", "ul");
        }

        private void D_Click(object sender, EventArgs e)
        {
            dtStep.Rows.Add( "D", "", "s");
        }

        private void up_Click(object sender, EventArgs e)//选中行上移
        {
            if (dgvProdure.SelectedRows.Count < 1) 
            {
                MessageBox.Show("没有选中数据！");
                return;
            }
            int index = dgvProdure.SelectedRows[0].Index;

            if (index <= 0)
            {
                return;
            }
            else
            {
                List<string> tempList = new List<string>();
                for (int i = 0; i < dgvProdure.Columns.Count; i++)
                {
                    tempList.Add(dgvProdure.Rows[index].Cells[i].Value.ToString());
                }
                for (int i = 0; i < dgvProdure.Columns.Count; i++)
                {
                    dgvProdure.Rows[index].Cells[i].Value = dgvProdure.Rows[index-1].Cells[i].Value;
                    dgvProdure.Rows[index-1].Cells[i].Value = tempList[i];
                }
            }
            dgvProdure.ClearSelection();

            dgvProdure.Rows[index].Selected = false;
            dgvProdure.Rows[index - 1].Selected = true;
        }

        private void down_Click(object sender, EventArgs e)//选中行下移
        {
            if (dgvProdure.SelectedRows.Count < 1)
            {
                MessageBox.Show("没有选中数据！");
                return;
            }
            int index = dgvProdure.SelectedRows[0].Index;

            if (index >= dgvProdure.Rows.Count - 1)
            {
                return;
            }
            else
            {
                List<string> tempList = new List<string>();
                for (int i = 0; i < dgvProdure.Columns.Count; i++)
                {
                    tempList.Add(dgvProdure.Rows[index].Cells[i].Value.ToString());
                }
                for (int i = 0; i < dgvProdure.Columns.Count; i++)
                {
                    dgvProdure.Rows[index].Cells[i].Value = dgvProdure.Rows[index + 1].Cells[i].Value;
                    dgvProdure.Rows[index + 1].Cells[i].Value = tempList[i];
                }
            }
            dgvProdure.ClearSelection();
            dgvProdure.Rows[index + 1].Selected = true;
            dgvProdure.Rows[index].Selected = false;
        }

        private void delete_Click(object sender, EventArgs e)//选中行删除
        {
            if (dgvProdure.SelectedRows.Count < 1)
            {
                MessageBox.Show("没有选中数据！");
                return;
            }
            int index = dgvProdure.SelectedRows[0].Index;
            dgvProdure.Rows.RemoveAt(index);
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);

        }

        private void r(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)//返回
        {
            FrmMain f = new FrmMain();
            f.Show();
            this.Close();
        }

        private void btnGener_Click(object sender, EventArgs e)//生成编码
        {
            Utils.instance.clearShowData(panel1,dtProject);
            Slist.Clear();
            if (rbtnOnlyPara.Checked == false && rbtnProAndPara.Checked == false && rbtnAddProject.Checked == false) 
            {
                MessageBox.Show("必须选择一种修改方式");
                return;
            }
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
            if (rbtnOnlyPara.Checked == true) //仅修改参数
            {
                //f表示不修改实验流程，t表示修改实验流程
                Slist.Add("f");
                #region 生成过程
                //遍历dgvProdure数据，并将数据存储生成实验流程
                for (int i = 0; i < dgvProdure.Rows.Count; i++)
                {
                    string tempStep = dgvProdure.Rows[i].Cells[0].Value.ToString();
                    string temPara = dgvProdure.Rows[i].Cells[1].Value.ToString();
                    int tempIntPara;
                    string tempResult =""; 
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
                                tempResult = tempResult.Insert(0,"0");
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
               
            }
            else if (rbtnProAndPara.Checked == true) //修改参数和实验流程
            {
                Slist.Add("t");
                #region
                for (int i = 0; i < dgvProdure.Rows.Count; i++)
                {
                    string tempStep = dgvProdure.Rows[i].Cells[0].Value.ToString();
                    string temPara = dgvProdure.Rows[i].Cells[1].Value.ToString();
                    int tempIntPara;
                    string tempResult = "";
                    string unit = dgvProdure.Rows[i].Cells[2].Value.ToString();
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
                            Slist.Add("S");
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
                            Slist.Add("R");
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
                            Slist.Add("r");
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
                            Slist.Add("E");
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
                            Slist.Add("e");
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
                            Slist.Add("H");
                            Slist.Add(tempResult);
                            break;
                        case "B":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 10;
                            tempResult = tempIntPara.ToString();
                            Slist.Add("B");
                            Slist.Add(tempResult);
                            break;
                        case "W":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 100;
                            tempResult = tempIntPara.ToString();
                            Slist.Add("W");
                            Slist.Add(tempResult);
                            break;
                        case "T":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 100;
                            tempResult = tempIntPara.ToString();
                            Slist.Add("T");
                            Slist.Add(tempResult);
                            break;
                        case "D":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 10;
                            tempResult = tempIntPara.ToString();
                            Slist.Add("D");
                            Slist.Add(tempResult);
                            break;
                    }
                }
                #endregion
            }
            else if (rbtnAddProject.Checked == true) //增加新的实验
            {
                //增加一个新的项目暂时不能实现，因为需要更新的太多
            }
            #endregion
            StringBuilder sb = new StringBuilder();
            foreach(var para in Slist)
            {
                sb.Append(para);
            }
            #endregion
            //MessageBox.Show(sb.ToString());
            dtProject.Rows.Add(sb.ToString(),Utils.instance.ToEncryption(sb.ToString()));//dt 0列存储明文 1列存储加密
            Utils.instance.ToMadePictureFromdt(panel1, dtProject);
            //MessageBox.Show("1:"+Utils.instance.ToDecryption(Utils.instance.ToEncryption(sb.ToString())));
        }


        private Bitmap ToWriteBitPicture(string num)//根据文本内容，生成相应的一维码
        {
            EncodingOptions opt = new EncodingOptions();//一维码选项-尺寸
            opt.Height = 80;
            opt.Width = 300;

            ZXing.BarcodeFormat format = new BarcodeFormat();//一维码格式
            format = BarcodeFormat.CODE_128 ;//判断选用的一维码格式

            ZXing.BarcodeWriter writer = new BarcodeWriter();//一维码绘制对象
            writer.Options = opt;
            writer.Format = format;
            return writer.Write(num);
        }


        private void cmbProject_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (dtStep.Rows.Count > 0)
                dtStep.Rows.Clear();
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
                MessageBox.Show("数据库导出项目信息失败，请查看数据库项目流程格式是否正确\n"+ex.Message);
            }       
            
        }

        private void rbtnProAndPara_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnProAndPara.Checked == true) 
            {
                //初始化设置增加步骤及上下移按钮可用
                foreach (var ctr in GRBAddProdure.Controls)
                {
                    ((Button)ctr).Enabled = true;
                }
                foreach (var ctr in GRBLocaOrData.Controls)
                {
                    ((Button)ctr).Enabled = true;
                }
            }
            if (rbtnProAndPara.Checked == false)
            {
                //初始化设置增加步骤及上下移按钮不可用
                foreach (var ctr in GRBAddProdure.Controls)
                {
                    ((Button)ctr).Enabled = false;
                }
                foreach (var ctr in GRBLocaOrData.Controls)
                {
                    ((Button)ctr).Enabled = false;
                }
            }
        }

        private void rbtnAddProject_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAddProject.Checked == true)
            {
                //初始化设置增加步骤及上下移等按钮不可用
                foreach (var ctr in GRBAddProdure.Controls)
                {
                    ((Button)ctr).Enabled = true;
                }
                foreach (var ctr in GRBLocaOrData.Controls)
                {
                    ((Button)ctr).Enabled = true;
                }
               //项目相关信息可见性
                //foreach (var ctr in grbProjectInfo.Controls)
                //{
                //    if (ctr is Label) 
                //    {
                //        ((Label)ctr).Visible = true;
                //    }
                //    if (ctr is TextBox)
                //    {
                //        ((TextBox)ctr).Visible = true;
                //    }
                //}
            }
            if (rbtnAddProject.Checked == false)
            {
                //初始化设置增加步骤及上下移等按钮不可用
                foreach (var ctr in GRBAddProdure.Controls)
                {
                    ((Button)ctr).Enabled = false;
                }
                foreach (var ctr in GRBLocaOrData.Controls)
                {
                    ((Button)ctr).Enabled = false;
                }
                
            }
        }

        private void rbtnOnlyPara_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnProAndPara.Checked == true)
            {
                //初始化设置增加步骤及上下移按钮可用
                foreach (var ctr in GRBAddProdure.Controls)
                {
                    ((Button)ctr).Enabled = true;
                }
                foreach (var ctr in GRBLocaOrData.Controls)
                {
                    ((Button)ctr).Enabled = true;
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dtProject.Rows.Count < 1)
            {
                MessageBox.Show("请先生成数据再进行保存！");
                return;

            }
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择保存文件夹";
            if (dialog.ShowDialog() == DialogResult.OK || dialog.ShowDialog() == DialogResult.Yes)
            {
                string fPath = dialog.SelectedPath;
                //保存一维码
                Utils.instance.saveImage(dtProject, fPath , 2);
            }
            ////保存一维码
            //Utils.instance.saveImage(dtProject, "项目");
        }
    }
}


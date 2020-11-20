using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace EBarv0._2
{
    public partial class FrmScaling : Form
    {
        /// <summary>
        /// 存放原始编码、中间码和密文的链表，根据需求可以关联DataGridView的链表以动态的显示在界面上
        /// </summary>
       
        //
        DataTable dtScaling = new DataTable();
        bool closedFlag = false;
        public FrmScaling()
        {
            InitializeComponent();
        }

        private void FrmScaling_Load(object sender, EventArgs e)
        {
            textBoxA.Focus();//加载界面后将焦点放在第一个textbox
            this.MaximizeBox = false;//禁止最大化
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;//禁止进行拖拽
            //默认用6点定标
            rbtn6Point.Checked = true;

            //设置dt显示格式
            dtScaling.Columns.Add("原始编号",typeof(string));
            dtScaling.Columns.Add("密文",typeof(string));
            //不允许修改
            dtScaling.Columns[0].ReadOnly = true;
            dtScaling.Columns[1].ReadOnly = true;

            this.DGV1.DataSource = dtScaling;
            this.DGV1.Columns[0].Width = 191;
            this.DGV1.Columns[1].Width = 191;
            this.DGV1.RowHeadersVisible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            closedFlag = true;
            FrmMain.f0.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var ctr in grbScaling.Controls) //对输入的数字进行正则匹配，确定是否符合要求
            {
                string text = ((TextBox)ctr).Text.Trim();
                
                //if (!(Regex.IsMatch(text, "^([0-9]{1,})$") || !Regex.IsMatch(text, "^([0-9]{1,}[.][0-9]*)$")))   //1位的0~9 或者1位的0~9 加小数点后任意位数
                

                //lyq add 20190810 判空
                if (rbtn6Point.Checked == true)
                {
                    if (((TextBox)ctr)  != textBoxG )
                    {
                        if (!((Regex.IsMatch(text, "^([0-9]{1,})$") || Regex.IsMatch(text, "^([0-9]{1,})+(.[0-9]{1,})$"))))
                        {
                            MessageBox.Show("输入字符不符合要求，请确认后重新输入。", "温馨提示");
                            ((TextBox)ctr).Text = "";
                            ((TextBox)ctr).Focus();
                            return;
                        }
                        if (text == "" || ((TextBox)ctr).Text.Length == 0)
                        {
                            MessageBox.Show("有输入字符为空，请确认后重新输入。", "温馨提示");
                            ((TextBox)ctr).Text = "";
                            ((TextBox)ctr).Focus();
                            return;
                        }
                    }                    
                }
                else
                {
                    if (!((Regex.IsMatch(text, "^([0-9]{1,})$") || Regex.IsMatch(text, "^([0-9]{1,})+(.[0-9]{1,})$"))))
                    {
                        MessageBox.Show("输入字符不符合要求，请确认后重新输入。", "温馨提示");
                        ((TextBox)ctr).Text = "";
                        ((TextBox)ctr).Focus();
                        return;
                    }
                    if (text == "" || ((TextBox)ctr).Text.Length == 0)
                    {
                        MessageBox.Show("有输入字符为空，请确认后重新输入。", "温馨提示");
                        ((TextBox)ctr).Text = "";
                        ((TextBox)ctr).Focus();
                        return;
                    }
                }

            }

        
            Utils.instance.clearShowData(panel1,dtScaling);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            //验证输入的数据是不是数
            foreach (Control ctr in groupBox1.Controls) 
            {
                //if (ctr is TextBox) 
                //{
                //    if (!(Regex.Match(ctr.Text.Trim(), @"^\d+(\.\d+)?$").Success))
                //    {
                //        MessageBox.Show("数据不符合要求，请再次确认！");
                //        return;
                //    }                   
                //}
            }
            #region 将界面数据加工
            //条码序号
            sb.Append("3");
            //生成定标数据和条码
            sb.Append("A");
            sb.Append(textBoxA.Text.Trim().ToString());
            sb.Append("f");

            sb.Append("B");
            sb.Append(textBoxB.Text.Trim().ToString());
            sb.Append("t");

            sb.Append("C");
            sb.Append(textBoxC.Text.Trim().ToString());
            sb.Append("f");

            sb.Append("D");
            sb.Append(textBoxD.Text.Trim().ToString());
            sb.Append("f");

            //MessageBox.Show(sb.ToString());
            //条码序号
            sb2.Append("4");
            sb2.Append("E");
            sb2.Append(textBoxE.Text.Trim().ToString());
            sb2.Append("t");

            sb2.Append("F");
            sb2.Append(textBoxF.Text.Trim().ToString());
            sb2.Append("f");

            if (rbtn7Point.Checked == true)
            {
                sb2.Append("G");
                sb2.Append(textBoxG.Text.Trim().ToString());
                sb2.Append("f");
            }
            #endregion
            //将明文和密码装进去
            string encry1 = Utils.instance.ToEncryption(sb.ToString());
            dtScaling.Rows.Add(sb.ToString(), encry1);
            //panel显示一维码

            string encry2 = Utils.instance.ToEncryption(sb2.ToString());
            dtScaling.Rows.Add(sb2.ToString(),encry2);            
            Utils.instance.ToMadePictureFromdt(panel1,dtScaling);
            //MessageBox.Show("1:" + Utils.instance.ToDecryption(encry1) + "\n2:" + Utils.instance.ToDecryption(encry2));
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (DGV1.Rows.Count < 1)
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
                Utils.instance.saveImage(dtScaling, fPath , 3); //lyq mod 20190821
            }
            ////保存一维码
            //Utils.instance.saveImage(dtScaling,"浓度");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void DGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Rbtn6Point_CheckedChanged(object sender, EventArgs e)
        {
            label5.Enabled = false;
            textBoxG.Text = "";
            textBoxG.Enabled = false;
           
        }

        private void Rbtn7Point_CheckedChanged(object sender, EventArgs e)
        {
            label5.Enabled = true;
            textBoxG.Enabled = true;            
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            textBoxA.Text = "";
            textBoxB.Text = "";
            textBoxC.Text = "";
            textBoxD.Text = "";
            textBoxE.Text = "";
            textBoxF.Text = "";
            textBoxG.Text = "";
            panel1.Controls.Clear();
            dtScaling.Clear();
        }

        private void GrbScaling_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void FrmScaling_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!closedFlag)
            {
                FrmMain.f0.Close();
                System.Environment.Exit(0);
            }
        }
    }
}

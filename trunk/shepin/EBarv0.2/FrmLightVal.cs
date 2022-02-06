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
using System.Numerics;

namespace EBarv0._2
{
    public partial class FrmLightVal : frmParent
    {
        /// <summary>
        /// 存放原始编码、中间码和密文的链表，根据需求可以关联DataGridView的链表以动态的显示在界面上
        /// </summary>
        DataTable dtLightVal = new DataTable();
        bool closedFlag = false;
        public FrmLightVal()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            closedFlag = true;
            FrmMain.f0.Show();
            this.Close();
        }

        private void FrmLightVal_Load(object sender, EventArgs e)
        {
            LightValueA.Focus();//焦点放在第一个TextBox
            this.MaximizeBox = false;//不允许最大化
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;//禁止进行拖拽

            //设置dt显示格式
            dtLightVal.Columns.Add("原始编号", typeof(string));
            dtLightVal.Columns.Add("密文", typeof(string));
            this.DGV1.DataSource = dtLightVal;
            this.DGV1.Columns[0].Width = 191;
            this.DGV1.Columns[1].Width = 191;
            this.DGV1.RowHeadersVisible = false;

            rbtn6Point.Checked = true;
        }

        private void btnGener_Click(object sender, EventArgs e)
        {
            foreach (var ctr in grbLight.Controls) //对输入的数字进行正则匹配，确定是否符合要求
            {
                string text = ((TextBox)ctr).Text.Trim();
                
                //if (!(Regex.IsMatch(text, "^([0-9]{1,})$")||!Regex.IsMatch(text,"^([0-9]{1,}[.][0-9]*)$"))) 
                //{
                //    MessageBox.Show("输入字符不符合要求，请重新输入！","温馨提示");
                //    ((TextBox)ctr).Text = "";
                //    ((TextBox)ctr).Focus();
                //    return;
                //}
                //lyq add 20190810 判空
                if (rbtn6Point.Checked == true)
                {                    
                    if (((TextBox)ctr) != textBoxG)
                    {
                        //if (!((Regex.IsMatch(text, "^([0-9]{1,})$") || Regex.IsMatch(text, "^([0-9]{1,})+(.[0-9]{1,})$"))))
                        //{
                        //    MessageBox.Show("输入字符不符合要求，请确认后重新输入！", "温馨提示");
                        //    ((TextBox)ctr).Text = "";
                        //    ((TextBox)ctr).Focus();
                        //    return;
                        //}
                        if (!(Regex.IsMatch(text, "^([1-9]{1}[0-9]{0,8})$"))) //只允许整数(最多9位)
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
                    //if (!((Regex.IsMatch(text, "^([0-9]{1,})$") || Regex.IsMatch(text, "^([0-9]{1,})+(.[0-9]{1,})$"))))
                    //{
                    //    MessageBox.Show("输入字符不符合要求，请确认后重新输入！", "温馨提示");
                    //    ((TextBox)ctr).Text = "";
                    //    ((TextBox)ctr).Focus();
                    //    return;
                    //}
                    if (!(Regex.IsMatch(text, "^([1-9]{1}[0-9]{0,8})$"))) //只允许整数(最多9位)
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
            Utils.instance.clearShowData(panel1,dtLightVal);
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder sb4 = new StringBuilder();
            
            sb1 = DecimalTo16("5",  LightValueA.Text.Trim(),"B",LightValueB.Text.Trim());
            sb2 = DecimalTo16("6",  LightValueC.Text.Trim(),  "D", LightValueD.Text.Trim());
            sb3 = DecimalTo16("7",  LightValueE.Text.Trim(), "F", LightValueF.Text.Trim());
            dtLightVal.Rows.Add(sb1.ToString(), Utils.instance.ToEncryption(sb1.ToString()));
            dtLightVal.Rows.Add(sb2.ToString(),Utils.instance.ToEncryption(sb2.ToString()));
            dtLightVal.Rows.Add(sb3.ToString(),Utils.instance.ToEncryption(sb3.ToString()));
            if (rbtn7Point.Checked == true)
            {
                //发光值的第一个条码
                sb4.Append(8);
                //取AB发光值并转换成16进制
                string[] array = textBoxG.Text.Trim().Split('.');
                //sb4.Append("G");
                if (array.Length > 1)
                {
                    //整数和小数部分分别转换成16进制并添加到StringBuilder中
                    sb4.Append(Convert.ToString(int.Parse(array[0]), 16));
                    sb4.Append(".");
                    sb4.Append(Convert.ToString(int.Parse(array[1]), 16));

                }
                else if (array.Length == 1)
                {
                    //只有整数部分
                    sb4.Append(Convert.ToString(int.Parse(array[0]), 16));
                }
                while (sb4.Length < 8)
                {
                    sb4.Insert(1,"0");
                }
                dtLightVal.Rows.Add(sb4.ToString(),Utils.instance.ToEncryption(sb4.ToString()));                
            }
            Utils.instance.ToMadePictureFromdt(panel1, dtLightVal);
            //string a1 = Utils.instance.ToDecryption(Utils.instance.ToEncryption(sb1.ToString()));
            //string a2 = Utils.instance.ToDecryption(Utils.instance.ToEncryption(sb2.ToString()));
            //string a3 = Utils.instance.ToDecryption(Utils.instance.ToEncryption(sb3.ToString()));
           
            //MessageBox.Show("------:" + a1 + "\n--------;" + a2
            //    + "\n-------;" + a3);
        }
        //抽象一个方法向StringBuilder里面添加经过转换的十六进制数字
        StringBuilder DecimalTo16(string index1,  string LightValue1,  string ValueLocation2, string LightValue2)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            if (LightValue1 != null && LightValue1 != "")
            {
                //发光值的第一个条码
                sb.Append(index1);
                //取AB发光值并转换成16进制
                string[] array1 = LightValue1.Split('.');
                //sb.Append(ValueLocation1);
                if (array1.Length > 1)
                {
                    //整数和小数部分分别转换成16进制并添加到StringBuilder中
                    sb.Append(Convert.ToString(int.Parse(array1[0]), 16));
                    sb.Append(".");
                    sb.Append(Convert.ToString(int.Parse(array1[1]), 16));

                }
                else if (array1.Length == 1)
                {
                    //只有整数部分
                    sb.Append(Convert.ToString(int.Parse(array1[0]), 16));
                }
                while (sb.Length < 8)
                {
                    sb.Insert(1,"0");
                }

                //取AB发光值并转换成16进制
                string[] array2 = LightValue2.Split('.');
                //sb2.Append(ValueLocation2);
                if (array2.Length > 1)
                {
                    //整数和小数部分分别转换成16进制并添加到StringBuilder中
                    sb2.Append(Convert.ToString(int.Parse(array2[0]), 16));
                    sb2.Append(".");
                    sb2.Append(Convert.ToString(int.Parse(array2[1]), 16));

                }
                else if (array2.Length == 1)
                {
                    //只有整数部分
                    sb2.Append(Convert.ToString(int.Parse(array2[0]), 16));
                }
                while (sb2.Length < 7)
                {
                    sb2.Insert(0, "0");
                }
            }
            else
            {
                MessageBox.Show("发光值不能为空！");
            }
            return sb.Append(sb2);
        }

        // 保存一维码
        private void button1_Click(object sender, EventArgs e)
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
                Utils.instance.saveImage(dtLightVal, fPath , 4);  //lyq mod 20190821
            }
            //Utils.instance.saveImage(dtLightVal,"发光值");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(ToDecryption(Nlist[0].EncryNum));
            
        }

        private string ToDecryption(string encry)//解密方法
        {
            BigInteger num = BigInteger.Parse(encry);
            if (num >= 2713844813137321)
            {
                MessageBox.Show("输入不合法", "错误");
                return "";
            }
            num = BigInteger.ModPow(num, 275547707, 2713844813137321);
            StringBuilder str = new StringBuilder(num.ToString());
            while (str.Length < 12) str.Insert(0, 0);
            return str.ToString();
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
            LightValueA.Text = "";
            LightValueB.Text = "";
            LightValueC.Text = "";
            LightValueD.Text = "";
            LightValueE.Text = "";
            LightValueF.Text = "";
            textBoxG.Text = "";
            panel1.Controls.Clear();
            dtLightVal.Clear();
        }

        private void GrbLight_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void FrmLightVal_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!closedFlag)
            {
                FrmMain.f0.Close();
                System.Environment.Exit(0);
            }
        }
    }
}

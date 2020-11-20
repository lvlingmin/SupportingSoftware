using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DebugTool
{
    public partial class frmAttenuator : Form
    {
        private bool ReadNumHasText = false;
        public frmAttenuator()
        {
            InitializeComponent();
        }

        private void TextBox2_MouseEnter(object sender, EventArgs e)
        {
            //if (ReadNumHasText == false)
            //    textBox2.Text = "";
            //textBox2.ForeColor = Color.Black;
        }

        private void TextBox2_MouseLeave(object sender, EventArgs e)
        {
            //if (textBox2.Text == "" && textBox2.Focused == false)
            //{
            //    ReadNumHasText = false;
            //    textBox2.Text = "请输入读数次数";
            //    textBox2.ForeColor = Color.LightGray;
            //}
            //else
            //    ReadNumHasText = true;
        }

        private void FbtnReadNum_Click(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                MessageBox.Show("仪器正在运动，请稍等！", "仪器调试");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("请输入正确指令！", "仪器调试");
                textBox2.Focus();
                return;
            }
            if (!(Regex.IsMatch(textBox2.Text, "^([1-9]{1}[0-9]{0,1})$")))
            {
                MessageBox.Show("请输入正确参数！", "仪器调试");
                textBox2.Focus();
                return;
            }
            fbtnReadNum.Enabled = false;
            textBox2.ReadOnly = true;
            //int num = 0;
            BeginInvoke(new Action(() =>
            {
                richTextBox1.AppendText(Environment.NewLine + Environment.NewLine + DateTime.Now.ToString("HH-mm-ss") + ":开始读数。" + Environment.NewLine);
                // richTextBox1.AppendText(Environment.NewLine + Environment.NewLine + DateTime.Now.ToString("HH-mm-ss") + ": 第 " + num++ + " 个管正在读数。" + Environment.NewLine);
            }));

            NetCom3.Instance.ReceiveHandel += GetReadNum2;
            for (int i = 0; i < int.Parse(textBox2.Text); i++)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 31 03 03 00 00 00 01"), 2);
                if (!NetCom3.Instance.WashQuery())
                    return;
                // num++;
            }
            NetCom3.Instance.ReceiveHandel -= GetReadNum2;
            textBox2.ReadOnly = false;
            fbtnReadNum.Enabled = true;
            BeginInvoke(new Action(() =>
            {
                richTextBox1.AppendText(Environment.NewLine + DateTime.Now.ToString("HH-mm-ss") + ": 测试结束。" + Environment.NewLine + Environment.NewLine);
            }));
        }
        private void GetReadNum2(string order)
        {
            if (order.Contains("EB 90 31 A3"))
            {
                string temp = order.Replace(" ", "");
                int pos = temp.IndexOf("EB9031A3");
                temp = temp.Substring(pos, 32);
                temp = temp.Substring(temp.Length - 8);
                temp = Convert.ToInt64(temp, 16).ToString();
                if (int.Parse(temp) > Math.Pow(10, 5))
                    temp = ((int)GetPMT(double.Parse(temp))).ToString();
                if (int.Parse(temp) == 0)
                {
                    return;
                }
                LogFile.Instance.Write(DateTime.Now.ToString("HH:mm:ss:fff") + ": " + "PMT背景值：" + temp);
                BeginInvoke(new Action(() =>
                {
                    richTextBox1.AppendText(DateTime.Now.ToString("HH-mm-ss") + ": " + "PMT背景值：" + temp + Environment.NewLine);
                }));
            }
        }
        public double GetPMT(double pmt)
        {
            return pmt = pmt / (1 - pmt * 20 * Math.Pow(10, -9)); ;
        }

        private void BtnClearTxt_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void FrmAttenuator_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}

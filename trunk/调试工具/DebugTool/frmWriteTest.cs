using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DebugTool
{
    public partial class frmWriteTest : Form
    {
        List<string> funcList1 = new List<string>();
        List<string> funcList2 = new List<string>();
        List<string> funcList3 = new List<string>();
        List<string> funcList4 = new List<string>();
        List<string> funcList5 = new List<string>();
        private string hData = "";
        private int num = 0;
        string item;
        string itemIndex;
        string func ;
        string funcIndex;
        int paraValue;
        public frmWriteTest()
        {
            InitializeComponent();
        }

        private void frmWriteTest_Load(object sender, EventArgs e)
        {
            NetCom3.Instance.ReceiveHandel += dealOrder;
            funcList1.Add("E2写入测试");

            funcList2.Add("E2写入测试");
            funcList2.Add("蠕动泵");
            funcList2.Add("计量泵");
            funcList2.Add("混匀电机");

            funcList3.Add("E2写入测试");
            funcList3.Add("读数");

            funcList4.Add("E2写入测试");
            funcList4.Add("齿轮泵");
            funcList4.Add("隔膜泵");
            funcList4.Add("电磁阀");
            funcList4.Add("托马斯泵");

            funcList5.Add("E2写入测试");
            funcList5.Add("混匀电机");
        }

        private void btnWriteIn_Click(object sender, EventArgs e)
        {
            if(num1.Value == 0)
            {
                MessageBox.Show("参数不允许是零！");
                return;
            }
            btnWriteIn.Enabled = false;
            #region para
            item = comboBox1.Text.Trim();
            itemIndex = (comboBox1.SelectedIndex + 1).ToString("X2");
            func = comboBox2.Text;
            funcIndex = (comboBox2.SelectedIndex).ToString("X2");
            paraValue = (int)num1.Value;

            #endregion

            #region
            num++;
            hData = paraValue.ToString("x8");

            #endregion

            #region
            if (func.Contains("E2"))
            {
                hData = "";
                hData = int.Parse(num1.Value.ToString()).ToString("X4");
                hData = hData.Insert(2, " ");
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 F0 01 " + itemIndex + " 00 " + hData), 5);
                NetCom3.Instance.SingleQuery();
            }
            else if(item.Contains("计数器板"))
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 F0 01 " + itemIndex + " " + funcIndex), 5);
                NetCom3.Instance.SingleQuery();
                //deal read num
            }
            else
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 F0 01 " + itemIndex + " " + funcIndex + " " + hData.Substring(0, 2) + " " + hData.Substring(2, 2) + " "
                      + hData.Substring(4, 2) + " " + hData.Substring(6, 2)), 5);
                NetCom3.Instance.SingleQuery();
            }
           
            #endregion

            btnWriteIn.Enabled = true;
        }

        private void dealOrder(string order)
        {
            if(order.Contains("EB 90 01 A0"))
            {
                int tempInt = order.IndexOf("EB 90 01 A0 ");
                string pra = order.Substring(tempInt + 12, 5);
                string pra2 = order.Substring(tempInt + 36, 11);
                string temp = "";
                if (func.Contains("E2"))//e2测试
                {
                    if (pra == hData)
                    {
                        temp = "写入成功！";
                    }
                    else if (pra == "00 00")
                    {
                        temp = "没有返回参数！";
                    }
                    else
                    {
                        temp = "返回参数错误！";
                    }
                }
                else if(func.Contains("读数"))//计数板读数
                {
                    pra2 = pra2.Replace(" ","");
                    pra2 = Convert.ToInt64(pra2, 16).ToString();
                    if (int.Parse(pra2) > Math.Pow(10, 5))
                        temp = ((int)GetPMT(double.Parse(pra2))).ToString();
                    if (int.Parse(pra2) == 0)
                    {
                        temp = "发光值：" + temp + ",异常";
                    }
                    else
                    {
                        temp = "发光值：" + temp;
                    }
                }
                else//点击步进
                {
                    string sign = "";
                    if(paraValue > 0)
                    {
                        sign = "+";
                    }
                    temp = item + "--" + func + sign + paraValue;
                }

                BeginInvoke(new Action(() =>
                {
                    txtResultShow.AppendText("\n(" + num + ") " + DateTime.Now.ToString("hh:mm:ss-> ") + temp);
                }));
            }
        }
        public double GetPMT(double pmt)
        {
            return pmt = pmt / (1 - pmt * 20 * Math.Pow(10, -9)); ;
        }
        private void frmWriteTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            NetCom3.Instance.ReceiveHandel -= dealOrder;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            NetCom3.Instance.ReceiveHandel -= dealOrder;
            Environment.Exit(0);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Contains("清洗板"))
            {
                comboBox2.DataSource = funcList2;
            }
            else if (comboBox1.SelectedItem.ToString().Contains("计数器板"))
            {
                comboBox2.DataSource = funcList3;
            }
            else if (comboBox1.SelectedItem.ToString().Contains("加样板"))
            {
                comboBox2.DataSource = funcList4;
            }
            else if (comboBox1.SelectedItem.ToString().Contains("温育板"))
            {
                comboBox2.DataSource = funcList5;
            }
            else
            {
                comboBox2.DataSource = funcList1;
            }
        }
    }
}

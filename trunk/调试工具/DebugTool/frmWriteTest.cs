using System;
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
        private string hData = "";
        private int num = 0;
        public frmWriteTest()
        {
            InitializeComponent();
        }

        private void frmWriteTest_Load(object sender, EventArgs e)
        {
            NetCom3.Instance.ReceiveHandel += dealOrder;
        }

        private void btnWriteIn_Click(object sender, EventArgs e)
        {
            btnWriteIn.Enabled = false;
           
            num++;
            hData = "";
            hData = int.Parse(num1.Value.ToString()).ToString("X4");
            hData = hData.Insert(2 , " ");

            NetCom3.Instance.Send(NetCom3.Cover("EB 90 F0 01 " + hData) , 5);
            NetCom3.Instance.SingleQuery();
            
            btnWriteIn.Enabled = true;
        }

        private void dealOrder(string order)
        {
            if(order.Contains("EB 90 01 A0"))
            {
                int tempInt = order.IndexOf("EB 90 01 A0 ");
                string pra = order.Substring(tempInt + 12, 5);

                string temp;
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

                BeginInvoke(new Action(() =>
                {
                    txtResultShow.AppendText("\n(" + num  +") " + DateTime.Now.ToString("hh:mm:ss-> ") + temp);
                }));
            }
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebugTool
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// 下位机返回数据
        /// </summary>
        string[] dataRecive = new string[16];
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;

            if (!NetCom3.isConnect)
            {
                if (NetCom3.Instance.CheckMyIp_Port_Link())
                {
                    NetCom3.Instance.ConnectServer();

                    if (!NetCom3.isConnect)
                    {
                        MessageBox.Show("仪器初始化失败！请确认仪器是否已经开启！", "开机提示");

                        goto errorend;
                    }

                }
                else
                {
                    MessageBox.Show("仪器初始化失败！请确认网线连接状态及仪器的连接地址是否正确！", "开机提示");

                    goto errorend;
                }
            }
            if (NetCom3.isConnect)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 F1 01"), 5);
                if (!NetCom3.Instance.SingleQuery())
                {
                    goto errorend;
                }
                #region 判断各个模组是否握手成功
                int[] HandData = new int[16];
                while (dataRecive[0] == null)
                {
                    Thread.Sleep(10);
                }
                HandData = NetCom3.converTo10(dataRecive);
                if (HandData[4] != 255)
                {
                    MessageBox.Show("计数器模组握手失败！", "上下位机握手");

                    goto errorend;
                }

                if (HandData[7] != 255)
                {
                    MessageBox.Show("清洗模组握手失败！", "上下位机握手");

                    goto errorend;
                }
                #endregion
            }
            Thread.Sleep(2000);

            BeginInvoke(new Action(() =>
            {
                frmPhoton f = new frmPhoton();
                this.Hide();
                f.Show();
            }));
            return;

            errorend:
            BeginInvoke(new Action(() =>
            {
                if (DialogResult.OK == MessageBox.Show("是否退出软件。", "握手失败", MessageBoxButtons.OKCancel))
                    Close();
                NetCom3.Instance.stopsendFlag = false;
                NetCom3.isConnect = false;
                btnLogin.Enabled = true;
                return;
            }));

        }
        void Instance_ReceiveHandel(object obj)
        {
            dataRecive = obj.ToString().Split(' ');
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NetCom3.Instance.ReceiveHandel += new Action<string>(Instance_ReceiveHandel);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            NetCom3.Instance.ReceiveHandel -= new Action<string>(Instance_ReceiveHandel);
            System.Environment.Exit(0);
        }

        private void BtnIAP_Click(object sender, EventArgs e)
        {
            btnIAP.Enabled = false;
            if (!NetCom3.isConnect)
            {
                if (NetCom3.Instance.CheckMyIp_Port_Link())
                {
                    NetCom3.Instance.ConnectServer();

                    if (!NetCom3.isConnect)
                    {
                        MessageBox.Show("仪器初始化失败！请确认仪器是否已经开启！", "开机提示");

                        goto errorend;
                    }

                }
                else
                {
                    MessageBox.Show("仪器初始化失败！请确认网线连接状态及仪器的连接地址是否正确！", "开机提示");

                    goto errorend;
                }
            }

            Thread.Sleep(500);

            BeginInvoke(new Action(() =>
            {
                frmIAP f = new frmIAP();
                this.Hide();
                f.Show();
            }));
            return;

            errorend:
            BeginInvoke(new Action(() =>
            {
                if (DialogResult.OK == MessageBox.Show("是否退出软件。", "握手失败", MessageBoxButtons.OKCancel))
                    Close();
                NetCom3.Instance.stopsendFlag = false;
                NetCom3.isConnect = false;
                btnIAP.Enabled = true;
                return;
            }));
        }

        private void BtnAttenuator_Click(object sender, EventArgs e)
        {
            btnAttenuator.Enabled = false;

            if (!NetCom3.isConnect)
            {
                if (NetCom3.Instance.CheckMyIp_Port_Link())
                {
                    NetCom3.Instance.ConnectServer();

                    if (!NetCom3.isConnect)
                    {
                        MessageBox.Show("仪器初始化失败！请确认仪器是否已经开启！", "开机提示");

                        goto errorend;
                    }

                }
                else
                {
                    MessageBox.Show("仪器初始化失败！请确认网线连接状态及仪器的连接地址是否正确！", "开机提示");

                    goto errorend;
                }
            }
           
            Thread.Sleep(2000);

            BeginInvoke(new Action(() =>
            {
                frmAttenuator f = new frmAttenuator();
                this.Hide();
                f.Show();
            }));
            return;

            errorend:
            BeginInvoke(new Action(() =>
            {
                if (DialogResult.OK == MessageBox.Show("是否退出软件。", "握手失败", MessageBoxButtons.OKCancel))
                    Close();
                NetCom3.Instance.stopsendFlag = false;
                NetCom3.isConnect = false;
                btnLogin.Enabled = true;
                return;
            }));
        }

        private void btnLinearityTest_Click(object sender, EventArgs e)
        {
            btnLinearityTest.Enabled = false;

            if (!NetCom3.isConnect)
            {
                if (NetCom3.Instance.CheckMyIp_Port_Link())
                {
                    NetCom3.Instance.ConnectServer();

                    if (!NetCom3.isConnect)
                    {
                        MessageBox.Show("仪器初始化失败！请确认仪器是否已经开启！", "开机提示");

                        goto errorend;
                    }

                }
                else
                {
                    MessageBox.Show("仪器初始化失败！请确认网线连接状态及仪器的连接地址是否正确！", "开机提示");

                    goto errorend;
                }
            }

            Thread.Sleep(2000);

            BeginInvoke(new Action(() =>
            {
                frmLinearityTest f = new frmLinearityTest();
                this.Hide();
                f.Show();
            }));
            return;

            errorend:
            BeginInvoke(new Action(() =>
            {
                if (DialogResult.OK == MessageBox.Show("是否退出软件。", "握手失败", MessageBoxButtons.OKCancel))
                    Close();
                NetCom3.Instance.stopsendFlag = false;
                NetCom3.isConnect = false;
                btnLinearityTest.Enabled = true;
                return;
            }));
        }
    }
}

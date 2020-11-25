using EBarv0._2.CustomControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EBarv0._2.user
{
    public partial class frmUser : frmParent
    {
        public static frmUser f0 = null;
        public frmUser()
        {
            InitializeComponent();
            f0 = this;
        }

        private void frmUser_Load(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            btnPrint.Enabled = false;
            if (!NetCom3.isConnect)
            {
                if (NetCom3.Instance.CheckMyIp_Port_Link())
                {
                    NetCom3.Instance.ConnectServer();

                    if (!NetCom3.isConnect)
                    {
                        MessageBox.Show("仪器连接失败！请确认仪器是否已经开启！", "开机提示");

                        goto errorend;
                    }
                }
                else
                {
                    MessageBox.Show("仪器连接失败！请确认网线连接状态及仪器的连接地址是否正确！", "开机提示");
                    goto errorend;
                }
            }

            if(chkInit.Checked)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 CA F1 01"), 5);
                if (!NetCom3.Instance.SingleQuery())
                {
                    MessageBox.Show("读卡器初始化失败，请重新连接");
                    goto errorend;
                }
            }

            NetCom3.Delay(1000);
            btnPrint.Enabled = true;
            this.Hide();
            frmSpImport f = new frmSpImport();
            f.ShowDialog();
            return;

            errorend:
            BeginInvoke(new Action(() =>
            {
                if (DialogResult.OK == MessageBox.Show("是否退出软件。", "握手失败", MessageBoxButtons.OKCancel))
                    Close();
                NetCom3.Instance.stopsendFlag = false;
                NetCom3.isConnect = false;
                btnPrint.Enabled = true;
                return;
            }));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (!NetCom3.isConnect)
            {
                if (NetCom3.Instance.CheckMyIp_Port_Link())
                {
                    NetCom3.Instance.ConnectServer();

                    if (!NetCom3.isConnect)
                    {
                        MessageBox.Show("仪器连接失败！请确认仪器是否已经开启！", "开机提示");

                        goto errorend;
                    }
                }
                else
                {
                    MessageBox.Show("仪器连接失败！请确认网线连接状态及仪器的连接地址是否正确！", "开机提示");
                    goto errorend;
                }
            }

            if (chkInit.Checked)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 CA F1 01"), 5);
                if (!NetCom3.Instance.SingleQuery())
                {
                    MessageBox.Show("读卡器初始化失败，请重新连接");
                    goto errorend;
                }
            }

            NetCom3.Delay(1000);
            button1.Enabled = true;
            this.Hide();
            frmBarImportSP f = new frmBarImportSP();
            f.ShowDialog();
            return;

        errorend:
            BeginInvoke(new Action(() =>
            {
                if (DialogResult.OK == MessageBox.Show("是否退出软件。", "握手失败", MessageBoxButtons.OKCancel))
                    Close();
                NetCom3.Instance.stopsendFlag = false;
                NetCom3.isConnect = false;
                button1.Enabled = true;
                return;
            }));

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ZXing.Common;
using ZXing;

namespace EBarv0._2
{
    public partial class FrmMain : Form
    {
        public static FrmMain f0 = null;
        public FrmMain()
        {
            InitializeComponent();
            f0 = this;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmScaling frmScaling = new FrmScaling();
            frmScaling.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLightVal frmLightVal = new FrmLightVal();
            frmLightVal.ShowDialog();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmProject frmFrmProject = new FrmProject();
            frmFrmProject.ShowDialog();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLoadItem frmLoadItem = new FrmLoadItem();
            frmLoadItem.ShowDialog();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmProject2 fp2 = new FrmProject2();
            fp2.ShowDialog();
        }

        private void btnElectronicLabelEntry_Click(object sender, EventArgs e)
        {
            btnElectronicLabelEntry.Enabled = false;
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

            NetCom3.Delay(1000);
            btnElectronicLabelEntry.Enabled = true;
            this.Hide();
            frmElectronicLabelEntry f = new frmElectronicLabelEntry();
            f.ShowDialog();
            return;

        errorend:
            BeginInvoke(new Action(() =>
            {
                if (DialogResult.OK == MessageBox.Show("是否退出软件。", "握手失败", MessageBoxButtons.OKCancel))
                    Close();
                NetCom3.Instance.stopsendFlag = false;
                NetCom3.isConnect = false;
                btnElectronicLabelEntry.Enabled = true;
                return;
            }));

        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQualityControl f = new frmQualityControl();
            f.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSubstrate f = new frmSubstrate();
            f.ShowDialog();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDilute f = new frmDilute();
            f.ShowDialog();
        }
    }
}

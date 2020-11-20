using BioBase.HSCIADebug.SysMaintenance;
using Dialogs;
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

namespace BioBase.HSCIADebug
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// 下位机返回数据
        /// </summary>
        string[] dataRecive = new string[16];
        Autosize autosize = new Autosize();
        private float X;//宽度 
        private float Y;//高度
        public frmMain()
        {
            InitializeComponent();
            X = this.Width;
            Y = this.Height;
            autosize.setTag(this);
            NetCom3.Instance.ReceiveHandel += new Action<string>(Instance_ReceiveHandel);
            if (!NetCom3.isConnect)
            {
                if (NetCom3.Instance.CheckMyIp_Port_Link())
                {
                    NetCom3.Instance.ConnectServer();

                    if (!NetCom3.isConnect)
                    {
                        DialogResult r =
                            MessageBox.Show("仪器初始化失败！请确认仪器是否已经开启！是否脱机运行！", "开机提示",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        DialogResult = r;
                        return;
                    }

                }
                else
                {
                    DialogResult r =
                        MessageBox.Show("仪器初始化失败！请确认网线连接状态及仪器的连接地址是否正确！是否脱机运行！", "开机提示",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    DialogResult = r;
                    return;
                }
            }
            if (NetCom3.isConnect)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 F1 01"), 5);
                if (!NetCom3.Instance.SingleQuery())
                {
                    DialogResult r =
                       MessageBox.Show("上下位机握手失败，指令超时返回！", "开机提示",
                       MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    return;
                }
                #region 判断各个模组是否握手成功
                int[] HandData = new int[16];
                while (dataRecive[0] == null)
                {
                    Thread.Sleep(10);
                }
                HandData = NetCom3.converTo10(dataRecive);
                StringBuilder err = new StringBuilder();
                if (HandData[4] != 255)
                {
                    err.Append("计数器模组握手失败！\n");
                }
                if (HandData[5] != 255)
                {
                    err.Append("加样机模组握手失败！\n");
                }
                if (HandData[6] != 255)
                {
                    err.Append("理杯机模组握手失败!\n");
                }
                if (HandData[7] != 255)
                {
                    err.Append("清洗模组握手失败!\n");
                }
                if (HandData[8] != 255)
                {
                    err.Append("报警模组握手失败!\n");
                }
                if (HandData[9] != 255)
                {
                    err.Append("温育盘模组握手失败!\n");
                }
                if (!string.IsNullOrEmpty(err.ToString()))
                {
                    //Invoke(new Action(() =>
                    //{
                        
                    //}));
                    MessageBox.Show("仪器初始化异常:\n\n" + err.ToString(), "温馨提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                frmMessageShow frmMessage = new frmMessageShow();
                frmMessage.MessageShow("温馨提示", "仪器初始化上下位机握手成功");
                //MessageBox.Show("仪器初始化上下位机握手成功", "温馨提示",
                //           MessageBoxButtons.OK, MessageBoxIcon.Information);
                #endregion
            }
            
        }
        void Instance_ReceiveHandel(object obj)
        {
            dataRecive = obj.ToString().Split(' ');
        }
        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            autosize.setControls(newx, newy, this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否确定关闭正在运行的系统！", "系统退出警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                Application.Exit();
                System.Environment.Exit(0);
            }
        }

        private void functionButton1_Click(object sender, EventArgs e)
        {
            frmDiagnost frmim = new frmDiagnost();
            frmim.MdiParent = this;//指定当前窗体为顶级Mdi窗体
            frmim.Parent = this.pnlPublic;//指定子窗体的父容器为
            frmim.Show();
        }

        private void btnPerformance_Click(object sender, EventArgs e)
        {
            FrmPerformance frmim = new FrmPerformance();
            frmim.MdiParent = this;//指定当前窗体为顶级Mdi窗体
            frmim.Parent = this.pnlPublic;//指定子窗体的父容器为
            frmim.Show();
        }

        private void functionButton2_Click(object sender, EventArgs e)
        {
            if (!NetCom3.isConnect)
            {
                if (NetCom3.Instance.CheckMyIp_Port_Link())
                {
                    NetCom3.Instance.ConnectServer();

                    if (!NetCom3.isConnect)
                    {
                        DialogResult r =
                            MessageBox.Show("仪器初始化失败！请确认仪器是否已经开启！是否脱机运行！", "开机提示",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        DialogResult = r;
                        goto complete;
                    }

                }
                else
                {
                    DialogResult r =
                        MessageBox.Show("仪器初始化失败！请确认网线连接状态及仪器的连接地址是否正确！是否脱机运行！", "开机提示",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    DialogResult = r;
                    goto complete;
                }
            }
            if (NetCom3.isConnect)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 F1 01"), 5);
                if (!NetCom3.Instance.SingleQuery())
                {
                    DialogResult r =
                       MessageBox.Show("上下位机握手失败，指令超时返回！", "开机提示",
                       MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    goto complete;
                }
                #region 判断各个模组是否握手成功
                int[] HandData = new int[16];
                while (dataRecive[0] == null)
                {
                    Thread.Sleep(10);
                }
                HandData = NetCom3.converTo10(dataRecive);
                StringBuilder err = new StringBuilder();
                if (HandData[4] != 255)
                {
                    err.Append("计数器模组握手失败！\n");
                }
                if (HandData[5] != 255)
                {
                    err.Append("加样机模组握手失败！\n");
                }
                if (HandData[6] != 255)
                {
                    err.Append("理杯机模组握手失败!\n");
                }
                if (HandData[7] != 255)
                {
                    err.Append("清洗模组握手失败!\n");
                }
                if (HandData[8] != 255)
                {
                    err.Append("报警模组握手失败!\n");
                }
                if (HandData[9] != 255)
                {
                    err.Append("温育盘模组握手失败!\n");
                }
                if (!string.IsNullOrEmpty(err.ToString()))
                {
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show("仪器初始化异常:\n\n" + err.ToString(), "温馨提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }));
                    goto complete;
                }
                MessageBox.Show("仪器初始化上下位机握手成功", "温馨提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                #endregion
            }
            complete:
            return;
        }

        private void functionButton3_Click(object sender, EventArgs e)
        {
                //仪器初始化
                //BeginInvoke(new Action(() =>
                //{
                //    progressData.Value = 70;
                //    lblDescribe.Text = "仪器初始化..." + " " + progressData.Value.ToString() + "%";
                //}));
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 F1 02"), 5);
                if (!NetCom3.Instance.SingleQuery())
                {
                    goto complete;
                }
                #region 判断各个模组是否初始化成功
                if (NetCom3.Instance.ErrorMessage != null)
                {
                    //2018-09-06 zlx mod
                    Invoke(new Action(() =>
                    {
                        DialogResult r = MessageBox.Show(NetCom3.Instance.ErrorMessage + "\r是否脱机运行！", "温馨提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }));
                    goto complete;
                }
            #endregion
                BeginInvoke(new Action(() =>
                {
                    //progressData.Value = 100;
                    //lblDescribe.Text = "仪器初始化..." + " " + progressData.Value.ToString() + "%";
                }));
            complete:
            MessageBox.Show("仪器初始化成功", "温馨提示",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fbtnGroupTest_Click(object sender, EventArgs e)
        {
            frmInstruGroupTest frmInstruGroup = new frmInstruGroupTest();
            frmInstruGroup.MdiParent = this;//指定当前窗体为顶级Mdi窗体
            frmInstruGroup.Parent = this.pnlPublic;//指定子窗体的父容器为
            frmInstruGroup.Show();
        }
    }
}

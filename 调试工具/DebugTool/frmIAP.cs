using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebugTool
{
    public partial class frmIAP : Form
    {
        /// <summary>
        /// 返回指令
        /// </summary>
        string BackObj = "";
        #region 烧录界面变量
        int selectZhenID = -1;
        int[] receiveFlag = new int[6];
        string iapFilePath = ""; //烧录文件路径
        Thread thFire = null;
        #endregion
        public frmIAP()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (cmbZhenID.SelectedIndex < 0)
            {
                MessageBox.Show("未选择电控板位，请先选择电控板位！");
                cmbZhenID.Focus();
                return;
            }
            OpenFileDialog openFile = new OpenFileDialog();
            string strPath;
            if (txtFilePath.Text == "")
            {
                strPath = System.Windows.Forms.Application.StartupPath;
            }
            else
            {
                strPath = txtFilePath.Text.ToString().Substring(0, txtFilePath.Text.LastIndexOf(@"\"));
            }
            //openFile.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            openFile.InitialDirectory = strPath;
            openFile.Filter = "bin文件|*.bin";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFile.FileName;
            }
            openFile.Dispose();
        }

        private void BtnLoadProgram_Click(object sender, EventArgs e)
        {
            if (cmbZhenID.SelectedIndex < 0)
            {
                MessageBox.Show("未选择电控板位，请重新选择！");
                cmbZhenID.Focus();
                return;
            }
            if (txtFilePath.Text.Trim() == "" || !txtFilePath.Text.Trim().Substring(txtFilePath.Text.Trim().LastIndexOf(@"\")).Contains(cmbZhenID.SelectedItem.ToString().Substring(0, 2)))
            {
                MessageBox.Show("文件路径错误，请重新选择与所选电控板位对应的 " + cmbZhenID.SelectedItem.ToString().Substring(0, 2) + "模组 文件");
                txtFilePath.Focus();
                return;
            }
            if (!NetCom3.totalOrderFlag)
            {
                MessageBox.Show("仪器正在运动，请稍等！", "仪器调试");
                return;
            }
            if (cmbSendDelay.SelectedIndex < 0)
                return;
            iapFilePath = txtFilePath.Text;
            selectZhenID = cmbZhenID.SelectedIndex + 1;  //1-8 计数器 通讯 抓手 加样 清洗 报警 制冷 温育 
            cmbZhenID.Enabled = txtFilePath.Enabled = btnSelectBin.Enabled = btnLoadProgram.Enabled = false;
            thFire = new Thread(new ThreadStart(LoadPro));
            thFire.Start();
            cmbSendDelay.Enabled = false;
        }
        private void LoadPro() //IAP
        {
            BeginInvoke(new Action(() =>
            {
                pgbLoad.Value = 1; //初始化进度栏
                lblPercentage.Text = pgbLoad.Value.ToString() + "%"; //进度栏下面打印进度
            }));
            FileStream Myfile;
            try
            {
                Myfile = new FileStream(iapFilePath.Trim(), FileMode.Open, FileAccess.Read);
                if (Myfile.Length < 50)
                {
                    MessageBox.Show("这是一个空文件", "ERROR");
                    goto errorOrEnd;
                }
            }
            catch (System.Exception exp)
            {
                MessageBox.Show("获取该文件信息失败", "ERROR");
                Console.WriteLine(exp.ToString());
                goto errorOrEnd;
            }
            if (selectZhenID == 2)
            {
                //通讯板稳定延迟20ms
                Invoke(new Action(() =>
                {
                    cmbSendDelay.Text = "20";
                }));
            }

            string boardName = "";
            Invoke(new Action(() =>
            {
                boardName = cmbZhenID.SelectedItem.ToString();
            }));
            BinaryReader binreader = new BinaryReader(Myfile);
            int file_len = (int)Myfile.Length;//获取bin文件长度

            int allNumber = file_len + 8 - (file_len % 8 == 0 ? 8 : file_len % 8); //1107mod:EB90后一次发送8字节
            byte[] buff = new byte[allNumber];
            byte[] buff1 = binreader.ReadBytes(file_len);

            Myfile.Dispose();
            binreader.Dispose();


            for (int i = 0; i < allNumber; i++)
            {
                if (i < file_len)
                {
                    buff[i] = buff1[i];
                }
                else
                {
                    buff[i] = 0;
                }
            }
            NetCom3.Instance.ReceiveHandel += DealReceive;

            //握手
            NetCom3.Instance.iapIsRun = true;

            if (selectZhenID != 2)
            {
                BeginInvoke(new Action(() =>
                {
                    lblDescribe.Text = "上下位机正在握手...";
                }));
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 AA 01"), 5); //通讯板处理
                if (!NetCom3.Instance.SingleQuery())
                {
                    MessageBox.Show("握手指令发送失败", "ERROR");
                    goto errorOrEnd;
                }
                while (receiveFlag[0] == 0)
                {
                    NetCom3.Delay(100);
                }

                //梁皓辉说工装这一条不发
                ////握手 下位机
                //BeginInvoke(new Action(() =>
                //{
                //    lblDescribe.Text = "正在跳转 " + boardName;
                //}));
                //NetCom3.Instance.iapNoBack = true; //A0 01改成不返回了
                //NetCom3.Instance.Send(NetCom3.Cover("EB 90 A0 01 0" + selectZhenID), 5); //下位机跳转 
                //if (!NetCom3.Instance.SingleQuery())
                //{
                //    MessageBox.Show("跳转指令发送失败", "ERROR");
                //    goto errorOrEnd;
                //}
                //NetCom3.Instance.iapNoBack = false;

                //if (selectZhenID == 2) //如果烧录通讯板，在这一步通讯板要跳转需要重连
                //{
                //    aabc:                    
                //    if (NetCom3.Instance.CheckMyIp_Port_Link())
                //    {
                //        NetCom3.isConnect = false;
                //        NetCom3.Instance.iapConnectTwice();
                //        if (!NetCom3.isConnect)
                //        {
                //            MessageBox.Show("重新连接失败", "ERROR");
                //            goto aabc;
                //        }
                //    }
                //    else
                //        goto aabc;
                //}

                NetCom3.Delay(5000);
            }

            //握手 下位机IAP
            BeginInvoke(new Action(() =>
            {
                lblDescribe.Text = "正在与 " + boardName + " 的IAP握手";
            }));

            NetCom3.Instance.Send(NetCom3.Cover("EB 90 B0 01 0" + selectZhenID), 5); //与IAP握手
            if (!NetCom3.Instance.SingleQuery())
            {
                MessageBox.Show("IAP握手指令发送失败", "ERROR");
                goto errorOrEnd;
            }
            NetCom3.Delay(2000); //等待1秒
            while (receiveFlag[2] == 0)
            {
                NetCom3.Delay(100);
            }

            //擦除       
            BeginInvoke(new Action(() =>
            {
                pgbLoad.Value = 5;//进度百分之五
                lblPercentage.Text = pgbLoad.Value.ToString() + "%";
                lblDescribe.Text = "正在擦除Flash...";
            }));
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 B0 02"), 5); //擦除
            if (!NetCom3.Instance.SingleQuery())
            {
                MessageBox.Show("擦除指令发送失败", "ERROR");
                goto errorOrEnd;
            }
            while (receiveFlag[3] == 0)
            {
                NetCom3.Delay(100);
            }

            //传输数据长度
            BeginInvoke(new Action(() =>
            {
                pgbLoad.Value = 25;//进度百分之二十五
                lblPercentage.Text = pgbLoad.Value.ToString() + "%";
                lblDescribe.Text = "正在发送文件长度.";
            }));
            string len = file_len.ToString("x8");  //文件长度转化为十六进制八位
            len = len.Substring(0, 2) + " " + len.Substring(2, 2) + " " + len.Substring(4, 2) + " " + len.Substring(6, 2);
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 B0 03 00 00 " + len), 5);
            if (!NetCom3.Instance.SingleQuery())
            {
                MessageBox.Show("文件长度发送失败", "ERROR");
                goto errorOrEnd;
            }
            while (receiveFlag[4] == 0)
            {
                NetCom3.Delay(100);
            }
            NetCom3.Delay(500);
            //传输数据
            NetCom3.Instance.iapNoBack = true;
            BeginInvoke(new Action(() =>
            {
                pgbLoad.Value = 30;//进度百分之30
                lblPercentage.Text = pgbLoad.Value.ToString() + "%";
                lblDescribe.Text = "正在发送数据...";
            }));

            int sendDelay = 20;//ms
            Invoke(new Action(()=>
            {
                sendDelay = int.Parse(cmbSendDelay.Text);
            }));
            //Stopwatch swatch = new Stopwatch();
            ManualResetEvent loadDataDone = new ManualResetEvent(false);
            for (int m = 0, k = 1; m < allNumber; m += 8) //通讯指令16进制2位一字节
            {
                //Console.WriteLine(m+"-------"+allNumber);//测试
                
                loadDataDone.Reset();
                if ((m + 8) >= allNumber)
                    NetCom3.Instance.iapNoBack = false; ;
                string data = "EB 90 "
                            + buff[m].ToString("x2") + " " + buff[m + 1].ToString("x2") + " "
                            + buff[m + 2].ToString("x2") + " " + buff[m + 3].ToString("x2") + " "
                            + buff[m + 4].ToString("x2") + " " + buff[m + 5].ToString("x2") + " "
                            + buff[m + 6].ToString("x2") + " " + buff[m + 7].ToString("x2");
                //NetCom3.Instance.Send(NetCom3.Cover(data), 5);
                NetCom3.Instance.iapSend(NetCom3.Cover(data), 5);
                
                if (!IapTraQuery())//循环等待变量是延迟1ms
                {
                    MessageBox.Show("数据传输失败！", "ERROR");
                    goto errorOrEnd;
                }

                if (m / (allNumber / 50) == k)
                {
                    BeginInvoke(new Action(() =>
                    {
                        pgbLoad.Value = 30 + k;//进度百分之N
                        lblPercentage.Text = pgbLoad.Value.ToString() + "%";
                    }));
                    k++;
                }
                //Thread.Sleep(2);
                if (!loadDataDone.WaitOne(sendDelay, false))
                {
                    ;
                }
            }
            while (true)
            {
                if (receiveFlag[5] == 0)
                    NetCom3.Delay(100);
                else if (receiveFlag[5] == 1)
                    break;
                else if (receiveFlag[5] == 2)
                {
                    MessageBox.Show("IAP程序烧录失败", "ERROR");
                    goto errorOrEnd;
                }
            }

            NetCom3.Instance.iapNoBack = true;
            BeginInvoke(new Action(() =>
            {
                pgbLoad.Value = 90;//进度百分之90
                lblPercentage.Text = pgbLoad.Value.ToString() + "%";
                lblDescribe.Text = "数据传输完成...";
            }));

            NetCom3.Delay(1000);

            //结束返回软件
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 B0 05 00 00 00 00 80 00"), 5);
            if (!NetCom3.Instance.SingleQuery())
            {
                MessageBox.Show("结束指令发送失败", "ERROR");
                goto errorOrEnd;
            }
            else
            {
                BeginInvoke(new Action(() =>
                {
                    pgbLoad.Value = 100;//进度百分之100
                    lblPercentage.Text = pgbLoad.Value.ToString() + "%";
                    lblDescribe.Text = "程序下载成功！";
                }));
            }

            //通讯板重连
            if (selectZhenID == 2)
            {
                bbcd:
                if (NetCom3.Instance.CheckMyIp_Port_Link())
                {
                    NetCom3.isConnect = false;
                    NetCom3.Instance.iapConnectTwice();
                    if (!NetCom3.isConnect)
                    {
                        MessageBox.Show("重新连接失败", "ERROR");
                        goto bbcd;
                    }
                }
                else
                    goto bbcd;
            }


            NetCom3.Instance.iapNoBack = false;
            NetCom3.Delay(500);
            NetCom3.Instance.ReceiveHandel -= DealReceive;

            if (selectZhenID == 2)
            {
                MessageBox.Show("请重启软件和仪器，点击确定自动退出软件", "IAP");
                Environment.Exit(0);
            }

            #region  烧完程序初始化前先握手
            BackObj = "";
            int[] HandData = new int[16];
            NetCom3.Delay(2000); //预留b0 05跳转时间
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 F1 01"), 5);
            if (!NetCom3.Instance.SingleQuery())
            {
                goto errorOrEnd;
            }
            //判断各个模组是否握手成功            
            while (true)
            {
                if (BackObj.Contains("EB 90 F1 01"))
                {
                    HandData = NetCom3.converTo10(BackObj.Split(' '));
                    break;
                }
            }
            if (HandData[4] != 255)
            {
                MessageBox.Show("计数器模组握手失败！", "上下位机握手");
                goto errorOrEnd;
            }
            if (HandData[5] != 255)
            {
                MessageBox.Show("加样机模组握手失败！", "上下位机握手");
                goto errorOrEnd;
            }
            if (HandData[6] != 255)
            {
                MessageBox.Show("理杯机模组握手失败！", "上下位机握手");
                goto errorOrEnd;
            }
            if (HandData[7] != 255)
            {
                MessageBox.Show("清洗模组握手失败!", "上下位机握手");
                goto errorOrEnd;
            }
            if (HandData[8] != 255)
            {
                MessageBox.Show("报警模组握手失败！", "上下位机握手");
                goto errorOrEnd;
            }
            if (HandData[9] != 255)
            {
                MessageBox.Show("温育盘模组握手失败!", "上下位机握手");
                goto errorOrEnd;
            }
            #endregion
            //传输完毕后初始化仪器           
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 F1 02"), 5);
            if (!NetCom3.Instance.SingleQuery())
            {
                goto errorOrEnd;
            }
            //判断是否初始化成功            
            if (NetCom3.Instance.ErrorMessage != null)
            {
                MessageBox.Show(NetCom3.Instance.ErrorMessage, "仪器初始化");
                goto errorOrEnd;
            }

            MessageBox.Show("程序下载成功", "IAP");

            errorOrEnd:
            Invoke(new Action(() =>
            {
                for (int i = 0; i < receiveFlag.Length; i++)
                {
                    receiveFlag[i] = 0;
                }
                pgbLoad.Value = 0;
                lblPercentage.Text = "0%";
                lblDescribe.Text = "...";
                cmbZhenID.Enabled = txtFilePath.Enabled = btnSelectBin.Enabled = btnLoadProgram.Enabled = true;
                NetCom3.Instance.iapIsRun = false;
                NetCom3.Instance.iapNoBack = false;
                cmbSendDelay.Enabled = true;
            }));
            buff = new byte[1];
            buff1 = new byte[1];
            GC.Collect();
        }
        private void DealReceive(string order)
        {
            order = order.Substring(6, 8);

            if (selectZhenID != 2)
            {
                if (order.Contains("55 01"))
                {
                    receiveFlag[0] = 1;
                }
                //else if (order.Contains("0A A0 0" + selectZhenID))  //已修改，不返回这条指令
                //{
                //    receiveFlag[1] = 1;
                //}
            }
            //if (order.Contains("6A A6 0" + selectZhenID))
            //{
            //    receiveFlag[2] = 1;
            //}
            if (order.Contains("6A A6"))
            {
                receiveFlag[2] = 1;
            }
            else if (order.Contains("B0 02 FF"))
            {
                receiveFlag[3] = 1;
            }
            else if (order.Contains("B0 03 FF"))
            {
                receiveFlag[4] = 1;
            }
            else if (order.Contains("B0 04 FF"))//烧录成功
            {
                receiveFlag[5] = 1;
            }
            else if (order.Contains("B0 04 01"))//烧录失败
            {
                receiveFlag[5] = 2;
            }

        }
        private bool IapTraQuery()
        {
            int sendFlag = 0;
            aabc:
            while (!NetCom3.totalOrderFlag || !NetCom3.callBack)
            {
                //Thread.Sleep(1);
            }
            if (NetCom3.Instance.errorFlag != (int)ErrorState.Success)
            {
                if (++sendFlag < 2)
                {
                    LogFile.Instance.Write("errorFlag = ： " + NetCom3.Instance.errorFlag + "  *****当前 " + DateTime.Now.ToString("HH - mm - ss") + "  *****IAP数据传输，正在等待连接 ");

                    NetCom3.Delay(5000);
                    if (NetCom3.isConnect)//如果连接上了，goto继续
                    {
                        goto aabc;
                    }
                }
                LogFile.Instance.Write("errorFlag = ： " + NetCom3.Instance.errorFlag + "  *****当前 " + DateTime.Now.ToString("HH - mm - ss"));
                return false;
            }
            else
            {
                return true;
            }
        }
        void Instance_ReceiveHandel(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            else
            {
                BackObj = obj;
            }
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void FrmIAP_Load(object sender, EventArgs e)
        {
            NetCom3.Instance.ReceiveHandel += new Action<string>(Instance_ReceiveHandel);
            cmbSendDelay.SelectedIndex = 0;
        }

        private void frmIAP_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}

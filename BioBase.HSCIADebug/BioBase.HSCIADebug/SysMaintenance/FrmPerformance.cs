using Common;
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

namespace BioBase.HSCIADebug.SysMaintenance
{
    public partial class FrmPerformance : frmParent
    {
        #region 配置文件变量
        /// <summary>
        /// 底物与管架配置文件地址
        /// </summary>
        string iniPathSubstrateTube = Directory.GetCurrentDirectory() + "\\SubstrateTube.ini";
        /// <summary>
        /// 试剂盘配置文件地址
        /// </summary>
        string iniPathReagentTrayInfo = Directory.GetCurrentDirectory() + "\\ReagentTrayInfo.ini";
        /// <summary>
        /// 反应盘配置文件地址
        /// </summary>
        string iniPathReactTrayInfo = Directory.GetCurrentDirectory() + "\\ReactTrayInfo.ini";
        /// <summary>
        /// 清洗盘配置文件地址
        /// </summary>
        string iniPathWashTrayInfo = Directory.GetCurrentDirectory() + "\\WashTrayInfo.ini";
        #endregion
        #region 变量
        //提示框
        frmMessageShow frmMsgShow = new frmMessageShow();
        //版本号暂存
        string[] boardVersion = new string[16];
        //暂存盘是否有管
        bool AgingHaveTube = false;
        /// <summary>
        /// 0-准备发送,1-成功 2-发送失败 3-接收失败 4-抓管撞管（撞针） 5-抓空 6-混匀异常 7-放管撞管 8-理杯机缺管 9-发送超时
        /// </summary>
        string[] State = { "准备发送", "成功", "发送失败", "接收失败", "抓管撞管（撞针）", "抓空", "混匀异常", "放管撞管", "理杯机缺管", "发送超时" };
        //txtAgingInfoShow最后一条log
        string showLog = "";
        //
        List<Thread> threadList = new List<Thread>();
        #endregion

        #region 线程
        Thread agingTestThread;
        Thread mixTestThread;
        Thread perfusionTextThread;
        #endregion
        public FrmPerformance()
        {
            InitializeComponent();
        }
        private void FrmPerformance_Load(object sender, EventArgs e)
        {
            cbAgingTest.SelectedIndex = 0;
            new Thread(new ParameterizedThreadStart((obj) =>
            {
                NetCom3.Instance.ReceiveHandel += new Action<string>(Instance_ReceiveHandel);
                if (!NetCom3.isConnect)
                {
                    if (NetCom3.Instance.CheckMyIp_Port_Link())
                    {
                        NetCom3.Instance.ConnectServer();

                        if (!NetCom3.isConnect)
                            return;

                    }
                }
            }))
            { IsBackground = true }.Start();
            //this block add y 20180816
            new Thread(new ParameterizedThreadStart((obj) =>
            {
                NetCom3.Instance.ReceiveHandelForQueryTemperatureAndLiquidLevel += new Action<string>(Read);
                if (!NetCom3.isConnect)
                {
                    if (NetCom3.Instance.CheckMyIp_Port_Link())
                    {
                        NetCom3.Instance.ConnectServer();

                        if (!NetCom3.isConnect)
                            return;

                    }
                }
            }))
            { IsBackground = true }.Start();
        }
        private void FrmPerformance_SizeChanged(object sender, EventArgs e)
        {
            formSizeChange(this);
        }
       
        /// <summary>
        /// 处理返回31指令
        /// </summary>
        /// <param name="obj"></param>
        void Instance_ReceiveHandel(string obj)
        {
            if (obj.IsNullOrEmpty())
            {
                return;
            }
            else
            {
                //BackObj = obj;
            }
        }

        /// <summary>
        /// 处理返回调试指令
        /// </summary>
        /// <param name="order"></param>
        void Read(string order)
        {
            if (!order.Contains("EB 90 11 AF"))
                return;
            if (order.Contains("EB 90 11 AF 01 06 FF"))
                AgingHaveTube = true;
            //string[] dataRecive = order.Split(' ');
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            foreach (Thread a in threadList)
            {
                if (a != null && a.IsAlive)
                    a.Abort();
            }
            this.Close();
        }

        #region 版本号查询
        private void btnVersion_Click(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            TxtVersionClear();

            btnVersion.Enabled = false;

            NetCom3.Instance.ReceiveHandel += GetVersionNum;
            for (int i = 1; i <= 15; i++)
            {
                string boardId = i.ToString("x2");

                NetCom3.Instance.Send(NetCom3.Cover("EB 90 11 FF " + boardId), 5);
                VersionQuery();
            }
            NetCom3.Instance.ReceiveHandel -= GetVersionNum;

            for (int i = 1; i <= 15; i++)
            {
                if (i == 1)
                {
                    txtPhotonVersion.Text = boardVersion[i];
                }
                else if (i == 2)
                {
                    txtCommunicationVersion.Text = boardVersion[i];
                }
                else if (i == 3)
                {
                    txtGripper1Version.Text = boardVersion[i];
                }
                else if (i == 4)
                {
                    txtSimpleVersion.Text = boardVersion[i];
                }
                else if (i == 5)
                {
                    txtWashVersion.Text = boardVersion[i];
                }
                else if (i == 6)
                {
                    txtAlertVersion.Text = boardVersion[i];
                }
                else if (i == 7)
                {
                    txtRefrigerationVersion.Text = boardVersion[i];
                }
                else if (i == 8)
                {
                    txtTrayVersion.Text = boardVersion[i];
                }
                else if (i == 9)
                {
                    txtGripper2Version.Text = boardVersion[i];
                }
                else if (i == 10)
                {
                    txtReagentVersion.Text = boardVersion[i];
                }
                else if (i == 11)
                {
                    txtdispatchI.Text = boardVersion[i];
                }
                else if (i == 12)
                {
                    txtdispatchII.Text = boardVersion[i];
                }
                else if (i == 13)
                {
                    txtTrackI.Text = boardVersion[i];
                }
                else if (i == 14)
                {
                    txtTrackII.Text = boardVersion[i];
                }
                else if (i == 15)
                {
                    txtTrackIII.Text = boardVersion[i];
                }
            }
            btnVersion.Enabled = true;
        }
        /// <summary>
        /// 版本号查询输入重置
        /// </summary>
        private void TxtVersionClear()
        {
            Invoke(new Action(() =>
            {
                txtPhotonVersion.Clear(); //计数器
                txtCommunicationVersion.Clear();//通讯

                txtGripper1Version.Clear();//理杯&抓手1
                txtGripper2Version.Clear();//抓手2

                txtSimpleVersion.Clear();//加样针
                txtReagentVersion.Clear();//加试剂针

                txtWashVersion.Clear();//清洗
                txtAlertVersion.Clear();//报警
                txtRefrigerationVersion.Clear();//制冷
                txtTrayVersion.Clear();//温育盘

                txtdispatchI.Clear();//调度I
                txtdispatchII.Clear();//调度II

                txtTrackI.Clear();//轨道I
                txtTrackII.Clear();//轨道II
                txtTrackIII.Clear();//轨道III
            }));
        }
        /// <summary>
        /// 处理返回指令得到版本号
        /// </summary>
        /// <param name="order"></param>
        private void GetVersionNum(string order)
        {
            if (order.Contains("EB 90 11 FF"))
            {
                string bVersion;//版本号字符串
                int MM; //大版本号
                int NN; //小版本号
                string version = order.Replace(" ", "");
                int pos = version.IndexOf("EB9011FF");
                version = version.Substring(pos, 32);
                version = version.Substring(8);
                int tag = Convert.ToInt32(version.Substring(0, 2), 16); //0X 选择板子
                MM = Convert.ToInt32(version.Substring(2, 2), 16);
                NN = Convert.ToInt32(version.Substring(4, 2), 16);

                bVersion = "V" + tag + "." + MM + "." + NN;

                boardVersion[tag] = bVersion;
            }
        }
        /// <summary>
        /// 版本号超时默认查询
        /// </summary>
        /// <returns></returns>
        public bool VersionQuery()
        {
            DateTime vsDt = DateTime.Now;
            while (!NetCom3.totalOrderFlag)
            {
                if (DateTime.Now.Subtract(vsDt).TotalMilliseconds > 2000)
                {
                    NetCom3.Instance.errorFlag = (int)ErrorState.OverTime;
                    NetCom3.totalOrderFlag = true;
                    NetCom3.DiagnostDone.Set();
                }
                NetCom3.Delay(10);
            }
            if (NetCom3.Instance.errorFlag != (int)ErrorState.Success)
            {
                LogFile.Instance.Write("errorFlag = ： " + NetCom3.Instance.errorFlag + "  *****当前 " + DateTime.Now.ToString("HH - mm - ss"));
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 老化测试
        private void fbtnAgingStart_Click(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cbAgingTest.SelectedIndex < 0)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择测试模块！");
                return;
            }
            if (txtAgingTestNum.Text.Trim() == "" || txtAgingTestNum.Text.Trim() == "0")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入测试次数！");
                return;
            }
            fbtnAgingStart.Enabled = false;
            fbtnAgingStop.Enabled = true;
            txtAgingTestNum.ReadOnly = true;
            txtAgingInfoShow.Clear();
            txtTestErrorShow.Clear();

            agingTestThread = new Thread(agingTestRun);
            agingTestThread.IsBackground = true;
            agingTestThread.Name = "agingTestThread";
            agingTestThread.Start();

            threadList.Add(agingTestThread);
        }

        private void fbtnAgingStop_Click(object sender, EventArgs e)
        {
            if (agingTestThread != null)
                agingTestThread.Abort();

            Invoke(new Action(() =>
            {
                txtAgingTestNum.ReadOnly = false;
            }));
            fbtnAgingStart.Enabled = true;
            fbtnAgingStop.Enabled = false;
        }

        private void ControlIntit()
        {
            threadList.RemoveAll(xy => xy.Name == "AgingTestRun");
            Invoke(new Action(() =>
            {
                txtAgingTestNum.ReadOnly = false;
            }));
            showLog = "";
            fbtnAgingStart.Enabled = true;
            fbtnAgingStop.Enabled = false;
        }
        private void agingTestRun()
        {
            //测试项目
            string testItem = "";
            //测试次数
            int testNum = int.Parse(txtAgingTestNum.Text);
            //剩余测试次数
            int surplusNum = testNum;
            //加试剂选择温育盘位置
            int cbSTrayPosIndex = -1;
            //加样本选择温育盘位置
            int cbRTrayPosIndex = -1;
            Invoke(new Action(() =>
            {
                testItem = cbAgingTest.Text.ToString();
                cbSTrayPosIndex = cbSTrayPos.SelectedIndex;
                cbRTrayPosIndex = cbRTrayPos.SelectedIndex;
                txtSurplusNum.Text = surplusNum.ToString();
            }));
            //orderSend("EB 90 F1 02", 5, "仪器初始化");
            //switch (orderSend("EB 90 F1 02", 5, "仪器初始化"))
            //{
            //    case (int)ErrorState.Success:
            //        break;
            //    default:
            //        ControlIntit();
            //        return;
            //}
            if (testItem == "理杯&&抓手I")
            {
                string log = "";
                if (rbtnRackAbandon.Checked)//暂存盘-》废弃处
                {
                    #region  暂存盘取管到废弃处
                    BeginInvoke(new Action(() =>
                    {
                        txtAgingInfoShow.AppendText("---移管手老化测试开始，暂存盘与废弃处之间移管测试，测试次数：" + testNum.ToString() + "---" + Environment.NewLine + Environment.NewLine);
                    }));
                    while (surplusNum > 0)
                    {
                        log = "抓手I正在暂存盘扔第" + (testNum - surplusNum + 1) + "个管";
                        switch (orderSend("EB 90 31 01 07", 1, log))
                        {
                            case (int)ErrorState.Success:
                                break;
                            case (int)ErrorState.IsKnocked:
                            case (int)ErrorState.IsNull:
                                if (orderSend("EB 90 01 03 00", 5) != 1)
                                {
                                    ControlIntit();
                                    return;
                                }
                                if (orderSend("EB 90 31 01 07", 1) != 1)
                                {
                                    ControlIntit();
                                    return;
                                }
                                break;
                            default:
                                ControlIntit();
                                return;
                        }
                        surplusNum--;
                        BeginInvoke(new Action(() =>
                        {
                            txtSurplusNum.Text = surplusNum.ToString();
                        }));
                    }
                    #endregion
                }
                else if (rbtnRackTray.Checked)//暂存盘-》温育盘
                {
                    #region 暂存盘取管到温育盘
                    int SumNum = 1;//循环几圈
                    int singleLooPNum = 1;//一圈执行几次
                    int TubetempNum = 1;//夹管临时计数
                    int CurrentReactPos = 1;//当前孔位
                    if (!reactTrayTubeClear())
                    {
                        ControlIntit();
                        return;
                    }
                    BeginInvoke(new Action(() =>
                    {
                        txtAgingInfoShow.AppendText("---移管手老化测试开始，暂存盘与温育盘之间移管测试，测试次数：" + testNum.ToString() + "---" + Environment.NewLine + Environment.NewLine);
                    }));
                    if (testNum <= ReactTrayNum)
                    {
                        SumNum = 1;
                        singleLooPNum = testNum;
                    }
                    else
                    {
                        SumNum = testNum % ReactTrayNum == 0 ? testNum / ReactTrayNum : testNum / ReactTrayNum + 1;
                        singleLooPNum = ReactTrayNum;
                    }
                    for (int j = 0; j < SumNum; j++)
                    {
                        TubetempNum = 1;
                        CurrentReactPos = 1;
                        while (TubetempNum <= singleLooPNum)
                        {
                            log = "抓手I夹新管到温育盘" + CurrentReactPos.ToString() + "位置";
                            bool sendOnceAgain = true;
                            sendAgain:
                            switch (orderSend("EB 90 31 01 01 " + CurrentReactPos.ToString("x2"), 1, log))
                            {
                                case (int)ErrorState.Success:
                                    break;
                                case (int)ErrorState.IsKnocked:
                                case (int)ErrorState.IsNull:
                                    if (sendOnceAgain)
                                    {
                                        sendOnceAgain = false;
                                        if (orderSend("EB 90 01 03 00", 5) != 1)
                                        {
                                            ControlIntit();
                                            return;
                                        }
                                        goto sendAgain;
                                    }
                                    ControlIntit();
                                    return;
                                case (int)ErrorState.putKnocked:
                                    if (sendOnceAgain)
                                    {
                                        sendOnceAgain = false;
                                        log = "抓手I在温育盘" + CurrentReactPos.ToString() + "号位扔管";
                                        switch (orderSend("EB 90 31 01 05 " + CurrentReactPos.ToString("x2"), 1, log))
                                        {
                                            case (int)ErrorState.Success:
                                            case (int)ErrorState.IsNull:
                                                break;
                                            default:
                                                ControlIntit();
                                                return;
                                        }
                                        goto sendAgain;
                                    }
                                    ControlIntit();
                                    return;
                                default:
                                    ControlIntit();
                                    return;
                            }
                            CurrentReactPos++;
                            TubetempNum++;
                            surplusNum--;
                            BeginInvoke(new Action(() =>
                            {
                                txtSurplusNum.Text = surplusNum.ToString();
                            }));
                            OperateIniFile.WriteIniData("ReactTrayInfo", "no" + CurrentReactPos, "1", iniPathReactTrayInfo);
                        }
                        TubetempNum = 1;
                        CurrentReactPos = 1;
                        while (CurrentReactPos <= singleLooPNum)
                        {
                            #region 温育盘扔管
                            log = "抓手I在温育盘" + CurrentReactPos.ToString() + "号位扔管";
                            bool sendOnceAgain = true;
                            sendAgain:
                            switch (orderSend("EB 90 31 01 05 " + CurrentReactPos.ToString("x2"), 1, log))
                            {
                                case (int)ErrorState.Success:
                                    break;
                                case (int)ErrorState.IsKnocked:
                                case (int)ErrorState.IsNull:
                                    if (sendOnceAgain)
                                    {
                                        sendOnceAgain = false;
                                        if (orderSend("EB 90 01 03 00", 5) != 1)
                                        {
                                            ControlIntit();
                                            return;
                                        }
                                        goto sendAgain;
                                    }
                                    ControlIntit();
                                    return;
                                default:
                                    ControlIntit();
                                    return;
                            }
                            OperateIniFile.WriteIniData("ReactTrayInfo", "no" + CurrentReactPos, "0", iniPathReactTrayInfo);
                            #endregion
                            CurrentReactPos++;
                        }
                        singleLooPNum = SumNum - (j + 2) == 0 ? testNum - (j + 1) * ReactTrayNum : ReactTrayNum;
                    }
                    #endregion
                }
            }
            else if (testItem == "抓手II")
            {
                if (rbtnWashAbandon.Checked)//清洗盘-》废弃处
                {
                    #region 暂存盘-》温育盘-》清洗盘-》废弃处
                    if (!twoTrayTubeClear())
                    {
                        ControlIntit();
                        return;
                    }
                    BeginInvoke(new Action(() =>
                    {
                        txtAgingInfoShow.AppendText("---移管手老化测试开始，清洗盘扔管到废弃处测试，测试次数：" + testNum.ToString() + "---" + Environment.NewLine + Environment.NewLine);
                    }));
                    List<string> tubeInReactTray = new List<string>();
                    int SumNum, singleLooPNum = testNum;
                    if (testNum <= WashTrayNum)
                    {
                        SumNum = 1;
                    }
                    else
                    {
                        SumNum = testNum % WashTrayNum == 0 ? testNum / WashTrayNum : testNum / WashTrayNum + 1;
                        singleLooPNum = WashTrayNum;
                    }
                    //暂存-》温育
                    BeginInvoke(new Action(() =>
                    {
                        int i;
                        string log = "";
                        for (int x = 1; x <= testNum; x++)
                        {
                            i = x % ReactTrayNum == 0 ? ReactTrayNum : x % ReactTrayNum;
                            log = "抓手I夹新管到温育盘" + i.ToString() + "号位";
                            bool sendOnceAgain = true;
                            sendAgain:
                            switch (orderSend("EB 90 31 01 01 " + i.ToString("X2"), 1, log))
                            {
                                case (int)ErrorState.Success:
                                    break;
                                case (int)ErrorState.IsKnocked:
                                case (int)ErrorState.IsNull:
                                    if (sendOnceAgain)
                                    {
                                        sendOnceAgain = false;
                                        if (orderSend("EB 90 01 03 00", 5) != 1)
                                        {
                                            ControlIntit();
                                            return;
                                        }
                                        goto sendAgain;
                                    }
                                    ControlIntit();
                                    return;
                                case (int)ErrorState.putKnocked:
                                    if (sendOnceAgain)
                                    {
                                        log = "抓手I在温育盘" + i.ToString() + "号位扔管";
                                        switch (orderSend("EB 90 31 01 05 " + i.ToString("X2"), 1, log))
                                        {
                                            case (int)ErrorState.Success:
                                            case (int)ErrorState.IsNull:
                                                break;
                                            default:
                                                ControlIntit();
                                                return;
                                        }
                                        goto sendAgain;
                                    }
                                    ControlIntit();
                                    return;
                                default:
                                    ControlIntit();
                                    return;
                            }
                            OperateIniFile.WriteIniData("ReactTrayInfo", "no" + i, "1", iniPathReactTrayInfo);
                            tubeInReactTray.Add(i.ToString("X2"));
                            while (tubeInReactTray.Count >= 150)
                            {
                                NetCom3.Delay(20000);
                            }
                            if (!agingTestThread.IsAlive)
                                break;
                        }
                    }));
                    //温育盘-》清洗盘-》废弃处
                    while (surplusNum > 0)
                    {
                        string log = "";

                        int singleLoopNum = surplusNum >= WashTrayNum ? WashTrayNum : surplusNum;
                        //温育盘-》清洗盘
                        for (int i = 0; i < singleLoopNum; i++)
                        {
                            while (tubeInReactTray.Count == 0)
                            {
                                NetCom3.Delay(500);
                            }
                            string holl = tubeInReactTray.FirstOrDefault();
                            log = "抓手II在温育盘" + Convert.ToInt32(holl, 16) + "号位取管到清洗盘";
                            bool sendOnceAgain = true;
                            sendAgain:
                            switch (orderSend("EB 90 31 11 02 " + holl, 11, log))
                            {
                                case (int)ErrorState.Success:
                                    break;
                                case (int)ErrorState.IsKnocked:
                                    if (sendOnceAgain)
                                    {
                                        sendOnceAgain = false;
                                        if (orderSend("EB 90 21 03 00", 5) != 1)
                                        {
                                            ControlIntit();
                                            return;
                                        }
                                        goto sendAgain;
                                    }
                                    ControlIntit();
                                    return;
                                default:
                                    ControlIntit();
                                    return;
                            }
                            tubeInReactTray.RemoveAt(0);
                            OperateIniFile.WriteIniData("ReactTrayInfo", "no" + Convert.ToInt32(holl, 16), "0", iniPathReactTrayInfo);
                            log = "清洗盘逆时针旋转一位";
                            switch (orderSend("EB 90 31 03 01 01", 2, log))
                            {
                                case (int)ErrorState.Success:
                                    break;
                                default:
                                    ControlIntit();
                                    return;
                            }
                        }
                        if (singleLoopNum != WashTrayNum)
                        {
                            log = "清洗盘顺时针旋转" + singleLoopNum + "位";
                            switch (orderSend("EB 90 31 03 01 " + ((-1) * singleLoopNum).ToString("X2").Substring(6, 2), 2, log))
                            {
                                case (int)ErrorState.Success:
                                    break;
                                default:
                                    ControlIntit();
                                    return;
                            }
                        }
                        //清洗盘-》废弃处
                        for (int i = 0; i < singleLooPNum; i++)
                        {
                            log = "抓手II在清洗盘 #" + (i + 1) + " 取放管处扔废管";
                            bool sendOnceAgain = true;
                            sendAgain:
                            switch (orderSend("EB 90 31 11 04 06", 11, log))
                            {
                                case (int)ErrorState.Success:
                                case (int)ErrorState.IsNull:
                                    break;
                                case (int)ErrorState.IsKnocked:
                                    if (sendOnceAgain)
                                    {
                                        sendOnceAgain = false;
                                        if (orderSend("EB 90 21 03 00", 5) != 1)
                                        {
                                            ControlIntit();
                                            return;
                                        }
                                        goto sendAgain;
                                    }
                                    ControlIntit();
                                    return;
                                default:
                                    ControlIntit();
                                    return;
                            }
                            log = "清洗盘逆时针旋转一位";
                            switch (orderSend("EB 90 31 03 01 01", 2, log))
                            {
                                case (int)ErrorState.Success:
                                    break;
                                default:
                                    ControlIntit();
                                    return;
                            }
                            surplusNum--;
                            BeginInvoke(new Action(() =>
                            {
                                txtSurplusNum.Text = surplusNum.ToString();
                            }));
                        }
                    }
                    #endregion
                }
                else if (rbtnTrayWash.Checked)
                {
                    #region 温育盘-》清洗盘
                    if (!twoTrayTubeClear())
                    {
                        ControlIntit();
                        return;
                    }
                    BeginInvoke(new Action(() =>
                    {
                        txtAgingInfoShow.AppendText("---移管手老化测试开始，温育盘和清洗盘之间移管测试，测试次数：" + testNum.ToString() + "---" + Environment.NewLine + Environment.NewLine);
                    }));
                    //暂存盘取40管放到温育盘
                    //暂存盘取管放到温育盘
                    List<string> tubeInReactTray = new List<string>();
                    int SumNum, singleLooPNum = testNum;
                    Random random = new Random();//清洗-》温育 随机
                    if (testNum <= WashTrayNum)
                    {
                        SumNum = 1;
                    }
                    else
                    {
                        SumNum = testNum % WashTrayNum == 0 ? testNum / WashTrayNum : testNum / WashTrayNum + 1;
                        singleLooPNum = WashTrayNum;
                    }
                    //暂存-》温育
                    BeginInvoke(new Action(() =>
                    {
                        string log2 = "";
                        int i = 1;
                        for (int x = 1; x <= singleLooPNum; x++)
                        {
                            i = i + 114;
                            i = i % 150 == 0 ? 150 : i % 150;
                            log2 = "抓手I夹新管到温育盘" + i.ToString() + "号位";
                            bool sendOnceAgain = true;
                            sendAgain:
                            switch (orderSend("EB 90 31 01 01 " + i.ToString("X2"), 1, log2))
                            {
                                case (int)ErrorState.Success:
                                    break;
                                case (int)ErrorState.IsKnocked:
                                case (int)ErrorState.IsNull:
                                    if (sendOnceAgain)
                                    {
                                        sendOnceAgain = false;
                                        if (orderSend("EB 90 01 03 00", 5) != 1)
                                        {
                                            ControlIntit();
                                            return;
                                        }
                                        goto sendAgain;
                                    }
                                    ControlIntit();
                                    return;
                                case (int)ErrorState.putKnocked:
                                    if (sendOnceAgain)
                                    {
                                        log2 = "抓手I在温育盘" + i.ToString() + "号位扔管";
                                        switch (orderSend("EB 90 31 01 05 " + i.ToString("X2"), 1, log2))
                                        {
                                            case (int)ErrorState.Success:
                                            case (int)ErrorState.IsNull:
                                                break;
                                            default:
                                                ControlIntit();
                                                return;
                                        }
                                        goto sendAgain;
                                    }
                                    ControlIntit();
                                    return;
                                default:
                                    ControlIntit();
                                    return;
                            }
                            OperateIniFile.WriteIniData("ReactTrayInfo", "no" + i, "1", iniPathReactTrayInfo);
                            tubeInReactTray.Add(i.ToString("X2"));
                            if (!agingTestThread.IsAlive)
                                break;
                        }
                    }));
                    //温育盘-》清洗盘
                    string log = "";
                    for (int i = 0; i < singleLooPNum; i++)
                    {
                        while (tubeInReactTray.Count == 0)
                        {
                            NetCom3.Delay(500);
                        }
                        string holl = tubeInReactTray.FirstOrDefault();
                        log = "抓手II在温育盘" + Convert.ToInt32(holl, 16) + "号位取管到清洗盘";
                        bool sendOnceAgain = true;
                        sendAgain:
                        switch (orderSend("EB 90 31 11 02 " + holl, 11, log))
                        {
                            case (int)ErrorState.Success:
                                break;
                            case (int)ErrorState.IsKnocked:
                                if (sendOnceAgain)
                                {
                                    sendOnceAgain = false;
                                    if (orderSend("EB 90 21 03 00", 5) != 1)
                                    {
                                        ControlIntit();
                                        return;
                                    }
                                    goto sendAgain;
                                }
                                ControlIntit();
                                return;
                            default:
                                ControlIntit();
                                return;
                        }
                        tubeInReactTray.RemoveAt(0);
                        OperateIniFile.WriteIniData("ReactTrayInfo", "no" + Convert.ToInt32(holl, 16), "0", iniPathReactTrayInfo);
                        log = "清洗盘逆时针旋转一位";
                        switch (orderSend("EB 90 31 03 01 01", 2, log))
                        {
                            case (int)ErrorState.Success:
                                break;
                            default:
                                ControlIntit();
                                return;
                        }
                    }
                    if (singleLooPNum != WashTrayNum)
                    {
                        log = "清洗盘顺时针旋转" + singleLooPNum + "位";
                        switch (orderSend("EB 90 31 03 01 " + ((-1) * singleLooPNum).ToString("X2").Substring(6, 2), 2, log))
                        {
                            case (int)ErrorState.Success:
                                break;
                            default:
                                ControlIntit();
                                return;
                        }
                    }
                    for (int i = 0; i < SumNum; i++)
                    {
                        singleLooPNum = surplusNum > WashTrayNum ? WashTrayNum : surplusNum;
                        List<string> randomHollList = new List<string>();
                        //清洗盘-》温育盘
                        for (int x = 1; x <= singleLooPNum; x++)
                        {
                            int raHoll = random.Next(1, 151);
                            while (randomHollList.FindIndex(xy => xy == raHoll.ToString("X2")) != -1)
                            {
                                raHoll = random.Next(1, 151);
                            }
                            log = "抓手II在清洗盘取放管位取管到温育盘" + raHoll + "号位";
                            bool sendOnceAgain = true;
                            sendAgain:
                            switch (orderSend("EB 90 31 11 03 " + raHoll.ToString("X2") + " 02", 11, log))
                            {
                                case (int)ErrorState.Success:
                                    break;
                                case (int)ErrorState.IsKnocked:
                                case (int)ErrorState.IsNull:
                                    if (sendOnceAgain)
                                    {
                                        sendOnceAgain = false;
                                        if (orderSend("EB 90 21 03 00", 5) != 1)
                                        {
                                            ControlIntit();
                                            return;
                                        }
                                        goto sendAgain;
                                    }
                                    ControlIntit();
                                    return;
                                default:
                                    ControlIntit();
                                    return;
                            }
                            randomHollList.Add(raHoll.ToString("X2"));
                            OperateIniFile.WriteIniData("ReactTrayInfo", "no" + raHoll, "1", iniPathReactTrayInfo);
                            log = "清洗盘逆时针旋转一位";
                            switch (orderSend("EB 90 31 03 01 01", 2, log))
                            {
                                case (int)ErrorState.Success:
                                    break;
                                default:
                                    ControlIntit();
                                    return;
                            }
                        }
                        //温育盘-》清洗盘
                        for (int x = 1; x <= singleLooPNum; x++)
                        {
                            string raHoll = randomHollList.FirstOrDefault();
                            log = "抓手II在温育盘" + Convert.ToInt32(raHoll, 16) + "号位取管放到清洗盘";
                            bool sendOnceAgain = true;
                            sendAgain:
                            switch (orderSend("EB 90 31 11 02 " + raHoll, 11, log))
                            {
                                case (int)ErrorState.Success:
                                    break;
                                case (int)ErrorState.IsKnocked:
                                    if (sendOnceAgain)
                                    {
                                        sendOnceAgain = false;
                                        if (orderSend("EB 90 21 03 00", 5) != 1)
                                        {
                                            ControlIntit();
                                            return;
                                        }
                                        goto sendAgain;
                                    }
                                    ControlIntit();
                                    return;
                                default:
                                    ControlIntit();
                                    return;
                            }
                            randomHollList.RemoveAt(0);
                            OperateIniFile.WriteIniData("ReactTrayInfo", "no" + Convert.ToInt32(raHoll, 16), "0", iniPathReactTrayInfo);
                            log = "清洗盘逆时针旋转一位";
                            switch (orderSend("EB 90 31 03 01 01", 2, log))
                            {
                                case (int)ErrorState.Success:
                                    break;
                                default:
                                    ControlIntit();
                                    return;
                            }
                            surplusNum--;
                            BeginInvoke(new Action(() =>
                            {
                                txtSurplusNum.Text = surplusNum.ToString();
                            }));
                        }
                    }
                    #endregion
                }
            }
            else if (testItem == "加样-针I")
            {
                #region 加样-针I
                if (txtSPos1.Text == "" || txtSPos2.Text == "")
                {
                    frmMsgShow.MessageShow("仪器调试", "请输入测试样本位置！");
                    return;
                }
                if (cbSTrayPosIndex < 0)
                {
                    frmMsgShow.MessageShow("仪器调试", "请选择测试温育盘位置！");
                    return;
                }
                BeginInvoke(new Action(() =>
                {
                    txtAgingInfoShow.AppendText("---加样-针I老化测试开始，加样测试，测试次数：" + testNum.ToString() + "---" + Environment.NewLine + Environment.NewLine);
                }));
                int samplePos1 = 1;
                int samplePos2 = 1;
                string reactTrayPos = "";
                string SEmergency = "00";//调度，00-普通，01-急诊
                string trackEmergency = "01";//轨道，01-普通，02-急诊
                string log = "";
                Random random = new Random();
                Invoke(new Action(() =>
                {
                    samplePos1 = int.Parse(txtSPos1.Text.ToString());
                    samplePos2 = int.Parse(txtSPos2.Text.ToString());
                    reactTrayPos = cbSTrayPos.Text.ToString();
                }));
                if (cbSTrayPosIndex != 2)
                {
                    reactTrayPos = int.Parse(reactTrayPos).ToString("X2");
                }
                if (chkSampleEmergency.Checked)
                {
                    SEmergency = "01";
                    trackEmergency = "02";
                    //样本加急诊指令暂时没有跳过
                    //ControlIntit();
                    //return;
                }
                //测试次数，一次一圈，调度-》轨道-》加样-》调度
                for (int i = 0; i < testNum; i++)
                {
                    log = "推送测试样本架到扫码I前位置";
                    //推送测试样本架到光电
                    switch (orderSend("EB 90 31 05 01 01 00", 15, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    log = "推送测试样本架到调度等待区";
                    //扫码，并推送到调度等待区
                    switch (orderSend("EB 90 31 05 02", 15, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    log = "推送测试样本架到轨道等待区";
                    //测试样本架推送到轨道等待区
                    switch (orderSend("EB 90 31 05 04 " + SEmergency, 15, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    log = "推送测试样本架到加样区";
                    //推送样本架，轨道等待区到加样区
                    switch (orderSend("EB 90 31 06 " + trackEmergency + " 01 02", 6, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    //10个位置依次加样
                    for (int x = samplePos1; x <= samplePos2; x++)
                    {
                        if (cbSTrayPosIndex == 2)
                        {
                            reactTrayPos = random.Next(1, 151).ToString("X2");
                        }
                        log = "加样针I取样本架" + x + "号样本到温育盘" + Convert.ToInt32(reactTrayPos, 16) + "号位置";
                        //需要修改，没定好 aa bb cc=>aa bb cc dd
                        switch (orderSend("EB 90 31 02 01 " + x.ToString("X2") + " " + reactTrayPos + " 1C " + SEmergency, 0, log))
                        {
                            case (int)ErrorState.Success:
                                break;
                            default:
                                ControlIntit();
                                return;
                        }
                        NetCom3.Delay(1000);
                    }
                    log = "推送测试样本架到常规区";
                    //加样区推送到回归区
                    switch (orderSend("EB 90 31 06 " + trackEmergency + " 02 03", 6, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    log = "推送测试样本架到回归区";
                    switch (orderSend("EB 90 31 06 " + trackEmergency + " 03 05", 6, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    //回归区回归调度待检区
                    log = "推送测试样本架到后皮带等待区";
                    switch (orderSend("EB 90 31 06 03 01 02", 6, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    log = "推送测试样本架到调度返回链盘";
                    switch (orderSend("EB 90 31 06 03 02 03", 6, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    //回收链盘带回待检区
                    log = "推送测试样本架到回收链盘";
                    switch (orderSend("EB 90 31 07 01 " + (27).ToString("X2"), 7, log))//应该是27格
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    log = "推送测试样本架到待检区";
                    switch (orderSend("EB 90 31 07 02 01", 7, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    surplusNum--;
                    BeginInvoke(new Action(() =>
                    {
                        txtSurplusNum.Text = surplusNum.ToString();
                    }));
                }
                #endregion
            }
            else if (testItem == "加试剂-针II")
            {
                #region 加试剂-针II
                if (txtReagentPos.Text == "")
                {
                    frmMsgShow.MessageShow("仪器调试", "请输入测试试剂位置！");
                    return;
                }
                if (cbRTrayPosIndex < 0)
                {
                    frmMsgShow.MessageShow("仪器调试", "请选择测试温育盘位置！");
                    return;
                }
                BeginInvoke(new Action(() =>
                {
                    txtAgingInfoShow.AppendText("---加试剂-针II老化测试开始，加液测试，测试次数：" + testNum.ToString() + "---" + Environment.NewLine + Environment.NewLine);
                }));
                int reagentPos = 1;
                string reactTraypos = "";
                string log = "";
                Random random = new Random();
                Invoke(new Action(() =>
                {
                    reagentPos = int.Parse(txtReagentPos.Text);
                    reactTraypos = cbRTrayPos.Text.ToString();
                }));
                if (cbRTrayPosIndex != 2)
                {
                    reactTraypos = int.Parse(reactTraypos).ToString("X2");
                }
                for (int i = 0; i < testNum; i++)
                {
                    if (cbRTrayPosIndex == 2)
                    {
                        reactTraypos = random.Next(1, 151).ToString("X2");
                    }
                    log = "加试剂-针II取试剂盘" + reagentPos + "号位置R1到温育盘" + Convert.ToInt32(reactTraypos, 16) + "号位置";
                    //加R1
                    switch (orderSend("EB 90 31 12 03 " + reagentPos.ToString("X2") + " " + reactTraypos + " 1C 1C 20", 10, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    NetCom3.Delay(500);
                    log = "加试剂-针II取试剂盘" + reagentPos + "号位置R2到温育盘" + Convert.ToInt32(reactTraypos, 16) + "号位置";
                    //加R2
                    switch (orderSend("EB 90 31 12 04 " + reagentPos.ToString("X2") + " " + reactTraypos + " 1C 1C 20", 10, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    NetCom3.Delay(500);
                    log = "加试剂-针II取试剂盘" + reagentPos + "号位置R3到温育盘" + Convert.ToInt32(reactTraypos, 16) + "号位置";
                    //加R3
                    switch (orderSend("EB 90 31 12 05 " + reagentPos.ToString("X2") + " " + reactTraypos + " 1C 1C 20", 10, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    NetCom3.Delay(500);
                    log = "加试剂-针II取试剂盘" + reagentPos + "号位置稀释液到温育盘" + Convert.ToInt32(reactTraypos, 16) + "号位置";
                    //加稀释液
                    switch (orderSend("EB 90 31 12 06 " + reagentPos.ToString("X2") + " " + reactTraypos + " 1C 1C 20", 10, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    NetCom3.Delay(500);
                    log = "加试剂-针II取试剂盘" + reagentPos + "号位置磁珠到温育盘" + Convert.ToInt32(reactTraypos, 16) + "号位置";
                    //加磁珠
                    switch (orderSend("EB 90 31 12 07 " + reagentPos.ToString("X2") + " " + reactTraypos + " 1C 1C 20", 10, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    surplusNum--;
                    BeginInvoke(new Action(() =>
                    {
                        txtSurplusNum.Text = surplusNum.ToString();
                    }));
                }
                #endregion
            }
            else if (testItem == "清洗盘")
            {
                #region 清洗盘
                if (txtTubeNum.Text == "")
                {
                    frmMsgShow.MessageShow("仪器调试", "请输入放入清洗盘管数量！");
                    return;
                }
                if (txtTubeInterval.Text == "")
                {
                    frmMsgShow.MessageShow("仪器调试", "请输入放入清洗盘管间隔！");
                    return;
                }
                if ((int.Parse(txtTubeNum.Text) - 1) * int.Parse(txtTubeInterval.Text) + int.Parse(txtTubeNum.Text) > 40)
                {
                    frmMsgShow.MessageShow("仪器调试", "请输入清洗盘装得下的合理数量！");
                    return;
                }
                if (!twoTrayTubeClear())
                {
                    ControlIntit();
                    return;
                }
                BeginInvoke(new Action(() =>
                {
                    txtAgingInfoShow.AppendText("---清洗盘老化测试开始，测试次数：" + testNum.ToString() + "---" + Environment.NewLine + Environment.NewLine);
                }));
                int tubeNum = 1;
                int tubeInterval = 1;
                int[] washTubeInHoll = new int[WashTrayNum];
                List<string> tubeInReactTray = new List<string>();
                Invoke(new Action(() =>
                {
                    tubeNum = int.Parse(txtTubeNum.Text);
                    tubeInterval = int.Parse(txtTubeInterval.Text) + 1;
                }));
                //暂存-》温育
                BeginInvoke(new Action(() =>
                {
                    string log = "";
                    for (int i = 1; i <= tubeNum; i++)
                    {
                        log = "抓手I夹新管到温育盘" + i.ToString() + "号位";
                        bool sendOnceAgain = true;
                        sendAgain:
                        switch (orderSend("EB 90 31 01 01 " + i.ToString("X2"), 1, log))
                        {
                            case (int)ErrorState.Success:
                                break;
                            case (int)ErrorState.IsKnocked:
                            case (int)ErrorState.IsNull:
                                if (sendOnceAgain)
                                {
                                    sendOnceAgain = false;
                                    if (orderSend("EB 90 01 03 00", 5) != 1)
                                    {
                                        ControlIntit();
                                        return;
                                    }
                                    goto sendAgain;
                                }
                                ControlIntit();
                                return;
                            case (int)ErrorState.putKnocked:
                                if (sendOnceAgain)
                                {
                                    log = "抓手I在温育盘" + i.ToString() + "号位扔管";
                                    switch (orderSend("EB 90 31 01 05 " + i.ToString("X2"), 1, log))
                                    {
                                        case (int)ErrorState.Success:
                                        case (int)ErrorState.IsNull:
                                            break;
                                        default:
                                            ControlIntit();
                                            return;
                                    }
                                    goto sendAgain;
                                }
                                ControlIntit();
                                return;
                            default:
                                ControlIntit();
                                return;
                        }
                        OperateIniFile.WriteIniData("ReactTrayInfo", "no" + i, "1", iniPathReactTrayInfo);
                        tubeInReactTray.Add(i.ToString("X2"));
                        if (!agingTestThread.IsAlive)
                            break;
                    }
                }));
                //温育盘-》清洗盘
                for (int i = 0; i < tubeNum; i++)
                {
                    while (tubeInReactTray.Count == 0)
                    {
                        NetCom3.Delay(500);
                    }
                    string holl = tubeInReactTray.FirstOrDefault();
                    string log = "";
                    log = "抓手II在温育盘" + Convert.ToInt32(holl, 16) + "号位取管到清洗盘";
                    bool sendOnceAgain = true;
                    sendAgain:
                    switch (orderSend("EB 90 31 11 02 " + holl, 11, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        case (int)ErrorState.IsKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                if (orderSend("EB 90 21 03 00", 5) != 1)
                                {
                                    ControlIntit();
                                    return;
                                }
                                goto sendAgain;
                            }
                            ControlIntit();
                            return;
                        default:
                            ControlIntit();
                            return;
                    }
                    tubeInReactTray.RemoveAt(0);
                    washTubeInHoll[0] = 1;
                    OperateIniFile.WriteIniData("ReactTrayInfo", "no" + Convert.ToInt32(holl, 16), "0", iniPathReactTrayInfo);
                    log = "清洗盘逆时针旋转" + tubeInterval + "位";
                    switch (orderSend("EB 90 31 03 01 " + tubeInterval.ToString("X2"), 2, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    int[] temp = new int[WashTrayNum];
                    washTubeInHoll.CopyTo(temp, 0);
                    for (int k = 1; k <= WashTrayNum; k++)
                    {
                        int tempNum = (k + tubeInterval) > 40 ? (k + tubeInterval) % 40 : (k + tubeInterval);
                        washTubeInHoll[tempNum - 1] = temp[k - 1];
                    }
                }
                #region 清洗盘指令
                string read = "0";//是否进行读数
                string AddSubstrate = "0";//是否加底物标志位
                string[] LiquidInjectionFlag = new string[3];//注液标志位
                string ImbibitionFlag = "00";//吸液标志位
                #endregion
                for (int i = 0; i < testNum; i++)
                {
                    #region 清洗盘指令
                    //读数
                    if (washTubeInHoll[29] == 1)
                        read = "11";
                    else
                        read = "10";
                    //加底物
                    if (washTubeInHoll[23] == 1)
                        AddSubstrate = "1";
                    else
                        AddSubstrate = "0";
                    //吸液
                    if (washTubeInHoll[21] == 1 || washTubeInHoll[16] == 1 || washTubeInHoll[11] == 1 || washTubeInHoll[6] == 1)
                        ImbibitionFlag = "01";
                    else
                        ImbibitionFlag = "00";
                    //注液
                    if (washTubeInHoll[7] == 1)
                        LiquidInjectionFlag[0] = "1";
                    else
                        LiquidInjectionFlag[0] = "0";
                    if (washTubeInHoll[12] == 1)
                        LiquidInjectionFlag[1] = "1";
                    else
                        LiquidInjectionFlag[1] = "0";
                    if (washTubeInHoll[17] == 1)
                        LiquidInjectionFlag[2] = "1";
                    else
                        LiquidInjectionFlag[2] = "0";
                    string order = "EB 90 31 03 03 " + ImbibitionFlag + " " + LiquidInjectionFlag[0] +
                        LiquidInjectionFlag[1] + " " + LiquidInjectionFlag[2] + AddSubstrate + " " + read;
                    string log = "清洗盘 ";
                    if (ImbibitionFlag == "01")
                        log += "抽液、";
                    if (LiquidInjectionFlag[0] == "1")
                        log += "#1注液、";
                    if (LiquidInjectionFlag[1] == "1")
                        log += "#2注液、";
                    if (LiquidInjectionFlag[2] == "1")
                        log += "#3注液、";
                    if (AddSubstrate == "1")
                        log += "加底物、";
                    if (read == "11")
                        log += "读数.";
                    if (log != "清洗盘 ")
                    {
                        log = log.Substring(0, log.Length - 1);
                        BeginInvoke(new Action(() =>
                        {
                            txtAgingInfoShow.AppendText(Environment.NewLine + DateTime.Now.ToString("HH:mm:ss->") + log);
                        }));
                        switch (orderSend(order, 2, log))
                        {
                            case (int)ErrorState.Success:
                                break;
                            default:
                                ControlIntit();
                                return;
                        }
                    }
                    #endregion
                    NetCom3.Delay(2000);
                    log = "清洗盘逆时针旋转一位";
                    switch (orderSend("EB 90 31 03 01 01", 2, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            ControlIntit();
                            return;
                    }
                    int[] temp = new int[WashTrayNum];
                    washTubeInHoll.CopyTo(temp, 0);
                    for (int k = 1; k <= WashTrayNum; k++)
                    {
                        int tempNum = (k + 1) > 40 ? (k + 1) % 40 : (k + 1);
                        washTubeInHoll[tempNum - 1] = temp[k - 1];
                    }
                    surplusNum--;
                    BeginInvoke(new Action(() =>
                    {
                        txtSurplusNum.Text = surplusNum.ToString();
                    }));
                }
                #endregion
            }
            else if (testItem == "轨道&&调度")
            {
                if (rbtnTrackDispatch.Checked)
                {
                    ;
                }
                if (rbtnTrack.Checked)
                {
                    ;
                }
                if (rbtnDispatch.Checked)
                {
                    ;
                }
            }
            string itemSelectFlag = "";
            Invoke(new Action(() =>
            {
                itemSelectFlag = txtAgingInfoShow.Lines.ElementAtOrDefault(0);
            }));
            if (itemSelectFlag == "" || itemSelectFlag == null)
            {
                ControlIntit();
                return;
            }
            BeginInvoke(new Action(() =>
            {
                txtAgingInfoShow.AppendText(Environment.NewLine + "--------------------------测试完成--------------------------");
                txtAgingTestNum.ReadOnly = false;
            }));
            ControlIntit();
        }

        #region 指令发送与处理
        /// <summary>
        /// 指令发送
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderType">发送指令类型，加样（0）加试剂（10），加新管（1）移管（11），清洗（2）,
        /// 混匀内圈（4），混匀外圈（14），仪器调试（5），样本调度区（15），轨道（6）,回收链盘-轨道（7）</param>
        /// <returns>0-准备发送,1-成功 2-发送失败 3-接收失败 4-抓管撞管（撞针） 5-抓空 6-混匀异常 7-放管撞管 8-理杯机缺管 9-发送超时</returns>
        private int orderSend(string order, int orderType, string log = "")
        {
            if (log != "")
                BeginInvoke(new Action(() =>
                {
                    txtAgingInfoShow.AppendText(DateTime.Now.ToString("HH:mm:ss->") + log + Environment.NewLine);
                }));
            int sendNum = 0;
            int errorFlag;
            sendAgain:
            errorFlag = (int)ErrorState.Success;
            NetCom3.Instance.Send(NetCom3.Cover(order), orderType);
            switch (orderType)
            {
                case 1: //理杯&抓手I
                    if (!NetCom3.Instance.MoveQuery())
                    {
                        errorFlag = NetCom3.Instance.MoverrorFlag;
                    }
                    break;
                case 11: //抓手II
                    if (!NetCom3.Instance.Move2Query())
                    {
                        errorFlag = NetCom3.Instance.Move2rrorFlag;
                    }
                    break;
                case 0: //加样-针I
                    if (!NetCom3.Instance.SPQuery())
                    {
                        errorFlag = NetCom3.Instance.AdderrorFlag;
                    }
                    break;
                case 10: //加试剂-针II
                    if (!NetCom3.Instance.SP2Query())
                    {
                        errorFlag = NetCom3.Instance.Add2errorFlag;
                    }
                    break;
                case 2: //清洗
                    if (!NetCom3.Instance.WashQuery())
                    {
                        errorFlag = NetCom3.Instance.WasherrorFlag;
                    }
                    break;
                case 5: //调试
                    if (!NetCom3.Instance.SingleQuery())
                    {
                        errorFlag = NetCom3.Instance.errorFlag;
                    }
                    break;
                case 4: //混匀内圈
                    if (!NetCom3.Instance.MixQuery())
                    {
                        errorFlag = NetCom3.Instance.MixerrorFlag;
                    }
                    break;
                case 14: //混匀外圈
                    if (!NetCom3.Instance.Mix2Query())
                    {
                        errorFlag = NetCom3.Instance.Mix2errorFlag;
                    }
                    break;
                case 15: //调度-样本添加区
                    if (!NetCom3.Instance.SampleAdditionQuery())
                    {
                        errorFlag = NetCom3.Instance.SampleAdditionerrorFlag;
                    }
                    break;
                case 6: //轨道
                    if (!NetCom3.Instance.TrackStripQuery())
                    {
                        errorFlag = NetCom3.Instance.TrackStriperrorFlag;
                    }
                    break;
                case 7: //回收链盘-轨道
                    if (!NetCom3.Instance.CapstanStripQuery())
                    {
                        errorFlag = NetCom3.Instance.CapstanStriperrorFlag;
                    }
                    break;
                default:
                    break;
            }
            if (errorFlag != (int)ErrorState.Success && (agingTestThread != null && agingTestThread.IsAlive))
            {
                if (log != "")
                {
                    if (log.Contains("清管") && errorFlag == (int)ErrorState.IsNull)
                        return errorFlag;
                    BeginInvoke(new Action(() =>
                    {
                        txtTestErrorShow.AppendText(DateTime.Now.ToString("HH:mm:ss->") + log + "——" + State[errorFlag] + Environment.NewLine);
                    }));
                }
                if (errorFlag == 0 || errorFlag == 2)//重发
                {
                    sendNum++;
                    if (sendNum < 2)
                    {
                        goto sendAgain;
                    }
                }
                else if (errorFlag == 8)//缺管
                {
                    AgingHaveTube = false;
                    while (!AgingHaveTube)
                    {
                        NetCom3.Instance.Send(NetCom3.Cover("EB 90 11 01 06"), 5);
                        if (!NetCom3.Instance.SingleQuery())
                        {
                            return (int)ErrorState.BlendUnusua;//异常错误，直接退出
                        }

                        int delay = 1000;
                        while (!AgingHaveTube && delay > 0)
                        {
                            NetCom3.Delay(100);
                            delay -= 100;
                        }
                        if (AgingHaveTube)
                        {
                            AgingHaveTube = false;
                            goto sendAgain;//重发
                        }
                    }
                }
            }
            return errorFlag;
        }
        #endregion
        private void cbAgingTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region 切牌
            if (cbAgingTest.Text == "理杯&&抓手I")
            {
                if (!gbGraspI.Visible)
                {
                    Invoke(new Action(() =>
                    {
                        gbGraspI.Visible = true;
                        gbGraspII.Visible = gbSample.Visible = gbReagent.Visible = gbWash.Visible = gbTrackDispatch.Visible = false;
                    }));
                }
            }
            else if (cbAgingTest.Text == "抓手II")
            {
                if (!gbGraspII.Visible)
                {
                    Invoke(new Action(() =>
                    {
                        gbGraspII.Visible = true;
                        gbGraspI.Visible = gbSample.Visible = gbReagent.Visible = gbWash.Visible = gbTrackDispatch.Visible = false;
                    }));
                }
            }
            else if (cbAgingTest.Text == "加样-针I")
            {
                if (!gbSample.Visible)
                {
                    Invoke(new Action(() =>
                    {
                        gbSample.Visible = true;
                        gbGraspI.Visible = gbGraspII.Visible = gbReagent.Visible = gbWash.Visible = gbTrackDispatch.Visible = false;
                    }));
                }
            }
            else if (cbAgingTest.Text == "加试剂-针II")
            {
                if (!gbReagent.Visible)
                {
                    Invoke(new Action(() =>
                    {
                        gbReagent.Visible = true;
                        gbGraspI.Visible = gbGraspII.Visible = gbSample.Visible = gbWash.Visible = gbTrackDispatch.Visible = false;
                    }));
                }
            }
            else if (cbAgingTest.Text == "清洗盘")
            {
                if (!gbWash.Visible)
                {
                    Invoke(new Action(() =>
                    {
                        gbWash.Visible = true;
                        gbGraspI.Visible = gbGraspII.Visible = gbSample.Visible = gbReagent.Visible = gbTrackDispatch.Visible = false;
                    }));
                }
            }
            else if (cbAgingTest.Text == "轨道&&调度")
            {
                if (!gbTrackDispatch.Visible)
                {
                    Invoke(new Action(() =>
                    {
                        gbTrackDispatch.Visible = true;
                        gbGraspI.Visible = gbGraspII.Visible = gbSample.Visible = gbReagent.Visible = gbWash.Visible = false;
                    }));
                }
            }
            #endregion
        }
        private void txtAgingInfoShow_TextChanged(object sender, EventArgs e)
        {
            int index = txtAgingInfoShow.Lines.Count();
            showLog = txtAgingInfoShow.Lines.ElementAtOrDefault(index - 1);
            if (showLog == "")
                showLog = txtAgingInfoShow.Lines.ElementAtOrDefault(index - 2);
            txtAgingInfoShow.ScrollToCaret();
        }
        private void txtTestErrorShow_TextChanged(object sender, EventArgs e)
        {
            txtTestErrorShow.ScrollToCaret();
        }
        /// <summary>
        /// 温育反应盘清管
        /// </summary>
        private bool reactTrayTubeClear()
        {
            bool agingTestStartFlag = false;
            bool breakFlag = false;
            List<string> tubeInTray = new List<string>();
            DataTable dtInTrayIni = OperateIniFile.ReadConfig(iniPathReactTrayInfo);
            for (int i = 0, j = 0; i < dtInTrayIni.Rows.Count; i++)
            {
                if (int.Parse(dtInTrayIni.Rows[i][1].ToString()) >= 1)
                {
                    tubeInTray.Add(int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2)).ToString("x2"));
                }
            }
            if (agingTestThread != null)
                if (agingTestThread.IsAlive)
                    agingTestStartFlag = true;
            string moveTemp, moveTemp2 = moveTemp = "temp";
            object locker = new object();

            Thread r1 = new Thread(new ThreadStart(() =>
            {
                string log = "";
                while (moveTemp != null && !breakFlag)
                {
                    lock (locker)
                    {
                        if (tubeInTray.Count > 0)
                        {
                            moveTemp = tubeInTray.FirstOrDefault();
                            tubeInTray.RemoveAt(0);
                        }
                        else
                            break;
                    }
                    log = "抓手I在温育盘" + Convert.ToInt32(moveTemp, 16) + "号位清管";
                    bool sendOnceAgain = true;
                    sendAgain:
                    switch (orderSend("EB 90 31 01 05 " + moveTemp, 1, log))
                    {
                        case (int)ErrorState.Success:
                        case (int)ErrorState.IsNull:
                            break;
                        case (int)ErrorState.IsKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                if (orderSend("EB 90 01 03 00", 5) != 1)
                                {
                                    breakFlag = true;
                                    break;
                                }
                                goto sendAgain;
                            }
                            breakFlag = true;
                            break;
                        default:
                            breakFlag = true;
                            break;
                    }
                    OperateIniFile.WriteIniData("ReactTrayInfo", "no" + Convert.ToInt32(moveTemp, 16), "0", iniPathReactTrayInfo);
                    if (breakFlag)
                        break;
                    if (agingTestStartFlag)
                        if (!agingTestThread.IsAlive)
                            return;
                }
            }))
            { IsBackground = true };
            Thread r2 = new Thread(new ThreadStart(() =>
            {
                string log = "";
                while (moveTemp2 != null && !breakFlag)
                {
                    lock (locker)
                    {
                        if (tubeInTray.Count > 0)
                        {
                            moveTemp2 = tubeInTray.FirstOrDefault();
                            tubeInTray.RemoveAt(0);
                        }
                        else
                            break;
                    }
                    log = "抓手II在温育盘" + Convert.ToInt32(moveTemp2, 16) + "号位清管";
                    bool sendOnceAgain = true;
                    sendAgain:
                    switch (orderSend("EB 90 31 11 05 " + moveTemp2, 11, log))
                    {
                        case (int)ErrorState.Success:
                        case (int)ErrorState.IsNull:
                            break;
                        case (int)ErrorState.IsKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                if (orderSend("EB 90 21 03 00", 5) != 1)
                                {
                                    breakFlag = true;
                                    break;
                                }
                                goto sendAgain;
                            }
                            breakFlag = true;
                            break;
                        default:
                            breakFlag = true;
                            break;
                    }
                    OperateIniFile.WriteIniData("ReactTrayInfo", "no" + Convert.ToInt32(moveTemp2, 16), "0", iniPathReactTrayInfo);
                    if (breakFlag)
                        break;
                    if (agingTestStartFlag)
                        if (!agingTestThread.IsAlive)
                            return;
                    Application.DoEvents();
                }
            }))
            { IsBackground = true };
            #region 屏蔽
            IAsyncResult ir1 = BeginInvoke(new Action(() =>
            {
                //string log = "";
                //while (moveTemp != null && !breakFlag)
                //{
                //    lock (locker)
                //    {
                //        if (tubeInTray.Count > 0)
                //        {
                //            moveTemp = tubeInTray.FirstOrDefault();
                //            tubeInTray.RemoveAt(0);
                //        }
                //        else
                //            break;
                //    }
                //    log = "抓手I在温育盘" + Convert.ToInt32(moveTemp, 16) + "号位清管";
                //    bool sendOnceAgain = true;
                //    sendAgain:
                //    switch (orderSend("EB 90 31 01 05 " + moveTemp, 1 ,log))
                //    {
                //        case (int)ErrorState.Success:
                //        case (int)ErrorState.IsNull:
                //            break;
                //        case (int)ErrorState.IsKnocked:
                //            if (sendOnceAgain)
                //            {
                //                sendOnceAgain = false;
                //                if (orderSend("EB 90 01 03 00", 5) != 1)
                //                {
                //                    breakFlag = true;
                //                    break;
                //                }
                //                goto sendAgain;
                //            }
                //            breakFlag = true;
                //            break;
                //        default:
                //            breakFlag = true;
                //            break;
                //    }
                //    OperateIniFile.WriteIniData("ReactTrayInfo", "no" + Convert.ToInt32(moveTemp, 16), "0", iniPathReactTrayInfo);
                //    if (breakFlag)
                //        break;
                //    if (agingTestStartFlag)
                //        if (!agingTestThread.IsAlive)
                //            return;
                //}
            }));
            NetCom3.Delay(200);
            IAsyncResult ir2 = BeginInvoke(new Action(() =>
            {
                //string log = "";
                //while (moveTemp2 != null && !breakFlag)
                //{
                //    lock (locker)
                //    {
                //        if (tubeInTray.Count > 0)
                //        {
                //            moveTemp2 = tubeInTray.FirstOrDefault();
                //            tubeInTray.RemoveAt(0);
                //        }
                //        else
                //            break;
                //    }
                //    log = "抓手II在温育盘" + Convert.ToInt32(moveTemp2, 16) + "号位清管";
                //    bool sendOnceAgain = true;
                //    sendAgain:
                //    switch (orderSend("EB 90 31 11 05 " + moveTemp2, 11, log))
                //    {
                //        case (int)ErrorState.Success:
                //        case (int)ErrorState.IsNull:
                //            break;
                //        case (int)ErrorState.IsKnocked:
                //            if (sendOnceAgain)
                //            {
                //                sendOnceAgain = false;
                //                if (orderSend("EB 90 21 03 00", 5) != 1)
                //                {
                //                    breakFlag = true;
                //                    break;
                //                }
                //                goto sendAgain;
                //            }
                //            breakFlag = true;
                //            break;
                //        default:
                //            breakFlag = true;
                //            break;
                //    }
                //    OperateIniFile.WriteIniData("ReactTrayInfo", "no" + Convert.ToInt32(moveTemp2, 16), "0", iniPathReactTrayInfo);
                //    if (breakFlag)
                //        break;
                //    if (agingTestStartFlag)
                //        if (!agingTestThread.IsAlive)
                //            return;
                //    Application.DoEvents();
                //}
            }));
            #endregion
            r1.Start();
            r2.Start();
            while (r1.IsAlive || r2.IsAlive)
                NetCom3.Delay(1000);
            if (breakFlag)
                return false;
            OperateIniFile.WriteIniData("Tube", "ReacTrayTub", "", iniPathSubstrateTube);
            return true;
        }
        /// <summary>
        /// 清洗盘清管
        /// </summary>
        /// <returns></returns>
        private bool washTrayTubeClear()
        {
            for (int i = 0; i < frmParent.WashTrayNum; i++)
            {
                bool sendOnceAgain = true;
                sendAgain:
                switch (orderSend("EB 90 31 11 04 06", 11))
                {
                    case (int)ErrorState.Success:
                    case (int)ErrorState.IsNull:
                        break;
                    case (int)ErrorState.IsKnocked:
                        if (sendOnceAgain)
                        {
                            sendOnceAgain = false;
                            if (orderSend("EB 90 21 03 00", 5) != 1)
                            {
                                return false;
                            }
                            goto sendAgain;
                        }
                        return false;
                    default:
                        return false;
                }
                switch (orderSend("EB 90 31 03 01 01", 2))
                {
                    case (int)ErrorState.Success:
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }
        private bool twoTrayTubeClear()
        {
            bool agingTestStartFlag = false;
            bool breakFlag = false;
            List<string> tubeInTray = new List<string>();
            DataTable dtInTrayIni = OperateIniFile.ReadConfig(iniPathReactTrayInfo);
            //for (int i = 0, j = 0; i < dtInTrayIni.Rows.Count; i++)
            //{
            //    if (int.Parse(dtInTrayIni.Rows[i][1].ToString()) >= 1)
            //    {
            //        tubeInTray.Add(int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2)).ToString("x2"));
            //    }
            //}
            for(int i =1;i<=ReactTrayNum;i++)
            {
                tubeInTray.Add(i.ToString("x2"));
            }
            string moveTemp, moveTemp2 = moveTemp = "temp";
            object locker = new object();
            if (agingTestThread != null)
                if (agingTestThread.IsAlive)
                    agingTestStartFlag = true;
            Thread r1 = new Thread(new ThreadStart(() =>
            {
                string log = "";
                for (int i = 0; i < frmParent.WashTrayNum; i++)
                {
                    log = "抓手II在清洗盘" + (i + 1).ToString() + "号位清管";
                    bool sendOnceAgain = true;
                    sendAgain:
                    switch (orderSend("EB 90 31 11 04 06", 11, log))
                    {
                        case (int)ErrorState.Success:
                        case (int)ErrorState.IsNull:
                            break;
                        case (int)ErrorState.IsKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                if (orderSend("EB 90 21 03 00", 5) != 1)
                                {
                                    breakFlag = true;
                                    break;
                                }
                                goto sendAgain;
                            }
                            breakFlag = true;
                            break;
                        default:
                            breakFlag = true;
                            break;
                    }
                    if (breakFlag)
                        break;
                    log = "清洗盘逆时针旋转一位";
                    switch (orderSend("EB 90 31 03 01 01", 2, log))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            breakFlag = true;
                            break;
                    }
                    if (breakFlag)
                        break;
                    if (agingTestStartFlag)
                        if (!agingTestThread.IsAlive)
                            return;
                }
                while (moveTemp2 != null && !breakFlag)
                {
                    lock (locker)
                    {
                        if (tubeInTray.Count > 0)
                        {
                            //moveTemp2 = tubeInTray.FirstOrDefault();
                            //tubeInTray.RemoveAt(0);
                            moveTemp2 = tubeInTray.FirstOrDefault();
                            int tempNum = Convert.ToInt32(moveTemp2, 16) + 35/*- 1 + 17*/;
                            if (tempNum <= 150)
                            {
                                moveTemp2 = tempNum.ToString("x2");
                                if (tubeInTray.Find(xy => xy == moveTemp2) == null)
                                {
                                    continue;
                                }
                                tubeInTray.RemoveAll(xy => xy == moveTemp2);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                            break;
                    }
                    log = "抓手II在温育盘" + Convert.ToInt32(moveTemp2, 16) + "号位清管";
                    bool sendOnceAgain = true;
                    sendAgain:
                    switch (orderSend("EB 90 31 11 05 " + moveTemp2, 11, log))
                    {
                        case (int)ErrorState.Success:
                        case (int)ErrorState.IsNull:
                            break;
                        case (int)ErrorState.IsKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                if (orderSend("EB 90 21 03 00", 5) != 1)
                                {
                                    breakFlag = true;
                                    break;
                                }
                                goto sendAgain;
                            }
                            breakFlag = true;
                            break;
                        default:
                            breakFlag = true;
                            break;
                    }
                    OperateIniFile.WriteIniData("ReactTrayInfo", "no" + Convert.ToInt32(moveTemp2, 16), "0", iniPathReactTrayInfo);
                    if (breakFlag)
                        break;
                    if (agingTestStartFlag)
                        if (!agingTestThread.IsAlive)
                            return;
                }
            }))
            { IsBackground = true };
            Thread r2 = new Thread(new ThreadStart(() =>
            {
                string log = "";
                while (moveTemp != null && !breakFlag)
                {
                    lock (locker)
                    {
                        if (tubeInTray.Count > 0)
                        {
                            moveTemp = tubeInTray.FirstOrDefault();
                            tubeInTray.RemoveAt(0);
                        }
                        else
                            break;
                    }
                    log = "抓手I在温育盘" + Convert.ToInt32(moveTemp, 16) + "号位清管";
                    bool sendOnceAgain = true;
                    sendAgain:
                    switch (orderSend("EB 90 31 01 05 " + moveTemp, 1, log))
                    {
                        case (int)ErrorState.Success:
                        case (int)ErrorState.IsNull:
                            break;
                        case (int)ErrorState.IsKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                if (orderSend("EB 90 01 03 00", 5) != 1)
                                {
                                    breakFlag = true;
                                    break;
                                }
                                goto sendAgain;
                            }
                            breakFlag = true;
                            break;
                        default:
                            breakFlag = true;
                            break;
                    }
                    OperateIniFile.WriteIniData("ReactTrayInfo", "no" + Convert.ToInt32(moveTemp, 16), "0", iniPathReactTrayInfo);
                    if (breakFlag)
                        break;
                    if (agingTestStartFlag)
                        if (!agingTestThread.IsAlive)
                            return;
                }
            }))
            { IsBackground = true };
            #region 屏蔽
            IAsyncResult ir1 = BeginInvoke(new Action(() =>
            {
                //string log = "";
                //for (int i = 0; i < frmParent.WashTrayNum ; i++)
                //{
                //    log = "抓手II在清洗盘" + (i + 1).ToString() + "号位清管";
                //    bool sendOnceAgain = true;
                //    sendAgain:
                //    switch (orderSend("EB 90 31 11 04 06", 11, log))
                //    {
                //        case (int)ErrorState.Success:
                //        case (int)ErrorState.IsNull:
                //            break;
                //        case (int)ErrorState.IsKnocked:
                //            if (sendOnceAgain)
                //            {
                //                sendOnceAgain = false;
                //                if (orderSend("EB 90 21 03 00", 5) != 1)
                //                {
                //                    breakFlag = true;
                //                    break;
                //                }
                //                goto sendAgain;
                //            }
                //            breakFlag = true;
                //            break;
                //        default:
                //            breakFlag = true;
                //            break;
                //    }
                //    if (breakFlag)
                //        break;
                //    log = "清洗盘逆时针旋转一位";
                //    switch (orderSend("EB 90 31 03 01 01", 2 , log))
                //    {
                //        case (int)ErrorState.Success:
                //            break;
                //        default:
                //            breakFlag = true;
                //            break;
                //    }
                //    if (breakFlag)
                //        break;
                //    if (agingTestStartFlag)
                //        if(!agingTestThread.IsAlive)
                //            return;
                //}
                //while (moveTemp2 != null && !breakFlag)
                //{
                //    lock (locker)
                //    {
                //        if (tubeInTray.Count > 0)
                //        {
                //            moveTemp2 = tubeInTray.FirstOrDefault();
                //            tubeInTray.RemoveAt(0);
                //        }
                //        else
                //            break;
                //    }
                //    log = "抓手II在温育盘" + Convert.ToInt32(moveTemp2, 16) + "号位清管";
                //    bool sendOnceAgain = true;
                //    sendAgain:
                //    switch (orderSend("EB 90 31 11 05 " + moveTemp2, 11, log))
                //    {
                //        case (int)ErrorState.Success:
                //        case (int)ErrorState.IsNull:
                //            break;
                //        case (int)ErrorState.IsKnocked:
                //            if (sendOnceAgain)
                //            {
                //                sendOnceAgain = false;
                //                if (orderSend("EB 90 21 03 00", 5) != 1)
                //                {
                //                    breakFlag = true;
                //                    break;
                //                }
                //                goto sendAgain;
                //            }
                //            breakFlag = true;
                //            break;
                //        default:
                //            breakFlag = true;
                //            break;
                //    }
                //    OperateIniFile.WriteIniData("ReactTrayInfo", "no" + Convert.ToInt32(moveTemp2, 16), "0", iniPathReactTrayInfo);
                //    if (breakFlag)
                //        break;
                //    if (agingTestStartFlag)
                //        if (!agingTestThread.IsAlive)
                //            return;
                //}
            }));
            IAsyncResult ir2 = BeginInvoke(new Action(() =>
            {
                //string log = "";
                //while (moveTemp != null && !breakFlag)
                //{
                //    lock (locker)
                //    {
                //        if (tubeInTray.Count > 0)
                //        {
                //            moveTemp = tubeInTray.FirstOrDefault();
                //            tubeInTray.RemoveAt(0);
                //        }
                //        else
                //            break;
                //    }
                //    log = "抓手I在温育盘" + Convert.ToInt32(moveTemp, 16) + "号位清管";
                //    bool sendOnceAgain = true;
                //    sendAgain:
                //    switch (orderSend("EB 90 31 01 05 " + moveTemp, 1, log))
                //    {
                //        case (int)ErrorState.Success:
                //        case (int)ErrorState.IsNull:
                //            break;
                //        case (int)ErrorState.IsKnocked:
                //            if (sendOnceAgain)
                //            {
                //                sendOnceAgain = false;
                //                if (orderSend("EB 90 01 03 00", 5) != 1)
                //                {
                //                    breakFlag = true;
                //                    break;
                //                }
                //                goto sendAgain;
                //            }
                //            breakFlag = true;
                //            break;
                //        default:
                //            breakFlag = true;
                //            break;
                //    }
                //    OperateIniFile.WriteIniData("ReactTrayInfo", "no" + Convert.ToInt32(moveTemp, 16), "0", iniPathReactTrayInfo);
                //    if (breakFlag)
                //        break;
                //    if (agingTestStartFlag)
                //        if (!agingTestThread.IsAlive)
                //            return;
                //}
            }));
            #endregion
            r1.Start();
            r2.Start();
            while (r1.IsAlive || r2.IsAlive)
            {
                NetCom3.Delay(30);
            }
            if (agingTestStartFlag)
                Invoke(new Action(() =>
                {
                    txtAgingInfoShow.AppendText(Environment.NewLine);
                }));
            if (breakFlag)
                return false;
            OperateIniFile.WriteIniData("Tube", "ReacTrayTub", "", iniPathSubstrateTube);
            return true;
        }
        #endregion

        #region 混匀
        private void rdbLoopMix_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLoopMix.Checked)
            {
                label34.Visible = txtMixInterval.Visible = true;
            }
            else
            {
                label34.Visible = txtMixInterval.Visible = false;
            }
        }

        private void fbtnMixStart_Click(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (txtMixInterval.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "混匀间隔不能为空！");
                return;
            }
            if (txtMixHoll.Text.Trim() == "" || txtMixHoll.Text.Trim() == "0")
            {
                frmMsgShow.MessageShow("仪器调试", "混匀孔位不能为空！");
                return;
            }
            if (txtMixNum.Text.Trim() == "" || txtMixNum.Text.Trim() == "0")
            {
                frmMsgShow.MessageShow("仪器调试", "混匀次数不能为空！");
                return;
            }
            fbtnMixStart.Enabled = false;
            fbtnMixStop.Enabled = true;
            txtMixNum.ReadOnly = true;

            mixTestThread = new Thread(mixTestRun);
            mixTestThread.IsBackground = true;
            mixTestThread.Name = "mixTestThread";
            mixTestThread.Start();

            threadList.Add(mixTestThread);
        }
        private void fbtnMixStop_Click(object sender, EventArgs e)
        {
            if (mixTestThread != null)
                mixTestThread.Abort();

            Invoke(new Action(() =>
            {
                txtMixNum.ReadOnly = false;
            }));
            fbtnMixStart.Enabled = true;
            fbtnMixStop.Enabled = false;
        }
        private void mixTestRun()
        {
            int mixNum = int.Parse(txtMixNum.Text);
            int mixInterval = int.Parse(txtMixInterval.Text);
            int mixHoll = int.Parse(txtMixHoll.Text);
            int mixSurplusNum = mixNum;
            BeginInvoke(new Action(() =>
            {
                txtMixSurplusNum.Text = mixSurplusNum.ToString();
            }));
            if (rdbStepMix.Checked)
            {
                string order = "";
                int orderType = 0;
                if(mixHoll % 2 == 0)
                {
                    order = "EB 90 31 04 01 " + mixHoll.ToString("x2");
                    orderType = 4;
                }
                else
                {
                    order = "EB 90 31 14 01 " + mixHoll.ToString("x2");
                    orderType = 14;
                }
                for(int i =0;i<mixNum;i++)
                {
                    switch(orderSend(order , orderType))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            mixControlInit();
                            return;
                    }
                    mixSurplusNum--;
                    BeginInvoke(new Action(() =>
                    {
                        txtMixSurplusNum.Text = mixSurplusNum.ToString();
                    }));
                }
            }
            else if(rdbLoopMix.Checked)
            {
                string order = "";
                int orderType = 0;
                for (int i = 0; i < mixNum; i++)
                {
                    if (mixHoll % 2 == 0)
                    {
                        order = "EB 90 31 04 01 " + mixHoll.ToString("x2");
                        orderType = 4;
                    }
                    else
                    {
                        order = "EB 90 31 14 01 " + mixHoll.ToString("x2");
                        orderType = 14;
                    }
                    switch (orderSend(order , orderType))
                    {
                        case (int)ErrorState.Success:
                            break;
                        default:
                            mixControlInit();
                            return;
                    }
                    mixHoll = (mixHoll + mixInterval + 1) % ReactTrayNum == 0 ? ReactTrayNum : (mixHoll + mixInterval + 1) % ReactTrayNum;
                    mixSurplusNum--;
                    BeginInvoke(new Action(() =>
                    {
                        txtMixSurplusNum.Text = mixSurplusNum.ToString();
                    }));
                }
            }
            mixControlInit();
        }
        private void mixControlInit()
        {
            threadList.RemoveAll(xy => xy.Name == "mixTestThread");
            Invoke(new Action(() =>
            {
                txtMixNum.ReadOnly = false;
            }));
            fbtnMixStart.Enabled = true;
            fbtnMixStop.Enabled = false;
        }
        #endregion
        /// <summary>
        /// 单步指令发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fbtnorderSend_Click(object sender, EventArgs e)
        {
            if(txtStepOrder.Text == "")
            {
                MessageBox.Show("请输入通讯指令");
            }

            fbtnorderSend.Enabled = false;
            string order = txtStepOrder.Text.ToString();

            if(order.Contains("EB 90 31 01"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), 1);
                NetCom3.Instance.MoveQuery();
            }
            else if(order.Contains("EB 90 31 11"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), 11);
                NetCom3.Instance.Move2Query();
            }
            else if (order.Contains("EB 90 31 02"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), 0);
                NetCom3.Instance.SPQuery();
            }
            else if (order.Contains("EB 90 31 12"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), 10);
                NetCom3.Instance.SP2Query();
            }
            else if (order.Contains("EB 90 31 03"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), 2);
                NetCom3.Instance.WashQuery();
            }
            else if (order.Contains("EB 90 31 04"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), 4);
                NetCom3.Instance.MixQuery();
            }
            else if (order.Contains("EB 90 31 05"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), 15);
                NetCom3.Instance.SampleAdditionQuery();
            }
            else if (order.Contains("EB 90 31 06"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), 6);
                NetCom3.Instance.TrackStripQuery();
            }
            else if (order.Contains("EB 90 31 07"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), 7);
                NetCom3.Instance.CapstanStripQuery();
            }
            else
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), 5);
                NetCom3.Instance.SingleQuery();
            }
            fbtnorderSend.Enabled = true;
        }
        #region 灌注
        private void fbtnPerfusionStart_Click(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (txtPerfusionNum.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "灌注次数不能为空！");
                return;
            }
            fbtnPerfusionStart.Enabled = false;
            fbtnPerfusionStop.Enabled = true;
            txtPerfusionNum.ReadOnly = true;

            perfusionTextThread = new Thread(perfusionTestRun);
            perfusionTextThread.IsBackground = true;
            perfusionTextThread.Name = "perfusionTextThread";
            perfusionTextThread.Start();

            threadList.Add(perfusionTextThread);
        }
        private void fbtnPerfusionStop_Click(object sender, EventArgs e)
        {
            if (perfusionTextThread != null)
                perfusionTextThread.Abort();

            Invoke(new Action(() =>
            {
                txtPerfusionNum.ReadOnly = false;
            }));
            fbtnPerfusionStart.Enabled = true;
            fbtnPerfusionStop.Enabled = false;
        }
        private void perfusionControlInit()
        {
            threadList.RemoveAll(xy => xy.Name == "perfusionTextThread");
            Invoke(new Action(() =>
            {
                txtPerfusionNum.ReadOnly = false;
            }));
            fbtnPerfusionStart.Enabled = true;
            fbtnPerfusionStop.Enabled = false;
        }
        private void perfusionTestRun()
        {
            int perfusionNum = int.Parse(txtPerfusionNum.Text);
            bool addTubeFlag = chkAddTube.Checked;
            int perfusionSurplusNum = perfusionNum;
            BeginInvoke(new Action(() =>
            {
                txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
            }));

            if(rdbSamplePerfusion.Checked)
            {
                //加样-针I灌注
                for(int i=0;i< perfusionNum;i++)
                {
                    if(orderSend("EB 90 31 02 08" , 0) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                    NetCom3.Delay(500);
                    perfusionSurplusNum--;
                    BeginInvoke(new Action(() =>
                    {
                        txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
                    }));
                }
            }
            else if(rdbReagentPerfusion.Checked)
            {
                //加试剂-针II灌注
                for (int i = 0; i < perfusionNum; i++)
                {
                    if (orderSend("EB 90 31 12 08", 10) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                    NetCom3.Delay(500);
                    perfusionSurplusNum--;
                    BeginInvoke(new Action(() =>
                    {
                        txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
                    }));
                }
            }
            else if (rdbWashPerfusionI.Checked)
            {
                //清洗-注液针I灌注
                washTrayTubeClear();
                //夹新管
                if(addTubeFlag)
                {
                    bool sendOnceAgain = true;
                    sendAgainI:
                    switch(orderSend("EB 90 31 01 01 01" , 1))
                    {
                        case (int)ErrorState.Success:
                            break;
                        case (int)ErrorState.putKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                switch (orderSend("EB 90 31 01 05 01", 1))
                                {
                                    case (int)ErrorState.Success:
                                    case (int)ErrorState.IsNull:
                                        break;
                                    default:
                                        perfusionControlInit();
                                        return;
                                }
                                goto sendAgainI;
                            }
                            perfusionControlInit();
                            return;
                        default:
                            perfusionControlInit();
                            return;
                    }
                    sendOnceAgain = true;
                    sendAgainII:
                    switch(orderSend("EB 90 31 11 02 01" , 11))
                    {
                        case (int)ErrorState.Success:
                            break;
                        case (int)ErrorState.IsKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                if (orderSend("EB 90 21 03 00", 5) != 1)
                                {
                                    perfusionControlInit();
                                    return;
                                }
                                goto sendAgainII;
                            }
                            perfusionControlInit();
                            return;
                        default:
                            perfusionControlInit();
                            return;
                    }
                    if(orderSend("EB 90 31 03 01 07" , 2) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                    for(int i =0; i < perfusionNum; i++)
                    {
                        //注、转、抽、转
                        if (orderSend("EB 90 31 03 03 00 10", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 01 " + (-1).ToString("x2").Substring(6, 2), 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 03 01", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 01 01", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        perfusionSurplusNum--;
                        BeginInvoke(new Action(() =>
                        {
                            txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
                        }));
                    }
                    //转、扔
                    if (orderSend("EB 90 31 03 01 " + (-7).ToString("x2").Substring(6, 2), 2) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                    if (orderSend("EB 90 31 11 04 06", 11) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                }
                else
                {
                    for(int i = 0; i < perfusionNum; i++)
                    {
                        if (orderSend("EB 90 31 03 03 00 10 00 00", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        NetCom3.Delay(500);
                        perfusionSurplusNum--;
                        BeginInvoke(new Action(() =>
                        {
                            txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
                        }));
                    }
                }
            }
            else if (rdbWashPerfusionII.Checked)
            {
                //清洗-注液针II灌注
                washTrayTubeClear();
                //夹新管
                if (addTubeFlag)
                {
                    bool sendOnceAgain = true;
                    sendAgain:
                    switch (orderSend("EB 90 31 01 01 01", 1))
                    {
                        case (int)ErrorState.Success:
                            break;
                        case (int)ErrorState.putKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                switch (orderSend("EB 90 31 01 05 01", 1))
                                {
                                    case (int)ErrorState.Success:
                                    case (int)ErrorState.IsNull:
                                        break;
                                    default:
                                        perfusionControlInit();
                                        return;
                                }
                                goto sendAgain;
                            }
                            perfusionControlInit();
                            return;
                        default:
                            perfusionControlInit();
                            return;
                    }
                    sendOnceAgain = true;
                    sendAgainII:
                    switch (orderSend("EB 90 31 11 02 01", 11))
                    {
                        case (int)ErrorState.Success:
                            break;
                        case (int)ErrorState.IsKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                if (orderSend("EB 90 21 03 00", 5) != 1)
                                {
                                    perfusionControlInit();
                                    return;
                                }
                                goto sendAgainII;
                            }
                            perfusionControlInit();
                            return;
                        default:
                            perfusionControlInit();
                            return;
                    }
                    if (orderSend("EB 90 31 03 01 0C", 2) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                    for (int i = 0; i < perfusionNum; i++)
                    {
                        //注、转、抽、转
                        if (orderSend("EB 90 31 03 03 00 01", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 01 " + (-1).ToString("x2").Substring(6, 2), 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 03 01", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 01 01", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        perfusionSurplusNum--;
                        BeginInvoke(new Action(() =>
                        {
                            txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
                        }));
                    }
                    //转、扔
                    if (orderSend("EB 90 31 03 01 " + (-12).ToString("x2").Substring(6, 2), 2) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                    if (orderSend("EB 90 31 11 04 06", 11) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                }
                else
                {
                    for (int i = 0; i < perfusionNum; i++)
                    {
                        if (orderSend("EB 90 31 03 03 00 01 00 00", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        NetCom3.Delay(500);
                        perfusionSurplusNum--;
                        BeginInvoke(new Action(() =>
                        {
                            txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
                        }));
                    }
                }
            }
            else if (rdbWashPerfusionIII.Checked)
            {
                //清洗-注液针III灌注
                washTrayTubeClear();
                //夹新管
                if (addTubeFlag)
                {
                    bool sendOnceAgain = true;
                    sendAgain:
                    switch (orderSend("EB 90 31 01 01 01", 1))
                    {
                        case (int)ErrorState.Success:
                            break;
                        case (int)ErrorState.putKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                switch (orderSend("EB 90 31 01 05 01", 1))
                                {
                                    case (int)ErrorState.Success:
                                    case (int)ErrorState.IsNull:
                                        break;
                                    default:
                                        perfusionControlInit();
                                        return;
                                }
                                goto sendAgain;
                            }
                            perfusionControlInit();
                            return;
                        default:
                            perfusionControlInit();
                            return;
                    }
                    sendOnceAgain = true;
                    sendAgainII:
                    switch (orderSend("EB 90 31 11 02 01", 11))
                    {
                        case (int)ErrorState.Success:
                            break;
                        case (int)ErrorState.IsKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                if (orderSend("EB 90 21 03 00", 5) != 1)
                                {
                                    perfusionControlInit();
                                    return;
                                }
                                goto sendAgainII;
                            }
                            perfusionControlInit();
                            return;
                        default:
                            perfusionControlInit();
                            return;
                    }
                    if (orderSend("EB 90 31 03 01 11", 2) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                    for (int i = 0; i < perfusionNum; i++)
                    {
                        //注、转、抽、转
                        if (orderSend("EB 90 31 03 03 00 00 10", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 01 " + (-1).ToString("x2").Substring(6, 2), 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 03 01", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 01 01", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        perfusionSurplusNum--;
                        BeginInvoke(new Action(() =>
                        {
                            txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
                        }));
                    }
                    //转、扔
                    if (orderSend("EB 90 31 03 01 " + (-17).ToString("x2").Substring(6, 2), 2) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                    if (orderSend("EB 90 31 11 04 06", 11) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                }
                else
                {
                    for (int i = 0; i < perfusionNum; i++)
                    {
                        if (orderSend("EB 90 31 03 03 00 00 10 00", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        NetCom3.Delay(500);
                        perfusionSurplusNum--;
                        BeginInvoke(new Action(() =>
                        {
                            txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
                        }));
                    }
                }
            }
            else if (rdbWashPerfusion.Checked)
            {
                //清洗-注液针灌注
                washTrayTubeClear();
                //夹新管
                if (addTubeFlag)
                {
                    bool sendOnceAgain = true;
                    for (int i = 1;i <= 3; i++)
                    {
                        sendOnceAgain = true;
                        sendAgain:
                        switch (orderSend("EB 90 31 01 01 " + i.ToString("x2"), 1))
                        {
                            case (int)ErrorState.Success:
                                break;
                            case (int)ErrorState.putKnocked:
                                if (sendOnceAgain)
                                {
                                    sendOnceAgain = false;
                                    switch (orderSend("EB 90 31 01 05 " + i.ToString("x2"), 1))
                                    {
                                        case (int)ErrorState.Success:
                                        case (int)ErrorState.IsNull:
                                            break;
                                        default:
                                            perfusionControlInit();
                                            return;
                                    }
                                    goto sendAgain;
                                }
                                perfusionControlInit();
                                return;
                            default:
                                perfusionControlInit();
                                return;
                        }
                    }
                    for(int i = 1; i <= 3; i++)
                    {
                        sendOnceAgain = true;
                        sendAgain:
                        switch (orderSend("EB 90 31 11 02 " + i.ToString("x2"), 11))
                        {
                            case (int)ErrorState.Success:
                                break;
                            case (int)ErrorState.IsKnocked:
                                if (sendOnceAgain)
                                {
                                    sendOnceAgain = false;
                                    if (orderSend("EB 90 21 03 00", 5) != 1)
                                    {
                                        perfusionControlInit();
                                        return;
                                    }
                                    goto sendAgain;
                                }
                                perfusionControlInit();
                                return;
                            default:
                                perfusionControlInit();
                                return;
                        }
                        if(i == 3)
                        {
                            if (orderSend("EB 90 31 03 01 07", 2) != (int)ErrorState.Success)
                            {
                                perfusionControlInit();
                                return;
                            }
                        }
                        else
                        {
                            if (orderSend("EB 90 31 03 01 05", 2) != (int)ErrorState.Success)
                            {
                                perfusionControlInit();
                                return;
                            }
                        }
                    }
                    for(int i = 0; i < perfusionNum; i++)
                    {
                        //注、转、抽、转
                        if (orderSend("EB 90 31 03 03 00 11 10", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 01 " + (-1).ToString("x2").Substring(6, 2), 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 03 01", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 01 01", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        perfusionSurplusNum--;
                        BeginInvoke(new Action(() =>
                        {
                            txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
                        }));
                    }
                    for(int i = 0; i < 3; i++)
                    {
                        //转、扔
                        if(i == 0)
                        {
                            if (orderSend("EB 90 31 03 01 " + (-7).ToString("x2").Substring(6, 2), 2) != (int)ErrorState.Success)
                            {
                                perfusionControlInit();
                                return;
                            }
                        }
                        else
                        {
                            if (orderSend("EB 90 31 03 01 " + (-5).ToString("x2").Substring(6, 2), 2) != (int)ErrorState.Success)
                            {
                                perfusionControlInit();
                                return;
                            }
                        }                       
                        if (orderSend("EB 90 31 11 04 06", 11) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < perfusionNum; i++)
                    {
                        if (orderSend("EB 90 31 03 03 00 11 10 00", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        NetCom3.Delay(500);
                        perfusionSurplusNum--;
                        BeginInvoke(new Action(() =>
                        {
                            txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
                        }));
                    }
                }
            }
            else if(rdbSubPerfusion.Checked)
            {
                //底物灌注
                washTrayTubeClear();
                //夹新管
                if (addTubeFlag)
                {
                    bool sendOnceAgain = true;
                    sendAgain:
                    switch (orderSend("EB 90 31 01 01 01", 1))
                    {
                        case (int)ErrorState.Success:
                            break;
                        case (int)ErrorState.putKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                switch (orderSend("EB 90 31 01 05 01", 1))
                                {
                                    case (int)ErrorState.Success:
                                    case (int)ErrorState.IsNull:
                                        break;
                                    default:
                                        perfusionControlInit();
                                        return;
                                }
                                goto sendAgain;
                            }
                            perfusionControlInit();
                            return;
                        default:
                            perfusionControlInit();
                            return;
                    }
                    sendOnceAgain = true;
                    sendAgainII:
                    switch (orderSend("EB 90 31 11 02 01", 11))
                    {
                        case (int)ErrorState.Success:
                            break;
                        case (int)ErrorState.IsKnocked:
                            if (sendOnceAgain)
                            {
                                sendOnceAgain = false;
                                if (orderSend("EB 90 21 03 00", 5) != 1)
                                {
                                    perfusionControlInit();
                                    return;
                                }
                                goto sendAgainII;
                            }
                            perfusionControlInit();
                            return;
                        default:
                            perfusionControlInit();
                            return;
                    }
                    if (orderSend("EB 90 31 03 01 17", 2) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                    for (int i = 0; i < perfusionNum; i++)
                    {
                        //注、转、抽、转
                        if (orderSend("EB 90 31 03 03 00 00 01 10", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 01 " + (-2).ToString("x2").Substring(6, 2), 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 03 01", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        if (orderSend("EB 90 31 03 01 02", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        perfusionSurplusNum--;
                        BeginInvoke(new Action(() =>
                        {
                            txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
                        }));
                    }
                    //转、扔
                    if (orderSend("EB 90 31 03 01 " + (-23).ToString("x2").Substring(6, 2), 2) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                    if (orderSend("EB 90 31 11 04 06", 11) != (int)ErrorState.Success)
                    {
                        perfusionControlInit();
                        return;
                    }
                }
                else
                {
                    for (int i = 0; i < perfusionNum; i++)
                    {
                        if (orderSend("EB 90 31 03 03 00 00 01 10", 2) != (int)ErrorState.Success)
                        {
                            perfusionControlInit();
                            return;
                        }
                        NetCom3.Delay(500);
                        perfusionSurplusNum--;
                        BeginInvoke(new Action(() =>
                        {
                            txtSurplusPerfusionNum.Text = perfusionSurplusNum.ToString();
                        }));
                    }
                }
            }
            perfusionControlInit();
        }



        #endregion
        private void grpNoBoard_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
        }

        private void cmbTestName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbTestName.SelectedIndex == 0)
            {
                grpReactTrayHoll.Visible = grpSampleHoll1.Visible = grpSampleVol.Visible = true;
                grpInjection.Visible = grpReadNum.Visible = 
                    grpSampleHoll2.Visible = grpSampleNum.Visible = false;
            }
            else if(cmbTestName.SelectedIndex == 1)
            {
                grpSampleHoll1.Visible = grpSampleVol.Visible = 
                    grpSampleNum.Visible = grpReadNum.Visible = true;
                grpReactTrayHoll.Visible = grpInjection.Visible = grpSampleHoll2.Visible = false;
            }
            else if(cmbTestName.SelectedIndex == 2)
            {
                grpSampleHoll1.Visible = grpSampleHoll2.Visible = 
                    grpSampleVol.Visible = grpSampleNum.Visible = grpReadNum.Visible = true;
                grpReactTrayHoll.Visible = grpInjection.Visible = false;
            }
            else if(cmbTestName.SelectedIndex == 3)
            {
                grpInjection.Visible = true;
                grpSampleHoll1.Visible = grpSampleHoll2.Visible = grpSampleVol.Visible = 
                    grpSampleNum.Visible = grpReadNum.Visible = grpReactTrayHoll.Visible = false;
            }
        }

        
    }
}

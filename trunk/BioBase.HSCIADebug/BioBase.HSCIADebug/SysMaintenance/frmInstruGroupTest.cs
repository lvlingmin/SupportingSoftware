using BioBase.HSCIADebug.ControlInfo;
using Common;
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

namespace BioBase.HSCIADebug.SysMaintenance
{
    public partial class frmInstruGroupTest : frmParent
    {
        #region 变量
        bool isNewWashRun = false;
        bool CancellationToken = false;
        /// <summary>
        /// 级联机器编号
        /// </summary>
        string strModel = "00";
        #endregion
        #region 读写文件信息
        /// <summary>
        /// 发送移管手指令
        /// </summary>
        /// <param name="movestate">动作状态</param>
        /// <param name="movestate">抓手标志 0-移新管 1-移管 </param>
        /// <param name="startpos">开始位置</param>
        /// <param name="goalpos">结束位置</param>
        /// <returns></returns>
        public int Move(string strModel,MoveSate MoveSate, int movehand, int startpos = 0, int goalpos = 0)
        {
            MoveHandSend.MoveSate = MoveSate;
           
            if(movehand ==(int)MoveHand.Movehand)
            { 
                MoveHandSend.Move2(strModel, goalpos);
                return NetCom3.Instance.Move2rrorFlag;
            }
            else
            {
                MoveHandSend.Move(strModel, goalpos);
                return NetCom3.Instance.MoverrorFlag;
            }
               
        }
        /// <summary>
        /// 读温育盘配置信息
        /// </summary>
        /// <returns></returns>
        public DataTable ReadReact()
        {
            DataTable dt = OperateIniFile.ReadConfig(ParaIniInfo.iniPathReactTrayInfo);
            return dt;
        }
        /// <summary>
        /// 读温育盘配置信息
        /// </summary>
        /// <returns></returns>
        public int ReadSubstrate()
        {
            int Substrate = int.Parse(OperateIniFile.ReadIniData("Substrate1", "LeftCount", "0", ParaIniInfo.iniPathSubstrateTube));
            return Substrate;
        }
        public void WriteSubstrate(int substrateNum)
        {
            OperateIniFile.WriteIniData("Substrate1", "LeftCount", substrateNum.ToString(), ParaIniInfo.iniPathSubstrateTube);
        }
        /// <summary>
        /// 写温育盘配置信息
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="num"></param>
        public void WriteReact(int pos, int num)
        {
            OperateIniFile.WriteIniData("ReactTrayInfo", "no" + pos.ToString(), num.ToString(), ParaIniInfo.iniPathReactTrayInfo);
        }
        /// <summary>
        /// 读取WashTrayInfo
        /// </summary>
        /// <returns></returns>
        public string ReadWash(int pos)
        {
            string LeftCount = OperateIniFile.ReadIniData("TubePosition", "No" + pos.ToString(), "1", ParaIniInfo.iniPathWashTrayInfo); ;
            return LeftCount;
        }
        public DataTable ReadWash()
        {
            DataTable dt = OperateIniFile.ReadConfig(ParaIniInfo.iniPathWashTrayInfo);
            return dt;
        }
        /// <summary>
        /// 写读取WashTrayInfo
        /// </summary>
        /// <returns></returns>
        public void WriteWash(int pos, int num)
        {
            OperateIniFile.WriteIniData("TubePosition", "No" + pos.ToString(), num.ToString(), ParaIniInfo.iniPathWashTrayInfo);
        }
        /// <summary>
        /// 清洗盘旋转
        /// </summary>
        /// <param name="num">旋转孔数</param>
        /// <returns></returns>
        public int WashTurn(int num)
        {
            return WashSend.WashTurn(strModel,num);
        }
        /// <summary>
        /// 批量写入数据
        /// </summary>
        /// <param name="iniPath">配置信息</param>
        /// <param name="dataTable">数据表</param>
        public void WriteConfigToFile(string iniPath, DataTable dataTable)
        {
            OperateIniFile.WriteConfigToFile("[TubePosition]", iniPath, dataTable);
        }
        /// <summary>
        /// 批量更新清洗盘配置信息
        /// </summary>
        public void UpdatConfigWash()
        {
            //查询清洗盘信息
            DataTable dtWashTrayInfo = ReadWash();
            DataTable dtTemp = new DataTable();
            dtTemp = dtWashTrayInfo.Copy();
            //清洗盘状态列表中添加反应盘位置字段
            dtWashTrayInfo.Rows[0][1] = dtTemp.Rows[dtWashTrayInfo.Rows.Count - 1][1];
            for (int i = 1; i < dtWashTrayInfo.Rows.Count; i++)
            {
                dtWashTrayInfo.Rows[i][1] = dtTemp.Rows[i - 1][1];
            }
            WriteConfigToFile(ParaIniInfo.iniPathWashTrayInfo, dtWashTrayInfo);
        }
        /// <summary>
        /// 清洗盘加液/注液/读数
        /// </summary>
        /// <returns></returns>
        public int WashAddLiquidR(string strModel)
        {
            return WashSend.WashAddLiquidR(strModel);
        }
        #endregion
        public frmInstruGroupTest()
        {
            InitializeComponent();
        }
        private void frmInstruGroupTest_Load(object sender, EventArgs e)
        {
            cmbSelectAct.SelectedIndex = 0;
        }
        private void frmInstruGroupTest_SizeChanged(object sender, EventArgs e)
        {
            formSizeChange(this);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
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
                if (double.Parse(temp) > Math.Pow(10, 5))
                    temp = ((int)GetPMT(double.Parse(temp))).ToString();
                TExtAppend(DateTime.Now.ToString("HH-mm-ss") + ": " + "PMT背景值：" + temp);
            }
            if (isNewWashEnd()) return; //lyq add 20190822 结束标志
        }
        bool isNewWashEnd()
        {
            if (CancellationToken)
            {
                NewWashEnd();
                return true;
            }
            else return false;
        }
        void NewWashEnd(int errorflag = 0)
        {
            if (errorflag == 1)
            {
                if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.IsKnocked)
                    TExtAppend("抓手在取管位置发生了撞管。");
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.putKnocked)
                    TExtAppend("抓手在放管位置发生了撞管。");
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.LackTube)
                    TExtAppend("理杯机位置缺管。");
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.OverTime)
                    TExtAppend("移管手指令接收超时。");

            }
            else if (errorflag == 3)
            {
                if (NetCom3.Instance.WasherrorFlag == (int)ErrorState.OverTime)
                    TExtAppend("清洗指令接收超时。");
            }
            else if (errorflag == 2)
            {
                if (NetCom3.Instance.AdderrorFlag == (int)ErrorState.OverTime)
                    TExtAppend("加指令接收超时。");
                else if (NetCom3.Instance.AdderrorFlag == (int)ErrorState.IsKnocked)
                    TExtAppend("加样针加样时发生撞针。");
            }
            isNewWashRun = false;
            CancellationToken = true; //lyq add 20190822
            btnStart.Enabled = true;
            NetCom3.Instance.ReceiveHandel -= GetReadNum2;
            TExtAppend("已结束。");
            if (btnPourInto.Text == "停止")
                BeginInvoke(new Action(() => { btnPourInto.Text = "执行"; }));
            btnPourInto.Enabled = true;
        }
        void TExtAppend(string text)
        {
            while (!this.IsHandleCreated)
            {
            }
            textBox1.Invoke(new Action(() => { textBox1.AppendText(Environment.NewLine + text); }));
        }
        int tubeCount = 0;
        #region 组合测试清洗测试流程
        private void btnStart_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (isNewWashRun)
            {
                frmMsgShow.MessageShow("信息提示", "正在运行。");
                return;
            }
            int ReactStart = (int)numIPos.Value;
            int tubeNum = (int)numTubeCount.Value;
            int repeatRead = (int)numReadCount.Value;
            if (ReactStart + tubeNum > ReactTrayNum)
            {
               
                frmMsgShow.MessageShow("信息提示", "参数设置错误，请从新设置。");
                return;
            }
            if (chkWash.Checked && !ChkAddSub.Checked)
            {
                frmMsgShow.MessageShow("信息提示", "进行清洗时，必须同时原则加底物。");
                return;
            }
            isNewWashRun = true;
            CancellationToken = false;
            btnStart.Enabled = false;
            btnEnd.Enabled = true;
            textBox1.Text = "";
            while (!this.IsHandleCreated)
            {
                Thread.Sleep(15);
            }
            //清空温育盘
            if (chkClearTray.Checked)
            {
                TExtAppend("\n正在清空温育盘。。。");
                tubeCount = 0;
                bool b = reactTrayTubeClear(0);
                if (!b) return;
                TExtAppend("\n清空温育盘结束。。。\n");
            }
            //清空清洗盘
            if (ChkClearWash.Checked)
            {
                TExtAppend("\n正在清空清洗盘。。。");
                bool b = washTrayTubeClear();
                if (!b) return;
                TExtAppend("\n清空清洗盘结束。。。\n");
            }
            //底物灌注
            if (ChkSubPourIn.Checked)  //底物灌注
            {
                int SubstrateCount = int.Parse(numDwPourin.Value.ToString());
                SubstratePipeline(SubstrateCount);
            }
            //加新管
            if (ChkAddTube.Checked)
            {
                for (int i = 0; i < tubeNum; i++)
                {
                    if (isNewWashEnd())
                        return;
                    int errorflag = Move(strModel,MoveSate.NewtubeToReact,0, ReactStart+i);
                    if (errorflag != (int)ErrorState.Success)
                    {
                        NewWashEnd(1);
                        return;
                    }
                }
            }
            CleanTray tray = new CleanTray(1);
            int temptubenum = tubeNum;
            int tempread = 1;
            int tubecount = 1;
            for (int i = 0; i < WashTrayNum+ tubeNum; i++)
            {
                int errorflag = 0;
                if (isNewWashEnd()) return;
                if (temptubenum > 0)//夹新管
                {
                    if (ChkAddTube.Checked)
                    {
                        TExtAppend("正在加第" + (tubeNum - temptubenum + 1) + "个新管");
                        int errorFlag = Move(strModel,MoveSate.ReactToWash,(int)MoveHand.Movehand, ReactStart+i);
                        if (errorFlag != (int)ErrorState.Success)
                        {
                            NewWashEnd(1);
                            return;
                        }
                    }
                    tray.pointer[0].Value[1] = 1;
                    temptubenum--;
                }
                if (isNewWashEnd()) return;

                if (tray.needStay(chkWash.Checked, ChkAddSub.Checked, ChkRead.Checked))//清洗盘工作
                {
                    if (ChkRead.Checked && tray.pointer[9].Value[1] == 1 && tempread <= tubeNum)
                    {
                        NetCom3.Instance.ReceiveHandel += GetReadNum2;
                        TExtAppend("第" + tempread + "个管正在读数");
                        tempread++;
                    }
                    errorflag=tray.WashAddLiquidR(strModel,chkWash.Checked, ChkAddSub.Checked,ChkRead.Checked,1);
                    if (errorflag != (int)ErrorState.Success)
                    {
                        NewWashEnd(3);
                        return;
                    }
                    else
                    {
                        if (ChkAddSub.Checked && tray.pointer[8].Value[1] == 1)
                        {
                            int LeftCount1 = ReadSubstrate();
                            WriteSubstrate(LeftCount1-1);
                        }
                    }
                    if (ChkRead.Checked && tray.pointer[9].Value[1] == 1)//重复读数
                    {
                        for (int j = 1; j < repeatRead; j++)
                        {
                            if (isNewWashEnd()) return;
                            WashSend.Initial();
                            WashSend.ReadFlag = 1;
                            errorflag = WashAddLiquidR(strModel);
                            if (errorflag != (int)ErrorState.Success)
                            {
                                NewWashEnd(3);
                                return;
                            }
                        }
                        Thread.Sleep(500);
                        NetCom3.Instance.ReceiveHandel -= GetReadNum2;
                    }
                }
                if (isNewWashEnd()) return;  //lyq add 20190822
                if (ChkLossTube.Checked && tray.pointer[10].Value[1] == 1)
                {
                    TExtAppend("正在从清洗盘扔第" + tubecount + "个管。");
                    errorflag = Move(strModel,MoveSate.WashLoss,(int)MoveHand.Movehand,(int)WashLossPos.ReadFinshPos);
                    if (errorflag != (int)ErrorState.Success)
                    {
                        NewWashEnd(1);
                        return;
                    }
                    tubecount++;
                    tray.pointer[10].Value[1] = 0;
                }
                if (isNewWashEnd()) return;
                errorflag = WashTurn(1);
                if (errorflag != (int)ErrorState.Success)
                {

                    NewWashEnd(3);
                    return;
                }
                tray.PoninterMoveOnePace();
                if (chkDelay.Checked)
                    NetCom3.Delay(20000);
                else
                    Thread.Sleep(500);
            }
            if (cbCleanTray.Checked)
            {
                LoopPourinto = 1;
                bLoopRun = true;
                TExtAppend("管路灌注开始。。。\n");
                TestLoopRun();
            }
            NewWashEnd();
        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (CancellationToken || !isNewWashRun)
            {
                MessageBox.Show("已经准备终止或者实验没有在运行。");
                return;
            }
            CancellationToken = true;
            NewWashEnd();
        }
        Thread ThNewMove;
        Thread ThMove;
        /// <summary>
        /// 温育反应盘清管
        /// Movetate 抓手标志 0-移新管抓手 1-移管手
        /// </summary>
        bool reactTrayTubeClear(int Movetate)
        {
            DataTable dtInTrayIni = ReadReact();
            //ThNewMove = new Thread(reactNewTrayClear);
            //ThNewMove.IsBackground = true;
            //ThNewMove.Start();
            //ThMove = new Thread(reactTrayClear);
            //ThMove.IsBackground = true;
            //ThMove.Start();
            //while (tubeCount < ReactTrayNum && !isNewWashEnd())
            //{
            //    NetCom3.Delay(1000);
            //}
            //if (tubeCount < ReactTrayNum|| isNewWashEnd())
            //    return false;
            for (int i = 0; i < dtInTrayIni.Rows.Count; i++)
            {
                if (isNewWashEnd()) return false;
                if (Movetate == 0)
                {
                    int statpos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                    int errorflag = Move(strModel,MoveSate.ReactLoss, Movetate, statpos);
                    if (errorflag != (int)ErrorState.IsNull && errorflag != (int)ErrorState.Success)
                    {
                        return false;
                    }
                    //修改反应盘信息
                }
                else
                {
                    int statpos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                    int errorflag = Move(strModel,MoveSate.ReactLoss, Movetate, statpos);
                    if (errorflag != (int)ErrorState.IsNull && errorflag != (int)ErrorState.Success)
                    {
                        return false;
                    }
                }
                int pos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                WriteReact(pos, 0);
            }
            return true;
        }
        private void reactNewTrayClear()
        {
            DataTable dtInTrayIni = ReadReact();
            for (int j = 0; j < ReactTrayNum / 40+1; j = j + 2)
            {
                for (int i = j * 40; i <(j + 1) * 40; i++)
                {
                    if (i >= 150) return;
                    if (isNewWashEnd()) return;
                    int statpos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                    int errorflag = Move(strModel,MoveSate.ReactLoss, 0, statpos);
                    //string order = "EB 90 31 01 05 " + statpos.ToString("x2");
                    //NetCom3.Instance.Send(NetCom3.Cover(order), (int)OrderSendType.MoveNewTube);
                    //bool errorflag = NetCom3.Instance.MoveQuery();
                    if (errorflag != (int)ErrorState.IsNull && errorflag != (int)ErrorState.Success)
                    {
                        NewWashEnd();
                        return;
                    }
                    int pos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                    WriteReact(pos, 0);
                    tubeCount ++ ;
                }
            }
        }
        private void reactTrayClear()
        {
            DataTable dtInTrayIni = ReadReact();
            for (int j = 1; j < (ReactTrayNum / 40)+1; j = j + 2)
            {
                for(int i=j*40; i<(j+1)*40;i++)
                {
                    if (i >= 150) return;
                    if (isNewWashEnd()) return;
                    int statpos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                    int errorflag = Move(strModel,MoveSate.ReactLoss, 1, statpos);
                    //string order = "EB 90 31 11 05 " + statpos.ToString("x2");
                    //NetCom3.Instance.Send(NetCom3.Cover(order), (int)OrderSendType.MoveTube);
                    //bool errorflag = NetCom3.Instance.Move2Query();
                    if (errorflag != (int)ErrorState.IsNull && errorflag != (int)ErrorState.Success)
                    {
                        NewWashEnd();
                        return;
                    }
                    int pos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                    WriteReact(pos, 0);
                    tubeCount++;
                }
            }
        }
        /// <summary>
        /// 清洗盘清管
        /// </summary>
        /// <returns></returns>
        bool washTrayTubeClear()
        {
            DataTable dtWashTrayIni = ReadWash();
            //2018-08-20 zlx mod
            int errorflag = 0;
            for (int i = 0; i < dtWashTrayIni.Rows.Count; i++)
            {
                if (isNewWashEnd()) return false;
                if (i != 0)
                {
                    //清洗盘顺时针旋转一位
                    errorflag = WashTurn(-1);
                    if (errorflag != (int)ErrorState.Success)
                    {
                        return false;
                    }
                    tubeHoleNum = tubeHoleNum - 1;
                    //如果孔号小于等于0
                    if (tubeHoleNum <= 0)
                    {
                        tubeHoleNum = tubeHoleNum + WashTrayNum;
                    }
                    dtWashTrayIni = ReadWash();
                    DataTable dtTemp = new DataTable();
                    dtTemp = dtWashTrayIni.Copy();
                    //清洗盘状态列表中添加反应盘位置字段，赋值需多赋值一个字段。 
                    for (int j = 1; j < 2; j++)
                        dtWashTrayIni.Rows[dtWashTrayIni.Rows.Count - 1][j] = dtTemp.Rows[0][j];
                    for (int k = 0; k < dtWashTrayIni.Rows.Count - 1; k++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            dtWashTrayIni.Rows[k][j] = dtTemp.Rows[k + 1][j];
                        }
                    }
                    WriteConfigToFile(ParaIniInfo.iniPathWashTrayInfo, dtWashTrayIni);
                }
                #region 移管手取放管位置取管扔废管
                LogFile.Instance.Write("==============  " + tubeHoleNum + "  扔管");
                errorflag = Move(strModel,MoveSate.WashLoss, (int)MoveHand.Movehand, (int)WashLossPos.PutTubePos);
                if (errorflag != (int)ErrorState.IsNull && errorflag != (int)ErrorState.Success)
                {
                    return false;
                }
                WriteWash(1, 0);
                #endregion
            }
            return true;
        }
        /// <summary>
        /// 底物管路灌注
        /// </summary>
        //2019.5.15  hly add
        void SubstratePipeline(int num)
        {
            this.BeginInvoke(new Action(() => { textBox1.AppendText("底物管路灌注。。。" + Environment.NewLine); }));
            int pos1 = WashAddSubPos;
            int Num = num;
            int substrateNum1 = ReadSubstrate();
            #region 清洗盘顺时针18位，然后放管
            int errorflag = Move(strModel,MoveSate.NewtubeToReact,(int)MoveHand.NewMovehand,1);
            if (errorflag != (int)ErrorState.Success)
            {
                NewWashEnd(1);
                return;
            }
             errorflag = WashTurn(1 - pos1);
            if (errorflag != (int)ErrorState.Success)
            {
                NewWashEnd(3);
                return;
            }
            errorflag = Move(strModel,MoveSate.ReactToWash, (int)MoveHand.Movehand, 1) ;
            if (errorflag != (int)ErrorState.Success)
            {
                NewWashEnd(1);
                return;
            }
            #region 取放管成功，相关配置文件修改
            WriteWash(pos1,1);
            #endregion
            #endregion
            #region 清洗盘逆时针旋转19位，回到19号孔，加底物位置
            errorflag = WashTurn(pos1-1);
            if (errorflag != (int)ErrorState.Success)
            {
                NewWashEnd(3);
                return;
            }
            #endregion
            while (Num > 0)
            {
                #region 底物注液
                WashSend.Initial();
                WashSend.AddSubstrateFlag = 1;
                errorflag = WashAddLiquidR(strModel);
                if (errorflag != (int)ErrorState.Success)
                {
                    NewWashEnd(3);
                    return;
                }
                substrateNum1 = substrateNum1 - 1;
                WriteSubstrate(substrateNum1);
                #endregion
                #region 清洗盘顺时针旋转2位，底物吸液位置
                errorflag = WashTurn(-2);
                if (errorflag != (int)ErrorState.Success)
                {
                    NewWashEnd(3);
                    return;
                }
                #region 吸液
                WashSend.Initial();
                WashSend.ImbibitionFlag = 1;
                errorflag = WashAddLiquidR(strModel);
                if (errorflag != (int)ErrorState.Success)
                {
                    NewWashEnd(3);
                    return;
                }
                #endregion
                #endregion
                #region 清洗盘转回底物注液位置，19号位
                errorflag = WashTurn(2);
                if (errorflag != (int)ErrorState.Success)
                {
                    NewWashEnd(3);
                    return;
                }
                #endregion
                Num--;
            }
            #region 加底物位置扔废管
            //清洗盘逆时针旋转12位转到取放管位置
            errorflag = WashTurn(1 - pos1);
            if (errorflag != (int)ErrorState.Success)
            {
                NewWashEnd(3);
                return;
            }
            errorflag = Move(strModel,MoveSate.WashLoss,(int)MoveHand.Movehand,(int)WashLossPos.PutTubePos);
            if (errorflag != (int)ErrorState.Success)
            {
                NewWashEnd(1);
                return;
            }
            WriteWash(pos1,0);
            #endregion
        }
        /// <summary>
        /// 循环灌注
        /// </summary>
        void TestLoopRun()
        {
            //BeginInvoke(new Action(() => { textBox1.Text = ""; }));
            #region 清空清洗盘
            TExtAppend("清空清洗盘。。。\n");
            bool b = washTrayTubeClear();
            if (!b)
            {
                NewWashEnd();
                return;
            }
            #endregion
            TExtAppend("清洗盘清管完成。。。\n");
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            if (!AddTubeInCleanTray())
            {
                NewWashEnd(1);
                return;
            }
            TExtAppend("夹第1个新管。。。\n");
            CleanTrayMovePace(WashAddSubPos-WashInjectPos[2]);
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            if (!AddTubeInCleanTray())
            {
                NewWashEnd(1);
                return;
            }
            TExtAppend("夹第2个新管。。。\n");
            CleanTrayMovePace(WashInjectPos[2]- WashInjectPos[1]);
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            if (!AddTubeInCleanTray())
            {
                NewWashEnd(1);
                return;
            }
            TExtAppend("夹第3个新管。。。\n");
            CleanTrayMovePace(WashInjectPos[1]- WashInjectPos[0]);
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            if (!AddTubeInCleanTray())
            {
                NewWashEnd();
                return;
            }
            TExtAppend("夹第4个新管。。。\n");
            CleanTrayMovePace(WashInjectPos[0]-1);
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            TExtAppend("灌注开始。。。\n");
            for (int i = 0; i < LoopPourinto; i++)
            {
                if (bLoopClick)
                    BeginInvoke(new Action(() => { numRepeat.Value = LoopPourinto - i; }));
                if (isNewWashEnd()) return;  //lyq add 20190822
                //底物灌注改为1次
                for (int index = 0; index < 1; index++)
                {
                    if (!bLoopRun)
                    {
                        NewWashEnd();
                        return;
                    }
                    CleanTrayWash(1);
                    Thread.Sleep(500);
                    CleanTrayMovePace(-1);
                    Thread.Sleep(500);
                    CleanTrayWash(2);
                    Thread.Sleep(500);
                    CleanTrayMovePace(-1);
                    Thread.Sleep(500);
                    CleanTrayWash(2);
                    Thread.Sleep(500);
                    CleanTrayMovePace(2);
                    Thread.Sleep(500);
                }
            }
            TExtAppend("灌注完成。。。\n");
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            CleanTrayMovePace(1-WashInjectPos[0]);
            if (!RemoveTubeOutCleanTray())
            {
                NewWashEnd(1);
                return;
            }
            TExtAppend("扔第1个新管完成。。。\n");
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            CleanTrayMovePace(WashInjectPos[0]- WashInjectPos[1]);
            if (!RemoveTubeOutCleanTray())
            {
                NewWashEnd(1);
                return;
            }
            TExtAppend("扔第2个新管完成。。。\n");
            CleanTrayMovePace(WashInjectPos[1]- WashInjectPos[2]);
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            if (!RemoveTubeOutCleanTray())
            {
                NewWashEnd(1);
                return;
            }
            TExtAppend("扔第3个新管完成。。。\n");
            CleanTrayMovePace(WashInjectPos[2]- WashAddSubPos);
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            if (!RemoveTubeOutCleanTray())
            {
                NewWashEnd(1);
                return;
            }
            TExtAppend("扔第4个新管完成。。。\n");
            NewWashEnd();
            TExtAppend("循环灌注完成。。。\n");
        }
        /// <summary>
        /// 向清洗盘加新管
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool AddTubeInCleanTray(int pos = 0)
        {
            int iNeedCool = 0;
            int IsKnockedCool = 0;
            AgainNewMove:
            if (isNewWashEnd()) return false;
            int errorflag = Move(strModel,MoveSate.NewtubeToReact, (int)MoveHand.NewMovehand, 1);
            if (errorflag != (int)ErrorState.Success)
            {
                #region 发生异常处理
                if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.IsNull)
                {
                    iNeedCool++;
                    if (iNeedCool < 2)
                        goto AgainNewMove;
                    else
                    {
                        DialogResult tempresult = MessageBox.Show("移管手抓新管抓空！", "移管手错误！", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.LackTube)
                {
                    DialogResult tempresult = MessageBox.Show("理杯机缺管，实验停止运行!", "移管手错误！", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return false;
                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.Sendfailure)
                {
                    if (NetCom3.Instance.waitAndAgainSend != null && NetCom3.Instance.waitAndAgainSend is Thread)
                    {
                        NetCom3.Instance.waitAndAgainSend.Abort();
                    }
                    goto AgainNewMove;
                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.IsKnocked)
                {
                    IsKnockedCool++;
                    if (IsKnockedCool < 2)
                        goto AgainNewMove;
                    else
                    {
                        DialogResult tempresult = MessageBox.Show("移管手在暂存盘向温育盘取放管处抓管发生撞管，实验将进行停止！", "移管手错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return false;
                    }

                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.putKnocked)
                {
                    IsKnockedCool++;
                    if (IsKnockedCool < 2)
                        goto AgainNewMove;
                    else
                    {
                        DialogResult tempresult = MessageBox.Show("移管手在清洗盘抓管时发生撞管，实验将进行停止！", "移管手错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.OverTime)
                {
                    DialogResult tempresult = MessageBox.Show("移管手在暂存盘向温育盘取放管处抓管接收数据超时，实验将进行停止！", "移管手错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return false;
                }
                #endregion
            }
            if (isNewWashEnd()) return false;
            errorflag = Move(strModel,MoveSate.ReactToWash,(int)MoveHand.Movehand,1);
            if (errorflag != (int)ErrorState.Success)
            {
                #region 发生异常处理
                if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.IsNull)
                {
                    iNeedCool++;
                    if (iNeedCool < 2)
                        goto AgainNewMove;
                    else
                    {
                        DialogResult tempresult = MessageBox.Show("移管手在温育盘向清洗盘抓空！实验将停止运行！", "移管手错误！", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.LackTube)
                {
                    DialogResult tempresult = MessageBox.Show("理杯机缺管，实验停止运行!", "移管手错误！", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return false;
                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.Sendfailure)
                {
                    if (NetCom3.Instance.waitAndAgainSend != null && NetCom3.Instance.waitAndAgainSend is Thread)
                    {
                        NetCom3.Instance.waitAndAgainSend.Abort();
                    }
                    goto AgainNewMove;
                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.IsKnocked)
                {
                    IsKnockedCool++;
                    if (IsKnockedCool < 2)
                        goto AgainNewMove;
                    else
                    {
                        DialogResult tempresult = MessageBox.Show("移管手在温育盘向清洗盘取放管处抓管发生撞管，实验将进行停止！", "移管手错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return false;
                    }

                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.putKnocked)
                {
                    IsKnockedCool++;
                    if (IsKnockedCool < 2)
                        goto AgainNewMove;
                    else
                    {
                        DialogResult tempresult = MessageBox.Show("移管手在温育盘向清洗盘抓管时发生撞管，实验将进行停止！", "移管手错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.OverTime)
                {
                    DialogResult tempresult = MessageBox.Show("移管手在温育盘向清洗盘取放管处抓管接收数据超时，实验将进行停止！", "移管手错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return false;
                }
                #endregion
            }
            return true;
        }
        /// <summary>
        /// 清洗盘旋转固定孔数
        /// </summary>
        /// <param name="pace"></param>
        void CleanTrayMovePace(int pace)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            DataTable dtWashTrayIni = ReadWash();
            AgainNewMove:
            int errorflag = WashTurn(pace);
            if (isNewWashEnd()) return;  //lyq add 20190822
            if (errorflag != (int) ErrorState.Success)
            {
                if (NetCom3.Instance.WasherrorFlag == (int)ErrorState.Sendfailure)
                    goto AgainNewMove;
                else if (NetCom3.Instance.WasherrorFlag == (int)ErrorState.OverTime)
                {
                    NetCom3.Instance.stopsendFlag = true;
                    frmMsgShow.MessageShow("清洗指令错误提示", "指令接收超时，实验已终止");
                }
                else
                    return;

            }
            countWashHole(pace);
        }
        /// <summary>
        /// 清洗抽液灌注
        /// </summary>
        /// <param name="oneOrTwo"></param>
        void CleanTrayWash(int oneOrTwo)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            WashSend.Initial();
            int substrateNum1 = 0;
            if (oneOrTwo == 1)
            {
                substrateNum1 = ReadSubstrate();
                WashSend.AddSubstrateFlag = 1;
                WashSend.LiquidInjectionFlag[0] = 1;
                WashSend.LiquidInjectionFlag[1] = 1;
                WashSend.LiquidInjectionFlag[2] = 1;
                WashSend.AddSubstrateFlag = 1;
            }
            else if (oneOrTwo == 2)
            {
                WashSend.ImbibitionFlag = 1;
            }
            else
            {
                throw new Exception();
            }
            AgainNewMove:
            if (isNewWashEnd()) return;  //lyq add 20190822
            int errorflag = WashAddLiquidR(strModel);
            if (errorflag != (int)ErrorState.Success)
            {
                if (NetCom3.Instance.WasherrorFlag == (int)ErrorState.Sendfailure)
                    goto AgainNewMove;
                else if (NetCom3.Instance.WasherrorFlag == (int)ErrorState.OverTime)
                {
                    NetCom3.Instance.stopsendFlag = true;
                    frmMsgShow.MessageShow("清洗指令错误提示", "指令接收超时，实验已终止");
                }
                else
                    return;
            }
            else
            {
                if (oneOrTwo == 1)
                {
                    substrateNum1--;
                    WriteSubstrate(substrateNum1);
                }
            }
        }
        private bool RemoveTubeOutCleanTray()
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            int iNeedCool = 0;
            int IsKnockedCool = 0;
            AgainNewMove:
            if (isNewWashEnd()) return false;
            int errorflag = Move(strModel,MoveSate.WashLoss,(int)MoveHand.Movehand,(int)WashLossPos.PutTubePos);
            if (errorflag != (int)ErrorState.Success)
            {
                #region 发生异常处理
                if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.IsNull)
                {
                    iNeedCool++;
                    if (iNeedCool < 2)
                        goto AgainNewMove;
                    else
                    {
                        DialogResult tempresult = frmMsgShow.MessageShow("移管手错误", "移管手在清洗盘扔废管时多次抓空，实验将进行停止！");
                        return false;
                    }
                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.Sendfailure)
                {
                    if (NetCom3.Instance.waitAndAgainSend != null && NetCom3.Instance.waitAndAgainSend is Thread)
                    {
                        NetCom3.Instance.waitAndAgainSend.Abort();
                    }
                    goto AgainNewMove;
                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.IsKnocked)
                {
                    IsKnockedCool++;
                    if (IsKnockedCool < 2)
                    {
                        goto AgainNewMove;
                    }
                    else
                    {
                        DialogResult tempresult = frmMsgShow.MessageShow("移管手错误", "移管手在清洗盘扔废管时发生撞管，实验将进行停止！");
                        return false;
                    }
                }
                else if (NetCom3.Instance.Move2rrorFlag == (int)ErrorState.OverTime)
                {
                    DialogResult tempresult = frmMsgShow.MessageShow("移管手错误", "移管手在清洗盘扔废管时接收数据超时，实验将进行停止！");
                    return false;
                }
                #endregion
            }
            return true;
        }
        class CleanTray
        {
            LinkedList<int[]> tray = new LinkedList<int[]>();
            public List<LinkedListNode<int[]>> pointer { get; set; }

            public CleanTray()
            {
                pointer = new List<LinkedListNode<int[]>>(11);
                for (int i = 0; i < WashTrayNum; i++)
                {
                    int[] temp = new int[2] { i + 1, 0 };
                    LinkedListNode<int[]> tempnode = new LinkedListNode<int[]>(temp);
                    tray.AddLast(tempnode);
                    if (i == 0) pointer.Add(tempnode);
                    if (i == (WashPumPos[0]-1)) pointer.Add(tempnode);
                    if (i == (WashInjectPos[0]-1)) pointer.Add(tempnode);
                    if (i == (WashPumPos[1] - 1)) pointer.Add(tempnode);
                    if (i == (WashInjectPos[1] - 1)) pointer.Add(tempnode);
                    if (i == (WashPumPos[2] - 1)) pointer.Add(tempnode);
                    if (i == (WashInjectPos[2] - 1)) pointer.Add(tempnode);
                    if (i == (WashPumPos[3] - 1)) pointer.Add(tempnode);
                    if (i == (WashAddSubPos - 1)) pointer.Add(tempnode);
                    if (i == (ReadPos-1)) pointer.Add(tempnode);
                    if (i == (WashTrayNum - 2)) pointer.Add(tempnode);
                }
            }
            public CleanTray(int starhole)
                : this()
            {
                while (starhole != pointer[0].Value[0])
                {
                    PoninterMoveOnePace();
                }
            }
            public void PoninterMoveOnePace()
            {
                for (int i = 0; i < 11; i++)
                {
                    pointer[i] = pointer[i].Previous;
                    if (pointer[i] == null) pointer[i] = tray.Last;
                }
            }

            public bool needStay(bool wash, bool addbase, bool read)
            {
                bool stay = false;
                if (wash)
                {
                    for (int i = 1; i < 8; i++)
                    {
                        if (pointer[i].Value[1] == 1)
                        {
                            stay = true;
                            return stay;
                        }
                    }
                }
                if (addbase && pointer[8].Value[1] == 1 || (read && pointer[9].Value[1] == 1))
                {
                    stay = true;
                    return stay;
                }
                return stay;
            }
            public int WashAddLiquidR(string strModel,bool wash, bool addbase, bool read, int washPipe)
            {
                WashSend.Initial();
                if (wash && (pointer[1].Value[1] == 1 || pointer[3].Value[1] == 1 || pointer[5].Value[1] == 1 || pointer[7].Value[1] == 1))
                {
                    WashSend.ImbibitionFlag = 1;
                }
                if (wash)
                {
                    WashSend.LiquidInjectionFlag[0] = pointer[2].Value[1];
                    WashSend.LiquidInjectionFlag[1] = pointer[4].Value[1];
                    WashSend.LiquidInjectionFlag[2] = pointer[6].Value[1];
                }
                if (addbase)
                {
                    WashSend.AddSubstrateFlag = pointer[8].Value[1];
                    WashSend.substratePipe = 1;
                }
                if (read && pointer[9].Value[1] == 1)
                {
                    WashSend.ReadFlag = pointer[9].Value[1];
                }
                int errorflag = WashSend.WashAddLiquidR(strModel);
                return errorflag;
            }
        }
        #endregion
        bool bLoopRun = false;
        int LoopPourinto = 0;
        Thread Loopthread = null;
        bool bLoopClick = false;
        bool IsClearNewTube = false;
        private void btnPourInto_Click(object sender, EventArgs e)
        {
            if (btnPourInto.Text == "停止")
            {
                #region 停止灌注
                NewWashEnd();
                btnPourInto.Text = "执行";
                #endregion
            }
            else
            {
                textBox1.Text = "";
                if (isNewWashRun)
                {
                    MessageBox.Show("正在运行。");
                    return;
                }
                btnPourInto.Enabled = false;
                textBox1.Text = "";
                btnPourInto.Text = "停止";
                #region 判断运行条件
                if (numRepeat.Value.ToString().Trim() == "0")
                {
                    frmMessageShow f = new frmMessageShow();
                    f.MessageShow("温馨提示", "灌注次数为0，请输入适当灌注次数！");
                    btnPourInto.Enabled = true;
                    btnPourInto.Text = "执行";
                    return;
                }
                else if (cmbSelectAct.SelectedItem.ToString() == "磁珠清洗液灌注" && numRepeat.Value > 20)
                {
                    frmMessageShow f = new frmMessageShow();
                    f.MessageShow("温馨提示", "磁珠清洗液灌注限制最大次数为20，请输入适当灌注次数！");
                    btnPourInto.Enabled = true;
                    btnPourInto.Text = "执行";
                    return;
                }
                else if (cmbSelectAct.SelectedItem.ToString() == "探针清洗液灌注" && numRepeat.Value > 10)
                {
                    frmMessageShow f = new frmMessageShow();
                    f.MessageShow("温馨提示", "探针清洗液灌注限制最大次数为10，请输入适当灌注次数！");
                    btnPourInto.Enabled = true;
                    btnPourInto.Text = "执行";
                    return;
                }
                #endregion
                #region 启用灌注线程

                isNewWashRun = true;
                LoopPourinto = int.Parse(numRepeat.Value.ToString());
                switch (cmbSelectAct.SelectedItem.ToString())
                {
                    case "循环灌注":
                        bLoopRun = true;
                        CancellationToken = false;
                        if (Loopthread == null || Loopthread.ThreadState != ThreadState.Running)
                        {
                            LoopPourinto = int.Parse(numRepeat.Value.ToString());
                            TExtAppend("循环灌注开始。。。\n");
                            bLoopClick = true;
                            Loopthread = new Thread(new ThreadStart(TestLoopRun));
                            Loopthread.IsBackground = true;
                            Loopthread.Start();
                            btnPourInto.Enabled = true;
                        }
                        break;
                    case "磁珠清洗液灌注":
                        bLoopRun = true;
                        CancellationToken = false;
                        IsClearNewTube = checkNewTube.Checked ;
                        if (Loopthread == null || Loopthread.ThreadState != ThreadState.Running)
                        {
                            LoopPourinto = int.Parse(numRepeat.Value.ToString());
                            TExtAppend("磁珠清洗液灌注。。。\n");
                            bLoopClick = true;
                            Loopthread = new Thread(new ThreadStart(ClearPourIntoRun));
                            Loopthread.IsBackground = true;
                            Loopthread.Start();
                            btnPourInto.Enabled = true;
                        }
                        break;
                    case "底物灌注":
                        bLoopRun = true;
                        CancellationToken = false;
                        IsClearNewTube = checkNewTube.Checked;
                        if (Loopthread == null || Loopthread.ThreadState != ThreadState.Running)
                        {
                            LoopPourinto = int.Parse(numRepeat.Value.ToString());
                            TExtAppend("底物灌注开始。。。\n");
                            bLoopClick = true;
                            Loopthread = new Thread(new ThreadStart(SubstratePourIntoRun));
                            Loopthread.IsBackground = true;
                            Loopthread.Start();
                            btnPourInto.Enabled = true;
                        }
                        break;
                    case "加样针清洗液灌注":
                        bLoopRun = true;
                        CancellationToken = false;
                        if (Loopthread == null || Loopthread.ThreadState != ThreadState.Running)
                        {
                            LoopPourinto = int.Parse(numRepeat.Value.ToString());
                            TExtAppend("探针清洗液灌注开始。。。\n");
                            bLoopClick = true;
                            Loopthread = new Thread(new ThreadStart(AddSPPourIntoRun));
                            Loopthread.IsBackground = true;
                            Loopthread.Start();
                            btnPourInto.Enabled = true;
                        }
                        break;
                    case "加试剂针清洗液灌注":
                        bLoopRun = true;
                        CancellationToken = false;
                        if (Loopthread == null || Loopthread.ThreadState != ThreadState.Running)
                        {
                            LoopPourinto = int.Parse(numRepeat.Value.ToString());
                            TExtAppend("探针清洗液灌注开始。。。\n");
                            bLoopClick = true;
                            Loopthread = new Thread(new ThreadStart(AddSRPourIntoRun));
                            Loopthread.IsBackground = true;
                            Loopthread.Start();
                            btnPourInto.Enabled = true;
                        }
                        break;
                }
                #endregion
            }
        }
        /// <summary>
        /// 磁珠清洗液灌注
        /// </summary>
        void ClearPourIntoRun()
        {
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            if (IsClearNewTube)
            {

                #region 清空清洗盘
                TExtAppend("清空清洗盘。。。\n");
                bool b = washTrayTubeClear();
                if (!b)
                {
                    TExtAppend("清洗盘清管异常。。。\n");
                    NewWashEnd();
                    return;
                }
                TExtAppend("清洗盘清管完成。。。\n");
                #endregion
                TExtAppend("开始夹新管。。。\n");
                if (isNewWashEnd()) return;
                if (!AddTubeInCleanTray())
                {
                    NewWashEnd(1);
                    return;
                }
                CleanTrayMovePace(WashInjectPos[2]- WashInjectPos[1]);
                if (isNewWashEnd()) return;
                if (!AddTubeInCleanTray())
                {
                    NewWashEnd(1);
                    return;
                }
                if (isNewWashEnd()) return;
                CleanTrayMovePace(WashInjectPos[1]- WashInjectPos[0]);
                if (!AddTubeInCleanTray())
                {
                    NewWashEnd(1);
                    return;
                }
                CleanTrayMovePace(WashInjectPos[0]-1);
            }
            TExtAppend("开始灌注。。。\n");
            for (int i = 0; i < LoopPourinto; i++)
            {
                if (isNewWashEnd()) return;
                if (bLoopClick)
                    BeginInvoke(new Action(() => { numRepeat.Value = LoopPourinto - i; }));
                if (!bLoopRun)
                {
                    NewWashEnd();
                    return;
                }
                //清洗管路灌注
                if (!CleanPowerInto(IsClearNewTube))
                {
                    NewWashEnd(3);
                    return;
                }
                if (IsClearNewTube)
                {
                    CleanTrayMovePace(-1);
                    NetCom3.Instance.Send(NetCom3.Cover("EB 90 11 0B 01 00"), 5);
                    if (!(NetCom3.Instance.SingleQuery() && NetCom3.Instance.errorFlag == (int)ErrorState.Success))
                    {
                        TExtAppend("灌注失败，错误类型为 " + Enum.GetName(typeof(ErrorState), NetCom3.Instance.errorFlag) + "\n");
                        NewWashEnd();
                        return;
                    }
                    CleanTrayMovePace(1);
                }
            }
            TExtAppend("灌注完成。。。\n");
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            if (IsClearNewTube)
            {
                CleanTrayMovePace(1-WashInjectPos[0]);
                if (!RemoveTubeOutCleanTray())
                {
                    NewWashEnd(1);
                    return;
                }
                if (!bLoopRun)
                {
                    NewWashEnd();
                    return;
                }
                CleanTrayMovePace(WashInjectPos[0]- WashInjectPos[1]);
                if (!RemoveTubeOutCleanTray())
                {
                    NewWashEnd(1);
                    return;
                }
                CleanTrayMovePace(WashInjectPos[1]- WashInjectPos[2]);
                if (!bLoopRun)
                {
                    NewWashEnd();
                    return;
                }
                if (!RemoveTubeOutCleanTray())
                {
                    NewWashEnd(1);
                    return;
                }
            }
            NewWashEnd();
            TExtAppend("磁珠清洗液灌注完成。。。\n");
        }
        /// <summary>
        /// 加样针探针清洗液灌注
        /// </summary>
        void AddSPPourIntoRun()
        {
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            if (isNewWashEnd()) return;
            TExtAppend("开始灌注。。。\n");
            for (int i = 0; i < LoopPourinto; i++)
            {
                if (isNewWashEnd()) return;  //lyq add 20190822
                if (bLoopClick)
                    BeginInvoke(new Action(() => { numRepeat.Value = LoopPourinto - i; }));

                NetCom3.Instance.Send(NetCom3.Cover("EB 90 31 02 08"),(int)OrderSendType.AddSp);
                if (!NetCom3.Instance.SPQuery() && NetCom3.Instance.AdderrorFlag != (int)ErrorState.Success)
                {
                    TExtAppend("灌注失败，错误类型为 " + Enum.GetName(typeof(ErrorState), NetCom3.Instance.AdderrorFlag) + "\n");
                    NewWashEnd();
                    return;
                }
                Thread.Sleep(2000);
            }
            if (isNewWashEnd()) return;
            BeginInvoke(new Action(() => { btnPourInto.Text = "执行"; }));
            bLoopRun = false;
            btnPourInto.Enabled = true;
            NewWashEnd();
            TExtAppend("探针清洗液灌注完成。。。\n");
        }
        /// <summary>
        /// 加试剂针探针清洗液灌注
        /// </summary>
        void AddSRPourIntoRun()
        {
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            if (isNewWashEnd()) return;
            TExtAppend("开始灌注。。。\n");
            for (int i = 0; i < LoopPourinto; i++)
            {
                if (isNewWashEnd()) return;  //lyq add 20190822
                if (bLoopClick)
                    BeginInvoke(new Action(() => { numRepeat.Value = LoopPourinto - i; }));

                NetCom3.Instance.Send(NetCom3.Cover("EB 90 31 12 08"), (int)OrderSendType.AddSR);
                if (!NetCom3.Instance.SP2Query() && NetCom3.Instance.Add2errorFlag != (int)ErrorState.ReadySend)
                {
                    TExtAppend("灌注失败，错误类型为 " + Enum.GetName(typeof(ErrorState), NetCom3.Instance.Add2errorFlag) + "\n");
                    NewWashEnd();
                    return;
                }
                Thread.Sleep(2000);
            }
            if (isNewWashEnd()) return;
            BeginInvoke(new Action(() => { btnPourInto.Text = "执行"; }));
            bLoopRun = false;
            btnPourInto.Enabled = true;
            NewWashEnd();
            TExtAppend("探针清洗液灌注完成。。。\n");
        }
        /// <summary>
        /// 清洗管路灌注
        /// </summary>
        /// <param name="IsTube">是否有管</param>
        bool CleanPowerInto(bool IsTube)
        {
            string order = "";
            if (IsTube)
                order = "EB 90 11 0B 01 03";
            else
                order = "EB 90 11 0B 00 03";
            NetCom3.Instance.Send(NetCom3.Cover(order), 5);
            //if (!NetCom3.Instance.SingleQuery())
            //    return false;
            if (!NetCom3.Instance.SingleQuery() && NetCom3.Instance.errorFlag != (int)ErrorState.ReadySend)
            {
                TExtAppend("灌注失败，错误类型为 " + Enum.GetName(typeof(ErrorState), NetCom3.Instance.errorFlag) + "\n");
                return false;
            }
            else
                return true;
        }
        /// <summary>
        /// 底物灌注
        /// </summary>
        void SubstratePourIntoRun()
        {
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            if (IsClearNewTube)
            {

                #region 清空清洗盘
                TExtAppend("清空清洗盘。。。\n");
                bool b = washTrayTubeClear();
                if (!b)
                {
                    TExtAppend("清洗盘清管异常。。。\n");
                    NewWashEnd();
                    return;
                }
                TExtAppend("清洗盘清管完成。。。\n");
                #endregion
                TExtAppend("开始夹新管。。。\n");
                if (isNewWashEnd()) return;
                if (!AddTubeInCleanTray())
                {
                    NewWashEnd(1);
                    return;
                }
                CleanTrayMovePace(WashAddSubPos-1);
            }
            TExtAppend("开始灌注。。。\n");
            for (int i = 0; i < LoopPourinto; i++)
            {
                if (isNewWashEnd()) return;
                if (bLoopClick)
                    BeginInvoke(new Action(() => { numRepeat.Value = LoopPourinto - i; }));
                if (!bLoopRun)
                {
                    NewWashEnd();
                    return;
                }
                if (!SubstratePourIn(IsClearNewTube))
                {
                    NewWashEnd(3);
                    return;
                }
                if (IsClearNewTube)
                {
                    CleanTrayMovePace(-2);
                    WashSend.Initial();
                    WashSend.ImbibitionFlag = 1;
                    int errorflag = WashAddLiquidR(strModel);
                    if ( NetCom3.Instance.WasherrorFlag != (int)ErrorState.Success)
                    {
                        TExtAppend("灌注失败，错误类型为 " + Enum.GetName(typeof(ErrorState), NetCom3.Instance.WasherrorFlag) + "\n");
                        NewWashEnd();
                        return;
                    }
                    CleanTrayMovePace(2);
                }
            }
            TExtAppend("灌注完成。。。\n");
            if (!bLoopRun)
            {
                NewWashEnd();
                return;
            }
            if (IsClearNewTube)
            {
                CleanTrayMovePace(1-WashAddSubPos);
                if (!RemoveTubeOutCleanTray())
                {
                    NewWashEnd(1);
                    return;
                }
            }
            NewWashEnd();
            TExtAppend("底物灌注完成。。。\n");
        }
        /// <summary>
        /// <summary>
        /// 底物管路灌注
        /// </summary>
        /// <param name="IsTube"></param>
        /// <returns></returns>
        bool SubstratePourIn(bool IsTube)
        {
            string order = "";
            if (IsTube)
            {
                WashSend.Initial();
                WashSend.AddSubstrateFlag = 1;
                int errorflag = WashAddLiquidR(strModel);
                if (errorflag != (int)ErrorState.Success)
                    return false;
            }
            else
            {
                order = "EB 90 11 07 07 00";
                NetCom3.Instance.Send(NetCom3.Cover(order), 5);
                if (!NetCom3.Instance.SingleQuery())
                    return false;
            }
            return true;
        }
    }
}

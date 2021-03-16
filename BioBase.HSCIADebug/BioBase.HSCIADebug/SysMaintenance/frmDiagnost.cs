using BioBase.HSCIADebug.ControlInfo;
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
namespace BioBase.HSCIADebug.SysMaintenance
{
    public partial class frmDiagnost : frmParent
    {
        #region 变量
        /// <summary>
        /// 返回指令
        /// </summary>
        string BackObj = "";
        /// <summary>
        /// 清洗盘取放管位置当前孔号
        /// </summary>
        int currentHoleNum = 1;
        /// <summary>
        /// 存储线程，返回按钮结束时使用
        /// </summary>
        List<Thread> threadList = new List<Thread>();
        /// <summary>
        /// 清洗盘信息
        /// </summary>
        DataTable dtWashTrayInfo = new DataTable();
        /// <summary>
        /// 界面可关闭标志
        /// </summary>
        bool bClose = true;
        #endregion
        /// <summary>
        /// 发送移管手指令
        /// </summary>
        /// <param name="strModel">级联机器</param>
        /// <param name="MoveSate">抓手标志 0-移新管 1-移管 </param>
        /// <param name="startpos">开始位置</param>
        /// <param name="goalpos">结束位置</param>
        /// <returns></returns>
        public int Move(string strModel, MoveSate MoveSate, int movehand,int startpos = 0, int goalpos = 0)
        {
            MoveHandSend.MoveSate = MoveSate;
            
            if(movehand ==(int)MoveHand.Movehand)
            {
                MoveHandSend.Move2(strModel,startpos, goalpos);
                return NetCom3.Instance.Move2rrorFlag;
            }
            else
            {
                MoveHandSend.Move(strModel,startpos, goalpos);
                return NetCom3.Instance.MoverrorFlag;
            }
                
        }
        /// <summary>
        /// 发送加样机指令
        /// </summary>
        /// <param name="AddLiquid">仪器级联编号</param>
        /// <param name="step">加样开始位置</param>
        /// <param name="startpos">加样开始位置</param>
        /// <param name="endpos">加样结束位置</param>
        /// <param name="liquidVol">加样体积</param>
        /// <param name="leftVol">剩余体积</param>
        /// <returns></returns>
        public int AddLiquid(string strModel,AddLiquidStep step, int startpos = 0, int endpos = 0, int liquidVol = 0, int leftVol = 0)
        {
            if (MAddSR == 0)
            {
                AddLiquidSend.Liquidstep = step;
                AddLiquidSend.AddSample(strModel,startpos, endpos, liquidVol, leftVol);
                return NetCom3.Instance.AdderrorFlag;
            }
            else
            {
                AddLiquidSend.Liquidstep = step;
                AddLiquidSend.AddReagent(strModel,startpos, endpos, liquidVol, leftVol);
                return NetCom3.Instance.Add2errorFlag;
            }

        }
        /// <summary>
        /// 清洗盘旋转
        /// </summary>
        /// <param name="num">旋转孔数</param>
        /// <returns></returns>
        public int WashTurn(string strModel,int num)
        {
            return WashSend.WashTurn(strModel,num);
        }
        /// <summary>
        /// 批量更新清洗盘配置信息
        /// </summary>
        public void UpdatConfigWash()
        {
            //查询清洗盘信息
            dtWashTrayInfo = ReadWash();
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
        /// <summary>
        /// 获取重新计算的PMT值
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public  double GetPMT(double temp)
        {
            return WashSend.GetPMT(temp);
        }
        #region 读写配置文件
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
        /// 读温育盘配置信息
        /// </summary>
        /// <returns></returns>
        public DataTable ReadReact()
        {
            DataTable dt = OperateIniFile.ReadConfig(ParaIniInfo.iniPathReactTrayInfo);
            return dt;
        }
        public string ReadReact(int pos)
        {
            string Rpos = "";
            return Rpos;
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
        /// 更改配置文件,InstrumentPara,tempreature
        /// </summary>
        public void iniseter()
        {
            OperateIniFile.WriteIniPara("temperature", "numOfSam", numOfSam.ToString());
            OperateIniFile.WriteIniPara("temperature", "timespan", timespan.ToString());
            OperateIniFile.WriteIniPara("temperature", "num1", num1.ToString());
            OperateIniFile.WriteIniPara("temperature", "num2", num2.ToString());
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
        /// 读取保存指令配置文件
        /// </summary>
        /// <param name="strModel">级联机器编号</param>
        /// <param name="state">调试模块编号</param>
        /// 配置文件的保存格式“机器编码 调试模块 模块名称 位置 = 机器编码 调试模块 模块名称 位置 数据1 数据2”
        /// <returns></returns>
        private List<string> GetData(string strModel, string state)
        {
            string path = Directory.GetCurrentDirectory() + "\\LocationData.ini";
            if (!File.Exists(path))
            {
                MessageBox.Show("在指定目录下不存在数据文件文件，请确认！");
                return new List<string>();
            }

            List<string> data = new List<string>();

            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("="))
                    data.Add(line);
            }

            List<string> finalData = data.Where(item => (item.Substring(0, 2) == strModel && item.Substring(3, 2) == state)).ToList();

            return finalData;
        }
        #endregion
        public frmDiagnost()
        {
            InitializeComponent();
        }
        private void frmDiagnost_Load(object sender, EventArgs e)
        {
            cmbMcontrol.SelectedIndex = 0;
            cmbMAddSR.SelectedIndex = 0;
            cmbMMove.SelectedIndex = 0;
            cmbMOrbiter.SelectedIndex = 0;
            cmbMIncubation.SelectedIndex = 0;
            cmbChooseMove.SelectedIndex = 0;
            bClose = false;
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
            //block end
            //查询清洗盘管信息
            dtWashTrayInfo = ReadWash(); 
            iniReader();//读取温度监控参数信息
            numOfSample.Value = (decimal)numOfSam;//更新控件的值
            timespanOfSample.Value = timespan;
            numDown.Value = (decimal)num1;
            numUp.Value = (decimal)num2;
            chart1.ChartAreas[0].AxisX.Maximum = numOfSam;//确定图标的属性
            timer1.Interval = Convert.ToInt32(timespan) * 1000;//timer1的间隔时间
            chart1.ChartAreas[0].AxisY.Minimum = num1;
            chart1.ChartAreas[0].AxisY.Maximum = num2;
            saveFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + @"\daily";//默认路径
            // 停止试剂盘旋转
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 31 12 09 01"), (int)OrderSendType.AddSR);
            while (!NetCom3.Sp2ReciveFlag)
            {
                NetCom3.Delay(1);
            }
            bClose = true;
        }
        void Instance_ReceiveHandel(string obj)
        {
            if (obj.IsNullOrEmpty())
            {
                return;
            }
            else
            {
                BackObj = obj;
            }
        }
        void Read(string order)//y mod 20180816
        {
            if (!order.Contains("EB 90 11 AF"))
                return;
            string[] dataRecive = order.Split(' ');
            decimal readData = Math.Round(Convert.ToDecimal(NetCom3.HexToFloat(dataRecive[12] + dataRecive[13] + dataRecive[14] + dataRecive[15])), 1);
            #region 读取仪器实际温度
            if (dataRecive[5] == "04")
            {
                if ((beginAndStop.Text != "开始"))
                {
                    switch (dataRecive[4])
                    {
                        case "04":
                            Wlist.Add(readData);
                            while (Wlist.Count > numOfSam)
                            {
                                Wlist.RemoveAt(0);
                            }
                            break;
                        case "05":
                            Qlist.Add(readData);
                            while (Qlist.Count > numOfSam)
                            {
                                Qlist.RemoveAt(0);
                            }
                            break;
                        case "06":
                            Qglist.Add(readData);
                            while (Qglist.Count > numOfSam)
                            {
                                Qglist.RemoveAt(0);
                            }
                            break;
                        case "07":
                            Dlist.Add(readData);
                            while (Dlist.Count > numOfSam)
                            {
                                Dlist.RemoveAt(0);
                            }
                            break;
                        case "08":
                            Rlist.Add(readData);
                            while (Rlist.Count > numOfSam)
                            {
                                Rlist.RemoveAt(0);
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    while (this == null || !this.IsHandleCreated)
                    { }
                    //if (!btnSelect.Enabled)
                        this.Invoke(new SetTextCallBack(SettxtStandard), readData.ToString());
                    //this.Invoke(new SetTextCallBack(SettxtStandard), readData.ToString());
                }

            }
            #endregion
            #region 读取仪器校准温度
            if (dataRecive[5] == "06")
            {
                switch (dataRecive[4])
                {
                    case "04":
                        this.Invoke(new SetTextCallBack(SettxtStandard), readData.ToString());
                        break;
                    case "05":
                        this.Invoke(new SetTextCallBack(SettxtStandard), readData.ToString());
                        break;
                    case "06":
                        this.Invoke(new SetTextCallBack(SettxtStandard), readData.ToString());
                        break;
                    case "07":
                        this.Invoke(new SetTextCallBack(SettxtStandard), readData.ToString());
                        break;
                    case "08":
                        this.Invoke(new SetTextCallBack(SettxtStandard), readData.ToString());
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDiagnost_SizeChanged(object sender, EventArgs e)
        {
            formSizeChange(this);
        }
        #region 加样模块
        /// <summary>
        /// 加样针模块表示 0-加样针模块 1-加试剂针模块
        /// </summary>
        int MAddSR =0;
        /// <summary>
        /// 加样针位置列表 
        /// "样本位置(急诊)", "样本位置(普通)", "温育盘", "清洗杯位置"
        /// </summary>
        string[] AddSPPos = { "样本位置(急诊)", "样本位置(普通)", "温育盘", "清洗杯位置" };
        /// <summary>
        /// 加试剂针位置列表
        /// "R1位置", "R2位置", "R3位置", "稀释液位置", "磁珠位置","温育盘", "清洗杯位置" 
        /// </summary>
        string[] AddSRPos = { "R1位置", "R2位置", "R3位置", "稀释液位置", "磁珠位置", "温育盘", "清洗杯位置" };
        /// <summary>
        /// 加样针模块电机列表 "旋转电机","垂直电机"
        /// </summary>
        string[] AddSPAEM = { "旋转电机", "垂直电机"};//, "急诊轨道电机", "普通轨道电机" 
        /// <summary>
        /// 加试剂模块电机列表 
        /// "旋转电机", "垂直电机", "试剂盘电机"
        /// </summary>
        string[] AddSRAEM = { "旋转电机", "垂直电机", "试剂盘电机" };
        /// <summary>
        /// 样品盘位置
        /// </summary>
        int SamplePos = 10;
        /// <summary>
        /// 加试剂动作
        /// "加稀释液","加R1", "加R2", "加R3", "加磁珠"
        /// </summary>
        string[] AddRMove = {"加稀释液","加R1", "加R2", "加R3", "加磁珠"};
        /// <summary>
        /// 加样本动作
        ///  "加普通样本", "加急诊样本", "加稀释后的样本"
        /// </summary>
        string[] AddSMove = { "加普通样本", "加急诊样本", "加稀释后的样本" };
        /// <summary>
        /// 级联机器编号
        /// </summary>
       string NumMAddSR ="90";
        private void numMAddSR_ValueChanged(object sender, EventArgs e)
        {
            string num = ((int)numMAddSR.Value-1).ToString("x2");
            NumMAddSR = "9" + num.Substring(1, 1).ToUpper();
        }
        private void cmbMAddSR_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAddSRPos.Items.Clear();
            cmbAElecMachine.Items.Clear();
            cmbRegentTray.Items.Clear();
            cmbAsArmX.Items.Clear();
            cmbAddsamle.Items.Clear();
            if (cmbMAddSR.SelectedIndex == 0)
            {
                MAddSR = 1;
                foreach (string pos in AddSRPos)
                {
                    cmbAddSRPos.Items.Add(pos);
                    cmbAsArmX.Items.Add(pos);
                    if (!(pos == AddSRPos[AddSRPos.Length - 1] || pos == AddSRPos[AddSRPos.Length - 2] || pos == AddSRPos[AddSRPos.Length - 3]))
                        cmbRegentTray.Items.Add(pos);
                }
                foreach (string aem in AddSRAEM)
                {
                    cmbAElecMachine.Items.Add(aem);

                }
                foreach (string move in AddRMove)
                {
                    cmbAddsamle.Items.Add(move);
                }
                btnRReset.Visible = true;

            }
            else
            {
                MAddSR = 0;
                foreach (string pos in AddSPPos)
                {
                    cmbAddSRPos.Items.Add(pos);
                    cmbAsArmX.Items.Add(pos);
                    //if (!(pos == AddSPPos[AddSPPos.Length - 1] || pos == AddSPPos[AddSPPos.Length - 2]))
                    //    cmbRegentTray.Items.Add(pos);

                }
                foreach (string pos in AddSRPos)
                {
                    if (!(pos == AddSRPos[AddSRPos.Length - 1] || pos == AddSRPos[AddSRPos.Length - 2] || pos == AddSRPos[AddSRPos.Length - 3]))
                        cmbRegentTray.Items.Add(pos);
                }
                foreach (string aem in AddSPAEM)
                {
                    cmbAElecMachine.Items.Add(aem);
                }
                foreach (string move in AddSMove)
                {
                    cmbAddsamle.Items.Add(move);
                }
                btnRReset.Visible = false;
            }
        }
        private void cmbAddSRPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAddSRPos.SelectedItem == null) return;
            if (!NetCom3.totalOrderFlag)
            {
                frmMessageShow frmMsgShow = new frmMessageShow();
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            string pos = cmbAddSRPos.SelectedItem.ToString().Trim();
            if (pos == "")
            {
                frmMessageShow frmMsgShow = new frmMessageShow();
                frmMsgShow.MessageShow("仪器调试", "参数名为空，请稍等！");
                return;
            }
            if (MAddSR == 1)
            {
                if (pos == AddSRPos[0].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 A1 01 06";
                    //strorder = "EB 90 12 A1 01 06";
                }
                else if (pos == AddSRPos[1].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 A1 01 05";
                }
                else if (pos == AddSRPos[2].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 A1 01 04";
                }
                else if (pos == AddSRPos[3].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 A1 01 03";
                }
                else if (pos == AddSRPos[4].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 A1 01 07";
                }
                else if (pos == AddSRPos[5].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 A1 01 00";
                }
                else if (pos == AddSRPos[6].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 A1 01 01";
                }
            }
            else
            {
                if (pos == AddSPPos[0].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 02 A1 01 03";
                }
                else if (pos == AddSPPos[1].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 02 A1 01 02";
                }
                else if (pos == AddSPPos[2].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 02 A1 01 00";
                }
                else if (pos == AddSPPos[3].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 02 A1 01 01";
                }
            }
            cmbAddSRPos.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            cmbAddSRPos.Enabled = true;
        }
        private void btnSRInc_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbAElecMachine.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择需调试的电机！");
                return;
            }
            if (txtAddSRIncValue.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入增量值！");
                return;
            }
            string incream = int.Parse(txtAddSRIncValue.Text.Trim()).ToString("x8");
            string strorder = "";
            if (MAddSR == 1)
            {
                if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSRAEM[0].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 01 10 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSRAEM[1].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 02 10 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSRAEM[2].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 04 10 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }

            }
            else
            {
                if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSPAEM[0].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 02 01 10 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSPAEM[1].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 02 02 10 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
            }
            btnSRInc.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnSRInc.Enabled = true;
        }

        private void cmbRegentTray_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbRegentpos.Items.Clear();
            string pos = cmbRegentTray.Text.Trim();
            if (pos == AddSPPos[0] || pos == AddSPPos[1])
            {
                for (int i = 1; i <= SamplePos; i++)
                    cmbRegentpos.Items.Add(i);
            }
            if (pos == AddSRPos[0] || pos == AddSRPos[1] || pos == AddSRPos[2] || pos == AddSRPos[3] || pos == AddSRPos[4])
            {
                for (int i = 1; i <= RegentNum; i++)
                    cmbRegentpos.Items.Add(i);
            }
        }
        private void btnSRDec_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbAElecMachine.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择需调试的电机！");
                return;
            }
            if (txtAddSRIncValue.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入增量值！");
                return;
            }
            string incream = int.Parse("-" + txtAddSRIncValue.Text.Trim()).ToString("x8");
            string strorder = "";
            if (MAddSR == 1)
            {
                if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSRAEM[0].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 01 10 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSRAEM[1].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 02 10 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSRAEM[2].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 04 10 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }

            }
            else
            {
                if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSPAEM[0].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 02 01 10 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSPAEM[1].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 02 02 10 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                //else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSPAEM[2].Trim())
                //{
                //    strorder = "EB "+ NumMAddSR + " 02 03 10 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                //                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                //}
                //else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSPAEM[3].Trim())
                //{
                //    strorder = "EB "+ NumMAddSR + " 02 03 10 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                //                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                //}
            }
            btnSRDec.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnSRDec.Enabled = true;
        }
        private void btnSRInave_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MAddSR == 1)
            {
                if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSRAEM[0].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 01 13";
                }
                else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSRAEM[1].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 02 13";
                }
                else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSRAEM[2].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 12 04 13";
                }
            }
            else
            {

                if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSPAEM[0].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 02 01 13";
                }
                else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSPAEM[1].Trim())
                {
                    strorder = "EB "+ NumMAddSR + " 02 02 13";
                }
                //else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSPAEM[2].Trim())
                //{
                //    strorder = "EB "+ NumMAddSR + " 12 03 13";
                //}
                //else if (cmbAElecMachine.SelectedItem.ToString().Trim() == AddSPAEM[3].Trim())
                //{
                //    strorder = "EB "+ NumMAddSR + " 12 03 13";
                //}
            }
            btnSRInave.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnSRInave.Enabled = true;
        }
        private void btnSRInaveData_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            btnSRInaveData.Enabled = false;
            List<string> data = new List<string>();
            if (MAddSR == 1)
            {
                data = GetData(NumMAddSR, "12");
            }
            else
            {
                data = GetData(NumMAddSR, "02");
            }
            if (data.Count() < 1)
            {
                MessageBox.Show("数据不存在");
            }
            foreach (string item in data)
            {
                string s = ("EB " + item.Substring(0, 8) + " 13 " + item.Substring(18)).TrimEnd();
                NetCom3.Instance.Send(NetCom3.Cover(s), 5);
                NetCom3.Instance.SingleQuery();
            }
            btnSRInaveData.Enabled = true;
        }
        private void btnSRAllReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MAddSR == 0)
            {
                strorder = "EB "+ NumMAddSR + " 02 00";
            }
            else
            {
                strorder = "EB "+ NumMAddSR + " 12 00";
            }
            btnSRAllReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnSRAllReset.Enabled = true;
        }

        private void btnSRXReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MAddSR == 0)
            {
                strorder = "EB " + NumMAddSR + " 02 01 00";
            }
            else
            {
                strorder = "EB "+ NumMAddSR + " 12 01 00";
            }
            btnSRXReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnSRXReset.Enabled = true;
        }
        private void btnSRZReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MAddSR == 0)
            {
                strorder = "EB " + NumMAddSR + " 02 02 00";
            }
            else
            {
                strorder = "EB "+ NumMAddSR + " 12 02 00";
            }
            btnSRZReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnSRZReset.Enabled = true;
        }

        private void btnRReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "EB "+ NumMAddSR + " 12 04 00";
            btnRReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnRReset.Enabled = true;
        }
        private void btbAsArmZEx_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MAddSR == 0)
            {
                if (cmbAsArmZ.SelectedItem.ToString() == "下降")
                    strorder = "EB "+ NumMAddSR + " 11 02 11 41 00 00";
                else
                    strorder = "EB "+ NumMAddSR + " 11 02 11 40 00 00";
            }
            else
            {
                if (cmbAsArmZ.SelectedItem.ToString() == "下降")
                    strorder = "EB "+ NumMAddSR + " 11 12 11 41 00 00";
                else
                    strorder = "EB "+ NumMAddSR + " 11 12 11 40 00 00";
            }
            btbAsArmZEx.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btbAsArmZEx.Enabled = true;
        }

        private void btbAsArmXEx_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbAsArmX.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择加样针旋转位置！");
                return;
            }
            string strorder = "";

            if (MAddSR == 1)
            {
                if (cmbAsArmX.SelectedItem.ToString() == AddSRPos[0])
                {

                    strorder = "EB "+ NumMAddSR + " 11 12 12 06";
                }
                else if (cmbAsArmX.SelectedItem.ToString() == AddSRPos[1])
                {

                    strorder = "EB "+ NumMAddSR + " 11 12 12 05";
                }
                else if (cmbAsArmX.SelectedItem.ToString() == AddSRPos[2])
                {

                    strorder = "EB "+ NumMAddSR + " 11 12 12 04";
                }
                else if (cmbAsArmX.SelectedItem.ToString() == AddSRPos[3])
                {

                    strorder = "EB "+ NumMAddSR + " 11 12 12 03";
                }
                else if (cmbAsArmX.SelectedItem.ToString() == AddSRPos[4])
                {

                    strorder = "EB "+ NumMAddSR + " 11 12 12 07";
                }
                else if (cmbAsArmX.SelectedItem.ToString() == AddSRPos[5])
                {

                    strorder = "EB "+ NumMAddSR + " 11 12 12 00";
                }
                //else if (cmbAsArmX.SelectedItem.ToString() == AddSRPos[6])
                //{

                //    strorder = "EB "+ NumMAddSR + " 11 12 12 00 0A";
                //}
                else if (cmbAsArmX.SelectedItem.ToString() == AddSRPos[6])
                {

                    strorder = "EB "+ NumMAddSR + " 11 12 12 01";
                }
            }
            else
            {
                if (cmbAsArmX.SelectedItem.ToString() == AddSPPos[0])
                {

                    strorder = "EB "+ NumMAddSR + " 11 02 12 03";
                }
                else if (cmbAsArmX.SelectedItem.ToString() == AddSPPos[1])
                {

                    strorder = "EB "+ NumMAddSR + " 11 02 12 02";
                }
                else if (cmbAsArmX.SelectedItem.ToString() == AddSPPos[2])
                {

                    strorder = "EB "+ NumMAddSR + " 11 02 12 00";
                }
                //else if (cmbAsArmX.SelectedItem.ToString() == AddSPPos[3])
                //{

                //    strorder = "EB "+ NumMAddSR + " 11 12 12 00 0A";
                //}
                else if (cmbAsArmX.SelectedItem.ToString() == AddSPPos[3])
                {

                    strorder = "EB "+ NumMAddSR + " 11 02 12 01";
                }
            }
            btbAsArmXEx.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btbAsArmXEx.Enabled = true;
        }

        private void btnRegentTrayEx_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            //if (MAddSR == 1)
            //{
            if (cmbRegentTray.SelectedItem.ToString() == AddSRPos[0])
            {

                strorder = "EB "+ NumMAddSR + " 11 12 13 06";
            }
            else if (cmbRegentTray.SelectedItem.ToString() == AddSRPos[1])
            {

                strorder = "EB "+ NumMAddSR + " 11 12 13 05";
            }
            else if (cmbRegentTray.SelectedItem.ToString() == AddSRPos[2])
            {

                strorder = "EB "+ NumMAddSR + " 11 12 13 04";
            }
            else if (cmbRegentTray.SelectedItem.ToString() == AddSRPos[3])
            {

                strorder = "EB "+ NumMAddSR + " 11 12 13 03";
            }
            else if (cmbRegentTray.SelectedItem.ToString() == AddSRPos[4])
            {

                strorder = "EB "+ NumMAddSR + " 11 12 13 07";
            }

            //}
            //else
            //{
            //    if (cmbRegentTray.SelectedItem.ToString() == AddSPPos[0])
            //    {

            //        strorder = "EB "+ NumMAddSR + " 11 02  13 05";
            //    }
            //    else if (cmbRegentTray.SelectedItem.ToString() == AddSPPos[1])
            //    {

            //        strorder = "EB "+ NumMAddSR + " 11 12 13 05";
            //    }
            //}
            int BB = 1;
            if (cmbRegentpos.SelectedItem == null)
                cmbRegentpos.SelectedIndex = 0;
            BB = int.Parse(cmbRegentpos.Text.Trim());
            strorder = strorder + " " + BB.ToString("x2") ;
            string CC = "00";

            if (cmbTurnType.SelectedItem == null)
                CC = "00";
            else if (cmbTurnType.SelectedItem.ToString() == "转动后停止扫描")
                CC = "31";
            else if (cmbTurnType.SelectedItem.ToString() == "单次触发")
                CC = "32";
            else if (cmbTurnType.SelectedItem.ToString() == "连续触发")
                CC = "30";
            else
                CC = "00";
            strorder = strorder + " "+CC;
            btnRegentTrayEx.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnRegentTrayEx.Enabled = true;
        }

        private void fbtnAsPumpEx_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (cmbAsPump.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择柱塞泵数值！");
                return;
            }
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            string temp = int.Parse(cmbAsPump.SelectedItem.ToString()).ToString("x4");
            string temp1 = temp.Substring(0, 2);
            string temp2 = temp.Substring(2, 2);
            if (MAddSR == 0)
            {
                strorder = "EB "+ NumMAddSR + " 11 02 14 " + temp1 + " " + temp2 + " 00";
            }
            else
            {
                strorder = "EB "+ NumMAddSR + " 11 12 14 " + temp1 + " " + temp2 + " 00";
            }
            fbtnAsPumpEx.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnAsPumpEx.Enabled = true;
        }
        private void fbtnAddSR_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (cmbAddsamle.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择加样执行动作！");
                return;
            }
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (txtStartHole.Text=="")
            {
                frmMsgShow.MessageShow("仪器调试", "请选择加样取样位置！");
                return;
            }
            if (txtEndHole.Text == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请选择加样目的位置！");
                return;
            }
            fbtnAddSR.Enabled = false;
            int startPos = int.Parse(txtStartHole.Text);
            int endPos = int.Parse(txtEndHole.Text);
            if (MAddSR == 0)
            {
                if (cmbAddsamle.SelectedItem.ToString() == AddSMove[0])
                {
                    AddLiquid(NumMAddSR,AddLiquidStep.AddLiquidCS,startPos, endPos);
                }
                else if (cmbAddsamle.SelectedItem.ToString() == AddSMove[1])
                {
                    AddLiquid(NumMAddSR,AddLiquidStep.AddLiquidES, startPos, endPos);
                }
                else if (cmbAddsamle.SelectedItem.ToString() == AddSMove[2])
                {
                    AddLiquid(NumMAddSR,AddLiquidStep.AddLiquidDS, startPos, endPos);
                }
            }
            else
            {
                if (cmbAddsamle.SelectedItem.ToString() == AddRMove[0])
                {
                    AddLiquid(NumMAddSR,AddLiquidStep.AddLiquidD, startPos, endPos);
                }
                else if (cmbAddsamle.SelectedItem.ToString() == AddRMove[1])
                {
                    AddLiquid(NumMAddSR,AddLiquidStep.AddSingleR1, startPos, endPos);
                }
                else if (cmbAddsamle.SelectedItem.ToString() == AddRMove[2])
                {
                    AddLiquid(NumMAddSR,AddLiquidStep.AddSingleR2, startPos, endPos);
                }
                else if (cmbAddsamle.SelectedItem.ToString() == AddRMove[3])
                {
                    AddLiquid(NumMAddSR,AddLiquidStep.AddSingleR3, startPos, endPos);
                }
                else if (cmbAddsamle.SelectedItem.ToString() == AddRMove[4])
                {
                    AddLiquid(NumMAddSR,AddLiquidStep.AddBeads, startPos, endPos);
                }
            }
            fbtnAddSR.Enabled = true;
        }
        #endregion
        #region 移管手
        /// <summary>
        /// 移管手模块标志 0-移管手(新) 1-移管手
        /// </summary>
        int MMove = 0;
        /// <summary>
        /// 移管手(新)调试位置
        /// 温育盘位置,暂存盘位置,废管位置,"理杯块位置","清洗盘位置"
        /// </summary>
        string[] MoveNewPos = { "温育盘位置", "暂存盘位置", "废管位置", "理杯块位置","清洗盘位置" };
        /// <summary>
        /// 移管手调试位置
        /// 温育盘位置,清洗盘位置,废管位置
        /// </summary>
        string[] MovePos = { "温育盘位置",  "清洗盘位置", "废管位置" };
        /// <summary>
        /// 抓手模块电机列表
        /// 垂直电机，旋转电机，暂存盘电机，理杯块电机,温育盘电机
        /// </summary>
        string[] MoveElecMachine = { "垂直电机", "旋转电机", "暂存盘电机", "理杯块电机", "温育盘电机" };
        /// <summary>
        /// 级联机器编号
        /// </summary>
        string strMMove = "";
        private void numMMove_ValueChanged(object sender, EventArgs e)
        {
            string num = ((int)numMMove.Value - 1).ToString("x2");
            strMMove = "9" + num.Substring(1, 1).ToUpper();
        }
       
        private void cmbMMove_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMovePos.Items.Clear();
            cmbMoveX.Items.Clear();
            cmbMoveElecMachine.Items.Clear();
            if (cmbMMove.SelectedIndex == 0)
            {
                MMove = 0;
                foreach (string pos in MoveNewPos)
                {
                    cmbMovePos.Items.Add(pos);
                    if(pos!= "理杯块位置")
                        cmbMoveX.Items.Add(pos);
                }
            }
            else
            {
                MMove = 1;
                foreach (string pos in MovePos)
                {
                    cmbMovePos.Items.Add(pos);
                    cmbMoveX.Items.Add(pos);
                }
            }
            foreach (string MoveE in MoveElecMachine)
            {
                cmbMoveElecMachine.Items.Add(MoveE);
            }
        }
        private void cmbMovePos_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMove == 0)
            {
                if (cmbMovePos.SelectedItem.ToString() == MoveNewPos[0])
                {
                    strorder = "EB "+ strMMove+ " 01 01 02";
                }
                //else if (cmbMovePos.SelectedItem.ToString() == MoveNewPos[1])
                //{
                //    strorder = "EB "+ strMMove+ " 01 01 02 00";
                //}
                else if (cmbMovePos.SelectedItem.ToString() == MoveNewPos[1])
                {
                    strorder = "EB "+ strMMove+ " 01 01 01";
                }
                else if (cmbMovePos.SelectedItem.ToString() == MoveNewPos[2])
                {
                    strorder = "EB "+ strMMove+ " 01 01 04";
                }
                else if (cmbMovePos.SelectedItem.ToString() == MoveNewPos[3])
                {
                    strorder = "EB "+ strMMove+ " 01 01 05";
                }
                else if (cmbMovePos.SelectedItem.ToString() == MoveNewPos[4])
                {
                    strorder = "EB " + strMMove + " 01 01 06";
                }
            }
            else
            {
                if (cmbMovePos.SelectedItem.ToString() == MovePos[0])
                {
                    strorder = "EB "+ strMMove+ " 21 01 02";
                }
                //else if (cmbMovePos.SelectedItem.ToString() == MovePos[1])
                //{
                //    strorder = "EB "+ strMMove+ " 21 01 02 00";
                //}
                else if (cmbMovePos.SelectedItem.ToString() == MovePos[1])
                {
                    strorder = "EB "+ strMMove+ " 21 01 03";
                }
                else if (cmbMovePos.SelectedItem.ToString() == MovePos[2])
                {
                    strorder = "EB "+ strMMove+ " 21 01 04";
                }
            }
            cmbMovePos.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            cmbMovePos.Enabled = true;
        }
        private void btnMoveInc_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (cmbMovePos.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择校准位置！");
                return;
            }
            if (cmbMoveElecMachine.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择要进行校准的电机！");
                return;
            }
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            string incream = int.Parse(txtMoveIncValue.Text.Trim()).ToString("x8");
            if (MMove == 0)
            {
                if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[0])
                    strorder = "EB "+ strMMove+ " 01 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[1])
                    strorder = "EB "+ strMMove+ " 01 02 04 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[2])
                    strorder = "EB "+ strMMove+ " 01 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[3])
                    strorder = "EB "+ strMMove+ " 01 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if(cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[4])
                    strorder = "EB "+ strMIncubation+ " 14 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);

            }
            else
            {
                if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[0])
                    strorder = "EB "+ strMMove+ " 21 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[1])
                    strorder = "EB "+ strMMove+ " 21 02 04 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[2])
                    strorder = "EB "+ strMMove+ " 01 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[3])
                    strorder = "EB "+ strMMove+ " 01 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[4])
                    strorder = "EB "+ strMIncubation+ " 14 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            btnMoveInc.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnMoveInc.Enabled = true;
        }
        private void btnMoveDec_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (cmbMovePos.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择校准位置！");
                return;
            }
            if (cmbMoveElecMachine.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择要进行校准的电机！");
                return;
            }
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            string incream = int.Parse("-" + txtMoveIncValue.Text.Trim()).ToString("x8");
            if (MMove == 0)
            {
                if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[0])
                    strorder = "EB "+ strMMove+ " 01 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[1])
                    strorder = "EB "+ strMMove+ " 01 02 04 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[2])
                    strorder = "EB "+ strMMove+ " 01 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[3])
                    strorder = "EB "+ strMMove+ " 01 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[4])
                    strorder = "EB "+ strMIncubation+ " 14 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            else
            {
                if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[0])
                    strorder = "EB "+ strMMove+ " 21 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[1])
                    strorder = "EB "+ strMMove+ " 21 02 04 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[2])
                    strorder = "EB "+ strMMove+ " 01 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[3])
                    strorder = "EB "+ strMMove+ " 01 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbMoveElecMachine.SelectedItem.ToString() == MoveElecMachine[4])
                    strorder = "EB "+ strMIncubation+ " 14 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            btnMoveDec.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnMoveDec.Enabled = true;
        }
        private void btnMoveSave_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMove == 0)
            {
                strorder = "EB "+ strMMove+ " 01 13";
            }
            else
            {
                strorder = "EB "+ strMMove+ " 21 13";
            }
            btnMoveSave.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnMoveSave.Enabled = true;
        }
        private void btnMoveSaveData_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            btnMoveSaveData.Enabled = false;
            List<string> data = new List<string>();
            if (MMove == 0)
            {
                data = GetData(strMMove, "01");
            }
            else
            {
                data = GetData(strMMove, "21");
            }
            if (data.Count() < 1)
            {
                MessageBox.Show("数据不存在");
            }
            foreach (string item in data)
            {
                string s = ("EB " + item.Substring(0, 5) + " 13 " + item.Substring(18)).TrimEnd();
                NetCom3.Instance.Send(NetCom3.Cover(s), 5);
                NetCom3.Instance.SingleQuery();
            }
            btnMoveSaveData.Enabled = true;
        }
        private void btnMoveAllReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMove == 0)
            {
                strorder = "EB "+ strMMove+ " 01 03";
            }
            else
            {
                strorder = "EB "+ strMMove+ " 21 03";
            }
            btnMoveAllReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnMoveAllReset.Enabled = true;
        }
        private void btnMoveXReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMove == 0)
            {
                strorder = "EB "+ strMMove+ " 01 03 04";
            }
            else
            {
                strorder = "EB "+ strMMove+ " 21 03 04";
            }
            btnMoveXReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnMoveXReset.Enabled = true;
        }
        private void btnHandOpen_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMove == 0)
            {
                strorder = "EB "+ strMMove+ " 11 01 03 31";
            }
            else
            {
                strorder = "EB "+ strMMove+ " 11 11 03 31";
            }
            btnHandOpen.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnHandOpen.Enabled = true;
        }
        private void btnMoveZReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMove == 0)
            {
                strorder = "EB "+ strMMove+ " 01 03 03";
            }
            else
            {
                strorder = "EB "+ strMMove+ " 21 03 03";
            }
            btnMoveZReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnMoveZReset.Enabled = true;
        }
        private void btnHandClose_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMove == 0)
            {
                strorder = "EB "+ strMMove+ " 11 01 03 30";
            }
            else
            {
                strorder = "EB "+ strMMove+ " 11 11 03 30";
            }
            btnHandClose.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnHandClose.Enabled = true;
        }
        private void btnSDickInit_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            strorder = "EB "+ strMMove+ " 01 03 02";
            btnSDickInit.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnSDickInit.Enabled = true;
        }
        private void btnPutCupInit_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            strorder = "EB "+ strMMove+ " 01 03 01";
            btnPutCupInit.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnPutCupInit.Enabled = true;
        }
        private void fbtnSDiskTurn_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            fbtnSDiskTurn.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover("EB "+ strMMove+ " 11 01 05 01"), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnSDiskTurn.Enabled = true;
        }
        private void btnMoveX_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (cmbMoveX.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择抓手旋转位置！");
                return;
            }
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMove == 0)
            {
                if (cmbMoveX.SelectedItem.ToString() == MoveNewPos[0])
                {
                    strorder = "EB "+ strMMove+ " 11 01 01 02 00 00";
                }
                else if (cmbMoveX.SelectedItem.ToString() == MoveNewPos[1])
                {
                    strorder = "EB "+ strMMove+ " 11 01 01 01";
                }
                else if (cmbMoveX.SelectedItem.ToString() == MoveNewPos[2])
                {
                    strorder = "EB "+ strMMove+ " 11 01 01 04";
                }
                else if (cmbMoveX.SelectedItem.ToString() == MoveNewPos[4])
                {
                    strorder = "EB " + strMMove + " 11 01 01 03";
                }
            }
            else
            {
                if (cmbMoveX.SelectedItem.ToString() == MovePos[0])
                {
                    strorder = "EB "+ strMMove+ " 11 11 01 02 00 00";
                }
                else if (cmbMoveX.SelectedItem.ToString() == MovePos[1])
                {
                    strorder = "EB "+ strMMove+ " 11 11 01 03";
                }
                else if (cmbMoveX.SelectedItem.ToString() == MovePos[2])
                {
                    strorder = "EB "+ strMMove+ " 11 11 01 04";
                }
            }
            btnMoveX.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnMoveX.Enabled = true;
        }
        private void btnMoveY_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (cmbMoveY.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择抓手垂直位置！");
                return;
            }
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMove == 0)
            {
                if (cmbMoveY.SelectedItem.ToString() == "升到光电开关位置")
                {
                    strorder = "EB "+ strMMove+ " 11 01 02 30";
                }
                else if (cmbMoveY.SelectedItem.ToString() == "在当前位置下降")
                {
                    strorder = "EB "+ strMMove+ " 11 01 02 31";
                }
            }
            else
            {
                if (cmbMoveY.SelectedItem.ToString() == "升到光电开关位置")
                {
                    strorder = "EB "+ strMMove+ " 11 11 02 30";
                }
                else if (cmbMoveY.SelectedItem.ToString() == "在当前位置下降")
                {
                    strorder = "EB "+ strMMove+ " 11 11 02 31";
                }
            }
            btnMoveY.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnMoveY.Enabled = true;
        }
        private void btnPutCup_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbCupMake.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择理杯块运行动作");
                return;
            }
            btnPutCup.Enabled = false;
            if (cmbCupMake.SelectedItem.ToString() == "理杯块运行到最低点")
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+ strMMove+ " 11 01 04 30 00 00"), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            else if (cmbCupMake.SelectedItem.ToString() == "理杯块运行到最高点")
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+ strMMove+ " 11 01 04 31 00 00"), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            btnPutCup.Enabled = true;
        }
        #endregion
        #region 温育盘
        /// <summary>
        /// 温育盘调试标识  1-外 0-内
        /// </summary>
        int MMIncubation = 0;
        /// <summary>
        /// 温育盘位置
        /// 压杯开始位置(Z轴在夹管最低位)，压杯最低位置
        /// </summary>
        string[] IncubationPos = { "压杯开始位置，Z轴在夹管最低位", "压杯最低位置" };
        /// <summary>
        /// 温育盘电机  
        /// 垂直电机，压杯电机
        /// </summary>
        string[] IncubaElecMachine = { "垂直电机", "压杯电机" };

        string strMIncubation = "90";
        private void numMIncubation_ValueChanged(object sender, EventArgs e)
        {
            string num = ((int)numMIncubation.Value - 1).ToString("x2");
            strMIncubation = "9" + num.Substring(1, 1).ToUpper();
        }
        private void cmbMIncubation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMIncubation.SelectedItem.ToString() == "温育盘(内)")
                MMIncubation = 0;
            else
                MMIncubation = 1;
        }
        private void CmbIpara_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (CmbIpara.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择参数信息！");
                return;
            }
            string strorder = "";
            if (MMIncubation == 0)
            {
                if (CmbIpara.SelectedItem.ToString().Contains(IncubationPos[0]))
                {
                    strorder = "EB "+ strMIncubation+ " 04 01 01";
                }
                else if (CmbIpara.SelectedItem.ToString().Contains(IncubationPos[1]))
                {
                    strorder = "EB "+ strMIncubation+ " 04 01 02";
                }
                else if (CmbIpara.SelectedItem.ToString().Contains(IncubationPos[2]))
                {
                    strorder = "EB "+ strMIncubation+ " 04 01 03";
                }
            }
            else
            {
                if (CmbIpara.SelectedItem.ToString().Contains(IncubationPos[0]))
                {
                    strorder = "EB "+ strMIncubation+ " 14 01 01";
                }
                else if (CmbIpara.SelectedItem.ToString().Contains(IncubationPos[1]))
                {
                    strorder = "EB "+ strMIncubation+ " 14 01 02";
                }
                else if (CmbIpara.SelectedItem.ToString().Contains(IncubationPos[2]))
                {
                    strorder = "EB "+ strMIncubation+ " 14 01 03";
                }
            }
            CmbIpara.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            CmbIpara.Enabled = true;
        }
        private void btnIAdd_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (txtIIncrem.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入电机增量！！");
                return;
            }
            string strorder = "";
            string incream = int.Parse(txtIIncrem.Text.Trim()).ToString("x8");
            if (MMIncubation == 0)
            {
                if (cmbIElecMachine.SelectedItem.ToString() == IncubaElecMachine[0])
                    strorder = "EB "+ strMIncubation+ " 04 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbIElecMachine.SelectedItem.ToString() == IncubaElecMachine[1])
                    strorder = "EB "+ strMIncubation+ " 04 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            else
            {
                if (cmbIElecMachine.SelectedItem.ToString() == IncubaElecMachine[0])
                    strorder = "EB "+ strMIncubation+ " 14 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbIElecMachine.SelectedItem.ToString() == IncubaElecMachine[1])
                    strorder = "EB "+ strMIncubation+ " 14 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            btnIAdd.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnIAdd.Enabled = true;
        }
        private void btnISub_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (txtIIncrem.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入电机增量！！");
                return;
            }
            string strorder = "";
            string incream = int.Parse("-" + txtIIncrem.Text.Trim()).ToString("x8");
            if (MMIncubation == 0)
            {
              
                if (cmbIElecMachine.SelectedItem.ToString() == IncubaElecMachine[0])
                    strorder = "EB "+ strMIncubation+ " 04 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2) ;
                else if (cmbIElecMachine.SelectedItem.ToString() == IncubaElecMachine[1])
                    strorder = "EB "+ strMIncubation+ " 04 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            else
            {
             
                if (cmbIElecMachine.SelectedItem.ToString() == IncubaElecMachine[0])
                    strorder = "EB "+ strMIncubation+ " 14 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                else if (cmbIElecMachine.SelectedItem.ToString() == IncubaElecMachine[1])
                    strorder = "EB "+ strMIncubation+ " 14 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                              + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            btnISub.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnISub.Enabled = true;
        }
        private void btnISave_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMIncubation == 0)
            {
                strorder = "EB "+ strMIncubation+ " 04 13";
            }
            else
            {
                strorder = "EB "+ strMIncubation+ " 14 13";
            }
            btnISave.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnISave.Enabled = true;
        }
        private void btnISaveData_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            btnISaveData.Enabled = false;
            List<string> data = new List<string>();
            if (MMIncubation == 0)
            {
                data = GetData(strMIncubation, "04");
            }
            else
            {
                data = GetData(strMIncubation, "14");
            }
            if (data.Count() < 1)
            {
                MessageBox.Show("数据不存在");
            }
            foreach (string item in data)
            {
                string s = ("EB " + item.Substring(0, 5) + " 13 " + item.Substring(18)).TrimEnd();
                NetCom3.Instance.Send(NetCom3.Cover(s), 5);
                NetCom3.Instance.SingleQuery();
            }
            btnISaveData.Enabled = true;
        }

        private void btnIAllInit_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            strorder = "EB "+ strMIncubation+ " 14 03 00";
            btnIAllInit.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnIAllInit.Enabled = true;
        }

        private void fbtnInTubeReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            strorder = "EB "+ strMIncubation+ " 14 03 01";
            fbtnInTubeReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnInTubeReset.Enabled = true;
        }

        private void btnIYInit_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMIncubation == 0)
            {
                strorder = "EB "+ strMIncubation+ " 04 03 02";
            }
            else
            {
                strorder = "EB "+ strMIncubation+ " 14 03 02";
            }
            btnIYInit.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnIYInit.Enabled = true;
        }

        private void fbtnPressCupZero_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMIncubation == 0)
            {
                strorder = "EB "+ strMIncubation+ " 04 03 03";
            }
            else
            {
                strorder = "EB "+ strMIncubation+ " 14 03 03";
            }
            fbtnPressCupZero.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnPressCupZero.Enabled = true;
        }
        Thread ThNewMove = null;
        Thread ThMove = null;
        private void fbtnInTubeClear_Click(object sender, EventArgs e)
        {
            fbtnInTubeClear.Enabled = false;
            fbtnInTubeCS.Enabled = true;
            if (cmbChooseMove.SelectedItem == null)
                cmbChooseMove.SelectedIndex = 0;
            if (cmbChooseMove.SelectedItem.ToString().Trim() == "移管手")
                reactTrayTubeClear(strMIncubation,1);
            else if (cmbChooseMove.SelectedItem.ToString().Trim() == "移新管抓手")
                reactTrayTubeClear(strMIncubation,0);
            else
            {
                bRRectClear = true;
                ThNewMove = new Thread(new ParameterizedThreadStart(reactNewTrayClear));
                //ThNewMove = new Thread(reactNewTrayClear);
                ThNewMove.IsBackground = true;
                ThNewMove.Start(strMIncubation);
                ThMove = new Thread(new ParameterizedThreadStart(reactNewTrayClear));
                ThMove.IsBackground = true;
                ThMove.Start(strMIncubation);
                while (tubeCount < ReactTrayNum && bRRectClear)
                {
                    NetCom3.Delay(1000);
                }
            }
            fbtnInTubeCS.Enabled = false;
            fbtnInTubeClear.Enabled = true;
        }
        private void fbtnInTubeCS_Click(object sender, EventArgs e)
        {
            fbtnInTubeCS.Enabled = false;
            bRRectClear = false;
            if (ThMove != null)
                ThMove.Abort();
            if (ThNewMove != null)
                ThNewMove.Abort();
            fbtnInTubeClear.Enabled = true;
        }
        /// <summary>
        /// 反应个数
        /// </summary>
        int tubeCount=0;
        /// <summary>
        /// 是否停止清空温育盘
        /// </summary>
        bool bRRectClear = false;
        /// <summary>
        /// 温育反应盘清管
        /// strModel 级联机器编号
        /// Movetate 抓手标志 0-移新管抓手 1-移管手
        /// </summary>
        bool reactTrayTubeClear(string strModel,int Movetate)
        {
            bRRectClear = true;
            LogFile.Instance.Write("运行调试界面温育盘清空代码！");
            DataTable dtInTrayIni = ReadReact();
            for (int i = 0; i < dtInTrayIni.Rows.Count; i++)
            {
                if (!bRRectClear)
                    return false;
                if (Movetate == 0)
                {
                    int statpos= int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                    int errorflag = Move(strModel,MoveSate.ReactLoss, Movetate, statpos);
                    if (errorflag != (int)ErrorState.IsNull && errorflag != (int)ErrorState.Success)
                    {
                        fbtnInTubeClear.Enabled = true;
                        return false;
                    }
                    //修改反应盘信息
                }
                else if(Movetate == 1)
                {
                    int statpos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                    int errorflag = Move(strModel,MoveSate.ReactLoss, Movetate, statpos);
                    if (errorflag != (int)ErrorState.IsNull && errorflag != (int)ErrorState.Success)
                    {
                        fbtnInTubeClear.Enabled = true;
                        return false;
                    }
                }
                int pos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                WriteReact(pos,0);
            }
            return true;
        }
        /// <summary>
        /// 移新管抓手清空温育盘
        /// </summary>
        /// <param name="strModel">级联机器编号</param>
        private void reactNewTrayClear(object strModel)
        {
            DataTable dtInTrayIni = ReadReact();
            for (int j = 0; j < ReactTrayNum / 40 + 1; j = j + 2)
            {
                for (int i = j * 40; i < (j + 1) * 40; i++)
                {
                    if (i >= 150) return;
                    if (!bRRectClear) return;
                    int statpos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                    int errorflag = Move(strModel.ToString(),MoveSate.ReactLoss, 0, statpos);
                    if (errorflag != (int)ErrorState.IsNull && errorflag != (int)ErrorState.Success)
                    {
                        bRRectClear = false;
                        return;
                    }
                    int pos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                    WriteReact(pos, 0);
                    tubeCount++;
                }
            }
        }
        /// <summary>
        /// 移管手清空温育盘
        /// </summary>
        /// <param name="strModel"></param>
        private void reactTrayClear(string strModel)
        {
            DataTable dtInTrayIni = ReadReact();
            for (int j = 1; j < (ReactTrayNum / 40) + 1; j = j + 2)
            {
                for (int i = j * 40; i < (j + 1) * 40; i++)
                {
                    if (i >= 150) return;
                    if (!bRRectClear) return;
                    int statpos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                    int errorflag = Move(strModel,MoveSate.ReactLoss, 1, statpos);
                    if (errorflag != (int)ErrorState.IsNull && errorflag != (int)ErrorState.Success)
                    {
                        bRRectClear = false;
                        return;
                    }
                    int pos = int.Parse(dtInTrayIni.Rows[i][0].ToString().Substring(2));
                    WriteReact(pos, 0);
                    tubeCount++;
                }
            }
        }
        private void fbtnMixArm_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMIncubation == 0)
            {
                if (cmbMixArm.SelectedItem.ToString() == "上（光电位置）")
                    strorder = "EB "+ strMIncubation+ " 11 0a 02 30";
                else
                    strorder = "EB "+ strMIncubation+ " 11 0a 02 31";
            }
            else
            {
                if (cmbMixArm.SelectedItem.ToString() == "上（光电位置）")
                    strorder = "EB "+ strMIncubation+ " 11 1a 02 30";
                else
                    strorder = "EB "+ strMIncubation+ " 11 1a 02 31";
            }
            fbtnMixArm.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnMixArm.Enabled = true;
        }


        private void fbtnMixTurnNum_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            int SendType = -1;
            int HoleNum = int.Parse(txtInHoleNum.Text.Trim());
            if (HoleNum %2  == 0)
            {
                strorder = "EB " + strMIncubation + " 31 04 01 " + HoleNum.ToString("x2");
                SendType = (int)OrderSendType.Mix;
            }
            else
            {
                strorder = "EB " + strMIncubation + " 31 14 01 " + HoleNum.ToString("x2");
                SendType = (int)OrderSendType.Mix2;
            }
            fbtnMixTurnNum.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), SendType);
            NetCom3.Instance.SP2Query();
            fbtnMixTurnNum.Enabled = true;
        }

        private void btnPressCup_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMIncubation == 0)
            {
                if (cmbPressCup.SelectedItem.ToString() == "归零")
                    strorder = "EB "+ strMIncubation+ " 11 0a 03 30";
                else
                    strorder = "EB "+ strMIncubation+ " 11 0a 03 31";
            }
            else
            {
                if (cmbPressCup.SelectedItem.ToString() == "归零")
                    strorder = "EB "+ strMIncubation+ " 11 1a 03 30";
                else
                    strorder = "EB "+ strMIncubation+ " 11 1a 03 31";
            }
            btnPressCup.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnPressCup.Enabled = true;
        }

        private void fbtnMixStart_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MMIncubation == 0)
            {
                strorder = "EB "+ strMIncubation+ " 11 0a 04 30";
                //strorder = "EB 90 11 0a 04 31";混匀停止
            }
            else
            {
                strorder = "EB "+ strMIncubation+ " 11 1a 04 30";
            }
            fbtnMixStart.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnMixStart.Enabled = true;
        }
        #endregion
        #region 清洗盘
        /// <summary>
        /// 清洗盘级联编号
        /// </summary>
        string strMWash = "90";
        private void numMWash_ValueChanged(object sender, EventArgs e)
        {
            string num = ((int)numMIncubation.Value - 1).ToString("x2");
            strMWash = "9" + num.Substring(1, 1).ToUpper();
        }
        private void cmbWashPara_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            //压杯开始位置
            if (cmbWashPara.SelectedItem.ToString().Contains("压杯开始位置"))
            {
                strorder = "EB "+ strMWash+ " 03 01 01";
            }
            //压杯最低位置
            else if(cmbWashPara.SelectedItem.ToString().Contains("压杯最低位置"))
            {
                strorder = "EB "+ strMWash+ " 03 01 02";
            }
            //清洗盘初始位置
            else if (cmbWashPara.SelectedItem.ToString().Contains("清洗盘初始位置"))
            {
                strorder = "EB "+ strMWash+ " 03 01 03";
            }
            cmbWashPara.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            cmbWashPara.Enabled = true;
        }
        private void fbtnWashAdd_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbWashElecMachine.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择需调试的电机！");
                return;
            }
            if (txtWashIncream.Text == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入增量值！");
                txtWashIncream.Focus();
                return;
            }
            string strorder = "";
            string incream = int.Parse(txtWashIncream.Text.Trim()).ToString("x8");
            if (cmbWashElecMachine.SelectedItem.ToString().Contains("Z轴电机"))
            {
                strorder = "EB "+ strMWash+ " 03 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                    + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            else if (cmbWashElecMachine.SelectedItem.ToString().Contains("清洗盘"))
            {
                strorder = "EB "+ strMWash+ " 03 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                    + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            else if (cmbWashElecMachine.SelectedItem.ToString().Contains("压杯电机"))
            {
                strorder = "EB "+ strMWash+ " 03 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                    + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            fbtnWashAdd.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnWashAdd.Enabled = true;
        }

        private void fbtnWashSub_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbWashElecMachine.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择需调试的电机！");
                return;
            }
            if (txtWashIncream.Text == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入增量值！");
                txtWashIncream.Focus();
                return;
            }
            string strorder = "";
            string incream = int.Parse("-" + txtWashIncream.Text.Trim()).ToString("x8");
            if (cmbWashElecMachine.SelectedItem.ToString().Contains("Z轴电机"))
            {
                strorder = "EB "+ strMWash+ " 03 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                    + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            else if (cmbWashElecMachine.SelectedItem.ToString().Contains("清洗盘"))
            {
                strorder = "EB "+ strMWash+ " 03 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                    + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            else if (cmbWashElecMachine.SelectedItem.ToString().Contains("压杯电机"))
            {
                strorder = "EB "+ strMWash+ " 03 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                    + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
            }
            fbtnWashSub.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnWashSub.Enabled = true;
        }

        private void fbtnWashSave_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "EB "+ strMWash+ " 03 03 00";
            fbtnWashSave.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnWashSave.Enabled = true;
        }
        private void fbtnWashSaveData_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            fbtnWashSaveData.Enabled = false;
            List<string> data = new List<string>();
            if (MMIncubation == 0)
            {
                data = GetData(strMWash, "04");
            }
            else
            {
                data = GetData(strMWash, "14");
            }
            if (data.Count() < 1)
            {
                MessageBox.Show("数据不存在");
            }
            foreach (string item in data)
            {
                string s = ("EB " + item.Substring(0, 5) + " 13 " + item.Substring(18)).TrimEnd();
                NetCom3.Instance.Send(NetCom3.Cover(s), 5);
                NetCom3.Instance.SingleQuery();
            }
            fbtnWashSaveData.Enabled = true;
        }

        private void fbtnWashReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "EB "+ strMWash+ " 03 00 00";
            fbtnWashReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            currentHoleNum = 1;
            fbtnWashReset.Enabled = true;
        }

        private void fbtnWashTrayReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "EB "+ strMWash+ " 03 00 02";
            fbtnWashTrayReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnWashTrayReset.Enabled = true;
        }

        private void fbtnWashZReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "EB "+ strMWash+ " 03 00 01";
            fbtnWashZReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnWashZReset.Enabled = true;
        }

        private void fbtnWashPressCupReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "EB "+ strMWash+ " 03 00 03";
            fbtnWashPressCupReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnWashPressCupReset.Enabled = true;
        }
       /// <summary>
       /// 清洗盘清洗运行标志
       /// </summary>
        bool bRunWClear = false;
        private void fbtnWashTubeClear_Click(object sender, EventArgs e)
        {
            fbtnWashTubeClear.Enabled = false;
            fbtnWashTubeCS.Enabled = true;
            washTrayTubeClear(strMWash);
            fbtnWashTubeClear.Enabled = true;
            fbtnWashTubeCS.Enabled = false;
        }
        private void fbtnWashTubeCS_Click(object sender, EventArgs e)
        {
            fbtnWashTubeCS.Enabled = false;
            bRunWClear = false;
            fbtnWashTubeClear.Enabled = true;
        }
        /// <summary>
        /// 清洗盘清管
        /// </summary>
        /// <param name="strModel">级联机器编号</param>
        /// <returns></returns>
        bool washTrayTubeClear(string strModel)
        {
            DataTable dtWashTrayIni = ReadWash();
            //2018-08-20 zlx mod
            int errorflag = 0;
            bRunWClear = true;
            for (int i = 0; i < dtWashTrayIni.Rows.Count; i++)
            {
                if (!bRunWClear)
                    return false;
                if (i != 0)
                {
                    //清洗盘顺时针旋转一位
                    errorflag = WashTurn(strModel ,- 1);
                    if (errorflag!=(int)ErrorState.Success)
                    {
                        fbtnWashTubeClear.Enabled = true;
                        return false;
                    }
                    currentHoleNum = currentHoleNum - 1;
                    //如果孔号小于等于0
                    if (currentHoleNum <= 0)
                    {
                        currentHoleNum = currentHoleNum + WashTrayNum;
                    }
                    OperateIniFile.WriteIniPara("OtherPara", "washCurrentHoleNum", currentHoleNum.ToString());
                    dtWashTrayIni =ReadWash();
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
                LogFile.Instance.Write("==============  " + currentHoleNum + "  扔管");
                errorflag = Move(strModel,MoveSate.WashLoss,1,(int)WashLossPos.PutTubePos);
                if (errorflag != (int)ErrorState.IsNull && errorflag != (int)ErrorState.Success)
                {
                    fbtnWashTubeClear.Enabled = true;
                    return false;
                }
                WriteWash(1,0);
                #endregion
            }
            return true;
        }

        private void fbtnHoleTarget_Click(object sender, EventArgs e)
        {
            fbtnHoleTarget.Enabled = false;
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!(NetCom3.totalOrderFlag || NetCom3.WashReciveFlag))
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (txtHoleTarget.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入目标孔位的位置！");
                txtHoleTarget.Focus();
                return;
            }
            int holetarget = int.Parse(txtHoleTarget.Text.Trim());
            string holetargethex = holetarget.ToString("x2");
            if (holetarget == 0 || holetarget == WashTrayNum)
            {
                frmMsgShow.MessageShow("仪器调试", "当前孔位没有变化！");
                txtHoleTarget.Focus();
                fbtnHoleTarget.Enabled = true;
            }

            else if (holetarget > 0 && holetarget < WashTrayNum)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 31 03 02" + " " + holetargethex.Substring(0, 2)), 2);
                NetCom3.Instance.WashQuery();
                fbtnHoleTarget.Enabled = true;
            }
            else if (holetarget > WashTrayNum)
            {
                frmMsgShow.MessageShow("仪器调试", "孔位支持移动的范围为0~"+ WashTrayNum + "！");
                txtHoleTarget.Focus();
            }
            fbtnHoleTarget.Enabled = true;
        }


        private void btnWashTurn_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            string holetargethex = "";
            btnWashTurn.Enabled = false;
            if (!(NetCom3.totalOrderFlag || NetCom3.WashReciveFlag))
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (txtWashTurn.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入转动个数！");
                txtWashTurn.Focus();
                return;
            }
            try
            {
                int holetarget = int.Parse(txtWashTurn.Text.Trim());
                if (holetarget > 0)
                {
                    holetargethex = holetarget.ToString("x2");
                }
                else if (holetarget < 0)
                {
                    holetargethex = holetarget.ToString("X2").Substring(6, 2);
                }
                if (holetarget == 0 || holetarget == WashTrayNum || holetarget == -WashTrayNum)
                {
                    frmMsgShow.MessageShow("仪器调试", "当前孔位没有变化！");
                    txtWashTurn.Focus();
                    btnWashTurn.Enabled = true;
                }
                else if (holetarget > -WashTrayNum && holetarget < WashTrayNum)
                {
                    WashTurn(strMWash,holetarget);
                    //NetCom3.Instance.Send(NetCom3.Cover("EB 90 31 03 01" + " " + holetargethex.Substring(0, 2)), 2);
                    //NetCom3.Instance.WashQuery();
                    btnWashTurn.Enabled = true;
                }
                else if (holetarget > WashTrayNum || holetarget < -WashTrayNum)
                {
                    frmMsgShow.MessageShow("仪器调试", "孔位支持移动的范围为-"+ WashTrayNum + "~"+ WashTrayNum + "！");
                    txtWashTurn.Focus();
                }
                btnWashTurn.Enabled = true;
            }
            catch (Exception exp)
            {
                MessageBox.Show("请输入数字！");
            }
            finally
            {
                btnWashTurn.Enabled = true;
            }
        }
        Thread Loopthread;
        bool bLoopRun = false;
   
        private void btnLoopTurn_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            btnLoopTurn.Enabled = false;
            if (!(NetCom3.totalOrderFlag || NetCom3.WashReciveFlag))
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (txtWashTurn.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入转动个数！");
                txtWashTurn.Focus();
                return;
            }
            if (bLoopRun)
            {
                bLoopRun = false;
                btnLoopTurn.Enabled = true;
                btnLoopTurn.Text = "循环执行";
            }
            else
            {
                string holetargethex = "";
                int holetarget = int.Parse(txtWashTurn.Text.Trim());
                if (holetarget > 0)
                {
                    holetargethex = holetarget.ToString("x2");
                }
                else if (holetarget < 0)
                {
                    holetargethex = holetarget.ToString("X2").Substring(6, 2);
                }
                if (holetarget == 0 || holetarget == WashTrayNum || holetarget == -WashTrayNum)
                {
                    frmMsgShow.MessageShow("仪器调试", "当前孔位没有变化！");
                    txtWashTurn.Focus();
                    btnLoopTurn.Enabled = true;
                }
                else if (holetarget > WashTrayNum || holetarget < -WashTrayNum)
                {
                    frmMsgShow.MessageShow("仪器调试", "孔位支持移动的范围为-"+ WashTrayNum + "~"+ WashTrayNum + "！");
                    txtWashTurn.Focus();
                    btnLoopTurn.Enabled = true;
                }
                else if (holetarget > -WashTrayNum && holetarget < WashTrayNum)
                {
                    bLoopRun = true;
                    Loopthread = new Thread(new ThreadStart(TestLoopRun));
                    Loopthread.IsBackground = true;
                    Loopthread.Start();
                    btnLoopTurn.Enabled = true;
                    btnLoopTurn.Text = "停止循环";
                }
            }
        }
        void TestLoopRun()
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            int holetarget = int.Parse(txtWashTurn.Text.Trim());
            while (bLoopRun)
            {
                WashTurn(strMWash, holetarget);
                Thread.Sleep(1000);
            }

        }
        private void fbtnWashZEx_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbWashZ.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择Z轴动作！");
            }
            fbtnWashZEx.Enabled = false;
            if (cmbWashZ.SelectedItem.ToString().Contains("吸液夹管开始位置"))
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+ strMWash+ " 11 03 31 30"), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            else if (cmbWashZ.SelectedItem.ToString().Contains("吸液夹管最低位"))
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+ strMWash+ " 11 03 31 31"), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            fbtnWashZEx.Enabled = true;
        }

        private void fbtnWashPressCupEx_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbWashPressCup.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择压杯电机动作！");
            }
            fbtnWashPressCupEx.Enabled = false;
            if (cmbWashPressCup.SelectedItem.ToString().Contains("压杯开始位置"))
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+ strMWash+ " 11 03 32 30"), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            else if (cmbWashPressCup.SelectedItem.ToString().Contains("压杯最低位置"))
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+ strMWash+ " 11 03 32 31"), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            fbtnWashPressCupEx.Enabled = true;
        }

        private void btnWashMix_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbWashMix.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择压杯电机动作！");
            }
            btnWashMix.Enabled = false;
            if (cmbWashMix.SelectedIndex == 0)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+ strMWash+ " 11 03 33 01"), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            else if (cmbWashMix.SelectedIndex == 1)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+ strMWash+ " 11 03 33 02"), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            btnWashMix.Enabled = true;
        }

        private void fbtnPeristalticPEx_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            int InPowerLoquid = int.Parse(numPeristalticPVol.Value.ToString());
            fbtnPeristalticPEx.Enabled = false;
            string temp = InPowerLoquid.ToString("x4");
            string temp1 = temp.Substring(0, 2);
            string temp2 = temp.Substring(2, 2);
            NetCom3.Instance.Send(NetCom3.Cover("EB "+ strMWash+ " 11 03 14 " + temp1 + " " + temp2 + " 00"), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            fbtnPeristalticPEx.Enabled = true;
        }

        #endregion

        #region 光子板/扫码器/烧录
        #region 变量
        int selectZhenID = -1;
        int[] receiveFlag = new int[6];
        string iapFilePath = ""; //烧录文件路径
        Thread thFire = null;
        bool bRead = false;
        #endregion
        string strNumMRead = "90";
        private void numMRead_ValueChanged(object sender, EventArgs e)
        {
            string num = ((int)numMIncubation.Value - 1).ToString("x2");
            strNumMRead = "9" + num.Substring(1, 1).ToUpper();
        }
        private void fbtnRead_Click(object sender, EventArgs e)
        {
            fbtnRead.Enabled = false;
            fbtnReadEnd.Enabled = true;
            int Num = int.Parse(numTubeNum.Value.ToString());
            int TubeNum = int.Parse(numTubeNum.Value.ToString());
            int washturnnum = ReadPos - TubeNum;
            txtReadShow.Text = "";
            //读数数据表
            if (chkWashTubeClear.Checked ==true)
            {
                txtReadShow.AppendText("开始清空清洗盘" + Environment.NewLine);
                bool bclear = washTrayTubeClear(strNumMRead);
                if (!bclear)
                {
                    string message = "";
                    if (NetCom3.Instance.MoverrorFlag == (int)ErrorState.IsKnocked)
                    {
                        message = "在清洗盘位置撞管";
                    }
                    txtReadShow.AppendText("清空清洗盘异常，异常原因:" + message + Environment.NewLine);
                    fbtnRead.Enabled = true;
                    fbtnReadEnd.Enabled = false;
                    return;
                }
                txtReadShow.AppendText("清洗盘清空结束" + Environment.NewLine);
            }
            if (chkInTubeClear.Checked == true)
            {
                txtReadShow.AppendText("开始清空温育盘" + Environment.NewLine);
                bool bclear = reactTrayTubeClear(strNumMRead,0);
                if (!bclear)
                {
                    string message = "";
                    if (NetCom3.Instance.MoverrorFlag == (int)ErrorState.IsKnocked)
                    {
                        message = "在温育盘位置撞管";
                    }
                    txtReadShow.AppendText("清空温育盘异常，异常原因:" + message + Environment.NewLine);
                    fbtnRead.Enabled = true;
                    fbtnReadEnd.Enabled = false;
                    return;
                }
                txtReadShow.AppendText("温育盘清空结束" + Environment.NewLine);
            }
            bRead = true;
            if (chkAddTube.Checked == true)
            {
                txtReadShow.AppendText("从暂存盘向温育盘开始放管" + Environment.NewLine);
                for (int i = 1; i <= TubeNum; i++)
                {
                    if (!bRead)
                        return;
                    string message = "";
                    int Tube = 0;
                    Again:
                    int moverrorflag = Move(strNumMRead,MoveSate.NewtubeToReact, 0, i);
                    if (moverrorflag != (int)ErrorState.Success)
                    {
                        if (moverrorflag == (int)ErrorState.LackTube)
                        {
                            message = "理杯机缺管";
                        }
                        else if (moverrorflag == (int)ErrorState.IsNull)
                        {
                            Tube = Tube + 1;
                            if (Tube < 2)
                                goto Again;
                            else
                                message = "抓手在暂存盘位置取管连续抓空";
                        }
                        else if (moverrorflag == (int)ErrorState.IsKnocked)
                        {
                            message = "抓手在暂存盘位置取管发生撞管";
                        }
                        else if (moverrorflag == (int)ErrorState.putKnocked)
                        {
                            message = "抓手在温育盘位置放管发生撞管";
                        }
                        txtReadShow.AppendText("抓手从暂存盘向温育盘抓管异常,异常原因:" + message + Environment.NewLine);
                        fbtnRead.Enabled = true;
                        fbtnReadEnd.Enabled = false;
                        return;
                    }
                }
                txtReadShow.AppendText("从暂存盘向温育盘开始放管结束" + Environment.NewLine);
                txtReadShow.AppendText("从温育盘向清洗盘取管开始" + Environment.NewLine);
                for (int i = 1; i <= TubeNum; i++)
                {
                    if (!bRead)
                        return;
                    string message = "";
                    int Tube = 0;
                    Again:
                    int moverror = Move(strNumMRead,MoveSate.ReactToWash, 1, i);
                    if (moverror != (int)ErrorState.Success)
                    {
                        if (moverror == (int)ErrorState.IsNull)
                        {
                            Tube = Tube + 1;
                            if (Tube < 2)
                                goto Again;
                            else
                                message = "抓手在暂存盘位置取管连续抓空";
                        }
                        else if (moverror == (int)ErrorState.IsKnocked)
                        {
                            message = "抓手在暂存盘位置取管发生撞管";
                        }
                        else if (moverror == (int)ErrorState.putKnocked)
                        {
                            message = "抓手在温育盘位置放管发生撞管";
                        }
                        txtReadShow.AppendText("抓手从温育盘向清洗盘抓管异常,异常原因:" + message + Environment.NewLine);
                        fbtnRead.Enabled = true;
                        fbtnReadEnd.Enabled = false;
                        return;
                    }
                    int errror = WashTurn(strNumMRead,1);
                    if (errror != (int)ErrorState.Success)
                    {
                        txtReadShow.AppendText("清洗盘旋转指令异常:" + Environment.NewLine);
                        fbtnRead.Enabled = true;
                        fbtnReadEnd.Enabled = false;
                        return;
                    }
                    Thread.Sleep(500);
                }
                txtReadShow.AppendText("清洗盘旋转"+ washturnnum + "个孔位" + Environment.NewLine);
                int washerrror = WashTurn(strNumMRead,washturnnum);
                if (washerrror != (int)ErrorState.Success)
                {
                    txtReadShow.AppendText("清洗盘旋转指令异常:" + Environment.NewLine);
                    fbtnRead.Enabled = true;
                    fbtnReadEnd.Enabled = false;
                    return;
                }
            }
            for (int i = 1; i <= TubeNum; i++)
            {
                if (!bRead)
                    return;
                txtReadShow.AppendText("第"+i+"个孔位开始读数" + Environment.NewLine);
                int readcount = int.Parse(NumReadNum.Value.ToString());
                for(int ii=0;ii< readcount;ii++)
                {
                    WashSend.Initial();
                    WashSend.ReadFlag = 1;
                    int errror = WashAddLiquidR(strNumMRead);
                    if (errror != (int)ErrorState.Success)
                    {
                        txtReadShow.AppendText("清洗灌注指令异常:" + Environment.NewLine);
                        fbtnRead.Enabled = true;
                        fbtnReadEnd.Enabled = false;
                        return;
                    }
                    while (!BackObj.Contains("EB 90 31 A3"))
                    {
                        Thread.Sleep(100);
                    }
                    if (BackObj.Contains("EB 90 31 A3"))
                    {
                        string temp = BackObj.Substring(BackObj.Length - 16).Replace(" ", "");
                        temp = Convert.ToInt64(temp, 16).ToString();

                        if (int.Parse(temp) > Math.Pow(10, (int)OrderSendType.Total))
                            temp = ((int)GetPMT(double.Parse(temp))).ToString();
                        txtReadShow.AppendText(DateTime.Now.ToString("HH-mm-ss") + ": " + "PMT背景值：" + temp + Environment.NewLine);
                    }
                    Thread.Sleep(1000);
                }
                int washerrror = WashTurn(strNumMRead,1);
                if (washerrror != (int)ErrorState.Success)
                {
                    txtReadShow.AppendText("清洗盘旋转指令异常:" + Environment.NewLine);
                    fbtnRead.Enabled = true;
                    fbtnReadEnd.Enabled = false;
                    return;
                }
                Thread.Sleep(500);
            }
            fbtnRead.Enabled = true;
            fbtnReadEnd.Enabled = false;
        }
        private void fbtnReadEnd_Click(object sender, EventArgs e)
        {
            fbtnReadEnd.Enabled = false;
            if (bRRectClear)
                bRRectClear = false;
            if (bRunWClear)
                bRunWClear = false;
            if (bRead)
                bRead = false;
            fbtnRead.Enabled = true;
        }
        private void btnScanSpCode_Click(object sender, EventArgs e)
        {
            string SpCodePos = nudSpCodePos.Value.ToString();
            ///发送指令读取SpCodePos位置的条码
            txtSpCode.Text = BackObj;
        }

        private void btnZx_Click(object sender, EventArgs e)
        {
            int pos = int.Parse(numXz.Text);
            int error = WashTurn(strNumMRead,pos);
        }

        private void btnReadNum_Click(object sender, EventArgs e)
        {
            textReadShow.Clear();
            btnReadNum.Enabled = false;
            int Num = int.Parse(numRepeat.Text);
            NetCom3.Instance.ReceiveHandel += GetReadNum2;
            for (int i = 1; i <= Num; i++)
            {
                WashSend.Initial();
                WashSend.ReadFlag = 1;
                int errror = WashAddLiquidR(strNumMRead);
                if (errror != (int)ErrorState.Success)
                {
                    fbtnRead.Enabled = true;
                    fbtnReadEnd.Enabled = false;
                    return;
                }
            }
            Thread.Sleep(500);
            NetCom3.Instance.ReceiveHandel -= GetReadNum2;
            TExtAppend("已完成" + Environment.NewLine);
            //textReadShow.AppendText("已完成" + Environment.NewLine);
            btnReadNum.Enabled = true;
        }
        private void TExtAppend(string text)
        {
            while (!this.IsHandleCreated)
            {
                Thread.Sleep(15);
            }
            textReadShow.Invoke(new Action(() => { textReadShow.AppendText(Environment.NewLine + text); }));
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
                if (int.Parse(temp) > Math.Pow(10, (int)OrderSendType.Total))
                    temp = ((int)GetPMT(double.Parse(temp))).ToString();
                TExtAppend(DateTime.Now.ToString("HH-mm-ss") + ": " + "PMT背景值：" + temp);
            }
        }
        private void btnSelectBin_Click(object sender, EventArgs e)
        {
            if (cmbZhenID.SelectedIndex < 0)
            {
                MessageBox.Show("未选择电控板位，请重新选择！");
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

        private void btnLoadProgram_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
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
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            iapFilePath = txtFilePath.Text;
            selectZhenID = cmbZhenID.SelectedIndex + 1;  //1-8 计数器 通讯 抓手 加样 清洗 报警 制冷 温育 
            cmbZhenID.Enabled = txtFilePath.Enabled = btnSelectBin.Enabled = btnLoadProgram.Enabled = false;
            thFire = new Thread(new ThreadStart(LoadPro));
            thFire.Start();
        }
        private void LoadPro() //IAP
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
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
                    frmMsgShow.MessageShow("ERROR", "这是一个空文件。");
                    goto errorOrEnd;
                }
            }
            catch (System.Exception exp)
            {
                frmMsgShow.MessageShow("ERROR", "获取该文件信息失败!");
                Console.WriteLine(exp.ToString());
                goto errorOrEnd;
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
            BeginInvoke(new Action(() =>
            {
                lblDescribe.Text = "上下位机正在握手...";
            }));
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 AA 01"), (int)OrderSendType.Total); //通讯板处理
            if (!NetCom3.Instance.SingleQuery())
            {
                frmMsgShow.MessageShow("ERROR", "握手指令发送失败！");
                goto errorOrEnd;
            }
            while (receiveFlag[0] == 0)
            {
                NetCom3.Delay(100);
            }
            //握手 下位机
            BeginInvoke(new Action(() =>
            {
                lblDescribe.Text = "正在跳转 " + boardName;
            }));
            NetCom3.Instance.iapNoBack = true; //A0 01改成不返回了
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 A0 01 0" + selectZhenID), (int)OrderSendType.Total); //下位机跳转 
            if (!NetCom3.Instance.SingleQuery())
            {
                frmMsgShow.MessageShow("ERROR", "跳转指令发送失败！");
                goto errorOrEnd;
            }
            NetCom3.Instance.iapNoBack = false;

            if (selectZhenID == 2) //如果烧录通讯板，在这一步通讯板要跳转需要重连
            {
                aabc:
                NetCom3.Delay(3000);
                if (NetCom3.Instance.CheckMyIp_Port_Link())
                {
                    NetCom3.isConnect = false;
                    NetCom3.Instance.iapConnectTwice();
                    if (!NetCom3.isConnect)
                    {
                        frmMsgShow.MessageShow("ERROR", "重新连接失败，正在重新连接。");
                        goto aabc;
                    }
                }
                else
                    goto aabc;
            }

            NetCom3.Delay(3000);
            //握手 下位机IAP
            BeginInvoke(new Action(() =>
            {
                lblDescribe.Text = "正在与 " + boardName + " 的IAP握手";
            }));

            NetCom3.Instance.Send(NetCom3.Cover("EB 90 B0 01 0" + selectZhenID), (int)OrderSendType.Total); //与IAP握手
            if (!NetCom3.Instance.SingleQuery())
            {
                frmMsgShow.MessageShow("ERROR", "IAP握手指令发送失败！");
                goto errorOrEnd;
            }
            NetCom3.Delay(1000); //等待1秒
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
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 B0 02"), (int)OrderSendType.Total); //擦除
            if (!NetCom3.Instance.SingleQuery())
            {
                frmMsgShow.MessageShow("ERROR", "擦除指令发送失败！");
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
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 B0 03 00 00 " + len), (int)OrderSendType.Total);
            if (!NetCom3.Instance.SingleQuery())
            {
                frmMsgShow.MessageShow("ERROR", "文件长度发送失败！");
                goto errorOrEnd;
            }
            while (receiveFlag[4] == 0)
            {
                NetCom3.Delay(100);
            }

            //传输数据
            NetCom3.Instance.iapNoBack = true;
            BeginInvoke(new Action(() =>
            {
                pgbLoad.Value = 30;//进度百分之30
                lblPercentage.Text = pgbLoad.Value.ToString() + "%";
                lblDescribe.Text = "正在发送数据...";
            }));
            Stopwatch swatch = new Stopwatch();
            for (int m = 0, k = 1; m < allNumber; m += 8) //通讯指令16进制2位一字节
            {
                //Console.WriteLine(m+"-------"+allNumber);//测试
                swatch.Reset();
                if ((m + 8) >= allNumber)
                    NetCom3.Instance.iapNoBack = false;
                string data = "EB 90 "
                            + buff[m].ToString("x2") + " " + buff[m + 1].ToString("x2") + " "
                            + buff[m + 2].ToString("x2") + " " + buff[m + 3].ToString("x2") + " "
                            + buff[m + 4].ToString("x2") + " " + buff[m + 5].ToString("x2") + " "
                            + buff[m + 6].ToString("x2") + " " + buff[m + 7].ToString("x2");
                NetCom3.Instance.Send(NetCom3.Cover(data), (int)OrderSendType.Total);
                if (!IapTraQuery())//循环等待变量是延迟1ms
                {
                    frmMsgShow.MessageShow("ERROR", "数据传输失败！");
                    goto errorOrEnd;
                }
                //if (!NetCom3.Instance.SingleQuery())
                //{
                //    frmMsgShow.MessageShow("ERROR", "数据传输失败！");
                //    goto errorOrEnd;
                //}

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
                swatch.Restart();
                while (swatch.Elapsed.TotalMilliseconds < 3)
                {
                    ;
                }
            }
            swatch.Stop();
            swatch.Reset();
            while (true)
            {
                if (receiveFlag[5] == 0)
                    NetCom3.Delay(100);
                else if (receiveFlag[5] == 1)
                    break;
                else if (receiveFlag[5] == 2)
                {
                    frmMsgShow.MessageShow("ERROR", "IAP程序烧录失败");
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
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 B0 05 00 00 00 00 80 00"), (int)OrderSendType.Total);
            if (!NetCom3.Instance.SingleQuery())
            {
                frmMsgShow.MessageShow("ERROR", "结束指令发送失败！");
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
                        frmMsgShow.MessageShow("ERROR", "重新连接失败，正在重新连接。");
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
                frmMsgShow.MessageShow("IAP", "请重启软件和仪器，点击确定自动退出软件");
                Environment.Exit(0);
            }

            #region  烧完程序初始化前先握手
            BackObj = "";
            int[] HandData = new int[16];
            NetCom3.Delay(2000); //预留b0 05跳转时间
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 F1 01"), (int)OrderSendType.Total);
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
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 F1 02"), (int)OrderSendType.Total);
            if (!NetCom3.Instance.SingleQuery())
            {
                goto errorOrEnd;
            }
            //判断是否初始化成功            
            if (NetCom3.Instance.ErrorMessage != null)
            {
                frmMsgShow.MessageShow("仪器初始化", NetCom3.Instance.ErrorMessage);
                goto errorOrEnd;
            }
            frmMsgShow.MessageShow("IAP", "程序下载成功！");

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
            }));
            buff = new byte[1];
            buff1 = new byte[1];
            GC.Collect();
        }
        private void DealReceive(string order)
        {
            order = order.Substring(6, 8);
            if (order.Contains("55 01"))
            {
                receiveFlag[0] = 1;
            }
            //else if (order.Contains("0A A0 0" + selectZhenID))  //已修改，不返回这条指令
            //{
            //    receiveFlag[1] = 1;
            //}
            else if (order.Contains("6A A6 0" + selectZhenID))
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
            while (!NetCom3.totalOrderFlag)
            {
                //Thread.Sleep(1);
            }
            if (NetCom3.Instance.errorFlag != (int)ErrorState.Success)
            {
                LogFile.Instance.Write("MoverrorFlag = ： " + NetCom3.Instance.MoverrorFlag + "  *****当前 " + DateTime.Now.ToString("HH - mm - ss"));
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        #region 温控设置
        /// <summary>
        /// 试剂盘温度列表
        /// </summary>
        List<decimal> Rlist = new List<decimal>();//试剂盘
        /// <summary>
        /// 温育盘温度列表
        /// </summary>
        List<decimal> Wlist = new List<decimal>();//温育盘
        /// <summary>
        /// 清洗盘温度列表
        /// </summary>
        BindingList<decimal> Qlist = new BindingList<decimal>();//清洗盘
        /// <summary>
        /// 底物管路温度列表
        /// </summary>
        BindingList<decimal> Dlist = new BindingList<decimal>();//底物
        /// <summary>
        /// 清洗管路温度列表
        /// </summary>
        BindingList<decimal> Qglist = new BindingList<decimal>();//底物
        decimal timespan;//时间间隔
        double numOfSam, num1, num2;//采样数、y轴下界、y轴上界
        string strMNumTem = "90";
        delegate void SetTextCallBack(string text);
        private void numTem_ValueChanged(object sender, EventArgs e)
        {
            string num = ((int)numMIncubation.Value - 1).ToString("x2");
            strMNumTem = "9" + num.Substring(1, 1).ToUpper();
        }
        private void SettxtStandard(string text)
        {
            txtStandard.Text = text;
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            txtStandard.Text = "";
            //2018-07-03 zlx add
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbModelName.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择要调试的温控模块！");
                return;
            }
            if (cmbStep.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择要查询的温控模块！");
                return;
            }
            else if (cmbStep.SelectedItem.ToString() != "查询校准值" && cmbStep.SelectedItem.ToString() != "查询温度")
            {
                frmMsgShow.MessageShow("仪器调试", "选择查询的温控模块有误，请重新选择！");
                return;
            }
            btnSelect.Enabled = false;
            switch (cmbModelName.SelectedItem.ToString())
            {
                case "温育盘":
                    if (cmbStep.SelectedItem.ToString() == "查询校准值")
                        NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 04 06"), (int)OrderSendType.Total);
                    else if (cmbStep.SelectedItem.ToString() == "查询温度")
                        NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 04 04"), (int)OrderSendType.Total);
                    break;
                case "清洗盘":
                    if (cmbStep.SelectedItem.ToString() == "查询校准值")
                        NetCom3.Instance.Send(NetCom3.Cover("EB 90 11 05 06"), (int)OrderSendType.Total);
                    else if (cmbStep.SelectedItem.ToString() == "查询温度")
                        NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 05 04"), (int)OrderSendType.Total);
                    break;
                case "清洗管路":
                    if (cmbStep.SelectedItem.ToString() == "查询校准值")
                        NetCom3.Instance.Send(NetCom3.Cover("EB 90 11 06 06"), (int)OrderSendType.Total);
                    else if (cmbStep.SelectedItem.ToString() == "查询温度")
                        NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 06 04"), (int)OrderSendType.Total);
                    break;
                case "底物管路":
                    if (cmbStep.SelectedItem.ToString() == "查询校准值")
                        NetCom3.Instance.Send(NetCom3.Cover("EB 90 11 07 06"), (int)OrderSendType.Total);
                    else if (cmbStep.SelectedItem.ToString() == "查询温度")
                        NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 07 04"), (int)OrderSendType.Total);
                    break;
                case "试剂盘":
                    if (cmbStep.SelectedItem.ToString() == "查询校准值")
                        NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 08 06"), (int)OrderSendType.Total);
                    else if (cmbStep.SelectedItem.ToString() == "查询温度")
                        NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 08 04"), (int)OrderSendType.Total);
                    if (!reagentColdQuery())
                    {
                        frmMsgShow.MessageShow("ERROR", "请检查是否打开制冷开关.");
                    }
                    break;
            }
            NetCom3.Instance.SingleQuery();
            btnSelect.Enabled = true;
        }
        /// <summary>
        /// 试剂盘温度超时默认查询
        /// </summary>
        /// <returns></returns>
        public bool reagentColdQuery()
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
                LogFile.Instance.Write("MoverrorFlag = ： " + NetCom3.Instance.MoverrorFlag + "  *****当前 " + DateTime.Now.ToString("HH - mm - ss"));
                return false;
            }
            else
            {
                return true;
            }
        }
        private void btnmakeStandard_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbModelName.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择要调试的温控模块！");
                return;
            }
            if (cmbStep.Text == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请选择要调试的操作内容！");
                return;
            }
            btnmakeStandard.Enabled = false;
            switch (cmbModelName.SelectedItem.ToString())
            {
                case "温育盘":
                    switch (cmbStep.SelectedItem.ToString())
                    {
                        case "加热打开":
                            NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 04 00"), (int)OrderSendType.Total);
                            break;
                        case "加热关闭":
                            NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 04 01"), (int)OrderSendType.Total);
                            break;
                        case "设置校准值":
                            if (txtStandard.Text == "")
                            {
                                frmMsgShow.MessageShow("仪器调试", "请录入要校准的温度值！");
                                return;
                            }
                            string jzwendu = NetCom3.FloatToHex(float.Parse(txtStandard.Text));
                            NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 04 03 " + jzwendu), (int)OrderSendType.Total);
                            break;
                        default:
                            break;
                    }
                    break;
                case "清洗盘":
                    switch (cmbStep.Text)
                    {
                        case "加热打开":
                            NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 05 00"), (int)OrderSendType.Total);
                            break;
                        case "加热关闭":
                            NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 05 01"), (int)OrderSendType.Total);
                            break;
                        case "设置校准值":
                            if (txtStandard.Text == "")
                            {
                                frmMsgShow.MessageShow("仪器调试", "请录入要校准的温度值！");
                                return;
                            }
                            string jzwendu = NetCom3.FloatToHex(float.Parse(txtStandard.Text));
                            NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 05 03 " + jzwendu), (int)OrderSendType.Total);
                            break;
                        default:
                            break;
                    }
                    break;
                case "清洗管路":
                    switch (cmbStep.Text)
                    {
                        case "加热打开":
                            NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 06 00"), (int)OrderSendType.Total);
                            break;
                        case "加热关闭":
                            NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 06 01"), (int)OrderSendType.Total);
                            break;
                        case "设置校准值":
                            if (txtStandard.Text == "")
                            {
                                frmMsgShow.MessageShow("仪器调试", "请录入要校准的温度值！");
                                return;
                            }
                            string jzwendu = NetCom3.FloatToHex(float.Parse(txtStandard.Text));
                            NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 06 03 " + jzwendu), (int)OrderSendType.Total);
                            break;
                        default:
                            break;
                    }
                    break;
                case "底物管路":
                    switch (cmbStep.Text)
                    {
                        case "加热打开":
                            NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 07 00"), (int)OrderSendType.Total);
                            break;
                        case "加热关闭":
                            NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 07 01"), (int)OrderSendType.Total);
                            break;
                        case "设置校准值":
                            if (txtStandard.Text == "")
                            {
                                frmMsgShow.MessageShow("仪器调试", "请录入要校准的温度值！");
                                return;
                            }
                            string jzwendu = NetCom3.FloatToHex(float.Parse(txtStandard.Text));
                            NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 07 03 " + jzwendu), (int)OrderSendType.Total);
                            break;
                        default:
                            break;
                    }
                    break;
                case "试剂盘":
                    if (cmbStep.Text.ToString() == "设置校准值")
                    {
                        if (txtStandard.Text == "")
                        {
                            frmMsgShow.MessageShow("仪器调试", "请录入要校准的温度值！");
                            break;
                        }
                        string sjwendu = NetCom3.FloatToHex(float.Parse(txtStandard.Text));
                        NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 08 03 " + sjwendu), (int)OrderSendType.Total);
                        if (!reagentColdQuery())
                        {
                            frmMsgShow.MessageShow("ERROR", "请检查是否打开制冷开关");
                        }
                    }
                    break;
                default:
                    break;
            }
            NetCom3.Instance.SingleQuery();
            btnmakeStandard.Enabled = true;
        }
        /// <summary>
        /// 读取配置文件,InstrumentPara,tempreature
        /// </summary>
        private void iniReader()
        {
            numOfSam = double.Parse(Common.OperateIniFile.ReadInIPara("temperature", "numOfSam"));
            timespan = decimal.Parse(Common.OperateIniFile.ReadInIPara("temperature", "timespan"));
            num1 = double.Parse(Common.OperateIniFile.ReadInIPara("temperature", "num1"));
            num2 = double.Parse(Common.OperateIniFile.ReadInIPara("temperature", "num2"));
        }
        private void saveSetting_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (saveSetting.Text == "更改设置")
            {

                saveSetting.Text = "保存设置";
                numDown.Enabled = numUp.Enabled = numOfSample.Enabled = timespanOfSample.Enabled = restore.Enabled = true;
            }
            else
            {

                numOfSam = (double)numOfSample.Value;
                timespan = timespanOfSample.Value;
                num1 = (double)numDown.Value;
                num2 = (double)numUp.Value;
                if (num1 >= num2)
                {
                    frmMsgShow.MessageShow("温度设置警告", "温度范围的选择只能从小到大，最低温度不能高于或等于最高温度");
                    numDown.Focus();
                    return;
                }
                timer1.Interval = Convert.ToInt32(timespan) * 1000;
                chart1.ChartAreas[0].AxisX.Maximum = numOfSam;
                chart1.ChartAreas[0].AxisY.Minimum = num1;
                chart1.ChartAreas[0].AxisY.Maximum = num2;
                iniseter();//保存一次配置文件
                saveSetting.Text = "更改设置";
                numDown.Enabled = numUp.Enabled = numOfSample.Enabled = timespanOfSample.Enabled = restore.Enabled = false;
            }
        }
        private void restore_Click(object sender, EventArgs e)
        {
            if (restore.Enabled == true)
            {
                numOfSample.Value = (decimal)numOfSam;
                timespanOfSample.Value = timespan;
                numDown.Value = (decimal)num1;
                numUp.Value = (decimal)num2;

                saveSetting.Text = "更改设置";
                numDown.Enabled = numUp.Enabled = numOfSample.Enabled = timespanOfSample.Enabled = restore.Enabled = false;
            }
        }
        bool SelectTemFlag;
        private void beginAndStop_Click(object sender, EventArgs e)
        {
             if (beginAndStop.Text == "开始")
            {
                suspendAndContinue.Enabled = true;
                beginAndStop.Text = "终止";
                suspendAndContinue.Text = "暂停";
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                SelectTemFlag = false;
                timer1.Enabled = false;
                timer1.Stop();
                timer1.Enabled = false;
                chart1.Series["reagent"].Points.Clear();
                chart1.Series["wenyu"].Points.Clear();
                chart1.Series["qingxi"].Points.Clear();
                chart1.Series["diwu"].Points.Clear();
                chart1.Series["qxgl"].Points.Clear();
                Rlist.Clear();
                Wlist.Clear();
                Qlist.Clear();
                Dlist.Clear();
                Qglist.Clear();
                suspendAndContinue.Enabled = false;
                beginAndStop.Text = "开始";
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag || SelectTemFlag)
            {
                return;
            }
            SelectTemFlag = true;
            if (chkWY.Checked)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 04 04"), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            if (chkQX.Checked)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 05 04"), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            if (chkDW.Checked)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 07 04"), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            if (chkQXGL.Checked)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 06 04"), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            if (chkRegent.Checked)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB "+strMNumTem+" 11 08 04"), (int)OrderSendType.Total);
                if (!reagentColdQuery())
                {
                    SelectTemFlag = false;
                    timer1.Enabled = false;
                    timer1.Stop();
                    timer1.Enabled = false;
                    chart1.Series["reagent"].Points.Clear();
                    chart1.Series["wenyu"].Points.Clear();
                    chart1.Series["qingxi"].Points.Clear();
                    chart1.Series["diwu"].Points.Clear();
                    chart1.Series["qxgl"].Points.Clear();//2018-07-05 zlx add
                    Rlist.Clear();
                    Wlist.Clear();
                    Qlist.Clear();
                    Dlist.Clear();
                    Qglist.Clear();//2018-07-05 zlx add                    
                    suspendAndContinue.Enabled = false;

                    frmMsgShow.MessageShow("ERROR", "请检查是否打开制冷开关.");


                    beginAndStop.Text = "开始";
                    return;
                }
            }

            NetCom3.Delay(100);
            if (Wlist.Count == 0 && Qlist.Count == 0 && Dlist.Count == 0 && Qglist.Count == 0 && Rlist.Count == 0)
                return;
            if (chart1.IsDisposed || chart1 == null) //防止nullreferencerefresh异常 jun add 20190426
            {
                return;
            }
            try
            {
                chart1.Series["reagent"].Points.DataBindY(Rlist);
                chart1.Series["wenyu"].Points.DataBindY(Wlist);
                chart1.Series["qingxi"].Points.DataBindY(Qlist);
                chart1.Series["diwu"].Points.DataBindY(Dlist);
                chart1.Series["qxgl"].Points.DataBindY(Qglist);
            }
            catch { }
            NetCom3.Delay(10);
            if (Rlist.Count > 0)
                labreagent.Text = Rlist[Rlist.Count - 1] + "";
            if (Wlist.Count > 0)
                labwenyu.Text = Wlist[Wlist.Count - 1] + "";
            if (Qlist.Count > 0)
                labqingxi.Text = Qlist[Qlist.Count - 1] + "";
            if (Dlist.Count > 0)
                labdiwu.Text = Dlist[Dlist.Count - 1] + "";
            if (Qglist.Count > 0)
                labqxgl.Text = Qglist[Qglist.Count - 1] + "";
            SelectTemFlag = false;
        }
        private void suspendAndContinue_Click(object sender, EventArgs e)
        {
            if (suspendAndContinue.Text == "暂停")
            {
                timer1.Enabled = false;
                suspendAndContinue.Text = "继续";
            }
            else
            {
                timer1.Enabled = true;
                suspendAndContinue.Text = "暂停";
            }
        }

        private void saveTo_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;

                FileStream file = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
                StreamWriter aw = new StreamWriter(file, Encoding.GetEncoding("GB2312"));
                StringBuilder sb = new StringBuilder();

                sb.Append(DateTime.Now.ToString("F") + Environment.NewLine);
                sb.Append("采样点" + "\t" + "试剂盘温度" + "\t" + "温育盘温度" + "\t" + "清洗盘温度" + "\t" + "底物温度" + "\t" + "清洗管路温度" + Environment.NewLine);

                for (int i = 1; i < Rlist.Count; i++)
                {
                    sb.Append(i.ToString() + "\t" + Rlist[i - 1] + "\t" + Wlist[i - 1] + "\t" + Qlist[i - 1] + "\t" + Dlist[i - 1] + "\t" + Qglist[i - 1] + Environment.NewLine);
                }

                aw.Write(sb.ToString());
                aw.Flush();
                file.Flush();
                aw.Close();
                file.Close();
                aw.Dispose();
                file.Dispose();
            }
        }
        #endregion

        #region 轨道机
        /// <summary>
        /// 轨道模块标志 
        /// 0-常规  1-急诊
        /// </summary>
        int MOrbiter = 0;
        /// <summary>
        /// 普通通道位置
        /// 挡块I，加样定位片，变道电机-常规道,变道电机-急诊道,变道电机-回归道,挡块II
        /// </summary>
        string[] OrbCPos = { "挡块I","加样定位片","变道电机-常规道","变道电机-急诊道 ","变道电机-回归道 ","挡块II" };
        /// <summary>
        /// 急诊通道位置
        /// 挡块I，加样定位片
        /// </summary>
        string[] OrbERPos = { "挡块I", "加样定位片" };
        /// <summary>
        /// 普通通道电机
        /// 挡块I，加样定位片，变道电机,挡块II
        /// </summary>
        string[] OrbCElecMach = { "挡块I","加样定位片","变道电机","挡块II" };
        /// <summary>
        /// 急诊通道电机
        /// 挡块I，加样定位片
        /// </summary>
        string[] OrbERElecMach = { "挡块I","加样定位片" };
        /// <summary>
        /// 级联仪器编号
        /// </summary>
        string numOrbiterID ="0";
        string OrbOrder = "90";
        private void NumOrbiterID_ValueChanged(object sender, EventArgs e)
        {
            numOrbiterID =int.Parse((NumOrbiterID.Value-1).ToString()).ToString("x2");
            OrbOrder = "9" + numOrbiterID.Substring(1, 1);
        }
        private void cmbMOrbiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbOrbPos.Items.Clear();
            cmbOrbElecMach.Items.Clear();
            if (cmbMOrbiter.SelectedItem.ToString() == "常规通道")
            {
                MOrbiter = 0;
                foreach (string pos in OrbCPos)
                {
                    cmbOrbPos.Items.Add(pos);
                }
                foreach (string Elec in OrbCElecMach)
                {
                    cmbOrbElecMach.Items.Add(Elec);
                }
                btnOrbRoutIReset.Text = "轨道I常规通道复位";
                btnCOrbitReset.Visible = true;
                btnOrbP2Reset.Visible = true;
            }
            else
            {
                MOrbiter = 1;
                foreach (string pos in OrbERPos)
                {
                    cmbOrbPos.Items.Add(pos);
                }
                foreach (string Elec in OrbERElecMach)
                {
                    cmbOrbElecMach.Items.Add(Elec);
                }
                btnOrbRoutIReset.Text = "轨道II急诊通道复位";
                btnCOrbitReset.Visible = false;
                btnOrbP2Reset.Visible = false;
            }
           
        }

        private void cmbOrbPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (cmbOrbPos.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择位置调教参数！");
                return;
            }
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MOrbiter == 0)
            {
                if (cmbOrbPos.SelectedItem.ToString() == OrbCPos[0])
                {
                    strorder = "EB "+OrbOrder+" 06 01 01";

                }
                else if (cmbOrbPos.SelectedItem.ToString() == OrbCPos[1])
                {
                    strorder = "EB "+OrbOrder+" 06 01 02";

                }
                else if (cmbOrbPos.SelectedItem.ToString() == OrbCPos[2])
                {
                    strorder = "EB "+OrbOrder+" 06 01 03";

                }
                else if (cmbOrbPos.SelectedItem.ToString() == OrbCPos[3])
                {
                    strorder = "EB "+OrbOrder+" 06 01 04";

                }
                else if (cmbOrbPos.SelectedItem.ToString() == OrbCPos[4])
                {
                    strorder = "EB "+OrbOrder+" 06 01 05";

                }
                else if (cmbOrbPos.SelectedItem.ToString() == OrbCPos[5])
                {
                    strorder = "EB "+OrbOrder+" 06 01 06";

                }
            }
            else
            {
                if (cmbOrbPos.SelectedItem.ToString() == OrbERPos[0])
                {
                    strorder = "EB "+OrbOrder+" 06 01 07";

                }
                else if (cmbOrbPos.SelectedItem.ToString() == OrbERPos[1])
                {
                    strorder = "EB "+OrbOrder+" 06 01 08";
                }
            }
            cmbMOrbiter.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            cmbMOrbiter.Enabled = true;
        }

        private void btnOrbInc_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbOrbElecMach.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择需调试的电机！");
                return;
            }
            if (txtOrbIncValue.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入增量值！");
                return;
            }
            string incream = int.Parse(txtOrbIncValue.Text.Trim()).ToString("x8");
            string strorder = "";
            if (MOrbiter == 0)
            {
                if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbCElecMach[0].Trim())
                {
                    strorder = "EB "+OrbOrder+" 06 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbCElecMach[1].Trim())
                {
                    strorder = "EB "+OrbOrder+" 06 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbCElecMach[2].Trim())
                {
                    strorder = "EB "+OrbOrder+" 06 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                //else if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbCElecMach[3].Trim())
                //{
                //    strorder = "EB "+OrbOrder+" 06 02 04 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                //                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                //}
                //else if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbCElecMach[4].Trim())
                //{
                //    strorder = "EB "+OrbOrder+" 06 02 05 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                //                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                //}
                else if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbCElecMach[3].Trim())
                {
                    strorder = "EB "+OrbOrder+" 06 02 06 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
               
            }
            else
            {

                if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbERElecMach[0].Trim())
                {
                    strorder = "EB "+OrbOrder+" 06 02 07 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2) ;
                }
                else if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbERElecMach[1].Trim())
                {
                    strorder = "EB "+OrbOrder+" 06 02 08 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2) ;
                }
            }
            btnOrbInc.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnOrbInc.Enabled = true;
        }

        private void btnOrbDsc_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbOrbElecMach.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择需调试的电机！");
                return;
            }
            if (txtOrbIncValue.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入增量值！");
                return;
            }
            string incream = int.Parse("-" + txtOrbIncValue.Text.Trim()).ToString("x8");
            string strorder = "";
            if (MOrbiter == 0)
            {
                if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbCElecMach[0].Trim())
                {
                    strorder = "EB "+OrbOrder+" 06 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2) ;
                }
                else if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbCElecMach[1].Trim())
                {
                    strorder = "EB "+OrbOrder+" 06 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2) ;
                }
                else if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbCElecMach[2].Trim())
                {
                    strorder = "EB "+OrbOrder+" 06 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2) ;
                }
                //else if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbCElecMach[3].Trim())
                //{
                //    strorder = "EB "+OrbOrder+" 06 02 04 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                //                + incream.Substring(4, 2) + " " + incream.Substring(6, 2) ;
                //}
                //else if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbCElecMach[4].Trim())
                //{
                //    strorder = "EB "+OrbOrder+" 06 02 05 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                //                + incream.Substring(4, 2) + " " + incream.Substring(6, 2) ;
                //}
                else if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbCElecMach[3].Trim())
                {
                    strorder = "EB "+OrbOrder+" 06 02 06 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2) ;
                }

            }
            else
            {

                if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbERElecMach[0].Trim())
                {
                    strorder = "EB "+OrbOrder+" 06 02 07 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2) ;
                }
                else if (cmbOrbElecMach.SelectedItem.ToString().Trim() == OrbERElecMach[1].Trim())
                {
                    strorder = "EB "+OrbOrder+" 06 02 08 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2) ;
                }
            }
            btnOrbDsc.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnOrbDsc.Enabled = true;
        }
        private void btnOrbSave_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MOrbiter == 0)
            {
                strorder = "EB "+OrbOrder+" 06 04";
            }
            else
            {
                strorder = "EB "+OrbOrder+" 06 14";
            }
            btnOrbSave.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnOrbSave.Enabled = true;
        }
        private void btnOrbSaveData_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            btnOrbSaveData.Enabled = false;
            List<string> data = GetData(OrbOrder, "06");

            if (data.Count() < 1)
            {
                MessageBox.Show("数据不存在");
            }
            if (MOrbiter == 0)
            {
                foreach (string item in data)
                {
                    string s = ("EB " + item.Substring(0, 5) + " 04 " + item.Substring(18)).TrimEnd();
                    NetCom3.Instance.Send(NetCom3.Cover(s), 5);
                    NetCom3.Instance.SingleQuery();
                }
            }
            else
            {
                foreach (string item in data)
                {
                    string s = ("EB " + item.Substring(0, 5) + " 14 " + item.Substring(18)).TrimEnd();
                    NetCom3.Instance.Send(NetCom3.Cover(s), 5);
                    NetCom3.Instance.SingleQuery();
                }
            }
            btnOrbSaveData.Enabled = true;
        }

        private void btnOrbAllReset_Click(object sender, EventArgs e)
        {

        }

        private void btnOrbP1Reset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MOrbiter == 0)
            {
                strorder = "EB "+OrbOrder+" 06 03 01";
            }
            else
            {
                strorder = "EB "+OrbOrder+" 06 03 07";
            }
            btnOrbP1Reset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnOrbP1Reset.Enabled = true;
        }

        private void btnOrbASPosReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MOrbiter == 0)
            {
                strorder = "EB "+OrbOrder+" 06 03 02";
            }
            else
            {
                strorder = "EB "+OrbOrder+" 06 03 08";
            }
            btnOrbASPosReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnOrbASPosReset.Enabled = true;
        }

        private void btnOrbRoutIReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MOrbiter == 0)
            {
                strorder = "EB "+OrbOrder+" 06 03 F1";
            }
            else
            {
                strorder = "EB "+OrbOrder+" 06 03 F2";
            }
            btnOrbRoutIReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnOrbRoutIReset.Enabled = true;
        }

        private void btnHGOrbitReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "EB "+OrbOrder+" 06 03 F3";
            btnHGOrbitReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnHGOrbitReset.Enabled = true;
        }

        private void btnCOrbitReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "EB "+OrbOrder+" 06 03 03";
            btnCOrbitReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnCOrbitReset.Enabled = true;
        }

        private void btnOrbP2Reset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "EB "+OrbOrder+" 06 03 06";
            btnOrbP2Reset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnOrbP2Reset.Enabled = true;
        }

        #endregion
        #region 调度模块
        /// <summary>
        /// 调度区标志
        /// 1-调度1区 2-调度2区
        /// </summary>
        int MConrrol = 1;
        /// <summary>
        /// 调度1区位置列表
        /// 样本待检区推手扫码位置，样本待检区结束终点位置，回完成区/待检区推手到完成区位置,回完成区/待检区推手到待检区位置,完成区挡片
        /// </summary>
        string[] MControlPos1 = { "样本待检区推手扫码位置", "样本待检区结束终点位置", "回完成区/待检区推手到完成区位置", "回完成区/待检区推手到待检区位置", "完成区挡片" };
        /// <summary>
        /// 调度2区位置列表
        /// 出样本待检区推手到链盘结束位置,出调度去轨道挡片退走到轨道位置,链盘电机在推向轨道处位置,链盘电机在推向完成待检区位置
        /// </summary>
        string[] MControlPos2 = { "出样本待检区推手到链盘结束位置", "出调度去轨道挡片退走到轨道位置", "链盘电机在推向轨道处位置", "链盘电机在推向完成待检区位置" };
        /// <summary>
        /// 调度1区电机列表
        /// 样本待检区推手电机，回完成区/待检区推手电机，完成区回收电机
        /// </summary>
        string[] MConElecMach1 = { "样本待检区推手电机", "回完成区/待检区推手电机", "完成区回收电机"};
        /// <summary>
        /// 调度2区电机列表
        /// 出样本待检区推手电机，出调度去轨道挡片退走电机，链盘电机
        /// </summary>
        string[] MConElecMach2 = { "出样本待检区推手电机", "出调度去轨道挡片退走电机", "链盘电机" };
        /// <summary>
        /// 级联机器编号
        /// </summary>
        string strMControl = "90";
        private void numMcontrol_ValueChanged(object sender, EventArgs e)
        {
            numOrbiterID = int.Parse((NumOrbiterID.Value - 1).ToString()).ToString("x2");
            OrbOrder = "9" + numOrbiterID.Substring(1, 1);
        }
        private void cmbMcontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbConPos.Items.Clear();
            cmbConElecMach.Items.Clear();
            if (cmbMcontrol.SelectedItem.ToString().Contains("调度I区"))
            {
                MConrrol = 1;
                btnCon1Reset.Text = "调度I区复位";
                btnConSDReset.Text = "样本待检区推手复位";
                btnConSMReset.Text = "扫码1挡板复位";
                btnConFReset.Text = "完成区挡板复位";
                foreach (string pos in MControlPos1)
                {
                    cmbConPos.Items.Add(pos);
                }
                foreach (string emc in MConElecMach1)
                {
                    cmbConElecMach.Items.Add(emc);
                }
                btnConRSDReset.Visible = true;
            }
            else
            {
                MConrrol = 2;
                btnCon1Reset.Text = "调度II区复位";
                btnConSDReset.Text = "出样本待检区推手复位";
                btnConSMReset.Text = "出调度去轨道挡片复位";
                btnConFReset.Text = "链盘电机复位";
                btnConRSDReset.Visible = false;
                foreach (string pos in MControlPos2)
                {
                    cmbConPos.Items.Add(pos);
                }
                foreach (string emc in MConElecMach2)
                {
                    cmbConElecMach.Items.Add(emc);
                }
            }
        }

        private void cmbConPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (cmbConPos.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择位置调教参数！");
                return;
            }
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MConrrol == 1)
            {
                if (cmbConPos.SelectedItem.ToString() == MControlPos1[0])
                {
                    strorder = "EB "+ strMControl + " 05 01 01";
                }
                else if (cmbConPos.SelectedItem.ToString() == MControlPos1[1])
                {
                    strorder = "EB "+ strMControl + " 05 01 02";
                }
                else if (cmbConPos.SelectedItem.ToString() == MControlPos1[2])
                {
                    strorder = "EB "+ strMControl + " 05 01 03";
                }
                else if (cmbConPos.SelectedItem.ToString() == MControlPos1[3])
                {
                    strorder = "EB "+ strMControl + " 05 01 04";
                }
                else if (cmbConPos.SelectedItem.ToString() == MControlPos1[4])
                {
                    strorder = "EB "+ strMControl + " 05 01 05";
                }
            }
            else
            {
                if (cmbConPos.SelectedItem.ToString() == MControlPos2[0])
                {
                    strorder = "EB "+ strMControl + " 05 01 06";
                }
                else if (cmbConPos.SelectedItem.ToString() == MControlPos2[1])
                {
                    strorder = "EB "+ strMControl + " 05 01 07";
                }
                else if (cmbConPos.SelectedItem.ToString() == MControlPos2[2])
                {
                    strorder = "EB "+ strMControl + " 05 01 08";
                }
                else if (cmbConPos.SelectedItem.ToString() == MControlPos2[3])
                {
                    strorder = "EB "+ strMControl + " 05 01 09";
                }
               
            }
            cmbConPos.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            cmbConPos.Enabled = true;
        }

        private void btnConInc_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbConElecMach.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择需调试的电机！");
                return;
            }
            if (txtConIncValue.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入增量值！");
                return;
            }
            string incream = int.Parse(txtConIncValue.Text.Trim()).ToString("x8");
            string strorder = "";
            if (MConrrol == 1)
            {
                if (cmbConElecMach.SelectedItem.ToString().Trim() == MConElecMach1[0].Trim())
                {
                    strorder = "EB "+ strMControl + " 05 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbConElecMach.SelectedItem.ToString().Trim() == MConElecMach1[1].Trim())
                {
                    strorder = "EB "+ strMControl + " 05 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2) ;
                }
                else if (cmbConElecMach.SelectedItem.ToString().Trim() == MConElecMach1[2].Trim())
                {
                    strorder = "EB "+ strMControl + " 05 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }

            }
            else
            {

                if (cmbConElecMach.SelectedItem.ToString().Trim() == MConElecMach2[0].Trim())
                {
                    strorder = "EB "+ strMControl + " 05 02 04 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbConElecMach.SelectedItem.ToString().Trim() == MConElecMach2[1].Trim())
                {
                    strorder = "EB "+ strMControl + " 05 02 05 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbConElecMach.SelectedItem.ToString().Trim() == MConElecMach2[2].Trim())
                {
                    strorder = "EB "+ strMControl + " 05 02 06 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
            }
            btnConInc.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnConInc.Enabled = true;
        }

        private void btnConDsc_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            if (cmbConElecMach.SelectedItem == null)
            {
                frmMsgShow.MessageShow("仪器调试", "请选择需调试的电机！");
                return;
            }
            if (txtConIncValue.Text.Trim() == "")
            {
                frmMsgShow.MessageShow("仪器调试", "请输入增量值！");
                return;
            }
            string incream = int.Parse("-" + txtConIncValue.Text.Trim()).ToString("x8");
            string strorder = "";
            if (MConrrol == 1)
            {
                if (cmbConElecMach.SelectedItem.ToString().Trim() == MConElecMach1[0].Trim())
                {
                    strorder = "EB "+ strMControl + " 05 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbConElecMach.SelectedItem.ToString().Trim() == MConElecMach1[1].Trim())
                {
                    strorder = "EB "+ strMControl + " 05 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbConElecMach.SelectedItem.ToString().Trim() == MConElecMach1[2].Trim())
                {
                    strorder = "EB "+ strMControl + " 05 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }

            }
            else
            {

                if (cmbConElecMach.SelectedItem.ToString().Trim() == MConElecMach2[0].Trim())
                {
                    strorder = "EB "+ strMControl + " 05 02 04 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbConElecMach.SelectedItem.ToString().Trim() == MConElecMach2[1].Trim())
                {
                    strorder = "EB "+ strMControl + " 05 02 05 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
                else if (cmbConElecMach.SelectedItem.ToString().Trim() == MConElecMach2[2].Trim())
                {
                    strorder = "EB "+ strMControl + " 05 02 06 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                                + incream.Substring(4, 2) + " " + incream.Substring(6, 2);
                }
            }
            btnConDsc.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnConDsc.Enabled = true;
        }

        private void btnConSave_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MConrrol == 1)
            {
                strorder = "EB "+ strMControl + " 05 13";
            }
            else
            {
                strorder = "EB "+ strMControl + " 05 23";
            }
            btnConSave.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnConSave.Enabled = true;
        }
        private void btnConSaveData_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            btnConSaveData.Enabled = false;
            List<string> data = GetData(strMControl, "05");

            if (data.Count() < 1)
            {
                MessageBox.Show("数据不存在");
            }
            foreach (string item in data)
            {
                string s = ("EB " + item.Substring(0, 5) + " 13 " + item.Substring(18)).TrimEnd();
                NetCom3.Instance.Send(NetCom3.Cover(s), 5);
                NetCom3.Instance.SingleQuery();
            }
            btnConSaveData.Enabled = true;
        }

        private void btnConAllReset_Click(object sender, EventArgs e)
        {

        }

        private void btnCon1Reset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MConrrol == 1)
            {
                strorder = "EB "+ strMControl + " 05 03 F1";
            }
            else
            {
                strorder = "EB "+ strMControl + " 05 03 F2";
            }
            btnCon1Reset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnCon1Reset.Enabled = true;
        }

        private void btnConSMReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MConrrol == 1)
            {
                strorder = "EB "+ strMControl + " 05 03 01";
            }
            else
            {
                strorder = "EB "+ strMControl + " 05 03 03";
            }
            btnConSMReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnConSMReset.Enabled = true;
        }

        private void btnConSDReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MConrrol == 1)
            {
                strorder = "EB "+ strMControl + " 05 03 00";
            }
            else
            {
                strorder = "EB "+ strMControl + " 05 03 02";
            }
            btnConSDReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnConSDReset.Enabled = true;
        }

        private void btnConFReset_Click(object sender, EventArgs e)
        {

            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            if (MConrrol == 1)
            {
                strorder = "EB "+ strMControl + " 05 03 05";
            }
            else
            {
                strorder = "EB "+ strMControl + " 05 03 06";
            }
            btnConFReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnConFReset.Enabled = true;
        }
        private void btnConRSDReset_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string strorder = "";
            strorder = "EB "+ strMControl + " 05 03 04";
            btnConRSDReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover(strorder), (int)OrderSendType.Total);
            NetCom3.Instance.SingleQuery();
            btnConRSDReset.Enabled = true;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            frmMessageShow frmMsgShow = new frmMessageShow();
            if (!NetCom3.totalOrderFlag)
            {
                frmMsgShow.MessageShow("仪器调试", "仪器正在运动，请稍等！");
                return;
            }
            string order = txtSingOrder.Text.ToString();
            btnSend.Enabled = false;
            if (order.Contains("EB 90 31 01"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), (int)OrderSendType.MoveNewTube);
                NetCom3.Instance.MoveQuery();
            }
            else if (order.Contains("EB 90 31 11"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), (int)OrderSendType.MoveTube);
                NetCom3.Instance.Move2Query();
            }
            else if (order.Contains("EB 90 31 02"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), (int)OrderSendType.AddSp);
                NetCom3.Instance.SPQuery();
            }
            else if (order.Contains("EB 90 31 12")|| order.Contains("EB 90 31 04")|| order.Contains("EB 90 31 14"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), (int)OrderSendType.AddSR);
                NetCom3.Instance.SP2Query();
            }
            else if (order.Contains("EB 90 31 03"))
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), (int)OrderSendType.Wash);
                NetCom3.Instance.WashQuery();
            }
            else
            {
                NetCom3.Instance.Send(NetCom3.Cover(order), (int)OrderSendType.Total);
                NetCom3.Instance.SingleQuery();
            }
            btnSend.Enabled = true;
        }
        #endregion


    }
}

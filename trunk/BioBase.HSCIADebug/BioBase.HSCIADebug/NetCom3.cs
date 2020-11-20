using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;
using Common;
using BioBaseCLIA.CalculateCurve;

namespace BioBase.HSCIADebug
{
    class NetCom3
    {
        #region 基础参数
        commands cmd = new commands();
        public static readonly NetCom3 Instance = new NetCom3();
        IPAddress ipAddress = IPAddress.Parse("192.168.1.146");
        //IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint remoteEP;
        /// <summary>
        /// 服务器端口号
        /// </summary>     
        private const int port = 8087;
        /// <summary>
        /// 创建客户端Socket
        /// </summary>
        Socket client;
        /// <summary>
        /// 返回的消息
        /// </summary>   
        private static String response = String.Empty;
        /// <summary>
        /// 创建锁
        /// </summary>
        static object locker = new object();
        /// <summary>
        ///指令接收数组
        /// </summary>
        public string[] ReciveData = new string[16];
        public string ErrorMessage = null;
        #endregion
        #region 线程、事件
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event Action<string> ReceiveHandel;
        public event Action<string> ReceiveHandelForQueryTemperatureAndLiquidLevel;
        /// <summary>
        /// 加样系统数据接收线程
        /// </summary>
        Thread SPReciveThread;
        /// <summary>
        /// 加样系统数据接收线程
        /// </summary>
        Thread SP2ReciveThread;
        /// <summary>
        /// 加新管系统
        /// </summary>
        Thread MOVEReciveThread;
        /// <summary>
        /// 加新管系统
        /// </summary>
        Thread MOVE2ReciveThread;
        /// <summary>
        /// 清洗系统
        /// </summary>
        Thread WASHReciveThread;
        /// <summary>
        /// 混匀内圈
        /// </summary>
        Thread MixReciveThread;
        /// <summary>
        /// 混匀外圈
        /// </summary>
        Thread Mix2ReciveThread;
        /// <summary>
        /// 调度-样本推送
        /// </summary>
        Thread SampleAdditionReciveThread;
        /// <summary>
        /// 轨道
        /// </summary>
        Thread TrackStripReciveThread;
        /// <summary>
        /// 回收链盘
        /// </summary>
        Thread CapstanStripReciveThread;
        /// <summary>
        /// 保持连接线程
        /// </summary>
        Thread KeepAliveThread;
        Thread thDataHandle = null;
        public event Action EventStop;//Stop事件
        #endregion

        #region 各模块信号变量
        /// <summary>
        /// 连接是否完成的信号实例
        /// </summary>  
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        /// <summary>
        /// 加样系统接收是否完成的信号实例
        /// </summary>  
        public static ManualResetEvent spreceiveDone = new ManualResetEvent(false);
        /// <summary>
        /// 加样系统接收是否完成的信号实例
        /// </summary>  
        public static ManualResetEvent sp2receiveDone = new ManualResetEvent(false);
        /// <summary>
        /// 加新管系统接收是否完成的信号实例
        /// </summary>  
        public static ManualResetEvent movereceiveDone = new ManualResetEvent(false);
        /// <summary>
        /// 移管系统接收是否完成的信号实例
        /// </summary>  
        public static ManualResetEvent move2receiveDone = new ManualResetEvent(false);
        /// <summary>
        /// 清洗系统接收是否完成的信号实例
        /// </summary>  
        public static ManualResetEvent washreceiveDone = new ManualResetEvent(false);
        /// <summary>
        /// 混匀内圈系统接收是否完成的信号实例
        /// </summary>  
        public static ManualResetEvent MixreceiveDone = new ManualResetEvent(false);
        /// <summary>
        /// 混匀外圈系统接收是否完成的信号实例
        /// </summary>  
        public static ManualResetEvent Mix2receiveDone = new ManualResetEvent(false);
        /// <summary>
        /// 调试系统接收是否完成的信号实例
        /// </summary>  
        public static ManualResetEvent DiagnostDone = new ManualResetEvent(false);
        /// <summary>
        /// 样本添加区接收是否完成的信号实例
        /// </summary>  
        public static ManualResetEvent SampleAdditionreceiveDone = new ManualResetEvent(false);
        /// <summary>
        /// 轨道系统接收是否完成的信号实例
        /// </summary>  
        public static ManualResetEvent TrackStripreceiveDone = new ManualResetEvent(false);
        /// <summary>
        /// 回收链盘系统接收是否完成的信号实例
        /// </summary>  
        public static ManualResetEvent CapstanStripreceiveDone = new ManualResetEvent(false);
        ///// <summary>
        ///// 温育盘是否被占用（避免加R2和混匀之间插入有关移管手有关需要温育盘的指令，导致温育盘被抢占，混匀错了反应管）
        ///// </summary>
        public static ManualResetEvent ComWait = new ManualResetEvent(true);
        //2018-09-04 zlx add
        public int FReciveCallBack = 0;
        #endregion
        #region 各模块标志位
        /// <summary>
        /// 测试是否连接服务器端
        /// </summary>
        public static bool isConnect = false;
        /// <summary>
        /// 加样指令发送标志位
        /// </summary>
        public static bool SpSendFlag = true;
        /// <summary>
        /// 加样指令发送标志位
        /// </summary>
        public static bool Sp2SendFlag = true;
        /// <summary>
        /// 加样指令接收标志位
        /// </summary>
        public static bool SpReciveFlag = true;
        /// <summary>
        /// 试剂指令接收标志位
        /// </summary>
        public static bool Sp2ReciveFlag = true;
        /// <summary>
        /// 清洗指令发送标志位
        /// </summary>
        public static bool WashSendFlag = true;
        /// <summary>
        /// 清洗指令接收标志位
        /// </summary>
        public static bool WashReciveFlag = true;
        /// <summary>
        /// 混匀指令发送标志位
        /// </summary>
        public static bool MixSendFlag = true;
        /// <summary>
        /// 混匀指令接收标志位
        /// </summary>
        public static bool MixReciveFlag = true;
        /// <summary>
        /// 混匀外圈指令发送标志位
        /// </summary>
        public static bool Mix2SendFlag = true;
        /// <summary>
        /// 轨道指令发送标志位
        /// </summary>
        public static bool TrackStripSendFlag = true;
        /// <summary>
        /// 回收指令发送标志位
        /// </summary>
        public static bool CapstanStripSendFlag = true;
        /// <summary>
        /// 混匀外圈指令接收标志位
        /// </summary>
        public static bool Mix2ReciveFlag = true;
        /// <summary>
        /// 轨道指令接收标志位
        /// </summary>
        public static bool TrackStripReciveFlag = true;
        /// <summary>
        /// 回收链盘指令接收标志位
        /// </summary>
        public static bool CapstanStripReciveFlag = true;
        /// <summary>
        /// 加新管指令发送标志位
        /// </summary>
        public static bool MoveSendFlag = true;
        /// <summary>
        /// 移管指令发送标志位
        /// </summary>
        public static bool Move2SendFlag = true;
        /// <summary>
        /// 移管指令接收标志位
        /// </summary>
        public static bool MoveReciveFlag = true;
        /// <summary>
        /// 移管指令接收标志位
        /// </summary>
        public static bool Move2ReciveFlag = true;
        /// <summary>
        /// 是否收到已经发送成功的指令
        /// </summary>
        public static bool totalOrderFlag = true;
        /// <summary>
        /// 是否收到已经发送成功的指令
        /// </summary>
        public static bool totalOrder2Flag = true;
        /// <summary>
        /// 样本添加区指令接收标志位
        /// </summary>
        public static bool SampleAdditionReciveFlag = true;
        /// <summary>
        /// 样本添加区指令发送标志位
        /// </summary>
        public static bool SampleAdditionSendFlag = true;
        /// <summary>
        /// 其他消息报错状态
        /// </summary>
        public int errorFlag = 0;
        /// <summary>
        /// 加新管消息报错状态
        /// </summary>
        public int MoverrorFlag = 0;
        /// <summary>
        /// 移管消息报错状态
        /// </summary>
        public int Move2rrorFlag = 0;
        /// <summary>
        /// 加样消息报错状态
        /// </summary>
        public int AdderrorFlag = 0;
        /// <summary>
        /// 加试剂消息报错状态
        /// </summary>
        public int Add2errorFlag = 0;
        /// <summary>
        /// 清洗消息报错状态
        /// </summary>
        public int WasherrorFlag = 0;
        /// <summary>
        /// 混匀消息报错状态
        /// </summary>
        public int MixerrorFlag = 0;
        /// <summary>
        /// 混匀消息报错状态
        /// </summary>
        public int Mix2errorFlag = 0;
        /// <summary>
        /// 轨道消息报错状态
        /// </summary>
        public int TrackStriperrorFlag = 0;
        /// <summary>
        /// 回收链条消息报错状态
        /// </summary>
        public int CapstanStriperrorFlag = 0;
        /// <summary>
        /// 保持连接指令
        /// </summary>
        public bool keepaliveFlag = false;
        /// <summary>
        /// 停止发送指令标志 
        /// </summary>
        public bool stopsendFlag = false;
        /// <summary>
        /// 混匀消息报错状态
        /// </summary>
        public int SampleAdditionerrorFlag = 0;
        /// <summary>
        /// IAP流程开始运行、不在循环发送温度缺管查询等指令
        /// </summary>
        public bool iapIsRun = false;
        /// <summary> 
        /// iap此次指令不需要返回
        /// </summary>
        public bool iapNoBack = false;
        #endregion

        public NetCom3()
        {
            //cleanReactionTray = new Thread(new ThreadStart(CleanAllReactTray)) { IsBackground = true };
            remoteEP = new IPEndPoint(ipAddress, port);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            KeepAliveThread = new Thread(new ParameterizedThreadStart(KeepAlive));
            KeepAliveThread.IsBackground = true;
            KeepAliveThread.Start();
            //frmMS = new frmMessageShow();
        }

        #region 服务器连接与判断
        /// <summary>
        /// 获得本机的IP地址
        /// </summary>
        /// <returns></returns>
        public string GetIP()
        {
            string hostNameOrIP = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostNameOrIP);
            string sIP = string.Empty;
            if (ipEntry.AddressList.Length > 0)
            {
                foreach (IPAddress addr in ipEntry.AddressList)
                {
                    if (addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        sIP = addr.ToString();
                        break;
                    }
                }
            }
            return sIP;
        }
        //检测有效的服务器
        public bool CheckNetWorkLink()
        {
            Ping p = new Ping();
            PingReply pr = p.Send(ipAddress);
            if (pr.Status != IPStatus.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 检查端口号是否开启
        /// </summary>
        /// <param name="myip">本机IP地址</param>
        /// <param name="myport">本机端口</param>
        /// <returns></returns>
        public bool CheckPort(string myip, string myport)
        {
            bool tcpListen = false;
            bool udpListen = false;//设定端口状态标识位
            System.Net.IPAddress myIpAddress = IPAddress.Parse(myip);
            System.Net.IPEndPoint myIpEndPoint = new IPEndPoint(myIpAddress, Convert.ToInt32(myport));
            try
            {
                System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient();
                tcpClient.Connect(myIpEndPoint);//对远程计算机的指定端口提出TCP连接请求搜索//////////////////***********标记：此处可能会引发多线程异步冲突
                tcpListen = true;
            }
            catch { }
            try
            {
                System.Net.Sockets.UdpClient udpClient = new UdpClient();
                udpClient.Connect(myIpEndPoint);//对远程计算机的指定端口提出UDP连接请求
                udpListen = true;
            }
            catch { }
            if (tcpListen == false && udpListen == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //联机检测
        public bool CheckMyIp_Port_Link()
        {
            string myip = GetIP();
            //string[] myiparray = myip.Split('.');
            if (!CheckNetWorkLink())
            {
                MessageBox.Show("网络连接不可用！", "温馨提示");
                return false;
            }
            if (!CheckPort(myip, "8087"))
            {
                MessageBox.Show("端口关闭！", "温馨提示");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 连接服务器方法
        /// </summary>
        public void ConnectServer()
        {
            try
            {
                if (!client.Connected)
                {
                    remoteEP = new IPEndPoint(ipAddress, port);
                    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                    if (connectDone.WaitOne(6000, false))
                    {
                        isConnect = true;
                        totalOrderFlag = true;
                    }
                    else
                    {
                        isConnect = false;
                        //frmMessageShow frmMS = new frmMessageShow();
                        //frmMS.MessageShow("", "无法连接到服务器！");
                        //frmMS.Dispose();
                        return;

                    }
                }
            }
            catch (Exception e)
            {
                isConnect = false;
                return;
            }
        }
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                //从状态对象检索套接字  
                Socket client = (Socket)ar.AsyncState;
                // 完成连接    
                client.EndConnect(ar);
                //连接成功
                isConnect = true;
                // 将连接信号置为终止状态  
                connectDone.Set();
            }
            catch (Exception e)
            {
                isConnect = false;
                // 将连接信号置为终止状态  
                connectDone.Set();
                return;
            }
        }
        /// <summary>
        /// 保持连接
        /// </summary>
        /// <param name="obj"></param>
        void KeepAlive(object obj)
        {
            while (true)
            {
                Thread.Sleep(15);
                if (isConnect)
                {
                    keepaliveFlag = false;
                    Thread.Sleep(20000);
                    if (SpSendFlag && SpReciveFlag && WashSendFlag && WashReciveFlag && MoveSendFlag && MoveReciveFlag && totalOrderFlag)
                    {
                        keepaliveFlag = true;
                        //NetCom3.Instance.Send(NetCom3.Cover("EB 90 F1 03"), 5);
                        //if (!NetCom3.Instance.SingleQuery())
                        //{
                        //    //ConnectServer();
                        //    keepaliveFlag = false;
                        //}
                    }
                }
                /*
                else
                {
                    if (NetCom3.Instance.CheckMyIp_Port_Link())
                    {
                        NetCom3.Instance.ConnectServer();
                    }
                    if (isConnect)
                        return;
                    else 
                    {
                        frmMessageShow frmMS = new frmMessageShow();
                        frmMS.MessageShow("连接服务器消息", "服务器断开重新连接失败！");
                        frmMS.Dispose();
                    }
                }
                 */
            }
        }
        #endregion
        #region 公共方法
        public static void Delay(int mm)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now)
            {
                Thread.Sleep(30);
                Application.DoEvents();
            }
            return;
        }
        /// <summary>
        /// 指令不足16位补位
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Cover(string s)
        {
            string order = s;
            string[] tempOrder = s.Split(' ');
            if (tempOrder.Length < 16)
            {
                for (int i = 0; i < 16 - tempOrder.Length; i++)
                {
                    order += " 00";
                }
            }
            return order;
        }

        /// <summary>
        /// 十六进制转为十进制
        /// </summary>
        /// <param name="reciveData"></param>
        /// <returns></returns>
        public static int[] converTo10(string[] reciveData)
        {
            int[] data = new int[16];
            for (int i = 0; i < 16; i++)
            {
                data[i] = System.Convert.ToInt32("0x" + reciveData[i], 16);
            }
            return data;
        }

        /// <summary>
        /// 单精度类型转为16进制 2018-07-04 zlx add
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FloatToHex(float f)
        {
            var b = BitConverter.GetBytes(f);
            string hex = BitConverter.ToString(b.Reverse().ToArray()).Replace("-", " ");
            //string hex = BitConverter.ToString(b.Reverse().ToArray()).Replace("-", "");
            return hex;
        }
        /// <summary>
        /// 16进制转为单精度类型 2018-07-04 zlx add
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static float HexToFloat(string hex)
        {
            uint num = uint.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);
            byte[] floatVals = BitConverter.GetBytes(num);
            float f = BitConverter.ToSingle(floatVals, 0);
            return f;
        }
        #endregion

        public Thread waitAndAgainSend;//实验的指令重发线程
        #region 发送方法
        /// <summary>
        /// 指令发送方法
        /// </summary>
        /// <param name="order">发送指令</param>
        /// <param name="orderType">
        /// 发送指令类型，加样（0）加试剂（10），
        /// 加新管（1）移管（11），清洗（2）,
        /// 仪器调试（5），样本调度区（15），
        /// </param>
        public static bool pflag = false;
        public void Send(String order, int orderType)
        {
            if (pflag && !order.Contains("90 31")) 
                return;
            while (stopsendFlag)
            {
                Delay(100);
                order = "";
            }
            if (order == "") return;
            JudgmentFlagBeforeSend(orderType);
            lock (this)
            {
                SendOrder(order, orderType);
            }
        }

        /// <summary>
        /// 发送命令前进行标志位修改
        /// </summary>
        private void JudgmentFlagBeforeSend(int orderType)
        {
            if (orderType == 0)//加样
            {
                while (!SpReciveFlag || !SpSendFlag)
                {
                    Delay(100);
                }
                SpSendFlag = false;
                SpReciveFlag = false;
                spreceiveDone.Reset();
                AdderrorFlag = (int)ErrorState.ReadySend;
            }
            if (orderType == 10)//加试剂
            {
                while (!Sp2ReciveFlag || !Sp2SendFlag)
                {
                    Delay(100);
                }
                Sp2SendFlag = false;
                Sp2ReciveFlag = false;
                sp2receiveDone.Reset();
                Add2errorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 1)//加新管
            {
                while (!MoveReciveFlag || !MoveSendFlag)
                {
                    Delay(100);
                }
                MoveSendFlag = false;
                MoveReciveFlag = false;
                movereceiveDone.Reset();
                MoverrorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 11)//移管
            {
                while (!Move2ReciveFlag || !Move2SendFlag)
                {
                    Delay(100);
                }
                Move2SendFlag = false;
                Move2ReciveFlag = false;
                move2receiveDone.Reset();
                Move2rrorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 2)//清洗
            {
                while (!WashReciveFlag || !WashSendFlag)
                {
                    Delay(100);
                }
                WashSendFlag = false;
                WashReciveFlag = false;
                washreceiveDone.Reset();
                WasherrorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 4)//混匀内圈
            {
                while (!MixReciveFlag || !MixSendFlag)
                {
                    Delay(100);
                }
                MixSendFlag = false;
                MixReciveFlag = false;
                MixreceiveDone.Reset();
                MixerrorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 14)//混匀外圈
            {
                while (!Mix2ReciveFlag || !Mix2SendFlag)
                {
                    Delay(100);
                }
                Mix2SendFlag = false;
                Mix2ReciveFlag = false;
                Mix2receiveDone.Reset();
                Mix2errorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 5)//调试
            {
                errorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 15)//调度-样本添加区
            {
                while (!SampleAdditionReciveFlag || !SampleAdditionSendFlag)
                {
                    Delay(100);
                }
                SampleAdditionSendFlag = false;
                SampleAdditionReciveFlag = false;
                SampleAdditionreceiveDone.Reset();
                SampleAdditionerrorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 6)//轨道
            {
                while (!TrackStripReciveFlag || !TrackStripSendFlag)
                {
                    Delay(100);
                }
                TrackStripSendFlag = false;
                TrackStripReciveFlag = false;
                TrackStripreceiveDone.Reset();
                TrackStriperrorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 7)//回收链盘-轨道
            {
                while (!CapstanStripReciveFlag || !CapstanStripSendFlag)
                {
                    Delay(100);
                }
                CapstanStripSendFlag = false;
                CapstanStripReciveFlag = false;
                CapstanStripreceiveDone.Reset();
                CapstanStriperrorFlag = (int)ErrorState.ReadySend;
            }
        }

        /// <summary>
        /// 重发线程
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private Thread GetResend(int orderType)
        {
            return new Thread(new ParameterizedThreadStart((object obj) =>
            {
                string waitOrder = obj as string;
                if (waitOrder == null || waitOrder == string.Empty) return;
                for (int i = 0; i < 18; i++)
                {
                    Thread.Sleep(100);
                }
                if (!totalOrderFlag)
                {
                    switch (orderType)
                    {
                        case 0:
                            if (AdderrorFlag == (int)ErrorState.ReadySend)
                            {
                                AdderrorFlag = (int)ErrorState.Sendfailure;
                                SpSendFlag = true;
                                SpReciveFlag = true;
                                totalOrderFlag = true;
                            }
                            break;
                        case 1://夹新管
                            if (MoverrorFlag == (int)ErrorState.ReadySend)
                            {
                                MoverrorFlag = (int)ErrorState.Sendfailure;
                                MoveSendFlag = true;
                                MoveReciveFlag = true;
                                totalOrderFlag = true;
                            }
                            break;
                        case 11://移管
                            if (Move2rrorFlag == (int)ErrorState.ReadySend)
                            {
                                Move2rrorFlag = (int)ErrorState.Sendfailure;
                                Move2SendFlag = true;
                                Move2ReciveFlag = true;
                                totalOrderFlag = true;
                            }
                            break;
                        case 2:
                            if (WasherrorFlag == (int)ErrorState.ReadySend)
                            {
                                WasherrorFlag = (int)ErrorState.Sendfailure;
                                WashSendFlag = true;
                                WashReciveFlag = true;
                                totalOrderFlag = true;
                            }
                            break;
                        case 4:
                            if (MixerrorFlag == (int)ErrorState.ReadySend)
                            {
                                MixerrorFlag = (int)ErrorState.Sendfailure;
                                MixSendFlag = true;
                                MixReciveFlag = true;
                                totalOrderFlag = true;
                            }
                            break;
                    }
                }
                //if (!totalOrderFlag)
                //{
                //    byte[] byteData2 = cmd.HexStringToByteArray(waitOrder);
                //    client.BeginSend(byteData2, 0, byteData2.Length, 0, new AsyncCallback(OtherSendCallback), client);
                //    LogFile.Instance.Write(string.Format("{0}->:{1}", DateTime.Now.ToString("HH:mm:ss:fff"), order));
                //}
            }));
        }

        /// <summary>
        /// 命令发送
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderType"></param>
        private void SendOrder(String order, int orderType)
        {
            try
            {
                byte[] byteData = cmd.HexStringToByteArray(order);
                while (!totalOrderFlag)
                {
                    Delay(100);
                }
                totalOrderFlag = false;
                //DiagnostDone.Reset();
                // 定时接收不到返回的接受指令进行指令重发⬇
                if (!order.Contains("EB 90"))
                {
                    AccordOrdertypeSetBeforfFlag(orderType);
                    return;
                }
                if (order.Contains("EB 90 31") || order.Contains("eb 90 31")
                    || order.Contains("Eb 90 31"))
                {
                    waitAndAgainSend = GetResend(orderType);
                    waitAndAgainSend.IsBackground = true;
                    waitAndAgainSend.Start(order);
                }
                LogFile.Instance.Write(string.Format("{0}->:{1}", DateTime.Now.ToString("HH:mm:ss:fff"), order));
                BeginSendOrder(client, byteData, orderType);
            }
            catch (Exception ex)
            {
                #region 这部分代码暂时先不删了
                LogFile.Instance.Write(DateTime.Now + "Send调用了EventStop");
                stopsendFlag = true;
                if (!totalOrderFlag)
                {
                    switch (orderType)
                    {
                        case 0:
                            if (AdderrorFlag == (int)ErrorState.ReadySend)
                            {
                                AdderrorFlag = (int)ErrorState.Sendfailure;
                                SpSendFlag = true;
                                SpReciveFlag = true;
                            }
                            break;
                        case 10:
                            if (Add2errorFlag == (int)ErrorState.ReadySend)
                            {
                                Add2errorFlag = (int)ErrorState.Sendfailure;
                                Sp2SendFlag = true;
                                Sp2ReciveFlag = true;
                            }
                            break;
                        case 1:
                            if (MoverrorFlag == (int)ErrorState.ReadySend)
                            {
                                MoverrorFlag = (int)ErrorState.Sendfailure;
                                MoveSendFlag = true;
                                MoveReciveFlag = true;
                            }
                            break;
                        case 11:
                            if (Move2rrorFlag == (int)ErrorState.ReadySend)
                            {
                                Move2rrorFlag = (int)ErrorState.Sendfailure;
                                Move2SendFlag = true;
                                Move2ReciveFlag = true;
                            }
                            break;
                        case 2:
                            if (WasherrorFlag == (int)ErrorState.ReadySend)
                            {
                                WasherrorFlag = (int)ErrorState.Sendfailure;
                                WashSendFlag = true;
                                WashReciveFlag = true;
                            }
                            break;
                        case 4:
                            if (MixerrorFlag == (int)ErrorState.ReadySend)
                            {
                                MixerrorFlag = (int)ErrorState.Sendfailure;
                                MixSendFlag = true;
                                MixReciveFlag = true;
                            }
                            break;
                        case 14:
                            if (Mix2errorFlag == (int)ErrorState.ReadySend)
                            {
                                Mix2errorFlag = (int)ErrorState.Sendfailure;
                                Mix2SendFlag = true;
                                Mix2ReciveFlag = true;
                            }
                            break;
                        case 5:
                            if (errorFlag == (int)ErrorState.ReadySend)
                            {
                                errorFlag = (int)ErrorState.Sendfailure;
                            }
                            break;
                        case 15:
                            if (SampleAdditionerrorFlag == (int)ErrorState.ReadySend)
                            {
                                SampleAdditionerrorFlag = (int)ErrorState.Sendfailure;
                                SampleAdditionSendFlag = true;
                                SampleAdditionReciveFlag = true;
                            }
                            break;
                        case 6:
                            if (TrackStriperrorFlag == (int)ErrorState.ReadySend)
                            {
                                TrackStriperrorFlag = (int)ErrorState.Sendfailure;
                                TrackStripSendFlag = true;
                                TrackStripReciveFlag = true;
                            }
                            break;
                        case 7:
                            if (CapstanStriperrorFlag == (int)ErrorState.ReadySend)
                            {
                                CapstanStriperrorFlag = (int)ErrorState.Sendfailure;
                                CapstanStripSendFlag = true;
                                CapstanStripReciveFlag = true;
                            }
                            break;
                        default:
                            if (errorFlag == (int)ErrorState.ReadySend)
                            {
                                errorFlag = (int)ErrorState.Sendfailure;
                            }
                            break;

                    }
                }
                totalOrderFlag = true;
                //MainWindow.LiquidQueryFlag = false;
                if (!keepaliveFlag)
                {
                    MessageBox.Show(ex.Message, "消息发送失败提示，Send" + orderType + "：");
                }
                #endregion 
            }
        }

        /// <summary>
        /// 根据指令类型设置准备发送标志位
        /// </summary>
        /// <param name="orderType"></param>
        public void AccordOrdertypeSetBeforfFlag(int orderType)
        {
            if (orderType == 0)
            {
                while (!SpReciveFlag || !SpSendFlag)
                {
                    Delay(100);
                }
                SpSendFlag = false;
                SpReciveFlag = false;
                spreceiveDone.Reset();
                AdderrorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 1)//夹新管
            {
                while (!MoveReciveFlag || !MoveSendFlag)
                {
                    Delay(100);
                }
                MoveSendFlag = false;
                MoveReciveFlag = false;
                movereceiveDone.Reset();
                Move2rrorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 11)//移管
            {
                while (!Move2ReciveFlag || !Move2SendFlag)
                {
                    Delay(100);
                }
                Move2SendFlag = false;
                Move2ReciveFlag = false;
                move2receiveDone.Reset();
                Move2rrorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 2)//清洗
            {
                while (!WashReciveFlag || !WashSendFlag)
                {
                    Delay(100);
                }
                WashSendFlag = false;
                WashReciveFlag = false;
                washreceiveDone.Reset();
                WasherrorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 4)//混匀内圈
            {
                while (!MixReciveFlag || !MixSendFlag)
                {
                    Delay(100);
                }
                MixSendFlag = false;
                MixReciveFlag = false;
                MixreceiveDone.Reset();
                MixerrorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 14)//混匀外圈
            {
                while (!Mix2ReciveFlag || !Mix2SendFlag)
                {
                    Delay(100);
                }
                Mix2SendFlag = false;
                Mix2ReciveFlag = false;
                Mix2receiveDone.Reset();
                Mix2errorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 5)//调试
            {
                //DiagnostNum = 16;
                errorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 15)//样本添加区
            {
                while (!SampleAdditionReciveFlag || !SampleAdditionSendFlag)
                {
                    Delay(100);
                }
                SampleAdditionSendFlag = false;
                SampleAdditionReciveFlag = false;
                SampleAdditionreceiveDone.Reset();
                SampleAdditionerrorFlag = (int)ErrorState.ReadySend;
            }
            else if (orderType == 6)//轨道
            {
                while (!TrackStripReciveFlag || !TrackStripSendFlag)
                {
                    Delay(100);
                }
                TrackStripSendFlag = false;
                TrackStripReciveFlag = false;
                TrackStripreceiveDone.Reset();
                TrackStriperrorFlag = (int)ErrorState.ReadySend;
            }
        }

        /// <summary>
        /// 开始异步发送指令
        /// </summary>
        /// <param name="client"></param>
        /// <param name="byteData"></param>
        /// <param name="orderType"></param>
        private void BeginSendOrder(Socket client, byte[] byteData, int orderType)
        {
            switch (orderType)
            {
                case 0://加样
                    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(AddArmSendCallback), client);
                    break;
                case 10://加试剂
                    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(Add2ArmSendCallback), client);
                    break;
                case 1://加新管
                    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(MoveSendCallback), client);
                    break;
                case 11://移管
                    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(Move2SendCallback), client);
                    break;
                case 2:
                    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(WashSendCallback), client);
                    break;
                case 4://混匀内圈
                    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(MixSendCallback), client);
                    break;
                case 14://混匀外圈
                    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(Mix2SendCallback), client);
                    break;
                case 5:
                    DiagnostDone.Reset();
                    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(DiagnostSendCallback), client);
                    break;
                case 15://样本添加区
                    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SampleAdditionSendCallback), client);
                    break;
                case 6://轨道
                    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(TrackStripSendCallback), client);
                    break;
                case 7://回收链盘
                    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(CapstanStripSendCallback), client);
                    break;
                default:
                    DiagnostDone.Reset();
                    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(OtherSendCallback), client);
                    break;
            }
        }
        /// <summary>
        /// 加样
        /// </summary>
        /// <param name="ar"></param>
        private void AddArmSendCallback(IAsyncResult ar)
        {
            try
            {
                // 从状态对象检索套接字。    
                Socket client = (Socket)ar.AsyncState;
                // 完成向下位机发送数据     
                int bytesSent = client.EndSend(ar);
                //SpOrderNum = 1;
                SpSendFlag = true;
                SPReciveThread = new Thread(new ParameterizedThreadStart(SPReciveMessage));
                SPReciveThread.IsBackground = true;
                SPReciveThread.Start();
            }
            catch (Exception e)
            {
                stopsendFlag = true;
                if (!totalOrderFlag && AdderrorFlag == (int)ErrorState.ReadySend)
                {
                    AdderrorFlag = (int)ErrorState.Sendfailure;
                }
                totalOrderFlag = true;
                SpSendFlag = true;
                SpReciveFlag = true;
                if (!keepaliveFlag)
                {
                    LogFile.Instance.Write(DateTime.Now + "AddArmSendCallback调用了EventStop");
                    if (EventStop != null)
                        EventStop();
                    MessageBox.Show("指令发送失败,AddArmSendCallback函数：" + e.Message, "温馨提示");
                }
            }
        }

        /// <summary>
        /// 加试剂
        /// </summary>
        /// <param name="ar"></param>
        private void Add2ArmSendCallback(IAsyncResult ar)
        {
            try
            {
                // 从状态对象检索套接字。    
                Socket client = (Socket)ar.AsyncState;
                // 完成向下位机发送数据     
                int bytesSent = client.EndSend(ar);
                //SpOrderNum = 1;
                Sp2SendFlag = true;
                SP2ReciveThread = new Thread(new ParameterizedThreadStart(SP2ReciveMessage));
                SP2ReciveThread.IsBackground = true;
                SP2ReciveThread.Start();
            }
            catch (Exception e)
            {
                stopsendFlag = true;
                if (!totalOrderFlag && AdderrorFlag == (int)ErrorState.ReadySend)
                {
                    AdderrorFlag = (int)ErrorState.Sendfailure;
                }
                totalOrderFlag = true;
                SpSendFlag = true;
                SpReciveFlag = true;
                if (!keepaliveFlag)
                {
                    LogFile.Instance.Write(DateTime.Now + "AddArmSendCallback调用了EventStop");
                    if (EventStop != null)
                        EventStop();
                    MessageBox.Show("指令发送失败,AddArmSendCallback函数：" + e.Message, "温馨提示");
                }
            }
        }
        /// <summary>
        /// 清洗
        /// </summary>
        /// <param name="ar"></param>
        private void WashSendCallback(IAsyncResult ar)
        {
            try
            {
                // 从状态对象检索套接字。    
                Socket client = (Socket)ar.AsyncState;
                // 完成向下位机发送数据     
                int bytesSent = client.EndSend(ar);
                //WashOrderNum = 1;
                WashSendFlag = true;
                WASHReciveThread = new Thread(new ParameterizedThreadStart(WASHReciveMessage));
                WASHReciveThread.IsBackground = true;
                WASHReciveThread.Start();
            }
            catch (Exception e)
            {
                stopsendFlag = true;
                if (!totalOrderFlag && WasherrorFlag == (int)ErrorState.ReadySend)
                    WasherrorFlag = (int)ErrorState.Sendfailure;
                totalOrderFlag = true;
                WashSendFlag = true;
                WashReciveFlag = true;
                if (!keepaliveFlag)
                {
                    LogFile.Instance.Write(DateTime.Now + "WashSendCallback调用了EventStop");
                    if (EventStop != null)
                        EventStop();
                    MessageBox.Show("指令发送失败，WashSendCallback：" + e.Message, "温馨提示");
                }
            }
        }

        /// <summary>
        /// 混匀
        /// </summary>
        /// <param name="ar"></param>
        private void MixSendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;// 从状态对象检索套接字。 
                int bytesSent = client.EndSend(ar);// 完成向下位机发送数据 
                MixSendFlag = true;
                MixReciveThread = new Thread(new ParameterizedThreadStart(MixReciveMessage));
                MixReciveThread.IsBackground = true;
                MixReciveThread.Start();
            }
            catch (Exception e)
            {
                stopsendFlag = true;
                if (!totalOrderFlag && MixerrorFlag == (int)ErrorState.ReadySend)
                    MixerrorFlag = (int)ErrorState.Sendfailure;
                totalOrderFlag = true;
                WashSendFlag = true;
                WashReciveFlag = true;
                if (!keepaliveFlag)
                {
                    LogFile.Instance.Write(DateTime.Now + "WashSendCallback调用了EventStop");
                    if (EventStop != null)
                        EventStop();
                    MessageBox.Show("指令发送失败，WashSendCallback：" + e.Message, "温馨提示");
                }
            }
        }
        /// <summary>
        /// 混匀外圈
        /// </summary>
        /// <param name="ar"></param>
        private void Mix2SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;// 从状态对象检索套接字。 
                int bytesSent = client.EndSend(ar);// 完成向下位机发送数据 
                Mix2SendFlag = true;
                Mix2ReciveThread = new Thread(new ParameterizedThreadStart(Mix2ReciveMessage));
                Mix2ReciveThread.IsBackground = true;
                Mix2ReciveThread.Start();
            }
            catch (Exception e)
            {
                stopsendFlag = true;
                if (!totalOrderFlag && MixerrorFlag == (int)ErrorState.ReadySend)
                    Mix2errorFlag = (int)ErrorState.Sendfailure;
                totalOrderFlag = true;
                WashSendFlag = true;
                WashReciveFlag = true;
                if (!keepaliveFlag)
                {
                    LogFile.Instance.Write(DateTime.Now + "WashSendCallback调用了EventStop");
                    if (EventStop != null)
                        EventStop();
                    MessageBox.Show("指令发送失败，WashSendCallback：" + e.Message, "温馨提示");
                }
            }
        }
        /// <summary>
        /// 加新管
        /// </summary>
        /// <param name="ar"></param>
        private void MoveSendCallback(IAsyncResult ar)
        {
            try
            {
                // 从状态对象检索套接字。    
                Socket client = (Socket)ar.AsyncState;
                // 完成向下位机发送数据     
                int bytesSent = client.EndSend(ar);
                //MoveOrderNum = 1;
                MoveSendFlag = true;
                MOVEReciveThread = new Thread(new ParameterizedThreadStart(MOVEReciveMessage));
                MOVEReciveThread.IsBackground = true;
                MOVEReciveThread.Start();
            }
            catch (Exception e)
            {
                stopsendFlag = true;
                if (!totalOrderFlag && MoverrorFlag == (int)ErrorState.ReadySend)
                    MoverrorFlag = (int)ErrorState.Sendfailure;
                totalOrderFlag = true;
                MoveSendFlag = true;
                MoveReciveFlag = true;
                if (!keepaliveFlag)
                {
                    LogFile.Instance.Write(DateTime.Now + "MoveSendCallback调用了EventStop");
                    if (EventStop != null)
                        EventStop();
                    MessageBox.Show("指令发送失败，MoveSendCallback：" + e.Message, "温馨提示");
                }
            }
        }

        /// <summary>
        /// 移管
        /// </summary>
        /// <param name="ar"></param>
        private void Move2SendCallback(IAsyncResult ar)
        {
            try
            {
                // 从状态对象检索套接字。    
                Socket client = (Socket)ar.AsyncState;
                // 完成向下位机发送数据     
                int bytesSent = client.EndSend(ar);
                //MoveOrderNum = 1;
                Move2SendFlag = true;
                MOVE2ReciveThread = new Thread(new ParameterizedThreadStart(MOVE2ReciveMessage));
                MOVE2ReciveThread.IsBackground = true;
                MOVE2ReciveThread.Start();
            }
            catch (Exception e)
            {
                stopsendFlag = true;
                if (!totalOrderFlag && Move2rrorFlag == (int)ErrorState.ReadySend)
                    Move2rrorFlag = (int)ErrorState.Sendfailure;
                totalOrderFlag = true;
                Move2SendFlag = true;
                Move2ReciveFlag = true;
                if (!keepaliveFlag)
                {
                    LogFile.Instance.Write(DateTime.Now + "Move2SendCallback调用了EventStop");
                    if (EventStop != null)
                        EventStop();
                    MessageBox.Show("指令发送失败，Move2SendCallback：" + e.Message, "温馨提示");
                }
            }
        }

        private void DiagnostSendCallback(IAsyncResult ar)
        {
            try
            {
                // 从状态对象检索套接字。    
                Socket client = (Socket)ar.AsyncState;
                // 完成向下位机发送数据     
                int bytesSent = client.EndSend(ar);
                try
                {
                    StateObject state = new StateObject();
                    state.workSocket = client;
                    // 开始从服务器接收数据
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    if (!DiagnostDone.WaitOne(30000, false))
                    {
                        LogFile.Instance.Write(DateTime.Now.ToString("hh:mm:ss:fff") + "DiagnostSendCallback调试指令接收出现异常！");
                        errorFlag = (int)ErrorState.OverTime;
                        totalOrderFlag = true;
                        MessageBox.Show("调试系统通讯故障！", "温馨提示");
                    }
                }
                catch (Exception ex)
                {
                    errorFlag = (int)ErrorState.Recivefailure;
                    if (!keepaliveFlag)
                    {
                        if (!ex.Message.Contains("正在中止线程"))
                        {
                            totalOrderFlag = true;
                            MessageBox.Show("DiagnostSendCallback:" + ex.Message, "温馨提示");
                        }
                    }

                }
            }
            catch (Exception e)
            {
                stopsendFlag = true;
                if (!totalOrderFlag)
                    errorFlag = (int)ErrorState.Sendfailure;
                totalOrderFlag = true;
                if (!keepaliveFlag)
                {
                    LogFile.Instance.Write(DateTime.Now + "DiagnostSendCallback调用了EventStop");
                    if (EventStop != null)
                        EventStop();
                    MessageBox.Show("DiagnostSendCallback指令发送失败：" + e.Message, "温馨提示");
                }
            }
        }
        private void OtherSendCallback(IAsyncResult ar)
        {
            try
            {
                //dw2018.12.24
                // 从状态对象检索套接字。    
                Socket client = (Socket)ar.AsyncState;
                // 完成向下位机发送数据     
                int bytesSent = client.EndSend(ar);
                //dw2018.12.24
                StateObject state = new StateObject();
                state.workSocket = client;
                // 开始从服务器接收数据
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                if (!DiagnostDone.WaitOne(60000, false))
                {
                    LogFile.Instance.Write(DateTime.Now.ToString("hh:mm:ss:fff") + "系统查询指令接收出现异常！");
                    FReciveCallBack++;
                    errorFlag = (int)ErrorState.OverTime;
                    totalOrderFlag = true;
                }
            }
            catch (Exception e)
            {
                errorFlag = (int)ErrorState.Recivefailure;
                totalOrderFlag = true;
                if (!keepaliveFlag)
                {
                    LogFile.Instance.Write(DateTime.Now + "OtherSendCallback调用了EventStop");
                    //    if (EventStop != null)
                    //        EventStop();
                    //    frmMessageShow frmMS = new frmMessageShow();
                    //    frmMS.MessageShow("", "指令发送失败：" + e.Message);
                    //    frmMS.Dispose();
                }
            }
        }

        /// <summary>
        /// 样本添加区
        /// </summary>
        /// <param name="ar"></param>
        private void SampleAdditionSendCallback(IAsyncResult ar)
        {
            try
            {
                // 从状态对象检索套接字。    
                Socket client = (Socket)ar.AsyncState;
                // 完成向下位机发送数据     
                int bytesSent = client.EndSend(ar);
                SampleAdditionSendFlag = true;
                SampleAdditionReciveThread = new Thread(new ParameterizedThreadStart(SampleAdditionReciveMessage));
                SampleAdditionReciveThread.IsBackground = true;
                SampleAdditionReciveThread.Start();
            }
            catch (Exception e)
            {

            }
        }
        /// <summary>
        /// 轨道
        /// </summary>
        /// <param name="ar"></param>
        private void TrackStripSendCallback(IAsyncResult ar)
        {
            try
            {
                // 从状态对象检索套接字。    
                Socket client = (Socket)ar.AsyncState;
                // 完成向下位机发送数据     
                int bytesSent = client.EndSend(ar);
                TrackStripSendFlag = true;
                TrackStripReciveThread = new Thread(new ParameterizedThreadStart(TrackStripReciveMessage));
                TrackStripReciveThread.IsBackground = true;
                TrackStripReciveThread.Start();
            }
            catch (Exception e)
            {

            }
        }
        /// <summary>
        /// 回收链盘
        /// </summary>
        /// <param name="ar"></param>
        private void CapstanStripSendCallback(IAsyncResult ar)
        {
            try
            {
                // 从状态对象检索套接字。    
                Socket client = (Socket)ar.AsyncState;
                // 完成向下位机发送数据     
                int bytesSent = client.EndSend(ar);
                CapstanStripSendFlag = true;
                CapstanStripReciveThread = new Thread(new ParameterizedThreadStart(CapstanStripReciveMessage));
                CapstanStripReciveThread.IsBackground = true;
                CapstanStripReciveThread.Start();
            }
            catch (Exception e)
            {

            }
        }
        #endregion

        #region 查询方法
        /// <summary>
        /// 加样系统方法查询
        /// </summary>
        /// <returns></returns>
        public bool SPQuery()
        {
            while (!SpReciveFlag)
                Delay(10);
            if (AdderrorFlag != (int)ErrorState.Success)
                return false;
            return true;
        }
        /// <summary>
        /// 加试剂系统方法查询
        /// </summary>
        /// <returns></returns>
        public bool SP2Query()
        {
            while (!Sp2ReciveFlag)
                Delay(10);
            if (Add2errorFlag != (int)ErrorState.Success)
                return false;
            return true;
        }
        /// <summary>
        /// 夹新管系统方法查询
        /// </summary>
        /// <returns></returns>
        public bool MoveQuery()
        {
            while (!MoveReciveFlag)
                Delay(10);
            if (MoverrorFlag != (int)ErrorState.Success)
                return false;
            return true;
        }
        /// <summary>
        /// 移管系统方法查询
        /// </summary>
        /// <returns></returns>
        public bool Move2Query()
        {
            while (!Move2ReciveFlag)
                Delay(10);
            if (Move2rrorFlag != (int)ErrorState.Success)
                return false;
            return true;
        }
        /// <summary>
        /// 清洗系统方法查询
        /// </summary>
        /// <returns></returns>
        public bool WashQuery()
        {
            while (!WashReciveFlag)
                Delay(10);
            if (WasherrorFlag != (int)ErrorState.Success)
                return false;
            return true;
        }
        /// <summary>
        /// 混匀内圈系统方法查询
        /// </summary>
        /// <returns></returns>
        public bool MixQuery()
        {
            while (!MixReciveFlag)
            {
                Delay(10);
            }
            if (MixerrorFlag != (int)ErrorState.Success)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 混匀系统方法查询
        /// </summary>
        /// <returns></returns>
        public bool Mix2Query()
        {
            while (!Mix2ReciveFlag)
            {
                Delay(10);
            }
            if (Mix2errorFlag != (int)ErrorState.Success)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 单模块系统方法查询
        /// </summary>
        /// <returns></returns>
        public bool SingleQuery()
        {
            while (!totalOrderFlag)
            {
                Delay(10);
            }
            if (errorFlag != (int)ErrorState.Success)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 样本添加区方法查询
        /// </summary>
        /// <returns></returns>
        public bool SampleAdditionQuery()
        {
            while (!SampleAdditionReciveFlag)
            {
                Delay(10);
            }
            if (SampleAdditionerrorFlag != (int)ErrorState.Success)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 轨道方法查询
        /// </summary>
        /// <returns></returns>
        public bool TrackStripQuery()
        {
            while (!TrackStripReciveFlag)
            {
                Delay(10);
            }
            if (TrackStriperrorFlag != (int)ErrorState.Success)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 轨道回收链盘方法查询
        /// </summary>
        /// <returns></returns>
        public bool CapstanStripQuery()
        {
            while (!CapstanStripReciveFlag)
            {
                Delay(10);
            }
            if (CapstanStriperrorFlag != (int)ErrorState.Success)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 接收方法
        /// <summary>
        /// 加样模块数据接收方法
        /// </summary>
        /// <param name="obj"></param>
        void SPReciveMessage(object obj)
        {
            Thread.Sleep(10);
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                // 开始从服务器接收数据
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                if (!spreceiveDone.WaitOne(100000, false))
                {
                    LogFile.Instance.Write(DateTime.Now + "加样系统接收数据超时");
                    AdderrorFlag = (int)ErrorState.OverTime;
                    SpReciveFlag = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                AdderrorFlag = (int)ErrorState.Recivefailure;
                SpReciveFlag = true;
                LogFile.Instance.Write(DateTime.Now + "SPReciveMessage:" + ex.Message);
                return;
            }
        }
        /// <summary>
        /// 加试剂模块数据接收方法
        /// </summary>
        /// <param name="obj"></param>
        void SP2ReciveMessage(object obj)
        {
            Thread.Sleep(10);
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                // 开始从服务器接收数据
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                if (!sp2receiveDone.WaitOne(100000, false))
                {
                    LogFile.Instance.Write(DateTime.Now + "加样系统接收数据超时");
                    Add2errorFlag = (int)ErrorState.OverTime;
                    Sp2ReciveFlag = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Add2errorFlag = (int)ErrorState.Recivefailure;
                Sp2ReciveFlag = true;
                LogFile.Instance.Write(DateTime.Now + "SPReciveMessage:" + ex.Message);
                return;
            }
        }
        /// <summary>
        /// 加新管模块数据接收方法
        /// </summary>
        /// <param name="obj"></param>
        void MOVEReciveMessage(object obj)
        {
            Thread.Sleep(10);
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                // 开始从服务器接收数据
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                if (!movereceiveDone.WaitOne(90000, false))
                {
                    LogFile.Instance.Write(DateTime.Now + "移管手通讯接收数据超时");
                    MoverrorFlag = (int)ErrorState.OverTime;
                    MoveReciveFlag = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                MoverrorFlag = (int)ErrorState.Recivefailure;
                MoveReciveFlag = true;
                LogFile.Instance.Write(DateTime.Now + "MOVEReciveMessage:" + ex.Message);
                return;
            }
        }
        /// <summary>
        /// 移管模块数据接收方法
        /// </summary>
        /// <param name="obj"></param>
        void MOVE2ReciveMessage(object obj)
        {
            Thread.Sleep(10);
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                // 开始从服务器接收数据
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                if (!move2receiveDone.WaitOne(100000, false))
                {
                    LogFile.Instance.Write(DateTime.Now + "移管手通讯接收数据超时");
                    Move2rrorFlag = (int)ErrorState.OverTime;
                    Move2ReciveFlag = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Move2rrorFlag = (int)ErrorState.Recivefailure;
                Move2ReciveFlag = true;
                LogFile.Instance.Write(DateTime.Now + "MOVE2ReciveMessage:" + ex.Message);
                return;
            }
        }
        /// <summary>
        /// 清洗模块数据接收方法
        /// </summary>
        /// <param name="obj"></param>
        void WASHReciveMessage(object obj)
        {
            Thread.Sleep(10);
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                // 开始从服务器接收数据
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                if (!washreceiveDone.WaitOne(100000, false))
                {
                    WasherrorFlag = (int)ErrorState.OverTime;
                    WashReciveFlag = true;
                    LogFile.Instance.Write(DateTime.Now + "清洗系统接收数据超时");
                    return;
                }
            }
            catch (Exception ex)
            {
                WasherrorFlag = (int)ErrorState.Recivefailure;
                WashReciveFlag = true;
                LogFile.Instance.Write(DateTime.Now + "WASHReciveMessage:" + ex.Message);
                return;
            }
        }
        /// <summary>
        /// 清洗模块数据接收方法
        /// </summary>
        /// <param name="obj"></param>
        void MixReciveMessage(object obj)
        {
            Thread.Sleep(10);
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                // 开始从服务器接收数据
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                if (!MixreceiveDone.WaitOne(100000, false))
                {
                    MixerrorFlag = (int)ErrorState.OverTime;
                    MixReciveFlag = true;
                    LogFile.Instance.Write(DateTime.Now + "混匀指令接收超时");
                    return;
                }
            }
            catch (Exception ex)
            {
                MixerrorFlag = (int)ErrorState.Recivefailure;
                MixReciveFlag = true;
                LogFile.Instance.Write(DateTime.Now + "WASHReciveMessage:" + ex.Message);
                return;
            }
        }
        /// <summary>
        /// 清洗模块数据接收方法
        /// </summary>
        /// <param name="obj"></param>
        void Mix2ReciveMessage(object obj)
        {
            Thread.Sleep(10);
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                // 开始从服务器接收数据
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                if (!Mix2receiveDone.WaitOne(100000, false))
                {
                    Mix2errorFlag = (int)ErrorState.OverTime;
                    Mix2ReciveFlag = true;
                    LogFile.Instance.Write(DateTime.Now + "混匀2指令接收超时");
                    return;
                }
            }
            catch (Exception ex)
            {
                Mix2errorFlag = (int)ErrorState.Recivefailure;
                Mix2ReciveFlag = true;
                LogFile.Instance.Write(DateTime.Now + "WASHReciveMessage:" + ex.Message);
                return;
            }
        }
        /// <summary>
        /// 加样模块数据接收方法
        /// </summary>
        /// <param name="obj"></param>
        void SampleAdditionReciveMessage(object obj)
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                // 开始从服务器接收数据
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                if (!SampleAdditionreceiveDone.WaitOne(10000, false))
                {
                    LogFile.Instance.Write(DateTime.Now + "样本添加接收数据超时");
                    SampleAdditionerrorFlag = (int)ErrorState.OverTime;
                    SampleAdditionReciveFlag = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                SampleAdditionerrorFlag = (int)ErrorState.Recivefailure;
                SampleAdditionReciveFlag = true;
                LogFile.Instance.Write(DateTime.Now + "SampleAdditionReciveMessage:" + ex.Message);
                return;
            }
        }
        /// <summary>
        /// 加样模块数据接收方法
        /// </summary>
        /// <param name="obj"></param>
        void TrackStripReciveMessage(object obj)
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                // 开始从服务器接收数据
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                if (!TrackStripreceiveDone.WaitOne(10000, false))
                {
                    LogFile.Instance.Write(DateTime.Now + "样本添加接收数据超时");
                    TrackStriperrorFlag = (int)ErrorState.OverTime;
                    TrackStripReciveFlag = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                TrackStriperrorFlag = (int)ErrorState.Recivefailure;
                TrackStripReciveFlag = true;
                LogFile.Instance.Write(DateTime.Now + "SampleAdditionReciveMessage:" + ex.Message);
                return;
            }
        }
        /// <summary>
        /// 回收链盘模块数据接收方法
        /// </summary>
        /// <param name="obj"></param>
        void CapstanStripReciveMessage(object obj)
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                // 开始从服务器接收数据
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                if (!TrackStripreceiveDone.WaitOne(10000, false))
                {
                    LogFile.Instance.Write(DateTime.Now + "样本添加接收数据超时");
                    CapstanStriperrorFlag = (int)ErrorState.OverTime;
                    CapstanStripReciveFlag = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                CapstanStriperrorFlag = (int)ErrorState.Recivefailure;
                CapstanStripReciveFlag = true;
                LogFile.Instance.Write(DateTime.Now + "SampleAdditionReciveMessage:" + ex.Message);
                return;
            }
        }
        byte WhereToReceive = 1;
        /// <summary>
        /// 接收指令判断
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            lock (locker)
            {
                if (!isConnect)
                {
                    MessageBox.Show("ReceiveCallback isConnect 断开连接！");
                    LogFile.Instance.Write(string.Format("{0}<-:{1}", DateTime.Now.ToString("HH:mm:ss:fff"), "isConnect退出"));
                    return;
                }
                StateObject state = (StateObject)ar.AsyncState;
                try
                {
                    if ((state.workSocket == null) || (!state.workSocket.Connected))
                    {
                        MessageBox.Show("ReceiveCallback state.workSocket 断开连接！");
                        LogFile.Instance.Write(string.Format("{0}<-:{1}", DateTime.Now.ToString("HH:mm:ss:fff"), "state.workSocket为空退出"));
                        return;
                    }
                    Socket client = state.workSocket;
                    // 读取下位机返回的字节数 
                    int bytesRead = 0;
                    bytesRead = client.EndReceive(ar);
                    if (bytesRead > 0)
                    {
                        Array.Resize(ref state.buffer, bytesRead);
                        state.sb.Append(cmd.ByteArrayToHexString(state.buffer));
                        response = response + state.sb;
                        ReciveData = new string[state.sb.Length / 48];
                        for (int i = 0; i < state.sb.Length / 48; i++)
                            ReciveData[i] = response.Substring(i * 48, 48);
                        foreach (var item in ReciveData)
                        {
                            LogFile.Instance.Write(string.Format("{0}<-:{1}", DateTime.Now.ToString("HH:mm:ss:fff"), item.ToString()));
                        }
                        response = string.Empty;
                        foreach (string tempResponse in ReciveData)
                        {
                            WhereToReceive = 1;
                            if (tempResponse.ToString().IndexOf("EB 90") > -1)
                            {
                                RunResultHandle(tempResponse);
                                DataHandle(WhereToReceive, tempResponse);
                                string orderTemp = tempResponse.Substring(tempResponse.ToString().IndexOf("EB 90") + 6, 5);
                                if (orderTemp == "00 00"|| orderTemp == "10 00")
                                {
                                    state.sb.Remove(0, state.sb.Length);
                                    state.buffer = new byte[StateObject.BufferSize];
                                    if (client.Connected)
                                    {
                                        // 获取其余的数据  
                                        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                            new AsyncCallback(ReceiveCallback), state);
                                    }
                                    LogFile.Instance.Write(DateTime.Now + ":0");
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogFile.Instance.Write(DateTime.Now + "ReceiveCallback调用了EventStop,异常：" + e.Message);
                    stopsendFlag = true;
                    if (waitAndAgainSend is Thread && waitAndAgainSend != null)
                    {
                        totalOrderFlag = true;
                        waitAndAgainSend.Abort();
                    }
                    if (state.sb.Length > 0)
                    {
                        state.sb.Remove(0, state.sb.Length);
                        state.buffer = new byte[StateObject.BufferSize];
                        if (client.Connected)
                        {
                            //获取其余的数据  
                            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                        }
                    }
                    close();
                }
            }
        }

        /// <summary>
        /// 根据接收数据判断初始化标志
        /// </summary>
        /// <param name="tempResponse"></param>
        private void JudgementInitFlagAfterSend(string tempResponse)
        {
            int tempInt;
            //发送的指令EB 90 F1 02，下位机返回的指令中是EB 90 F1 02 FF FF FF FF，需要进行修改具体每一位
            //待修改-现在下位机初始化返回的数据还是EB 90 F1 02 FF打头，在下位机初始化返回数据修改后也就是返回数据以EB 90 F1 02 00打头，就可以进行模块光电信号开关的检测
            //此部分为光电信号的检测，把所有检测到未开的开关输出到一个弹框内
            if (tempResponse.Contains("EB 90 F1 02 FF"))
            {
                tempInt = tempResponse.IndexOf("EB 90 F1 02 FF ");
            }
            else
            {
                tempInt = tempResponse.IndexOf("EB 90 F1 02 00 ");
            }
            //初始化检测II：加样模块光电信号
            string tempII = tempResponse.Substring(tempInt + 15, 2);
            //判断II位为1F
            ErrorMessage = null;
            if (tempII != "1F")
            {
                Byte bit = Convert.ToByte(tempII, 16);
                tempII = Convert.ToString(bit, 2);
                while (tempII.Length < 8)
                {
                    tempII = "0" + tempII;
                }
                if (tempII[7] != '1')
                {
                    ErrorMessage = ErrorMessage + "加样模块样品盘光电开关异常!\n";
                }
                if (tempII[6] != '1')
                {
                    ErrorMessage = ErrorMessage + "加样模块试剂盘光电开关异常!\n";
                }
                if (tempII[5] != '1')
                {
                    ErrorMessage = ErrorMessage + "加样模块垂直臂光电开关异常!\n";
                }
                if (tempII[4] != '1')
                {
                    ErrorMessage = ErrorMessage + "加样模块旋转臂光电开关异常!\n";
                }
                if (tempII[3] != '1')
                {
                    ErrorMessage = ErrorMessage + "加样模块柱塞泵光电开关异常!\n";
                }
            }
            //初始化检测JJ：抓手模块光电信号
            string tempJJ = tempResponse.Substring(tempInt + 18, 2);
            //判断JJ位为FF
            if (tempJJ != "3F")
            {
                Byte bit = Convert.ToByte(tempJJ, 16);
                tempJJ = Convert.ToString(bit, 2);
                while (tempJJ.Length < 8)
                {
                    tempJJ = "0" + tempJJ;
                }
                if (tempJJ[7] != '1')
                {
                    ErrorMessage = ErrorMessage + "理杯机模块理杯机光电开关异常!\n";
                }
                if (tempJJ[6] != '1')
                {
                    ErrorMessage = ErrorMessage + "理杯机模块暂存盘光电开关异常!\n";
                }
                if (tempJJ[5] != '1')
                {
                    ErrorMessage = ErrorMessage + "理杯机模块垂直光电开关异常!\n";
                }
                if (tempJJ[4] != '1')
                {
                    ErrorMessage = ErrorMessage + "理杯机模块旋转光电开关异常!\n";
                }
                if (tempJJ[3] != '1')
                {
                    ErrorMessage = ErrorMessage + "理杯机模块抓手光电开关异常!\n";
                }
                if (tempJJ[2] != '1')
                {
                    ErrorMessage = ErrorMessage + "理杯机模块抓空光电开关异常\n";
                }
            }
            //初始化检测KK：清洗模块光电信号
            string tempKK = tempResponse.Substring(tempInt + 21, 2);
            if (tempKK != "F")
            {
                Byte bit = Convert.ToByte(tempKK, 16);
                tempKK = Convert.ToString(bit, 2);
                while (tempKK.Length < 8)
                {
                    tempKK = "0" + tempKK;
                }
                if (tempKK[7] != '1')
                {
                    ErrorMessage = ErrorMessage + "清洗模块清洗盘光电异常!\n";
                }
                if (tempKK[6] != '1')
                {
                    ErrorMessage = ErrorMessage + "清洗模块压杯光电异常!\n";
                }
                if (tempKK[5] != '1')
                {
                    ErrorMessage = ErrorMessage + "清洗模块垂直光电异常!\n";
                }
                if (tempKK[4] != '1')
                {
                    ErrorMessage = ErrorMessage + "清洗模块计量泵光耦异常!\n";
                }
            }
            //初始化检测LL：温育盘模块光电信号
            string tempMM = tempResponse.Substring(tempInt + 24, 2);
            if (tempMM != "7")
            {
                Byte bit = Convert.ToByte(tempMM, 16);
                tempMM = Convert.ToString(bit, 2);
                while (tempMM.Length < 8)
                {
                    tempMM = "0" + tempMM;
                }
                if (tempMM[7] != '1')
                {
                    ErrorMessage = ErrorMessage + "温育盘模块温育盘光电开关异常!\n";
                }
                if (tempMM[6] != '1')
                {
                    ErrorMessage = ErrorMessage + "温育盘模块垂直光电开关异常!\n";
                }
                if (tempMM[5] != '1')
                {
                    ErrorMessage = ErrorMessage + "温育盘模块压杯光电开关异常!\n";
                }
            }
            //初始化检测NN：加试剂模块光电信号
            string tempNN = tempResponse.Substring(tempInt + 24, 2);
            if (tempMM != "###")//根据下位机具体确定，他们需要商量一下
            {
                Byte bit = Convert.ToByte(tempNN, 16);
                tempNN = Convert.ToString(bit, 2);
                while (tempNN.Length < 8)
                {
                    tempNN = "0" + tempNN;
                }
                if (tempNN[7] != '1')
                {
                    //ErrorMessage = ErrorMessage + "\n";
                }
                if (tempNN[6] != '1')
                {
                    ErrorMessage = ErrorMessage + "加试剂模块试剂盘模块垂直光电开关异常!\n";
                }
                if (tempNN[5] != '1')
                {
                    ErrorMessage = ErrorMessage + "加试剂模块垂直臂模块压杯光电开关异常!\n";
                }
                if (tempNN[4] != '1')
                {
                    ErrorMessage = ErrorMessage + "加试剂模块旋转臂模块压杯光电开关异常!\n";
                }
                if (tempNN[3] != '1')
                {
                    ErrorMessage = ErrorMessage + "加试剂模块柱塞泵模块压杯光电开关异常!\n";
                }
            }
            //初始化检测OO：移管模块光电信号
            string tempOO = tempResponse.Substring(tempInt + 24, 2);
            if (tempOO != "###")//根据下位机具体确定，他们需要商量一下
            {
                Byte bit = Convert.ToByte(tempOO, 16);
                tempOO = Convert.ToString(bit, 2);
                while (tempOO.Length < 8)
                {
                    tempOO = "0" + tempOO;
                }
                if (tempOO[7] != '1')
                {
                    //ErrorMessage = ErrorMessage + "\n";
                }
                if (tempOO[6] != '1')
                {
                    ErrorMessage = ErrorMessage + "移管模块垂直光电开关异常!\n";
                }
                if (tempOO[5] != '1')
                {
                    ErrorMessage = ErrorMessage + "移管模块旋转光电开关异常!\n";
                }
                if (tempOO[4] != '1')
                {
                    ErrorMessage = ErrorMessage + "移管模块抓手光电开关异常!\n";
                }
                if (tempOO[3] != '1')
                {
                    ErrorMessage = ErrorMessage + "移管模块抓空光电开关异常!\n";
                }
            }
        }

        #region 运行结果处理
        /// <summary>
        /// 加新管结果处理
        /// </summary>
        /// <param name="tempResponse"></param>
        private void MoveResultHandle(string tempResponse)
        {
            int tempInt = tempResponse.IndexOf("EB 90 31 A1 ");
            string temp = tempResponse.Substring(tempInt + 12, 2);
            Byte bit = Convert.ToByte(temp, 16);
            if (bit != Byte.MaxValue)
            {
                temp = Convert.ToString(bit, 2);
                while (temp.Length < 8)
                {
                    temp = "0" + temp;
                }
                if (temp.Substring(0, 1) == "0")//为空
                {
                    MoverrorFlag = (int)ErrorState.IsNull;
                }
                if (temp.Substring(1, 1) == "0")//取管撞管
                {
                    MoverrorFlag = (int)ErrorState.IsKnocked;
                }
                if (temp.Substring(2, 1) == "0")//放管撞管
                {
                    MoverrorFlag = (int)ErrorState.putKnocked;
                }
                if (temp.Substring(4, 1) == "0")//理杯机缺管
                {
                    MoverrorFlag = (int)ErrorState.LackTube;
                }
            }
            else
            {
                MoverrorFlag = (int)ErrorState.Success;//成功
            }
            MoveReciveFlag = true;
            movereceiveDone.Set();
        }
        /// <summary>
        /// 移管结果处理
        /// </summary>
        /// <param name="tempResponse"></param>
        private void Move2ResultHandle(string tempResponse)
        {
            int tempInt = tempResponse.IndexOf("EB 90 31 B1 ");
            string temp = tempResponse.Substring(tempInt + 12, 2);
            Byte bit = Convert.ToByte(temp, 16);
            if (bit != Byte.MaxValue)
            {
                temp = Convert.ToString(bit, 2);
                while (temp.Length < 8)
                {
                    temp = "0" + temp;
                }
                if (temp.Substring(0, 1) == "0")//为空
                {
                    Move2rrorFlag = (int)ErrorState.IsNull;
                }
                if (temp.Substring(1, 1) == "0")//取管撞管
                {
                    Move2rrorFlag = (int)ErrorState.IsKnocked;
                }
                if (temp.Substring(2, 1) == "0")//放管撞管
                {
                    Move2rrorFlag = (int)ErrorState.putKnocked;
                }
                if (temp.Substring(4, 1) == "0")//理杯机缺管
                {
                    Move2rrorFlag = (int)ErrorState.LackTube;
                }
            }
            else
            {
                Move2rrorFlag = (int)ErrorState.Success;//成功
            }
            Move2ReciveFlag = true;
            move2receiveDone.Set();
        }
        /// <summary>
        /// 加样结果处理
        /// </summary>
        /// <param name="tempResponse"></param>
        private void SpResultHadle(string tempResponse)
        {
            int tempInt = tempResponse.IndexOf("EB 90 31 A2 ");
            string temp = tempResponse.Substring(tempInt + 12, 2);
            if (temp == "7F")//加液针撞针
            {
                AdderrorFlag = (int)ErrorState.IsKnocked;
            }
            else
            {
                AdderrorFlag = (int)ErrorState.Success;
            }
            SpReciveFlag = true;
            spreceiveDone.Set();
        }
        /// <summary>
        /// 加试剂结果处理
        /// </summary>
        /// <param name="tempResponse"></param>
        private void Sp2ResultHadle(string tempResponse)
        {
            int tempInt = tempResponse.IndexOf("EB 90 31 B2 ");
            string temp = tempResponse.Substring(tempInt + 12, 2);
            if (temp == "7F")//加液针撞针
            {
                Add2errorFlag = (int)ErrorState.IsKnocked;
            }
            else
            {
                Add2errorFlag = (int)ErrorState.Success;
            }
            Sp2ReciveFlag = true;
            sp2receiveDone.Set();
        }
        /// <summary>
        /// 混匀结果处理
        /// </summary>
        /// <param name="tempResponse"></param>
        private void MixResultHadle(string tempResponse)
        {
            string orderTemp = tempResponse.Substring(tempResponse.ToString().IndexOf("EB 90") + 6, 5);
            switch (orderTemp)
            {
                case "31 A4":
                    MixerrorFlag = (int)ErrorState.Success;//成功
                    MixReciveFlag = true;
                    MixreceiveDone.Set();
                    break;
                case "31 B4":
                    Mix2errorFlag = (int)ErrorState.Success;//成功
                    Mix2ReciveFlag = true;
                    Mix2receiveDone.Set();
                    break;
            }
        }
        /// <summary>
        /// 样本添加结果处理
        /// </summary>
        /// <param name="tempResponse"></param>
        private void SampleAdditionResultHadle(string tempResponse)
        {
            int tempInt = tempResponse.IndexOf("EB 90 31 A5 ");
            string temp = tempResponse.Substring(tempInt + 12, 2);
            if (temp == "7F")
            {
                SampleAdditionerrorFlag = (int)ErrorState.IsKnocked;
            }
            else
            {
                SampleAdditionerrorFlag = (int)ErrorState.Success;
            }
            SampleAdditionReciveFlag = true;
            SampleAdditionreceiveDone.Set();
        }
        /// <summary>
        /// 轨道结果处理
        /// </summary>
        /// <param name="tempResponse"></param>
        private void TrackStripResultHadle(string tempResponse)
        {
            int tempInt = tempResponse.IndexOf("EB 90 31 A6 ");
            string temp = tempResponse.Substring(tempInt + 12, 2);
            if (temp == "7F")
            {
                TrackStriperrorFlag = (int)ErrorState.IsKnocked;
            }
            else
            {
                TrackStriperrorFlag = (int)ErrorState.Success;
            }
            TrackStripReciveFlag = true;
            TrackStripreceiveDone.Set();
        }
        /// <summary>
        /// 回收链盘结果处理
        /// </summary>
        /// <param name="tempResponse"></param>
        private void CapstanStripResultHadle(string tempResponse)
        {
            int tempInt = tempResponse.IndexOf("EB 90 31 A7 ");
            string temp = tempResponse.Substring(tempInt + 12, 2);
            if (temp == "7F")
            {
                CapstanStriperrorFlag = (int)ErrorState.IsKnocked;
            }
            else
            {
                CapstanStriperrorFlag = (int)ErrorState.Success;
            }
            CapstanStripReciveFlag = true;
            CapstanStripreceiveDone.Set();
        }
        private void MachineGetMessage(string tempResponse)
        {
            string orderTemp = tempResponse.Substring(tempResponse.ToString().IndexOf("EB 90") + 6, 5);
            switch (orderTemp)
            {
                case "00 00":
                    if (waitAndAgainSend is Thread && waitAndAgainSend != null)
                    {
                        totalOrderFlag = true;
                        waitAndAgainSend.Abort();
                    }
                    break;
                case "10 00":
                    if (waitAndAgainSend is Thread && waitAndAgainSend != null)
                    {
                        totalOrderFlag = true;
                        waitAndAgainSend.Abort();
                    }
                    break;
            }
        }
        /// <summary>
        /// 实验运行结果处理
        /// </summary>
        /// <param name="tempResponse"></param>
        private void RunResultHandle(string tempResponse)
        {
            string orderTemp = tempResponse.Substring(tempResponse.ToString().IndexOf("EB 90") + 6, 5);
            if (orderTemp == "11 FF" || orderTemp == "11 AF" || orderTemp == "01 A0" || orderTemp == "11 A0" //"11 FF"版本号返回指令//仪器调教指令处理//仪器调试收到查询温度
                || orderTemp == "A1 03" || orderTemp == "F1 01" || orderTemp == "F1 02" || orderTemp == "F1 03")//心跳包上下位机握手动作完毕//仪器初始化完毕   y modify 20180802  zlx mod 2018-08-16
            {
                #region 初始化检测模块光电信号开关
                if (orderTemp == "F1 02")
                {
                    JudgementInitFlagAfterSend(tempResponse);
                }
                #endregion
                LogFile.Instance.Write(string.Format("{0}<-:{1}", DateTime.Now.ToString("HH:mm:ss:fff"), "DiagnostDone.Set释放信号之前"));
                totalOrderFlag = true;
                errorFlag = (int)ErrorState.Success;
                DiagnostDone.Set();
                if (orderTemp == "11 AF") WhereToReceive = 2;
            }
            else if (orderTemp == "00 00" || orderTemp == "10 00")//下位机收到上位机指令
                MachineGetMessage(tempResponse);
            else if (orderTemp == "31 A1")//加新管模块动作执行完毕
                MoveResultHandle(tempResponse);
            else if (orderTemp == "31 B1")//移管模块动作执行完毕
                Move2ResultHandle(tempResponse);
            else if (orderTemp == "31 A2")//加样系统动作执行完毕
                SpResultHadle(tempResponse);
            else if (orderTemp == "31 B2")//加试剂系统动作执行完毕
                Sp2ResultHadle(tempResponse);
            else if (orderTemp == "31 A4")//混匀内圈完成
                MixResultHadle(tempResponse);
            else if (orderTemp == "31 B4")//混匀外圈完成
                MixResultHadle(tempResponse);
            else if (orderTemp == "31 A3")//清洗系统动作执行完毕
            {
                WasherrorFlag = (int)ErrorState.Success;
                WashReciveFlag = true;
                washreceiveDone.Set();
            }
            else if (orderTemp == "31 A5")//样本添加区
                SampleAdditionResultHadle(tempResponse);
            else if (orderTemp == "31 A6")//轨道
                TrackStripResultHadle(tempResponse);
            else if (orderTemp == "31 A7")//回收链盘
                CapstanStripResultHadle(tempResponse);
        }
        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="WhereToReceive"></param>
        /// <param name="tempResponse"></param>
        private void DataHandle(byte WhereToReceive, string tempResponse)
        {
            switch (WhereToReceive)
            {
                case 1:
                    thDataHandle = new Thread(new ParameterizedThreadStart(HandleMessage));
                    thDataHandle.IsBackground = true;
                    thDataHandle.Start(tempResponse);
                    break;
                case 2:
                    thDataHandle = new Thread(new ParameterizedThreadStart(HandleMessageForTemperatureAndLiquidLevel));
                    thDataHandle.IsBackground = true;
                    thDataHandle.Start(tempResponse);
                    break;
            }
        }
        #endregion

        /// <summary>
        /// 根据接收数据判断运行标志
        /// </summary>
        /// <param name="tempResponse"></param>
        private void JudgementRunFlagAfterSend(string tempResponse)
        {

        }
        object lockHand = new object();
        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        private void HandleMessage(object message)
        {
            lock (lockHand)
            {
                //将接收的数据传到相应的界面
                if (ReceiveHandel != null)
                    foreach (Delegate dele in ReceiveHandel.GetInvocationList())
                        try
                        {
                            ((Action<string>)dele).BeginInvoke(message.ToString(), null, null);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "温馨提示");
                            break;
                        }
            }
        }
        object locker1 = new object();
        private void HandleMessageForTemperatureAndLiquidLevel(object message)
        {
            lock (locker1)
            {
                if (ReceiveHandelForQueryTemperatureAndLiquidLevel != null)
                {
                    foreach (Delegate item in ReceiveHandelForQueryTemperatureAndLiquidLevel.GetInvocationList())
                    {
                        ((Action<string>)item).BeginInvoke(message.ToString(), null, null);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if (client != null)
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                isConnect = false;
            }
        }
        /// <summary>
        /// 断开连接停止相应线程
        /// </summary>
        public void close()
        {
            try
            {

            }
            catch
            {

            }

            if (client != null)
            {
                DisConnect();
                isConnect = false;
            }
        }
        /// <summary>
        /// IAP重连
        /// </summary>
        public void iapConnectTwice()
        {
            try
            {
                DisConnect();
                Delay(3000);
                Socket sc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (!sc.Connected)
                {
                    remoteEP = new IPEndPoint(ipAddress, port);
                    sc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sc.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), sc);

                    if (connectDone.WaitOne(6000, false))
                    {
                        isConnect = true;
                        totalOrderFlag = true;
                    }
                    else
                    {
                        isConnect = false;
                        return;
                    }
                }
                client = sc;
            }
            catch (Exception e)
            {
                LogFile.Instance.Write(DateTime.Now + "connectCatchENd");
                writeLog(e);
                isConnect = false;
                return;
            }
        }
        /// <summary>
        /// 写程序异常文件
        /// </summary>
        /// <param name="str"></param>
        public void writeLog(Exception ex)
        {
            string str = string.Format("异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                        ex.GetType().Name, ex.Message, ex.StackTrace);
            LogFile.Instance.Write(DateTime.Now + str);
        }
    }
    // State object for receiving data from remote device. 
    /// <summary>
    /// 信息传输错误状态
    /// 0-准备发送,1-成功 2-发送失败 3-接收失败 4-抓管撞管（撞针） 5-抓空 6-混匀异常 7-放管撞管 8-理杯机缺管 9-发送超时
    /// </summary>
    public enum ErrorState
    {
        ReadySend = 0, Success = 1, Sendfailure = 2, Recivefailure = 3,
        IsKnocked = 4, IsNull = 5, BlendUnusua = 6, putKnocked = 7, LackTube = 8, OverTime = 9
    }
    /// <summary>
    /// 指令发送状态 
    /// MoveNewTube-加新管 MoveTube-移管 AddSp-加样本 AddSR-加试剂  Wash-清洗 Total-调试
    /// Mix -混匀内圈 Mix2-混匀外圈  SampleAddition-调度样本添加区  TrackStrip-轨道  CapstanStrip-回收链盘
    /// </summary>
    public enum OrderSendType 
    { MoveNewTube = 1, MoveTube = 11, AddSp = 0, AddSR = 10, Wash = 2, Total = 5,
        Mix =4,Mix2 =14, SampleAddition = 15, TrackStrip = 6, CapstanStrip = 7 
    }
    public class StateObject
    {
        // Client socket.     
        public Socket workSocket = null;
        // Size of receive buffer.     
        public const int BufferSize = 1024;
        // Receive buffer.     
        public byte[] buffer = new byte[BufferSize];
        // Received data string.     
        public StringBuilder Spsb = new StringBuilder();
        public StringBuilder Movesb = new StringBuilder();
        public StringBuilder Washsb = new StringBuilder();
        public StringBuilder sb = new StringBuilder();
    }
    /// <summary>
    /// 日志
    /// </summary>
    public class LogFile
    {
        static object myObject = new object();
        private FileStream SW;
        private static LogFile _instance;
        public static LogFile Instance
        {
            get
            {
                lock (myObject)
                {
                    return _instance ?? (_instance = new LogFile());
                }
            }
        }
        public LogFile()
        {
            SW = new FileStream(Application.StartupPath + @"\Log\NetLog\C" + DateTime.Now.ToString("yyyyMMdd HHmmss") + ".txt", FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 100, FileOptions.Asynchronous);
            //SW = new FileStream(Application.StartupPath + @"\Log\C" + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".txt");
            //SW.AutoFlush = true;
        }
        public void Write(string str)
        {
            lock (myObject)
            {
                byte[] byteArray = System.Text.Encoding.Default.GetBytes(str + Environment.NewLine);
                SW.BeginWrite(byteArray, 0, byteArray.Length, null, null);
                //SW.WriteLine(str);
                SW.Flush();
            }
        }
        public void Close()
        {
            SW.Flush();
            SW.Close();
        }
    }
    /// <summary>
    /// 报警信息记录
    /// </summary>
    public class LogFileAlarm
    {
        static object myObject = new object();
        private FileStream Fs;
        private static LogFileAlarm _instance;
        public static LogFileAlarm Instance
        {
            get
            {
                lock (myObject)
                {
                    return _instance = new LogFileAlarm();
                }
            }
        }
        public LogFileAlarm()
        {
            if (File.Exists(Application.StartupPath + @"\Log\AlarmLog\I" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
            {
                Fs = new FileStream(Application.StartupPath + @"\Log\AlarmLog\I" + DateTime.Now.ToString("yyyyMMdd") + ".txt", FileMode.Append, FileAccess.Write, FileShare.Read);
                //Fs = new FileStream(Application.StartupPath + @"\Log\AlarmLog\I" + DateTime.Now.ToString("yyyyMMdd") + ".txt", FileMode.Open,FileAccess.ReadWrite,FileShare.Read );
            }
            else
            {
                Fs = new FileStream(Application.StartupPath + @"\Log\AlarmLog\I" + DateTime.Now.ToString("yyyyMMdd") + ".txt", FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 100, FileOptions.Asynchronous);
            }
        }
        public void Write(string str)
        {
            lock (myObject)
            {
                StreamWriter sw = new StreamWriter(Fs, System.Text.Encoding.Default);//转码
                sw.WriteLine(str);
                sw.Flush();
                sw.Close();
                Fs.Close();
            }
        }
        public void Close()
        {
            Fs.Flush();
            Fs.Close();
        }
    }
}
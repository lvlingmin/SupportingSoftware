using BioBaseCLIA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BioBase.HSCIADebug.ControlInfo
{
    public class WashSend
    {
        /// <summary>
        /// 吸液位置有管情况
        /// </summary>
        public static int ImbibitionFlag = 0;
        /// <summary>
        /// 注液位置有管情况
        /// </summary>
        public static int[] LiquidInjectionFlag = new int[3];
        /// <summary>
        /// 加底物位置是否有管
        /// </summary>
        public static int AddSubstrateFlag = 0;
        /// <summary>
        /// 读数位置是否有管
        /// </summary>
        public static int ReadFlag = 0;
        /// <summary>
        /// 初始化数据
        /// </summary>
        public static void Initial()
        {
            ImbibitionFlag = 0;
            LiquidInjectionFlag[0] = 0;
            LiquidInjectionFlag[1] = 0;
            LiquidInjectionFlag[2] = 0;
            AddSubstrateFlag = 0;
            ReadFlag = 0;
        }
        /// <summary>
        /// 底物管路
        /// </summary>
        public static int substratePipe = 0;
        /// <summary>
        /// 清洗盘旋转
        /// </summary>
        /// <param name="strModel">级联机器编号</param>
        /// <param name="num">孔位</param>
        /// <returns></returns>
        public static int WashTurn(string strModel, int num)
        {
            string order = "";
            if (num > 0)
            {
                order = "EB "+ strModel + " 31 03 01 " + (num).ToString("X2");
            }
            else if (num < 0)
            {
                order = "EB " + strModel + " 31 03 01 " + (num).ToString("X2").Substring(6, 2);
            }
            NetCom3.Instance.Send(NetCom3.Cover(order), (int)OrderSendType.Wash);
            NetCom3.Instance.WashQuery();
            return NetCom3.Instance.WasherrorFlag;
        }
        /// <summary>
        /// 清洗盘旋转到目标孔位
        /// </summary>
        /// <param name="strModel">级联机器编号</param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int WashTurnTo(string strModel,int num)
        {
            NetCom3.Instance.Send(NetCom3.Cover("EB " + strModel + " 31 03 02 " + num.ToString("x2") + ""), (int)OrderSendType.Wash);
            NetCom3.Instance.WashQuery();
            return NetCom3.Instance.WasherrorFlag;
        }
        /// <summary>
        /// 注液/抽液/加底物/读数
        /// </summary>
        /// <param name="strModel">级联机器编号</param>
        /// <returns></returns>
        public static int WashAddLiquidR(string strModel)
        {
            NetCom3.Instance.Send(NetCom3.Cover("EB "+ strModel + " 31 03 03 " + ImbibitionFlag.ToString("x2") + " " + LiquidInjectionFlag[0].ToString() +
                    LiquidInjectionFlag[1].ToString() + " " + LiquidInjectionFlag[2].ToString() + AddSubstrateFlag.ToString() + " " + substratePipe.ToString() + ReadFlag.ToString()), (int)OrderSendType.Wash);
            NetCom3.Instance.WashQuery();
            return NetCom3.Instance.WasherrorFlag;
        }
        /// <summary>
        /// 计算新的PMT值
        /// </summary>
        /// <param name="pmt"></param>
        /// <returns></returns>
        public static double GetPMT(double pmt)
        {
            return pmt = pmt / (1 - pmt * 20 * Math.Pow(10, -9)); ;
        }
    }
    
}

using BioBaseCLIA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BioBase.HSCIADebug.ControlInfo
{
    public class AddLiquidSend
    {
        /// <summary>
        ///加样机运行状态
        /// </summary>
        static AddLiquidStep liquidstep;
        public static AddLiquidStep Liquidstep { get => liquidstep; set => liquidstep = value; }
        /// <summary>
        /// 加样指令
        /// </summary>
        /// <param name="startpos">加样开始位置</param>
        /// <param name="endpos">加样结束位置</param>
        /// <param name="liquidVol">加样体积</param>
        /// <param name="leftVol">剩余体积</param>
        public static void AddSample(int startpos,int endpos, int liquidVol, int leftVol)
        {
            string order = "";
            //加普通样本
            if (liquidstep == AddLiquidStep.AddLiquidCS)
                order = "EB 90 31 02 01 " + startpos.ToString("x2") + " " + endpos.ToString("x2")
                                   + " " + liquidVol.ToString("x2")+" 00" ;
            //加急诊样本
            else if (liquidstep == AddLiquidStep.AddLiquidES)
                order = "EB 90 31 02 01 " + startpos.ToString("x2") + " " + endpos.ToString("x2")
                                  + " " + liquidVol.ToString("x2") + " 01";
            //加稀释样本
            else if(liquidstep == AddLiquidStep.AddLiquidDS)
                order = "EB 90 31 02 02 " + startpos.ToString("x2") + " " + endpos.ToString("x2")
                                  + " " + liquidVol.ToString("x2");
            //管路灌注
            else if (liquidstep==AddLiquidStep.AddstratePour)
                order = "EB 90 31 02 08 ";
            NetCom3.Instance.Send(NetCom3.Cover(order), (int)OrderSendType.AddSp);
            NetCom3.Instance.SPQuery();
        }
        /// <summary>
        /// 加试剂指令
        /// </summary>
        /// <param name="startpos">加样开始位置</param>
        /// <param name="endpos">加样结束位置</param>
        /// <param name="liquidVol">加样体积</param>
        /// <param name="leftVol">剩余体积</param>
        public static void AddReagent(int startpos, int endpos, int liquidVol, int leftVol)
        {
            string order = "";
            string StrLeftdiuVol = leftVol.ToString("x2");
            if (StrLeftdiuVol.Length < 4)
            {
                for (int i = StrLeftdiuVol.Length; i < 4; i++)
                {
                    StrLeftdiuVol = StrLeftdiuVol + "0";
                }
            }
            //加稀释液
            if (liquidstep == AddLiquidStep.AddLiquidD)
                order = "EB 90 31 12 06 " + startpos.ToString("x2") + " " + endpos.ToString("x2")
                                   + " " + liquidVol.ToString("x2") + " " +StrLeftdiuVol.Substring(0, 2)
                                   + " " + StrLeftdiuVol.Substring(2, 2);
            //加R1
            else if (liquidstep == AddLiquidStep.AddSingleR1)
                order = "EB 90 31 12 03 " + startpos.ToString("x2") + " " + endpos.ToString("x2")
                                  + " " + liquidVol.ToString("x2") + " " + StrLeftdiuVol.Substring(0, 2)
                                  + " " + StrLeftdiuVol.Substring(2, 2);
            //加R2
            else if (liquidstep == AddLiquidStep.AddSingleR2)
                order = "EB 90 31 12 04 " + startpos.ToString("x2") + " " + endpos.ToString("x2")
                                  + " " + liquidVol.ToString("x2") + " " + StrLeftdiuVol.Substring(0, 2)
                                 + " " + StrLeftdiuVol.Substring(2, 2);
            //加R3 
            else if (liquidstep == AddLiquidStep.AddSingleR3)
                order = "EB 90 31 12 05 " + startpos.ToString("x2") + " " + endpos.ToString("x2")
                                  + " " + liquidVol.ToString("x2") + " " + StrLeftdiuVol.Substring(0, 2)
                                  + " " + StrLeftdiuVol.Substring(2, 2);
            //加磁珠
            else if (liquidstep == AddLiquidStep.AddBeads)
                order = "EB 90 31 12 07 " + startpos.ToString("x2") + " " + endpos.ToString("x2")
                                  + " " + liquidVol.ToString("x2") + " " + StrLeftdiuVol.Substring(0, 2)
                                  + " " + StrLeftdiuVol.Substring(2, 2);
            NetCom3.Instance.Send(NetCom3.Cover(order), (int)OrderSendType.AddSR);
            NetCom3.Instance.SP2Query();
        }
    }
    /// <summary>
    /// 加液步骤
    /// AddLiquidCS-加普通样本
    /// AddLiquidES-加急诊样本
    /// AddLiquidDS-加稀释样本
    /// AddSingleR1-加试剂1
    /// AddSingleR2-加试剂2
    /// AddSingleR3-加试剂3
    /// AddLiquidD-加稀释液
    /// AddBeads-加磁珠
    /// AddstratePour-加样管路灌注
    /// RegentDTurn-试剂盘旋转
    /// SampleDTurn-样本盘旋转
    /// </summary>
    public enum AddLiquidStep
    {
        AddLiquidCS=1,
        AddLiquidES = 11,
        AddLiquidDS =2,
        AddSingleR1=3,
        AddSingleR2=4,
        AddSingleR3=5,
        AddLiquidD =6,
        AddBeads=7,
        AddstratePour=8,
        RegentDTurn=9,
        SampleDTurn= 10
    }
}
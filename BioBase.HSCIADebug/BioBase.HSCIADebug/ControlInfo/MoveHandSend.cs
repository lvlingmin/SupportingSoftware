using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioBase.HSCIADebug.ControlInfo
{
    class MoveHandSend
    {
        /// <summary>
        ///移管手运行状态
        /// </summary>
        static MoveSate moveSate;
        public static MoveSate MoveSate { get => moveSate; set => moveSate = value; }
        /// <summary>
        /// 移新管抓手发送指令
        /// </summary>
        /// <param name="movestate">动作状态</param>
        /// <param name="startpos">开始位置</param>
        /// <param name="goalpos">结束位置</param>
        /// <returns></returns>
        public static void Move(int startpos = 0, int goalpos = 0)
        {
            string order = "";
            //加新管到温育盘
            if (moveSate == MoveSate.NewtubeToReact)
            {
                order = "EB 90 31 01 01 "+ startpos.ToString("x2");
            }
            //温育盘夹管到清洗盘
            if (moveSate == MoveSate.ReactToWash)
            {
                order = "EB 90 31 01 02 " + startpos.ToString("x2");
            }
            //清洗盘夹管到温育盘
            if (moveSate == MoveSate.WashToReact)
            {
                order = "EB 90 31 01 03 " + startpos.ToString("x2") + " " + goalpos.ToString("x2") + "";
            }
            //清洗盘扔废管
            if (moveSate == MoveSate.WashLoss)
            {
                if (startpos == 0)
                    startpos = (int)WashLossPos.PutTubePos;
                order = "EB 90 31 01 04 " + startpos.ToString("x2");
            }
            //温育盘扔废管
            if (moveSate == MoveSate.ReactLoss)
            {
                order = "EB 90 31 01 05 " + startpos.ToString("x2");
            }
            //加新管到清洗盘
            if (moveSate == MoveSate.NewtubeToWash)
            {
                order = "EB 90 31 01 06 ";
            }
            int sendType = (int)OrderSendType.MoveNewTube;
            NetCom3.Instance.Send(NetCom3.Cover(order), sendType);
            NetCom3.Instance.MoveQuery();
        }
        /// <summary>
        /// 移管手发送指令
        /// </summary>
        /// <param name="startpos"></param>
        /// <param name="goalpos"></param>
        public static void Move2(int startpos = 0, int goalpos = 0)
        {
            string order = "";
            //加新管到温育盘
            if (moveSate == MoveSate.NewtubeToReact)
            {
                order = "EB 90 31 11 01 " + startpos.ToString("x2");
            }
            //温育盘夹管到清洗盘
            if (moveSate == MoveSate.ReactToWash)
            {
                order = "EB 90 31 11 02 " + startpos.ToString("x2");
            }
            //清洗盘夹管到温育盘
            if (moveSate == MoveSate.WashToReact)
            {
                order = "EB 90 31 11 03 " + startpos.ToString("x2") + " " + goalpos.ToString("x2") + "";
            }
            //清洗盘扔废管
            if (moveSate == MoveSate.WashLoss)
            {
                if (startpos == 0)
                    startpos = (int)WashLossPos.PutTubePos;
                order = "EB 90 31 11 04 " + startpos.ToString("x2");
            }
            //温育盘扔废管
            if (moveSate == MoveSate.ReactLoss)
            {
                order = "EB 90 31 11 05 " + startpos.ToString("x2");
            }
            //加新管到清洗盘
            if (moveSate == MoveSate.NewtubeToWash)
            {
                order = "EB 90 31 11 06 ";
            }
            int sendType = (int)OrderSendType.MoveTube;
            NetCom3.Instance.Send(NetCom3.Cover(order), sendType);
            NetCom3.Instance.Move2Query();
        }
    }
    /// <summary>
    /// 移管手运行状态
    /// NewtubeToReact-向温育盘夹新管
    /// ReactToWash-温育盘向清洗盘夹管
    /// WashToReact-清洗盘向温育盘夹管
    /// WashLoss-清洗盘扔管
    /// ReactLoss-温育盘扔管
    /// Blend-混匀
    /// </summary>
    public enum MoveSate
    {
        NewtubeToReact = 1,
        ReactToWash = 2,
        WashToReact = 3,
        WashLoss = 4,
        ReactLoss = 5,
        NewtubeToWash = 6,
        Blend = 11
    }
    /// <summary>
    /// 清洗盘扔管位置
    /// ReadFinshPos-读数完成29号位置
    /// LiquidInpos1-注液1位置
    /// LiquidInpos2-注液2位置
    /// LiquidInpos3-注液3位置
    /// AddSubPos-加底物位置
    /// PutTubePos-取放管位置
    /// LiquidOutpos3-抽液位置
    /// </summary>
    public enum WashLossPos
    {
        ReadFinshPos = 1,
        LiquidInpos1 = 2,
        LiquidInpos2 = 3,
        LiquidInpos3 = 4,
        AddSubPos = 5,
        PutTubePos = 6,
        LiquidOutpos3 = 9,
    }
    /// <summary>
    /// 移新管标志 NewMovehand-移新管抓手 Movehand-移管手
    /// </summary>
    public enum MoveHand {NewMovehand=0,Movehand=1 }
}

using BioBaseCLIA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BioBase.HSCIADebug.ControlInfo
{
    public class LiquidTemSend
    {
        /// <summary>
        /// 查询模块温度值
        /// </summary>
        /// <param name="type">模块类型</param>
        /// <returns></returns>
        public static string GetTemperature(ModeType type)
        {
            string Order = "EB 90 11 " + ((int)type).ToString("x2") + " 04 ";
            return Order;
        }
        /// <summary>
        /// 查询模块温度校准值
        /// </summary>
        /// <param name="type">模块类型</param>
        /// <returns></returns>
        public static string GetCalibrateValue(ModeType type)
        {
            string Order = "EB 90 11 " + ((int)type).ToString("x2") + " 06 ";
            return Order;
        }

    }
    /// <summary>
    /// 模块类型
    /// TypeReact-温育盘 TypeWash-清洗盘 TypeWashPipe-清洗管路 TypeSubPipe-底物管路
    /// </summary>
    public enum ModeType { TypeReact = 4,TypeWash=5,TypeWashPipe=6,TypeSubPipe=7}
}
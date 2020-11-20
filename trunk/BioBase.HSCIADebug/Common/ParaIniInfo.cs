using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 配置信息类
    /// </summary>
    public class ParaIniInfo
    {
        /// <summary>
        /// 管架配置信息地址
        /// </summary>
        public static string iniPathSubstrateTube = Directory.GetCurrentDirectory() + "\\SubstrateTube.ini";
        /// <summary>
        /// 试剂配置信息地址
        /// </summary>
        public static string iniPathReagentTrayInfo = Directory.GetCurrentDirectory() + "\\ReagentTrayInfo.ini";
        /// <summary>
        /// 温育盘配置信息地址
        /// </summary>
        public static string iniPathReactTrayInfo = Directory.GetCurrentDirectory() + "\\ReactTrayInfo.ini";
        /// <summary>
        /// 清洗盘配置信息地址
        /// </summary>
        public static string iniPathWashTrayInfo = Directory.GetCurrentDirectory() + "\\WashTrayInfo.ini";
        /// <summary>
        /// 报表打印项目顺序配置信息地址
        /// </summary>
        public static string iniPathReportSort = Directory.GetCurrentDirectory() + "\\ReportSort.ini";
        /// <summary>
        /// 仪器参数配置信息地址
        /// </summary>
        public static string iniPathInstrumentPara = Directory.GetCurrentDirectory() + "\\InstrumentPara.ini";
    }
}

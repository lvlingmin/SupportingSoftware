using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace DebugTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //只允许存在一个实例
            if ((Process.GetProcesses().Where(x => x.ProcessName == Process.GetCurrentProcess().ProcessName).Select(x => x).ToList().Count > 1))
            {
                MessageBox.Show("程序正在运行，请勿重复启动!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ////检查主软件快捷键启动本软件传递过来的命令行参数
            //bool start = args.Length < 1 ? (false) : (args[0] != "BioBaseDebugTool" + DateTime.Now.ToString("yyyyMMdd") ? false : true);
            //if (!start)
            //    return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EBarv0._2
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //增加新的主界面，主界面引导进入不同的实际基本信息和发光值等界面
            Application.Run(new FrmMain());
        }
    }
}

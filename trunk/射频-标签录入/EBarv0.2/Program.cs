using EBarv0._2.Administrator;
using EBarv0._2.user;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if ((Process.GetProcesses().Where(x => x.ProcessName == Process.GetCurrentProcess().ProcessName).Select(x => x).ToList().Count > 1))
            {
                MessageBox.Show("程序正在运行，请勿重复启动!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //增加新的主界面，主界面引导进入不同的实际基本信息和发光值等界面
            frmLogin f = new frmLogin();
            DialogResult dr = f.ShowDialog();
            if (frmParent.LoginUserType == "1")
            {
                //admin
                Application.Run(new frmAdmin());
            }
            else if (frmParent.LoginUserType == "0")
            {
                //user
                Application.Run(new frmUser());
            }
            //else if(frmParent.LoginUserType == "9")
            //{
            //    //developer
            //    //Application.Run(new frmDevelop());
            //}
            else
            {
                Application.Exit();
            }

            //Application.Run(new FrmMain());
        }
    }
}

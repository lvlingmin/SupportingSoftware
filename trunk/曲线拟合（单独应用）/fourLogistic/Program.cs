using fourLogistic.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace fourLogistic
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                string str = "";
                string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
                if (ex != null)
                {
                    str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                         ex.GetType().Name, ex.Message, ex.StackTrace);
                }
                else
                {
                    str = string.Format("应用程序线程错误:{0}", ex);
                }
                writeLog(str);
                MessageBox.Show("数据填写错误，请重启软件并填写数据！","错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            /// <summary>
            /// 写文件
            /// </summary>
            /// <param name="str"></param>
            void writeLog(string str)
            {
                if (!Directory.Exists("ErrLogs"))
                {
                    Directory.CreateDirectory("ErrLogs");
                }
                using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + @"\ErrLogs\ErrLog.txt", true))
                {
                    sw.WriteLine("\n************************************************************************");
                    sw.WriteLine(str);
                    sw.WriteLine("************************************************************************\n");
                    sw.Close();
                }
            }
        }
    }
}

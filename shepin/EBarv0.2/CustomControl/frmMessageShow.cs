using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EBarv0._2.CustomControl
{
    public partial class frmMessageShow : Form
    {
        delegate void SetTextCallBack(string text);
        public frmMessageShow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 自定义Dialog的Show()方法
        /// </summary>
        /// <param name="title">Dialog标题</param>
        /// <param name="content">内容</param>
        /// <returns>指示标志符以指示对话框的返回值</returns>
        public DialogResult MessageShow(string title, string content)
        {
            if (this.IsHandleCreated)
            {
                this.Invoke(new SetTextCallBack(SetText), content);
            }
            else
            {
                this.Text = title;
                this.lblMessage.Text = content;
            }
            this.Width = 12 * content.Length + 80;//根据提示信息长短确定弹窗大小。
            panel1.Width = 12 * content.Length + 80;
            //this.Height = ;
            this.btnOK.Left = this.Width / 2 - 37;//设置弹窗中按钮的位置
            //2018-11-23 zlx add
            if (this.Text.Contains("警告"))
                StartKiller();
            //DialogResult dr = this.ShowDialog();//增加DialogResult返回值
            DialogResult dr = DialogResult.OK;
            try
            {
                dr = this.ShowDialog();//增加DialogResult返回值
            }
            catch (System.InvalidOperationException ex)
            {
                //throw ex;
                Console.WriteLine(ex.ToString());
            }
            return dr;
        }
        public void MessageWrite(string title, string content)
        {
            Invoke(new SetTextCallBack(SetText), content);
        }
        /// <summary>
        /// 更改控件信息 20180524 zlx add
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        private void SetText(string content)
        {
            this.lblMessage.Text = content;
        }
        private void StartKiller()
        {
            Timer timer = new Timer();
            timer.Interval = 5000; //3秒启动 
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            btnOK_Click(null, null);
            //停止Timer 
            ((Timer)sender).Stop();
        }
        //public DialogResult MessageConfirm(string title, string content)
        //{
        //    this.Text = title;
        //    this.lblMessage.Text = content;
        //    this.Width = 12 * content.Length + 80;//根据提示信息长短确定弹窗大小。
        //    //this.Height = ;
        //    this.btnOK.Left = this.Width / 2 - 37;//设置弹窗中按钮的位置
        //    return MessageBox.Show(MessageBoxButtons.OKCancel, MessageBoxIcon.Question); ;
        //}

        internal System.Windows.Forms.DialogResult MessageShow(string p, string p_2, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            throw new NotImplementedException();
        }

        internal void MessageShow(string p)
        {
            throw new NotImplementedException();
        }

        private void frmMessageShow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                return;
            }
        }
    }
}

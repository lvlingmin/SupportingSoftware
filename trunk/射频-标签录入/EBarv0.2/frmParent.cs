using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EBarv0._2
{
    public partial class frmParent : Form
    {
        //2018-08-04
        /// <summary>
        /// 当前登录用户名
        /// </summary>
        public static string LoginUserName { get; set; }
        /// <summary>
        /// 当前登录用户类型
        /// 普通用户为0,管理员用户1，测试用户为9
        /// </summary>
        public static string LoginUserType { get; set; }
        public frmParent()
        {
            InitializeComponent();
            this.MaximizeBox = false;//禁止最大化
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;//禁止进行拖拽
        }
    }
}

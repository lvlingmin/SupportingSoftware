using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioBase.HSCIADebug
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
        Autosize autosize = new Autosize();//窗体自适应公共类
        public float X;//宽度 
        public float Y;//高度
        /// <summary>
        /// 清洗盘取放孔位
        /// </summary>
        public static int tubeHoleNum = 1;//Jun add 2019/1/25
        public static int washCountNum = 0;
        public static bool isHavedCount = false;
        /// <summary>
        /// 样本信息表
        /// </summary>
        public static DataTable dtSpInfo = new DataTable();
        /// <summary>
        /// 试剂信息表
        /// </summary>
        public static DataTable dtRgInfo = new DataTable();
        //添加样本运行信息表。 LYN add 20171114
        /// <summary>
        /// 样本运行信息表
        /// </summary>
        public static DataTable dtSampleRunInfo = new DataTable();
        /// <summary>
        /// 系统登录名称
        /// </summary>
        private static String _logingName;
        /// <summary>
        /// 样本加载位置数
        /// </summary>
        //public const int SampleNum = 60;//2019-02-26  zlx add
        /// <summary>
        /// 试剂加载位置数
        /// </summary>
        protected const int RegentNum = 25;
        /// <summary>
        /// 温育盘位置数(里75 外75）
        /// </summary>
        protected const int ReactTrayNum = 150;
        /// <summary>
        /// 清洗盘位置数
        /// </summary>
        protected const int WashTrayNum = 40;
        /// <summary>
        /// 清洗盘抽液位置
        /// </summary>
        protected static int[] WashPumPos ={5,10,15,20};
        /// <summary>
        /// 清洗盘注液位置
        /// </summary>
        protected static int[] WashInjectPos = { 6,11,16 };
        /// <summary>
        /// 清洗盘加底物位置
        /// </summary>
        protected static  int WashAddSubPos = 22;
        /// <summary>
        /// 清洗盘读数位置
        /// </summary>
        protected static int ReadPos = 30;
        /// <summary>
        /// 实验过程中正在加载试剂标志
        /// </summary>
        public static bool ReagentCaculatingFlag = false;
        public frmParent()
        {
            InitializeComponent();
            X = this.Width;
            Y = this.Height;
        }
        /// <summary>
        /// 窗体自适应方法，可随着分辨率的大小而变化
        /// </summary>
        /// <param name="form">窗体</param>
        public void formSizeChange(Form form)
        {
            float newx = (form.Width) / X;
            float newy = form.Height / Y;
            autosize.setTag(form);
            autosize.setControls(newx, newy, form);
        }
        /// <summary>
        /// 判断窗体是否存在
        /// </summary>
        /// <param name="Forms">已存在窗体的类型名</param>
        /// <returns>存在，则返回true</returns>
        public bool CheckFormIsOpen(string Forms)
        {
            bool bResult = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == Forms)
                {
                    bResult = true;
                    break;
                }
            }
            return bResult;
        }
        /// <summary>
        /// 正在登录名
        /// </summary>
        public static String LoginGName
        {
            get { return frmParent._logingName; }
            set { frmParent._logingName = value; }
        }

        private void frmParent_Load(object sender, EventArgs e)
        {

        }

        //对请洗盘当前孔号进行计数
        public void countWashHole(int pace)
        {
            if (pace > 0)
            {
                washCountNum = washCountNum - pace;
                if (washCountNum <= 0)
                    washCountNum = washCountNum + 30;
            }
            else if (pace < 0)
            {
                washCountNum = washCountNum - pace;
                if (washCountNum > 30)
                    washCountNum = washCountNum - 30;
            }
            LogFile.Instance.Write("==================  当前位置  " + washCountNum);
        }
        /// <summary>
        /// 计算新的PMT值
        /// </summary>
        /// <param name="pmt"></param>
        /// <returns></returns>
        public double GetPMT(double pmt)
        {
            return pmt = pmt / (1 - pmt * 20 * Math.Pow(10, -9)); ;
        }
    }
    
}

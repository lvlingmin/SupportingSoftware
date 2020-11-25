using EBarv0._2.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EBarv0._2.Admin
{
    public partial class frmQualityControl : frmParent
    {
        DataTable dtProject = new DataTable();//存放原始字符串和密文
        DataTable dt = new DataTable();
        bool closedFlag = false;
        int itemIndex = -1;

        public static string spQcOrder = "";
        public static string[] spQcLow = new string[3];
        public static string[] spQcHigh = new string[3];
        public static DateTime[] spQcDate = new DateTime[2];
        bool isLow = false;
        public frmQualityControl()
        {
            InitializeComponent();
        }
        public frmQualityControl(string order, DateTime date ,int itemInde)
        {
            InitializeComponent();
            spQcOrder = "";
            this.itemIndex = itemInde;
            
            if (order.Contains("sp1"))
            {
                rbLow.Checked = true;
                isLow = true;
            }
            else if(order.Contains("sp2"))
            {
                rbHigh.Checked = true;
                isLow = false;
            }
            if (isLow)
            {
                if (spQcLow[0] == null || spQcLow[0] == "")
                {
                    this.prodectTime.Value = date;
                }
            }
            else
            {
                if (spQcHigh[0] == null || spQcHigh[0] == "")
                {
                    this.prodectTime.Value = date;
                }
            }
        }
        private void frmQualityControl_Load(object sender, EventArgs e)
        {
            //设置标题
            dtProject.Columns.Add("原始字符串", typeof(string));
            dtProject.Columns.Add("密文", typeof(string));
            
            //设置reagenName显示
            string sql = "select ProjectNumber ,ShortName,ProjectProcedure from tbProject"; //查询语句
            dt = DbHelperOleDb.Query(0, sql).Tables[0];  //获取需要的数据库表赋值给dt
           
            this.reagentName.DataSource = dt;   //下拉框comboBox设置数据源为tbProject表
            this.reagentName.DisplayMember = "ShortName";  //comboBox设置显示列，列名为项目简称
            this.reagentName.ValueMember = "ProjectNumber"; //把表中的项目编号属性，设置为comboBox中选项的实际值
            
            this.reagentName.SelectedIndex = -1;
           
            reagentName.SelectedIndex = itemIndex;
            reagentName.Enabled = false;

            if(isLow)
            {
                if(spQcLow[0] != null && spQcLow[0] != "")
                {
                    txtX.Text = spQcLow[0];
                    txtSD.Text = spQcLow[1];
                    prodectTime.Value = spQcDate[0];
                }
            }
            else
            {
                if(spQcHigh[0] != null && spQcHigh[0] != "")
                {
                    txtX.Text = spQcHigh[0];
                    txtSD.Text = spQcHigh[1];
                    prodectTime.Value = spQcDate[1];
                }
            }
        }

        private void btnGener_Click(object sender, EventArgs e)
        {
            if (reagentName.Text == "" || txtX.Text == "" || txtSD.Text == "")
            {
                MessageBox.Show("有输入字符为空，请确认后重新输入。", "温馨提示");
                return;
            }
            if (dt.Select("ShortName = '" + reagentName.Text.Trim() + "'").Length <= 0)
            {
                MessageBox.Show("当前实验项目不存在!");
            }
            if (!(Regex.IsMatch(txtX.Text.Trim(), "^[0-9]{1,4}$") || Regex.IsMatch(txtX.Text.Trim(), "^([0-9]{1,})+(.[0-9]{1,2})$")))
            {
                MessageBox.Show("输入字符不符合要求，请确认后重新输入。", "温馨提示");
                return;
            }
            if (!(Regex.IsMatch(txtSD.Text.Trim(), "^[0-9]{1,4}$") || Regex.IsMatch(txtSD.Text.Trim(), "^([0-9]{1,})+(.[0-9]{1,3})$")))
            {
                MessageBox.Show("输入字符不符合要求，请确认后重新输入。", "温馨提示");
                return;
            }

            string oristr = ToGetOriginalString();
            string encrystr = Utils.instance.ToEncryption(oristr);
            dtProject.Rows.Add(oristr, encrystr);
            spQcOrder = encrystr;
            back.Enabled = true;
        }

        private void back_Click(object sender, EventArgs e)
        {
            if(txtX.Text == "" || txtSD.Text == "")
            {
                MessageBox.Show("请检查输入内容！" , "WARN");
                back.Enabled = false;
                return;
            }
            if(isLow)
            {
                spQcLow[0] = txtX.Text.ToString().Trim();
                spQcLow[1] = txtSD.Text.ToString().Trim();
                spQcLow[2] = reagentName.Text;
                spQcDate[0] = prodectTime.Value;
            }
            else
            {
                spQcHigh[0] = txtX.Text.ToString().Trim();
                spQcHigh[1] = txtSD.Text.ToString().Trim();
                spQcHigh[2] = reagentName.Text;
                spQcDate[1] = prodectTime.Value;
            }
            closedFlag = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            spQcOrder = "";
            closedFlag = true;
            this.Close();
        }
        private string ToGetOriginalString()
        {
            string strNo1 = "9"; //条码序号
            string strItemNum = ""; //项目名称
            string strDate = ""; //生产日期
            string strX = ""; //质控靶值
            string strSD = ""; //质控标准差
            string strLevel = ""; //质控类别
            string strRule = ""; //质控规则

            DataRow[] dr = dt.Select("ShortName = '" + reagentName.Text.Trim() + "'");  //将下拉框中选中的项目信息导入行dr
            if (dr.Length > 0)  //行内元素总数
            {
                string projectNum = dr[0]["ProjectNumber"].ToString();  //获取项目编号
                while (projectNum.Length < 3)
                {
                    projectNum = projectNum.Insert(0, "0");  //当项目编号小于三位，向前面填充0
                }
                strItemNum = projectNum;   //添加三位的编号
            }
            else
            {
                MessageBox.Show("当前实验项目不存在!");
                return "";
            }

            string time = string.Format("{0:yyyyMMdd}", prodectTime.Value);
            strDate = TimeToNewTime(time);     //将time日期转换为三位数，添加到sb字符串序列

            string[] tempX = txtX.Text.Trim().Split('.');
            if (tempX.Length == 1)
                strX = int.Parse(tempX[0]).ToString("X3") + "00";
            else
                strX = int.Parse(tempX[0]).ToString("X3") + int.Parse(tempX[1]).ToString("X2");

            string[] tempSD = txtSD.Text.Trim().Split('.');
            if (tempSD.Length == 1)
                strSD = int.Parse(tempSD[0]).ToString("X2") + "000";
            else
            {
                while (tempSD[1].Length < 3)
                {
                    tempSD[1] += "0";
                }
                //倒置
                string tempSD1 = tempSD[1];
                tempSD1 = tempSD1.Substring(2, 1) + tempSD1.Substring(1, 1) + tempSD1.Substring(0, 1);
                tempSD[1] = tempSD1;
                //X3
                strSD = int.Parse(tempSD[0]).ToString("X2") + int.Parse(tempSD[1]).ToString("X3");
            }
                //strSD = int.Parse(tempSD[0]).ToString("X2") + int.Parse(tempSD[1]).ToString("X2");

            strLevel = rbHigh.Checked ? "0" : (rbMid.Checked ? "1" : "2");//高中低-0/1/2

            strRule += cb1_2s.Checked ? "1" : "0";
            strRule += cb1_3s.Checked ? "1" : "0";
            strRule += cb2_2s.Checked ? "1" : "0";
            strRule += cb4_1s.Checked ? "1" : "0";
            strRule += cb10x.Checked ? "1" : "0";
            while (strRule.Length < 16)
                strRule += "0";
            strRule = Convert.ToInt32(strRule, 2).ToString("X4");

            string originalStr = strNo1 + strItemNum + strDate + strX + strSD + strLevel + strRule;

            return originalStr;
        }
        /// <summary>
        /// 返回特定方法产生的年月日
        /// </summary>
        /// <param name="time">8位年月日</param>
        /// <returns>返回特定年月日</returns>
        string TimeToNewTime(string time)  //将yyyyMMDD日期 转译为三位，大于10时用大写字母表示
        {
            //为了缩短长度，定的规则是，>=10的时候，用大写字母A-Z依次递增
            int timeYear = int.Parse(time.Substring(2, 2));
            string stringTimeYear = "";
            if (timeYear >= 10)
            {
                stringTimeYear = ((char)((timeYear - 10) + 'A')).ToString();  //年的后两位转译
            }
            else
            {
                stringTimeYear = timeYear.ToString();
            }
            int timeMon = int.Parse(time.Substring(4, 2)); //取两位 月份
            string stringTimeMon = "";
            if (timeMon >= 10)
            {
                stringTimeMon = ((char)((timeMon - 10) + 'A')).ToString(); //月份两位转译
            }
            else
            {
                stringTimeMon = timeMon.ToString();
            }
            int timeDay = int.Parse(time.Substring(6, 2));
            string stringTimeDay = "";
            if (timeDay >= 10)
            {
                stringTimeDay = ((char)((timeDay - 10) + 'A')).ToString(); //日期两位转译
            }
            else
            {
                stringTimeDay = timeDay.ToString();
            }
            return stringTimeYear + stringTimeMon + stringTimeDay; //返回编辑后的 三位数的日期
        }

        private void frmQualityControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!closedFlag && back.Text != "提交")
            {
                FrmMain.f0.Close();
                System.Environment.Exit(0);
            }
        }
        
    }
}

using EBarv0._2.DBUtility;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EBarv0._2
{
    public partial class frmSubstrate : Form
    {
        DataTable dtProject = new DataTable();//存放原始字符串和密文
        DataTable dt = new DataTable();
        bool closedFlag = false;

        public frmSubstrate()
        {
            InitializeComponent();
        }

        private void frmSubstrate_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//禁止最大化
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;//禁止进行拖拽

            //设置标题
            dtProject.Columns.Add("原始字符串", typeof(string));
            dtProject.Columns.Add("密文", typeof(string));
            dataGridView1.DataSource = dtProject;
            //设置显示
            this.dataGridView1.Columns[0].Width = 180;  //设定列宽
            this.dataGridView1.Columns[1].Width = 180;  //设定列宽掩饰
            this.dataGridView1.RowHeadersVisible = false;   //不显示包含标题的列

        }

        private void back_Click(object sender, EventArgs e)
        {
            closedFlag = true;
            FrmMain.f0.Show();
            this.Close();
        }

        private void btnGener_Click(object sender, EventArgs e)
        {
            if (!(Regex.IsMatch(txtTestNum.Text.Trim(), "^[0-9]{1,4}$") || Regex.IsMatch(txtTestNum.Text.Trim(), "^([0-9]{1,})+(.[0-9]{1,2})$")))
            {
                MessageBox.Show("输入字符不符合要求，请确认后重新输入。", "温馨提示");
                return;
            }

            Utils.instance.clearShowData(panel3, dtProject);  //清空原始字符串的dataGridView 和 显示条码的Panel1

            decimal DValue = num2.Value - num1.Value;
            for (int temporary = 0; temporary <= DValue; temporary++)
            {
                string oristr = ToGetOriginalString(temporary);//后面改到工具类中
                string encrystr = Utils.instance.ToEncryption(oristr);
                dtProject.Rows.Add(oristr, encrystr);
            }


            Utils.instance.ToMadePictureFromdt(panel3, dtProject);  //在panel1打印 完整条码（一维码）信息
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("请生成编码后再进行保存。", "温馨提示");
                return;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择保存文件夹";
            DialogResult flag = dialog.ShowDialog();
            if (flag == DialogResult.OK || flag == DialogResult.Yes)
            {
                string fPath = dialog.SelectedPath;
                //保存一维码
                Utils.instance.saveImage(dtProject, fPath, 6); //lyq mod 20190821
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            prodectTime.Value = DateTime.Now;
            num2.Value = num1.Value = 1;
            dtProject.Clear();
            panel3.Controls.Clear();
        }

        private void frmSubstrate_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!closedFlag)
            {
                FrmMain.f0.Close();
                System.Environment.Exit(0);
            }
        }

        private string ToGetOriginalString(decimal? add = 0)
        {
            string strNo1 = "A"; //条码序号
            string strBatchDate = "";//批号
            string strProductDate = ""; //生产日期
            string strSubNum = "1F4"; //底物默认测数 500测十六进制1F4

            string bTime = string.Format("{0:yyyyMMdd}", batchTime.Value);
            strBatchDate = TimeToNewTime(bTime);     //将time日期转换为三位数，添加到sb字符串序列

            string pTime = string.Format("{0:yyyyMMdd}", prodectTime.Value);
            strProductDate = TimeToNewTime(pTime);     //将time日期转换为三位数，添加到sb字符串序列

            string num = Convert.ToInt32(num1.Value + add).ToString("X4");

            string check = ((10 + 500 + int.Parse(bTime) + int.Parse(pTime) + Convert.ToInt32(num , 16)) % 7).ToString();
            string originalStr = strNo1 + strBatchDate + strProductDate + strSubNum + num +  check;
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
            int timeMon = int.Parse(time.Substring(4, 2));  //取两位 月份
            string stringTimeMon = "";
            if (timeMon >= 10)
            {
                stringTimeMon = ((char)((timeMon - 10) + 'A')).ToString();     //月份两位转译
            }
            else
            {
                stringTimeMon = timeMon.ToString();
            }
            int timeDay = int.Parse(time.Substring(6, 2));
            string stringTimeDay = "";
            if (timeDay >= 10)
            {
                stringTimeDay = ((char)((timeDay - 10) + 'A')).ToString();     //日期两位转译
            }
            else
            {
                stringTimeDay = timeDay.ToString();
            }
            return stringTimeYear + stringTimeMon + stringTimeDay;          //返回编辑后的 三位数的日期
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)  //控件CheckBox“批量生成”，没被选中
            {
                num2.Value = num1.Value;  //NumericUpDown控件被设定为和第一个NumericUpDown控件值一样
                label4.Visible = label5.Visible = false;  //设置两个标签不可见
                num2.Visible = false;       //NumericUpDown控件不可见
                label4.Enabled = label5.Enabled = false;  //设置两个标签不可用
                num2.Enabled = false;       //NumericUpDown不可用
            }
            else                            //控件CheckBox“批量生成”被选中
            {
                label4.Visible = label5.Visible = true;     //两个标签设定为可见
                num2.Visible = true;        //NumericUpDown控件可见
                label4.Enabled = label5.Enabled = true;     //两个标签设定为可用
                num2.Enabled = true;        //NumericUpDown控件可用
            }
        }

        private void num1_ValueChanged(object sender, EventArgs e)
        {
            num2.Minimum = num1.Value;  //限定从A到B，B的最小值为A的值
            if (checkBox1.Checked == false) num2.Value = num1.Value;  // 如果控件CheckBox没被选中，则把A到B，A和B的值设为一个值，既是A值
        }
    }
}

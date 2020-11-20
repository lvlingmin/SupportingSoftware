using EBarv0._2.DBUtility;
using EBarv0._2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EBarv0._2
{
    public partial class frmElectronicLabelEntry : Form
    {
        List<SpCode> spList = new List<SpCode>();
        DataTable dtPro;
        DataTable dtConcAndValue = new DataTable();
        int spSendFlag = 0;
        bool closedFlag = false;
        int serialNum = 1;//批内流水号
        public frmElectronicLabelEntry()
        {
            InitializeComponent();
            dtConcAndValue.Columns.Add("序号", typeof(string));
            dtConcAndValue.Columns.Add("浓度", typeof(string));
            dtConcAndValue.Columns.Add("发光值", typeof(string));
        }
        private void frmElectronicLabelEntry_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//禁止最大化
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;//禁止进行拖拽

            DbHelperOleDb db = new DbHelperOleDb(0);   //连接数据库ProjectInfo.mdb
            string sql = "select ProjectNumber ,ShortName,ProjectProcedure from tbProject"; //查询语句
            dtPro = DbHelperOleDb.Query(0,sql).Tables[0];  //获取需要的数据库表赋值给dt
            this.reagentName.SelectedIndexChanged -= new System.EventHandler(this.reagentName_SelectedIndexChanged);  //解除事件订阅（空函数（当选中项改变））      
            this.reagentName.DataSource = dtPro;   //下拉框comboBox设置数据源为tbProject表
            this.reagentName.DisplayMember = "ShortName";  //comboBox设置显示列，列名为项目简称
            this.reagentName.ValueMember = "ProjectNumber"; //把表中的项目编号属性，设置为comboBox中选项的实际值
            this.reagentName.SelectedIndexChanged += new System.EventHandler(this.reagentName_SelectedIndexChanged); //订阅事件（空函数（当选中项改变））
            dgvScaling.AllowUserToAddRows = false;
            dgvScaling.Columns.Clear();
            this.reagentName.SelectedIndex = -1;


        }
        private void back_Click(object sender, EventArgs e)
        {
            closedFlag = true;
            FrmMain.f0.Show();
            this.Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int num_1 = int.Parse(num1.Value.ToString());
            int num_2 = int.Parse(num2.Value.ToString());
            //清空list，重新生成
            spList.Clear();
            serialNum = 1;

            //非空判断
            if (prodectTime == null || reagentName.SelectedIndex < 0 || num_1 < 1 || num_2 < 1 || num_1 > num_2)
            {
                MessageBox.Show("输入错误，请检查后重新输入！", "WARN");
                return;
            }
            if (dtConcAndValue.Rows.Count > 0)
            {
                for (int p = 0; p < dtConcAndValue.Rows.Count; p++)
                {
                    if (dgvScaling.Rows[p].Cells["序号"].Value.ToString() == "" || dgvScaling.Rows[p].Cells["浓度"].Value.ToString() == "" || dgvScaling.Rows[p].Cells["发光值"].Value.ToString() == "")
                    {
                        MessageBox.Show("输入框为空，请检查后重新输入！", "WARN");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("未选择项目，请检查后重新操作！", "WARN");
                return;
            }
            if (cbReadyQC.Checked)
            {
                if (txtX.Text == "" || txtSD.Text == "")
                {
                    MessageBox.Show("输入框为空，请检查后重新输入！", "WARN");
                    return;
                }
                if (!(Regex.IsMatch(txtX.Text.Trim(), "^[0-9]{1,4}$") || Regex.IsMatch(txtX.Text.Trim(), "^([0-9]{1,})+(.[0-9]{1,2})$")))
                {
                    MessageBox.Show("输入错误，请检查后重新输入！", "WARN");
                    return;
                }
                if (!(Regex.IsMatch(txtSD.Text.Trim(), "^[0-9]{1,4}$") || Regex.IsMatch(txtSD.Text.Trim(), "^([0-9]{1,})+(.[0-9]{1,2})$")))
                {
                    MessageBox.Show("输入错误，请检查后重新输入！", "WARN");
                    return;
                }
            }
            btnGenerate.Enabled = false;
            groupBox2.Enabled = false;

            #region  获得数据
            //流程
            #region 流程
            string itemName = reagentName.Text;

            DbHelperOleDb db = new DbHelperOleDb(0);
            string sql = "select ProjectNumber from tbProject where ShortName = '" + itemName + "'";
            string projectNumber = DbHelperOleDb.Query(0,sql).Tables[0].Rows[0][0].ToString();
            while (projectNumber.Length < 3)
            {
                projectNumber = projectNumber.Insert(0, "0");
            }
            List<string> Slist = new List<string>();
            Slist.Add("2");
            Slist.Add(projectNumber);
            if (true) //仅修改参数
            {
                DataTable dtStep = new DataTable();
                dtStep.Columns.Add("step", typeof(string));
                dtStep.Columns.Add("para", typeof(string));
                dtStep.Columns.Add("unit", typeof(string));
                db = new DbHelperOleDb(0);
                sql = "select ProjectProcedure from tbProject where ShortName = '" + itemName + "'";
                string[] Procedure;
                DataTable dtProcedure = DbHelperOleDb.Query(0,sql).Tables[0];
                Procedure = dtProcedure.Rows[0][0].ToString().Split(';');
                //数据库存在当前项目
                if (Procedure.Length > 0)
                {
                    foreach (var proce in Procedure)
                    {
                        string[] steps = proce.Split('-');
                        dtStep.Rows.Add(steps[0], steps[1], steps[2]);
                    }
                }
                //f表示不修改实验流程，t表示修改实验流程
                Slist.Add("f");
                #region 生成过程
                //遍历dgvProdure数据，并将数据存储生成实验流程
                for (int i = 0; i < dtStep.Rows.Count; i++)
                {
                    string tempStep = dtStep.Rows[i][0].ToString();
                    string temPara = dtStep.Rows[i][1].ToString();
                    int tempIntPara;
                    string tempResult = "";
                    string unit = dtStep.Rows[i][2].ToString();
                    //有关每个步骤的具体占据位置和长度已经在文档中具体说明
                    switch (tempStep)
                    {
                        case "S":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 5;
                            tempResult = tempIntPara.ToString();
                            while (tempResult.Length < 2)
                            {
                                tempResult = tempResult.Insert(0, "0");
                            }
                            Slist.Add(tempResult);
                            break;
                        case "R1":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 10;
                            tempResult = tempIntPara.ToString();
                            while (tempResult.Length < 2)
                            {
                                tempResult = tempResult.Insert(0, "0");
                            }
                            Slist.Add(tempResult);
                            break;
                        case "R2":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 10;
                            tempResult = tempIntPara.ToString();
                            while (tempResult.Length < 2)
                            {
                                tempResult = tempResult.Insert(0, "0");
                            }
                            Slist.Add(tempResult);
                            break;
                        case "R3":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 10;
                            tempResult = tempIntPara.ToString();
                            while (tempResult.Length < 2)
                            {
                                tempResult = tempResult.Insert(0, "0");
                            }
                            Slist.Add(tempResult);
                            break;
                        case "R4":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 10;
                            tempResult = tempIntPara.ToString();
                            while (tempResult.Length < 2)
                            {
                                tempResult = tempResult.Insert(0, "0");
                            }
                            Slist.Add(tempResult);
                            break;
                        case "H":
                            if (unit == "min")
                            {
                                tempIntPara = int.Parse(temPara);
                                tempIntPara *= 60;
                                tempResult = Convert.ToString(tempIntPara, 16);
                                while (tempResult.Length < 3)
                                {
                                    tempResult = tempResult.Insert(0, "0");
                                }
                            }
                            else if (unit == "s")
                            {
                                tempIntPara = int.Parse(temPara);
                                tempResult = Convert.ToString(tempIntPara, 16);
                                while (tempResult.Length < 3)
                                {
                                    tempResult = tempResult.Insert(0, "0");
                                }
                            }
                            else
                            {
                                MessageBox.Show("只允许单位为min（分钟）和s（秒）");
                                return;
                            }
                            Slist.Add(tempResult);
                            break;
                        case "B":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 10;
                            tempResult = tempIntPara.ToString();
                            Slist.Add(tempResult);
                            break;
                        case "W":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 100;
                            tempResult = tempIntPara.ToString();
                            Slist.Add(tempResult);
                            break;
                        case "T":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 100;
                            tempResult = tempIntPara.ToString();
                            Slist.Add(tempResult);
                            break;
                        case "D":
                            tempIntPara = int.Parse(temPara);
                            tempIntPara = tempIntPara / 10;
                            tempResult = tempIntPara.ToString();
                            Slist.Add(tempResult);
                            break;
                    }
                }
                #endregion
            }

            StringBuilder sb = new StringBuilder();
            foreach (var para in Slist)
            {
                sb.Append(para);
            }
            #endregion
            string encryPro = Utils.instance.ToEncryption(sb.ToString());//流程密文

            //浓度
            #region 浓度
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            //条码序号
            sb1.Append("3");
            //生成定标数据和条码
            sb1.Append("A");
            sb1.Append(dtConcAndValue.Rows[0][1].ToString());
            sb1.Append("f");

            sb1.Append("B");
            sb1.Append(dtConcAndValue.Rows[1][1].ToString());
            sb1.Append("t");

            sb1.Append("C");
            sb1.Append(dtConcAndValue.Rows[2][1].ToString());
            sb1.Append("f");

            sb1.Append("D");
            sb1.Append(dtConcAndValue.Rows[3][1].ToString());
            sb1.Append("f");

            //条码序号
            sb2.Append("4");
            sb2.Append("E");
            sb2.Append(dtConcAndValue.Rows[4][1].ToString());
            sb2.Append("t");

            sb2.Append("F");
            sb2.Append(dtConcAndValue.Rows[5][1].ToString());
            sb2.Append("f");

            if (dtConcAndValue.Rows.Count > 6)
            {
                sb2.Append("G");
                sb2.Append(dtConcAndValue.Rows[6][1].ToString());
                sb2.Append("f");
            }
            //浓度密文    
            string encry1 = Utils.instance.ToEncryption(sb1.ToString());
            string encry2 = Utils.instance.ToEncryption(sb2.ToString());
            #endregion
            string[] encryConc = new string[2] { encry1, encry2 };

            //发光值
            #region 发光值
            StringBuilder sbv1 = new StringBuilder();
            StringBuilder sbv2 = new StringBuilder();
            StringBuilder sbv3 = new StringBuilder();
            StringBuilder sbv4 = new StringBuilder();

            sbv1 = DecimalTo16("5", dtConcAndValue.Rows[0][2].ToString().Trim(), "B", dtConcAndValue.Rows[1][2].ToString().Trim());
            sbv2 = DecimalTo16("6", dtConcAndValue.Rows[2][2].ToString(), "D", dtConcAndValue.Rows[3][2].ToString());
            sbv3 = DecimalTo16("7", dtConcAndValue.Rows[4][2].ToString(), "F", dtConcAndValue.Rows[5][2].ToString());
            string value1 = Utils.instance.ToEncryption(sbv1.ToString());
            string value2 = Utils.instance.ToEncryption(sbv2.ToString());
            string value3 = Utils.instance.ToEncryption(sbv3.ToString());
            string value4 = "";

            if (dtConcAndValue.Rows.Count > 6)
            {
                sbv4.Append(8);
                sbv4.Append(Convert.ToString(int.Parse(dtConcAndValue.Rows[6][2].ToString()), 16));

                while (sbv4.Length < 8)
                {
                    sbv4.Insert(1, "0");
                }
                value4 = Utils.instance.ToEncryption(sbv4.ToString());
            }
            #endregion
            string[] encryValue = new string[4] { value1, value2, value3, value4 };
            //质控配套
            #region 质控
            string encryQC = "";
            if (cbReadyQC.Checked)
            {
                string strNo1 = "9"; //条码序号
                string strItemNum = ""; //项目名称
                string strDate = ""; //生产日期
                string strX = ""; //质控靶值
                string strSD = ""; //质控标准差
                string strLevel = ""; //质控类别
                string strRule = ""; //质控规则

                //查询表中对应的项目编号
                DataRow[] dr = dtPro.Select("ShortName = '" + reagentName.Text.Trim() + "'");  //将下拉框中选中的项目信息导入行dr
                if (dr.Length > 0)  //行内元素总数
                {
                    string projectNum = dr[0]["ProjectNumber"].ToString();  //获取项目编号
                    while (projectNum.Length < 3)
                    {
                        projectNum = projectNum.Insert(0, "0");  //当项目编号小于三位，向前面填充0
                    }
                    strItemNum = projectNum;
                }
                else
                {
                    MessageBox.Show("当前实验项目不存在!");
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
                    strSD = int.Parse(tempSD[0]).ToString("X2") + "00";
                else
                    strSD = int.Parse(tempSD[0]).ToString("X2") + int.Parse(tempSD[1]).ToString("X2");

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
                encryQC = Utils.instance.ToEncryption(originalStr);                
            }
            #endregion
            #endregion
            //得到指令列表
            int DValue = num_2 - num_1;
            if (!cbReadyQC.Checked)
            {
                for (int temporary = 0; temporary <= DValue; temporary++)
                {
                    string oristr = ToGetOriginalString(temporary);
                    string encrystr = Utils.instance.ToEncryption(oristr); //加密后
                    SpCode sp = new SpCode(encrystr, encryPro, encryConc, encryValue);
                    spList.Add(sp);
                }
            }
            else
            {
                for (int temporary = 0; temporary <= DValue; temporary++)
                {
                    string oristr = ToGetOriginalString(temporary);
                    string encrystr = Utils.instance.ToEncryption(oristr); //加密后
                    SpCode sp = new SpCode(encrystr, encryPro, encryConc, encryValue, encryQC);
                    spList.Add(sp);
                }
            }
            string surplus = spList.Count.ToString();
            string qcReady = cbReadyQC.Checked ? "已配备质控" : "无配备质控";
            LogFile.Instance.Write(DateTime.Now.ToString("HH:mm:ss:fff") + " 生成成功, 剩余:" + surplus);
            label6.Text = "剩余：" + surplus + "("+ qcReady + ")";
            label7.Text = "下一个批内流水号：" + serialNum;
            btnGenerate.Enabled = true;
        }

        StringBuilder DecimalTo16(string index1, string LightValue1, string ValueLocation2, string LightValue2)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            if (LightValue1 != null && LightValue1 != "")
            {
                //发光值的第一个条码
                sb.Append(index1);
                //取AB发光值并转换成16进制
                string[] array1 = LightValue1.Split('.');
                //sb.Append(ValueLocation1);
                if (array1.Length > 1)
                {
                    //整数和小数部分分别转换成16进制并添加到StringBuilder中
                    sb.Append(Convert.ToString(int.Parse(array1[0]), 16));
                    sb.Append(".");
                    sb.Append(Convert.ToString(int.Parse(array1[1]), 16));

                }
                else if (array1.Length == 1)
                {
                    //只有整数部分
                    sb.Append(Convert.ToString(int.Parse(array1[0]), 16));
                }
                while (sb.Length < 8)
                {
                    sb.Insert(1, "0");
                }

                //取AB发光值并转换成16进制
                string[] array2 = LightValue2.Split('.');
                //sb2.Append(ValueLocation2);
                if (array2.Length > 1)
                {
                    //整数和小数部分分别转换成16进制并添加到StringBuilder中
                    sb2.Append(Convert.ToString(int.Parse(array2[0]), 16));
                    sb2.Append(".");
                    sb2.Append(Convert.ToString(int.Parse(array2[1]), 16));

                }
                else if (array2.Length == 1)
                {
                    //只有整数部分
                    sb2.Append(Convert.ToString(int.Parse(array2[0]), 16));
                }
                while (sb2.Length < 7)
                {
                    sb2.Insert(0, "0");
                }
            }
            else
            {
                MessageBox.Show("发光值不能为空！");
            }
            return sb.Append(sb2);
        }

        private string ToGetOriginalString(decimal? add = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1");//条码编号，表示条码1
            string time = string.Format("{0:yyyyMMdd}", prodectTime.Value);//8位时间
            string timeYear = time.Substring(0, 4);
            string timeDay = time.Substring(6, 2);
            //将四位数年份和四位数月日异或，取较低两位作为校验位

            string checkNum = (int.Parse(timeYear.Substring(2, 2)) ^ int.Parse(timeDay)).ToString(); // 这是把年的后两位和日的两位异或
            if (checkNum.Length < 2)
            {
                while (checkNum.Length < 2)
                {
                    checkNum = checkNum.Insert(0, "0");
                }
            }
            sb.Append(checkNum);        //添加两位的校验码 到sb
            //查询表中对应的项目编号
            DataRow[] dr = dtPro.Select("ShortName = '" + reagentName.Text.Trim() + "'");  //将下拉框中选中的项目信息导入行dr
            if (dr.Length > 0)  //行内元素总数
            {
                string projectNum = dr[0]["ProjectNumber"].ToString();  //获取项目编号
                while (projectNum.Length < 3)
                {
                    projectNum = projectNum.Insert(0, "0");  //当项目编号小于三位，向前面填充0
                }
                sb.Append(projectNum);      //添加三位的编号 到sb
            }
            else
            {
                MessageBox.Show("当前实验项目不存在!");
            }
            sb.Append(TimeToNewTime(time));     //将time日期转换为三位数，添加到sb字符串序列
            string num = Convert.ToString(num1.Value + add);
            while (num.Length < 4)              //如果num1控件内的数字位数不足4位，则前面填充0
            {
                num = num.Insert(0, "0");
            }
            string allTest = (100 / 10).ToString();//10倍乘表示测数   、、comboBox 100测/10 
            //MessageBox.Show(allTest);
            while (allTest.ToString().Length < 2)      //测数小于两位前面填充0
            {
                allTest = allTest.Insert(0, "0");
            }
            sb.Append(allTest);         //将测数（两位）添加到sb后面
            sb.Append(num);             //将编号（四位）添加到sb后面

            return sb.ToString();
        }

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

        private void reagentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reagentName.SelectedIndex < 0)
                return;
            DataTable temp = dtConcAndValue.Clone();
            dgvScaling.DataSource = temp;
            dtConcAndValue.Clear();

            //改变dtConcAndValue的行数，6点6行，7点7行
            BLL.tbProject bllProject = new BLL.tbProject();
            DbHelperOleDb db = new DbHelperOleDb(0);
            string itemName = reagentName.Text;

            DataTable dt = bllProject.GetList(" ShortName='" + itemName + "'").Tables[0];

            //根据几点定标，来生成几行
            string calPonitNum = dt.Rows[0]["CalPointNumber"].ToString();

            int calPN = Convert.ToInt32(calPonitNum);
            for (int i = 0; i < calPN; i++)
            {
                dtConcAndValue.Rows.Add(i + 1, dt.Rows[0]["CalPointConc"].ToString().Split(',')[i], "");
            }

            dgvScaling.DataSource = dtConcAndValue;
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            //循环将SpList中的SpCode类对象发出去
            if (spList.Count < 1)
                return;
            btnInput.Enabled = false;
            int num = dtConcAndValue.Rows.Count;// 6 or 7
            int listCount = spList.Count;

            NetCom3.Instance.ReceiveHandel += dealSpInput;
            #region 屏蔽 循环录入
            //for (int i = 0; i < listCount; i++)
            //{
            //    SpCode sp = spList.First();
            //    string reagentInfo = sp.getReagentInfo();
            //    string pro = sp.getProjectFlow();
            //    string conc1 = sp.getScaling_1();
            //    string conc2 = sp.getScaling_2();
            //    string value1 = sp.getValue_1();
            //    string value2 = sp.getValue_2();
            //    string value3 = sp.getValue_3();
            //    string value4 = "";
            //    if (num == 7)
            //        value4 = sp.getValue_4();

            //    if (!spInfoSendInput(reagentInfo, "11"))
            //    {
            //        MessageBox.Show("指令发送失败");
            //        return;
            //    }
            //    if (!spInfoSendInput(pro, "12"))
            //    {
            //        MessageBox.Show("指令发送失败");
            //        return;
            //    }
            //    if (!spInfoSendInput(conc1, "13"))
            //    {
            //        MessageBox.Show("指令发送失败");
            //        return;
            //    }
            //    if (!spInfoSendInput(conc2, "14"))
            //    {
            //        MessageBox.Show("指令发送失败");
            //        return;
            //    }
            //    if (!spInfoSendInput(value1, "15"))
            //    {
            //        MessageBox.Show("指令发送失败");
            //        return;
            //    }
            //    if (!spInfoSendInput(value2, "16"))
            //    {
            //        MessageBox.Show("指令发送失败");
            //        return;
            //    }
            //    if (!spInfoSendInput(value3, "17"))
            //    {
            //        MessageBox.Show("指令发送失败");
            //        return;
            //    }
            //    if (value4 != "")
            //    {
            //        if (!spInfoSendInput(value4, "18"))
            //        {
            //            MessageBox.Show("指令发送失败");
            //            return;
            //        }
            //    }
            //    spList.RemoveAt(0);
            //    label6.Text = "剩余：" + spList.Count.ToString();
            //    if (spList.Count > 0)
            //        MessageBox.Show("录入成功 " + (i + 1) + " 次。\n请放置好下一个标签点击确定进行录入", "弹窗");
            //    else
            //        MessageBox.Show("录入成功!\n录入成功 " + (i + 1) + " 次。", "弹窗");
            //}
            #endregion
            #region 单次录入
            SpCode sp = spList.First();
            string reagentInfo = sp.getReagentInfo();
            string pro = sp.getProjectFlow();
            string conc1 = sp.getScaling_1();
            string conc2 = sp.getScaling_2();
            string value1 = sp.getValue_1();
            string value2 = sp.getValue_2();
            string value3 = sp.getValue_3();
            string value4 = "";
            string qc = sp.getQualityControls();
            if (num == 7)
                value4 = sp.getValue_4();

            if (!spInfoSendInput(reagentInfo, "11"))
            {
                goto errorEnd;
            }
            if (!spInfoSendInput(pro, "12"))
            {
                goto errorEnd;
            }
            if (!spInfoSendInput(conc1, "13"))
            {
                goto errorEnd;
            }
            if (!spInfoSendInput(conc2, "14"))
            {
                goto errorEnd;
            }
            if (!spInfoSendInput(value1, "15"))
            {
                goto errorEnd;
            }
            if (!spInfoSendInput(value2, "16"))
            {
                goto errorEnd;
            }
            if (!spInfoSendInput(value3, "17"))
            {
                goto errorEnd;
            }
            if (value4 != "")
            {
                if (!spInfoSendInput(value4, "18"))
                {
                    goto errorEnd;
                }
            }
            if (qc != "")
            {
                if (!spInfoSendInput(qc, "19"))
                {
                    goto errorEnd;
                }
            }
            spList.RemoveAt(0);
            int surplus = spList.Count;
            string qcReady = qc!="" ? "已配备质控" : "无配备质控";
            LogFile.Instance.Write(DateTime.Now.ToString("HH:mm:ss:fff") + " 录入成功, 剩余:" + surplus);            
            label6.Text = "剩余：" + surplus + "(" + qcReady + ")";
            label7.Text = "下一个批内流水号：" + (surplus == 0 ? "无" : (++serialNum).ToString());
            if (spList.Count > 0)
            {                
                MessageBox.Show("录入成功,剩余 " + surplus + " 。\n请放置好下一个标签进行下一次录入", "弹窗");
            }
            else
            {
                MessageBox.Show("录入成功!", "弹窗");
            }            
            #endregion
            errorEnd:
            NetCom3.Instance.ReceiveHandel -= dealSpInput;
            btnInput.Enabled = true;
        }

        private bool spInfoSendInput(string code, string tpye)
        {
            byte[] tempByte = System.Text.Encoding.Default.GetBytes(code.ToString());
            //string content = "EB 90 CA 11 " + code.Length.ToString("X2");
            string content = "EB 90 CA " + tpye + " " + code.Length.ToString("X2");
            foreach (byte byte1 in tempByte)
            {
                content += " " + byte1.ToString("x2");
            }
            spSendFlag = 0;//readySend
            NetCom3.Instance.Send(NetCom3.Cover(content), 5);
            if (!NetCom3.Instance.SingleQuery() && NetCom3.Instance.errorFlag != (int)ErrorState.ReadySend)
            {
                MessageBox.Show("指令发送失败");
                MessageBox.Show(Enum.GetName(typeof(ErrorState), NetCom3.Instance.errorFlag));          
                return false ;
            }
            while (spSendFlag == 0)
            {
                NetCom3.Delay(10);
            }
            if (spSendFlag == 2)
            {
                MessageBox.Show("指令录入失败");
                return false;

            }
            return true;
        }
        private void dealSpInput(string order)
        {
            if (order.Contains("EB 90 CA"))
            {
                string tempOrder = order.Trim().Replace(" " , "");
                string flag = tempOrder.Substring(8 , 2);
                if (flag == "FF")
                {
                    spSendFlag = 1;//success
                }
                else if (flag == "00")
                {
                    spSendFlag = 2;//false
                }
            }
        }

        private void frmElectronicLabelEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!closedFlag)
            {
                FrmMain.f0.Close();
                System.Environment.Exit(0);
            }
        }

        private void cbReadyQC_CheckedChanged(object sender, EventArgs e)
        {
            if (cbReadyQC.Checked)
            {
                groupBox2.Enabled = true;
            }
            else
            {
                groupBox2.Enabled = false;
            }
        }
    }
}

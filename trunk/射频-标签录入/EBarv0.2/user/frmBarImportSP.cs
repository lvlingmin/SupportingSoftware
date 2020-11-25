using EBarv0._2.Common;
using EBarv0._2.CustomControl;
using EBarv0._2.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace EBarv0._2.user
{
    public partial class frmBarImportSP : frmParent
    {
        bool closedFlag = false;
        int spSendFlag = 0;
        string qcOrder1 = "";
        string qcOrder2 = "";
        DataTable dtPro;
        DataTable dtQc = new DataTable();
        BarCodeHook barCodeHook = new BarCodeHook();
        private BLL.tbProject bllP = new BLL.tbProject();
        frmMessageShow MsgShow = new frmMessageShow();
        System.Windows.Forms.Timer timerCheckConn = new System.Windows.Forms.Timer();
        public frmBarImportSP()
        {
            InitializeComponent();
        }

        private void frmBarImportSP_Load(object sender, EventArgs e)
        {
            string sql = "select ProjectNumber ,ShortName,ProjectProcedure from tbProject"; //查询语句
            dtPro = DbHelperOleDb.Query(0, sql).Tables[0];  //获取需要的数据库表赋值给dt   
            this.cmbReagentName.DataSource = dtPro;   //下拉框comboBox设置数据源为tbProject表
            this.cmbReagentName.DisplayMember = "ShortName";  //comboBox设置显示列，列名为项目简称
            this.cmbReagentName.ValueMember = "ProjectNumber"; //把表中的项目编号属性，设置为comboBox中选项的实际值
            this.cmbReagentName.SelectedIndex = 0;

            dtQc.Columns.Add("qcNum",typeof(string));
            dtQc.Columns.Add("qcItem",typeof(string));
            dtQc.Columns.Add("qcTime", typeof(string));
            dtQc.Columns.Add("qcX", typeof(string));
            dtQc.Columns.Add("qcSd", typeof(string));
            dgvQc.DataSource = dtQc;

            timerCheckConn.Interval = 5000;
            timerCheckConn.Tick += new System.EventHandler(checkConn);
            timerCheckConn.Start();

            barCodeHook.BarCodeEvent += new BarCodeHook.BarCodeDelegate(BarCode_BarCodeEvent);
            
        }
        /// <summary>
        /// 钩子回调方法 j
        /// </summary>
        /// <param name="barCode">条码</param>
        void BarCode_BarCodeEvent(BarCodeHook.BarCodes barCode)
        {
            HandleBarCode(barCode);
        }
        private delegate void ShowInfoDelegate(BarCodeHook.BarCodes barCode);
        /// <summary>
        /// 扫码信息处理函数 j
        /// </summary>
        /// <param name="barCode">条码</param>
        private void HandleBarCode(BarCodeHook.BarCodes barCode)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowInfoDelegate(HandleBarCode), new object[] { barCode });
            }
            else
            {
                if (barCode.IsValid)
                {
                    if (Utils.instance.CheckFormIsOpen("frmMessageShow"))
                    {
                        return;
                    }
                    initTextBox();
                    //使用一个正则，使得里面的空格，制表符等去除,把信息写到条码框里
                    string rgCode = Regex.Replace(barCode.BarCode, @"\s", "");
                    if (rgCode != null && rgCode != "")
                    {
                        txtBarCode.Text = rgCode;
                    }
                }
            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            closedFlag = true;
            frmUser.f0.Show();
            this.Close();
        }

        private void btnStartBar_Click(object sender, EventArgs e)
        {
            if(cmbReagentName.SelectedIndex < 0)
            {
                MsgShow.MessageShow("射频录入","请选择项目名称！");
                return;
            }
            if(dtQc.Rows.Count < 2)
            {
                MsgShow.MessageShow("射频录入", "请填写质控信息！");
                return;
            }
            if(dtQc.Rows[0]["qcItem"].ToString() != cmbReagentName.Text || dtQc.Rows[1]["qcItem"].ToString() != cmbReagentName.Text)
            {
                MsgShow.MessageShow("射频录入", "质控项目不统一，请检查后重新输入！");
                return;
            }
            btnQc1.Enabled = btnQc2.Enabled = cmbReagentName.Enabled = btnStartBar.Enabled = false;
            btnCloseBar.Enabled = true;
            barCodeHook.Start();
        }

        private void btnCloseBar_Click(object sender, EventArgs e)
        {
            btnQc1.Enabled = btnQc2.Enabled = cmbReagentName.Enabled = btnStartBar.Enabled = true;
            btnCloseBar.Enabled = false;
            barCodeHook.Stop();
        }

        private void frmBarImportSP_FormClosed(object sender, FormClosedEventArgs e)
        {
            closedFlag = true;
            frmUser.f0.Show();
            this.Close();
        }

        private void btnQc1_Click(object sender, EventArgs e)
        {
            Admin.frmQualityControl f = new Admin.frmQualityControl("frmElectronicLabelEntry_sp1", DateTime.Now, cmbReagentName.SelectedIndex);
            f.ShowDialog();
            if (Admin.frmQualityControl.spQcOrder != "")
            {
                qcOrder1 = Admin.frmQualityControl.spQcOrder;
            }
            updateTable();
        }

        private void btnQc2_Click(object sender, EventArgs e)
        {
            Admin.frmQualityControl f = new Admin.frmQualityControl("frmElectronicLabelEntry_sp2", DateTime.Now, cmbReagentName.SelectedIndex);
            f.ShowDialog();
            if (Admin.frmQualityControl.spQcOrder != "")
            {
                qcOrder2 = Admin.frmQualityControl.spQcOrder;
            }
            updateTable();
        }

        private void updateTable()
        {
            //dgv clear
            dtQc.Clear();
            //qc1
            if (Admin.frmQualityControl.spQcLow[0] != "" && Admin.frmQualityControl.spQcLow[0] != null)
            {
                //add
                dtQc.Rows.Add("质控1", Admin.frmQualityControl.spQcLow[2], Admin.frmQualityControl.spQcDate[0].ToString(@"yyyy/MM/dd"), Admin.frmQualityControl.spQcLow[0], Admin.frmQualityControl.spQcLow[1]);
            }
            //qc2
            if (Admin.frmQualityControl.spQcHigh[0] != "" && Admin.frmQualityControl.spQcHigh[0] != null)
            {
                //add
                dtQc.Rows.Add("质控2", Admin.frmQualityControl.spQcHigh[2], Admin.frmQualityControl.spQcDate[1].ToString(@"yyyy/MM/dd"), Admin.frmQualityControl.spQcHigh[0], Admin.frmQualityControl.spQcHigh[1]);
            }
            //Sort
            DataView dv = dtQc.DefaultView;
            dv.Sort = "qcNum";
            dtQc = dv.ToTable();
            dgvQc.DataSource = dtQc;
        }
        
        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            if (txtBarCode.Text == "")
            {
                return;
            }
            //check
            if (!judgeBarCode(txtBarCode.Text))
            {
                MsgShow.MessageShow("射频录入", "未通过条码校验！");
                return;
            }
            //fill
            if(!fillBarCode(txtBarCode.Text))
            {
                MsgShow.MessageShow("射频录入", "扫描失败，请重新扫描！");
                return;
            }
            if(cmbReagentName.Text != txtItemName.Text)
            {
                //BeginInvoke(new Action(() =>
                //{
                //    MsgShow.MessageShow("射频录入", "条码项目不匹配，请检查后重新扫描！");
                //}));
                MsgShow.MessageShow("射频录入", "条码项目不匹配，请检查后重新扫描！");

                return;
            }
            IapProBar.Visible = true;
            IapProBar.Value = 10;
            //send
            NetCom3.Instance.ReceiveHandel += dealSpInput;
            if (!spInfoSendInput(txtBarCode.Text, "11"))
            {
                goto errorEnd;
            }
            IapProBar.Value = 40;
            if (!spInfoSendInput(qcOrder1, "19"))
            {
                goto errorEnd;
            }
            IapProBar.Value = 80;
            if (!spInfoSendInput(qcOrder2, "1A"))
            {
                goto errorEnd;
            }
            IapProBar.Value = 100;
            MsgShow.MessageShow("射频录入", "录入成功!");
        errorEnd:
            NetCom3.Instance.ReceiveHandel -= dealSpInput;
            IapProBar.Visible = false;
            //if(!sendSp())
            //{
            //    MsgShow.MessageShow("error");
            //    return;
            //}
        }

        private bool judgeBarCode(string code)
        {
            string decryption = Utils.instance.ToDecryption(code);
            if (decryption.Substring(0, 1) != "1")
            {
                return false;
            }
            string productDay = decryption.Substring(6, 3);
            string countCheckNum = getCheckNum(productDay);
            string checkNum = decryption.Substring(1, 2);
            if (checkNum != countCheckNum) //计算得到的校验位和明文校验位不相等    
            {
                return false;
            }
            return true;
        }

        private bool fillBarCode(string code)
        {
            string[] tempBar = dealBarCode(code);
            Invoke(new Action(()=>
            {
                txtItemName.Text = tempBar[0];
                txtProductionDate.Text = tempBar[1];
                txtTestNum.Text = tempBar[2];
                txtNum.Text = tempBar[3];
            }));

            return true;
        }

        private void initTextBox()
        {
            Invoke(new Action(() =>
            {
                txtBarCode.Text = "";
                txtItemName.Text = "";
                txtProductionDate.Text = "";
                txtTestNum.Text = "";
                txtNum.Text = "";
            }));
        }

        private void dealSpInput(string order)
        {
            if (order.Contains("EB 90 CA"))
            {
                string tempOrder = order.Trim().Replace(" ", "");
                string flag = tempOrder.Substring(8, 2);
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
                MsgShow.MessageShow("射频录入", "指令发送失败," + Enum.GetName(typeof(ErrorState), NetCom3.Instance.errorFlag));
                return false;
            }
            while (spSendFlag == 0)
            {
                NetCom3.Delay(10);
            }
            if (spSendFlag == 2)
            {
                MsgShow.MessageShow("射频录入", "指令录入失败");
                return false;

            }
            return true;
        }
        /// <summary>
        /// 返回校验位
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private string getCheckNum(string number)
        {
            string year = "";
            string month = "";
            string day = "";
            char[] chYear = number.Substring(0, 1).ToCharArray();
            char[] chMonth = number.Substring(1, 1).ToCharArray();
            char[] chDay = number.Substring(2, 1).ToCharArray();
            if (chYear[0] >= '0' && chYear[0] <= '9')
            {
                year = chYear[0].ToString();
            }
            else
            {
                year = ((chYear[0] - 'A') + 10).ToString();
            }
            if (chMonth[0] >= '0' && chMonth[0] <= '9')
            {
                month = chMonth[0].ToString();
            }
            else
            {
                month = ((chMonth[0] - 'A') + 10).ToString();
            }

            if (chDay[0] >= '0' && chDay[0] <= '9')
            {
                day = chDay[0].ToString();
            }
            else
            {
                day = ((chDay[0] - 'A') + 10).ToString();
            }
            string checkNum = (int.Parse(year) ^ int.Parse(day)).ToString();
            while (checkNum.Length < 2)
            {
                checkNum = checkNum.Insert(0, "0");
            }
            return checkNum;
        }
        private string[] dealBarCode(string rgcode)
        {
            string decryption = Utils.instance.ToDecryption(rgcode);
            string productDay = decryption.Substring(6, 3);

            string rgNameCode = decryption.Substring(3, 3);//试剂编号
            //去数据库查询编号对应的短名
            DataTable dtAll = bllP.GetAllList().Tables[0];
            string shortName =
                dtAll.Select("ProjectNumber ='" + int.Parse(rgNameCode).ToString() + "'")[0]["ShortName"].ToString();
            if (shortName == null && shortName == "")
            {
                return null;
            }
            string year = "";
            string month = "";
            string day = "";
            year = reverseDate(productDay.Substring(0, 1).ToCharArray()[0]);
            month = reverseDate(productDay.Substring(1, 1).ToCharArray()[0]);
            day = reverseDate(productDay.Substring(2, 1).ToCharArray()[0]);
            while (year.Length < 4)
            {
                year = year.Insert(0, "20");
            }
            while (month.Length < 2)
            {
                month = month.Insert(0, "0");
            }
            while (day.Length < 2)
            {
                day = day.Insert(0, "0");
            }
            string testTimes = (int.Parse(decryption.Substring(9, 2)) * 10).ToString();//测试
            string rgNums = decryption.Substring(11);//流水编号
            //return shortName + "?" /*+ shortName*/ + year + month + day + "?" + testTimes;
            return new string[4] { shortName, year +"-"+ month +"-"+ day, testTimes ,rgNums};
        }
        private string reverseDate(char oriDate)
        {
            string date = "";
            if (oriDate >= '0' && oriDate <= '9')
            {
                date = oriDate.ToString();
            }
            else
            {
                date = ((oriDate - 'A') + 10).ToString();
            }
            return date;
        }

        private void checkConn(object sender, EventArgs e)
        {
            //if (NetCom3.Instance.CheckMyIp_Port_Link())
            if(!NetCom3.Instance.CheckNetWorkLink())
            {
                timerCheckConn.Stop();
                MessageBox.Show("网络连接中断！", "连接中断");

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.Name == "frmMessageShow")
                    {
                        frm.Close();
                    }
                }

                Environment.Exit(0);
            }
        }

    }
}

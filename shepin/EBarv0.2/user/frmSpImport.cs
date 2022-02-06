using EBarv0._2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EBarv0._2.user
{
    public partial class frmSpImport : frmParent
    {
        bool closedFlag = false;
        int spSendFlag = 0;
        DataTable dtable = new DataTable();
        
        public frmSpImport()
        {
            InitializeComponent();
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            btnImportFile.Enabled = false;
            btnInNext.Enabled = false;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            dialog.Filter = "xls文件|*.xls";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = dialog.FileName;
                DataTable dt = OperateExcel.ImPortExcel(filePath);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("导入的数据为空！", "");
                    return;
                }
                dtable = dt;
                dgvSp.DataSource = dtable;
                MessageBox.Show("导入完成！", "");
            }
            label6.Text = "剩余：" + dtable.Rows.Count;
            btnInNext.Text = "开始录入";
            btnInNext.Enabled = true;
            btnImportFile.Enabled = true;
        }

        private void btnInNext_Click(object sender, EventArgs e)
        {
            //循环将SpList中的SpCode类对象发出去
            if (dgvSp.Rows.Count < 2)
            {
                return;
            }
            btnInNext.Text = "录入下一个";
            LogFile.Instance.Write(DateTime.Now.ToString("HH:mm:ss:fff") + " 开始录入, 当前id:" + dtable.Rows[0][0].ToString());
            btnInNext.Enabled = false;
            int num = dgvSp.Rows[0].Cells[8].Value.ToString() == "" ? 6 : 7;// 6 or 7
            //int listCount = spList.Count;

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
            //SpCode sp = spList.First();
            string reagentInfo = dtable.Rows[0][1].ToString();
            string pro = dtable.Rows[0][2].ToString();
            string conc1 = dtable.Rows[0][3].ToString();
            string conc2 = dtable.Rows[0][4].ToString();
            string value1 = dtable.Rows[0][5].ToString();
            string value2 = dtable.Rows[0][6].ToString();
            string value3 = dtable.Rows[0][7].ToString();
            string value4 = "";
            string qc1 = dtable.Rows[0][9].ToString();
            string qc2 = dtable.Rows[0][10].ToString();
            string itemXml = dtable.Rows[0][11].ToString();
            if (num == 7)
                value4 = dtable.Rows[0][8].ToString();

            if (!spInfoSendInput(reagentInfo, "11"))
            {
                goto errorEnd;
            }
            if(pro != "" && pro != null)
            {
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
                if (qc1 != "")
                {
                    if (!spInfoSendInput(qc1, "19"))
                    {
                        goto errorEnd;
                    }
                }
                if (qc2 != "")
                {
                    if (!spInfoSendInput(qc2, "1A"))
                    {
                        goto errorEnd;
                    }
                }
                if(itemXml != "")
                {
                    if (!spInfoSendInput(itemXml, "1B"))
                    {
                        goto errorEnd;
                    }
                }
            }
            dtable.Rows.RemoveAt(0);
            int surplus = dtable.Rows.Count;
            //string qcReady = qc!="" ? "已配备质控" : "无配备质控";
            LogFile.Instance.Write(DateTime.Now.ToString("HH:mm:ss:fff") + " 录入成功, 剩余:" + surplus);
            label6.Text = "剩余：" + surplus /*+ "(" + qcReady + ")"*/;
            if (surplus > 0)
            {
                MessageBox.Show("录入成功,剩余 " + surplus + " 。\n请放置好下一个标签进行下一次录入", "");
            }
            else
            {
                MessageBox.Show("录入成功!", "");
            }
            #endregion
            errorEnd:
            NetCom3.Instance.ReceiveHandel -= dealSpInput;
            btnInNext.Enabled = true;
        }

        private bool spInfoSendInput(string code, string tpye)
        {
            byte[] tempByte = System.Text.Encoding.Default.GetBytes(code.ToString());
            string codeLength = code.Length.ToString("X2"); ;
            //string content = "EB 90 CA 11 " + code.Length.ToString("X2");
            if(tpye =="1B")
                codeLength = tempByte.Length.ToString("X4").Insert(2," ");
            string content = "EB 90 CA " + tpye + " " + codeLength;
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
                return false;
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

        private void btnback_Click(object sender, EventArgs e)
        {
            closedFlag = true;
            frmUser.f0.Show();
            this.Close();
        }

        private void frmSpImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!closedFlag)
            {
                frmUser.f0.Close();
                System.Environment.Exit(0);
            }
        }
    }
}

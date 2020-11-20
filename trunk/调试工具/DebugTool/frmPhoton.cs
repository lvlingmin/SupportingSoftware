using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebugTool
{
    public partial class frmPhoton : Form
    {
        #region 参数
        private bool RotateNumHasText = false;
        private bool StepOrderHasText = false;
        private bool ReadNumHasText = false;
        #endregion

        public frmPhoton()
        {
            InitializeComponent();
        }

        private void CmbWashPara_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                MessageBox.Show("仪器正在运动，请稍等！", "仪器调试");
                return;
            }
            cmbWashPara.Enabled = false;
            //压杯开始位置
            if (cmbWashPara.SelectedIndex == 0)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 03 01 01"), 5);
                NetCom3.Instance.SingleQuery();
            }
            //压杯最低位置
            else if (cmbWashPara.SelectedIndex == 1)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 03 01 02"), 5);
                NetCom3.Instance.SingleQuery();
            }
            //调整清洗液注液量
            else if (cmbWashPara.SelectedIndex == 2)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 03 01 03"), 5);
                NetCom3.Instance.SingleQuery();
            }

            cmbWashPara.Enabled = true;
        }

        private void FbtnWashAdd_Click(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                MessageBox.Show("仪器正在运动，请稍等！", "仪器调试");
                return;
            }
            if (cmbWashElecMachine.SelectedItem == null)
            {
                MessageBox.Show("请选择需调试的电机！", "仪器调试");
                return;
            }
            if (txtWashIncream.Text == "")
            {
                MessageBox.Show("请输入增量值！", "仪器调试");
                txtWashIncream.Focus();
                return;
            }
            if (!(Regex.IsMatch(txtWashIncream.Text, "^([1-9]{1}[0-9]{0,3})$")))
            {
                MessageBox.Show("请输入正确参数！", "仪器调试");
                txtWashIncream.Focus();
                return;
            }
            fbtnWashAdd.Enabled = false;
            string incream = int.Parse(txtWashIncream.Text.Trim()).ToString("x8");
            //清洗盘电机
            if (cmbWashElecMachine.SelectedIndex == 0)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 03 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                    + incream.Substring(4, 2) + " " + incream.Substring(6, 2)), 5);
                NetCom3.Instance.SingleQuery();
            }
            //Z轴电机
            else if (cmbWashElecMachine.SelectedIndex == 1)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 03 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                       + incream.Substring(4, 2) + " " + incream.Substring(6, 2)), 5);
                NetCom3.Instance.SingleQuery();
            }
            //压杯电机
            else if (cmbWashElecMachine.SelectedIndex == 2)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 03 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                   + incream.Substring(4, 2) + " " + incream.Substring(6, 2)), 5);
                NetCom3.Instance.SingleQuery();
            }
            fbtnWashAdd.Enabled = true;
        }

        private void FbtnWashSub_Click(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                MessageBox.Show("仪器正在运动，请稍等！", "仪器调试");
                return;
            }
            if (cmbWashElecMachine.SelectedItem == null)
            {
                MessageBox.Show("请选择需调试的电机！", "仪器调试");
                return;
            }
            if (txtWashIncream.Text == "")
            {
                MessageBox.Show("请输入增量值！", "仪器调试");
                txtWashIncream.Focus();
                return;
            }
            if (!(Regex.IsMatch(txtWashIncream.Text, "^([1-9]{1}[0-9]{0,3})$")))
            {
                MessageBox.Show("请输入正确参数！", "仪器调试");
                txtWashIncream.Focus();
                return;
            }
            fbtnWashSub.Enabled = false;
            string incream = int.Parse("-" + txtWashIncream.Text.Trim()).ToString("x8");
            //清洗盘电机
            if (cmbWashElecMachine.SelectedIndex == 0)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 03 02 02 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                  + incream.Substring(4, 2) + " " + incream.Substring(6, 2)), 5);
                NetCom3.Instance.SingleQuery();
            }
            //Z轴电机
            else if (cmbWashElecMachine.SelectedIndex == 1)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 03 02 01 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                       + incream.Substring(4, 2) + " " + incream.Substring(6, 2)), 5);
                NetCom3.Instance.SingleQuery();
            }
            //压杯电机
            else if (cmbWashElecMachine.SelectedIndex == 2)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 03 02 03 " + incream.Substring(0, 2) + " " + incream.Substring(2, 2) + " "
                   + incream.Substring(4, 2) + " " + incream.Substring(6, 2)), 5);
                NetCom3.Instance.SingleQuery();
            }
            fbtnWashSub.Enabled = true;
        }

        private void FbtnWashSave_Click(object sender, EventArgs e)
        {

            if (!NetCom3.totalOrderFlag)
            {
                MessageBox.Show("仪器正在运动，请稍等！", "仪器调试");
                return;
            }
            fbtnWashSave.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 03 03 00"), 5);
            NetCom3.Instance.SingleQuery();
            fbtnWashSave.Enabled = true;
        }

        private void FbtnWashReset_Click(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                MessageBox.Show("仪器正在运动，请稍等！", "仪器调试");
                return;
            }
            fbtnWashReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 03 00 00"), 5);
            NetCom3.Instance.SingleQuery();
            fbtnWashReset.Enabled = true;
        }

        private void FbtnWashTrayReset_Click(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                MessageBox.Show("仪器正在运动，请稍等！", "仪器调试");
                return;
            }

            fbtnWashTrayReset.Enabled = false;
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 03 00 02"), 5);
            NetCom3.Instance.SingleQuery();
            fbtnWashTrayReset.Enabled = true;
        }

        private void FbtnWashTrayRotate_Click(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                MessageBox.Show("仪器正在运动，请稍等！", "仪器调试");
                return;
            }
            if (txtRotateNum.Text == "")
            {
                MessageBox.Show("请输入正确参数！", "仪器调试");
                txtRotateNum.Focus();
                return;
            }
            //if (!((Regex.IsMatch(txtRotateNum.Text.ToString().Substring(1), "^([1-9]{1}[0-9]{0,1})$")) && txtRotateNum.Text.ToString().Substring(0 , 1) == "-") && !(Regex.IsMatch(txtRotateNum.Text, "^([1-9]{1}[0-9]{0,1})$")))
            if (!(Regex.IsMatch(txtRotateNum.Text, "^((-)?[1-9]{1,}[0,9]{0,1})$")))
            {
                MessageBox.Show("请输入正确参数！", "仪器调试");
                txtRotateNum.Focus();
                return;
            }
            if (int.Parse(txtRotateNum.Text) < -30 || int.Parse(txtRotateNum.Text) > 30 || int.Parse(txtRotateNum.Text) == 0)
            {
                MessageBox.Show("请输入正确参数！", "仪器调试");
                txtRotateNum.Focus();
                return;
            }

            fbtnWashTrayRotate.Enabled = false;
            int rotateNum = int.Parse(txtRotateNum.Text.Trim());
            string strNum = "";
            if (rotateNum > 0)
                strNum = rotateNum.ToString("x2");
            else if (rotateNum < 0)
            {
                strNum = rotateNum.ToString("X2").Substring(6, 2);
            }
            NetCom3.Instance.Send(NetCom3.Cover("EB 90 31 03 01 " + strNum), 2);
            NetCom3.Instance.WashQuery();

            fbtnWashTrayRotate.Enabled = true;
        }

        private void FbtnWashStepOrder_Click(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                MessageBox.Show("仪器正在运动，请稍等！", "仪器调试");
                return;
            }
            if (textBox3.Text == "")
            {
                MessageBox.Show("请输入正确指令！", "仪器调试");
                textBox3.Focus();
                return;
            }
            if (!textBox3.Text.Contains("EB 90") && !textBox3.Text.Contains("eb 90"))
            {
                MessageBox.Show("请输入正确指令！", "仪器调试");
                textBox3.Focus();
                return;
            }

            fbtnWashStepOrder.Enabled = false;
            string order = textBox3.Text.ToString();
            NetCom3.Instance.Send(NetCom3.Cover(order), 2);
            NetCom3.Instance.WashQuery();

            fbtnWashStepOrder.Enabled = true;
        }

        private void FbtnReadNum_Click(object sender, EventArgs e)
        {
            if (!NetCom3.totalOrderFlag)
            {
                MessageBox.Show("仪器正在运动，请稍等！", "仪器调试");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("请输入正确指令！", "仪器调试");
                textBox2.Focus();
                return;
            }
            if (!(Regex.IsMatch(textBox2.Text, "^([1-9]{1}[0-9]{0,1})$")))
            {
                MessageBox.Show("请输入正确参数！", "仪器调试");
                textBox2.Focus();
                return;
            }
            fbtnReadNum.Enabled = false;
            textBox2.ReadOnly = true;
            //int num = 0;
            BeginInvoke(new Action(() =>
            {
                richTextBox1.AppendText(Environment.NewLine + Environment.NewLine + DateTime.Now.ToString("HH-mm-ss") + ":开始读数。" + Environment.NewLine);
                // richTextBox1.AppendText(Environment.NewLine + Environment.NewLine + DateTime.Now.ToString("HH-mm-ss") + ": 第 " + num++ + " 个管正在读数。" + Environment.NewLine);
            }));

            NetCom3.Instance.ReceiveHandel += GetReadNum2;
            for (int i = 0; i < int.Parse(textBox2.Text); i++)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 31 03 03 00 00 00 01"), 2);
                if (!NetCom3.Instance.WashQuery())
                    return;
                // num++;
            }
            NetCom3.Instance.ReceiveHandel -= GetReadNum2;
            textBox2.ReadOnly = false;
            fbtnReadNum.Enabled = true;
            BeginInvoke(new Action(() =>
            {
                richTextBox1.AppendText(Environment.NewLine + DateTime.Now.ToString("HH-mm-ss") + ": 测试结束。" + Environment.NewLine + Environment.NewLine);
            }));
        }

        private void GetReadNum2(string order)
        {
            if (order.Contains("EB 90 31 A3"))
            {
                string temp = order.Replace(" ", "");
                int pos = temp.IndexOf("EB9031A3");
                temp = temp.Substring(pos, 32);
                temp = temp.Substring(temp.Length - 8);
                temp = Convert.ToInt64(temp, 16).ToString();
                if (int.Parse(temp) > Math.Pow(10, 5))
                    temp = ((int)GetPMT(double.Parse(temp))).ToString();
                if (int.Parse(temp) == 0)
                {
                    return;
                }
                LogFile.Instance.Write(DateTime.Now.ToString("HH:mm:ss:fff") + ": " + "PMT背景值：" + temp);
                BeginInvoke(new Action(() =>
                {
                    richTextBox1.AppendText(DateTime.Now.ToString("HH-mm-ss") + ": " + "PMT背景值：" + temp + Environment.NewLine);
                }));
            }
        }
        public double GetPMT(double pmt)
        {
            return pmt = pmt / (1 - pmt * 20 * Math.Pow(10, -9)); ;
        }
        private void TxtRotateNum_MouseEnter(object sender, EventArgs e)
        {
            if (RotateNumHasText == false)
                txtRotateNum.Text = "";
            txtRotateNum.ForeColor = Color.Black;
        }

        private void TxtRotateNum_MouseLeave(object sender, EventArgs e)
        {
            if (txtRotateNum.Text == "" && txtRotateNum.Focused == false)
            {
                RotateNumHasText = false;
                txtRotateNum.Text = "请输入旋转孔位步数参数";
                txtRotateNum.ForeColor = Color.LightGray;
            }
            else
                RotateNumHasText = true;
        }

        private void TextBox3_MouseEnter(object sender, EventArgs e)
        {
            if (StepOrderHasText == false)
                textBox3.Text = "";
            textBox3.ForeColor = Color.Black;
        }

        private void TextBox3_MouseLeave(object sender, EventArgs e)
        {
            if (textBox3.Text == "" && textBox3.Focused == false)
            {
                StepOrderHasText = false;
                textBox3.Text = "请输入清洗盘单步指令";
                textBox3.ForeColor = Color.LightGray;
            }
            else
                StepOrderHasText = true;
        }

        private void TextBox2_MouseEnter(object sender, EventArgs e)
        {
            if (ReadNumHasText == false)
                textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
        }

        private void TextBox2_MouseLeave(object sender, EventArgs e)
        {
            if (textBox2.Text == "" && textBox2.Focused == false)
            {
                ReadNumHasText = false;
                textBox2.Text = "请输入读数次数";
                textBox2.ForeColor = Color.LightGray;
            }
            else
                ReadNumHasText = true;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void FrmWash_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void BtnClearTxt_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }
}

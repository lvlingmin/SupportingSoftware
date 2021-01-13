using DebugTool.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DebugTool
{
    public partial class frmLinearityTest : Form
    {
        private DataTable dtValue = new DataTable();
        int repeatNum = 10;
        private int readType = 0;
        private int readNum = 0;
        public frmLinearityTest()
        {
            InitializeComponent();
        }

        private void frmLinearityTest_Load(object sender, EventArgs e)
        {
            dtValue.Columns.Add("data0" , typeof(string));
            dtValue.Columns.Add("data1", typeof(string));
            dtValue.Columns.Add("data2", typeof(string));
            dtValue.Columns.Add("data3", typeof(string));
            dtValue.Columns.Add("data4", typeof(string));
            dtValue.Columns.Add("data5", typeof(string));
            dtValue.Columns.Add("data6", typeof(string));
            dtValue.Columns.Add("data7", typeof(string));
            dtValue.Columns.Add("data8", typeof(string));

            dgvValue.DataSource = dtValue;

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if(!Regex.IsMatch(txtRepeatNum.Text , "^[0-9]{1,2}$"))
            {
                MessageBox.Show("请检查输入读数次数！");
                return;
            }
            btnRead.Enabled = false;
            if (rb1.Checked)
                readType = 1;
            else if (rb2.Checked)
                readType = 2;
            else if (rb3.Checked)
                readType = 3;
            else if (rb4.Checked)
                readType = 4;
            else if (rb5.Checked)
                readType = 5;
            else if (rb6.Checked)
                readType = 6;
            else if (rb7.Checked)
                readType = 7;
            else if (rb8.Checked)
                readType = 8;
            repeatNum = int.Parse(txtRepeatNum.Text);
            readNum = 0;

            if (dtValue.Rows.Count < repeatNum)
            {
                for(int i = dtValue.Rows.Count;i < repeatNum; i++)
                {
                    dtValue.Rows.Add();
                }
            }
            NetCom3.Instance.ReceiveHandel += GetReadNum2;
            for (int i = 0; i < repeatNum; i++)
            {
                NetCom3.Instance.Send(NetCom3.Cover("EB 90 31 03 03 00 00 00 01") , 2);
                if(!NetCom3.Instance.WashQuery())
                {
                    MessageBox.Show("错误！");
                    goto ErrorOrEnd;
                }
                NetCom3.Delay(1000);
            }
           
            ErrorOrEnd:
            NetCom3.Instance.ReceiveHandel -= GetReadNum2;
            btnRead.Enabled = true;
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
                
                dtValue.Rows[readNum++][readType] = temp.ToString();
                if (dtValue.Rows[readNum - 1][0].ToString() == "")
                {
                    dtValue.Rows[readNum - 1][0] = readNum.ToString();
                }

                
            }
        }
        public double GetPMT(double pmt)
        {
            return pmt = pmt / (1 - pmt * 20 * Math.Pow(10, -9)); ;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            btnClear.Enabled = false;
            dtValue.Clear();
            btnClear.Enabled = true;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if(dtValue.Rows.Count == 0)
            {
                MessageBox.Show("没有结果！");
                return;
            }
            try
            {
                #region 数据导出                  
                string filePath = "";
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择保存文件夹";
                DialogResult flag = dialog.ShowDialog();
                if (flag != DialogResult.OK)
                    return;

                filePath = dialog.SelectedPath + @"\" + "光子计数器线性测试" + "_" + DateTime.Now.ToString("yyyyMMdd HHmmss") + ".xls";
                DataTable dd = dtValue;
                dd.Columns[0].ColumnName = "序号";
                dd.Columns[1].ColumnName = "暗室读数";
                dd.Columns[2].ColumnName = "稳定光源";
                dd.Columns[3].ColumnName = "衰减50%";
                dd.Columns[4].ColumnName = "衰减25%";
                dd.Columns[5].ColumnName = "衰减10%";
                dd.Columns[6].ColumnName = "衰减5%";
                dd.Columns[7].ColumnName = "衰减2.5%";
                dd.Columns[8].ColumnName = "衰减1%";
                DataTableExcel.TableToExcel(dd, filePath);
                MessageBox.Show( "导出成功。", "SUCCESS");

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show( "导出失败。\n" + ex.Message , "ERROR");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void frmLinearityTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnCoefficient_Click(object sender, EventArgs e)
        {
            repeatNum = int.Parse(txtRepeatNum.Text);
            try
            {
                for (int i = 1; i < dtValue.Columns.Count; i++)
                {
                    if (dtValue.Rows[repeatNum - 1][i].ToString() == null)
                    {
                        MessageBox.Show("请测试全部数据后再计算相关线性系数！");
                        return;
                    }
                }
            }
            catch(System.Exception ex)
            {
                MessageBox.Show("请测试全部数据后再计算相关线性系数！");
                LogFile.Instance.Write(ex.ToString());
                return;
            }

            //均值
            double[] avg = new double[8];
            for(int i = 1; i <= 8; i++)
            {
                double num = 0;
                for(int j = 0; j < repeatNum; j++)
                {
                    num += int.Parse(dtValue.Rows[j][i].ToString());
                }
                avg[i - 1] = num / 8;
            }
            dtValue.Rows.Add("均值", avg[0].ToString("0.0"), avg[1].ToString("0.0"), avg[2].ToString("0.0"), 
                avg[3].ToString("0.0"), avg[4].ToString("0.0"), avg[5].ToString("0.0"), avg[6].ToString("0.0"), avg[7].ToString("0.0"));

            double avgX = (1 + 0.5 + 0.25 + 0.1 + 0.05 + 0.025 + 0.01) / 7;
            double avgY;
            //理论百分比
            dtValue.Rows.Add("理论百分比", "", "100.00%", "50.00%", "25.00%", "10.00%", "5.00%", "2.50%", "1.00%");
            //实际百分比
            double[] tempY = new double[7] { 1 ,avg[2] / avg[1], avg[3] / avg[1] , avg[4] / avg[1] , avg[5] / avg[1] , avg[6] / avg[1] , avg[7] / avg[1] };
            avgY = (tempY[0] + tempY[1] + tempY[2] + tempY[3] + tempY[4] + tempY[5] + tempY[6]) / 7;
            dtValue.Rows.Add("实际百分比", "", tempY[0].ToString("0.00%"), tempY[1].ToString("0.00%"), tempY[2].ToString("0.00%"),
               tempY[3].ToString("0.00%"), tempY[4].ToString("0.00%"), tempY[5].ToString("0.00%"),
                tempY[6].ToString("0.00%"));
            //线性相关R
            double[] tempX = new double[7] {1 , 0.5 ,0.25 ,0.1 ,0.05 ,0.025 ,0.01 };
            double[] x_avg = new double[7];
            double[] y_avg = new double[7];
            for(int i = 0;i < 7;i++)
            {
                x_avg[i] = tempX[i] - avgX;
                y_avg[i] = tempY[i] - avgY;
            }
            double numerator = 0, denominator = 0 , denominator_1 = 0 , denominator_2 = 0;
            for(int i = 0; i < 7; i++)
            {
                numerator += x_avg[i] * y_avg[i];
                denominator_1 += x_avg[i] * x_avg[i];
                denominator_2 += y_avg[i] * y_avg[i];
            }
            denominator = Math.Sqrt(denominator_1 * denominator_2);
            double coefficient = numerator / denominator;
            dtValue.Rows.Add("线性相关R", "", "", "", coefficient.ToString("0.000000000"), "", "", "", "");

        }

        private void dgvValue_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;
            

            if(dtValue.Rows[e.RowIndex][0].ToString() == "线性相关R")
            {
                if(e.ColumnIndex > 0 && e.ColumnIndex < 8)
                {
                    e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
                }
            }
        }
    }
}

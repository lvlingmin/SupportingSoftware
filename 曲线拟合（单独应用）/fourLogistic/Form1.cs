using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using fourLogistic.Caculate;
using fourLogistic.Calculate;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using fourORfiveLogistic;

namespace fourLogistic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region 
        DataTable dtCountConc = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(500);
            cmbFit.SelectedIndex = 5;
            cmbX.SelectedIndex = 0;
            cmbY.SelectedIndex = 0;
            dataGridView1.ClearSelection();
            rbtnPmtToConc.Checked = true;
        }
        drawCurve dc = new drawCurve();
        int count = 0;
        List<Data_Value> ltData = new List<Data_Value>();
        List<Data_Value> CurveData = new List<Data_Value>();
        private void btnFit_Click2(object sender, EventArgs e)
        {
            #region 计算代码
            count = 0;
            ltData = new List<Data_Value>();
            CurveData = new List<Data_Value>();
            //List<Data_Value> ltData = new List<Data_Value>();
            //List<Data_Value> CurveData = new List<Data_Value>();
            CalculateFactory ft = new CalculateFactory();
            Calculater er = null;
            er = ft.getCaler(cmbFit.SelectedIndex);//取到公式
            for (int i = 0; i < dataGridView1.Rows.Count; i++)//DataGridView中的数据
            {
                if (dataGridView1[0, i].Value != null && dataGridView1[1, i].Value != null)
                    count++;
            }
            //
            DataTable dt = new DataTable();
            dt.Columns.Add("consistence", typeof(float));
            dt.Columns.Add("absorbency", typeof(float));
            for (int j = 0; j < count; j++)
            {
                //dt.Rows.Add(double.Parse(dataGridView1[0, j].Value.ToString()), double.Parse(dataGridView1[1, j].Value.ToString()));
                //浓度和发光值
                try
                {
                    ltData.Add(new Data_Value() { Data = double.Parse(dataGridView1[0, j].Value.ToString()), DataValue = double.Parse(dataGridView1[1, j].Value.ToString()) });
                    CurveData.Add(new Data_Value() { Data = double.Parse(dataGridView1[0, j].Value.ToString()), DataValue = double.Parse(dataGridView1[1, j].Value.ToString()) });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请确保数据格式正确，错误信息：" + ex.Message);
                }
            }
            //根据浓度值排序
            ltData.Sort(new Data_ValueDataAsc());
            CurveData.Sort(new Data_ValueDataAsc());
            #region 去除重复数据
            for (int i = ltData.Count - 2; i >= 0; i--)//去掉相同浓度
            {
                Data_Value v2 = ltData[i + 1];
                Data_Value v1 = ltData[i];
                if (v2.Data == v1.Data)
                {
                    v1.DataValue = (v1.DataValue + v2.DataValue) / 2;
                    ltData.RemoveAt(i + 1);
                }
            }
            for (int i = CurveData.Count - 2; i >= 0; i--)
            {
                Data_Value v2 = CurveData[i + 1];
                Data_Value v1 = CurveData[i];
                if (v2.Data == v1.Data)
                {
                    v1.DataValue = (v1.DataValue + v2.DataValue) / 2;
                    CurveData.RemoveAt(i + 1);
                }
            }
            #endregion          
            if (ltData.Count > 0)//ltData标准品
            {
                #region 常规性对0 1 浓度处理
                for (int i = 0; i < ltData.Count; i++)
                {
                    if (ltData[i].Data == 0)
                    {
                        ltData[i].Data = 0.0001;
                    }
                    if (ltData[i].Data == 1)
                    {
                        ltData[i].Data = 0.999999;
                    }
                    if (ltData[i].DataValue == 0)
                    {
                        ltData[i].DataValue = 0.0001;
                    }
                }
                for (int i = 0; i < CurveData.Count; i++)
                {
                    if (CurveData[i].Data == 0)
                    {
                        CurveData[i].Data = 0.0001;
                    }
                    if (CurveData[i].Data == 1)
                    {
                        CurveData[i].Data = 0.999999;
                    }
                    if (CurveData[i].DataValue == 0)
                    {
                        CurveData[i].DataValue = 0.0001;
                    }
                }
                #endregion
                #region  曲线拟合，这部分可以忽略
                foreach (Data_Value dv1 in CurveData)
                {
                    switch (cmbX.SelectedIndex)
                    {
                        case 1:
                            if ((cmbFit.SelectedIndex == 4 || cmbFit.SelectedIndex == 5 || cmbFit.SelectedIndex == 6) && dv1.Data < 1)
                                dv1.Data = 1.001;
                            dv1.Data = Math.Log(dv1.Data, 2);
                            break;
                        case 2:
                            if ((cmbFit.SelectedIndex == 4 || cmbFit.SelectedIndex == 5 || cmbFit.SelectedIndex == 6) && dv1.Data < 1)
                                dv1.Data = 1.001;
                            dv1.Data = Math.Log10(dv1.Data);
                            break;
                        case 3:
                            if ((cmbFit.SelectedIndex == 4 || cmbFit.SelectedIndex == 5 || cmbFit.SelectedIndex == 6) && dv1.Data < 1)
                                dv1.Data = 1.001;
                            dv1.Data = Math.Log(dv1.Data, Math.E);
                            break;
                        case 4:
                            dv1.Data *= dv1.Data;
                            break;
                        case 5:
                            dv1.Data = Math.Sqrt(dv1.Data);
                            break;
                        case 6:
                            dv1.Data = Math.Pow(Math.E, dv1.Data);
                            break;
                    }
                    switch (cmbY.SelectedIndex)
                    {
                        case 1:
                            dv1.DataValue = Math.Log(dv1.DataValue, 2);
                            break;
                        case 2:
                            dv1.DataValue = Math.Log10(dv1.DataValue);
                            break;
                        case 3:
                            dv1.DataValue = Math.Log(dv1.DataValue, Math.E);
                            break;
                        case 4:
                            dv1.DataValue *= dv1.DataValue;
                            break;
                        case 5:
                            dv1.DataValue = Math.Sqrt(dv1.DataValue);
                            break;
                        case 6:
                            dv1.DataValue = Math.Pow(Math.E, dv1.DataValue);
                            break;
                    }
                }
                if (cmbFit.SelectedIndex == 4 && ltData[0].Data != 0.0001)
                {
                    MessageBox.Show("必须有一个或几个X值为0的点!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (Data_Value dv in ltData)
                {
                    switch (cmbX.SelectedIndex)
                    {
                        case 1:
                            dv.Data = Math.Log(dv.Data, 2);
                            break;
                        case 2:
                            dv.Data = Math.Log10(dv.Data);
                            break;
                        case 3:
                            dv.Data = Math.Log(dv.Data, Math.E);
                            break;
                        case 4:
                            dv.Data *= dv.Data;
                            break;
                        case 5:
                            dv.Data = Math.Sqrt(dv.Data);
                            break;
                        case 6:
                            dv.Data = Math.Pow(Math.E, dv.Data);
                            break;
                    }
                    switch (cmbY.SelectedIndex)
                    {
                        case 1:
                            dv.DataValue = Math.Log(dv.DataValue, 2);
                            break;
                        case 2:
                            dv.DataValue = Math.Log10(dv.DataValue);
                            break;
                        case 3:
                            dv.DataValue = Math.Log(dv.DataValue, Math.E);
                            break;
                        case 4:
                            dv.DataValue *= dv.DataValue;
                            break;
                        case 5:
                            dv.DataValue = Math.Sqrt(dv.DataValue);
                            break;
                        case 6:
                            dv.DataValue = Math.Pow(Math.E, dv.DataValue);
                            break;
                    }
                    dt.Rows.Add(dv.Data, dv.DataValue);
                }
                if (cmbFit.SelectedIndex == 4)
                {
                    dt.Rows.Clear();
                    for (int i = 1; i < ltData.Count; i++)
                    {
                        if ((1 - ltData[i].DataValue / ltData[0].DataValue) < 0)
                        {
                            MessageBox.Show("必须有一个或几个X值为0的点，该点的Y值最大，才能使用Logit-Log直线回归模型", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        ltData[i].Data = Math.Log10(ltData[i].Data);
                        ltData[i].DataValue = Math.Log((ltData[i].DataValue / ltData[0].DataValue) / (1 - ltData[i].DataValue / ltData[0].DataValue), Math.E);
                        dt.Rows.Add(ltData[i].Data, ltData[i].DataValue);
                    }
                    ltData.RemoveAt(0);
                }
                for (int i = 0; i < ltData.Count; i++)
                {
                    if (double.IsNaN(ltData[i].Data) || double.IsNaN(ltData[i].DataValue) || double.IsNaN(CurveData[i].DataValue) || double.IsNaN(CurveData[i].Data))
                    {
                        MessageBox.Show("函数计算错误，可能该数据不适合此回归模型", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                #endregion
                for (int i = 0; i < CurveData.Count; i++)
                {
                    er.AddData(CurveData);
                    er.Fit();
                }
            }
            foreach (double par in er._pars)
            {
                if (double.IsNaN(par) || double.IsInfinity(par))
                {
                    MessageBox.Show("回归计算时出现运算错误，可能该数据不适合此回归模型", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            EquationCurvet(er, dt);
            //CountConcentrationt(er, count);
            #endregion 
        }

        /// <summary>
        /// 计算结果
        /// </summary>
        /// <param name="ltData">定标数据</param>
        /// <param name="PMT">待计算数据</param>
        /// <returns></returns>
        double CountResultTestt(List<Data_Value> ltData, double PMT)
        {
            Application.DoEvents();
            double concentration = 0;
            #region 标准计算不允许某些位置为特定数字
            Calculater er = new FourPL();
            //排序
            ltData.Sort(new Data_ValueDataAsc());
            for (int i = 0; i < ltData.Count; i++)
            {
                if (ltData[i].Data == 0)
                {
                    ltData[i].Data = 0.0001;
                }
                if (ltData[i].Data == 1)
                {
                    ltData[i].Data = 0.999999;
                }
                if (ltData[i].DataValue == 0)
                {
                    ltData[i].DataValue = 0.0001;
                }
            }
            for (int i = 0; i < ltData.Count; i++)
            {
                er.AddData(ltData);
                er.Fit();
            }
            if (ltData[0].DataValue < PMT && PMT < ltData[1].DataValue)
            {
                List<Data_Value> templtData = new List<Data_Value>();
                for (int i = 0; i < ltData.Count; i++)
                {
                    if (i == 0 || i == 1)
                    {
                        templtData.Add(ltData[i]);
                    }
                }
                concentration = CountLinearResultt(templtData, PMT);
            }
            else
            {
                concentration = er.GetResultInverse(PMT);
                if (double.IsNaN(concentration))
                {
                    if ((ltData[0].DataValue < ltData[1].DataValue && PMT <= ltData[0].DataValue) ||
                        (ltData[0].DataValue > ltData[1].DataValue && PMT >= ltData[0].DataValue))
                    {
                        concentration = 0.001;
                    }
                    else
                    {
                        concentration = ltData[ltData.Count - 1].DataValue;
                    }
                }
            }
            Application.DoEvents();
            return concentration;
            #endregion
        }

        /// <summary>
        /// 计算线性
        /// </summary>
        /// <param name="ltData">定标数据</param>
        /// <param name="PMT">发光值</param>
        /// <returns></returns>
        double CountLinearResultt(List<Data_Value> ltData, double PMT)
        {
            Calculater er = new Linear();
            ltData.Sort(new Data_ValueDataAsc());
            //for (int i = 0; i < ltData.Count; i++)
            //{
                er.AddData(ltData);
                er.Fit();
            //}
            double concentration = er.GetResultInverse(PMT);
            return concentration;
        }

        /// <summary>
        /// 通过已有数据计算预测浓度
        /// </summary>
        /// <param name="er">计算所得数据</param>
        /// <param name="count">数据列表中不为空的行数</param>
        private void CountConcentrationt(Calculater er, int count)
        {
            if (cmbFit.SelectedIndex == 5) //选择第五个回归模型才进行计算
            {
                string[] strpar = er.StrPars.Split('|');
                string paraA = strpar[0];
                string paraB = strpar[1];
                string paraC = strpar[2];
                string paraD = strpar[3];
                double paradoubleA = double.Parse(Convert.ToDecimal(paraA).ToString("F2"));
                double paradoubleB = double.Parse(Convert.ToDecimal(paraB).ToString("F2"));
                double paradoubleC = double.Parse(Convert.ToDecimal(paraC).ToString("F2"));
                double paradoubleD = double.Parse(Convert.ToDecimal(paraD).ToString("F2"));
                for (int i = 0; i < count; i++)
                {
                    string tempValue = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    double paradoubleY = double.Parse(Convert.ToDecimal(tempValue).ToString("F2"));
                    double temp = (paradoubleA - paradoubleY) / (paradoubleY - paradoubleD);
                    if (temp <= 0)
                    {
                        temp = -temp;
                    }
                    double powNum = 1 / paradoubleB;
                    double paradoubleX = Math.Pow(temp, powNum);
                    MessageBox.Show(paradoubleX.ToString());
                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToDecimal(paradoubleX).ToString("F2");
                }
                //string couConcentration = 
            }
        }

        private void btnCurve_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnEquation_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dataGridView1.GetClipboardContent());
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            Paste(dataGridView1, "", 0, false);
        }

        #region 方程及曲线显示
        /// <summary>
        /// 方程及曲线显示解析
        /// </summary>
        /// <param name="er"></param>
        public void EquationCurvet(Calculater er, DataTable dt)
        {
            #region 开始
            double tempCal = 0;
            string newPars = string.Empty;
            foreach (string par in er.StrPars.Split('|'))
            {
                if (double.TryParse(par, out tempCal))
                {
                    if (double.IsInfinity(tempCal) || double.IsNaN(tempCal))
                    {
                        newPars += "0|";
                    }
                    else
                    {
                        newPars += par + "|";
                    }
                }
                else
                {
                    newPars += par + "0|";
                }
            }
            List<double> listPar = new List<double>();
            foreach (string str in er.StrPars.Split('|'))
            {
                listPar.Add(double.Parse(str));
            }
            fourLogistic.Calculate.CalculateFactory fy = new fourLogistic.Calculate.CalculateFactory();
            //fy.PointNum = int.Parse(dgScalis.CurrentRow.Cells["standardNum"].Value.ToString());
            List<Data_Value> litData_Value = new List<Data_Value>();
            Calculater er1 = fy.getCaler(listPar);
            dc.paintEliseScaling(ShowCurvePnl, dt, er.GetResult, "", cmbFit.SelectedIndex);//mdscalingInfo.fittingResult.IsNullOrEmpty() ? "" : "R: " + Convert.ToDouble(mdscalingInfo.fittingResult).ToString("0.###")
            GetParaAndShow(er);//设置参数显示
            List<double> lsDouble = new List<double>();
            foreach (var v in ltData)
            {
                lsDouble.Add(v.DataValue);
            }
            //minValue =lsDouble.Min();
            if (rbtnPmtToConc.Checked)
            {
                //minValue = er.GetResult(0);
                //DataTable dtCountConc = new DataTable();
                for (int j = 0; j < count; j++)//原有数据的预测值
                {
                    //dt.Rows.Add(double.Parse(dataGridView1[0, j].Value.ToString()), double.Parse(dataGridView1[1, j].Value.ToString()));
                    //浓度和发光值
                    //if (double.Parse(dataGridView1[1, j].Value.ToString()) <= minValue)
                    //{
                    //    conc = er.GetResultInverse(minValue);
                    //    dataGridView1[2, j].Value = Convert.ToDouble(conc).ToString("0.00000");
                    //    continue;
                    //}
                    //conc = er.GetResultInverse(double.Parse(dataGridView1[1, j].Value.ToString()));
                    //if (double.IsNaN(conc))
                    //{
                    //    conc = 0.001;
                    //}
                    //;
                    double PMT = double.Parse(dataGridView1[1, j].Value.ToString());
                    double conc;
                    //if ((ltData[0].DataValue <= PMT && PMT <= ltData[1].DataValue) || (ltData[0].DataValue >= PMT && PMT >= ltData[1].DataValue))
                    //{
                    //    List<Data_Value> templtData = new List<Data_Value>();
                    //    for (int i = 0; i < ltData.Count; i++)
                    //    {
                    //        if (i == 0 || i == 1)
                    //        {
                    //            templtData.Add(ltData[i]);
                    //        }
                    //    }
                    //    conc = CountLinearResultt(templtData, PMT);
                    //}
                    //else
                    //{
                        conc = er.GetResultInverse(PMT);
                        if (double.IsNaN(conc))
                        {
                            if ((ltData[0].DataValue < ltData[1].DataValue && PMT <= ltData[0].DataValue) ||
                                (ltData[0].DataValue > ltData[1].DataValue && PMT >= ltData[0].DataValue))
                            {
                                conc = 0.001;
                            }
                            else
                            {
                                conc = ltData[ltData.Count - 1].DataValue;
                            }
                        }
                    //}
                    if (conc <= ltData[0].Data)
                    {
                        conc = ltData[0].Data + 0.001;
                    }
                    if (conc >= (ltData[ltData.Count - 1].Data + ltData[ltData.Count - 1].Data * 0.3))
                    {
                        //conc = ltData[ltData.Count - 1].Data;
                        dataGridView1[2, j].Value = "大于理论最大值的30%";
                    }
                    else
                    {
                        dataGridView1[2, j].Value = Convert.ToDouble(conc).ToString("0.000");
                    }
                    //conc = CountResultTestt(ltData, double.Parse(dataGridView1[1, j].Value.ToString()));
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)//想要预测的数据
                {
                    if (dataGridView1[0, i].Value == null && dataGridView1[1, i].Value != null)
                    {
                        double conc;
                        //if (double.Parse(dataGridView1[1, i].Value.ToString()) <= minValue)
                        //{
                        //    conc = er.GetResultInverse(minValue);
                        //    dataGridView1[2, i].Value = Convert.ToDouble(conc).ToString("0.00000");
                        //    continue;
                        //}
                        //conc = er.GetResultInverse(double.Parse(dataGridView1[1, i].Value.ToString()));

                        //if (double.IsNaN(conc))
                        //{
                        //    dataGridView1[2, i].Value = "0.001";
                        //    continue;
                        //}
                        //conc = CountResultTestt(ltData, double.Parse(dataGridView1[1, i].Value.ToString()));
                        double PMT = double.Parse(dataGridView1[1, i].Value.ToString());
                        //if (ltData[0].DataValue <= PMT && PMT <= ltData[1].DataValue)
                        //{
                        //    List<Data_Value> templtData = new List<Data_Value>();
                        //    for (int z = 0; z < ltData.Count; z++)
                        //    {
                        //        if (z == 0 || z == 1)
                        //        {
                        //            templtData.Add(ltData[z]);
                        //        }
                        //    }
                        //    conc = CountLinearResultt(templtData, PMT);
                        //}
                        //else
                        if (PMT < ltData[0].DataValue&& ltData[0].DataValue  < ltData[1].DataValue)
                        {
                            conc = ltData[0].Data;
                        }
                        else
                        {
                            conc = er.GetResultInverse(PMT);
                            if (double.IsNaN(conc))
                            {
                                if ((ltData[0].DataValue < ltData[1].DataValue && PMT <= ltData[0].DataValue) ||
                                    (ltData[0].DataValue > ltData[1].DataValue && PMT >= ltData[0].DataValue))
                                {
                                    conc = 0.001;
                                }
                                //else if(PMT)
                                else if (PMT >= ltData[1].DataValue && PMT <= ltData[0].DataValue)
                                {
                                    conc = ltData[0].Data + 0.001;
                                }
                                else
                                {
                                    conc = ltData[ltData.Count - 1].Data;
                                }
                            } //conc = er.GetResultInverse(PMT);
                        }
                        if (conc <= ltData[0].Data)
                        {
                            conc = ltData[0].Data + 0.001;
                        }
                        if (conc >= (ltData[ltData.Count - 1].Data + ltData[ltData.Count - 1].Data * 0.3))
                        {
                            //conc = ltData[ltData.Count - 1].Data;
                            dataGridView1[2, i].Value = "大于理论最大值的30%";
                        }
                        else
                        {
                            dataGridView1[2, i].Value = Convert.ToDouble(conc).ToString("0.000");
                        }
                    }
                }
            }
            if (rbtnConcToPmt.Checked)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)//想要预测的数据
                {
                    double PMT;
                    if (dataGridView1[0, i].Value == null && dataGridView1[1, i].Value == null && dataGridView1[2, i].Value != null)
                    {
                        double conc = double.Parse(dataGridView1[2, i].Value.ToString());
                        if (conc == 0)
                        {
                            conc = 0.001;
                        }
                        if ((ltData[0].Data <= conc && conc <= ltData[1].Data) || (ltData[0].Data >= conc && conc >= ltData[1].Data))
                        {
                            Calculater linear = new Linear();
                            ltData.Sort(new Data_ValueDataAsc());
                            for (int l = 0; l < 2; l++)
                            {
                                if (ltData[l].Data == 0.001)
                                {
                                    ltData[l].Data = 0;
                                }
                            }
                            linear.AddData(ltData);
                            linear.Fit();
                            PMT = linear.GetResult(conc);
                            dataGridView1[1, i].Value = Convert.ToDouble(PMT).ToString("0.000");
                        }
                        else
                        {
                            PMT = er.GetResult(conc);
                            dataGridView1[1, i].Value = Convert.ToDouble(PMT).ToString("0.000");
                        }
                        //double pmt;
                        //pmt = er.GetResult(double.Parse(dataGridView1[2, i].Value.ToString()));
                        //if (double.IsNaN(pmt))
                        //{
                        //    dataGridView1[1, i].Value = "0.001";
                        //    continue;
                        //}
                        //dataGridView1[1, i].Value = Convert.ToDouble(pmt).ToString("0.000");
                    }
                }
            }
            #endregion
            // dataGridView2.DataSource = dtCountConc;
            //dc.paintEliseScaling(doublePanel1, dt, er.GetResult, "",cmbFit.SelectedIndex);//mdscalingInfo.fittingResult.IsNullOrEmpty() ? "" : "R: " + Convert.ToDouble(mdscalingInfo.fittingResult).ToString("0.###")
            //#region 获取方程参数
            //string[] strpar = er.StrPars.Split('|');
            //string Furmula = string.Empty;
            //if (strpar.Length > 0)
            //{
            //    switch (cmbFit.SelectedIndex)
            //    {
            //        case 0:
            //            txtFormulaInfo.Clear();
            //            Furmula = "方程式： y = A + B*x";
            //            txtFormulaInfo.AppendText("直线回归：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[1] + "\r\n" + "B: " + strpar[0] + "\r\n" + "R^2: " + er.R2);
            //            break;
            //        case 1:
            //            txtFormulaInfo.Clear();
            //            Furmula = "";
            //            break;
            //        case 2:
            //            txtFormulaInfo.Clear();
            //            Furmula = "方程式： y = A + B*x + C*x^2";
            //            txtFormulaInfo.AppendText("二次多项式" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[2] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[0] + "\r\n" + "R^2: " + er.R2);
            //            break;
            //        case 3:
            //            txtFormulaInfo.Clear();
            //            Furmula = "方程式： y = A + B*x + C*x^2 + D*x^3";
            //            txtFormulaInfo.AppendText("三次多项式：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[3] + "\r\n" + "B: " + strpar[2] + "\r\n" + "C: " + strpar[1] + "\r\n" + "D: " + strpar[0] + "\r\n" + "R^2: " + er.R2);
            //            break;
            //        case 5:
            //            txtFormulaInfo.Clear();
            //            Furmula = "方程式： y = (A - D) / [1 + (x/C)^B] + D";
            //            txtFormulaInfo.AppendText("四参数Logistic曲线拟合：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[0] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[2] + "\r\n" + "D: " + strpar[3] + "\r\n" + "R^2: " + er.R2);
            //            break;
            //        #region
            //        case 6:
            //            txtFormulaInfo.Clear();
            //            Furmula = "方程式： y = (A - D) / [(1 + (x/C)^B)]^n + D";
            //            txtFormulaInfo.AppendText("五参数Logistic曲线拟合：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[0] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[2] + "\r\n" + "D: " + strpar[3] + "\r\n" + "n: " + strpar[4] + "\r\n" + "R^2: " + er.R2);
            //            break;
            //        #endregion
            //        case 4:
            //            txtFormulaInfo.Clear();
            //            //进行了描述补充 2017.12.16
            //            Furmula = " p=1/(1+e^-(A+B*x)),p = OD/OD0,  q = 1 - p," + "\r\n" + "y = ln (p / q)" + "\r\n" + "OD:" + "反应值" + ",OD0 :" + "浓度为0时的反应值的均值" + "\r\n" + "方程式：y = A + B*log(x)";
            //            txtFormulaInfo.AppendText("Logit-log :" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[1] + "\r\n" + "B: " + strpar[0] + "\r\n" + "R^2: " + er.R);
            //            break;
            //        case 7:
            //            txtFormulaInfo.Clear();
            //            Furmula = "方程式： y=A+B/(1+exp(-(C+D*ln(x))))";
            //            txtFormulaInfo.AppendText("四参数Logistic曲线拟合：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[0] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[2] + "\r\n" + "D: " + strpar[3] + "\r\n" + "R^2: " + er.R2);
            //            break;
            //    }
            //}
            //#endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="er"></param>
        /// <param name="PMT"></param>
        /// <returns></returns>
        double GetReverseResultTestt(Calculater er, double PMT)
        {
            #region 开始
            List<double> listPar = new List<double>();
            foreach (string str in er.StrPars.Split('|'))
            {
                listPar.Add(double.Parse(str));
            }
            #endregion
            return er.GetResultInverse(5233973);
        }
        #endregion
        /// <summary>
        /// 获取方程的参数并显示
        /// </summary>
        private void GetParaAndShow(Calculater er)
        {
            string[] strpar = er.StrPars.Split('|');
            string Furmula = string.Empty;
            if (strpar.Length > 0)
            {
                switch (cmbFit.SelectedIndex)
                {
                    case 0:
                        rtbPara.Clear();
                        Furmula = "方程式： y = A + B*x";
                        rtbPara.AppendText("直线回归：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[1] + "\r\n" + "B: " + strpar[0] + "\r\n" + "R^2: " + er.R2);
                        break;
                    case 1:
                        rtbPara.Clear();
                        Furmula = "";
                        break;
                    case 2:
                        rtbPara.Clear();
                        Furmula = "方程式： y = A + B*x + C*x^2";
                        rtbPara.AppendText("二次多项式" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[2] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[0] + "\r\n" + "R^2: " + er.R2);
                        break;
                    case 3:
                        rtbPara.Clear();
                        Furmula = "方程式： y = A + B*x + C*x^2 + D*x^3";
                        rtbPara.AppendText("三次多项式：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[3] + "\r\n" + "B: " + strpar[2] + "\r\n" + "C: " + strpar[1] + "\r\n" + "D: " + strpar[0] + "\r\n" + "R^2: " + er.R2 + "\r\n" + Math.Pow(er.R2, 0.5));
                        break;
                    case 5:
                        rtbPara.Clear();
                        Furmula = "方程式： y = (A - D) / [1 + (x/C)^B] + D";
                        //rtbPara.AppendText("四参数Logistic曲线拟合：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[0] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[2] + "\r\n" + "D: " + strpar[3] + "\r\n" + "R^2: " + er.R2+ "\r\n");
                        rtbPara.AppendText("四参数Logistic曲线拟合：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[0] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[2] + "\r\n" + "D: " + strpar[3] + "\r\n" + "R: " + Math.Pow(er.R2, 0.5) + "\r\n" + "R^2 : " + er.R2 + "\r\n");
                        break;
                    #region
                    case 6:
                        rtbPara.Clear();
                        Furmula = "方程式： y = (A - D) / [(1 + (x/C)^B)]^n + D";
                        rtbPara.AppendText("五参数Logistic曲线拟合：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[0] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[2] + "\r\n" + "D: " + strpar[3] + "\r\n" + "n: " + strpar[4] + "\r\n" + "R^2: " + er.R2);
                        break;
                    #endregion
                    case 4:
                        rtbPara.Clear();
                        //进行了描述补充 2017.12.16
                        Furmula = " p=1/(1+e^-(A+B*x)),p = OD/OD0,  q = 1 - p," + "\r\n" + "y = ln (p / q)" + "\r\n" + "OD:" + "反应值" + ",OD0 :" + "浓度为0时的反应值的均值" + "\r\n" + "方程式：y = A + B*log(x)";
                        rtbPara.AppendText("Logit-log :" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[1] + "\r\n" + "B: " + strpar[0] + "\r\n" + "R^2: " + er.R);
                        break;
                    case 7:
                        rtbPara.Clear();
                        Furmula = "方程式： y=A+B/(1+exp(-(C+D*ln(x))))";
                        rtbPara.AppendText("四参数Logistic曲线拟合：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[0] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[2] + "\r\n" + "D: " + strpar[3] + "\r\n" + "R^2: " + er.R2);
                        break;
                }
            }
        }
        #region 粘贴功能
        public int Paste(DataGridView dgv, string pasteText, int kind, bool b_cut)
        {
            try
            {
                if (kind == 0)
                {
                    pasteText = Clipboard.GetText();
                }
                if (string.IsNullOrEmpty(pasteText))
                    return -1;
                int rowNum = 0;
                int columnNum = 0;
                //获得当前剪贴板内容的行、列数
                for (int i = 0; i < pasteText.Length; i++)
                {
                    if (pasteText.Substring(i, 1) == "\t")
                    {
                        columnNum++;
                    }
                    if (pasteText.Substring(i, 1) == "\n")
                    {
                        rowNum++;
                    }
                }
                Object[,] data;
                //粘贴板上的数据来自于EXCEL时，每行末都有\n，在DATAGRIDVIEW内复制时，最后一行末没有\n
                if (pasteText.Substring(pasteText.Length - 1, 1) == "\n")
                {
                    rowNum = rowNum - 1;
                }
                columnNum = columnNum / (rowNum + 1);
                data = new object[rowNum + 1, columnNum + 1];

                String rowStr;
                //对数组赋值
                for (int i = 0; i < (rowNum + 1); i++)
                {
                    for (int colIndex = 0; colIndex < (columnNum + 1); colIndex++)
                    {
                        rowStr = null;
                        //一行中的最后一列
                        if (colIndex == columnNum && pasteText.IndexOf("\r") != -1)
                        {
                            rowStr = pasteText.Substring(0, pasteText.IndexOf("\r"));
                        }
                        //最后一行的最后一列
                        if (colIndex == columnNum && pasteText.IndexOf("\r") == -1)
                        {
                            rowStr = pasteText.Substring(0);
                        }
                        //其他行列
                        if (colIndex != columnNum)
                        {
                            rowStr = pasteText.Substring(0, pasteText.IndexOf("\t"));
                            pasteText = pasteText.Substring(pasteText.IndexOf("\t") + 1);
                        }
                        if (rowStr == string.Empty)
                            rowStr = null;
                        data[i, colIndex] = rowStr;
                    }
                    //截取下一行数据
                    pasteText = pasteText.Substring(pasteText.IndexOf("\n") + 1);
                }
                /*检测值是否是列头*/
                /*
                //获取当前选中单元格所在的列序号
                int columnindex = dgv.CurrentRow.Cells.IndexOf(dgv.CurrentCell);
                //获取获取当前选中单元格所在的行序号
                int rowindex = dgv.CurrentRow.Index;*/
                int columnindex = -1, rowindex = -1;
                int columnindextmp = -1, rowindextmp = -1;
                if (dgv.SelectedCells.Count != 0)
                {
                    columnindextmp = dgv.SelectedCells[0].ColumnIndex;
                    rowindextmp = dgv.SelectedCells[0].RowIndex;
                }
                //取到最左上角的 单元格编号
                foreach (DataGridViewCell cell in dgv.SelectedCells)
                {
                    //dgv.Rows[cell.RowIndex].Selected = true;
                    columnindex = cell.ColumnIndex;
                    if (columnindex > columnindextmp)
                    {
                        //交换
                        columnindex = columnindextmp;
                    }
                    else
                        columnindextmp = columnindex;
                    rowindex = cell.RowIndex;
                    if (rowindex > rowindextmp)
                    {
                        rowindex = rowindextmp;
                        rowindextmp = rowindex;
                    }
                    else
                        rowindextmp = rowindex;
                }
                if (kind == -1)
                {
                    columnindex = 0;
                    rowindex = 0;
                }

                ////如果行数超过当前列表行数
                //if (rowindex + rowNum + 1 > dgv.RowCount)
                //{
                //    int mm = rowNum + rowindex + 1 - dgv.RowCount;
                //    for (int ii = 0; ii < mm+1; ii++)
                //    {
                //        dgv.DataBindings.Clear();
                //        DataRow row = row = dgv.Tables[0].NewRow();
                //        dgv.Tables[0].Rows.InsertAt(row, ii + rowindex + 1);
                //    }
                //}

                //如果列数超过当前列表列数
                if (columnindex + columnNum + 1 > dgv.ColumnCount)
                {
                    int mmm = columnNum + columnindex + 1 - dgv.ColumnCount;
                    for (int iii = 0; iii < mmm; iii++)
                    {
                        dgv.DataBindings.Clear();
                        DataGridViewTextBoxColumn colum = new DataGridViewTextBoxColumn();
                        dgv.Columns.Insert(columnindex + 1, colum);
                    }
                }

                //增加超过的行列
                for (int j = 0; j < (rowNum + 1); j++)
                {
                    for (int colIndex = 0; colIndex < (columnNum + 1); colIndex++)
                    {
                        if (colIndex + columnindex < dgv.Columns.Count)
                        {
                            if (dgv.Columns[colIndex + columnindex].CellType.Name == "DataGridViewTextBoxCell")
                            {
                                if (dgv.Rows[j + rowindex].Cells[colIndex + columnindex].ReadOnly == false)
                                {
                                    dgv.Rows[j + rowindex].Cells[colIndex + columnindex].Value = data[j, colIndex];
                                    dgv.Rows[j + rowindex].Cells[colIndex + columnindex].Selected = true;
                                }
                            }
                        }
                    }
                }
                //清空剪切板内容
                if (b_cut)
                    Clipboard.Clear();
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region 右击保存定标曲线图片
        private void doublePanel1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    Point p = new Point(e.X, e.Y);
            //    MenuCurve.Show(doublePanel1, p);
            //}
        }
        string temp = string.Empty;
        string fileNameExt = string.Empty;
        string localFilePath = string.Empty;

        private void itemSave_Click(object sender, EventArgs e)
        {

            //Graphics gSrc = doublePanel1.CreateGraphics();
            ////Bitmap bmSave = new Bitmap(this.doublePanel1.Width, this.doublePanel1.Height, gSrc);
            //Bitmap bmSave = new Bitmap(this.doublePanel1.Width, this.doublePanel1.Height);
            //SaveFileDialog path = new SaveFileDialog();
            //path.Filter = "jpg文件(*.JPEG)|*.JPEG|All File(*.*)|*.*";
            //path.AddExtension = true;
            //path.OverwritePrompt = true;
            //if (path.ShowDialog() == DialogResult.OK)
            //{
            //    //this.TopMost = true;
            //    localFilePath = path.FileName.ToString(); //获得文件路径 
            //    fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径

            //}
            //if (localFilePath == "")
            //    return;

            //else
            //{
            //    #region 保存图片另一种方法
            //    //    Graphics gSave = Graphics.FromImage(bmSave);     //创建该位图的Graphics对
            //    //    //得到屏幕的DC 
            //    //    IntPtr dc1 = gSrc.GetHdc();
            //    //    //得到Bitmap的DC 
            //    //    IntPtr dc2 = gSave.GetHdc();
            //    //    //调用此API函数，实现屏幕捕获 
            //    //    BitBlt(dc2, 0, 0, doublePanel1.Width, doublePanel1.Height, dc1, 0, 0, 13369376);
            //    //    //释放掉屏幕的DC 
            //    //    gSrc.ReleaseHdc(dc1);
            //    //    //释放掉Bitmap的DC 
            //    //    gSave.ReleaseHdc(dc2);

            //    #endregion
            //    temp = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
            //    doublePanel1.DrawToBitmap(bmSave, new Rectangle(0, 0, bmSave.Width, bmSave.Height));
            //    bmSave.Save(@temp + "\\" + fileNameExt);
            //}
        }
        #endregion

        private void itemSave_Click_1(object sender, EventArgs e)
        {
        }

        private void btnClear_Click2(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(500);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Clipboard.Clear();
        }

        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)//删除选中内容
        {
            if (e.Control && e.KeyCode == Keys.V) // 是否按下ctrl+v
            {
                Paste(dataGridView1, "", 0, false);
            }
            if (e.Control && e.KeyCode == Keys.D) // 是否按下ctrl+D
            {
                //if (dataGridView1.SelectedRows.Count < 1) 
                //{
                //    MessageBox.Show("没有选中数据，请选择！");
                //    return;
                //}
                for (int i = 0; i < 500; i++)//对每个单元格内容进行删除
                {
                    if (dataGridView1.Rows[i].Cells[0].Selected == true)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = null;
                    }
                    if (dataGridView1.Rows[i].Cells[1].Selected == true)
                    {
                        dataGridView1.Rows[i].Cells[1].Value = null;
                    }
                    if (dataGridView1.Rows[i].Cells[2].Selected == true)
                    {
                        dataGridView1.Rows[i].Cells[2].Value = null;
                    }
                }
            }
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            count = 0;
            List<Data_Value> standard = new List<Data_Value>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)//DataGridView中的数据
            {
                if (dataGridView1[0, i].Value != null && dataGridView1[1, i].Value != null)
                    count++;
            }
            for (int j = 0; j < count; j++)
            {
                standard.Add(new Data_Value() { Data = double.Parse(dataGridView1[0, j].Value.ToString()), DataValue = double.Parse(dataGridView1[1, j].Value.ToString()) });
            }
            string tempString = tbTest.Text.ToString();
            double tempDouble = CountResultTestt(standard, double.Parse(tempString));
            rtbTest.Text = tempDouble.ToString();
        }

        private void Button1_Click2(object sender, EventArgs e)
        {
            for (int index = 0; index < dataGridView1.SelectedCells.Count; index++)
            {
                dataGridView1.SelectedCells[index].Value = null;
            }
        }

        private void GrbPara_Enter(object sender, EventArgs e)
        {

        }

        #endregion

 

    }
}

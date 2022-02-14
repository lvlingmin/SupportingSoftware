using fourLogistic.Caculate;
using fourLogistic.Calculate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace fourLogistic.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        ///夹心法主曲线列表
        /// </summary>
        List<Conc_PMT> ListMainPoint2 = new List<Conc_PMT>();
        /// <summary>
        ///竞争法主曲线列表
        /// </summary>
        List<Conc_PMT> ListMainPoint0 = new List<Conc_PMT>();
        private void MainForm_Load(object sender, EventArgs e)
        {
            #region 夹心
            dataGridView1.Rows.Add(500);
            double[] nongdu = { 0,0.15, 0.5, 1.5, 4.5, 50 };
            double[] faguagnzhi = { 100, 43205, 121537, 352335, 1149230, 11664921 };
            //for (int i = 0; i < nongdu.Length; i++)
            //{
            //    dataGridView1.Rows[i].Cells[0].Value = nongdu[i];
            //    dataGridView1.Rows[i].Cells[1].Value = faguagnzhi[i];
            //}
            //double[] nongdu = {  10, 40,100, 180, 400 };
            //double[] faguagnzhi = {  251795, 740941, 2081760, 2863564, 3543672 };
            double[] canshu = new double[5];
            canshu = canshuvalue(nongdu, faguagnzhi);    //计算4参数的numA numB numC numD  R2
            double[] result = new double[faguagnzhi.Length];
            double[] faguagnzhi1 = { 43000, 43500, 12250, 43205, 121537, 352335, 1149230, 11664921, 14388476 };
            double[] nongduall = { 0, 0.15, 0.5, 1.5, 4.5, 50 };
            double[] faguagnzhiall = { 12216, 43205, 121537, 352335, 1149230, 11664921 };
            result = resultvalue(faguagnzhi1, nongduall, faguagnzhiall);  //拟合浓度计算
            dataGridView3.Rows.Add(500);
            dataGridView4.Rows.Add(500);
            #endregion

            #region 竞争
            dataGridView2.Rows.Add(500);
            cmbFit.SelectedIndex = 5;
            cmbX.SelectedIndex = 0;
            cmbY.SelectedIndex = 0;
            double[] faguagnzhi0 = { 180000, 90000, 30000, 10000, 4500, 500 };
            //for (int i = 0; i < nongduall.Length; i++)
            //{
            //    dataGridView2.Rows[i].Cells[0].Value = nongduall[i];
            //    dataGridView2.Rows[i].Cells[1].Value = faguagnzhi0[i];
            //}
            dataGridView2.ClearSelection();
            rbtnPmtToConc.Checked = true;
            dataGridView5.Rows.Add(500);
            dataGridView6.Rows.Add(500);
            #endregion 
        }

        #region 
        #region
        public double numA = 0;
        public double numB = 0;
        public double numC = 0;
        public double numD = 0;
        public string R2 = "";
        public static object da1(double[] A_0)
        {
            checked
            {
                double[] array = new double[4];
                int num = A_0.Length - 1;
                array[0] = A_0[0];
                array[2] = A_0[0]; ;
                for (int i = 0; i <= num; i++)
                {
                    if (array[0] < A_0[i])
                    {
                        array[0] = A_0[i];
                        array[1] = (double)i;
                    }
                    if (array[2] > A_0[i])
                    {
                        array[2] = A_0[i];
                        array[3] = (double)i;
                    }
                }
                return array;
            }
        }
        public static object db1(double[] A_0, double[] A_1, double[] A_2)
        {
            int num = A_0.Length - 1;
            double[] array = new double[num + 1];
            double[] array2 = new double[num + 1];
            double num2 = 0.0;
            for (int i = 0; i <= num; i++)
            {
                unchecked
                {
                    array[i] = Math.Log((A_1[i] - A_2[2]) / (A_2[1] - A_1[i]));
                    array2[i] = Math.Log(A_0[i]);
                    num2 += array2[i];
                }
            }
            num2 /= (double)(num + 1);
            double[] array3 = (double[])da2(array2, array, 2);
            A_2[4] = -array3[1];
            A_2[3] = Math.Exp((array3[0] - array3[1] * num2) / -array3[1]);
            return A_2;
        }
        public static object db2(double[] A_0, double[] A_1, double[] A_2, float A_3)
        {
            checked
            {
                int num = A_0.Length - 1;
                double[,] array = new double[5, num + 1];
                double[] array2 = new double[num + 1];
                for (int i = 0; i <= num; i++)
                {
                    unchecked
                    {
                        if (A_2[4] > 0.0)
                        {
                            array2[i] = Math.Pow(A_0[i] / A_2[3], A_2[4]);
                        }
                        else
                        {
                            array2[i] = Math.Pow(A_2[3] / A_0[i], -A_2[4]);
                        }
                        array[4, i] = (A_1[i] - ((A_2[1] - A_2[2]) / (1.0 + array2[i]) + A_2[2])) * (double)A_3;
                        array[0, i] = 1.0 / (1.0 + array2[i]);
                        array[1, i] = 2.0 - array[0, i];
                        array[2, i] = A_2[4] / A_2[3] * ((A_2[1] - A_2[2]) / Math.Pow(1.0 + array2[i], 2.0)) * array2[i];
                        array[3, i] = -array2[i] * Math.Log(A_0[i] / A_2[3]) * ((A_2[1] - A_2[2]) / Math.Pow(1.0 + array2[i], 2.0));
                    }
                }
                double[,] array3 = new double[5, 5];
                int num3 = 0;
                do
                {
                    int num4 = 0;
                    do
                    {
                        for (int j = 0; j <= num; j++)
                        {
                            unchecked
                            {
                                if (num4 < 4)
                                {
                                    array3[num3, num4] += array[num3, j] * array[num4, j];
                                }
                                else
                                {
                                    array3[num3, num4] += array[num3, j] * array[4, j];
                                }
                            }
                        }
                        num4++;
                    }
                    while (num4 <= 4);
                    num3++;
                }
                while (num3 <= 3);
                double[] array4 = (double[])dd1(array3);
                double[] array5 = new double[5];
                double[] array6 = new double[2];
                double[] array7 = new double[num + 1];
                double[] array8 = (double[])da1(A_1);
                double[] array9 = new double[5];
                int num6 = -1;
                array6[0] = 0.0;
                num6 = -1;
                do
                {
                    int num7 = 1;
                    do
                    {
                        unchecked
                        {
                            if (num6 == -1)
                            {
                                array5[num7] = array4[checked(num7 - 1)] * 2.5 + A_2[num7];
                            }
                            else
                            {
                                array5[num7] = array4[checked(num7 - 1)] / Math.Pow(2.0, (double)num6) + A_2[num7];
                            }
                        }
                        num7++;
                    }
                    while (num7 <= 2);
                    if (array5[1] >= array8[0] & array5[2] <= array8[2])
                    {
                        array5 = (double[])db1(A_0, A_1, array5);
                        if (array5[3] > 0.0)
                        {
                            for (int k = 0; k <= num; k++)
                            {
                                unchecked
                                {
                                    if (array5[4] >= 0.0)
                                    {
                                        array7[k] = (array5[1] - array5[2]) / (1.0 + Math.Pow(A_0[k] / array5[3], array5[4])) + array5[2];
                                    }
                                    else
                                    {
                                        array7[k] = (array5[1] - array5[2]) / (1.0 + 1.0 / Math.Pow(A_0[k] / array5[3], -array5[4])) + array5[2];
                                    }
                                }
                            }
                            if (array6[0] > Convert.ToDouble(da3(A_1, array7).ToString()))
                            {
                                break;
                            }
                            array6[0] = Convert.ToDouble(da3(A_1, array7));
                            array6[1] = (double)num6;
                            int num9 = 0;
                            do
                            {
                                array9[num9] = array5[num9];
                                num9++;
                            }
                            while (num9 <= 4);
                        }
                    }
                    num6++;
                }
                while (num6 <= 100);
                return array9;
            }
        }

        public static object dd1(double[,] A_0)
        {
            checked
            {
                int num = (int)Math.Round(unchecked(Math.Pow((double)A_0.Length, 0.5) - 1.0));
                for (int i = 0; i <= num - 1; i++)
                {
                    double num3 = A_0[i, i];
                    if (A_0[i, i] == 0.0)
                    {
                        for (int j = i + 1; j <= num - 1; j++)
                        {
                            if (A_0[j, i] != 0.0)
                            {
                                for (int k = 0; k <= num; k++)
                                {
                                    A_0[num, k] = A_0[i, k];
                                    A_0[i, k] = A_0[j, k];
                                    A_0[j, k] = A_0[num, k];
                                }
                                i--;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int l = 0; l <= num; l++)
                        {
                            A_0[i, l] /= num3;
                        }
                        for (int m = i + 1; m <= num - 1; m++)
                        {
                            double num8 = A_0[m, i];
                            if (A_0[m, i] != 0.0)
                            {
                                for (int n = i; n <= num; n++)
                                {
                                    A_0[m, n] /= num8;
                                    A_0[m, n] = unchecked(A_0[i, n] - A_0[m, n]);
                                }
                            }
                        }
                    }
                }
                for (int num10 = num - 1; num10 >= 0; num10 += -1)
                {
                    double num11 = 0.0;
                    for (int num13 = num10; num13 <= num - 1; num13++)
                    {
                        unchecked
                        {
                            num11 += A_0[num10, num13] * A_0[num13, num];
                        }
                    }
                    A_0[num10, num] = unchecked(2.0 * A_0[num10, num] - num11);
                }
                double[] array = new double[num];
                for (int num15 = 0; num15 <= num - 1; num15++)
                {
                    array[num15] = A_0[num15, num];
                }
                A_0 = null;
                return array;
            }
        }
        public static object da2(double[] A_0, double[] A_1, int A_2)
        {

            double[] array = new double[20];
            double[] array2 = new double[20];
            double[] array3 = new double[20];
            double[] array4 = new double[]
             {
            0.0,
            0.0,
            0.0
             };
            int num = A_0.Length;
            double[] array5 = new double[num + 1];
            for (int i = 0; i <= A_2 - 1; i++)
            {
                array5[i] = 0.0;
            }
            if (A_2 > num)
            {
                A_2 = num;
            }
            if (A_2 > 20)
            {
                A_2 = 20;
            }
            double num3 = 0.0;
            double num5 = 0;
            for (int i = 0; i <= num - 1; i++)
            {
                unchecked
                {
                    num5 += A_0[i];
                    num3 += A_0[i] / (1.0 * (double)num);
                }
            }
            num5 /= (double)num;
            array3[0] = 1.0;
            double num6 = unchecked(1.0 * (double)num);
            double num7 = 0.0;
            double num8 = 0.0;
            for (int i = 0; i <= num - 1; i++)
            {
                unchecked
                {
                    num7 += A_0[i] - num3;
                    num8 += A_1[i];
                }
            }
            num8 /= num6;
            num7 /= num6;
            array5[0] = num8 * array3[0];
            double num13 = 0;
            if (A_2 > 1)
            {
                array2[1] = 1.0;
                array2[0] = -1.0 * num7;
                checked
                {
                    double num10 = 0.0;
                    num8 = 0.0;
                    double num11 = 0.0;
                    for (int i = 0; i <= num - 1; i++)
                    {
                        unchecked
                        {
                            num13 = A_0[i] - num3 - num7;
                            num10 += num13 * num13;
                            num8 += A_1[i] * num13;
                            num11 += (A_0[i] - num3) * num13 * num13;
                        }
                    }
                    num8 /= num10;
                    num7 = num11 / num10;
                    num13 = num10 / num6;
                    num6 = num10;
                }
                array5[1] = num8 * array2[1];
                array5[0] = num8 * array2[0] + array5[0];
            }
            checked
            {
                for (int j = 2; j <= A_2 - 1; j++)
                {
                    array[j] = array2[j - 1];
                    array[j - 1] = unchecked(-1.0 * num7 * array2[checked(j - 1)] + array2[checked(j - 2)]);
                    if (j >= 3)
                    {
                        for (int k = j - 2; k >= 1; k += -1)
                        {
                            array[k] = unchecked(-1.0 * num7 * array2[k] + array2[checked(k - 1)] - num13 * array3[k]);
                        }
                    }
                    array[0] = unchecked(-1.0 * num7 * array2[0] - num13 * array3[0]);
                    double num10 = 0.0;
                    num8 = 0.0;
                    double num11 = 0.0;
                    for (int i = 0; i <= num - 1; i++)
                    {
                        num13 = array[j];
                        for (int k = j - 1; k >= 0; k += -1)
                        {
                            num13 = unchecked(num13 * (A_0[i] - num3) + array[k]);
                        }
                        unchecked
                        {
                            num10 += num13 * num13;
                            num8 += A_1[i] * num13;
                            num11 += (A_0[i] - num3) * num13 * num13;
                        }
                    }
                    num8 /= num10;
                    num7 = num11 / num10;
                    num13 = num10 / num6;
                    num6 = num10;
                    array5[j] = unchecked(num8 * array[j]);
                    array2[j] = array[j];
                    for (int k = j - 1; k >= 0; k += -1)
                    {
                        array5[k] = unchecked(num8 * array[k] + array5[k]);
                        array3[k] = array2[k];
                        array2[k] = array[k];
                    }
                }
                array4[0] = 0.0;
                array4[1] = 0.0;
                array4[2] = 0.0;
                for (int i = 0; i <= num - 1; i++)
                {
                    num13 = array5[A_2 - 1];
                    for (int k = A_2 - 2; k >= 0; k += -1)
                    {
                        num13 = unchecked(array5[k] + num13 * (A_0[i] - num3));
                    }
                    unchecked
                    {
                        num7 = num13 - A_1[i];
                        if (Math.Abs(num7) > array4[2])
                        {
                            array4[2] = Math.Abs(num7);
                        }
                        array4[0] = array4[0] + num7 * num7;
                        array4[1] = array4[1] + Math.Abs(num7);
                    }
                }
                return array5;
            }
        }
        public static object da3(double[] A_0, double[] A_1)
        {
            int num = A_0.Length - 1;
            double num3 = 0.0;
            for (int i = 0; i <= num; i++)
            {
                unchecked
                {
                    num3 += A_0[i];
                }
            }
            num3 /= (double)(num + 1);
            double num5 = 0.0;
            double num6 = 0.0;
            for (int j = 0; j <= num; j++)
            {
                unchecked
                {

                    num5 += Math.Pow(Math.Abs(A_0[j] - A_1[j]), 2.0);

                    num6 += Math.Pow(Math.Abs(A_0[j] - num3), 2.0);
                }
            }
            double num7 = 0.0;
            if (num6 != 0.0)
            {
                num7 = 1.0 - num5 / num6;
            }
            else
            {
                num7 = 1.0;
            }
            return num7;
        }
        public static object c(double[,] A_0)
        {
            double[] array3 = new double[5];
            double[] array = new double[5];
            double[] array2 = new double[5];

            float a_3 = 1f;
            int num = A_0.GetLength(1) - 1;
            double[] array4 = new double[num + 1];
            double[] array5 = new double[num + 1];
            double[] array6 = new double[num + 1];
            double num2 = 0.0;
            for (int i = 0; i <= num; i++)
            {
                array4[i] = A_0[0, i];
                array5[i] = A_0[1, i];
            }
            double[] array7 = (double[])da1(array5);
            double[,] array8 = new double[2, 4];
            double[,] array9 = new double[2, 4];
            double[,] array10 = new double[2, 4];
            double[] array11 = new double[5];
            double[] array12 = new double[5];
            double[] array13 = new double[5];
            double num4 = array7[0] + 0.5;
            double num5 = array7[2] - 0.5;
            double num6 = array7[0] + 2.0;
            double num7 = array7[2] - 1.0;
            double num11 = 0;
            double num12 = 0;
            if (num > 3)
            {
                checked
                {
                    array9[0, 0] = array4[0];
                    array9[0, 1] = array4[1];
                    array9[0, 2] = array4[2];
                    array9[0, 3] = array4[3];
                    array9[1, 0] = array5[0];
                    array9[1, 1] = array5[1];
                    array9[1, 2] = array5[2];
                    array9[1, 3] = array5[3];
                    array10[0, 0] = array4[num - 3];
                    array10[0, 1] = array4[num - 2];
                    array10[0, 2] = array4[num - 1];
                    array10[0, 3] = array4[num];
                    array10[1, 0] = array5[num - 3];
                    array10[1, 1] = array5[num - 2];
                    array10[1, 2] = array5[num - 1];
                    array10[1, 3] = array5[num];
                }
                if (array5[0] > array5[num])
                {
                    array11 = (double[])c(array9);
                    array12 = (double[])c(array10);
                    if (array11[1] < array5[0])
                    {
                        num6 = array5[0] + Math.Abs(array5[0]) / 1000000.0;
                    }
                    else
                    {
                        num6 = array11[1];
                    }
                    if (array12[2] > array5[num])
                    {
                        num7 = array5[num] - Math.Abs(array5[num]) / 1000000.0;
                    }
                    else
                    {
                        num7 = array12[2];
                    }
                }
                else
                {
                    array11 = (double[])c(array9);
                    array12 = (double[])c(array10);
                    if (array11[2] > array5[0])
                    {
                        num7 = array5[0] - Math.Abs(array5[0]) / 1000000.0;
                    }
                    else
                    {
                        num7 = array11[2];
                    }
                    if (array12[1] < array5[num])
                    {
                        num6 = array5[num] + Math.Abs(array5[num]) / 1000000.0;
                    }
                    else
                    {
                        num6 = array12[1];
                    }
                }
                if (array4[0] == 0.0)
                {
                    array8[0, 0] = array4[1];
                    array8[0, 1] = array4[2];
                    array8[0, 2] = array4[3];
                    array8[0, 3] = array4[4];
                    array8[1, 0] = array5[1];
                    array8[1, 1] = array5[2];
                    array8[1, 2] = array5[3];
                    array8[1, 3] = array5[4];
                    if (array5[0] > array5[num])
                    {
                        array13 = (double[])c(array8);
                        if (array13[1] < array5[0])
                        {
                            num4 = array5[0] + Math.Abs(array5[0]) / 1000000.0;
                        }
                        else
                        {
                            num4 = array13[1];
                        }
                        if (array12[2] > array5[num])
                        {
                            num5 = array5[num] - Math.Abs(array5[num]) / 1000000.0;
                        }
                        else
                        {
                            num5 = array12[2];
                        }
                    }
                    else
                    {
                        array13 = (double[])c(array8);
                        if (array13[2] > array5[0])
                        {
                            num5 = array5[0] - Math.Abs(array5[0]) / 1000000.0;
                        }
                        else
                        {
                            num5 = array13[2];
                        }
                        if (array12[1] < array5[num])
                        {
                            num4 = array5[num] + Math.Abs(array5[num]) / 1000000.0;
                        }
                        else
                        {
                            num4 = array12[1];
                        }
                    }
                }

            }
            if (array4[0] == 0.0)
            {
                double num8 = Math.Pow(array4[1], 2.0) / (array4[2] * 1000.0);
                checked
                {
                    double num9 = array4[1] / 103.0;
                    int num10 = 0;
                    int num13 = 0;
                    int num15 = 0;
                    while (true)
                    {
                        switch (num10)
                        {
                            case 0:
                                {
                                    num11 = num4 - array7[0];
                                    num12 = array7[2] - num5;
                                    a_3 = 1f;
                                    array4[0] = num8;
                                    goto IL_585;
                                }
                            case 1:
                                {
                                    num11 = 1.0;
                                    num12 = 0.0001;
                                    a_3 = 1f;
                                    array4[0] = num8;
                                    goto IL_585;
                                }
                            case 2:
                                {
                                    num11 = num6 - array7[0];
                                    num12 = array7[2] - num7;
                                    a_3 = 1f;
                                    array4[0] = num8;
                                    if (!(num11 == 2.0 & num12 == 1.0))
                                    {
                                        goto IL_585;
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    num11 = num4 - array7[0];
                                    num12 = array7[2] - num5;
                                    a_3 = 1f;
                                    array4[0] = num9;
                                    goto IL_585;
                                }
                            case 4:
                                {
                                    num11 = 1.0;
                                    num12 = 0.0001;
                                    a_3 = 1f;
                                    array4[0] = num9;
                                    goto IL_585;
                                }
                            case 5:
                                {
                                    num11 = num6 - array7[0];
                                    num12 = array7[2] - num7;
                                    a_3 = 1f;
                                    array4[0] = num9;
                                    if (!(num11 == 2.0 & num12 == 1.0))
                                    {
                                        goto IL_585;
                                    }
                                    break;
                                }
                            default:
                                {
                                    goto IL_585;
                                }
                        }
                    IL_718:
                        num10++;
                        if (num10 > 5)
                        {
                            break;
                        }
                        continue;
                    IL_585:
                        array[1] = array7[0] + num11;
                        array[2] = array7[2] - num12;
                        array = (double[])db1(array4, array5, array);
                        num2 = 0.0;
                        num13 = 0;
                        do
                        {
                            array = (double[])db2(array4, array5, array, a_3);
                            if (array[1] == array[2])
                            {
                                break;
                            }
                            for (int j = 0; j <= num; j++)
                            {
                                unchecked
                                {
                                    if (array[4] >= 0.0)
                                    {
                                        array6[j] = (array[1] - array[2]) / (1.0 + Math.Pow(array4[j] / array[3], array[4])) + array[2];
                                    }
                                    else
                                    {
                                        array6[j] = (array[1] - array[2]) / (1.0 + 1.0 / Math.Pow(array4[j] / array[3], -array[4])) + array[2];
                                    }
                                }
                            }
                            if (num2 > Math.Abs(Convert.ToDouble(da3(array5, array6))))
                            {
                                break;
                            }
                            num2 = Convert.ToDouble(da3(array5, array6));
                            num15 = 0;
                            do
                            {
                                array2[num15] = array[num15];
                                num15++;
                            }
                            while (num15 <= 4);
                            num13++;
                        }
                        while (num13 <= 300);
                        array2[0] = num2;
                        if (array3[0] >= array2[0])
                        {
                            goto IL_718;
                        }
                        int num16 = 0;
                        while (true)
                        {
                            array3[num16] = array2[num16];
                            num16++;
                            if (num16 > 4)
                            {
                                goto IL_718;
                            }
                        }
                    }
                    double[,] array14 = new double[2, num + 1];
                    for (int k = 0; k <= num; k++)
                    {
                        array14[0, k] = A_0[0, k];
                        array14[1, k] = A_0[1, k];
                    }
                    array14[0, 0] = array4[1] / 103.0;
                    double[] array15 = (double[])c(array14);
                    if (array3[0] < array15[0])
                    {
                        int num18 = 0;
                        do
                        {
                            array3[num18] = array15[num18];
                            num18++;
                        }
                        while (num18 <= 4);
                    }
                }
            }
            else
            {
                int num19 = 0;
                while (true)
                {
                    switch (num19)
                    {
                        case 0:
                            {
                                num11 = num4 - array7[0];
                                num12 = array7[2] - num5;
                                a_3 = 1f;
                                goto IL_8C8;
                            }
                        case 1:
                            {
                                num11 = 1.0;
                                num12 = 0.0001;
                                a_3 = 1f;
                                goto IL_8C8;
                            }
                        case 2:
                            {
                                num11 = num6 - array7[0];
                                num12 = array7[2] - num7;
                                a_3 = 1.05f;
                                if (!(num11 == 2.0 & num12 == 1.0))
                                {
                                    goto IL_8C8;
                                }
                                break;
                            }
                        case 3:
                            {
                                num11 = num6 - array7[0];
                                num12 = array7[2] - num7;
                                a_3 = 1f;
                                if (!(num11 == 2.0 & num12 == 1.0))
                                {
                                    goto IL_8C8;
                                }
                                break;
                            }
                        case 4:
                            {
                                num11 = num6 - array7[0];
                                num12 = array7[2] - num7;
                                a_3 = 0.95f;
                                if (!(num11 == 2.0 & num12 == 1.0))
                                {
                                    goto IL_8C8;
                                }
                                break;
                            }
                        default:
                            {
                                goto IL_8C8;
                            }
                    }
                IL_A5B:
                    num19++;
                    if (num19 > 4)
                    {
                        break;
                    }
                    continue;
                IL_8C8:
                    array[1] = array7[0] + num11;
                    array[2] = array7[2] - num12;
                    checked
                    {
                        array = (double[])db1(array4, array5, array);
                        num2 = 0.0;
                        int num20 = 0;
                        do
                        {
                            array = (double[])db2(array4, array5, array, a_3);
                            if (array[1] == array[2])
                            {
                                break;
                            }
                            for (int l = 0; l <= num; l++)
                            {
                                unchecked
                                {
                                    if (array[4] >= 0.0)
                                    {
                                        array6[l] = (array[1] - array[2]) / (1.0 + Math.Pow(array4[l] / array[3], array[4])) + array[2];
                                    }
                                    else
                                    {
                                        array6[l] = (array[1] - array[2]) / (1.0 + 1.0 / Math.Pow(array4[l] / array[3], -array[4])) + array[2];
                                    }
                                }
                            }
                            num2 = Convert.ToDouble(da3(array5, array6));
                            int num22 = 0;
                            do
                            {
                                array2[num22] = array[num22];
                                num22++;
                            }
                            while (num22 <= 4);
                            num20++;
                        }
                        while (num20 <= 300);
                        array2[0] = num2;
                        if (array3[0] >= array2[0])
                        {
                            goto IL_A5B;
                        }
                        int num23 = 0;
                        while (true)
                        {
                            array3[num23] = array2[num23];
                            num23++;
                            if (num23 > 4)
                            {
                                goto IL_A5B;
                            }
                        }
                    }
                }
            }
            return array3;
        }

        public double[] resultvalue(double[] faguangzhi, double[] nongdu, double[] xiguangdu)
        {
            double[] arrary = new double[faguangzhi.Length];
            double tt = 0;
            for (int i = 0; i < faguangzhi.Length; i++)
            {
                tt = faguangzhi[i];
                if ((tt >= xiguangdu[0] && tt <= xiguangdu[1]) ||
                    (tt <= xiguangdu[0] && tt >= xiguangdu[1]))
                {
                    double Xvalue = xiguangdu[1];
                    double Yvalue = Math.Abs(Convert.ToDouble(numC) *
                        Math.Pow((Convert.ToDouble(numA) - Xvalue) / (Xvalue - Convert.ToDouble(numD)),
                        1.0 / Convert.ToDouble(numB)));
                    double[] linerX = { nongdu[0], Yvalue };
                    double[] linerY = { xiguangdu[0], Xvalue };
                    double[] linerCanshu = lineBack(linerX, linerY);
                    arrary[i] = linerCanshu[0] * tt + linerCanshu[1];
                    if (arrary[i] < 0)
                    {
                        arrary[i] = 0.0;
                    }
                }
                else
                    arrary[i] = Math.Abs(Convert.ToDouble(numC) *
                        Math.Pow((Convert.ToDouble(numA) - tt) / (tt - Convert.ToDouble(numD)),
                        1.0 / Convert.ToDouble(numB)));
            }
            return arrary;
        }

        public double[] canshuvalue(double[] nongdu, double[] xiguangdu)
        {
            double[] canshu5 = new double[5];
            zedGraphControl1.GraphPane.CurveList.Clear();
            double[,] arrary2 = new double[2, nongdu.Length];
            double[] array22 = new double[5];
            for (int i = 0; i < nongdu.Length; i++)
            {
                arrary2[0, i] = nongdu[i];
                arrary2[1, i] = xiguangdu[i];
            }

            array22 = (double[])c(arrary2);
            numA = array22[1];
            numB = array22[4];
            numC = array22[3];
            numD = array22[2];
            canshu5[0] = numA;
            canshu5[1] = numB;
            canshu5[2] = numC;
            canshu5[3] = numD;
            canshu5[4] = array22[0];
            return canshu5;
        }

        public double[] lineBack(double[] nongdu, double[] xiguangdu)
        {
            double[] array = new double[3];
            int cout = nongdu.Length;
            double heX = he(xiguangdu);
            double heY = he(nongdu);
            double x_ = heX / cout;
            double y_ = heY / cout;
            double xx1 = 0;
            for (int i = 0; i < cout; i++)
            {
                xx1 += Math.Pow(xiguangdu[i], 2); //吸光度和
            }
            double xx2 = Math.Pow(heX, 2) / cout;   //吸光度2次方平均值
            double xx = xx1 - xx2;

            double xy1 = 0;
            for (int i = 0; i < cout; i++)
            {
                xy1 += xiguangdu[i] * nongdu[i]; //吸光度和浓度乘积和
            }
            double xy2 = heX * heY / cout;         //吸光度和与浓度和乘积平均值
            double xy = xy1 - xy2;

            double yy1 = 0;
            for (int i = 0; i < cout; i++)
            {
                yy1 += Math.Pow(nongdu[i], 2);
            }
            double yy2 = Math.Pow(heY, 2) / cout;
            double b = xy / xx;
            double a = y_ - b * x_;
            array[0] = double.IsNaN(b) ? 0 : b;
            array[1] = a;
            double sumYA = 0;
            double sumYB = 0;

            for (int i = 0; i < cout; i++)
            {
                double yvalue = nongdu[i];
                double xvalue = xiguangdu[i];
                sumYA = Math.Pow((yvalue - a + b * xvalue), 2);
                sumYB = Math.Pow(yvalue - y_, 2);
            }
            array[2] = 1 - sumYA / sumYB;
            return array;
            //return double.IsNaN(b) ? 0 : b;
        }

        double he(double[] arrary)
        {
            double temp = 0;
            for (int i = 0; i < arrary.Length; i++)
            {
                temp += arrary[i];
            }
            return temp;
        }
        public string[] nongdu(string Blankcon, string OD0, string OD50, string OD100, string OD200, string OD400)
        {
            string[] canshu5 = new string[5];
            zedGraphControl1.GraphPane.CurveList.Clear();
            double[,] arrary2 = new double[2, 5];
            double[] array22 = new double[5];
            if (Blankcon != "0")
            {
                arrary2[0, 0] = double.Parse(Blankcon);
            }
            else
                arrary2[0, 0] = 0;
            arrary2[0, 1] = 50;
            arrary2[0, 2] = 100;
            arrary2[0, 3] = 200;
            arrary2[0, 4] = 400;
            if (OD0 != "0")
            {
                arrary2[1, 0] = double.Parse(OD0);
            }
            else
                arrary2[1, 0] = 0;
            arrary2[1, 1] = double.Parse(OD50);
            arrary2[1, 2] = double.Parse(OD100);
            arrary2[1, 3] = double.Parse(OD200);
            arrary2[1, 4] = double.Parse(OD400);
            array22 = (double[])c(arrary2);
            numA = array22[1];
            numB = array22[4];
            numC = array22[3];
            numD = array22[2];
            canshu5[0] = numA.ToString("#0.0000");
            canshu5[1] = numB.ToString("#0.0000");
            canshu5[2] = numC.ToString("#0.0000");
            canshu5[3] = numD.ToString("#0.0000");
            canshu5[4] = array22[0].ToString("#0.0000");
            R2 = array22[0].ToString("#0.0000");
            DataTable dt = new DataTable("cart");
            DataColumn dc1 = new DataColumn("consistence", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("absorbency", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("geneA", Type.GetType("System.String"));
            DataColumn dc4 = new DataColumn("geneB", Type.GetType("System.String"));
            DataColumn dc5 = new DataColumn("geneC", Type.GetType("System.String"));
            DataColumn dc6 = new DataColumn("geneD", Type.GetType("System.String"));
            DataColumn dc7 = new DataColumn("geneE", Type.GetType("System.String"));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            for (int i = 0; i < 5; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = arrary2[0, i].ToString();
                dr[1] = arrary2[1, i].ToString();
                dr[2] = numA.ToString();
                dr[3] = numB.ToString();
                dr[4] = numC.ToString();
                dr[5] = numD.ToString();
                dr[6] = 0;
                dt.Rows.Add(dr);
            }
            CreateGraph(zedGraphControl1, dt, 6);
            string path3 = "123.bmp";
            Image image = this.zedGraphControl1.GetImage();
            image.Save(path3, System.Drawing.Imaging.ImageFormat.Bmp);
            return canshu5;
        }

        public string nongduSample(string sampleOD, string numA, string numB, string numC, string numD)
        {
            double tt = Math.Abs(double.Parse(sampleOD));
            double SampleValue = 0;
            if ((Convert.ToDouble(numA) - tt) > 0)
            {
                SampleValue = Math.Abs(Convert.ToDouble(numC) * Math.Pow((Convert.ToDouble(numA) - tt) / (tt - Convert.ToDouble(numD)), 1.0 / Convert.ToDouble(numB)));
                if (SampleValue > 450)
                {
                    SampleValue = 400 * (1 + (Convert.ToDouble(numA) - tt) * 0.1);
                }
            }
            else
            {
                SampleValue = 400 * (1 + (tt - Convert.ToDouble(numA)) * 0.3);
            }
            return SampleValue.ToString("#0.0000");

        }
        #endregion

        #region 画图
        public void CreateGraph(ZedGraphControl z1, DataTable dt, int Scaltype)
        {
            // get a reference to the GraphPane
            GraphPane myPane = z1.GraphPane;

            //清空图表内容
            myPane.CurveList.Clear();

            //设置标题  
            myPane.Title.Text = "方程y=(" + numA.ToString("#0.0000") + "+" + numD.ToString("#0.0000") + ")/(1.0+(x/" + numC.ToString("#0.0000") + ")^" + numB.ToString("#0.0000") + ")+" + numD.ToString("#0.0000") + "\n       R^2=" + R2 + "\n" + "\n标准浓度吸光度曲线图";
            myPane.XAxis.Title.Text = "浓度";
            myPane.YAxis.Title.Text = "吸光度";
            DataTable dtScaling;
            dtScaling = dt;
            double[,] A_1 = new double[2, dtScaling.Rows.Count];
            if (dtScaling.Rows.Count > 0)
            {
                PointPairList cList = new PointPairList();
                PointPairList cList2 = new PointPairList();

                double[] xx = new double[dtScaling.Rows.Count];
                double[] yy = new double[dtScaling.Rows.Count];
                double[] xxx = new double[dtScaling.Rows.Count * 50];
                double[] yyy = new double[dtScaling.Rows.Count * 50];

                // Add a smoothed curve
                LineItem curve;

                if ((Scaltype == 6) || (Scaltype == 7) || (Scaltype == 8))
                {
                    for (int j = 0; j < dtScaling.Rows.Count; j++)
                    {
                        xx[j] = Convert.ToDouble(dtScaling.Rows[j][0]);
                        yy[j] = Convert.ToDouble(dtScaling.Rows[j][1]);

                        cList.Add(xx[j], yy[j]);
                        double[] A_0 = new double[4];
                        if (Scaltype == 8)
                        {
                            A_0[0] = numA;
                            A_0[1] = numB;
                            A_0[2] = numC;
                            A_0[3] = numD;
                        }
                        if (Scaltype == 6)
                        {
                            //每2个点之间再分50次计算
                            for (int k = 1; k <= 50; k++)
                            {
                                if (j == 0)
                                    xxx[k] = Convert.ToDouble(xx[j]) / 50.0 * k;
                                else
                                    xxx[k] = Convert.ToDouble(xx[j - 1]) + Convert.ToDouble(xx[j] - xx[j - 1]) / 50.0 * k;
                                //yyy[k] = Convert.ToDouble((numD - numC) / (1.0 + Math.Pow((double)(xxx[k] / numA), numB)) + numC);
                                yyy[k] = Convert.ToDouble((numA - numD) / (1.0 + Math.Pow((double)(xxx[k] / numC), numB)) + numD);
                                cList2.Add(xxx[k], yyy[k]);
                            }
                        }
                        if (Scaltype == 7)
                        {
                            //每2个点之间再分50次计算
                            for (int k = 1; k <= 50; k++)
                            {
                                if (j == 0)
                                    xxx[k] = Convert.ToDouble(xx[j]) / 50.0 * k;
                                else
                                    xxx[k] = Convert.ToDouble(xx[j - 1]) + Convert.ToDouble(xx[j] - xx[j - 1]) / 50.0 * k;
                                //yyy[k] = numA * Math.Pow(Math.Pow((numD - numC) / (Math.Abs(xxx[k]) - numC), 1.0 / numE) - 1.0, 1.0 / numB);

                                yyy[k] = Convert.ToDouble((numA - numD) / (1.0 + Math.Pow((double)(xxx[k] / numC), numB)) + numD);
                                cList2.Add(xxx[k], yyy[k]);
                            }
                        }
                        if (Scaltype == 8)
                        {
                            //每2个点之间再分50次计算
                            for (int k = 1; k <= 50; k++)
                            {
                                if (j == 0)
                                    xxx[k] = Convert.ToDouble(xx[j]) / 50.0 * k;
                                else
                                    xxx[k] = Convert.ToDouble(xx[j - 1]) + Convert.ToDouble(xx[j] - xx[j - 1]) / 50.0 * k;
                                ////Y = A+B*X+C*X^2+D*X^3
                                yyy[k] = A_0[0] + A_0[1] * xxx[k] + A_0[2] * xxx[k] * xxx[k] + A_0[3] * xxx[k] * xxx[k] * xxx[k];
                                cList2.Add(xxx[k], yyy[k]);
                            }
                        }
                    }

                    //画测试的点
                    curve = myPane.AddCurve("", cList, Color.Black, SymbolType.Diamond);
                    //去掉连接线
                    curve.Line.IsVisible = false;
                    //坐标点填充颜色
                    curve.Symbol.Fill = new Fill(Color.Red);
                    //坐标点大小
                    curve.Symbol.Size = 10;
                    //画拟合的点
                    curve = myPane.AddCurve("", cList2, Color.Red, SymbolType.Diamond);
                    //坐标点填充颜色
                    //curve.Symbol.Fill = new Fill(Color.Blue);
                    //坐标点大小
                    curve.Symbol.Size = 0;
                    //曲线平滑
                    curve.Line.IsSmooth = true;
                    //曲线平滑张力
                    curve.Line.SmoothTension = 0.5F;

                    double _minX = 0, _maxX = 0, _maxY = 0, _minY = 0;
                    for (int k = 1; k <= 50; k++)
                    {
                        if (_minX > xxx[k])
                        {
                            _minX = xxx[k];
                        }
                        if (_maxX < xxx[k])
                        {
                            _maxX = xxx[k];
                        }
                        if (_minY > yyy[k])
                        {
                            _minY = yyy[k];
                        }
                        if (_maxY < yyy[k])
                        {
                            _maxY = yyy[k];
                        }
                    }

                    //设置X轴值范围
                    myPane.XAxis.Scale.Min = _minX;
                    myPane.XAxis.Scale.Max = _maxX;
                    ////设置Y轴值范围
                    //myPane.YAxis.Scale.Min = _minY;
                    //myPane.YAxis.Scale.Max = _maxY;
                }
                else
                {
                    for (int j = 0; j < dtScaling.Rows.Count; j++)
                    {
                        xx[j] = Convert.ToDouble(dtScaling.Rows[j][0]);
                        yy[j] = Convert.ToDouble(dtScaling.Rows[j][1]);

                        cList.Add(xx[j], yy[j]);
                    }
                    //画测试的点
                    curve = myPane.AddCurve("", cList, Color.Black, SymbolType.Diamond);
                    //连接线
                    curve.Line.IsVisible = true;
                    //坐标点填充颜色
                    curve.Symbol.Fill = new Fill(Color.Red);
                    //坐标点大小
                    curve.Symbol.Size = 10;

                    double _minX = 0, _maxX = 0;
                    for (int j = 0; j < dtScaling.Rows.Count; j++)
                    {
                        if (_minX > xx[j])
                        {
                            _minX = xx[j];
                        }
                        if (_maxX < xx[j])
                        {
                            _maxX = xx[j];
                        }
                    }

                    //设置X轴值范围
                    myPane.XAxis.Scale.Min = _minX;
                    myPane.XAxis.Scale.Max = _maxX;
                }
            }
            //显示图表中的网格线
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0F);

            z1.AxisChange();
            z1.Invalidate();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < dataGridView1.SelectedCells.Count; index++)
            {
                dataGridView1.SelectedCells[index].Value = null;
            }
        }

        #region 复制
        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dataGridView1.GetClipboardContent());
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            Paste(dataGridView1, "", 0, false);
        }
        #endregion

        #region 粘贴功能
        //public int Paste(DataGridView dgv, string pasteText, int kind, bool b_cut)
        //{
        //    try
        //    {
        //        if (kind == 0)
        //        {
        //            pasteText = Clipboard.GetText();
        //        }
        //        if (string.IsNullOrEmpty(pasteText))
        //            return -1;
        //        int rowNum = 0;
        //        int columnNum = 0;
        //        //获得当前剪贴板内容的行、列数
        //        for (int i = 0; i < pasteText.Length; i++)
        //        {
        //            if (pasteText.Substring(i, 1) == "\t")
        //            {
        //                columnNum++;
        //            }
        //            if (pasteText.Substring(i, 1) == "\n")
        //            {
        //                rowNum++;
        //            }
        //        }
        //        Object[,] data;
        //        //粘贴板上的数据来自于EXCEL时，每行末都有\n，在DATAGRIDVIEW内复制时，最后一行末没有\n
        //        if (pasteText.Substring(pasteText.Length - 1, 1) == "\n")
        //        {
        //            rowNum = rowNum - 1;
        //        }
        //        columnNum = columnNum / (rowNum + 1);
        //        data = new object[rowNum + 1, columnNum + 1];

        //        String rowStr;
        //        //对数组赋值
        //        for (int i = 0; i < (rowNum + 1); i++)
        //        {
        //            for (int colIndex = 0; colIndex < (columnNum + 1); colIndex++)
        //            {
        //                rowStr = null;
        //                //一行中的最后一列
        //                if (colIndex == columnNum && pasteText.IndexOf("\r") != -1)
        //                {
        //                    rowStr = pasteText.Substring(0, pasteText.IndexOf("\r"));
        //                }
        //                //最后一行的最后一列
        //                if (colIndex == columnNum && pasteText.IndexOf("\r") == -1)
        //                {
        //                    rowStr = pasteText.Substring(0);
        //                }
        //                //其他行列
        //                if (colIndex != columnNum)
        //                {
        //                    rowStr = pasteText.Substring(0, pasteText.IndexOf("\t"));
        //                    pasteText = pasteText.Substring(pasteText.IndexOf("\t") + 1);
        //                }
        //                if (rowStr == string.Empty)
        //                    rowStr = null;
        //                data[i, colIndex] = rowStr;
        //            }
        //            //截取下一行数据
        //            pasteText = pasteText.Substring(pasteText.IndexOf("\n") + 1);
        //        }
        //        /*检测值是否是列头*/
        //        /*
        //        //获取当前选中单元格所在的列序号
        //        int columnindex = dgv.CurrentRow.Cells.IndexOf(dgv.CurrentCell);
        //        //获取获取当前选中单元格所在的行序号
        //        int rowindex = dgv.CurrentRow.Index;*/
        //        int columnindex = -1, rowindex = -1;
        //        int columnindextmp = -1, rowindextmp = -1;
        //        if (dgv.SelectedCells.Count != 0)
        //        {
        //            columnindextmp = dgv.SelectedCells[0].ColumnIndex;
        //            rowindextmp = dgv.SelectedCells[0].RowIndex;
        //        }
        //        //取到最左上角的 单元格编号
        //        foreach (DataGridViewCell cell in dgv.SelectedCells)
        //        {
        //            //dgv.Rows[cell.RowIndex].Selected = true;
        //            columnindex = cell.ColumnIndex;
        //            if (columnindex > columnindextmp)
        //            {
        //                //交换
        //                columnindex = columnindextmp;
        //            }
        //            else
        //                columnindextmp = columnindex;
        //            rowindex = cell.RowIndex;
        //            if (rowindex > rowindextmp)
        //            {
        //                rowindex = rowindextmp;
        //                rowindextmp = rowindex;
        //            }
        //            else
        //                rowindextmp = rowindex;
        //        }
        //        if (kind == -1)
        //        {
        //            columnindex = 0;
        //            rowindex = 0;
        //        }

        //        ////如果行数超过当前列表行数
        //        //if (rowindex + rowNum + 1 > dgv.RowCount)
        //        //{
        //        //    int mm = rowNum + rowindex + 1 - dgv.RowCount;
        //        //    for (int ii = 0; ii < mm+1; ii++)
        //        //    {
        //        //        dgv.DataBindings.Clear();
        //        //        DataRow row = row = dgv.Tables[0].NewRow();
        //        //        dgv.Tables[0].Rows.InsertAt(row, ii + rowindex + 1);
        //        //    }
        //        //}

        //        //如果列数超过当前列表列数
        //        if (columnindex + columnNum + 1 > dgv.ColumnCount)
        //        {
        //            int mmm = columnNum + columnindex + 1 - dgv.ColumnCount;
        //            for (int iii = 0; iii < mmm; iii++)
        //            {
        //                dgv.DataBindings.Clear();
        //                DataGridViewTextBoxColumn colum = new DataGridViewTextBoxColumn();
        //                dgv.Columns.Insert(columnindex + 1, colum);
        //            }
        //        }

        //        //增加超过的行列
        //        for (int j = 0; j < (rowNum + 1); j++)
        //        {
        //            for (int colIndex = 0; colIndex < (columnNum + 1); colIndex++)
        //            {
        //                if (colIndex + columnindex < dgv.Columns.Count)
        //                {
        //                    if (dgv.Columns[colIndex + columnindex].CellType.Name == "DataGridViewTextBoxCell")
        //                    {
        //                        if (dgv.Rows[j + rowindex].Cells[colIndex + columnindex].ReadOnly == false)
        //                        {
        //                            dgv.Rows[j + rowindex].Cells[colIndex + columnindex].Value = data[j, colIndex];
        //                            dgv.Rows[j + rowindex].Cells[colIndex + columnindex].Selected = true;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        //清空剪切板内容
        //        if (b_cut)
        //            Clipboard.Clear();
        //        return 1;
        //    }
        //    catch
        //    {
        //        return -1;
        //    }
        //}
        #endregion

        #region 计算
        private void btnFit_Click(object sender, EventArgs e)
        {
            #region 计算参数(这部分是温英立的程序，不在修改直接用吧)
            List<Conc_PMT> mainDatas = GetMainCurve();
            double[] nongdu = new double[mainDatas.Count - 1];
            double[] faguagnzhi = new double[mainDatas.Count - 1];
            for (int index = 1; index < mainDatas.Count; index++)
            {
                nongdu[index - 1] = mainDatas[index].Conc;
                faguagnzhi[index - 1] = mainDatas[index].PMT;
            }
            double[] canshu = new double[5];
            canshu = canshuvalue(nongdu, faguagnzhi);    //计算4参数的numA numB numC numD  R2
            richTextBox1.Text = "\n A= " + canshu[0] +
                "\n B= " + canshu[1] +
                "\n C= " + canshu[2] +
                "\n D= " + canshu[3] +
                " R^2 =" + canshu[4] * 0.999;
            List<double> preconcs = new List<double>();

            double[] result = new double[1];
            double[] faguagnzhi1 = { 43000, 43500, 12250, 43205, 121537, 352335, 1149230, 11664921, 14388476 };

            double[] nongduall = new double[mainDatas.Count];
            double[] faguagnzhiall = new double[mainDatas.Count];
            for (int index = 0; index < mainDatas.Count; index++)
            {
                nongduall[index] = mainDatas[index].Conc;
                faguagnzhiall[index] = mainDatas[index].PMT;
            }
            #endregion

            #region  发光值到浓度
            if (rbtnPmtToConc0.Checked) //发光值到浓度
            {
                #region 预测
                List<double> predata = new List<double>();
                for (int i = 1; i < mainDatas.Count; i++)//想要预测的数据
                {
                    double[] array = { Convert.ToDouble(dataGridView1[1, i].Value) };
                    result = resultvalue(array,
                        nongduall, faguagnzhiall);  //拟合浓度计算
                                                    //dataGridView1[2, i].Value = result[0];
                    predata.Add(result[0]);
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)//想要预测的数据
                {
                    if (dataGridView1[0, i].Value != null && dataGridView1[1, i].Value != null)
                    {
                        if (Convert.ToDouble(dataGridView1[1, i].Value) > mainDatas[1].PMT)
                        {
                            double[] array = { Convert.ToDouble(dataGridView1[1, i].Value) };
                            result = resultvalue(array,
                                nongduall, faguagnzhiall);
                            dataGridView1[2, i].Value = result[0].ToString("0.000");
                            preconcs.Add(result[0]);
                        }
                        else
                        {
                            List<Data_Value> temp = new List<Data_Value>();
                            temp.Add(new Data_Value() { Data = mainDatas[0].Conc, DataValue = mainDatas[0].PMT });
                            temp.Add(new Data_Value() { Data = predata[0], DataValue = mainDatas[1].PMT });
                            double tempNum = CountLinearResult(temp, Convert.ToDouble(dataGridView1[1, i].Value));
                            dataGridView1[2, i].Value = (tempNum <= mainDatas[0].Conc ?
                                mainDatas[0].Conc + 0.001 : tempNum).ToString("0.000");
                            preconcs.Add(tempNum);
                        }
                    }
                    if (dataGridView1[0, i].Value == null && dataGridView1[1, i].Value != null)
                    {
                        if (Convert.ToDouble(dataGridView1[1, i].Value) > mainDatas[1].PMT)
                        {
                            double[] array = { Convert.ToDouble(dataGridView1[1, i].Value) };
                            result = resultvalue(array,
                                nongduall, faguagnzhiall);
                            double d = mainDatas[mainDatas.Count() - 1].Conc;
                            dataGridView1[2, i].Value = (result[0] > mainDatas[mainDatas.Count() - 1].Conc * 1.2 || double.IsNaN(result[0])) ?
                                ">" + mainDatas[mainDatas.Count() - 1].Conc * 1.2 : result[0].ToString("0.000");
                        }
                        else
                        {
                            List<Data_Value> temp = new List<Data_Value>();
                            temp.Add(new Data_Value() { Data = mainDatas[0].Conc, DataValue = mainDatas[0].PMT });
                            temp.Add(new Data_Value() { Data = predata[0], DataValue = mainDatas[1].PMT });
                            double tempNum = CountLinearResult(temp, Convert.ToDouble(dataGridView1[1, i].Value));
                            dataGridView1[2, i].Value = (tempNum <= mainDatas[0].Conc ? mainDatas[0].Conc + 0.001 : tempNum).ToString("0.000");
                        }
                    }
                }

                #region 画图
                DataTable datas = new DataTable();
                datas.Columns.Add("consistence", Type.GetType("System.String"));
                datas.Columns.Add("absorbency", Type.GetType("System.String"));
                datas.Columns.Add("geneA", Type.GetType("System.String"));
                datas.Columns.Add("geneB", Type.GetType("System.String"));
                datas.Columns.Add("geneC", Type.GetType("System.String"));
                datas.Columns.Add("geneD", Type.GetType("System.String"));
                datas.Columns.Add("geneE", Type.GetType("System.String"));
                for (int i = 0; i < mainDatas.Count; i++)
                {
                    if (i == 0)
                    {
                        datas.Rows.Add(preconcs[0], mainDatas[i].PMT, canshu[0].ToString(),
                        canshu[1].ToString(), canshu[2].ToString(), canshu[3].ToString(), 0.ToString());
                    }
                    else
                    {
                        datas.Rows.Add(predata[i - 1], mainDatas[i].PMT, canshu[0].ToString(),
                        canshu[1].ToString(), canshu[2].ToString(), canshu[3].ToString(), 0.ToString());
                    }
                }
                zedGraphControl1.GraphPane.CurveList.Clear();
                CreateGraph(zedGraphControl1, datas, mainDatas.Count);
                string path3 = "123.bmp";
                Image image = this.zedGraphControl1.GetImage();
                image.Save(path3, System.Drawing.Imaging.ImageFormat.Bmp);
                #endregion
                #endregion
            }
            #endregion

            #region 浓度到发光值
            if (rbtnConcToPmt0.Checked) //浓度到发光值
            {
                List<Data_Value> data_Values = GetMainData_DataValue();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)//想要预测的数据
                {
                    if (dataGridView1[0, i].Value == null && dataGridView1[1, i].Value == null && dataGridView1[2, i].Value != null)
                    {
                        double conc = double.Parse(dataGridView1[2, i].Value.ToString());
                        if (conc == 0)
                        {
                            conc = 0.001;
                        }
                        if ((data_Values[0].Data <= conc && conc <= data_Values[1].Data))
                        {
                            double resultLine = GetLineResultSandwich(Convert.ToDouble(dataGridView1[2, i].Value), data_Values);
                            dataGridView1[1, i].Value = resultLine < data_Values[0].Data ? (data_Values[0].Data + 0.001) : resultLine;
                        }
                        else
                        {
                            double resultSandwich = GetResultSandwich(Convert.ToDouble(dataGridView1[2, i].Value), canshu);
                            dataGridView1[1, i].Value =
                            resultSandwich > data_Values[data_Values.Count - 1].DataValue * 1.2 ?
                            ">" + data_Values[data_Values.Count - 1].DataValue * 1.2 : resultSandwich.ToString();
                        }
                    }
                }
            }
            #endregion

        }
       
        /// <summary>
        /// 夹心法由浓度计算发光值
        /// </summary>
        /// <param name="xValue">浓度</param>
        /// <param name="parameters">ABCD四个参数</param>
        /// <returns></returns>
        public double GetResultSandwich(double xValue, double[] parameters)
        {
            //if (xValue < 0)
            //    xValue = 0;
            //return parameters[0] + parameters[1] / (1 + Math.Exp(-(parameters[2] + parameters[3] * Math.Log(xValue))));
            if (xValue < 0)
                xValue = 0;
            return (parameters[0] - parameters[3]) / (1 + Math.Pow(xValue / parameters[2], parameters[1])) + parameters[3];
            //return _pars[2] * (Math.Pow((((_pars[0] - _pars[3]) / (yValue - _pars[3])) - 1), (1 / _pars[1])));
        }
        /// <summary>
        ///  夹心法由浓度计算发光值（初始直线部分）
        /// </summary>
        /// <param name="xValue">浓度</param>
        /// <param name="parameters">发光值</param>
        /// <returns></returns>
        public double GetLineResultSandwich(double xValue, List<Data_Value> Linear2Points)
        {
            Linear linear = new Linear();
            linear.AddData(Linear2Points);
            linear.Fit();
            return linear.GetResult(xValue);
        }
        /// <summary>
        /// 计算线性
        /// </summary>
        /// <param name="ltData">定标数据</param>
        /// <param na me="PMT">发光值</param>
        /// <returns></returns>
        double CountLinearResult(List<Data_Value> ltData, double PMT)
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
        /// 主曲线数据
        /// </summary>
        /// <returns></returns>
        private List<Conc_PMT> GetMainCurve()
        {
            List<Conc_PMT> mainData = new List<Conc_PMT>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)//想要预测的数据
            {
                if (dataGridView1[0, i].Value != null && dataGridView1[1, i].Value != null)
                {
                    mainData.Add(new Conc_PMT()
                    {
                        Conc = Convert.ToDouble(dataGridView1[0, i].Value),
                        PMT = Convert.ToDouble(dataGridView1[1, i].Value)
                    });
                }
            }
            return mainData;
        }
        /// <summary>
        /// 主曲线数据
        /// </summary>
        /// <returns></returns>
        private List<Data_Value> GetMainData_DataValue()
        {
            List<Data_Value> mainData = new List<Data_Value>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)//想要预测的数据
            {
                if (dataGridView1[0, i].Value != null && dataGridView1[1, i].Value != null)
                {
                    mainData.Add(new Data_Value()
                    {
                        Data = Convert.ToDouble(dataGridView1[0, i].Value),
                        DataValue = Convert.ToDouble(dataGridView1[1, i].Value)
                    });
                }
            }
            return mainData;
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)

            {
                for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
                {
                    this.dataGridView1.Rows[i].Cells[j].Value = null;
                }
            }
        }
        #endregion

        #region 
        DataTable dtCountConc = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView2.Rows.Add(500);
            cmbFit.SelectedIndex = 5;
            cmbX.SelectedIndex = 0;
            cmbY.SelectedIndex = 0;
            dataGridView2.ClearSelection();
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
            CalculateFactory ft = new CalculateFactory();
            Calculater er = null;
            er = ft.getCaler(cmbFit.SelectedIndex);//取到公式
            for (int i = 0; i < dataGridView2.Rows.Count; i++)//DataGridView中的数据
            {
                if (dataGridView2[0, i].Value != null && dataGridView2[1, i].Value != null)
                    count++;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("consistence", typeof(float));
            dt.Columns.Add("absorbency", typeof(float));
            for (int j = 0; j < count; j++)
            {
                try
                {
                    ltData.Add(new Data_Value() { Data = double.Parse(dataGridView2[0, j].Value.ToString()), DataValue = double.Parse(dataGridView2[1, j].Value.ToString()) });
                    CurveData.Add(new Data_Value() { Data = double.Parse(dataGridView2[0, j].Value.ToString()), DataValue = double.Parse(dataGridView2[1, j].Value.ToString()) });
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
                    for (int i = 0; i < ltData.Count; i++)
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
                    string tempValue = dataGridView2.Rows[i].Cells[1].Value.ToString();
                    double paradoubleY = double.Parse(Convert.ToDecimal(tempValue).ToString("F2"));
                    double temp = (paradoubleA - paradoubleY) / (paradoubleY - paradoubleD);
                    if (temp <= 0)
                    {
                        temp = -temp;
                    }
                    double powNum = 1 / paradoubleB;
                    double paradoubleX = Math.Pow(temp, powNum);
                    MessageBox.Show(paradoubleX.ToString());
                    dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(paradoubleX).ToString("F2");
                }
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
            List<Data_Value> litData_Value = new List<Data_Value>();
            Calculater er1 = fy.getCaler(listPar);
            dc.paintEliseScaling(ShowCurvePnl, dt, er.GetResult, "", cmbFit.SelectedIndex);
            GetParaAndShow(er);//设置参数显示
            List<double> lsDouble = new List<double>();
            foreach (var v in ltData)
            {
                lsDouble.Add(v.DataValue);
            }
            if (rbtnPmtToConc.Checked)
            {
                for (int j = 0; j < count; j++)//原有数据的预测值
                {
                    double PMT = double.Parse(dataGridView2[1, j].Value.ToString());
                    if (PMT <= CurveData[0].DataValue && PMT > CurveData[1].DataValue)
                    {
                        double lineResultInverse = GetLineInverseResult(PMT);
                        double resultInverse = er.GetResultInverse(CurveData[1].DataValue);
                        dataGridView2[2, j].Value = (lineResultInverse > resultInverse ?
                            resultInverse : lineResultInverse).ToString("0.000");
                        continue;
                    }
                    double conc;
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
                    if (conc <= ltData[0].Data)
                    {
                        conc = ltData[0].Data + 0.001;
                    }
                    if (conc >= (ltData[ltData.Count - 1].Data + ltData[ltData.Count - 1].Data * 0.3))
                    {
                        dataGridView2[2, j].Value = ">" + ltData[ltData.Count - 1].Data * 1.3;
                    }
                    else
                    {
                        dataGridView2[2, j].Value = Convert.ToDouble(conc).ToString("0.000");
                    }
                }
                for (int i = 0; i < dataGridView2.Rows.Count; i++)//想要预测的数据
                {
                    if (dataGridView2[0, i].Value == null && dataGridView2[1, i].Value != null)
                    {
                        double conc;
                        double PMT = double.Parse(dataGridView2[1, i].Value.ToString());
                        if (PMT <= CurveData[0].DataValue && PMT > CurveData[1].DataValue)
                        {
                            double lineResultInverse = GetLineInverseResult(PMT);
                            double resultInverse = er.GetResultInverse(CurveData[1].DataValue);
                            dataGridView2[2, i].Value = (lineResultInverse > resultInverse ?
                                resultInverse : lineResultInverse).ToString("0.000");
                            continue;
                        }
                        if (PMT < ltData[0].DataValue && ltData[0].DataValue < ltData[1].DataValue)
                        {
                            conc = ltData[0].Data;
                        }
                        else
                        {
                            conc = er.GetResultInverse(PMT);
                            if (double.IsNaN(conc))
                            {
                                double borderPMT = er.GetResult(ltData[ltData.Count - 1].DataValue * 1.3);

                                if (PMT < borderPMT)
                                {
                                    conc = ltData[ltData.Count - 1].DataValue * 1.3;
                                    dataGridView2[2, i].Value = ">" + ltData[ltData.Count - 1].Data * 1.3;
                                    continue;
                                }

                                if ((ltData[0].DataValue < ltData[1].DataValue && PMT <= ltData[0].DataValue) ||
                                    (ltData[0].DataValue > ltData[1].DataValue && PMT >= ltData[0].DataValue))
                                {
                                    conc = 0.001;
                                }
                                else if (PMT >= ltData[1].DataValue && PMT <= ltData[0].DataValue)
                                {
                                    conc = ltData[0].Data + 0.001;
                                }
                                else
                                {
                                    conc = ltData[ltData.Count - 1].Data;
                                }
                            }
                        }
                        if (conc <= ltData[0].Data)
                        {
                            conc = ltData[0].Data + 0.001;
                        }
                        if (conc >= (ltData[ltData.Count - 1].Data + ltData[ltData.Count - 1].Data * 0.3))
                        {
                            dataGridView2[2, i].Value = ">" + ltData[ltData.Count - 1].Data * 1.3;
                        }
                        else
                        {
                            dataGridView2[2, i].Value = Convert.ToDouble(conc).ToString("0.000");
                        }
                    }
                }
            }
            if (rbtnConcToPmt.Checked)
            {
                for (int i = 0; i < dataGridView2.Rows.Count; i++)//想要预测的数据
                {
                    double PMT;
                    if (dataGridView2[0, i].Value == null && dataGridView2[1, i].Value == null && dataGridView2[2, i].Value != null)
                    {
                        double conc = double.Parse(dataGridView2[2, i].Value.ToString());
                        if (conc >= CurveData[0].Data && conc <= CurveData[1].Data)
                        {
                            dataGridView2[1, i].Value = GetLineResult(conc).ToString("0.000");
                            continue;
                        }
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
                            dataGridView2[1, i].Value = Convert.ToDouble(PMT).ToString("0.000");
                        }
                        else
                        {
                            PMT = er.GetResult(conc);
                            dataGridView2[1, i].Value = Convert.ToDouble(PMT).ToString("0.000");
                        }
                    }
                }
            }
            #endregion
        }
        public void EquationCurvet2(DataGridView dataGridView, Calculater er, DataTable dt,Panel showCurvePnl)
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
            List<Data_Value> litData_Value = new List<Data_Value>();
            Calculater er1 = fy.getCaler(listPar);
            dc.paintEliseScaling(showCurvePnl, dt, er.GetResult, "", cmbFit.SelectedIndex);
            GetParaAndShow2(er);//设置参数显示
            List<double> lsDouble = new List<double>();
            foreach (var v in ltData)
            {
                lsDouble.Add(v.DataValue);
            }
            if (rbtnPmtToConc.Checked)
            {
                for (int j = 0; j < count; j++)//原有数据的预测值
                {
                    double PMT = double.Parse(dataGridView[1, j].Value.ToString());
                    if (PMT <= CurveData[0].DataValue && PMT > CurveData[1].DataValue)
                    {
                        double lineResultInverse = GetLineInverseResult(PMT);
                        double resultInverse = er.GetResultInverse(CurveData[1].DataValue);
                        dataGridView[2, j].Value = (lineResultInverse > resultInverse ?
                            resultInverse : lineResultInverse).ToString("0.000");
                        continue;
                    }
                    double conc;
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
                    if (conc <= ltData[0].Data)
                    {
                        conc = ltData[0].Data + 0.001;
                    }
                    if (conc >= (ltData[ltData.Count - 1].Data + ltData[ltData.Count - 1].Data * 0.3))
                    {
                        dataGridView[2, j].Value = ">" + ltData[ltData.Count - 1].Data * 1.3;
                    }
                    else
                    {
                        dataGridView[2, j].Value = Convert.ToDouble(conc).ToString("0.000");
                    }
                }
                for (int i = 0; i < dataGridView.Rows.Count; i++)//想要预测的数据
                {
                    if (dataGridView[0, i].Value == null && dataGridView[1, i].Value != null)
                    {
                        double conc;
                        double PMT = double.Parse(dataGridView[1, i].Value.ToString());
                        if (PMT <= CurveData[0].DataValue && PMT > CurveData[1].DataValue)
                        {
                            double lineResultInverse = GetLineInverseResult(PMT);
                            double resultInverse = er.GetResultInverse(CurveData[1].DataValue);
                            dataGridView[2, i].Value = (lineResultInverse > resultInverse ?
                                resultInverse : lineResultInverse).ToString("0.000");
                            continue;
                        }
                        if (PMT < ltData[0].DataValue && ltData[0].DataValue < ltData[1].DataValue)
                        {
                            conc = ltData[0].Data;
                        }
                        else
                        {
                            conc = er.GetResultInverse(PMT);
                            if (double.IsNaN(conc))
                            {
                                double borderPMT = er.GetResult(ltData[ltData.Count - 1].DataValue * 1.3);

                                if (PMT < borderPMT)
                                {
                                    conc = ltData[ltData.Count - 1].DataValue * 1.3;
                                    dataGridView[2, i].Value = ">" + ltData[ltData.Count - 1].Data * 1.3;
                                    continue;
                                }

                                if ((ltData[0].DataValue < ltData[1].DataValue && PMT <= ltData[0].DataValue) ||
                                    (ltData[0].DataValue > ltData[1].DataValue && PMT >= ltData[0].DataValue))
                                {
                                    conc = 0.001;
                                }
                                else if (PMT >= ltData[1].DataValue && PMT <= ltData[0].DataValue)
                                {
                                    conc = ltData[0].Data + 0.001;
                                }
                                else
                                {
                                    conc = ltData[ltData.Count - 1].Data;
                                }
                            }
                        }
                        if (conc <= ltData[0].Data)
                        {
                            conc = ltData[0].Data + 0.001;
                        }
                        if (conc >= (ltData[ltData.Count - 1].Data + ltData[ltData.Count - 1].Data * 0.3))
                        {
                            dataGridView[2, i].Value = ">" + ltData[ltData.Count - 1].Data * 1.3;
                        }
                        else
                        {
                            dataGridView[2, i].Value = Convert.ToDouble(conc).ToString("0.000");
                        }
                    }
                }
            }
            if (rbtnConcToPmt.Checked)
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)//想要预测的数据
                {
                    double PMT;
                    if (dataGridView[0, i].Value == null && dataGridView[1, i].Value == null && dataGridView[2, i].Value != null)
                    {
                        double conc = double.Parse(dataGridView[2, i].Value.ToString());
                        if (conc >= CurveData[0].Data && conc <= CurveData[1].Data)
                        {
                            dataGridView[1, i].Value = GetLineResult(conc).ToString("0.000");
                            continue;
                        }
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
                            dataGridView[1, i].Value = Convert.ToDouble(PMT).ToString("0.000");
                        }
                        else
                        {
                            PMT = er.GetResult(conc);
                            dataGridView[1, i].Value = Convert.ToDouble(PMT).ToString("0.000");
                        }
                    }
                }
            }
            #endregion
        }
        /// <summary>
        /// 竞争法得到线性发光值到浓度计算结果
        /// </summary>
        /// <param name="PMT">发光值</param>
        /// <returns></returns>
        private double GetLineInverseResult(double PMT)
        {
            List<Data_Value> linearData = new List<Data_Value>();
            linearData.Add(CurveData[0].Data == 0.0001 ? new Data_Value() { Data = 0, DataValue = CurveData[0].DataValue } : CurveData[0]);
            linearData.Add(CurveData[1]);
            Linear linear = new Linear();
            linear.AddData(linearData);
            linear.Fit();
            return linear.GetResultInverse(PMT);
        }
        /// <summary>
        /// 竞争法得到线性浓度到发光值计算结果
        /// </summary>
        /// <param name="PMT"></param>
        /// <returns></returns>
        private double GetLineResult(double conc)
        {
            List<Data_Value> linearData = new List<Data_Value>();
            linearData.Add(CurveData[0].Data == 0.0001 ? new Data_Value() { Data = 0, DataValue = CurveData[0].DataValue } : CurveData[0]);
            linearData.Add(CurveData[1]);
            Linear linear = new Linear();
            linear.AddData(linearData);
            linear.Fit();
            return linear.GetResult(conc);
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
        private void GetParaAndShow2(Calculater er)
        {
            string[] strpar = er.StrPars.Split('|');
            string Furmula = string.Empty;
            if (strpar.Length > 0)
            {
                switch (cmbFit.SelectedIndex)
                {
                    case 0:
                        richTextBox3.Clear();
                        Furmula = "方程式： y = A + B*x";
                        richTextBox3.AppendText("直线回归：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[1] + "\r\n" + "B: " + strpar[0] + "\r\n" + "R^2: " + er.R2);
                        break;
                    case 1:
                        richTextBox3.Clear();
                        Furmula = "";
                        break;
                    case 2:
                        richTextBox3.Clear();
                        Furmula = "方程式： y = A + B*x + C*x^2";
                        richTextBox3.AppendText("二次多项式" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[2] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[0] + "\r\n" + "R^2: " + er.R2);
                        break;
                    case 3:
                        richTextBox3.Clear();
                        Furmula = "方程式： y = A + B*x + C*x^2 + D*x^3";
                        richTextBox3.AppendText("三次多项式：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[3] + "\r\n" + "B: " + strpar[2] + "\r\n" + "C: " + strpar[1] + "\r\n" + "D: " + strpar[0] + "\r\n" + "R^2: " + er.R2 + "\r\n" + Math.Pow(er.R2, 0.5));
                        break;
                    case 5:
                        richTextBox3.Clear();
                        Furmula = "方程式： y = (A - D) / [1 + (x/C)^B] + D";
                        //rtbPara.AppendText("四参数Logistic曲线拟合：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[0] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[2] + "\r\n" + "D: " + strpar[3] + "\r\n" + "R^2: " + er.R2+ "\r\n");
                        richTextBox3.AppendText("四参数Logistic曲线拟合：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[0] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[2] + "\r\n" + "D: " + strpar[3] + "\r\n" + "R: " + Math.Pow(er.R2, 0.5) + "\r\n" + "R^2 : " + er.R2 + "\r\n");
                        break;
                    #region
                    case 6:
                        richTextBox3.Clear();
                        Furmula = "方程式： y = (A - D) / [(1 + (x/C)^B)]^n + D";
                        richTextBox3.AppendText("五参数Logistic曲线拟合：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[0] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[2] + "\r\n" + "D: " + strpar[3] + "\r\n" + "n: " + strpar[4] + "\r\n" + "R^2: " + er.R2);
                        break;
                    #endregion
                    case 4:
                        richTextBox3.Clear();
                        //进行了描述补充 2017.12.16
                        Furmula = " p=1/(1+e^-(A+B*x)),p = OD/OD0,  q = 1 - p," + "\r\n" + "y = ln (p / q)" + "\r\n" + "OD:" + "反应值" + ",OD0 :" + "浓度为0时的反应值的均值" + "\r\n" + "方程式：y = A + B*log(x)";
                        richTextBox3.AppendText("Logit-log :" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[1] + "\r\n" + "B: " + strpar[0] + "\r\n" + "R^2: " + er.R);
                        break;
                    case 7:
                        richTextBox3.Clear();
                        Furmula = "方程式： y=A+B/(1+exp(-(C+D*ln(x))))";
                        richTextBox3.AppendText("四参数Logistic曲线拟合：" + "\r\n" + Furmula + "\r\n" + "A: " + strpar[0] + "\r\n" + "B: " + strpar[1] + "\r\n" + "C: " + strpar[2] + "\r\n" + "D: " + strpar[3] + "\r\n" + "R^2: " + er.R2);
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

        #region 辅助
        private void itemSave_Click_1(object sender, EventArgs e)
        {
        }

        private void btnClear_Click2(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add(500);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Clipboard.Clear();
        }

        private void dataGridView2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)//删除选中内容
        {
            if (e.Control && e.KeyCode == Keys.V) // 是否按下ctrl+v
            {
                Paste(dataGridView2, "", 0, false);
            }
            if (e.Control && e.KeyCode == Keys.D) // 是否按下ctrl+D
            {
                //if (dataGridView2.SelectedRows.Count < 1) 
                //{
                //    MessageBox.Show("没有选中数据，请选择！");
                //    return;
                //}
                for (int i = 0; i < 500; i++)//对每个单元格内容进行删除
                {
                    if (dataGridView2.Rows[i].Cells[0].Selected == true)
                    {
                        dataGridView2.Rows[i].Cells[0].Value = null;
                    }
                    if (dataGridView2.Rows[i].Cells[1].Selected == true)
                    {
                        dataGridView2.Rows[i].Cells[1].Value = null;
                    }
                    if (dataGridView2.Rows[i].Cells[2].Selected == true)
                    {
                        dataGridView2.Rows[i].Cells[2].Value = null;
                    }
                }
            }
        }

        private void Button1_Click2(object sender, EventArgs e)
        {
            for (int index = 0; index < dataGridView2.SelectedCells.Count; index++)
            {
                dataGridView2.SelectedCells[index].Value = null;
            }
        }

        private void GrbPara_Enter(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Paste(dataGridView2, "", 0, false);
        }
        #endregion

        private void btnCopy2_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dataGridView2.GetClipboardContent());
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            ListMainPoint2.Clear();
            ListMainPoint2 = GetMainCurve();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ListMainPoint0.Clear();
            for (int i = 0; i < dataGridView2.Rows.Count; i++)//DataGridView中的数据
            {
                if (dataGridView2[0, i].Value != null && dataGridView2[1, i].Value != null)
                {
                    Conc_PMT conc_PMT = new Conc_PMT();
                    conc_PMT.Conc = Convert.ToDouble(dataGridView2[0, i].Value);
                    conc_PMT.PMT = Convert.ToDouble(dataGridView2[1, i].Value);
                    ListMainPoint0.Add(conc_PMT);
                }
            }
        }
        /// <summary>
        /// 获取新的定标信息
        /// </summary>
        /// <param name="MainPoint">主曲线</param>
        /// <param name="newPoint">新的校准值</param>
        /// <param name="select">校准选择 0-A点不变 1-A点变 2-浓度取对数</param>
        List<Conc_PMT> GetNewPoint(List<Conc_PMT> MainPoint, List<Conc_PMT> newPoint, int select)
        {
            if (newPoint.Count < 2)
            {
                MessageBox.Show("校准点小于两个点");
                return null;
            }
            if (MainPoint.Count < 2)
            {
                MessageBox.Show("没有相关的主曲线");
                return null;
            }
            List<Conc_PMT> NewPoint = null;
            switch (select)
            {
                case 0:
                    NewPoint = GetAPointKeep(MainPoint, newPoint);
                    break;
                case 1:
                    NewPoint = GetAPointChange(MainPoint, newPoint);
                    break;
                case 2:
                    NewPoint = GetPointChange(MainPoint, newPoint);
                    break;
                case 3:
                    NewPoint = GetPointLgCon(MainPoint, newPoint);
                    break;
                default:
                    break;
            }
            return NewPoint;
        }
        /// <summary>
        /// 保持A点不变计算新的定标点
        /// </summary>
        /// <param name="MainPoint">主曲线定标信息</param>
        /// <param name="newPoint">校准定标信息</param>
        List<Conc_PMT> GetAPointKeep(List<Conc_PMT> MainPoint, List<Conc_PMT> newPoint)
        {
            double[] listK = new double[2];
            for(int i=0;i<newPoint.Count;i++)
            {
                Conc_PMT OPoint = MainPoint.Find(x => x.Conc== newPoint[i].Conc);
                double k = (newPoint[i].PMT - OPoint.PMT) / OPoint.PMT;
                listK[i]=k;
            }
            double k1 = 0;
            foreach (double k in listK)
            {
                k1 = k1 + k;
            }
            double k2 = k1 / listK.Length ;
            List<Conc_PMT> NewPoint1 = new List<Conc_PMT>();
            for (int i = 0; i < MainPoint.Count; i++)
            {
                if (i == 0)
                    NewPoint1.Add(MainPoint[i]);
                else
                {
                    Conc_PMT point = null;
                    foreach (Conc_PMT temp in newPoint)
                    {
                        if (temp.Conc== MainPoint[i].Conc)
                        {
                            point = temp;
                            continue;
                        }
                    }
                    if (point == null)
                    {
                        point = new Conc_PMT();
                        point.Conc  = MainPoint[i].Conc;
                        point.PMT = Math.Round(MainPoint[i].PMT * (k2 + 1), 0);
                    }
                    NewPoint1.Add(point);
                }
            }
            return NewPoint1;
        }
        /// <summary>
        /// A点变化计算新的定标点
        /// </summary>
        /// <param name="MainPoint">主曲线定标信息</param>
        /// <param name="newPoint">校准定标信息</param>
        List<Conc_PMT> GetAPointChange(List<Conc_PMT> MainPoint, List<Conc_PMT> newPoint)
        {
            double[] listK = new double[2];
            for (int i = 0; i < newPoint.Count; i++)
            {
                Conc_PMT OPoint = MainPoint.Find(x => x.Conc == newPoint[i].Conc);
                double k = (newPoint[i].PMT - OPoint.PMT) / OPoint.PMT;
                listK[i] = k;
            }
            double k1 = 0;
            foreach (double k in listK)
            {
                k1 = k1 + k;
            }
            double k2 = k1 / listK.Length;
            List<Conc_PMT> NewPoint1 = new List<Conc_PMT>();
            for (int i = 0; i < MainPoint.Count; i++)
            {
                    Conc_PMT point = null;
                    foreach (Conc_PMT temp in newPoint)
                    {
                        if (temp.Conc == MainPoint[i].Conc)
                        {
                            point = temp;
                            continue;
                        }
                    }
                    if (point == null)
                    {
                        point = new Conc_PMT();
                        point.Conc = MainPoint[i].Conc;
                        point.PMT = Math.Round(MainPoint[i].PMT * (k2 + 1), 0);
                    }
                    NewPoint1.Add(point);
            }
            return NewPoint1;
        }
        /// <summary>
        /// 获获取拟合曲线参数(夹心法)
        /// </summary>
        /// <param name="mainDatas"></param>
        double[] GetCanshu2(List<Conc_PMT> mainDatas)
        {
            double[] nongdu = new double[mainDatas.Count - 1];
            double[] faguagnzhi = new double[mainDatas.Count - 1];
            for (int index = 1; index < mainDatas.Count; index++)
            {
                nongdu[index - 1] = mainDatas[index].Conc;
                faguagnzhi[index - 1] = mainDatas[index].PMT;
            }
            double[] canshu = new double[5];
            canshu = canshuvalue(nongdu, faguagnzhi);
            return canshu;
        }
        /// <summary>
        /// 根据浓度计算发光值
        /// </summary>
        /// <param name="conc">需要预测的浓度</param>
        /// <param name="mainDatas">使用的计算曲线信息</param>
        /// <param name="canshu">参数信息</param>
        double GetPMT2(double conc, List<Conc_PMT> mainDatas, double[] canshu)
        {
            List<Data_Value> data_Values = new List<Data_Value>();
            for (int i = 0; i < mainDatas.Count; i++)//想要预测的数据
            {
                if (mainDatas != null)
                {
                    data_Values.Add(new Data_Value()
                    {
                        Data = mainDatas[i].Conc,
                        DataValue = mainDatas[i].PMT
                    }); ;
                }
            }
            double resultSandwich = 0;
            if (conc == 0)
            {
                conc = 0.001;
            }
            if ((data_Values[0].Data <= conc && conc <= data_Values[1].Data))
            {
                double resultLine = GetLineResultSandwich(conc, data_Values);
                resultSandwich = resultLine < data_Values[0].Data ? (data_Values[0].Data + 0.001) : resultLine;
            }
            else
            {
                double resultSandwich1 = GetResultSandwich(Convert.ToDouble(conc), canshu);
                resultSandwich = resultSandwich1 > data_Values[data_Values.Count - 1].DataValue * 1.2 ?
                data_Values[data_Values.Count - 1].DataValue * 1.2 : resultSandwich1;
            }
            return resultSandwich;
        }
        double GetPMT0(double conc, List<Conc_PMT> mainDatas, Calculater er)
        {
            double PMT = 0;
            if (conc >= mainDatas[0].Conc && conc <= mainDatas[1].Conc)
            {
                PMT = GetLineResult(conc);
            }
            if (conc == 0)
            {
                conc = 0.001;
            }
            if ((mainDatas[0].Conc <= conc && conc <= mainDatas[1].Conc) || (mainDatas[0].Conc >= conc && conc >= mainDatas[1].Conc))
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
                //dataGridView2[1, i].Value = Convert.ToDouble(PMT).ToString("0.000");
            }
            else
            {
                PMT = er.GetResult(conc);
                //dataGridView2[1, i].Value = Convert.ToDouble(PMT).ToString("0.000");
            }
            return PMT;
        }
        /// <summary>
        /// 根据发光值计算浓度
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="zedGraphControl"></param>
        /// <param name="mainDatas"></param>
        /// <param name="canshu"></param>
        void GetConnettion2(DataGridView dataGrid, ZedGraphControl zedGraphControl, List<Conc_PMT> mainDatas, double[] canshu)
        {
            double[] nongduall = new double[mainDatas.Count];
            double[] faguagnzhiall = new double[mainDatas.Count];
            for (int index = 0; index < mainDatas.Count; index++)
            {
                nongduall[index] = mainDatas[index].Conc;
                faguagnzhiall[index] = mainDatas[index].PMT;
            }
            List<double> preconcs = new List<double>();
            double[] result = new double[1];
            #region 预测
            List<double> predata = new List<double>();
            for (int i = 1; i < mainDatas.Count; i++)//想要预测的数据
            {
                double[] array = { Convert.ToDouble(dataGrid[1, i].Value) };
                result = resultvalue(array,
                    nongduall, faguagnzhiall);  //拟合浓度计算
                                                //dataGridView1[2, i].Value = result[0];
                predata.Add(result[0]);
            }
            for (int i = 0; i < dataGrid.Rows.Count; i++)//想要预测的数据
            {
                if (dataGrid[0, i].Value != null && dataGrid[1, i].Value != null)
                {
                    if (Convert.ToDouble(dataGrid[1, i].Value) > mainDatas[1].PMT)
                    {
                        double[] array = { Convert.ToDouble(dataGrid[1, i].Value) };
                        result = resultvalue(array,
                            nongduall, faguagnzhiall);
                        dataGrid[2, i].Value = result[0].ToString("0.000");
                        preconcs.Add(result[0]);
                    }
                    else
                    {
                        List<Data_Value> temp = new List<Data_Value>();
                        temp.Add(new Data_Value() { Data = mainDatas[0].Conc, DataValue = mainDatas[0].PMT });
                        temp.Add(new Data_Value() { Data = predata[0], DataValue = mainDatas[1].PMT });
                        double tempNum = CountLinearResult(temp, Convert.ToDouble(dataGridView4[1, i].Value));
                        dataGrid[2, i].Value = (tempNum <= mainDatas[0].Conc ?
                            mainDatas[0].Conc + 0.001 : tempNum).ToString("0.000");
                        preconcs.Add(tempNum);
                    }
                }
                if (dataGrid[0, i].Value == null && dataGrid[1, i].Value != null)
                {
                    if (Convert.ToDouble(dataGrid[1, i].Value) > mainDatas[1].PMT)
                    {
                        double[] array = { Convert.ToDouble(dataGrid[1, i].Value) };
                        result = resultvalue(array,
                            nongduall, faguagnzhiall);
                        double d = mainDatas[mainDatas.Count() - 1].Conc;
                        dataGrid[2, i].Value = (result[0] > mainDatas[mainDatas.Count() - 1].Conc * 1.2 || double.IsNaN(result[0])) ?
                            ">" + mainDatas[mainDatas.Count() - 1].Conc * 1.2 : result[0].ToString("0.000");
                    }
                    else
                    {
                        List<Data_Value> temp = new List<Data_Value>();
                        temp.Add(new Data_Value() { Data = mainDatas[0].Conc, DataValue = mainDatas[0].PMT });
                        temp.Add(new Data_Value() { Data = predata[0], DataValue = mainDatas[1].PMT });
                        double tempNum = CountLinearResult(temp, Convert.ToDouble(dataGrid[1, i].Value));
                        dataGrid[2, i].Value = (tempNum <= mainDatas[0].Conc ? mainDatas[0].Conc + 0.001 : tempNum).ToString("0.000");
                    }
                }
            }

            #region 画图
            DataTable datas = new DataTable();
            datas.Columns.Add("consistence", Type.GetType("System.String"));
            datas.Columns.Add("absorbency", Type.GetType("System.String"));
            datas.Columns.Add("geneA", Type.GetType("System.String"));
            datas.Columns.Add("geneB", Type.GetType("System.String"));
            datas.Columns.Add("geneC", Type.GetType("System.String"));
            datas.Columns.Add("geneD", Type.GetType("System.String"));
            datas.Columns.Add("geneE", Type.GetType("System.String"));
            for (int i = 0; i < mainDatas.Count; i++)
            {
                if (i == 0)
                {
                    datas.Rows.Add(preconcs[0], mainDatas[i].PMT, canshu[0].ToString(),
                    canshu[1].ToString(), canshu[2].ToString(), canshu[3].ToString(), 0.ToString());
                }
                else
                {
                    datas.Rows.Add(predata[i - 1], mainDatas[i].PMT, canshu[0].ToString(),
                    canshu[1].ToString(), canshu[2].ToString(), canshu[3].ToString(), 0.ToString());
                }
            }
            zedGraphControl.GraphPane.CurveList.Clear();
            CreateGraph(zedGraphControl, datas, mainDatas.Count);
            string path3 = "123.bmp";
            Image image = zedGraphControl.GetImage();
            image.Save(path3, System.Drawing.Imaging.ImageFormat.Bmp);
            #endregion
            #endregion
        }
        /// <summary>
        /// 所有的点都进行改变，浓度不求对数(夹心法)
        /// </summary>
        /// <param name="MainPoint">主曲线那个地信息</param>
        /// <param name="newPoint">新的测试浓度</param>
        /// <returns></returns>
        List<Conc_PMT> GetPointChange(List<Conc_PMT> MainPoint, List<Conc_PMT> newPoint)
        {
            double[] maincanshu = GetCanshu2(MainPoint);
            //将浓度带入主曲线计算C1，E1两点的发光值
            List<Conc_PMT> newPoint1 = new List<Conc_PMT>();
            foreach (Conc_PMT point in newPoint)
            {
                Conc_PMT conc_PMT = new Conc_PMT();
                conc_PMT.Conc = point.Conc;
                conc_PMT.PMT = GetPMT2(point.Conc, MainPoint, maincanshu);
                newPoint1.Add(conc_PMT);
            }
            //计算（C1-C）/C,（E1-E）/E
            List<Conc_PMT> newPoint2 = new List<Conc_PMT>();
            foreach (Conc_PMT point in newPoint)
            {
                Conc_PMT p1 = newPoint1.Find(x => x.Conc == point.Conc);
                Conc_PMT conc_PMT = new Conc_PMT();
                conc_PMT.Conc = point.Conc;
                conc_PMT.PMT = (p1.PMT - point.PMT)/ point.PMT;
                newPoint2.Add(conc_PMT);
            }
            //计算k值
            double k = (newPoint2[1].PMT - newPoint2[0].PMT) / (newPoint[1].Conc - newPoint[0].Conc);
            //计算b值
            double b = newPoint2[0].PMT - (newPoint[0].Conc * k);
            //将主曲线浓度带入主曲线中计算发光值
            List<Conc_PMT> CMainPoint = new List<Conc_PMT>();
            foreach (Conc_PMT point in MainPoint)
            {
                Conc_PMT conc_PMT = new Conc_PMT();
                conc_PMT.Conc = point.Conc;
                conc_PMT.PMT = GetPMT2(point.Conc, MainPoint, maincanshu);
                CMainPoint.Add(conc_PMT);
            }
            //计算新的定标曲线
            List<Conc_PMT> NewMainPoint = new List<Conc_PMT>();
            foreach (Conc_PMT point in CMainPoint)
            {
                Conc_PMT conc_PMT = new Conc_PMT();
                conc_PMT.Conc = point.Conc;
                if (conc_PMT.Conc == 0)
                    conc_PMT.Conc = 0.001;
                conc_PMT.PMT =Math.Round(point.PMT*(k* conc_PMT.Conc + b)+ point.PMT,0);
                NewMainPoint.Add(conc_PMT);
            }
            return NewMainPoint;
        }
        /// <summary>
        /// 所有的点都进行改变，浓度不求对数(竞争法)
        /// </summary>
        /// <param name="MainPoint"></param>
        /// <param name="newPoint"></param>
        /// <returns></returns>
        List<Conc_PMT> GetPointChange0(List<Conc_PMT> MainPoint, List<Conc_PMT> newPoint)
        {
            CalculateFactory ft = new CalculateFactory();
            Calculater er = null;
            er = ft.getCaler(cmbFit.SelectedIndex);//取到公式
            //double[] maincanshu = GetCanshu2(MainPoint);
            //将浓度带入主曲线计算C1，E1两点的发光值
            //List<Conc_PMT> newPoint1 = new List<Conc_PMT>();
            //foreach (Conc_PMT point in newPoint)
            //{
            //    Conc_PMT conc_PMT = new Conc_PMT();
            //    conc_PMT.Conc = point.Conc;
            //    conc_PMT.PMT = GetPMT2(point.Conc, MainPoint, maincanshu);
            //    newPoint1.Add(conc_PMT);
            //}
            ////计算（C1-C）/C,（E1-E）/E
            //List<Conc_PMT> newPoint2 = new List<Conc_PMT>();
            //foreach (Conc_PMT point in newPoint)
            //{
            //    Conc_PMT p1 = newPoint1.Find(x => x.Conc == point.Conc);
            //    Conc_PMT conc_PMT = new Conc_PMT();
            //    conc_PMT.Conc = point.Conc;
            //    conc_PMT.PMT = (p1.PMT - point.PMT) / point.PMT;
            //    newPoint2.Add(conc_PMT);
            //}
            ////计算k值
            //double k = (newPoint2[1].PMT - newPoint2[0].PMT) / (newPoint[1].Conc - newPoint[0].Conc);
            ////计算b值
            //double b = newPoint2[0].PMT - (newPoint[0].Conc * k);
            ////将主曲线浓度带入主曲线中计算发光值
            //List<Conc_PMT> CMainPoint = new List<Conc_PMT>();
            //foreach (Conc_PMT point in MainPoint)
            //{
            //    Conc_PMT conc_PMT = new Conc_PMT();
            //    conc_PMT.Conc = point.Conc;
            //    conc_PMT.PMT = GetPMT2(point.Conc, MainPoint, maincanshu);
            //    CMainPoint.Add(conc_PMT);
            //}
            ////计算新的定标曲线
            List<Conc_PMT> NewMainPoint = new List<Conc_PMT>();
            //foreach (Conc_PMT point in CMainPoint)
            //{
            //    Conc_PMT conc_PMT = new Conc_PMT();
            //    conc_PMT.Conc = point.Conc;
            //    conc_PMT.PMT = Math.Round(point.PMT * (k * point.PMT + b) + conc_PMT.PMT, 0);
            //    NewMainPoint.Add(conc_PMT);
            //}
            return NewMainPoint;
        }
        /// <summary>
        /// 所有的点都进行改变，浓度求对数(夹心法)
        /// </summary>
        /// <param name="MainPoint">主曲线摸那个地信息</param>
        /// <param name="newPoint">新的测试浓度</param>
        /// <returns></returns>
        List<Conc_PMT> GetPointLgCon(List<Conc_PMT> MainPoint, List<Conc_PMT> newPoint)
        {
            double[] maincanshu = GetCanshu2(MainPoint);
            //将浓度带入主曲线计算C1，E1两点的发光值
            List<Conc_PMT> newPoint1 = new List<Conc_PMT>();
            List<double> Lgconc = new List<double>();
            foreach (Conc_PMT point in newPoint)
            {
                Conc_PMT conc_PMT = new Conc_PMT();
                double d = Math.Log10(conc_PMT.Conc);
                Lgconc.Add(d);
                conc_PMT.Conc = d;
                conc_PMT.PMT = GetPMT2(d, MainPoint, maincanshu);
                newPoint1.Add(conc_PMT);
            }
            //计算（C1-C）/C,（E1-E）/E
            List<Conc_PMT> newPoint2 = new List<Conc_PMT>();
            foreach (Conc_PMT point in newPoint)
            {
                Conc_PMT p1 = newPoint1.Find(x => x.Conc == point.Conc);
                Conc_PMT conc_PMT = new Conc_PMT();
                conc_PMT.Conc = point.Conc;
                try
                {
                    conc_PMT.PMT = (p1.PMT - point.PMT) / point.PMT;
                }
                catch (Exception e)
                {
                    MessageBox.Show(point.Conc +"浓度求对数失败，结束本次计算");
                    return null;
                }
                newPoint2.Add(conc_PMT);
            }
            //计算k值
            double k = (newPoint2[1].PMT - newPoint2[0].PMT) / (newPoint[1].Conc - newPoint[0].Conc);
            //计算b值
            double b = newPoint2[0].PMT - (newPoint[0].Conc * k);
            //将主曲线浓度带入主曲线中计算发光值
            List<Conc_PMT> CMainPoint = new List<Conc_PMT>();
            foreach (Conc_PMT point in MainPoint)
            {
                Conc_PMT conc_PMT = new Conc_PMT();
                conc_PMT.Conc =Math.Log10(point.Conc);
                conc_PMT.PMT = GetPMT2(point.Conc, MainPoint, maincanshu);
                CMainPoint.Add(conc_PMT);
            }
            //计算新的定标曲线
            List<Conc_PMT> NewMainPoint = new List<Conc_PMT>();
            foreach (Conc_PMT point in CMainPoint)
            {
                Conc_PMT conc_PMT = new Conc_PMT();
                conc_PMT.Conc = point.Conc;
                if (conc_PMT.Conc == 0)
                    conc_PMT.Conc = 0.001;
                conc_PMT.PMT = Math.Round(point.PMT * (k * conc_PMT.Conc + b) + point.PMT, 0);
                NewMainPoint.Add(conc_PMT);
            }
            return NewMainPoint;
        }
        private void button13_Click(object sender, EventArgs e)
        {
            button9_Click(sender,e);
            List<Conc_PMT> ListwoPoint = new List<Conc_PMT>();
            foreach (DataGridViewRow dr in dataGridView3.Rows) 
            {
                if (dr.Cells[0].Value != null && dr.Cells[1].Value != null)
                {
                    Conc_PMT point = new Conc_PMT();
                    point.Conc = double.Parse(dr.Cells[0].Value.ToString());
                    point.PMT  = double.Parse(dr.Cells[1].Value.ToString());
                    ListwoPoint.Add(point);
                }
            }
            List<Conc_PMT> NewPoint = null;
            if (radioButton3.Checked)
            {
                NewPoint =  GetNewPoint(ListMainPoint2, ListwoPoint,0);
            }
            else if (radioButton4.Checked)
            {
                NewPoint = GetNewPoint(ListMainPoint2, ListwoPoint, 1);
            }
            else if (radioButton5.Checked)
            {
                NewPoint = GetNewPoint(ListMainPoint2, ListwoPoint, 2);
            }
            else if (radioButton6.Checked)
            {
                NewPoint = GetNewPoint(ListMainPoint2, ListwoPoint, 3);
            }
            if (NewPoint==null || NewPoint.Count < 6)
                return;
            
            for (int i=0;i< NewPoint.Count;i++)
            {
                dataGridView4.Rows[i].Cells[0].Value = NewPoint[i].Conc;
                dataGridView4.Rows[i].Cells[1].Value = NewPoint[i].PMT;
                dataGridView4.Rows[i].Cells[2].Value = NewPoint[i].Conc;
            }
            double[] maincanshu = GetCanshu2(NewPoint);
            richTextBox2.Text = "\n A= " + maincanshu[0] +
               "\n B= " + maincanshu[1] +
               "\n C= " + maincanshu[2] +
               "\n D= " + maincanshu[3] +
               " R^2 =" + maincanshu[4] * 0.999;
            #region  发光值到浓度
            if (rbtnPmtToConc2.Checked) //发光值到浓度
            {
                #region 预测
                GetConnettion2(dataGridView4, zedGraphControl2, NewPoint, maincanshu);
                #endregion
            }
            #endregion

            #region 浓度到发光值
            for (int i = 0; i < dataGridView4.Rows.Count; i++)//想要预测的数据
            {
                if (dataGridView4[0, i].Value == null && dataGridView4[1, i].Value == null && dataGridView4[2, i].Value != null)
                {
                    double conc = double.Parse(dataGridView4[i, 2].Value.ToString());
                    if (conc == 0)
                    {
                        conc = 0.001;
                    }
                    double resultLine = GetPMT2(conc, NewPoint, maincanshu);
                    if (NewPoint[0].Conc <= conc && conc < NewPoint[1].Conc)
                    {
                        dataGridView4[i, 1].Value = resultLine < NewPoint[0].Conc ? (NewPoint[0].Conc + 0.001) : resultLine;
                    }
                    else
                    {
                        dataGridView4[i, 1].Value = resultLine < NewPoint[0].PMT *1.2 ? ">" + NewPoint[0].PMT * 1.2 : NewPoint[0].PMT.ToString();
                    }
                }
            }
            #endregion
        }

        private void button15_Click(object sender, EventArgs e)
        {
            
            List<Conc_PMT> NewPoint = new List<Conc_PMT>();
            for(int i=0; i<dataGridView4.Rows.Count;i++)
            {
                Conc_PMT point = new Conc_PMT();
                if (dataGridView4.Rows[i].Cells[0].Value !=null && dataGridView4.Rows[i].Cells[1].Value != null)
                {
                    point.Conc = double.Parse(dataGridView4.Rows[i].Cells[0].Value.ToString());
                    point.PMT = double.Parse(dataGridView4.Rows[i].Cells[1].Value.ToString());
                    NewPoint.Add(point);
                }
            }
            if (NewPoint.Count < 6)
            {
                MessageBox.Show("定标点数量不足");
                return;
            }
            double[] maincanshu = GetCanshu2(NewPoint);
            richTextBox2.Text = "\n A= " + maincanshu[0] +
               "\n B= " + maincanshu[1] +
               "\n C= " + maincanshu[2] +
               "\n D= " + maincanshu[3] +
               " R^2 =" + maincanshu[4] * 0.999;
            #region  发光值到浓度
            if (rbtnPmtToConc2.Checked) //发光值到浓度
            {
                #region 预测
                GetConnettion2(dataGridView4, zedGraphControl2, NewPoint, maincanshu);
                #endregion
            }
            #endregion

            #region 浓度到发光值
            for (int i = 0; i < dataGridView4.Rows.Count; i++)//想要预测的数据
            {
                if (dataGridView4[0, i].Value == null && dataGridView4[1, i].Value == null && dataGridView4[2, i].Value != null)
                {
                    double conc = double.Parse(dataGridView4[i, 2].Value.ToString());
                    if (conc == 0)
                    {
                        conc = 0.001;
                    }
                    double resultLine = GetPMT2(conc, NewPoint, maincanshu);
                    if (NewPoint[0].Conc <= conc && conc < NewPoint[1].Conc)
                    {
                        dataGridView4[i, 1].Value = resultLine < NewPoint[0].Conc ? (NewPoint[0].Conc + 0.001) : resultLine;
                    }
                    else
                    {
                        dataGridView4[i, 1].Value = resultLine < NewPoint[0].PMT * 1.2 ? ">" + NewPoint[0].PMT * 1.2 : NewPoint[0].PMT.ToString();
                    }
                }
            }
            #endregion
        }

        private void button14_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < dataGridView3.SelectedCells.Count; index++)
            {
                dataGridView3.SelectedCells[index].Value = null;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView3.Rows.Count; i++)
            {
                for (int j = 0; j < this.dataGridView3.Columns.Count; j++)
                {
                    this.dataGridView3.Rows[i].Cells[j].Value = null;
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < dataGridView4.SelectedCells.Count; index++)
            {
                dataGridView4.SelectedCells[index].Value = null;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView4.Rows.Count; i++)
            {
                for (int j = 0; j < this.dataGridView4.Columns.Count; j++)
                {
                    this.dataGridView4.Rows[i].Cells[j].Value = null;
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dataGridView4.GetClipboardContent());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Paste(dataGridView4, "", 0, false);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            button19_Click(sender,e);
            List<Conc_PMT> ListwoPoint = new List<Conc_PMT>();
            foreach (DataGridViewRow dr in dataGridView6.Rows)
            {
                if (dr.Cells[0].Value != null && dr.Cells[1].Value != null)
                {
                    Conc_PMT point = new Conc_PMT();
                    point.Conc = double.Parse(dr.Cells[0].Value.ToString());
                    point.PMT = double.Parse(dr.Cells[1].Value.ToString());
                    ListwoPoint.Add(point);
                }
            }
            List<Conc_PMT> NewPoint = null;
            if (radioButton1.Checked)
            {
                NewPoint = GetNewPoint(ListMainPoint0, ListwoPoint, 0);
            }
            else if (radioButton2.Checked)
            {
                NewPoint = GetNewPoint(ListMainPoint0, ListwoPoint, 1);
            }
            else if (radioButton7.Checked)
            {
                NewPoint = GetNewPoint(ListMainPoint0, ListwoPoint, 2);
            }
            else if (radioButton8.Checked)
            {
                NewPoint = GetNewPoint(ListMainPoint0, ListwoPoint, 3);
            }
            if (NewPoint == null || NewPoint.Count < 6)
                return;
            for (int i = 0; i < NewPoint.Count; i++)
            {
                dataGridView5.Rows[i].Cells[0].Value = NewPoint[i].Conc;
                dataGridView5.Rows[i].Cells[1].Value = NewPoint[i].PMT;
                dataGridView5.Rows[i].Cells[2].Value = NewPoint[i].Conc;
            }
            button16_Click(sender, e);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            #region 计算代码
            count = 0;
            ltData = new List<Data_Value>();
            CurveData = new List<Data_Value>();
            CalculateFactory ft = new CalculateFactory();
            Calculater er = null;
            er = ft.getCaler(cmbFit.SelectedIndex);//取到公式
            for (int i = 0; i < dataGridView5.Rows.Count; i++)//DataGridView中的数据
            {
                if (dataGridView5[0, i].Value != null && dataGridView5[1, i].Value != null)
                    count++;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("consistence", typeof(float));
            dt.Columns.Add("absorbency", typeof(float));
            for (int j = 0; j < count; j++)
            {
                try
                {
                    ltData.Add(new Data_Value() { Data = double.Parse(dataGridView5[0, j].Value.ToString()), DataValue = double.Parse(dataGridView5[1, j].Value.ToString()) });
                    CurveData.Add(new Data_Value() { Data = double.Parse(dataGridView5[0, j].Value.ToString()), DataValue = double.Parse(dataGridView5[1, j].Value.ToString()) });
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
                    for (int i = 0; i < ltData.Count; i++)
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
            EquationCurvet2(dataGridView5,er, dt, panel1);
            //EquationCurvet(er, dt);
            #endregion 
        }

        private void button18_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < dataGridView6.SelectedCells.Count; index++)
            {
                dataGridView6.SelectedCells[index].Value = null;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            dataGridView6.Rows.Clear();
            dataGridView6.Rows.Add(500);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < dataGridView5.SelectedCells.Count; index++)
            {
                dataGridView5.SelectedCells[index].Value = null;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            dataGridView5.Rows.Clear();
            dataGridView5.Rows.Add(500);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dataGridView5.GetClipboardContent());
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Paste(dataGridView5, "", 0, false);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.dataGridView3.GetClipboardContent());
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Paste(dataGridView3, "", 0, false);
        }

        private void button27_Click(object sender, EventArgs e)
        {

            Clipboard.SetDataObject(this.dataGridView6.GetClipboardContent());
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Paste(dataGridView6, "", 0, false);
        }
    }
}

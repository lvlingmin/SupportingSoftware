using EBarv0._2.Administrator;
using EBarv0._2.Common;
using EBarv0._2.Model;
using EBarv0._2.user;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EBarv0._2.Admin
{
    public partial class frmInfo2 : Form
    {
        List<SpCode> spList = new List<SpCode>();
        List<string> diluteList = new List<string>();
        BLL.tbProject blltbPro = new BLL.tbProject();
        DataTable dtProAll = new DataTable();
        bool closedFlag = false;
        DataTable dtable = new DataTable();
        enum enumDiluteType1 { 稀释液1 = 0, 稀释液2 = 1, 稀释液3 = 2, 稀释液4 = 3, 稀释液5 = 4, 稀释液6 = 5, 稀释液7 = 6, 稀释液8 = 7, 稀释液9 = 8, 稀释液10 = 9}
        enum enumDiluteType2 { 稀释液一 = 0, 稀释液二 = 1, 稀释液三 = 2, 稀释液四 = 3, 稀释液五 = 4, 稀释液六 = 5, 稀释液七 = 6, 稀释液八 = 7, 稀释液九 = 8, 稀释液十 = 9 }
        public frmInfo2()
        {
            InitializeComponent();
        }

        private void frmInfo2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;//禁止进行拖拽
            dtProAll = blltbPro.GetList("").Tables[0];
        }

        private void frmInfo2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!closedFlag)
            {
                frmUser.f0.Close();
                System.Environment.Exit(0);
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            btnImportExcel.Enabled = false;
            btnCreate.Enabled = false;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            dialog.Filter = "xls文件|*.xls";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = dialog.FileName;
                DataTable dt = OperateExcel.SpImPortExcel(filePath);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("导入的数据为空！", "");
                    return;
                }
                dtable = dt;
                dgvInfo.DataSource = dtable;
                MessageBox.Show("导入完成！", "");
            }
            #region 检查项目名称是否匹配数据库项目名称
            for(int i = 0;i < dtable.Rows.Count;i++)
            {
                if(dtable.Rows[i][1].ToString().Contains("名称") && !dtable.Rows[i][1].ToString().Contains("稀释液"))
                {
                    string itemName = dtable.Rows[i][2].ToString();
                    if(itemName == "" || itemName == null)
                    {
                        MessageBox.Show("存在试剂短名称为空，请检查后重新导入。");
                        dtable = new DataTable();
                        dgvInfo.DataSource = dtable;
                        goto errorOrEnd;
                    }
                    //检查试剂名称是否输入正确
                    for(int j = 0;j < dtProAll.Rows.Count;j++)
                    {
                        if(dtProAll.Rows[j]["ShortName"].ToString() == itemName)
                        {
                            break;
                        }
                        if(j == dtProAll.Rows.Count - 1)
                        {
                            MessageBox.Show("录入Excel项目模板中，" + itemName +" 项目未在数据库中查到相关项目，请检查项目名称是否录入正确。或者导入相关项目信息。");
                            dtable = new DataTable();
                            dgvInfo.DataSource = dtable;
                            goto errorOrEnd;
                        }
                    }
                }
            }
            #endregion
            #region 检查是否符合标准模版
            string[] excelRgSortMode = new string[] { "试剂短名称", "批号","生产日期", "规格", "编号" , "数量", "主曲线", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "质控批号", "质控1", "质控2" };
            string[] excelDiluteSortMode = new string[] { "稀释液名称", "批号", "生产日期", "规格", "编号", "数量" };

            int firstIndex = 0;
            check:
            if(dtable.Rows[firstIndex][1].ToString().Contains("试剂"))
            {
                int temp = firstIndex;
                int addNum = 0;
                for (int i = firstIndex; i < temp + 17/*dtable.Rows.Count*/; i++)
                {
                    if(!dtable.Rows[i][1].ToString().Contains(excelRgSortMode[addNum++ % 17]))
                    {
                        MessageBox.Show("检查不符合试剂标准模版参数顺序，请检查后重试！" + "----" + dtable.Rows[temp][2].ToString() + "--" + excelRgSortMode[(addNum - 1) % 17] );
                        dtable = new DataTable();
                        dgvInfo.DataSource = dtable;
                        goto errorOrEnd;
                    }
                    if (dtable.Rows[i][1].ToString().Contains("A1") && (dtable.Rows[i][2].ToString().Trim() != "0" && dtable.Rows[i][2].ToString().Trim() != "0.1" && dtable.Rows[i][2].ToString().Trim() != "0.01"))
                    {
                        MessageBox.Show("检查存在定标点A1不为0的情况，请检查后重试！" + "----" + dtable.Rows[temp][2].ToString() + "--" + excelRgSortMode[(addNum - 1) % 17]);
                        dtable = new DataTable();
                        dgvInfo.DataSource = dtable;
                        goto errorOrEnd;
                    }
                    firstIndex++;
                }
                if(firstIndex < dtable.Rows.Count)
                    goto check;
            }
            else if (dtable.Rows[firstIndex][1].ToString().Contains("稀释液"))
            {
                int temp = firstIndex;
                int addNum = 0;
                for (int i = firstIndex; i < temp + 6; i++)
                {
                    if (!dtable.Rows[i][1].ToString().Contains(excelDiluteSortMode[addNum++ % 6]))
                    {
                        MessageBox.Show("检查不符合稀释液标准模版参数顺序，请检查后重试！" + "----" + dtable.Rows[temp][2].ToString() + "--" + excelDiluteSortMode[(addNum - 1) % 6]);
                        dtable = new DataTable();
                        dgvInfo.DataSource = dtable;
                        goto errorOrEnd;
                    }
                    firstIndex++;
                }
                if (firstIndex < dtable.Rows.Count)
                    goto check;
            }
            else
            {
                MessageBox.Show("检查不符合标准模版参数顺序，请检查后重试！试剂或者稀释液！");
                dtable = new DataTable();
                dgvInfo.DataSource = dtable;
                goto errorOrEnd;
            }
            #endregion
            errorOrEnd:            
            btnImportExcel.Enabled = true;
            btnCreate.Enabled = true;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            int dtLength = dtable.Rows.Count;
            if (dtLength < 1)
                return;

            btnImportExcel.Enabled = false;
            btnCreate.Enabled = false;
            for (int i = 0;i < dtLength;i++)
            {
                diluteList.Clear();
                spList.Clear();
                int jumpI = 0;
                //处理第一个生成条目
                string itemName = dtable.Rows[i][2].ToString();
                //判断是试剂还是稀释液
                if(itemName.Contains("稀释液"))
                {
                    try
                    {
                        jumpI = 5;
                        //处理稀释液参数
                        string batch = dtable.Rows[i + 1][2].ToString();
                        string productDate = dtable.Rows[i + 2][2].ToString();
                        int vol = int.Parse(dtable.Rows[i + 3][2].ToString());
                        int startNum = int.Parse(dtable.Rows[i + 4][2].ToString());
                        int saveNum = int.Parse(dtable.Rows[i + 5][2].ToString());
                        //循环生成条码
                        for (int add = 0; add < saveNum; add++)
                        {
                            string encrystr = getDiluteInfoOriginal(itemName , batch, productDate, vol, startNum, add); 
                            diluteList.Add(encrystr);
                        }
                        DataTable tempTable = new DataTable();
                        tempTable.Columns.Add("id", typeof(int));
                        tempTable.Columns.Add("data1", typeof(string));
                        tempTable.Columns.Add("data2", typeof(string));
                        tempTable.Columns.Add("data3", typeof(string));
                        tempTable.Columns.Add("data4", typeof(string));
                        tempTable.Columns.Add("data5", typeof(string));
                        tempTable.Columns.Add("data6", typeof(string));
                        tempTable.Columns.Add("data7", typeof(string));
                        tempTable.Columns.Add("data8", typeof(string));
                        tempTable.Columns.Add("data9", typeof(string));
                        tempTable.Columns.Add("data10", typeof(string));
                        tempTable.Columns.Add("data11", typeof(string));

                        int id = 1;
                        foreach (string sp in diluteList)
                        {
                            tempTable.Rows.Add(id++, sp, "", "", "", "", "", "", "", "", "", "");
                        }
                        if (tempTable.Rows.Count == 0)
                        {
                            MessageBox.Show("worry", "null");
                            return;
                        }
                        try
                        {
                            #region 数据导出        
                            SaveFileDialog dialog = new SaveFileDialog();
                            dialog.Filter = "xls文件|*.xls";
                            dialog.FileName = @"shepin_" + itemName + "_" + startNum + "-" + (startNum + saveNum - 1) + "_" + DateTime.Now.ToString("yyyyMMdd HHmmss") + ".xls";
                            if (dialog.ShowDialog() != DialogResult.OK)
                            {
                                return;
                            }
                            DataTableExcel.TableToExcel(tempTable, dialog.FileName);
                            MessageBox.Show("导出成功。", "导出提示");

                            #endregion
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show("导出失败。\n" + ex.Message, "导出提示");
                        }
                    }
                    catch(Exception ex)
                    {
                        BeginInvoke(new Action(()=>
                        {
                            MessageBox.Show("模版异常，请检查规范模版格式..");
                        }));
                        goto errorOrEnd;
                    }
                }
                else
                {
                    try
                    {
                        jumpI = 16;
                        //处理试剂参数
                        string batch = dtable.Rows[i + 1][2].ToString();
                        string productDate = dtable.Rows[i + 2][2].ToString();
                        int vol = int.Parse(dtable.Rows[i + 3][2].ToString());
                        int startNum = int.Parse(dtable.Rows[i + 4][2].ToString());
                        int saveNum = int.Parse(dtable.Rows[i + 5][2].ToString());
                        string calNum = dtable.Rows[i + 6][2].ToString();
                        string[] calMainCurveConc = new string[int.Parse(calNum)];
                        string[] calMainCurveValue = new string[int.Parse(calNum)];
                        for (int j = 1;j <= calMainCurveValue.Length; j++)
                        {
                            calMainCurveConc[j - 1] = dtable.Rows[i + 6 + j][2].ToString();
                            calMainCurveValue[j - 1] = ((int)double.Parse(dtable.Rows[i + 6 + j][3].ToString())).ToString();
                        }
                        string qcBatch = dtable.Rows[i + 14][2].ToString();
                        string[] qc1 = new string[2];
                        string[] qc2 = new string[2];
                        for(int j = 0;j < 2;j++)
                        {
                            qc1[j] = dtable.Rows[i + 15][j + 2].ToString();
                            qc2[j] = dtable.Rows[i + 16][j + 2].ToString();
                        }
                        //生成除了Rg条码其他条码（每一批相同）
                        string encryPro = "";
                        try
                        {
                            encryPro = getRgProOriginal(itemName);
                        }
                        catch(System.Exception ex)
                        {
                            MessageBox.Show("请导入最新项目信息文件，更新项目信息数据库！");
                            goto errorOrEnd;
                        }
                        string[] encryConc = getMainCurveConcOriginal(calMainCurveConc);
                        string[] encryValue = getMainCurveValueOriginal(calMainCurveValue);
                        string encryQC1 = getQcOriginal(itemName, qcBatch, "2", qc1);
                        string encryQC2 = getQcOriginal(itemName, qcBatch, "0", qc2);
                        string encryItemXml = getItemXmlOriginal(itemName);
                        //循环生成Rg条码，添加到spList（Rg条码唯一）
                        for (int temporary = 0; temporary < saveNum; temporary++)
                        {
                            string oristr = getRgInfoOriginal(itemName, batch, productDate, vol, startNum, temporary);
                            string encrystr = Utils.instance.ToEncryption(oristr); //加密后
                            SpCode sp = new SpCode(encrystr, encryPro, encryConc, encryValue, encryQC1, encryQC2, encryItemXml);
                            spList.Add(sp);
                        }
                        //循环生成对应Excel
                        DataTable tempTable = new DataTable();
                        tempTable.Columns.Add("id", typeof(int));
                        tempTable.Columns.Add("data1", typeof(string));
                        tempTable.Columns.Add("data2", typeof(string));
                        tempTable.Columns.Add("data3", typeof(string));
                        tempTable.Columns.Add("data4", typeof(string));
                        tempTable.Columns.Add("data5", typeof(string));
                        tempTable.Columns.Add("data6", typeof(string));
                        tempTable.Columns.Add("data7", typeof(string));
                        tempTable.Columns.Add("data8", typeof(string));
                        tempTable.Columns.Add("data9", typeof(string));
                        tempTable.Columns.Add("data10", typeof(string));
                        tempTable.Columns.Add("data11", typeof(string));

                        int id = 1;
                        foreach (SpCode sp in spList)
                        {
                            tempTable.Rows.Add(id++, sp.getReagentInfo(), sp.getProjectFlow(), sp.getScaling_1(), sp.getScaling_2(), sp.getValue_1(), sp.getValue_2(), sp.getValue_3(), sp.getValue_4(), sp.getQualityControls1(), sp.getQualityControls2(),sp.getItemXml());
                        }
                        if (dtable.Rows.Count == 0)
                        {
                            MessageBox.Show("worry", "null");
                            return;
                        }
                        try
                        {
                            #region 数据导出        
                            SaveFileDialog dialog = new SaveFileDialog();
                            dialog.Filter = "xls文件|*.xls";
                            dialog.FileName = @"shepin_" + itemName + "_" + startNum + "-" + (startNum + saveNum - 1) + "_" + DateTime.Now.ToString("yyyyMMdd HHmmss") + ".xls";
                            if (dialog.ShowDialog() != DialogResult.OK)
                            {
                                return;
                            }
                            //FolderBrowserDialog dialog = new FolderBrowserDialog();
                            //dialog.Description = "请选择保存文件夹";
                            //DialogResult flag = dialog.ShowDialog();
                            //if (flag != DialogResult.OK)
                            //    return;
                            //filePath = dialog.SelectedPath + @"\" + reagentName.SelectedItem.ToString() + "_" + num1.Value.ToString() + "-" + num2.Value.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd HHmmss") + ".xls";
                            DataTableExcel.TableToExcel(tempTable, dialog.FileName);
                            MessageBox.Show("导出成功。", "导出提示");
                            #endregion
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show("导出Excel失败,请重试!\n" + ex.Message, "导出提示");
                        }
                    }
                    catch (Exception ex)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            MessageBox.Show("模版异常，请检查规范模版格式..");
                        }));
                        goto errorOrEnd;
                    }
                }
                //i+=n跳到下一个项目头部
                i += jumpI;
            }
            errorOrEnd:
            btnImportExcel.Enabled = true;
            btnCreate.Enabled = true;
        }
        private string getRgInfoOriginal(string itemName, string batch,string productTime, int vol, int startNum, decimal? add = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1");//条码编号，表示条码1
            string btime = batch;
            string timeYear = btime.Substring(0, 4);
            string timeDay = btime.Substring(6, 2);
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
            DataRow[] dr = dtProAll.Select("ShortName = '" + itemName + "'");  //将下拉框中选中的项目信息导入行dr
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
            sb.Append(TimeToNewTime(btime));
            string num = Convert.ToString(startNum + add);
            #region 生产日期
            int productDateAdd = DateTime.Parse(productTime.Insert(4, "/").Insert(7, "/")).Subtract(DateTime.Parse(batch.Insert(4, "/").Insert(7, "/"))).Days;
            string productDate = "";
            if (productDateAdd >= 10)
            {
                productDate = ((char)((productDateAdd - 10) + 'A')).ToString();
                if (productDate.ToCharArray()[0] > 'Z')
                    productDate = ((char)(productDate.ToCharArray()[0] + 6)).ToString();
            }
            else
            {
                if (productDateAdd == 0)
                {
                    productDate = "z";
                }
                else
                    productDate = productDateAdd.ToString();
            }
            if (productDate.Length > 1)
            {
                MessageBox.Show("生产日期异常！");
                return "";
            }
            #endregion
            int tempNum = int.Parse(num);
            num = "";
            while (tempNum != 0)
            {
                int temp = tempNum % 62;
                string tempStr = "";
                if (temp >= 10)
                {
                    tempStr = ((char)((temp - 10) + 'A')).ToString();
                    if (tempStr.ToCharArray()[0] > 'Z')
                        tempStr = ((char)(tempStr.ToCharArray()[0] + 6)).ToString();
                }
                else
                {
                    tempStr = temp.ToString();
                }
                tempNum = tempNum / 62;
                num = tempStr + num;
            }
            while (num.Length < 3)              //如果num1控件内的数字位数不足4位，则前面填充0
            {
                num = num.Insert(0, "0");
            }

            string allTest = (vol / 10).ToString();//10倍乘表示测数   、、comboBox 100测/10
            while (allTest.ToString().Length < 2)      //测数小于两位前面填充0
            {
                allTest = allTest.Insert(0, "0");
            }
            sb.Append(allTest);         //将测数（两位）添加到sb后面
            sb.Append(productDate);
            sb.Append(num);             //将编号（四位）添加到sb后面
            string checkUsedNum = ((1 + int.Parse(dr[0]["ProjectNumber"].ToString()) + int.Parse(btime) + int.Parse(allTest) + productDateAdd + Convert.ToInt32(startNum + add)) % 97).ToString("X2");
            sb.Remove(1, 2);
            sb.Insert(1, checkUsedNum);
            return sb.ToString();
        }
        private string getDiluteInfoOriginal(string itemName, string batch, string productTime, int vol, int startNum, decimal? add = 0)
        {
            string strNo1 = "B"; //条码序号
            string strbdate = "";//批号日期
            string strDilute = vol.ToString("X2"); //稀释液默认容量 25ml十六进制19
            string strType = "";// (reagentName.SelectedIndex + 1).ToString("X2");

            var e = new enumDiluteType1();
            int enumLength = System.Enum.GetNames(e.GetType()).Length;
            for(int i = 0; i < enumLength; i++)
            {
                if(itemName.Contains(Enum.GetName(typeof(enumDiluteType1) , i)))
                {
                    strType = (i + 1).ToString("X2");
                    break;
                }
                if (itemName.Contains(Enum.GetName(typeof(enumDiluteType2), i)))
                {
                    strType = (i + 1).ToString("X2");
                    break;
                }
                if (i == enumLength - 1)
                {
                    MessageBox.Show("稀释液类型错误，请检查重新输入或者联系技术人员。");
                    return "";
                }
            }
            if(strType == "")
            {
                MessageBox.Show("稀释液类型错误，请检查重新输入或者联系技术人员。");
                return "";
            }

            string bTime = batch;
            strbdate = TimeToNewTime(bTime);

            //string num = Convert.ToInt32(startNum + add).ToString("X4");
            string num = Convert.ToString(startNum + add);
            #region 生产日期
            int productDateAdd = DateTime.Parse(productTime.Insert(4, "/").Insert(7, "/")).Subtract(DateTime.Parse(batch.Insert(4, "/").Insert(7, "/"))).Days;
            string productDate = "";
            if (productDateAdd >= 10)
            {
                productDate = ((char)((productDateAdd - 10) + 'A')).ToString();
                if (productDate.ToCharArray()[0] > 'Z')
                    productDate = ((char)(productDate.ToCharArray()[0] + 6)).ToString();
            }
            else
            {
                if (productDateAdd == 0)
                {
                    productDate = "z";
                }
                else
                    productDate = productDateAdd.ToString();
            }
            if (productDate.Length > 1)
            {
                MessageBox.Show("生产日期异常！");
                return "";
            }
            #endregion
            int tempNum = int.Parse(num);
            int usedCheckNum = tempNum;
            num = "";
            while (tempNum != 0)
            {
                int temp = tempNum % 62;
                string tempStr = "";
                if (temp >= 10)
                {
                    tempStr = ((char)((temp - 10) + 'A')).ToString();
                    if (tempStr.ToCharArray()[0] > 'Z')
                        tempStr = ((char)(tempStr.ToCharArray()[0] + 6)).ToString();
                }
                else
                {
                    tempStr = temp.ToString();
                }
                tempNum = tempNum / 62;
                num = tempStr + num;
            }
            while (num.Length < 3)              //如果num1控件内的数字位数不足4位，则前面填充0
            {
                num = num.Insert(0, "0");
            }

            string check = ((11 + vol + Convert.ToInt32(strType, 16) + int.Parse(bTime) + productDateAdd + usedCheckNum/*Convert.ToInt32(num, 16)*/) % 7).ToString();
            string originalStr = strNo1 + strType + strbdate + strDilute + productDate + num + check;
            string encrystr = Utils.instance.ToEncryption(originalStr); //加密后

            return encrystr;
        }
        private string getRgProOriginal(string itemName)
        {
            #region 流程
            string sql = "select ProjectNumber from tbProject where ShortName = '" + itemName + "'";
            string projectNumber = dtProAll.Select("ShortName='" + itemName + "'")[0]["ProjectNumber"].ToString();      //DbHelperOleDb.Query(0, sql).Tables[0].Rows[0][0].ToString();
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
                sql = "select ProjectProcedure from tbProject where ShortName = '" + itemName + "'";
                string[] Procedure = dtProAll.Select("ShortName='" + itemName + "'")[0]["ProjectProcedure"].ToString().Split(';'); //DbHelperOleDb.Query(0, sql).Tables[0];
                                                                                                                                   //Procedure = dtProcedure.Rows[0][0].ToString().Split(';');
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
                                return "";
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
            return encryPro;
        }
        private string[] getMainCurveConcOriginal(string[] conc)
        {
            #region 浓度
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            //条码序号
            sb1.Append("3");
            //生成定标数据和条码
            sb1.Append("A");
            sb1.Append(conc[0]);
            sb1.Append("f");

            sb1.Append("B");
            sb1.Append(conc[1]);
            sb1.Append("t");

            sb1.Append("C");
            sb1.Append(conc[2]);
            sb1.Append("f");

            sb1.Append("D");
            sb1.Append(conc[3]);
            sb1.Append("f");

            //条码序号
            sb2.Append("4");
            sb2.Append("E");
            sb2.Append(conc[4]);
            sb2.Append("t");

            sb2.Append("F");
            sb2.Append(conc[5]);
            sb2.Append("f");

            if (conc.Length > 6)
            {
                sb2.Append("G");
                sb2.Append(conc[6]);
                sb2.Append("f");
            }
            //浓度密文    
            string encry1 = Utils.instance.ToEncryption(sb1.ToString());
            string encry2 = Utils.instance.ToEncryption(sb2.ToString());
            #endregion
            string[] encryConc = new string[2] { encry1, encry2 };
            return encryConc;
        }
        private string[] getMainCurveValueOriginal(string[] value)
        {
            #region 发光值
            StringBuilder sbv1 = new StringBuilder();
            StringBuilder sbv2 = new StringBuilder();
            StringBuilder sbv3 = new StringBuilder();
            StringBuilder sbv4 = new StringBuilder();

            sbv1 = DecimalTo16("5", value[0], "B", value[1]);
            sbv2 = DecimalTo16("6", value[2], "D", value[3]);
            sbv3 = DecimalTo16("7", value[4], "F", value[5]);
            string value1 = Utils.instance.ToEncryption(sbv1.ToString());
            string value2 = Utils.instance.ToEncryption(sbv2.ToString());
            string value3 = Utils.instance.ToEncryption(sbv3.ToString());
            string value4 = "";

            if (value.Length > 6)
            {
                sbv4.Append(8);
                sbv4.Append(Convert.ToString(int.Parse(value[6]), 16));

                while (sbv4.Length < 8)
                {
                    sbv4.Insert(1, "0");
                }
                value4 = Utils.instance.ToEncryption(sbv4.ToString());
            }
            #endregion
            string[] encryValue = new string[4] { value1, value2, value3, value4 };
            return encryValue;
        }
        /// <summary>
        /// type:高中低-0,1,2
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="qcbatch"></param>
        /// <param name="type"></param>
        /// <param name="qc"></param>
        /// <returns></returns>
        private string getQcOriginal(string itemName, string qcbatch,string type, string[] qc)
        {
            string strNo1 = "9"; //条码序号
            string strItemNum = ""; //项目名称
            string batchDate = "";
            string strX = ""; //质控靶值
            string strSD = ""; //质控标准差
            string strLevel = ""; //质控类别
            string strRule = ""; //质控规则

            DataRow[] dr = dtProAll.Select("ShortName = '" + itemName + "'");  //将下拉框中选中的项目信息导入行dr
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

            string time2 = qcbatch;
            batchDate = TimeToNewTime(time2);

            string[] tempX = qc[0].Split('.');
            if (tempX.Length == 1)
                strX = int.Parse(tempX[0]).ToString("X3") + "00";
            else
            {
                //strX = int.Parse(tempX[0]).ToString("X3") + int.Parse(tempX[1]).ToString("X2");
                while (tempX[1].Length < 2)
                {
                    tempX[1] += "0";
                }
                //倒置
                string tempX1 = tempX[1];
                tempX1 = tempX1.Substring(1, 1) + tempX1.Substring(0, 1);
                tempX[1] = tempX1;
                //X3
                strX = int.Parse(tempX[0]).ToString("X3") + int.Parse(tempX[1]).ToString("X2");
            }
               

            string[] tempSD = qc[1].Split('.');
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

            strLevel = type;//高中低-0/1/2
            strRule = "11111";
            //strRule += cb1_2s.Checked ? "1" : "0";
            //strRule += cb1_3s.Checked ? "1" : "0";
            //strRule += cb2_2s.Checked ? "1" : "0";
            //strRule += cb4_1s.Checked ? "1" : "0";
            //strRule += cb10x.Checked ? "1" : "0";
            while (strRule.Length < 16)
                strRule += "0";
            strRule = Convert.ToInt32(strRule, 2).ToString("X4");
            string originalStr = strNo1 + strItemNum + batchDate + strX + strSD + strLevel + strRule;
            originalStr = Utils.instance.ToEncryption(originalStr.ToString());
            return originalStr;
        }
        private string getItemXmlOriginal(string itemName)
        {
            Model.tbProject modelItem = new tbProject();
            int proId = int.Parse(dtProAll.Select("ShortName = '" + itemName + "'")[0]["ProjectID"].ToString());
            string str = "F"; //f->G =>C
            if (proId > 0)
            {
                modelItem = blltbPro.GetModel(proId);
                str += modelItem.ProjectNumber + "$$";
                str += modelItem.ShortName + "$$";
                str += modelItem.FullName + "$$";
                str += modelItem.ProjectType + "$$";
                str += modelItem.DiluteCount + "$$";
                str += modelItem.VRangeType + "$$";
                str += modelItem.RangeType + "$$";
                str += modelItem.ValueRange1 + "$$";
                str += modelItem.ValueRange2 + "$$";
                str += modelItem.ValueUnit + "$$";
                str += modelItem.MinValue + "$$";
                str += modelItem.MaxValue + "$$";
                str += modelItem.CalPointNumber + "$$";
                str += modelItem.CalPointConc + "$$";
                str += modelItem.QCPointNumber + "$$";
                str += modelItem.QCPoints + "$$";
                str += modelItem.ProjectProcedure + "$$";
                str += modelItem.CalMode + "$$";
                str += modelItem.CalMethod + "$$";
                str += modelItem.CalculateMethod + "$$";
                str += modelItem.LoadType + "$$";
                str += modelItem.ActiveStatus + "$$";
                str += modelItem.DiluteName + "$$";
                str += modelItem.ExpiryDate + "$$";
                str += modelItem.NoUsePro + "$$";
                int checkNum = 0;
                foreach(char temp in str)
                {
                    checkNum += (int)temp;
                }
                checkNum = checkNum % 97;
                str += checkNum.ToString("x2");
                string excryItemXml = Utils.instance.ToItemXmlEncryption(str.ToString());
                return excryItemXml;
            }
            return "";
        }
        string TimeToNewTime(string time)  //将yyyyMMDD日期 转译为三位，大于10时用大写字母表示
        {
            //为了缩短长度，定的规则是，>=10的时候，用大写字母A-Z依次递增
            int timeYear = int.Parse(time.Substring(2, 2));
            string stringTimeYear = "";
            if (timeYear >= 10)
            {
                stringTimeYear = ((char)((timeYear - 10) + 'A')).ToString();  //年的后两位转译
                if (stringTimeYear.ToCharArray()[0] > 'Z')
                    stringTimeYear = ((char)(stringTimeYear.ToCharArray()[0] + 6)).ToString();
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
        private void back_Click(object sender, EventArgs e)
        {
            closedFlag = true;
            frmAdmin.f0.Show();
            this.Close();
        }
    }
    
}

using EBarv0._2.Administrator;
using EBarv0._2.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EBarv0._2
{
    public partial class FrmLoadItem : frmParent
    {
        BLL.tbProject bllPj = new BLL.tbProject();
        private string filePath = "";
        private Common.ProjectXml proInfoXml = new Common.ProjectXml();
        private Model.tbProject mProject = new Model.tbProject();
        private BLL.tbProject bllProject = new BLL.tbProject();
        private DataTable dtItemInfo;//项目信息存储表
        DataTable dtGetShortName = new DataTable();//项目缩写名表
        private DataTable dtStep = new DataTable();//项目步骤表
        private DataTable dtStdConc = new DataTable();//定标浓度表
        private int num = 0;
        bool closedFlag = false;

        public FrmLoadItem()
        {
            InitializeComponent();
            #region 项目信息
            dtStep.Columns.Add("NO");
            dtStep.Columns.Add("StepName");
            dtStep.Columns.Add("Para");
            dtStep.Columns.Add("Unit");

            dtStdConc.Columns.Add("NO");
            dtStdConc.Columns.Add("StdName");
            dtStdConc.Columns.Add("StdConc");
            #endregion
        }

        private void FrmLoadItem_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;//禁止进行拖拽
            #region 项目信息
            DbHelperOleDb db = new DbHelperOleDb(0);
            dtItemInfo = bllProject.GetAllList().Tables[0];
            dtGetShortName = GetItemShortName(dtItemInfo);
            dgvItemList.RowHeadersVisible = false;//去掉列表左侧的黑三角显示

            //与下面的+=组合使用，原因：当datagridview赋值时内部会触发选中事件，故此处先注销掉，再在后面注册上。
            dgvItemList.SelectionChanged -= dgvItemList_SelectionChanged;
            dgvItemList.DataSource = dtGetShortName;
            dgvItemList.Columns[0].Width = 40;
            dgvItemList.SelectionChanged += dgvItemList_SelectionChanged;
            #endregion
        }

        private void btnLoadItem_Click(object sender, EventArgs e)
        {
            DbHelperOleDb db = new DbHelperOleDb(0);
            #region
            //using (OpenFileDialog dialog = new OpenFileDialog())
            //{
            //    dialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            //    dialog.Filter = "xml文件|*.xml";
            //    if (dialog.ShowDialog() == DialogResult.OK)
            //    {
            //        filePath = dialog.FileName;
            //        proInfoXml = Common.ProjectXml.GetProjectInfo(filePath);
            //        //增加一个判断，是否已经存在当前要导入的项目 jun add 20190422
            //        string shortName = proInfoXml.ShortName;
            //        XmlToModel(proInfoXml, mProject);
            //        if (bllProject.Exists_(shortName))
            //        {
            //            DialogResult dr = MessageBox.Show("温馨提示,项目已经存在，导入将删除原有项目，是否继续？");
            //            if (dr != DialogResult.OK)
            //            {
            //                return;
            //            }
            //            if (!bllProject.Delete_(shortName))
            //            {
            //                return;
            //            }
            //        }
            //        if (bllProject.Add(mProject))
            //        {
            //            dtItemInfo = bllProject.GetAllList().Tables[0];
            //            dtGetShortName = GetItemShortName(dtItemInfo);

            //            //与下面的+=组合使用，原因：当datagridview赋值时内部会触发选中事件，故此处先注销掉，再在后面注册上。
            //            dgvItemList.SelectionChanged -= dgvItemList_SelectionChanged;
            //            dgvItemList.DataSource = dtGetShortName;
            //            dgvItemList.Columns[0].Width = 40;
            //            dgvItemList.SelectionChanged += dgvItemList_SelectionChanged;
            //            MessageBox.Show("导入成功！");
            //        }
            //        else
            //        {
            //            MessageBox.Show("导入格式不正确！");
            //        }
            //    }

            //}
            #endregion
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                dialog.Filter = "xml文件|*.xml";
                dialog.Multiselect = true;//等于true表示可以选择多个文件
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in dialog.FileNames)
                    {
                        filePath = dialog.FileName = file;
                        XElement document = XElement.Load(filePath);
                        //proInfoXml = Common.ProjectXml.GetProjectInfo(filePath);
                        //增加一个判断，是否已经存在当前要导入的项目 jun add 20190422
                        //string shortName = proInfoXml.ShortName;
                        try
                        {
                            document = XmlRemoveSpaces(document); //lyq add 20191029
                            mProject = XmlToModelNew(document);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("导入项目出错，请重新导入");
                            return;
                        }
                        string shortName = mProject.ShortName;
                        if (bllProject.Exists_(shortName))
                        {
                            DialogResult dr = MessageBox.Show("项目已经存在，导入将删除原有项目，是否继续","项目管理");
                            if (dr != DialogResult.OK)
                            {
                                return;
                            }
                            if (!bllProject.Delete_(shortName))
                            {
                                return;
                            }
                        }
                        if (bllProject.Add(mProject))
                        {
                            dtItemInfo = bllProject.GetAllList().Tables[0];
                            dtGetShortName = GetItemShortName(dtItemInfo);

                            //与下面的+=组合使用，原因：当datagridview赋值时内部会触发选中事件，故此处先注销掉，再在后面注册上。
                            //dgvItemList.SelectionChanged -= dgvItemList_SelectionChanged;
                            dgvItemList.DataSource = dtGetShortName;
                            dgvItemList.Columns[0].Width = 40;
                            //dgvItemList.SelectionChanged += dgvItemList_SelectionChanged;
                            MessageBox.Show("导入项目成功", "项目管理");
                        }
                        else
                        {
                            MessageBox.Show("导入格式不正确", "项目管理");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 把导入的项目信息文件消除空格
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private XElement XmlRemoveSpaces(XElement document)
        {
            //////
            document.Element("ProjectNumber").Value = document.Element("ProjectNumber").Value.Replace(" ", "");
            document.Element("ProjectProcedure").Value = document.Element("ProjectProcedure").Value.Replace(" ", "");
            document.Element("ProjectType").Value = document.Element("ProjectType").Value.Replace(" ", "");
            document.Element("QCPointNumber").Value = document.Element("QCPointNumber").Value.Replace(" ", "");
            document.Element("QCPoints").Value = document.Element("QCPoints").Value.Replace(" ", "");
            document.Element("RangeType").Value = document.Element("RangeType").Value.Replace(" ", "");
            document.Element("ShortName").Value = document.Element("ShortName").Value.Replace(" ", "");
            document.Element("ValueRange1").Value = document.Element("ValueRange1").Value.Replace(" ", "");
            document.Element("ValueRange2").Value = document.Element("ValueRange2").Value.Replace(" ", "");
            document.Element("ValueUnit").Value = document.Element("ValueUnit").Value.Replace(" ", "");
            document.Element("MinValue").Value = document.Element("MinValue").Value.Replace(" ", "");
            document.Element("MaxValue").Value = document.Element("MaxValue").Value.Replace(" ", "");
            document.Element("LoadType").Value = document.Element("LoadType").Value.Replace(" ", "");
            document.Element("FullName").Value = document.Element("FullName").Value.Replace(" ", "");
            document.Element("DiluteCount").Value = document.Element("DiluteCount").Value.Replace(" ", "");
            document.Element("CalPointNumber").Value = document.Element("CalPointNumber").Value.Replace(" ", "");
            document.Element("CalPointConc").Value = document.Element("CalPointConc").Value.Replace(" ", "");
            document.Element("CalMode").Value = document.Element("CalMode").Value.Replace(" ", "");
            document.Element("CalMethod").Value = document.Element("CalMethod").Value.Replace(" ", "");
            document.Element("CalculateMethod").Value = document.Element("CalculateMethod").Value.Replace(" ", "");
            document.Element("ActiveStatus").Value = document.Element("ActiveStatus").Value.Replace(" ", "");
            document.Element("DiluteName").Value = document.Element("DiluteName").Value.Replace(" ", "");
            document.Element("ExpiryDate").Value = document.Element("ExpiryDate").Value.Replace(" ", "");
            document.Element("NoUsePro").Value = document.Element("NoUsePro").Value.Replace(" ", "");
            document.Element("VRangeType").Value = document.Element("VRangeType").Value.Replace(" ", "");

            return document;
        }

        private Model.tbProject XmlToModelNew(XElement document)
        {
            Model.tbProject mp = new Model.tbProject();
            mp.ProjectNumber = document.Element("ProjectNumber").Value.ToString();
            mp.ProjectProcedure = document.Element("ProjectProcedure").Value.ToString();
            mp.ProjectType = document.Element("ProjectType").Value.ToString();
            mp.QCPointNumber = Convert.ToInt32(document.Element("QCPointNumber").Value.ToString());
            mp.QCPoints = document.Element("QCPoints").Value.ToString();
            mp.RangeType = document.Element("RangeType").Value.ToString();
            mp.ShortName = document.Element("ShortName").Value.ToString();
            mp.ValueRange1 = document.Element("ValueRange1").Value.ToString();
            mp.ValueRange2 = document.Element("ValueRange2").Value.ToString();
            mp.ValueUnit = document.Element("ValueUnit").Value.ToString();
            mp.MinValue = Convert.ToDouble(document.Element("MinValue").Value.ToString());
            mp.MaxValue = Convert.ToDouble(document.Element("MaxValue").Value.ToString());
            mp.LoadType = Convert.ToInt32(document.Element("LoadType").Value.ToString());
            mp.FullName = document.Element("FullName").Value.ToString();
            mp.DiluteCount = Convert.ToInt32(document.Element("DiluteCount").Value.ToString());
            mp.CalPointNumber = Convert.ToInt32(document.Element("CalPointNumber").Value.ToString());
            mp.CalPointConc = document.Element("CalPointConc").Value.ToString();
            mp.CalMode = Convert.ToInt32(document.Element("CalMode").Value.ToString());
            mp.CalMethod = Convert.ToInt32(document.Element("CalMethod").Value.ToString());
            mp.CalculateMethod = document.Element("CalculateMethod").Value.ToString();
            mp.ActiveStatus = Convert.ToInt32(document.Element("ActiveStatus").Value.ToString());
            mp.DiluteName = document.Element("DiluteName").Value.ToString(); ;
            mp.ExpiryDate = Convert.ToInt32(document.Element("ExpiryDate").Value.ToString());
            mp.NoUsePro = document.Element("NoUsePro").Value.ToString(); ;
            mp.VRangeType = document.Element("VRangeType").Value.ToString();
            return mp;
        }
        private void XmlToModel(Common.ProjectXml xp, Model.tbProject mp)
        {
            mp.ProjectNumber = xp.ProjectNumber;
            mp.ProjectProcedure = xp.ProjectProcedure;
            mp.ProjectType = xp.ProjectType;
            mp.QCPointNumber = xp.QCPointNumber;
            mp.QCPoints = xp.QCPoints;
            mp.RangeType = xp.RangeType;
            mp.ShortName = xp.ShortName;
            mp.ValueRange1 = xp.ValueRange1;
            mp.ValueRange2 = xp.ValueRange2;
            mp.ValueUnit = xp.ValueUnit;
            mp.MinValue = xp.MinValue;
            mp.MaxValue = xp.MaxValue;
            mp.LoadType = xp.LoadType;
            mp.FullName = xp.FullName;
            mp.DiluteCount = xp.DiluteCount;
            mp.CalPointNumber = xp.CalPointNumber;
            mp.CalPointConc = xp.CalPointConc;
            mp.CalMode = xp.CalMode;
            mp.CalMethod = xp.CalMethod;
            mp.CalculateMethod = xp.CalculateMethod;
            mp.ActiveStatus = xp.ActiveStatus;
            // 稀释液名称，LYN add 20171114
            mp.DiluteName = xp.DiluteName;
            mp.ExpiryDate = xp.ExpiryDate;//2018-08-07 zlx add
            mp.NoUsePro = xp.NoUsePro;//2018-10-13 zlx add
            mp.VRangeType = xp.VRangeType;
        }

        /// <summary>
        /// 获取项目列表名称
        /// </summary>
        /// <param name="dtAllList">项目全列表</param>
        /// <returns></returns>
        public DataTable GetItemShortName(DataTable dtAllList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("No", typeof(int));
            dt.Columns.Add("ItemShortName", typeof(string));
            for (int i = 0; i < dtAllList.Rows.Count; i++)
            {
                dt.Rows.Add(dtAllList.Rows[i]["ProjectID"], dtAllList.Rows[i]["ShortName"]);
            }
            return dt;

        }

        private void dgvItemList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItemList.SelectedRows.Count > 0)
            {
                ShowItemAllValue(int.Parse(dgvItemList.SelectedRows[0].Cells[0].Value.ToString()));
            }
        }


        /// <summary>
        /// 将选中项目的所有信息显示在界面上
        /// </summary>
        /// <param name="selectedID">被选中项目的ID</param>
        private void ShowItemAllValue(int selectedID)
        {
            if (dtItemInfo.Rows.Count < 1) return;
            DataRow[] dr = dtItemInfo.Select("ProjectID=" + selectedID);
            if (dr.Length > 0)
            {
                dtStep.Rows.Clear();
                string[] allStep = (dr[0]["ProjectProcedure"].ToString()).Split(';');
                for (int i = 0; i < allStep.Length; i++)
                {
                    if (allStep[i] == "")
                        continue;
                    string[] singStep = allStep[i].Split('-');
                    dtStep.Rows.Add((i + 1).ToString(), GetStepName(singStep[0]), singStep[1], singStep[2]);
                }
                dgvItemStep.DataSource = dtStep;
                num = 0;
                Array.Clear(allStep, 0, allStep.Length);

                dtStdConc.Rows.Clear();
                string[] allConc = (dr[0]["CalPointConc"].ToString()).Split(',');
                for (int j = 0; j < allConc.Length; j++)
                {
                    dtStdConc.Rows.Add(j + 1, "S" + (j + 1), allConc[j]);
                }
                dgvItemStd.DataSource = dtStdConc;
                Array.Clear(allConc, 0, allConc.Length);

            }

        }
        /// <summary>
        /// 获取各个步骤缩写对应的实际步骤名称
        /// </summary>
        /// <param name="flagName">步骤缩写</param>
        /// <returns></returns>
        private string GetStepName(string flagName)
        {
            //S-30-ml;R1-30-ml;R2-30-ml;H-15-min;B-30-ml;H-5-min;W-300-ml;T-20-ml;D-10-s
            if (flagName == "S")
            {
                return "加样";
            }
            if (flagName == "R1")
            {
                return "试剂1";
            }
            if (flagName == "R2")
            {
                return "试剂2";
            }
            if (flagName == "R3")
            {
                return "试剂3";
            }
            if (flagName == "H")
            {
                return "孵育";
            }
            if (flagName == "B")
            {
                return "加磁珠";
            }
            if (flagName.Contains("W"))
            {
                return "清洗";
            }
            if (flagName == "T")
            {
                return "加底物";
            }
            if (flagName == "D")
            {
                return "读数";
            }
            return "自定义";

        }

        private void dgvItemStep_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //2018-07-30 
            //dgvItemStep.Rows[e.RowIndex].Cells[2].ReadOnly = false;//将当前单元格设为可读
            dgvItemStep.CurrentCell = dgvItemStep.Rows[e.RowIndex].Cells[2];//获取当前单元格
            dgvItemStep.BeginEdit(true);//将单元格设为编辑状态
        }

        private void Back_Click(object sender, EventArgs e)
        {
            closedFlag = true;
            frmAdmin.f0.Show();
            this.Close();
        }

        private void FrmLoadItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!closedFlag)
            {
                frmAdmin.f0.Close();
                System.Environment.Exit(0);
            }
        }

        private void btnUnLoadItem_Click(object sender, EventArgs e)
        {
            if (dgvItemList.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选中项目后进行删除！");
                return;
            }
            if (bllProject.Delete_(dgvItemList.SelectedRows[0].Cells[1].Value.ToString())) MessageBox.Show("项目删除成功！");
            dgvItemList.DataSource = GetItemShortName(bllProject.GetAllList().Tables[0]);
            dgvItemList.Columns[0].Width = 40;
        }
    }
}

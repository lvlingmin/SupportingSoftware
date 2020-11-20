using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Numerics;
using ZXing;
using ZXing.Common;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows;
using EBarv0._2.DBUtility;

namespace EBarv0._2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        DataTable dtProject = new DataTable();//存放原始字符串和密文
        DataTable dt = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//禁止最大化
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;//禁止进行拖拽
            cbma.Text = "Code128";//码制按钮默认显示项
            folderBrowserDialog1.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;//路径选择对话框初始路径
            address.Text = folderBrowserDialog1.SelectedPath;//路径textbox默认显示
            openFileDialog1.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//打开文件对话框初始路径

            //设置标题
            dtProject.Columns.Add("原始字符串",typeof(string));
            dtProject.Columns.Add("密文",typeof(string));
            this.dataGridView1.DataSource = dtProject;

            //设置reagenName显示
            DbHelperOleDb db = new DbHelperOleDb(0);   //连接数据库ProjectInfo.mdb
            string sql = "select ProjectNumber ,ShortName,ProjectProcedure from tbProject"; //查询语句
            dt = DbHelperOleDb.Query(0,sql).Tables[0];  //获取需要的数据库表赋值给dt
            this.reagentName.SelectedIndexChanged -= new System.EventHandler(this.reagentName_SelectedIndexChanged);  //解除事件订阅（空函数（当选中项改变））      
            this.reagentName.DataSource = dt;   //下拉框comboBox设置数据源为tbProject表
            this.reagentName.DisplayMember = "ShortName";  //comboBox设置显示列，列名为项目简称
            this.reagentName.ValueMember = "ProjectNumber"; //把表中的项目编号属性，设置为comboBox中选项的实际值
            this.reagentName.SelectedIndexChanged += new System.EventHandler(this.reagentName_SelectedIndexChanged); //订阅事件（空函数（当选中项改变））

            //设置显示
            this.dataGridView1.DataSource = dtProject;  //重复38行写了
            this.dataGridView1.Columns[0].Width = 180;  //设定列宽
            this.dataGridView1.Columns[1].Width = 180;  //设定列宽
            this.dataGridView1.RowHeadersVisible = false;   //不显示包含标题的列

            //lyq 20190810
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//checkedbox批量生成功能，控制部分控件可见性
        {
            if (checkBox1.Checked == false)  //控件CheckBox“批量生成”，没被选中
            {
                num2.Value = num1.Value;  //NumericUpDown控件被设定为和第一个NumericUpDown控件值一样
                label5.Visible = label6.Visible = false;  //设置两个标签不可见
                num2.Visible = false;       //NumericUpDown控件不可见
                label5.Enabled = label6.Enabled = false;  //设置两个标签不可用
                num2.Enabled = false;       //NumericUpDown不可用
            }
            else                            //控件CheckBox“批量生成”被选中
            {
                label5.Visible = label6.Visible = true;     //两个标签设定为可见
                num2.Visible = true;        //NumericUpDown控件可见
                label5.Enabled = label6.Enabled = true;     //两个标签设定为可用
                num2.Enabled = true;        //NumericUpDown控件可用
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)//checkedbox提示功能开启与关闭
        {
            if (checkBox2.Checked == true)      //CheckBox控件“开启提示”，被选中
            {
                toolTip1.Active = toolTip2.Active = true;   //控件ToolTip是被激活的，当指针指着一个控件时可以显示一个小弹窗，给出该控件的功能
            }
            else                                //CheckBox控件“开启提示”，没被选中
            {
                toolTip1.Active = toolTip2.Active = false;//控件ToolTip是没有被激活的，休眠的
            }
        }

        private void EventChangeNum2Min(object sender, EventArgs e)//num1值改变事件，让编号num2的最小值为num1的值
        {
            num2.Minimum=num1.Value;  //限定从A到B，B的最小值为A的值
            if (checkBox1.Checked == false) num2.Value = num1.Value;  // 如果控件CheckBox没被选中，则把A到B，A和B的值设为一个值，既是A值
        }

        private void ToEmptyingAllListandTable()//清空Nlist和Blist，以及他们对应的DataGridView和Panel
        {
            //this.dataGridView1.DataSource = null;
            //Nlist.Clear();
            panel1.Controls.Clear();  //panel1清除所有控件   ？？panel1 和 panel3重叠？
            //Blist.Clear();
        }

        /// <summary>
        /// 得到原始编号的方法。后期需要更改。
        /// </summary>
        /// <param name="add">编号=num1.text+add。这个add差值由调用它的方法计算赋予，以此实现批量生成功能</param>
        /// <returns>返回原始编号</returns>
        //private string ToGetOriginalString(decimal? add = 0)//得到原始编号的方法!!!!!!!!!!!!!后期确定编号的构成后，需要更改字符串获取方式
        private string ToGetOriginalString(decimal? add = 0)    
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1");//条码编号，表示条码1
            string time = string.Format("{0:yyyyMMdd}", prodectTime.Value);//8位时间
            string timeYear = time.Substring(0, 4);
            string timeDay = time.Substring(6, 2);
            //将四位数年份和四位数月日异或，取较低两位作为校验位

            string checkNum = (int.Parse(timeYear.Substring(2,2)) ^ int.Parse(timeDay)).ToString(); // 这是把年的后两位和日的两位异或
            if (checkNum.Length < 2)
            {
                while (checkNum.Length < 2)
                {
                    checkNum = checkNum.Insert(0, "0");
                }
            }            
            sb.Append(checkNum);        //添加两位的校验码 到sb
            //查询表中对应的项目编号
            DataRow[] dr =  dt.Select("ShortName = '"+reagentName.Text.Trim()+"'");  //将下拉框中选中的项目信息导入行dr
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
            string allTest = (int.Parse(reagentStandard.Text)/10).ToString();//10倍乘表示测数   、、comboBox 100测/10 
            //MessageBox.Show(allTest);
            while(allTest.ToString().Length<2)      //测数小于两位前面填充0
            {
                allTest = allTest.Insert(0,"0");
            }
            sb.Append(allTest);         //将测数（两位）添加到sb后面
            sb.Append(num);             //将编号（四位）添加到sb后面
            //time = 
            //string name = reagentName.Text;//试剂名称，接下来获取对应的代号，两位
            //string standard = reagentStandard.Text;//该试剂的规格，接下来获取此代并赋值号，一位
            //string batch = Convert.ToString( batchNum.Value);//批号，两位，通过maximun属性值控制
            //string num = Convert.ToString(num1.Value + add);//试剂编号，两位，通过maximun属性值控制
            //while (batch.Length < 2)
            //{
            //    batch = batch.Insert(0, "0");
            //}
            //while (num.Length < 2)
            //{
            //    num = num.Insert(0, "0");
            //}
            
            //if (name == "试剂A") name = "01";//获取试剂名称对应的编号，后期改为查询数据库
            //else if (name == "试剂B") name = "02";
            //else
            //{
            //    MessageBox.Show("错误", "品名代码转换错误");
            //    return null;
            //}

            //if (standard == "500测") standard = "1";//获取规格代号，后期改为查询数据库
            //else if (standard == "600测") standard = "2";
            //else
            //{
            //    MessageBox.Show("错误", "规格代码转换错误");
            //    return null;
            //}

            //if (batch=="0")
            //{
            //    MessageBox.Show("错误", "未选择批号");
            //    return null;
            //}
            
            //string str = time + name + standard + batch + num;
            return sb.ToString();
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

        private void button1_Click(object sender, EventArgs e)//生成编码按钮点击事件
        {
            Utils.instance.clearShowData(panel1, dtProject);  //清空原始字符串的dataGridView 和 显示条码的Panel1
            if (reagentName.Text != "" && reagentStandard.Text != "" && batchNum.Value != 0 && num1.Value != 0)//非空判断
            {
                //ToEmptyingAllListandTable();
                decimal DValue = num2.Value - num1.Value;

                for (int temporary = 0; temporary <= DValue; temporary++)
                {
                    string oristr = ToGetOriginalString(temporary);//后面改到工具类中
                    string encrystr = Utils.instance.ToEncryption(oristr);
                    dtProject.Rows.Add(oristr, encrystr);
                }
            }
            else MessageBox.Show("信息不能为空", "错误");
            Utils.instance.ToMadePictureFromdt(panel1, dtProject);  //在panel1打印 完整条码（一维码）信息
            //MessageBox.Show("1:"+Utils.instance.ToDecryption(dtProject.Rows[0][1].ToString()) );
        }

        /// <summary>
        /// 根据文本内容，生成相应的一维码
        /// </summary>
        /// <param name="num">文本</param>
        /// <returns>一维码图片</returns>
        private Bitmap ToWriteBitPicture(string num)//根据文本内容，生成相应的一维码  、、没被使用
        {
            EncodingOptions opt = new EncodingOptions();//一维码选项-尺寸
            opt.Height = 60;
            opt.Width = 300;

            ZXing.BarcodeFormat format = new BarcodeFormat();//一维码格式
            format = cbma.Text == "Code128" ? BarcodeFormat.CODE_128 : BarcodeFormat.ITF;//判断选用的一维码格式 、、code128或者交叉25（ITF）

            ZXing.BarcodeWriter writer = new BarcodeWriter();//一维码绘制对象
            writer.Options = opt;       //绘制一维码 加载尺寸
            writer.Format = format;     //条码 加载格式
            return writer.Write(num);   //
        }

        /// <summary>
        /// 识别一维码
        /// </summary>
        /// <param name="bit">一维码图片</param>
        /// <returns>解读出的密文</returns>
        private string ToReadBitPicture(Bitmap bit)
        {
            DecodingOptions opt = new DecodingOptions();
            opt.PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.CODE_128, BarcodeFormat.ITF };

            ZXing.BarcodeReader reader = new BarcodeReader();
            reader.Options = opt;
            ZXing.Result result = reader.Decode(bit);
            return result.ToString();

        }
        

        private void createMa_Click(object sender, EventArgs e)//一维码生成按钮点击事件
        {
            //if (Nlist == null) return;

            //ToMadePictureFromSlist();
        }

        private void browser_Click(object sender, EventArgs e)//浏览按钮点击事件
        {
            //if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    address.Text = folderBrowserDialog1.SelectedPath;
            //}
        }

        /// <summary>
        /// 保存条码到文件 按钮事件 lyq add 20190809
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, EventArgs e)//保存按钮事件
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择保存文件夹";
            if (dialog.ShowDialog() == DialogResult.OK || dialog.ShowDialog() == DialogResult.Yes)
            {
                string fPath = dialog.SelectedPath;
                //保存一维码
                Utils.instance.saveImage(dtProject, fPath , 1); //lyq mod 20190821
            }
            
            
            #region 注释代码
            /*
            if (Blist.Count < 1)
            {
                MessageBox.Show("温馨提示", "请生成条码后再进行保存");
                return;
            }
            if (!Directory.Exists("项目基本信息"))
            {
                Directory.CreateDirectory("项目基本信息");
            }
            string address = Environment.CurrentDirectory + @"\项目基本信息\";
            foreach (var a in Blist)
            {
                string filePath = address + "//" + "项目基本信息" + "#" + a.OriNum + ".jpg";
                a.Bit.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            MessageBox.Show("保存成功", "成功");
            string address = this.address.Text;
            foreach (var a in Blist)
            {
                string filePath = address + "//" + a.EncodeStandard + "#" + a.OriNum + ".jpg";
                a.Bit.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            MessageBox.Show("保存成功","成功");*/
            #endregion
        }

        private void encryption_KeyPress(object sender, KeyPressEventArgs e)//address的textbox控件的回车事件
        {
            //if (e.KeyChar == (char)13)
            //{
            //    if (encryption.Text.Length == 16)
            //    {
            //        //if (Regex.IsMatch(encryption.Text, @"^/d*[.]?/d*$"))
            //        //{
            //            if (ToRestoreOriginalNum(ToDecryption(encryption.Text)) != "")
            //            {
            //                Blist.Add(new BitPicture(ToRestoreOriginalNum(ToDecryption(encryption.Text)), cbma.Text, ToWriteBitPicture(encryption.Text)));//此处不完整,需要解码功能！！！！！！！！！！！！！！！！！！！！！！！！！！
            //                ToMadePictureFromBlist();

            //                if (Nlist.Count < Blist.Count)
            //                {
            //                    Nlist.Add(new Number(ToRestoreOriginalNum(ToDecryption(encryption.Text)), ToDecryption(encryption.Text), encryption.Text));

            //                    DGV1.DataSource = null;
            //                    DGV1.DataSource = Nlist;
            //                }
            //            }
            //        //}
            //    }
            //    else
            //    {
            //        MessageBox.Show("输入格式有误", "错误");
            //    }
            //}
        }
        /// <summary>
        /// 打开按钮 lyq add 20190809
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void open_Click(object sender, EventArgs e)//打开按钮事件
        {
            #region 注释
            //openFileDialog1.ShowDialog();//打开openFileDialog1
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//打开openFileDialog1
            {
                string fName = openFileDialog1.FileName;
                MessageBox.Show(fName);
                textBox1.Text = fName;
                
                Bitmap bm = new Bitmap(fName);
                PictureBox pb = new PictureBox();
                pb.Image = bm;
                panel1.Controls.Add(pb);
            }
            else
            {
                return;
            }
            #endregion
            //int encryptionBarCode = Int32.Parse(textBox1.Text.Trim());
            //int originalBarCode = 
            //Utils.instance.ToMadePictureFromdt(panel1, dtProject);  //在panel1打印 完整条码（一维码）信息

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)//openfliedialog1控件的打开按钮事件
        {
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
                ////Blist.Clear();
                //ToEmptyingAllListandTable();
                //foreach (var a in openFileDialog1.FileNames)
                //{
                //    Bitmap bit = (Bitmap)Bitmap.FromFile(a);
                //    Blist.Add(new BitPicture(ToRestoreOriginalNum( ToDecryption( ToReadBitPicture(bit))), "", bit));//没有写完，缺乏解码功能！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
                //    Nlist.Add(new Number(ToRestoreOriginalNum(ToDecryption(ToReadBitPicture(bit))), ToDecryption(ToReadBitPicture(bit)), ToReadBitPicture(bit)));
                //}
                //ToMadePictureFromBlist();
                //DGV1.DataSource = null;
                //DGV1.DataSource = Nlist;
                //flowmessagel.Controls.Clear();
                //panel2.Visible = false;
        }

        private void distinguish_Click(object sender, EventArgs e)
        {
            /*
             * foreach (System.Windows.Forms.PictureBox a in panel1.Controls)
            {
                string s = ToRestoreOriginalNum(ToDecryption(ToReadBitPicture((Bitmap)a.Image)));
                MessageBox.Show(s);
            }
             */
            //if (flowmessagel.Controls.Count > 0) flowmessagel.Controls.Clear();
            //foreach (var a in Nlist)
            //{
            //    string t = "  条  码:" + a.EncryNum+"\n对应编码:"+a.OriNum;
            //    string b = ToReadOriginal(a.OriNum);

            //    RichTextBox rtb = new RichTextBox();
            //    rtb.Font = new System.Drawing.Font(rtb.Font.Name, 11);
            //    rtb.ReadOnly = true;
            //    rtb.Text = t + "\n" + b;
            //    rtb.Size = new System.Drawing.Size(340,100);
            //    flowmessagel.Controls.Add(rtb);
            //}
            //panel2.Visible = true;
        }

        private void close_Click(object sender, EventArgs e)
        {
            flowmessagel.Controls.Clear();
            panel2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void batchNum_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbma_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void reagentStandard_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void flowmessagel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void num2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void reagentName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void encryption_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void address_TextChanged(object sender, EventArgs e)
        {

        }

        private void prodectTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void toolTip2_Popup(object sender, PopupEventArgs e)
        {

        }

        private void DGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void numberBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //点击“手动保存”按钮
        private void saveBtn_Click_1(object sender, EventArgs e)
        {
            //if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            //{
            //    address.Text = folderBrowserDialog2.SelectedPath;
            //}         
            //bool isSave = true;
            //if (reagentName.Text != "" && reagentStandard.Text != "" && batchNum.Value != 0 && num1.Value != 0)
            //{
            //    if (dtProject.Rows.Count >= 1)
            //    {
            //        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //        saveFileDialog1.Title = "一维码保存";
            //        saveFileDialog1.Filter = "Jpeg|*.jpg";                                
            //        saveFileDialog1.FileName = "1" + "#" + DateTime.Now.ToString("yyyyMMdd") + "_" + dtProject.Rows[0][0].ToString() + ".jpg";
            //            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //            {
            //                if (isSave)
            //                {
            //                    try
            //                    {
            //                        for (int i = 0; i < dtProject.Rows.Count; i++)
            //                        {
            //                        pictureBox4.Image = ToWriteBitPicture(dtProject.Rows[i][1].ToString());
            //                        Bitmap bt = new Bitmap(pictureBox4.Image);
            //                        String filePath = address.Text + "//" + (i + 1) + "#" + DateTime.Now.ToString("yyyyMMdd") + "_" + dtProject.Rows[i][0].ToString() + ".jpg";
            //                        bt.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            //                        }
            //                        MessageBox.Show("图片保存成功");
            //                    }
            //                    catch
            //                    {
            //                        MessageBox.Show("图片保存失败");
            //                    }
            //                }
            //            }
            //        //}
            //    }
            //    else
            //    {
            //        MessageBox.Show("请生成编码后再进行保存！");
            //    }

            //}
            //else
            //{
            //    Utils.instance.saveImage(dtProject, "");
            //}
           
        }      
    }
} 

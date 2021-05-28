using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ZXing.Common;
using ZXing;
using System.Numerics;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace EBarv0._2
{
    class Utils
    {
        public static readonly Utils instance = new Utils();

        #region 填充一个数据字典
        /// <summary>
        /// 填充一个简单加密的数据字典，如果后期觉得加密不够严谨可以更改为更复杂的算法
        /// </summary>
        /// <returns></returns>
        public Dictionary<char, char> FillData()
        {

            Dictionary<char, char> dataDictionary = new Dictionary<char, char>();
            //填充0-9
            for (int i = (int)'0', j = (int)'9'; i <= (int)'9' && j >= (int)'0'; i++, j--) 
            {
                dataDictionary.Add((char)i, (char)j);
            }
            //填充a-z
            for (int i = (int)'a', j = (int)'Z'; i <= (int)'z' && j >= (int)'A'; i++, j--)
            {
                dataDictionary.Add((char)i, (char)j);
                
            }
            //填充A-Z
            for (int i = (int)'A', j = (int)'z'; i <= (int)'Z' && j <= (int)'a'; i++, j--)
            {
                dataDictionary.Add((char)i, (char)j);
            }
            dataDictionary.Add('.','*');
            return dataDictionary;
        }
        #endregion

        /// <summary>
        /// 加密获得的明文
        /// </summary>
        /// <returns>加密后字符串</returns>
        public string ToEncryption(string content) 
        {
            int key = 4;
            char[] chs = content.ToCharArray();
            for (int i = 0; i < chs.Length; i++)
            {
                if (chs[i] >= 'a' && chs[i] <= 'z')
                {
                    chs[i] = (char)(((chs[i] - 'a') + key) % 26 + 'a');
                }
                else if (chs[i] >= 'A' && chs[i] <= 'Z')
                {
                    chs[i] = (char)(((chs[i] - 'A') + key) % 26 + 'A');
                }
                else if (chs[i] >= '0' && chs[i] <= '9')
                {
                    chs[i] = (char)(((chs[i] - '0') + key) % 10 + '0');
                }
                else if (chs[i] == '.')            // lyq add 20190813 出现小数点解密是8的情况。改小数点为'Z'.(只在定标浓度时使用)，解密后为'V'
                {
                    //chs[i] = (char)95;
                    chs[i] = 'Z';
                }
                else
                {
                    MessageBox.Show("字符串出现超出 字母 数字 小数点 外的其余字符！");
                    return "";
                }
            }
            return new string(chs);
            //StringBuilder sb = new StringBuilder();
            //Dictionary<char, char> dictionary = FillData();
            //char[] oriChar = oriString.ToCharArray();
            //foreach (var ch in oriChar)
            //{
            //    foreach (var dic in dictionary)
            //    {
            //        if (ch.ToString() == dic.Key.ToString())
            //        {
            //            sb.Append(dic.Value);
            //        }
            //    }
            //}
            //return sb.ToString();
        }
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <returns>解密后字符串</returns>
        public string ToDecryption(string content)
        {
            int key = 4;
            char[] chs = content.ToCharArray();
            for (int i = 0; i < chs.Length; i++)
            {
                
                if (chs[i] >= 'a' && chs[i] <= 'z')
                {
                    chs[i] = (char)((chs[i] - 'a' + (26 - key)) % 26 + 'a');
                }
                else if (chs[i] >= 'A' && chs[i] <= 'Z')
                {
                    chs[i] = (char)((chs[i] - 'A' + (26 - key)) % 26 + 'A');
                }
                else if (chs[i] >= '0' && chs[i] <= '9')
                {
                    chs[i] = (char)((chs[i] - '0' + (10 - key)) % 10 + '0');
                }
                else
                {
                    chs[i] = (char)((chs[i] - '.' + (5 - key)) % 5 + '.');
                }

            }

            //lyq add 20190813 针对定标浓度和发光值 的小数点转化问题
            string str = new string(chs);
            if (chs[0] >= 3)
            {                
                str = str.Replace('V', '.');
            }            
            return str;
            #region 注释掉
            //StringBuilder sb = new StringBuilder();
            //Dictionary<char, char> dictionary = FillData();
            //char[] encryChar = encry.ToCharArray();
            //foreach (var ch in encryChar)
            //{
            //    foreach (var dic in dictionary)
            //    {
            //        if ((int)ch == (int)dic.Value)
            //        {
            //            if ((int)ch == (int)'J')
            //            {
            //                MessageBox.Show("J出现了！");
            //            }
            //            sb.Append(dic.Key);
            //        }
            //    }
            //}
            //return sb.ToString();
            #endregion
        }
        #region 字符串转换成一维码
        /// <summary>
        /// 字符串转换成一维码
        /// </summary>
        /// <param name="num">待转换的字符串</param>
        /// <returns>返回一个Bitmap对象</returns>
        public  Bitmap ToWriteBitPicture(string content)//根据文本内容，生成相应的一维码
        {
            EncodingOptions opt = new EncodingOptions();
            //设置一维码选项-尺寸
            opt.Height = 40;
            opt.Width = 200;

            ZXing.BarcodeFormat format = new BarcodeFormat();//一维码格式
            format = BarcodeFormat.CODE_128;//一维码格式，固定使用128
            ZXing.BarcodeWriter writer = new BarcodeWriter();//一维码绘制对象
            writer.Options = opt;
            writer.Format = format;
            return writer.Write(content);
        }
        #endregion
        /// <summary>
        /// 将Datatable中数据生成一维码图片，并显示到Panel上
        /// </summary>
        public void ToMadePictureFromdt(Panel panel,DataTable dataTable)  
        {
            panel.Controls.Clear();//清空可能存在的一维码

            int temporary = 0;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                PictureBox pB = new PictureBox();
                pB.Size = new Size(60, 200);
                pB.Location = new Point(70, temporary * 80);
                pB.SizeMode = PictureBoxSizeMode.AutoSize;
                pB.Image = ToWriteBitPicture(dataTable.Rows[i][1].ToString());
                panel.Controls.Add(pB);
                temporary++;
            }
        }
        /// <summary>
        /// 保存生成的一维码
        ///  <param name="dt">存放数据的DataTable</param>
        ///  <param name="FileName">存放一维码的文件夹</param>
        /// </summary>
        public void saveImage(DataTable dt,string FileName ,int flag, int? from = 0) //lyq mod 20190821
        {
            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("请生成条码后再进行保存。","温馨提示");
                return;
            }
            if (!Directory.Exists(FileName))
            {
                Directory.CreateDirectory(FileName);
            }
            //string address = Environment.CurrentDirectory + "\\"+FileName+"\\";  lyq 暂时注释 190810
            string pro = "";
            if (flag == 1)
            {
                pro = "试剂信息";
            }
            else if (flag == 2)
            {
                pro = "项目流程";
            }
            else if (flag == 3)
            {
                pro = "定标浓度";
            }
            else if (flag == 4)
            {
                pro = "发光值";
            }
            else if (flag == 5)
            {
                pro = "质控配套";
            }
            else if (flag == 6)
            {
                pro = "底物瓶";
            }
            else if (flag == 7)
            {
                pro = "稀释液";
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {                
                try
                {
                    //string filePath = address + "//" + FileName + (i + 1) + "#" + DateTime.Now.ToString("yyyyMMdd") + "_" + dt.Rows[i][0].ToString() + ".jpg";
                    string filePath =  FileName+"\\" + pro + "_" + (i + 1) + "#" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + dt.Rows[i][0].ToString() + ".jpg";
                    Utils.instance.ToWriteBitPicture(dt.Rows[i][1].ToString()).Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                }catch(Exception e)
                {
                    MessageBox.Show("保存出现异常!");
                }
            }
            if(from == 0)
                MessageBox.Show("保存成功", "成功");
        }

        /// <summary>
        /// 一维码添加到Panel
        /// </summary>
        /// <param name="panel">显示一维码的panel控件</param>
        /// <param name="bitmap">要添加的一维码图片</param>
        public void addPanelIma(Panel panel,Bitmap bitmap )
        {
            PictureBox pB = new PictureBox();
            pB.Size = new Size(60, 300);   
            pB.Location = new Point(70, 80);
            pB.SizeMode = PictureBoxSizeMode.AutoSize;
            pB.Image = bitmap;
            panel.Controls.Add(pB);
        }
        /// <summary>
        /// 清空DatyGridView数据源和Panel
        /// </summary>
        /// <param name="panel">显示一维码的panel控件</param>
        /// <param name="dt">数据</param>
        /// <return></return>
        public void clearShowData(Panel panel,DataTable dt)
        {
            panel.Controls.Clear();
            dt.Clear();
        }
    }
}

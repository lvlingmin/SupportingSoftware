using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.IO;
using System.Drawing.Drawing2D;

namespace fourLogistic
{

    public class drawCurve
    {
        Control cc;
        DataTable dt;
        DataTable dtScaling;
        public double standard = 0;
        public int scal = 1;
        int StartCupNum = 0;
        double AVGVALUE = 0, DifferenceValue = 0;

        double x, y;
        double minX, minY;
        double yy = 0;
        bool isstd, _isDrawValue;
        double[] _readNum = new double[4];
        public drawCurve()
        {
        }
        //样品部分画线
    
        Point zz(double dx, double dy)//根据数值画线
        {
            return new Point(Convert.ToInt32(45 + x * dx), Convert.ToInt32(cc.Height - (20 + y * 1000 * (dy - minY))));

            //return new Point(Convert.ToInt32(35 + x * 1000 * (dx - minX)), Convert.ToInt32(cc.Height - (20 + y * 1000 * (dy - minY))));
        }
        Point zz(double dx, int bx, double dy, int by)//根据数值画线
        {
            return new Point(Convert.ToInt32(45 + x * dx) + bx, Convert.ToInt32(cc.Height - (20 + y * (dy - minY) * 1000 + by)));
        }
     
        Point zzOptical(double dx, double dy)//根据数值画线
        {
            return new Point(Convert.ToInt32(35 + x * dx), Convert.ToInt32(cc.Height - (20 + y * dy)));
        }
        Point zzOptical(double dx, int bx, double dy, int by)//根据数值画线
        {
            return new Point(Convert.ToInt32(35 + x * dx) + bx, Convert.ToInt32(cc.Height - (20 + y * dy + by)));
        }
        
        //void drawCircleScaling(Graphics g, Point pt, int r, double dataValue)
        //{
        //    double d1;
        //    d1 = pt.Y - r;
        //    if (d1 >= int.MaxValue - 2000000000 || d1 < int.MinValue + 2000000000)
        //        d1 = int.MaxValue - 2000000000;
        //    g.DrawEllipse(new Pen(getColor(dataValue), 1), pt.X - r, Convert.ToInt32(d1), 2 * r, 2 * r);
        //    g.FillEllipse(new SolidBrush(getColor(dataValue)), pt.X - r, Convert.ToInt32(d1), 2 * r, 2 * r);
        //}
        Color getColor(double dataValue)
        {

            if ((AVGVALUE + DifferenceValue >= dataValue) && (dataValue >= AVGVALUE - DifferenceValue))
                return Color.Green;
            else if ((AVGVALUE - DifferenceValue > dataValue) && (dataValue >= AVGVALUE - 2 * DifferenceValue))
                return Color.Orange;
            else if ((AVGVALUE + 2 * DifferenceValue >= dataValue) && (dataValue > AVGVALUE + DifferenceValue))
                return Color.Orange;
            else
                return Color.Red;
        }

        
        void drawCircleScaling(Graphics g, Point pt, int r)
        {
            g.DrawEllipse(new Pen(Color.Red, 3), pt.X - r, pt.Y - r, 2 * r, 2 * r);
            //g.FillEllipse(Brushes.Yellow, pt.X - r, pt.Y - r, 2 * r, 2 * r);
        }
        Point zzScaling(double dx, double dy)//根据数值画线
        {
            double d1, d2;
            d1 = 35 + x * (dx - minX);
            d2 = cc.Height - (20 + y * (dy - minY));
            if (d1 > int.MaxValue || d1 < int.MinValue)
                d1 = 99999999;
            if (d2 > int.MaxValue || d2 < int.MinValue)
                d2 = 99999999;
            return new Point(Convert.ToInt32(d1), Convert.ToInt32(d2));
        }

      
        Point zzScaling(double dx, int bx, double dy, int by)//根据数值画线
        {
            double d1, d2;
            d1 = 35 + x * (dx - minX) + bx;
            d2 = cc.Height - (20 + y * (dy - minY) + by);
            if (d2 < 0)
                d2 = Math.Ceiling(double.Parse(dtScaling.Compute("max(absorbency)", "").ToString()));
            if (d1 > int.MaxValue || d1 < int.MinValue)
                d1 = 99999999;
            if (d2 > int.MaxValue || d2 < int.MinValue)
                d2 = 99999999;
            return new Point(Convert.ToInt32(d1), Convert.ToInt32(d2));
        }
        //定标部分画线
        public void paintEliseScaling(Control con, DataTable dataSoure, doubled fun, string strTitle, int index)
        {
            cc = con;
            dtScaling = dataSoure;
            //x浓度Y吸光度
            double X = 0, Y = 0;
            #region 曲线坐标轴 modify lyn 2016.08.13
            if (dtScaling.Rows.Count > 0)
            {
                minX = Math.Floor(double.Parse(dtScaling.Compute("min(consistence)", "").ToString()));//20150922 lxm
                minY = Math.Floor(double.Parse(dtScaling.Compute("min(absorbency)", "").ToString()));
                //minX = minY = 0.0;                
                X = Math.Ceiling(double.Parse(dtScaling.Compute("max(consistence)", "").ToString()));
                Y = Math.Ceiling(double.Parse(dtScaling.Compute("max(absorbency)", "").ToString()));
            }
            if (X == minX) minX -= 10;
            if (Y == minY) minY -= 10;
            double rowNum = 0;
            rowNum = dataSoure.Rows.Count + 1;
            if (rowNum < 3) rowNum = 6;
            double perXvalue = (X - minX) / rowNum;
            double perYvalue = (Y - minY) / rowNum;
            //if (perXvalue <= 0.5) perXvalue = 0.5;
            //else if (perXvalue > 1)
            //    perXvalue = Math.Floor(perXvalue);

            //if (perYvalue <= 0.5) perYvalue = 0.5;
            //else if (perYvalue > 1)
            //    perYvalue = Math.Floor(perYvalue);

            #endregion
            x = (con.Width - 30) / ((X - minX) / 0.9);
            y = (con.Height - 40) / ((Y - minY) / 0.9);
            Pen pn = new Pen(new SolidBrush(Color.SkyBlue), 1);
            pn.DashStyle = DashStyle.Dash;
            SolidBrush br = new SolidBrush(Color.Black);
            Bitmap bmp = new Bitmap(con.Width, con.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(Color.Snow), 1, 1, cc.Width - 2, cc.Height - 2); //填充边框
            if (!strTitle.IsNullOrEmpty())
            {
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Near;
                sf.Alignment = StringAlignment.Center;
                g.DrawString(strTitle, new Font("宋体", 10), br, new RectangleF(0, 3, con.ClientRectangle.Width, con.ClientRectangle.Height), sf);
            }


            g.DrawLine(new Pen(Color.Black, 1), zzScaling(minX, minY), zzScaling(minX + rowNum * perXvalue, 20, minY, 0));
            g.DrawString("＞", new Font("宋体", 8), br, zzScaling(minX + rowNum * perXvalue, 15, minY, 6));
            // g.DrawString("＞", new Font("宋体", 8), br, zzScaling(minX + rowNum * perXvalue, 15, minY, 6));
            g.DrawLine(new Pen(Color.Black, 1), zzScaling(minX, minY), zzScaling(minX, 0, minY + rowNum * perYvalue, 20));
            g.DrawString("＾", new Font("宋体", 20), br, zzScaling(minX, -18, minY + rowNum * perYvalue, 28));
            //g.DrawString("＾", new Font("宋体", 20), br, zzScaling(minX, -18, minY + rowNum * perYvalue, 28));

            //g.DrawString(Convert.ToDouble(minY + standard).ToString("f3"), new Font("Arial", 8), br, zzScaling(minX, -34, minY, 9));
            if ((minY + standard) >= 0)
                g.DrawString(Convert.ToDouble(minY + standard) <= -10 ? Convert.ToDouble(minY + standard).ToString() : Convert.ToDouble(minY + standard).ToString(), new Font("宋体", 10), br, zzScaling(minX, -23, minY, 9));
            else
                g.DrawString(Convert.ToDouble(minY + standard) <= -10 ? Convert.ToDouble(minY + standard).ToString() : Convert.ToDouble(minY + standard).ToString(), new Font("宋体", 10), br, zzScaling(minX, -29, minY, 9));
            g.DrawString(Convert.ToDouble(minX).ToString(), new Font("宋体", 10), br, zzScaling(minX, -8, minY, -1));
            for (int i = 1; i < rowNum + 1; i++)
            {
                g.DrawLine(pn, zzScaling(minX, minY + i * perYvalue), zzScaling(minX + rowNum * perXvalue, 10, minY + i * perYvalue, 0));//横线
                //g.DrawString(Convert.ToDouble((minY + i * perYvalue) / scal + standard).ToString("0.###"), new Font("Arial", 8), br, zzScaling(minX, -34, minY + i * perYvalue, 9));
                if (perYvalue < 1)
                {
                    if (((minY + i * perYvalue) / scal + standard) >= 0)
                        g.DrawString(Convert.ToDouble((minY + i * perYvalue) / scal + standard).ToString("f1"), new Font("宋体", 10), br, zzScaling(minX, -25, minY + i * perYvalue, 9));
                    else
                        g.DrawString(Convert.ToDouble((minY + i * perYvalue) / scal + standard).ToString("f1"), new Font("宋体", 10), br, zzScaling(minX, -32, minY + i * perYvalue, 9));
                }
                else
                {

                    if (((minY + i * perYvalue) / scal + standard) >= 0)
                        g.DrawString(Convert.ToDouble((minY + i * perYvalue) / scal + standard).ToString("f0"), new Font("宋体", 10), br, zzScaling(minX, -20, minY + i * perYvalue, 9));
                    else
                        g.DrawString(Convert.ToDouble((minY + i * perYvalue) / scal + standard).ToString("f0"), new Font("宋体", 10), br, zzScaling(minX, -27, minY + i * perYvalue, 9));
                }
            }
            for (int i = 1; i < rowNum + 1; i++)
            {
                if (perXvalue < 1)
                {
                    g.DrawLine(pn, zzScaling(minX + i * perXvalue, minY), zzScaling(minX + i * perXvalue, 0, minY + rowNum * perYvalue, 10));//竖线
                    g.DrawString(Convert.ToDouble(minX + i * perXvalue).ToString("f1"), new Font("宋体", 10), br, zzScaling(minX + i * perXvalue, -8, minY, -1));
                }
                else
                {
                    g.DrawLine(pn, zzScaling(minX + i * perXvalue, minY), zzScaling(minX + i * perXvalue, 0, minY + rowNum * perYvalue, 10));//竖线
                    g.DrawString(Convert.ToDouble(minX + i * perXvalue).ToString("f0"), new Font("宋体", 10), br, zzScaling(minX + i * perXvalue, -8, minY, -1));
                }
            }
            if (dtScaling.Rows.Count > 1)
            {
                Point[] pts = new Point[dtScaling.Rows.Count + (dtScaling.Rows.Count - 1) * 200];
                int ptsNumadding = 0;
                for (int i = 1; i < dtScaling.Columns.Count; i++)
                {
                    for (int j = 0; j < dtScaling.Rows.Count; j++)
                    {
                        double d1, d2;
                        d1 = (Convert.ToDouble(dtScaling.Rows[j][0]) - standard) * scal;

                        if ( d1 == 0)
                        {
                            d1 = 0.000000000000001;
                            d2 = (fun(Convert.ToDouble(d1)));
                        }
                        else
                            d2 = (fun(Convert.ToDouble(dtScaling.Rows[j][0])) - standard) * scal;
                        if (double.IsNaN(d1) || double.IsInfinity(d1))
                            d1 = int.MaxValue;

                        if (double.IsNaN(d2) || double.IsInfinity(d2))
                            d2 = int.MaxValue;
                        pts[ptsNumadding++] = zzScaling(d1, d2);
                        //if (j > 0 && j < dtScaling.Rows.Count - 1)
                        drawCircleScaling(g, zzScaling(Convert.ToDouble(dtScaling.Rows[j][0]), (Convert.ToDouble(dtScaling.Rows[j][i]) - standard) * scal), 2);
                        if (j < dtScaling.Rows.Count - 1)
                        {
                            double intercept = (Convert.ToDouble(dtScaling.Rows[j + 1][0]) - Convert.ToDouble(dtScaling.Rows[j][0])) / 200;
                            double interbase = Convert.ToDouble(dtScaling.Rows[j][0]);
                            double interceptResult = 0;
                            //StreamWriter tr = new StreamWriter(@"c:\tu.txt", false);
                            for (int t = 1; t < 201; t++)
                            {
                                interceptResult = interbase + intercept * t;
                                double calTemp = fun(interceptResult);
                                if (double.IsInfinity(calTemp) || double.IsNaN(calTemp))
                                    calTemp = interceptResult;
                                pts[ptsNumadding++] = zzScaling(interceptResult, (calTemp - standard) * scal);
                                //tr.WriteLine(interceptResult.ToString() + "\t" + fun(interceptResult) + "\t" + (fun(interceptResult) - standard) * scal);
                            }
                            //tr.Flush();
                            //tr.Close();
                        }

                    }
                    if (pts.Length > 1)
                    { 
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                        g.DrawCurve(new Pen(Color.Blue), pts, float.Parse(" 0.001"));
                        
                    }

                }
            }
            ////
            cc.BackgroundImage = bmp;
            //Graphics gp = con.CreateGraphics();
            //gp.DrawImage(bmp, 0, 0);
        }
        void ExpansionFpints(double d1, double d2, doubled fun)
        {
            double intercept = (d2 - d1) / 100;
            for (int i = 0; i < 100; i++)
            {

            }
        }
        Point zzEliseScaling(double dx, double dy)//根据数值画线
        {
            return new Point(Convert.ToInt32(35 + x * (dx - minX)), Convert.ToInt32(cc.Height - (20 + y * (dy - minY))));
        }
        Point zzEliseScaling(double dx, int bx, double dy, int by)//根据数值画线
        {
            return new Point(Convert.ToInt32(35 + x * (dx - minX)) + bx, Convert.ToInt32(cc.Height - (20 + y * (dy - minY) + by)));
        }

       
    }
}

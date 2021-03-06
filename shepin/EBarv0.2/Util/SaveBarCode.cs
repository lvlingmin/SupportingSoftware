using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NPOI.XWPF;
/// <summary>
/// 保存panel中一竖列条码图片到文件 lyq add 190809
/// 使用：SaveBarCode.GetPanel(panelName).Save(filePath);
/// </summary>
namespace EBarv0._2
{
    class SaveBarCode
    {
        #region API
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }
        public enum ScrollBarInfoFlags
        {
            SIF_RANGE = 0x0001,
            SIF_PAGE = 0x0002,
            SIF_POS = 0x0004,
            SIF_DISABLENOSCROLL = 0x0008,
            SIF_TRACKPOS = 0x0010,
            SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS)
        }
        public enum ScrollBarRequests
        {
            SB_LINEUP = 0,
            SB_LINELEFT = 0,
            SB_LINEDOWN = 1,
            SB_LINERIGHT = 1,
            SB_PAGEUP = 2,
            SB_PAGELEFT = 2,
            SB_PAGEDOWN = 3,
            SB_PAGERIGHT = 3,
            SB_THUMBPOSITION = 4,
            SB_THUMBTRACK = 5,
            SB_TOP = 6,
            SB_LEFT = 6,
            SB_BOTTOM = 7,
            SB_RIGHT = 7,
            SB_ENDSCROLL = 8
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetScrollInfo(IntPtr hwnd, int bar, ref SCROLLINFO si);
        [DllImport("user32")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool Rush);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        #endregion
        public static Bitmap GetPanel(Panel p_Panel)
        {
            bool _PanelAotu = p_Panel.AutoScroll;
            Size _PanelSize = p_Panel.Size;
            p_Panel.Visible = false;
            p_Panel.AutoScroll = true;
            MoveBar(0, 0, p_Panel);
            MoveBar(1, 0, p_Panel);
            Point _Point = GetScrollPoint(p_Panel);
            p_Panel.Width += _Point.X + 5;
            p_Panel.Height += _Point.Y + 5;
            Bitmap _BitMap = new Bitmap(p_Panel.Width, p_Panel.Height);
            p_Panel.DrawToBitmap(_BitMap, new Rectangle(0, 0, _BitMap.Width, _BitMap.Height));
            p_Panel.AutoScroll = _PanelAotu;
            p_Panel.Size = _PanelSize;
            p_Panel.Visible = true;
            return _BitMap;
        }
        /// <summary>
        /// 获取滚动条数据
        /// </summary>
        /// <param name="MyControl"></param>
        /// <param name="ScrollSize"></param>
        /// <returns></returns>
        private static Point GetScrollPoint(Control MyControl)
        {
            Point MaxScroll = new Point();
            SCROLLINFO ScrollInfo = new SCROLLINFO();
            ScrollInfo.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(ScrollInfo);
            ScrollInfo.fMask = (uint)ScrollBarInfoFlags.SIF_ALL;
            GetScrollInfo(MyControl.Handle, 1, ref ScrollInfo);
            MaxScroll.Y = ScrollInfo.nMax - (int)ScrollInfo.nPage;
            if ((int)ScrollInfo.nPage == 0) MaxScroll.Y = 0;
            GetScrollInfo(MyControl.Handle, 0, ref ScrollInfo);
            MaxScroll.X = ScrollInfo.nMax - (int)ScrollInfo.nPage;
            if ((int)ScrollInfo.nPage == 0) MaxScroll.X = 0;
            return MaxScroll;
        }
        /// <summary>
        /// 移动控件滚动条位置
        /// </summary>
        /// <param name="Bar"></param>
        /// <param name="Point"></param>
        /// <param name="MyControl"></param>
        private static void MoveBar(int Bar, int Point, Control MyControl)
        {
            if (Bar == 0)
            {
                SetScrollPos(MyControl.Handle, 0, Point, true);
                SendMessage(MyControl.Handle, (int)0x0114, (int)ScrollBarRequests.SB_THUMBPOSITION, 0);
            }
            else
            {
                SetScrollPos(MyControl.Handle, 1, Point, true);
                SendMessage(MyControl.Handle, (int)0x0115, (int)ScrollBarRequests.SB_THUMBPOSITION, 0);
            }
        }
        /// <summary>
        /// 通用返回Bitmap
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        private Bitmap DrawMeToBitmap(Control Obj)  //可以改为public static
        {
            int w = 0, h = 0;
            foreach (Control var in Obj.Controls)
            {
                if (var.Right > w)
                    w = var.Right;
                if (var.Bottom > h)
                    h = var.Bottom;
            }
            w++;
            h++;
            Bitmap bmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(bmp);
            using (Brush backColorBrush = new SolidBrush(Obj.BackColor))
            {
                g.FillRectangle(backColorBrush, new Rectangle(0, 0, bmp.Width, bmp.Height));
                foreach (Control var in Obj.Controls)
                {
                    using (Bitmap b = new Bitmap(var.Width, var.Height))
                    {
                        var.DrawToBitmap(b, var.ClientRectangle);
                        g.DrawImage(b, var.Location);
                    }
                }
            }
            return bmp;
        }

       
    }
}

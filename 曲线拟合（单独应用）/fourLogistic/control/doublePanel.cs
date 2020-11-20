using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fourLogistic.control
{
    public partial class doublePanel : Panel
    {
        public doublePanel()
        {
             SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);   //   禁止擦除背景.   
            SetStyle(ControlStyles.DoubleBuffer, true);   //   双缓冲 
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EBarv0._2.CustomControl
{
    public partial class defineButton : Button
    {
        public defineButton()
        {
            InitializeComponent();
        }

        public defineButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if(e.GetType() != typeof(MouseEventArgs))
            {
                return;
            }
            if(this.Enabled)
                base.OnClick(e);
        }
    }
}

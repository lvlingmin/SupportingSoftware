using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EBarv0._2.Administrator
{
    public partial class frmAdmin : frmParent
    {
        public static frmAdmin f0 = null;
        public frmAdmin()
        {
            InitializeComponent();
            f0 = this;
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {

        }

        private void btnItemImport_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLoadItem frmLoadItem = new FrmLoadItem();
            frmLoadItem.ShowDialog();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmInfo f = new frmInfo();
            f.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnUserList_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmUserList f = new frmUserList();
            f.ShowDialog();
        }
    }
}

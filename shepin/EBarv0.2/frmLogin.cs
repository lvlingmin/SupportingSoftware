using EBarv0._2.Administrator;
using EBarv0._2.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EBarv0._2
{
    public partial class frmLogin : Form
    {
        DataTable dtUser = new DataTable();
        BLL.tbUser bllUser = new BLL.tbUser();
        string KeepPwd = "";
        List<string> lisUsedName = new List<string>();
        public frmLogin()
        {
            InitializeComponent();
            this.MaximizeBox = false;//禁止最大化
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            KeepPwd = OperateIniFile.ReadInIPara("UsedName", "KeepPwd");
            if (KeepPwd != "" && KeepPwd == "1")
                chkKeepPwd.Checked = true;
            else
                chkKeepPwd.Checked = false;
            new Thread(new ThreadStart(() =>
            {
                bindUsedName();
            }))
            { IsBackground = true }.Start();
        }
        void bindUsedName()
        {
            string UserNames = OperateIniFile.ReadInIPara("UsedName", "UserName");
            if (UserNames.Trim() != "")
            {
                string[] username = UserNames.Split(',');
                foreach (string un in username)
                {
                    if (un.Trim() != "")
                    {
                        lisUsedName.Add(un);
                    }
                }
                this.BeginInvoke(new Action(() => {
                    this.txtUserName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    this.txtUserName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    foreach (string un in username)
                    {
                        if (un.Trim() != "")
                        {
                            txtUserName.Items.Add(un);
                        }
                    }
                    if (txtUserName.Items.Count > 0)
                        txtUserName.SelectedIndex = 0;
                }));
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string loginName = txtUserName.Text;
            string loginPassword = txtPassword.Text;

            dtUser = bllUser.GetList("UserName='" + loginName + "'").Tables[0];
            if (dtUser.Rows.Count < 1)
            {
                MessageBox.Show("用户名不正确，请重新输入！","用户登录");
                txtUserName.Focus();
                return;
            }
            else
            {
                dtUser.CaseSensitive = true;
                var dr = dtUser.Select("UserPassword='" + loginPassword + "'");
                if (dr.Length < 1)
                {
                    MessageBox.Show("密码不正确，请重新输入！","用户登录");
                    txtPassword.Text = "";
                    txtPassword.Focus();
                    return;
                }
                else
                {
                    frmParent.LoginUserName = txtUserName.Text.Trim();
                    frmParent.LoginUserType = dr[0]["RoleType"].ToString();
                }
            }
            foreach (string usdName in lisUsedName)
            {
                if (usdName.Trim() != txtUserName.Text)
                {
                    OperateIniFile.WriteIniPara("UsedName", "UserName", txtUserName.Text.Trim() + ",");
                }
            }
            if (chkKeepPwd.Checked)
                KeepPwd = "1";
            else
                KeepPwd = "0";
            OperateIniFile.WriteIniPara("UsedName", "KeepPwd", KeepPwd);
            BeginInvoke(new Action(() =>
            {
                Close();
            }));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void txtUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeepPwd != "" && KeepPwd == "1")
            {
                List<Model.tbUser> userlist = bllUser.GetModelList("UserName='" + txtUserName.SelectedItem.ToString() + "'");
                for (int i = 0; i < userlist.Count; i++)
                {
                    if (userlist[i] != null)
                        txtPassword.Text = userlist[i].UserPassword;
                }
            }
        }
    }

}

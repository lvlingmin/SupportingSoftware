using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EBarv0._2.Administrator
{
    public partial class frmUserList : frmParent
    {
        bool closedFlag = false;
        BLL.tbUser bllUser = new BLL.tbUser();
        DataTable dtData = new DataTable();
        DataTable dtUser = new DataTable();
        Model.tbUser modelUser = new Model.tbUser();

        public frmUserList()
        {
            InitializeComponent();
        }

        DataTable temp = new DataTable();
        private void frmUserList_Load(object sender, EventArgs e)
        {
            dtUser = bllUser.GetAllList().Tables[0];
            dtData.Columns.Add("No", typeof(string));
            dtData.Columns.Add("UserName", typeof(string));
            dtData.Columns.Add("UserRoleType", typeof(string));
            dtData.Columns.Add("UserPassword", typeof(string));
            
            DataTableChange(Convert.ToInt32(LoginUserType));
            dgvUserInfo.SelectionChanged -= dgvUserInfo_SelectionChanged;
            dgvUserInfo.DataSource = dtData;
            dgvUserInfo.SelectionChanged += dgvUserInfo_SelectionChanged;
            txtName.Enabled = txtPassword.Enabled = txtConfirmPassword.Enabled = cmbType.Enabled = btnSave.Enabled = false;

            temp.Columns.Add("No", typeof(string));
            temp.Columns.Add("UserName", typeof(string));
            temp.Columns.Add("UserRoleType", typeof(string));
            temp.Columns.Add("UserPassword", typeof(string));
            temp.Rows.Clear();
        }
        private void DataTableChange(int RoleType)
        {
            dtData.Rows.Clear();
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtUser.Rows[i]["RoleType"]) > 1)
                {
                    if (Convert.ToInt32(LoginUserType) == 9)
                        dtData.Rows.Add((i + 1).ToString(), dtUser.Rows[i]["UserName"].ToString(), "程序开发", dtUser.Rows[i]["UserPassword"].ToString());
                }
                else
                {
                    dtData.Rows.Add((dtData.Rows.Count + 1).ToString(), dtUser.Rows[i]["UserName"].ToString(), dtUser.Rows[i]["RoleType"].ToString() == "1" ? "管理员" : "普通用户", dtUser.Rows[i]["UserPassword"].ToString());
                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text.Trim() == "添加用户")
            {
                txtName.Enabled = txtPassword.Enabled = txtConfirmPassword.Enabled = cmbType.Enabled = btnSave.Enabled = true;
                txtName.Text = txtPassword.Text = txtConfirmPassword.Text = "";
                btnDel.Enabled = btnModifyPassword.Enabled = false;
                cmbType.SelectedIndex = 0;
                btnAdd.Text = "取消";
            }
            else
            {
                btnAdd.Text = "添加用户";
                dgvUserInfo_SelectionChanged(sender, e);
                txtName.Enabled = txtPassword.Enabled = txtConfirmPassword.Enabled = cmbType.Enabled = btnSave.Enabled = false;
                if (dgvUserInfo.Rows.Count > 0)
                    btnDel.Enabled = btnModifyPassword.Enabled = true;
                if (dgvUserInfo.SelectedRows.Count > 0)
                {
                    txtName.Text = dgvUserInfo.SelectedRows[0].Cells["UserName"].Value.ToString();
                    cmbType.Text = dgvUserInfo.SelectedRows[0].Cells["UserRoleType"].Value.ToString();
                }
                else
                {
                    txtName.Text = "";
                    txtPassword.Text = txtConfirmPassword.Text = "";
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var dr = dtUser.Select("UserName='" + txtName.Text + "'");
            if (dr.Length > 0)
            {
                if (bllUser.Delete(int.Parse(dr[0]["UserID"].ToString())))
                {
                    dtUser.Rows.Remove(dr[0]);
                    DataTableChange(Convert.ToInt32(LoginUserType));//2018-08-04 zlx mod
                    MessageBox.Show("删除成功！" ,"用户信息设置");
                }
                if (dgvUserInfo.Rows.Count == 0)
                {
                    txtName.Text = "";
                    txtPassword.Text = txtConfirmPassword.Text = "";
                }
                else
                {
                    foreach (DataGridViewRow a in dgvUserInfo.SelectedRows)
                    {
                        a.Selected = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("删除的用户名不存在或未选中行，请重新选择！" ,"用户信息设置");
            }
            dtUser = bllUser.GetAllList().Tables[0];
            if (dgvUserInfo.Rows.Count == 0)
            {
                btnDel.Enabled = btnModifyPassword.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("确认密码与用户密码不一致，请重新输入！" ,"用户信息设置");
                txtConfirmPassword.Text = "";
                txtConfirmPassword.Focus();
                return;
            }
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("未输入用户名，请输入！" ,"用户信息设置");
                txtName.Focus();
                return;
            }

            if (!Regex.IsMatch((txtName.Text.Trim()), @"^[\u4E00-\u9FA5A-Z0-9a-z\*\._]+$"))
            {
                MessageBox.Show("用户名至少由一位汉字、字母、星号、数字、\".\"、下划线组成。" ,"用户信息设置");
                txtName.Focus();
                return;
            }
            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("用户密码不能为空。" ,"用户设置");
                txtPassword.Focus();
                return;
            }
            if (!Regex.IsMatch((txtPassword.Text.Trim()), @"^[\u4E00-\u9FA5A-Z0-9a-z\*\._]*$"))
            {
                MessageBox.Show("密码不能存在汉字、字母、星号、数字、\".\"、下划线以外的符号。" ,"用户信息设置");
                txtPassword.Focus();
                return;
            }

            var dr = dtUser.Select("UserName='" + txtName.Text.Trim() + "'");
            if (dr.Length < 1)
            {
                modelUser.UserName = txtName.Text.Trim();
                modelUser.UserPassword = txtPassword.Text.Trim();
                modelUser.RoleType = cmbType.SelectedIndex;
                modelUser.defaultValue = 0;

                if (bllUser.Add(modelUser))
                {
                    dtData.Rows.Add(dtData.Rows.Count + 1, modelUser.UserName, modelUser.RoleType == 0 ? "普通用户" : "管理员", modelUser.UserPassword);
                    txtName.Enabled = txtPassword.Enabled = txtConfirmPassword.Enabled = cmbType.Enabled = btnSave.Enabled = false;
                    btnAdd.Text = "添加用户";
                    if (dgvUserInfo.Rows.Count != 0)
                        btnDel.Enabled = btnModifyPassword.Enabled = true;
                    MessageBox.Show("添加成功！" ,"用户信息设置");
                    dtUser = bllUser.GetAllList().Tables[0];
                }
            }
            else
            {
                if (btnAdd.Text == "取消" && btnModifyPassword.Text == "修改密码")
                {
                    MessageBox.Show("已有同名帐号，自动更新其密码。" ,"用户信息设置");
                }
                modelUser.UserID = int.Parse(dr[0]["UserID"].ToString());
                modelUser.UserName = dr[0]["UserName"].ToString();
                modelUser.UserPassword = txtPassword.Text.Trim();
                modelUser.RoleType = int.Parse(dr[0]["RoleType"].ToString());
                modelUser.defaultValue = 0;

                if (bllUser.Update(modelUser))
                {
                    btnModifyPassword.Text = "修改密码";
                    txtName.Enabled = txtPassword.Enabled = txtConfirmPassword.Enabled = cmbType.Enabled = btnSave.Enabled = false;
                    btnAdd.Text = "添加用户";
                    
                    if (Convert.ToInt32(LoginUserType) == 0)
                        btnModifyPassword.Enabled = true;
                    else
                        btnDel.Enabled = btnAdd.Enabled = btnModifyPassword.Enabled = true;

                    dtUser = bllUser.GetAllList().Tables[0];
                    DataTableChange(Convert.ToInt32(LoginUserType));

                    MessageBox.Show("密码修改成功！" ,"用户信息设置");
                }
            }
        }

        private void btnModifyPassword_Click(object sender, EventArgs e)
        {
            if (dgvUserInfo.SelectedRows.Count == 0)
                return;
            if (btnModifyPassword.Text.Trim() == "修改密码")
            {
                txtPassword.Enabled = txtConfirmPassword.Enabled = btnSave.Enabled = true;
                btnModifyPassword.Text = "取消";
                btnDel.Enabled = btnAdd.Enabled = false;
            }
            else
            {
                btnModifyPassword.Text = "修改密码";
                
                if (Convert.ToInt32(LoginUserType) == 0)
                    btnModifyPassword.Enabled = true;
                else
                {
                    txtPassword.Enabled = txtConfirmPassword.Enabled = btnSave.Enabled = false;
                    if (dgvUserInfo.SelectedRows.Count != 0)
                        btnDel.Enabled = true;
                    btnAdd.Enabled = true;
                }
                if (dgvUserInfo.SelectedRows.Count > 0)
                {
                    txtName.Text = dgvUserInfo.SelectedRows[0].Cells["UserName"].Value.ToString();
                    cmbType.Text = dgvUserInfo.SelectedRows[0].Cells["UserRoleType"].Value.ToString();
                }
                else
                {
                    txtName.Text = "";
                    txtPassword.Text = txtConfirmPassword.Text = "";
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            closedFlag = true;
            frmAdmin.f0.Show();
            this.Close();
        }

        private void frmUserList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!closedFlag)
            {
                frmAdmin.f0.Close();
                System.Environment.Exit(0);
            }
        }

        private void dgvUserInfo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUserInfo.SelectedRows.Count > 0)
            {
                int index = int.Parse(dgvUserInfo.SelectedRows[0].Cells[0].Value.ToString()) - 1;
                txtName.Text = dtData.Rows[index]["UserName"].ToString();
                cmbType.Text = dtData.Rows[index]["UserRoleType"].ToString();
                
                
                btnModifyPassword.Enabled = true;
            }
        }

        
    }
}

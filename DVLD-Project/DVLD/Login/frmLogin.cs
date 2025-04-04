﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD_Business;

namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";
            if(clsGlobal.GetStoredCredentialFromRegistry(ref UserName,ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindByUserNameAndPassword(txtUserName.Text.Trim(), clsUser.ComputeHash(txtPassword.Text.Trim()));
            if (User != null)
            {
                if (chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPasswordUsingRegistry(txtUserName.Text.Trim(), clsUser.ComputeHash(txtPassword.Text.Trim()));

                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text, txtPassword.Text);
                    clsUser.ComputeHash(txtPassword.Text);
                }
                else
                {
                    clsGlobal.RememberUsernameAndPasswordUsingRegistry("", "");
                }
                if (!User.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                clsGlobal.CurrentUser = User;
                this.Hide();
                frmMain main= new frmMain(this);
                main.ShowDialog();
            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Worng Credentials",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}

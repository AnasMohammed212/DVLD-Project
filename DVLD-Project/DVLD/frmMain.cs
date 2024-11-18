using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD.Login;
using DVLD.People;
using DVLD.Users;

namespace DVLD
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;
        public frmMain( frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }
        
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            //frm.MdiParent = this;
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();  
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void changeUserPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm=new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }
        private bool _CloseApplication = true;
        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _CloseApplication = false;
            _frmLogin.Show();
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_CloseApplication)
            {
                _frmLogin.Close();
                return;
            }
        }
    }
}

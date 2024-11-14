using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD.Users
{
    public partial class frmListUsers : Form
    {
        private static DataTable _dtUsers=clsUser.GetAllUsers();
        public frmListUsers()
        {
            InitializeComponent();
        }
        private void _RefreshUsersList()
        {
            _dtUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtUsers;
            lblRecordsCount.Text=dgvUsers.Rows.Count.ToString();

        }
        private void frmListUsers_Load(object sender, EventArgs e)
        {
            dgvUsers.DataSource = _dtUsers;
            lblRecordsCount.Text=dgvUsers.Rows.Count.ToString();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm=new frmAddUpdateUser();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
            _RefreshUsersList();
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser Frm1 = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            Frm1.ShowDialog();
            frmListUsers_Load(null, null);
            _RefreshUsersList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

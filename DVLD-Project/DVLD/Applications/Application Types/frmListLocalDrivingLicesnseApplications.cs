using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Applications.Local_Driving_License;
using DVLD_Business;

namespace DVLD.Applications.Application_Types
{
    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        private DataTable _dtAllLocalDrivingLicenseApplications;
        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
        }

        private void frmListLocalDrivingLicesnseApplications_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvListLocalDrivingLicenseApplication.DataSource = _dtAllLocalDrivingLicenseApplications;

            lblRecordsCount.Text = dgvListLocalDrivingLicenseApplication.Rows.Count.ToString();
            if (dgvListLocalDrivingLicenseApplication.Rows.Count > 0)
            {

                dgvListLocalDrivingLicenseApplication.Columns[0].HeaderText = "L.D.L.AppID";
                dgvListLocalDrivingLicenseApplication.Columns[0].Width = 120;

                dgvListLocalDrivingLicenseApplication.Columns[1].HeaderText = "Driving Class";
                dgvListLocalDrivingLicenseApplication.Columns[1].Width = 300;

                dgvListLocalDrivingLicenseApplication.Columns[2].HeaderText = "National No.";
                dgvListLocalDrivingLicenseApplication.Columns[2].Width = 150;

                dgvListLocalDrivingLicenseApplication.Columns[3].HeaderText = "Full Name";
                dgvListLocalDrivingLicenseApplication.Columns[3].Width = 350;

                dgvListLocalDrivingLicenseApplication.Columns[4].HeaderText = "Application Date";
                dgvListLocalDrivingLicenseApplication.Columns[4].Width = 170;

                dgvListLocalDrivingLicenseApplication.Columns[5].HeaderText = "Passed Tests";
                    dgvListLocalDrivingLicenseApplication.Columns[5].Width = 150;
            }

            cbFilterBy.SelectedIndex = 0;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvListLocalDrivingLicenseApplication.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvListLocalDrivingLicenseApplication.Rows.Count.ToString();
                return;
            }
            if(FilterColumn== "LocalDrivingLicenseApplicationID")
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter=string.Format("[{0}]={1}",FilterColumn,txtFilterValue.Text.Trim());
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = dgvListLocalDrivingLicenseApplication.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text== "L.D.L.AppID")
                e.Handled=!char.IsDigit(e.KeyChar)&& !char.IsControl(e.KeyChar);
        }

        private void btnAddApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm=new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented Yet!", "Sorry");
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;

            frmAddUpdateLocalDrivingLicenseApplication frm =new frmAddUpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            int LocalDrivingLicenseApplicationID = (int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListLocalDrivingLicesnseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

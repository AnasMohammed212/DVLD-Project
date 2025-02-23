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
using DVLD.Licenses.Local_Licenses;
using DVLD.Tests;
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
                dgvListLocalDrivingLicenseApplication.Columns[0].Width = 100;

                dgvListLocalDrivingLicenseApplication.Columns[1].HeaderText = "Driving Class";
                dgvListLocalDrivingLicenseApplication.Columns[1].Width = 200;

                dgvListLocalDrivingLicenseApplication.Columns[2].HeaderText = "National No.";
                dgvListLocalDrivingLicenseApplication.Columns[2].Width = 100;

                dgvListLocalDrivingLicenseApplication.Columns[3].HeaderText = "Full Name";
                dgvListLocalDrivingLicenseApplication.Columns[3].Width = 300;

                dgvListLocalDrivingLicenseApplication.Columns[4].HeaderText = "Application Date";
                dgvListLocalDrivingLicenseApplication.Columns[4].Width = 170;

                dgvListLocalDrivingLicenseApplication.Columns[5].HeaderText = "Passed Tests";
                    dgvListLocalDrivingLicenseApplication.Columns[5].Width = 100;
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

        private void scheduleTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int LocalDrivingLicenseApplicationID = (int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;

            //clsTestAppointment Appointment =
            //    clsTestAppointment.GetLastTestAppointment(LocalDrivingLicenseApplicationID, clsTestType.enTestType.VisionTest);

            //if (Appointment == null)
            //{
            //    MessageBox.Show("No Vision Test Appointment Found!", "Set Appointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //frmTakeTest frm = new frmTakeTest(Appointment.TestAppointmentID, clsTestType.enTestType.VisionTest);
            //frm.ShowDialog();

            _ScheduleTest(clsTestType.enTestType.VisionTest);


        }

        private void writtenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int LocalDrivingLicenseApplicationID = (int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;


            //if (!clsLocalDrivingLicenseApplication.DoesPassTestType(LocalDrivingLicenseApplicationID, clsTestType.enTestType.VisionTest))
            //{
            //    MessageBox.Show("Person Should Pass the Vision Test First!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //clsTestAppointment Appointment =
            //   clsTestAppointment.GetLastTestAppointment(LocalDrivingLicenseApplicationID, clsTestType.enTestType.WrittenTest);


            //if (Appointment == null)
            //{
            //    MessageBox.Show("No Written Test Appointment Found!", "Set Appointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}


            //frmTakeTest frm = new frmTakeTest(Appointment.TestAppointmentID, clsTestType.enTestType.WrittenTest);
            //frm.ShowDialog();
            _ScheduleTest(clsTestType.enTestType.WrittenTest);

        }

        private void streetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int LocalDrivingLicenseApplicationID = (int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;

            //if (!clsLocalDrivingLicenseApplication.DoesPassTestType(LocalDrivingLicenseApplicationID, clsTestType.enTestType.WrittenTest))
            //{
            //    MessageBox.Show("Person Should Pass the Written Test First!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //clsTestAppointment Appointment =
            // clsTestAppointment.GetLastTestAppointment(LocalDrivingLicenseApplicationID, clsTestType.enTestType.StreetTest);


            //if (Appointment == null)
            //{
            //    MessageBox.Show("No Street Test Appointment Found!", "Set Appointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //frmTakeTest frm = new frmTakeTest(Appointment.TestAppointmentID, clsTestType.enTestType.StreetTest);
            //frm.ShowDialog();
            _ScheduleTest(clsTestType.enTestType.StreetTest);

        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListLocalDrivingLicesnseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            int TotalPassedTests = (int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[5].Value;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3);
            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();

            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editApplicationToolStripMenuItem.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
            ScheduleTestsMenu.Enabled = !LicenseExists;
            cancelToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
            deleteApplicationToolStripMenuItem.Enabled =
                (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest); ;
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;
            ScheduleTestsMenu.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (ScheduleTestsMenu.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;
                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
            }
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicenseFirstTime frm=new frmIssueDriverLicenseFirstTime((int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
        private void _ScheduleTest(clsTestType.enTestType TestType)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }
        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            _ScheduleTest(clsTestType.enTestType.WrittenTest);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            _ScheduleTest(clsTestType.enTestType.StreetTest);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID= (int)dgvListLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;
            int LicenseID=clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();
            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}

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

namespace DVLD.Tests.Tests_Types
{
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtAllTestTypes;
        public frmListTestTypes()
        {
            InitializeComponent();
        }
        private void _RefreshListTestTypes()
        {
            _dtAllTestTypes=clsTestType.GetAllTestTypes();
            dgvListTestTypes.DataSource = _dtAllTestTypes;
            lblRecordsCount.Text=dgvListTestTypes.Rows.Count.ToString();
            dgvListTestTypes.Columns[0].HeaderText = "ID";
            dgvListTestTypes.Columns[0].Width = 120;

            dgvListTestTypes.Columns[1].HeaderText = "Title";
            dgvListTestTypes.Columns[1].Width = 200;

            dgvListTestTypes.Columns[2].HeaderText = "Description";
            dgvListTestTypes.Columns[2].Width = 400;

            dgvListTestTypes.Columns[3].HeaderText = "Fees";
            dgvListTestTypes.Columns[3].Width = 100;
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshListTestTypes();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((int)(clsTestType.enTestType)dgvListTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListTestTypes_Load(null, null);
        }
    }
}

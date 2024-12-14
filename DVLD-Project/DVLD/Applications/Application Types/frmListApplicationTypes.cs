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

namespace DVLD.Applications.Application_Types
{
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _dtAllApplicationTypes;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes=clsApplicationType.GetAllApplicationTypes();
            dgvListApplicationTypes.DataSource = _dtAllApplicationTypes;
            lblRecordsCount.Text=dgvListApplicationTypes.Rows.Count.ToString();
            if (dgvListApplicationTypes.Rows.Count > 0)
            {
                dgvListApplicationTypes.Columns[0].HeaderText = "ID";
                dgvListApplicationTypes.Columns[0].Width = 110;
                dgvListApplicationTypes.Columns[1].HeaderText = "Title";
                dgvListApplicationTypes.Columns[1].Width = 400;
                dgvListApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvListApplicationTypes.Columns[2].Width = 100;
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

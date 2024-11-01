using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.People;

namespace DVLD
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }
        frmListPeople frm = new frmListPeople();
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frm.MdiParent = this;
            frm.Show();
        }
    }
}

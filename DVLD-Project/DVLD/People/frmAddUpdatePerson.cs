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
using static DVLD_Business.clsPerson;

namespace DVLD.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public enum enMode {AddNew=0,Update=1};
        public enum enGender {Male=0,Female=1};
        private enMode _Mode;
        private int _PersonID = -1;
        clsPerson _Person;
        public frmAddUpdatePerson()
        {
            InitializeComponent();
        }
        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID=PersonID;
        }
        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries=clsCountry.GetAllCountries();
            foreach (DataRow row in dtCountries.Rows)
                cbCountry.Items.Add(row["CountryName"]);

        }
        private void _ResetDefaultValues()
        {
            _FillCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
                lblTitle.Text = "Update Person";
        }
        private void _LoadData()
        {

        }
        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }
    }
}

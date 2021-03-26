using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examination_system
{
    public partial class ManageInstructor : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public ManageInstructor()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            CenterToScreen();
            base.OnLoad(e);
        }
        public void fillDataGridView()
        {
            //dataGridView1.DataSource = db.SP_Allinstructors();
            var instructours = (from obj in db.Instructors
                               join obj2 in db.Departments
                               on obj.deptID equals obj2.deptID
                               select new
                               {
                                   obj.instID,
                                   obj.fName,
                                   obj.lName,
                                   obj2.deptName,
                                   obj.instUserName,
                                   obj.instPassword
                               }).ToList();
            dataGridView1.DataSource = instructours;
        }
        public void fillCombobox()
        {
            var qDepartments = (from obj in db.Departments
                                select new
                                {
                                    obj.deptID,
                                    obj.deptName
                                }).ToList();
            foreach (var item in qDepartments)
            {
                cmboxDepartment.Items.Add(item.deptName);
            }
        }

        private void ManageInstructor_Load(object sender, EventArgs e)
        {
            fillDataGridView();
            fillCombobox();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string deptName = cmboxDepartment.SelectedItem.ToString();
            Department department = (from obj in db.Departments
                                     where obj.deptName == deptName
                                     select obj).Distinct().FirstOrDefault();
            Instructor inst = new Instructor();
            inst.fName = txtFName.Text;
            inst.lName = txtLName.Text;
            inst.deptID = department.deptID;
            inst.instUserName = txtUserName.Text;
            inst.instPassword = txtPassword.Text;
            db.Instructors.Add(inst);
            db.SaveChanges();
            txtFName.Text = txtInstID.Text = txtUserName.Text = txtPassword.Text = txtLName.Text = cmboxDepartment.Text = string.Empty;
            cmboxDepartment.Items.Clear();
            fillDataGridView();
            fillCombobox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtFName.Text = txtInstID.Text = txtUserName.Text = txtPassword.Text = txtLName.Text = cmboxDepartment.Text = string.Empty;
            cmboxDepartment.Items.Clear();
            fillDataGridView();
            fillCombobox();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string deptName = cmboxDepartment.SelectedItem.ToString();
            int instID = Convert.ToInt32(txtInstID.Text);
            Department department = (from obj in db.Departments
                                     where obj.deptName == deptName
                                     select obj).Distinct().FirstOrDefault();
            Instructor instructor = (from obj in db.Instructors
                                     where obj.instID == instID
                                     select obj).FirstOrDefault();
            instructor.fName = txtFName.Text;
            instructor.lName = txtLName.Text;
            instructor.deptID = department.deptID;
            instructor.instUserName = txtUserName.Text;
            instructor.instPassword = txtPassword.Text;
            db.SaveChanges();
            txtFName.Text = txtInstID.Text = txtPassword.Text = txtUserName.Text = txtLName.Text = cmboxDepartment.Text = string.Empty;
            cmboxDepartment.Items.Clear();
            fillDataGridView();
            fillCombobox();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            txtInstID.Text = selectedRow.Cells[0].Value.ToString();
            txtFName.Text = selectedRow.Cells[1].Value.ToString();
            txtLName.Text = selectedRow.Cells[2].Value.ToString();
            //int deptID = Convert.ToInt32(selectedRow.Cells[3].Value.ToString());
            //Department dept = (from obj in db.Departments
            //                   where obj.deptID == deptID
            //                   select obj).Distinct().FirstOrDefault();
            cmboxDepartment.Text = selectedRow.Cells[3].Value.ToString();
            if(selectedRow.Cells[4].Value == null && selectedRow.Cells[5].Value == null)
            {
                txtUserName.Text = "";
                txtPassword.Text = ""; 
            }
            else
            {
                txtUserName.Text = selectedRow.Cells[4].Value.ToString();
                txtPassword.Text = selectedRow.Cells[5].Value.ToString();
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int instID = Convert.ToInt32(txtInstID.Text);
            Instructor instructor = (from obj in db.Instructors
                                     where obj.instID == instID
                                     select obj).FirstOrDefault();
            db.SP_DeleteInsById(instructor.instID);
            txtFName.Text = txtInstID.Text = txtLName.Text = txtPassword.Text = txtUserName.Text = cmboxDepartment.Text = string.Empty;
            cmboxDepartment.Items.Clear();
            fillDataGridView();
            fillCombobox();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.SP_InsSelectByName(txtSearch.Text);
            var instructours = (from obj in db.Instructors
                                join obj2 in db.Departments
                                on obj.deptID equals obj2.deptID
                                where obj.fName == txtSearch.Text
                                select new
                                {
                                    obj.instID,
                                    obj.fName,
                                    obj.lName,
                                    obj2.deptName,
                                    obj.instUserName,
                                    obj.instPassword
                                }).ToList();
            dataGridView1.DataSource = instructours;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCrs_Click(object sender, EventArgs e)
        {
            ManageInstructorCourse ictCrs = new ManageInstructorCourse();
            ictCrs.Show();
        }

        
    }
}

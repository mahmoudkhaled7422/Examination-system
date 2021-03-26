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
    public partial class MangeStudent : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public MangeStudent()
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
            var Students = (from obj in db.Students
                            join obj2 in db.Departments
                            on obj.deptID equals obj2.deptID
                            select new
                            {
                                obj.stdID,
                                obj.fName,
                                obj.lName,
                                obj.email,
                                obj.phone,
                                obj.address,
                                obj2.deptName,
                                obj.stdUserName,
                                obj.stdPassword
                            }).ToList();
            dataGridView1.DataSource = Students;
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
        private void MangeStudent_Load(object sender, EventArgs e)
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
            Student std = new Student();
            std.fName = txtFName.Text;
            std.lName = txtLName.Text;
            std.phone = txtPhone.Text;
            std.address = txtAddress.Text;
            std.email = txtEmail.Text;
            std.deptID = department.deptID;
            std.stdUserName = txtUserName.Text;
            std.stdPassword = txtPassword.Text;
            db.Students.Add(std);
            db.SaveChanges();
            txtFName.Text = txtStdID.Text = txtLName.Text = txtEmail.Text
                = txtPhone.Text = txtAddress.Text = txtPassword.Text = txtUserName.Text = cmboxDepartment.Text = string.Empty;
            cmboxDepartment.Items.Clear();
            fillDataGridView();
            fillCombobox();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string deptName = cmboxDepartment.SelectedItem.ToString();
            int stdID = Convert.ToInt32(txtStdID.Text);
            Department department = (from obj in db.Departments
                                     where obj.deptName == deptName
                                     select obj).Distinct().FirstOrDefault();
            Student std = (from obj in db.Students
                           where obj.stdID == stdID
                           select obj).FirstOrDefault();
            std.fName = txtFName.Text;
            std.lName = txtLName.Text;
            std.phone = txtPhone.Text;
            std.address = txtAddress.Text;
            std.email = txtEmail.Text;
            std.deptID = department.deptID;
            std.stdUserName = txtUserName.Text;
            std.stdPassword = txtPassword.Text;
            db.SaveChanges();
            txtFName.Text = txtStdID.Text = txtLName.Text = txtEmail.Text
                = txtPhone.Text = txtAddress.Text = txtUserName.Text = txtPassword.Text = cmboxDepartment.Text = string.Empty;
            cmboxDepartment.Items.Clear();
            fillDataGridView();
            fillCombobox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtFName.Text = txtStdID.Text = txtLName.Text = txtEmail.Text
                = txtPhone.Text = txtAddress.Text = txtPassword.Text = txtUserName.Text = cmboxDepartment.Text = string.Empty;
            cmboxDepartment.Items.Clear();
            fillDataGridView();
            fillCombobox();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            txtStdID.Text = selectedRow.Cells[0].Value.ToString();
            txtFName.Text = selectedRow.Cells[1].Value.ToString();
            txtLName.Text = selectedRow.Cells[2].Value.ToString();
            txtEmail.Text = selectedRow.Cells[3].Value.ToString();
            txtPhone.Text = selectedRow.Cells[4].Value.ToString();
            txtAddress.Text = selectedRow.Cells[5].Value.ToString();
            //int deptID = Convert.ToInt32(selectedRow.Cells[6].Value.ToString());
            //Department dept = (from obj in db.Departments
            //                   where obj.deptID == deptID
            //                   select obj).Distinct().FirstOrDefault();
            cmboxDepartment.Text = selectedRow.Cells[6].Value.ToString();//dept.deptName;
            if (selectedRow.Cells[7].Value == null && selectedRow.Cells[8].Value == null)
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
            }
            else
            {
                txtUserName.Text = selectedRow.Cells[7].Value.ToString();
                txtPassword.Text = selectedRow.Cells[8].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int stdID = Convert.ToInt32(txtStdID.Text);
                Student std = (from obj in db.Students
                               where obj.stdID == stdID
                               select obj).FirstOrDefault();
                db.DelSt(std.stdID);
                db.SaveChanges();
                
            }
            catch (Exception)
            {

                
            }
            txtFName.Text = txtStdID.Text = txtLName.Text = txtEmail.Text
                     = txtPhone.Text = txtAddress.Text = txtPassword.Text = txtUserName.Text = cmboxDepartment.Text = string.Empty;
            cmboxDepartment.Items.Clear();
            fillDataGridView();
            fillCombobox();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.SP_stdSearchByName(txtSearch.Text);
            var Students = (from obj in db.Students
                            join obj2 in db.Departments
                            on obj.deptID equals obj2.deptID
                            where obj.fName == txtSearch.Text
                            select new
                            {
                                obj.stdID,
                                obj.fName,
                                obj.lName,
                                obj.email,
                                obj.phone,
                                obj.address,
                                obj2.deptName,
                                obj.stdUserName,
                                obj.stdPassword
                            }).ToList();
            dataGridView1.DataSource = Students;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCrs_Click(object sender, EventArgs e)
        {
            ManageStudentCourse stdCrs = new ManageStudentCourse();
            stdCrs.Show();

        }
    }
}

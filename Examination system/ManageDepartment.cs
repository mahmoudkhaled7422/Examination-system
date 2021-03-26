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
    public partial class ManageDepartment : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public ManageDepartment()
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
            //dataGridView1.DataSource = db.SP_AllTDepartments();
            var dept = (from obj in db.Departments
                       join obj1 in db.Instructors
                       on obj.instID equals obj1.instID
                       select new
                       {
                           obj.deptID,
                           obj.deptName,
                           obj1.fName,
                           obj1.lName
                       }).ToList();
            dataGridView1.DataSource = dept;
        }
        public void fillCombobox()
        {
            var qInstractors = (from obj in db.Instructors
                                select new
                                {
                                    obj.instID,
                                    instName = obj.fName+" "+obj.lName
                                }).ToList();
            foreach (var item in qInstractors)
            {
                cmboxMGRS.Items.Add(item.instName);
            }
        }
        private void ManageDepartment_Load(object sender, EventArgs e)
        {
            fillDataGridView();
            fillCombobox();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string FullName = "";
            string fName = "";
            string lName = "";
            if (cmboxMGRS.SelectedItem != null)
            {
                 FullName= cmboxMGRS.SelectedItem.ToString();
                int pos = FullName.IndexOf(" ");
                fName = FullName.Substring(0, pos);
                lName = FullName.Substring(pos + 1);
            }
            
            Instructor instructor = (from obj in db.Instructors
                                     where obj.fName == fName && obj.lName == lName
                                     select obj).Distinct().FirstOrDefault();
            Department dept = new Department();
            dept.deptName = txtDeptName.Text;
            if(cmboxMGRS.SelectedItem != null)
            {
                dept.instID = instructor.instID;
            }
            db.Departments.Add(dept);
            db.SaveChanges();
            txtDeptID.Text = txtDeptName.Text = cmboxMGRS.Text = string.Empty;
            cmboxMGRS.Items.Clear();
            MessageBox.Show("Department Saved Correctlly");
            fillDataGridView();
            fillCombobox();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            txtDeptID.Text = selectedRow.Cells[0].Value.ToString();
            txtDeptName.Text = selectedRow.Cells[1].Value.ToString();
            if(selectedRow.Cells[2].Value != null)
            {
                //int instID = Convert.ToInt32(selectedRow.Cells[2].Value.ToString());
                //Instructor instructor = (from obj in db.Instructors
                //                         where obj.instID == instID
                //                         select obj).Distinct().FirstOrDefault();
                cmboxMGRS.Text = selectedRow.Cells[2].Value.ToString() + " " + selectedRow.Cells[3].Value.ToString();
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int deptID = Convert.ToInt32(txtDeptID.Text);
            string FullName = "";
            string fName = "";
            if (cmboxMGRS.SelectedItem != null)
            {
                FullName = cmboxMGRS.SelectedItem.ToString();
                int pos = FullName.IndexOf(" ");
                fName = FullName.Substring(0, pos);
            }

            Instructor instructor = (from obj in db.Instructors
                                     where obj.fName == fName
                                     select obj).Distinct().FirstOrDefault();
            Department dept = (from obj in db.Departments
                               where obj.deptID == deptID
                               select obj).FirstOrDefault();
            dept.deptName = txtDeptName.Text;
            if(instructor != null)
            {
                dept.instID = instructor.instID;
            }
            
            db.SaveChanges();
            txtDeptID.Text = txtDeptName.Text = cmboxMGRS.Text = string.Empty;
            cmboxMGRS.Items.Clear();
            MessageBox.Show("Department updated Correctlly");
            fillDataGridView();
            fillCombobox();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int deptID = Convert.ToInt32(txtDeptID.Text);
            Department dept = (from obj in db.Departments
                               where obj.deptID == deptID
                               select obj).FirstOrDefault();
            //db.Departments.Remove(dept);
            //db.SaveChanges();
            db.SP_DeleteDepById(dept.deptID);
            txtDeptID.Text = txtDeptName.Text = cmboxMGRS.Text = string.Empty;
            cmboxMGRS.Items.Clear();
            MessageBox.Show("Department Deleted Correctlly");
            fillDataGridView();
            fillCombobox();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.SP_DepSelectByName(txtSearch.Text);
            var dept = (from obj in db.Departments
                        join obj1 in db.Instructors
                        on obj.instID equals obj1.instID
                        where obj.deptName == txtDeptName.Text
                        select new
                        {
                            obj.deptID,
                            obj.deptName,
                            obj1.fName,
                            obj1.lName
                        }).ToList();
            dataGridView1.DataSource = dept;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtDeptID.Text = txtDeptName.Text = cmboxMGRS.Text = string.Empty;
            cmboxMGRS.Items.Clear();
            fillDataGridView();
            fillCombobox();
        }
    }
}

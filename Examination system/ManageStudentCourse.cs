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
    public partial class ManageStudentCourse : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public ManageStudentCourse()
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
            var stdCrs = (from obj in db.stdCourses
                           join obj2 in db.Courses
                           on obj.crsID equals obj2.crsID
                           join obj3 in db.Students
                           on obj.stdID equals obj3.stdID
                           select new
                           {
                               obj.ID,
                               obj2.crsName,
                               obj3.fName,
                               obj3.lName
                           }).ToList();
            dataGridView1.DataSource = stdCrs;
        }
        public void fillComboboxCrs()
        {
            var qCourses = (from obj in db.Courses
                            select new
                            {
                                obj.crsID,
                                obj.crsName
                            }).ToList();
            foreach (var item in qCourses)
            {
                cmboxCourse.Items.Add(item.crsName);
                cmboxCourseSearch.Items.Add(item.crsName);
            }
        }
        public void fillComboboxStd()
        {
            var qStds = (from obj in db.Students
                                select new
                                {
                                    obj.stdID,
                                    obj.fName,
                                    obj.lName
                                }).ToList();
            foreach (var item in qStds)
            {
                cmboxStudent.Items.Add(item.fName + " " + item.lName);
            }
        }
        private void ManageStudentCourse_Load(object sender, EventArgs e)
        {
            fillDataGridView();
            fillComboboxCrs();
            fillComboboxStd();
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            string FullName = "";
            string fName = "";
            string lName = "";
            if (cmboxStudent.SelectedItem != null)
            {
                FullName = cmboxStudent.SelectedItem.ToString();
                int pos = FullName.IndexOf(" ");
                fName = FullName.Substring(0, pos);
                lName = FullName.Substring(pos+1);
            }
            Student std = (from obj in db.Students
                                     where obj.fName == fName && obj.lName == lName
            select obj).Distinct().FirstOrDefault();
            string crsName = cmboxCourse.SelectedItem.ToString();
            Course crs = (from obj in db.Courses
                          where obj.crsName == crsName
                          select obj).Distinct().FirstOrDefault();
            stdCourse stdCrs = new stdCourse();
            stdCrs.crsID = crs.crsID;
            stdCrs.stdID = std.stdID;
            stdCrs.stdCrsCode = 1000;
            db.stdCourses.Add(stdCrs);
            db.SaveChanges();
            cmboxCourse.Text = cmboxStudent.Text = string.Empty;
            cmboxStudent.Items.Clear();
            cmboxCourse.Items.Clear();
            cmboxCourseSearch.Items.Clear();
            fillComboboxCrs();
            fillComboboxStd();
            fillDataGridView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(txtID.Text);
            string FullName = "";
            string fName = "";
            string lName = "";
            if (cmboxStudent.SelectedItem != null)
            {
                FullName = cmboxStudent.SelectedItem.ToString();
                int pos = FullName.IndexOf(" ");
                fName = FullName.Substring(0, pos);
                lName = FullName.Substring(pos+1);
            }
            Student std = (from obj in db.Students
                                     where obj.fName == fName && obj.lName == lName
                           select obj).Distinct().FirstOrDefault();
            string crsName = cmboxCourse.SelectedItem.ToString();
            Course crs = (from obj in db.Courses
                          where obj.crsName == crsName
                          select obj).Distinct().FirstOrDefault();
            stdCourse stdCrs = (from obj in db.stdCourses
                                     where obj.ID == Id
                                     select obj).FirstOrDefault();
            stdCrs.crsID = Convert.ToInt32(crs.crsID);
            stdCrs.stdID = Convert.ToInt32(std.stdID);
            stdCrs.stdCrsCode = 1000;
            db.SaveChanges();
            txtID.Text = cmboxCourse.Text = cmboxStudent.Text = string.Empty;
            cmboxStudent.Items.Clear();
            cmboxCourse.Items.Clear();
            cmboxCourseSearch.Items.Clear();
            fillDataGridView();
            fillComboboxCrs();
            fillComboboxStd();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            txtID.Text = selectedRow.Cells[0].Value.ToString();
            if (selectedRow.Cells[1].Value != null)
            {
                //int crsID = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
                //Course crs = (from obj in db.Courses
                //                         where obj.crsID == crsID
                //                         select obj).Distinct().FirstOrDefault();
                cmboxCourse.Text = selectedRow.Cells[1].Value.ToString();
            }
            if (selectedRow.Cells[2].Value != null)
            {
                //int instID = Convert.ToInt32(selectedRow.Cells[1].Value.ToString());
                //Instructor instructor = (from obj in db.Instructors
                //                         where obj.instID == instID
                //                         select obj).Distinct().FirstOrDefault();
                cmboxStudent.Text = selectedRow.Cells[2].Value.ToString() + " " + selectedRow.Cells[3].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(txtID.Text);
            //string FullName = "";
            //string fName = "";
            //if (cmboxStudent.SelectedItem != null)
            //{
            //    FullName = cmboxStudent.SelectedItem.ToString();
            //    int pos = FullName.IndexOf(" ");
            //    fName = FullName.Substring(0, pos);
            //}
            //Instructor instructor = (from obj in db.Instructors
            //                         where obj.fName == fName
            //                         select obj).Distinct().FirstOrDefault();
            //string crsName = cmboxCourse.SelectedItem.ToString();
            //Course crs = (from obj in db.Courses
            //              where obj.crsName == crsName
            //              select obj).Distinct().FirstOrDefault();
            stdCourse stdCrs = (from obj in db.stdCourses
                                     where obj.ID == Id
                                     select obj).FirstOrDefault();
            db.stdCourses.Remove(stdCrs);
            db.SaveChanges();
            txtID.Text = cmboxCourse.Text = cmboxStudent.Text = string.Empty;
            cmboxStudent.Items.Clear();
            cmboxCourse.Items.Clear();
            cmboxCourseSearch.Items.Clear();
            fillDataGridView();
            fillComboboxCrs();
            fillComboboxStd();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtID.Text = cmboxCourse.Text = cmboxStudent.Text = string.Empty;
            cmboxStudent.Items.Clear();
            cmboxCourse.Items.Clear();
            cmboxCourseSearch.Items.Clear();
            fillDataGridView();
            fillComboboxCrs();
            fillComboboxStd();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string crsName = cmboxCourseSearch.SelectedItem.ToString();
            Course crs = (from obj in db.Courses
                          where obj.crsName == crsName
                          select obj).Distinct().FirstOrDefault();
            var stdCrs = (from obj in db.stdCourses
                           join obj2 in db.Courses
                           on obj.crsID equals obj2.crsID
                           join obj3 in db.Students
                           on obj.stdID equals obj3.stdID
                           where obj.crsID == crs.crsID
                           select new
                           {
                               obj.ID,
                               obj2.crsName,
                               obj3.fName,
                               obj3.lName
                           }).ToList();
            dataGridView1.DataSource = stdCrs;
        }
    }
    
}

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
    public partial class ManageInstructorCourse : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public ManageInstructorCourse()
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
            var instCrs = (from obj in db.instCourses
                           join obj2 in db.Courses
                           on obj.crsID equals obj2.crsID
                           join obj3 in db.Instructors
                           on obj.instID equals obj3.instID
                           select new
                           {
                               obj.ID,
                               obj2.crsName,
                               obj3.fName,
                               obj3.lName
                           }).ToList();
            dataGridView1.DataSource = instCrs;
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
        public void fillComboboxInst()
        {
            var qInstructors = (from obj in db.Instructors
                                select new
                                {
                                    obj.instID,
                                    obj.fName,
                                    obj.lName
                                }).ToList();
            foreach (var item in qInstructors)
            {
                cmboxInstructor.Items.Add(item.fName + " " + item.lName);
            }
        }
        private void ManageInstructorCourse_Load(object sender, EventArgs e)
        {
            fillDataGridView();
            fillComboboxCrs();
            fillComboboxInst();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string FullName = "";
            string fName = "";
            string lName = "";
            if (cmboxInstructor.SelectedItem != null)
            {
                FullName = cmboxInstructor.SelectedItem.ToString();
                int pos = FullName.IndexOf(" ");
                fName = FullName.Substring(0, pos);
                lName = FullName.Substring(pos+1);
            }
            var instructor = (from obj in db.Instructors
                                     where obj.fName == fName && obj.lName == lName
                                     select new { 
                                         obj.instID
                                        
                                     }).FirstOrDefault();
            string crsName = cmboxCourse.SelectedItem.ToString();
            Course crs = (from obj in db.Courses
                          where obj.crsName == crsName
                          select obj).Distinct().FirstOrDefault();
            instCourse instCourse = new instCourse();
            instCourse.crsID = crs.crsID;
            instCourse.instID = instructor.instID;
            instCourse.insCrsCode = 1000;
            db.instCourses.Add(instCourse);
            db.SaveChanges();
            cmboxCourse.Text = cmboxInstructor.Text = string.Empty;
            cmboxInstructor.Items.Clear();
            cmboxCourse.Items.Clear();
            cmboxCourseSearch.Items.Clear();
            fillComboboxCrs();
            fillComboboxInst();
            fillDataGridView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(txtID.Text);
            string FullName = "";
            string fName = "";
            string lName = "";
            if (cmboxInstructor.SelectedItem != null)
            {
                FullName = cmboxInstructor.SelectedItem.ToString();
                int pos = FullName.IndexOf(" ");
                fName = FullName.Substring(0, pos);
                lName = FullName.Substring(pos+1);
            }
            Instructor instructor = (from obj in db.Instructors
                                     where obj.fName == fName && obj.lName == lName
                                     select obj).Distinct().FirstOrDefault();
            string crsName = cmboxCourse.SelectedItem.ToString();
            Course crs = (from obj in db.Courses
                          where obj.crsName == crsName
                          select obj).Distinct().FirstOrDefault();
            instCourse instCourse = (from obj in db.instCourses
                                     where obj.ID == Id
                                     select obj).FirstOrDefault();
            instCourse.crsID = Convert.ToInt32(crs.crsID);
            instCourse.instID = Convert.ToInt32(instructor.instID);
            instCourse.insCrsCode = 1000;
            db.SaveChanges();
            txtID.Text = cmboxCourse.Text = cmboxInstructor.Text = string.Empty;
            cmboxInstructor.Items.Clear();
            cmboxCourse.Items.Clear();
            cmboxCourseSearch.Items.Clear();
            fillDataGridView();
            fillComboboxCrs();
            fillComboboxInst();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(txtID.Text);
            //string FullName = "";
            //string fName = "";
            //if (cmboxInstructor.SelectedItem != null)
            //{
            //    FullName = cmboxInstructor.SelectedItem.ToString();
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
            instCourse instCourse = (from obj in db.instCourses
                                     where obj.ID == Id
                                     select obj).FirstOrDefault();
            db.instCourses.Remove(instCourse);
            db.SaveChanges();
            txtID.Text = cmboxCourse.Text = cmboxInstructor.Text = string.Empty;
            cmboxInstructor.Items.Clear();
            cmboxCourse.Items.Clear();
            cmboxCourseSearch.Items.Clear();
            fillDataGridView();
            fillComboboxCrs();
            fillComboboxInst();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtID.Text = cmboxCourse.Text = cmboxInstructor.Text = string.Empty;
            cmboxInstructor.Items.Clear();
            cmboxCourse.Items.Clear();
            cmboxCourseSearch.Items.Clear();
            fillDataGridView();
            fillComboboxCrs();
            fillComboboxInst();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string crsName = cmboxCourseSearch.SelectedItem.ToString();
            Course crs = (from obj in db.Courses
                          where obj.crsName == crsName
                          select obj).Distinct().FirstOrDefault();
            var instCrs = (from obj in db.instCourses
                           join obj2 in db.Courses
                           on obj.crsID equals obj2.crsID
                           join obj3 in db.Instructors
                           on obj.instID equals obj3.instID
                           where obj.crsID == crs.crsID
                           select new
                           {
                               obj.ID,
                               obj2.crsName,
                               obj3.fName,
                               obj3.lName
                           }).ToList();
            dataGridView1.DataSource = instCrs;
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
                cmboxInstructor.Text = selectedRow.Cells[2].Value.ToString() + " " + selectedRow.Cells[3].Value.ToString();
            }
        }
    }
}

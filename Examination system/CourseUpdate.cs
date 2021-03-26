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
    public partial class CourseUpdate : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public CourseUpdate()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            CenterToScreen();
            base.OnLoad(e);
        }
        private void CourseUpdate_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'examination_systemDataSet1.SP_AllCrsSelect' table. You can move, or remove it, as needed.
            //this.sP_AllCrsSelectTableAdapter.Fill(this.examination_systemDataSet1.SP_AllCrsSelect);
            var qCourses = (from obj in db.Courses
                            select new
                            {
                                obj.crsID,
                                obj.crsName,
                                obj.duration
                            }).ToList();
            foreach (var item in qCourses)
            {
                comboBox1.Items.Add(item.crsName);
                //comboBox1.ValueMember = item.crsID.ToString();
            }
            //comboBox1.DataSource = qCourses;
            //comboBox1.DisplayMember = "crsName";
            //comboBox1.ValueMember = "crsID";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            comboBox1.SelectedIndex = 0;
            txtCourseDuration.Text = txtCourseName.Text = string.Empty;
            CorseAdd corseAdd = new CorseAdd();
            corseAdd.Show();
            this.Close();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if ((int)comboBox1.SelectedValue != -1)
            //{
               // int courseID = (int)comboBox1.SelectedValue;
                string crsName = comboBox1.SelectedItem.ToString();
                Course course = (from obj in db.Courses
                                 where obj.crsName == crsName
                                 select obj).Distinct().FirstOrDefault();
                db.SP_DeleteCrsById(course.crsID);
                db.SaveChanges();
                comboBox1.SelectedIndex = 0;
                txtCourseDuration.Text = txtCourseName.Text = string.Empty;
                MessageBox.Show("Course Deleted !!!");
                // this.ParentForm.Refresh();
                
            //}
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //if ((int)comboBox1.SelectedValue != -1)
            //{
            //int courseID = (int)comboBox1.SelectedValue;
            string crsName = comboBox1.SelectedItem.ToString();
            string courseName = txtCourseName.Text;
                string courseDuration = txtCourseDuration.Text;
                Course course = (from obj in db.Courses
                                 where obj.crsName == crsName
                                 select obj).Distinct().FirstOrDefault();
                course.crsName = courseName;
                course.duration = courseDuration;
                db.SaveChanges();
                //db.SP_CrsUpdate(courseID, courseName,courseDuration);
                db.SaveChanges();
            //}
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            
            //if ((int)comboBox1.SelectedValue != -1)
            //{
                comboBox1.Items.Clear();
                //comboBox1.Items.Clear();
                //string crsName = comboBox1.SelectedItem.ToString();
            //MessageBox.Show(courseID.ToString());
            var qCourses = (from obj in db.Courses
                            select new
                            {
                                obj.crsID,
                                obj.crsName,
                                obj.duration
                            }).ToList();
            foreach (var item in qCourses)
            {
                comboBox1.Items.Add(item.crsName);
                //comboBox1.ValueMember = item.crsID.ToString();
            }
            // }

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int courseID = (int)comboBox1.SelectedValue;
            string crsName = comboBox1.SelectedItem.ToString();
            //MessageBox.Show(courseID.ToString());
            Course course = (from obj in db.Courses
                             where obj.crsName == crsName
                             select obj).Distinct().FirstOrDefault();
            txtCourseName.Text = course.crsName;
            txtCourseDuration.Text = course.duration;
        }
    }
}

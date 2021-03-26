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
    public partial class CourseShow : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public CourseShow()
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
            dataGridView1.DataSource = db.SP_AllCrsSelect();
        }
        private void CourseShow_Load(object sender, EventArgs e)
        {
            fillDataGridView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.SP_CrsSelectByName(txtSearch.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Form1 form1 = new Form1();
            //form1.Show();
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            txtCrsID.Text = selectedRow.Cells[0].Value.ToString();
            txtCrsName.Text = selectedRow.Cells[1].Value.ToString();
            txtCrsDuration.Text = selectedRow.Cells[2].Value.ToString();

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Course crs = new Course();
            crs.crsName = txtCrsName.Text;
            crs.duration = txtCrsDuration.Text;
            db.Courses.Add(crs);
            db.SaveChanges();
            txtCrsName.Text = txtCrsDuration.Text = txtCrsID.Text = string.Empty;
            MessageBox.Show("Course Saved Correctlly");
            fillDataGridView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int crsID = Convert.ToInt32(txtCrsID.Text);
            string courseName = txtCrsName.Text;
            string courseDuration = txtCrsDuration.Text;
            Course course = (from obj in db.Courses
                             where obj.crsID == crsID
                             select obj).Distinct().FirstOrDefault();
            course.crsName = courseName;
            course.duration = courseDuration;
            db.SaveChanges();
            //db.SP_CrsUpdate(courseID, courseName,courseDuration);
            db.SaveChanges();
            txtCrsName.Text = txtCrsDuration.Text = txtCrsID.Text = string.Empty;
            MessageBox.Show("Course Updated Correctlly");
            fillDataGridView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int crsID = Convert.ToInt32(txtCrsID.Text);
            Course course = (from obj in db.Courses
                             where obj.crsID == crsID
                             select obj).Distinct().FirstOrDefault();
            db.SP_DeleteCrsById(course.crsID);
            db.SaveChanges();
            txtCrsName.Text = txtCrsDuration.Text = txtCrsID.Text = string.Empty;
            MessageBox.Show("Course Deleted !!!");
            fillDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtCrsName.Text = txtCrsDuration.Text = txtCrsID.Text = string.Empty;
            fillDataGridView();
        }

       
    }
}

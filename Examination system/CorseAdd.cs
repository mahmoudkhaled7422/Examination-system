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
    public partial class CorseAdd : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public CorseAdd()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //db.SP_CrsInsert(txtCourseName.Text, txtCourseDuration.Text);
            Course crs = new Course();
            crs.crsName = txtCourseName.Text;
            crs.duration = txtCourseDuration.Text;
            db.Courses.Add(crs);
            db.SaveChanges();
            MessageBox.Show("Course Saved Correctlly");

        }



        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CourseShow courseShow = new CourseShow();
            courseShow.Show();
            this.Close();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CourseUpdate courseUpdate = new CourseUpdate();
            courseUpdate.Show();
            this.Close();
        }
    }
}

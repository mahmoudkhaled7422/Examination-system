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
    public partial class Index : Form
    {
        public Index()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            CenterToScreen();
            base.OnLoad(e);
        }
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("aaaaaaaaaaaa");
            //CorseAdd form2 = new CorseAdd();
            CourseShow courseShow = new CourseShow();
            courseShow.Show();
        }

        private void courseTopicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manageCourseTopic topic = new manageCourseTopic();
            topic.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDepartment dep = new ManageDepartment();
            dep.Show();
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageInstructor inst = new ManageInstructor();
            inst.Show();
        }

        private void qustionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MangeQuestion que = new MangeQuestion();
            que.Show();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MangeStudent std = new MangeStudent();
            std.Show();
        }

        private void generateExamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateExam generateExam = new GenerateExam();
            generateExam.Show();
        }
    }
}

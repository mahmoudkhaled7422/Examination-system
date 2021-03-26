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
    public partial class GenerateExam : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public GenerateExam()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            CenterToScreen();
            base.OnLoad(e);
        }
        public void fillCombobox()
        {
            var qCourses = (from obj in db.Courses
                           select new
                           {
                               obj.crsID,
                               obj.crsName,
                               obj.duration
                           }).ToList();
            foreach (var item in qCourses)
            {
                cmboxCourse.Items.Add(item.crsName);
            }
        }
        private void GenerateExam_Load(object sender, EventArgs e)
        {
            fillCombobox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string crsName = cmboxCourse.SelectedItem.ToString();
            var ex = (from obj in db.Exams
                      join obj1 in db.Questions
                      on obj.quesID equals obj1.questionID
                      join obj2 in db.Courses
                      on obj1.crsID equals obj2.crsID
                      where obj2.crsName == crsName
                      select new
                      {
                          obj.examName
                      }).Distinct().FirstOrDefault();
            if(ex == null)
            {
                //MessageBox.Show("exam not found");
                db.ExamGenerations(crsName);
                db.SaveChanges();
                MessageBox.Show("Exam Generated Succefully");
            }
            else
            {
                MessageBox.Show("This course had exam");

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

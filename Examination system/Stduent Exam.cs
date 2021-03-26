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
    public partial class Stduent_Exam : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public int StdID;
        public Stduent_Exam(int _stdID)
        {
            StdID = _stdID;
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            CenterToScreen();
            base.OnLoad(e);
        }
        public void fillDataGridView(int crsID )
        {
            var exam = (from obj in db.Exams
                        join obj2 in db.Questions
                        on obj.quesID equals obj2.questionID
                        where obj2.crsID == crsID
                        select new
                        {
                            obj2.question1
                        }).ToList();
            dataGridView1.DataSource = exam;
            //var stdques = (from obj in db.stdExamQuestions
            //               join obj2 in db.Questions
            //               on obj.quesID equals obj2.questionID
            //               where obj.stdAnswer == null
            //               select new
            //               {
            //                   obj2.question1
            //               }).ToList();
            //dataGridView1.DataSource = stdques;

        }
        public void FillComboboxCourse()
        {
            var stds = (from obj in db.Students
                        join obj1 in db.stdCourses
                        on obj.stdID equals obj1.stdID
                        join obj2 in db.Courses
                        on obj1.crsID equals obj2.crsID
                        where obj.stdID == StdID
                        select new
                        {
                            obj.fName,
                            obj.lName,
                            obj2.crsName
                        }).ToList();
            if (stds != null)
            {
                foreach (var item in stds)
                {
                    cmboxCourse.Items.Add(item.crsName);
                }
            }
            else
            {
                MessageBox.Show("You don`t have courses to exam");
            }
            
        }
        private void Stduent_Exam_Load(object sender, EventArgs e)
        {
            Student stds = (from obj in db.Students
                            where obj.stdID == StdID
                            select obj).FirstOrDefault();
            lblStudentData.Text = "Hello " + stds.fName + " " + stds.lName + ", This your exam page";
           // fillDataGridView();
            FillComboboxCourse();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            string ques = selectedRow.Cells[0].Value.ToString();
            lblQues.Text = ques;
            cmboxAnswers.Text = "";
            cmboxAnswers.Items.Clear();
            var Ans = (from obj in db.Questions
                       join obj2 in db.Choices
                       on obj.questionID equals obj2.quesID
                       where obj.question1 == ques
                       select new
                       {
                           obj2.choice1
                       }).ToList();
            foreach (var item in Ans)
            {
                cmboxAnswers.Items.Add(item.choice1);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string question = lblQues.Text;
            int qId = (from Q in db.Questions
                       where Q.question1 == question
                       select Q.questionID).Distinct().FirstOrDefault();
            Exam ex = (from E in db.Exams
                       where E.quesID == qId
                       select E).Distinct().FirstOrDefault();
            
            db.ExamAnswerss(StdID, ex.examNumber, cmboxAnswers.SelectedItem.ToString(), qId);
            
            db.SaveChanges();
            db.examCorrections(StdID, ex.examNumber, qId);
            db.SaveChanges();
            MessageBox.Show("your Answer saved successfuly");
         
     
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            string question = lblQues.Text;
            int qId = (from Q in db.Questions
                       where Q.question1 == question
                       select Q.questionID).Distinct().FirstOrDefault();
            Exam ex = (from E in db.Exams
                       where E.quesID == qId
                       select E).Distinct().FirstOrDefault();
            Student stds = (from obj in db.Students
                        where obj.stdID == StdID
                        select obj).FirstOrDefault();
            var stdques = (from obj in db.stdExamQuestions
                           join obj2 in db.Questions
                           on obj.quesID equals obj2.questionID
                           where obj.stdID == StdID && obj.examNumber == ex.examNumber
                           select new
                           {
                               obj.stdGrade
                           }).ToList();
            int sum = 0;
            foreach (var item in stdques)
            {
                sum += Convert.ToInt32(item.stdGrade);
            }
            var quesDegree = (from obj in db.Questions
                             join obj1 in db.Exams
                             on obj.questionID equals obj1.quesID
                             where obj1.examNumber == ex.examNumber
                             select new
                             {
                                 obj.grade
                             }).ToList();
            int totalDegrees = 0;
            foreach (var item in quesDegree)
            {
                totalDegrees += Convert.ToInt32(item.grade);
            }
            if(sum >= sum / 2)
            {
                MessageBox.Show("Hello " + stds.fName + " " + stds.lName + " you passed and\n Your total degree is " + sum.ToString() +" of "+totalDegrees.ToString());
            }
            else
            {
                MessageBox.Show("Hello " + stds.fName + " " + stds.lName + " you failed and\n Your total degree is " + sum.ToString() + " of " + totalDegrees.ToString());
            }
            
        }

        private void cmboxCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            string crs = cmboxCourse.SelectedItem.ToString();
            var crsID = (from obj in db.Courses
                        where obj.crsName == crs
                        select new
                        {
                            obj.crsID
                        }).FirstOrDefault();
            fillDataGridView(Convert.ToInt32(crsID.crsID));
        }
    }
}

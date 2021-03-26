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
    public partial class MangeQuestion : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public MangeQuestion()
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
            var questions = (from obj in db.Questions
                             join obj2 in db.Courses
                             on obj.crsID equals obj2.crsID
                             join obj3 in db.QuesTypes
                             on obj.questionID equals obj3.quesID
                             select new
                             {
                                 obj.questionID,
                                 obj.question1,
                                 obj3.type,
                                 obj.grade,
                                 obj2.crsID,
                                 obj.correctChoice,

                             }).ToList();
            dataGridView1.DataSource = questions;
        }
        public void fillCombobox()
        {
            var course = (from obj in db.Courses
                          select new
                          {
                              obj.crsID,
                              obj.crsName
                          }).ToList();
            foreach (var item in course)
            {
                cmboxcrs.Items.Add(item.crsName);
            }
        }

        private void MangeQuestion_Load(object sender, EventArgs e)
        {
            fillDataGridView();
            fillCombobox();
            fillcourseType();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string crsname = cmboxcrs.SelectedItem.ToString();
            Course crs = (from obj in db.Courses
                          where obj.crsName == crsname
                          select obj).Distinct().FirstOrDefault();
            Question ques = new Question();
            //
            QuesType quest = new QuesType();
            ques.question1 = txtQues.Text;
            //
            quest.type = comboBoxType.Text;
            ques.grade = int.Parse(txtGrade.Text);
            ques.crsID = crs.crsID;
            ques.correctChoice = textCorrrectChoice.Text;
            db.Questions.Add(ques);
            db.QuesTypes.Add(quest);
            db.SaveChanges();
            txtGrade.Text = txtQues.Text = txtQuesD.Text = cmboxcrs.Text=comboBoxType.Text = textCorrrectChoice.Text = string.Empty;
            cmboxcrs.Items.Clear();
            comboBoxType.Items.Clear();
            fillDataGridView();
            fillCombobox();
            fillcourseType();
            MessageBox.Show("Question Added Successfully");
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string crsname = cmboxcrs.SelectedItem.ToString();
            //
            string qtype = comboBoxType.SelectedItem.ToString();

            int qid = Convert.ToInt32(txtQuesD.Text);
            Course crs = (from obj in db.Courses
                          where obj.crsName == crsname
                          select obj).Distinct().FirstOrDefault();
            //
            
            Question ques = (from obj in db.Questions
                             where obj.questionID == qid
                             select obj).FirstOrDefault();
            ques.question1 = txtQues.Text;
            //
            
            ques.grade = int.Parse(txtGrade.Text);
            ques.crsID = crs.crsID;
            ques.correctChoice = textCorrrectChoice.Text;
            db.SaveChanges();
            QuesType questype = (from obj in db.QuesTypes
                                 where obj.quesID == qid
                                 select obj).Distinct().FirstOrDefault();
            questype.type = comboBoxType.SelectedItem.ToString();
            db.SaveChanges();
            txtGrade.Text = txtQues.Text = txtQuesD.Text = cmboxcrs.Text = textCorrrectChoice.Text = comboBoxType.Text = string.Empty;
            cmboxcrs.Items.Clear();
            comboBoxType.Items.Clear();
            fillDataGridView();
            fillCombobox();
            fillcourseType();
            MessageBox.Show("Question Updated Successfully");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int qid = Convert.ToInt32(txtQuesD.Text);
            Question ques = (from obj in db.Questions
                             where obj.questionID == qid
                             select obj).FirstOrDefault();
            try
            {
                db.deletequestion(ques.questionID);
            }
            catch (Exception) { }

            txtGrade.Text = txtQues.Text = txtQuesD.Text = cmboxcrs.Text = textCorrrectChoice.Text=comboBoxType.Text = string.Empty;
            cmboxcrs.Items.Clear();
            comboBoxType.Items.Clear();
            fillDataGridView();
            fillCombobox();
            fillcourseType();
            MessageBox.Show("Question deleted Successfully");
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            txtQuesD.Text = selectedRow.Cells[0].Value.ToString();
            txtQues.Text = selectedRow.Cells[1].Value.ToString();
            txtGrade.Text = selectedRow.Cells[3].Value.ToString();
            textCorrrectChoice.Text = selectedRow.Cells[5].Value.ToString();

            int crsid = Convert.ToInt32(selectedRow.Cells[4].Value.ToString());
            Course crs = (from obj in db.Courses
                          where obj.crsID == crsid
                          select obj).Distinct().FirstOrDefault();
            cmboxcrs.Text = crs.crsName;

           comboBoxType.Text =selectedRow.Cells[2].Value.ToString();
 

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var questions = (from obj in db.Questions
                             join obj2 in db.Courses
                             on obj.crsID equals obj2.crsID
                             join obj3 in db.QuesTypes
                             on obj.questionID equals obj3.quesID
                             where obj.question1==txtSearch.Text
                             select new
                             {
                                 obj.questionID,
                                 obj.question1,
                                 obj3.type,
                                 obj.grade,
                                 obj2.crsID,
                                 obj.correctChoice,

                             }).ToList();
            dataGridView1.DataSource = questions;

        }


        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }



       
        public void fillcourseType()
        {
            List<string> questype = new List<string>() { "MCQ", "T OR F" };
            foreach (var item in questype)
            {
                comboBoxType.Items.Add(item);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtGrade.Text = txtQues.Text = txtQuesD.Text = cmboxcrs.Text = textCorrrectChoice.Text = comboBoxType.Text = string.Empty;
            cmboxcrs.Items.Clear();
            comboBoxType.Items.Clear();
            fillDataGridView();
            fillCombobox();
            fillcourseType();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManageChoice manageChoice = new ManageChoice();
            manageChoice.Show();
        }
    }
}

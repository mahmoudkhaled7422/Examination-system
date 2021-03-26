using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examination_system
{
    public partial class ManageChoice : Form
    {
        Examination_systemEntities entities;
        public ManageChoice()
        {
            entities = new Examination_systemEntities();
            InitializeComponent();
        }
      
        private void btnInsert_Click_1(object sender, EventArgs e)
        {
            if (txtChoiceName.Text != string.Empty && cmboxQuestions.Text != string.Empty)
            {
                string questionName = cmboxQuestions.SelectedItem.ToString();
                Question question = (from obj in entities.Questions
                where obj.question1 == questionName
                select obj).Distinct().FirstOrDefault();
                Choice c1 = new Choice();
                c1.choice1 = txtChoiceName.Text;
                c1.quesID = question.questionID;
                entities.Choices.Add(c1);
                entities.SaveChanges();
                MessageBox.Show("Saved");
                cmboxQuestions.Text = txtChoiceID.Text =
                txtChoiceName.Text = string.Empty;
                cmboxQuestions.Items.Clear();
                fillCombobox();
                fillGrid();
            }
            else MessageBox.Show("Choose your Answer plz");
        }

       
        public void fillCombobox()
        {
            var qQuestions = (from obj in entities.Questions
                                select new
                                {
                                    obj.question1
                                    
                                }).ToList();
            foreach (var item in qQuestions)
            {
                cmboxQuestions.Items.Add(item.question1);
            }
        }
        public void fillGrid() {
            List<string>choisez = (from obj1 in entities.Choices
                              select obj1.choice1
                              ).ToList();
            var qAnswers = (from obj in entities.Choices
                            select new
                            {
                                obj.choiceID,
                                obj.choice1,
                                obj.Question.question1
                            }).ToList();

          
            qGridView1.DataSource = qAnswers;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtChoiceName.Text != string.Empty && cmboxQuestions.Text != string.Empty)
            {
                string questionName = cmboxQuestions.SelectedItem.ToString();
                int choiceId = Convert.ToInt32(txtChoiceID.Text);
                Question question = (from obj in entities.Questions
                                     where obj.question1 == questionName
                                     select obj).Distinct().FirstOrDefault();
                Choice c = (from instance in entities.Choices
                            where instance.choiceID == choiceId
                            select instance).Distinct().FirstOrDefault();
                try
                {
                    entities.Choices.Remove(c);
                    entities.SaveChanges();
                    MessageBox.Show("Deleted");

                }
                catch (ArgumentNullException )
                { MessageBox.Show("Answer not found");}
                cmboxQuestions.Text = txtChoiceID.Text =
                txtChoiceName.Text = string.Empty;
                cmboxQuestions.Items.Clear();
                fillCombobox();
                fillGrid();
            }
            else MessageBox.Show("Choose your option plz");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmboxQuestions.Text != string.Empty)
            {
                string questionName = cmboxQuestions.SelectedItem.ToString();
                int choiceID = Convert.ToInt32(txtChoiceID.Text);
                Question question = (from obj in entities.Questions
                                     where obj.question1 == questionName
                                     select obj).Distinct().FirstOrDefault();
                Choice c = (from instance in entities.Choices
                            where instance.choiceID == choiceID
                            select instance).FirstOrDefault();

                try
                {
                    c.choice1 = txtChoiceName.Text;
                    entities.SaveChanges();
                    MessageBox.Show("updated");
                }
                catch (Exception)
                { MessageBox.Show("Answer not found"); }
                txtChoiceID.Text = cmboxQuestions.Text = txtChoiceName.Text = string.Empty;
                cmboxQuestions.Items.Clear();
                fillCombobox();
                fillGrid();
            }
            else { MessageBox.Show("choose question first "); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fillGrid();

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<string> choisez = (from obj1 in entities.Choices
                                    select obj1.choice1
                              ).ToList();
            var qAnswers = (from obj in entities.Choices
                            where obj.choice1==txtcSearch.Text
                            select new
                            {
                                obj.choiceID,
                                obj.choice1,
                                obj.Question.question1
                            }).ToList();


             qGridView1.DataSource = qAnswers;
           // qGridView1.DataSource = choisez.Find(txtcSearch.Text);

        }

        private void qGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedone = qGridView1.Rows[index];
            txtChoiceID.Text = selectedone.Cells[0].Value.ToString();
            txtChoiceName.Text = selectedone.Cells[1].Value.ToString();
            cmboxQuestions.Text = selectedone.Cells[2].Value.ToString();
        }

        private void ManageChoice_Load(object sender, EventArgs e)
        {
            fillGrid();
            fillCombobox();
        
           // base.OnLoad(e);
        
         }
    }
}

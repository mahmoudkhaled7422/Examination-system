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
    public partial class manageCourseTopic : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public manageCourseTopic()
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
            //dataGridView1.DataSource = db.SP_AllTopics();
            var courseTopic = (from obj in db.Topics
                              join obj1 in db.Courses
                              on obj.crsID equals obj1.crsID
                              select new
                              {
                                  obj.topicID,
                                  obj.topicName,
                                  obj1.crsName
                              }).ToList();
            dataGridView1.DataSource = courseTopic;
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
                cmboxCourses.Items.Add(item.crsName);
            }
        }
        private void manageCourseTopic_Load(object sender, EventArgs e)
        {
            fillDataGridView();
            fillCombobox();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string crsName = cmboxCourses.SelectedItem.ToString();
            Course course = (from obj in db.Courses
                             where obj.crsName == crsName
                             select obj).Distinct().FirstOrDefault();

            Topic topic = new Topic();
            topic.topicName = txtTopicName.Text;
            topic.crsID = course.crsID;
            db.Topics.Add(topic);
            db.SaveChanges();
            txtTopicName.Text = cmboxCourses.Text = txtTopicID.Text = string.Empty;
            cmboxCourses.Items.Clear();
            MessageBox.Show("Course Saved Correctlly");
            fillDataGridView();
            fillCombobox();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int topicID = Convert.ToInt32(txtTopicID.Text);
            string crsName = cmboxCourses.SelectedItem.ToString();
            Course course = (from obj in db.Courses
                             where obj.crsName == crsName
                             select obj).Distinct().FirstOrDefault();
            Topic topic = (from obj in db.Topics
                           where obj.topicID == topicID
                           select obj).FirstOrDefault();
            topic.topicName = txtTopicName.Text;
            topic.crsID = course.crsID;
            db.SaveChanges();
            txtTopicName.Text = cmboxCourses.Text = txtTopicID.Text = string.Empty;
            cmboxCourses.Items.Clear();
            MessageBox.Show("Topic Updated Correctlly");
            fillDataGridView();
            fillCombobox();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            txtTopicID.Text = selectedRow.Cells[0].Value.ToString();
            txtTopicName.Text = selectedRow.Cells[1].Value.ToString();
            //int crsID = Convert.ToInt32(selectedRow.Cells[2].Value.ToString());
            //Course course = (from obj in db.Courses
            //                 where obj.crsID == crsID
            //                 select obj).Distinct().FirstOrDefault();
            cmboxCourses.Text = selectedRow.Cells[2].Value.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int topID = Convert.ToInt32(txtTopicID.Text);
            Topic topic = (from obj in db.Topics
                             where obj.topicID == topID
                             select obj).Distinct().FirstOrDefault();
            db.SP_DeleteTopicById(topic.topicID);
            db.SaveChanges();
            txtTopicID.Text = txtTopicName.Text = cmboxCourses.Text = string.Empty;
            MessageBox.Show("Topic Deleted !!!");
            fillDataGridView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.SP_TopSelectByName(txtSearch.Text);
            var courseTopic = (from obj in db.Topics
                               join obj1 in db.Courses
                               on obj.crsID equals obj1.crsID
                               where obj.topicName == txtSearch.Text
                               select new
                               {
                                   obj.topicID,
                                   obj.topicName,
                                   obj1.crsName
                               }).ToList();
            dataGridView1.DataSource = courseTopic;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtTopicName.Text = cmboxCourses.Text = txtTopicID.Text = string.Empty;
            cmboxCourses.Items.Clear();
            fillDataGridView();
            fillCombobox();

        }
    }
}

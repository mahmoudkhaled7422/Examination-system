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
    public partial class Login : Form
    {
        Examination_systemEntities db = new Examination_systemEntities();
        public Login()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            CenterToScreen();
            base.OnLoad(e);
        }
        private void txtCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Student student = (from obj in db.Students
                               where obj.stdUserName.ToLower() == txtUserName.Text.ToLower() && obj.stdPassword == txtPassword.Text
                               select obj).FirstOrDefault();
            Instructor inst = (from obj in db.Instructors
                               where obj.instUserName.ToLower() == txtUserName.Text.ToLower() && obj.instPassword == txtPassword.Text
                               select obj).FirstOrDefault();
            if (student != null)
            {
                //MessageBox.Show("Your User Name is " + student.stdUserName);
                this.Hide();
                Stduent_Exam student_exam = new Stduent_Exam(student.stdID);
                student_exam.Closed += (s, args) => this.Close();
                student_exam.Show();
            }
            else if (inst != null)
            {
                //MessageBox.Show("Your User Name is " + inst.instUserName);
                this.Hide();
                Index form1 = new Index();
                form1.Closed += (s, args) => this.Close();
                form1.Show();
            }
            else
            {
                MessageBox.Show("Invalid User Name and Password");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

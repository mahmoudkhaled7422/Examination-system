
namespace Examination_system
{
    partial class Stduent_Exam
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stduent_Exam));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCalc = new System.Windows.Forms.Button();
            this.lblQues = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboxAnswers = new System.Windows.Forms.ComboBox();
            this.lblStudentData = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboxCourse = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(646, 34);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(160, 262);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnSave.Location = new System.Drawing.Point(81, 308);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(268, 53);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save Answers";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCalc
            // 
            this.btnCalc.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCalc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalc.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnCalc.Location = new System.Drawing.Point(374, 309);
            this.btnCalc.Margin = new System.Windows.Forms.Padding(2);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(268, 52);
            this.btnCalc.TabIndex = 2;
            this.btnCalc.Text = "Calculate Degree";
            this.btnCalc.UseVisualStyleBackColor = false;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // lblQues
            // 
            this.lblQues.AutoSize = true;
            this.lblQues.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblQues.Location = new System.Drawing.Point(11, 169);
            this.lblQues.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQues.Name = "lblQues";
            this.lblQues.Size = new System.Drawing.Size(0, 17);
            this.lblQues.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(12, 227);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Answers";
            // 
            // cmboxAnswers
            // 
            this.cmboxAnswers.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cmboxAnswers.FormattingEnabled = true;
            this.cmboxAnswers.Location = new System.Drawing.Point(81, 227);
            this.cmboxAnswers.Margin = new System.Windows.Forms.Padding(2);
            this.cmboxAnswers.Name = "cmboxAnswers";
            this.cmboxAnswers.Size = new System.Drawing.Size(234, 27);
            this.cmboxAnswers.TabIndex = 5;
            // 
            // lblStudentData
            // 
            this.lblStudentData.AutoSize = true;
            this.lblStudentData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentData.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblStudentData.Location = new System.Drawing.Point(11, 22);
            this.lblStudentData.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStudentData.Name = "lblStudentData";
            this.lblStudentData.Size = new System.Drawing.Size(0, 20);
            this.lblStudentData.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(12, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Course";
            // 
            // cmboxCourse
            // 
            this.cmboxCourse.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cmboxCourse.FormattingEnabled = true;
            this.cmboxCourse.Location = new System.Drawing.Point(81, 110);
            this.cmboxCourse.Margin = new System.Windows.Forms.Padding(2);
            this.cmboxCourse.Name = "cmboxCourse";
            this.cmboxCourse.Size = new System.Drawing.Size(234, 27);
            this.cmboxCourse.TabIndex = 5;
            this.cmboxCourse.SelectedIndexChanged += new System.EventHandler(this.cmboxCourse_SelectedIndexChanged);
            // 
            // Stduent_Exam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(807, 397);
            this.Controls.Add(this.lblStudentData);
            this.Controls.Add(this.cmboxCourse);
            this.Controls.Add(this.cmboxAnswers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblQues);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Stduent_Exam";
            this.Text = "Stduent_Exam";
            this.Load += new System.EventHandler(this.Stduent_Exam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Label lblQues;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboxAnswers;
        private System.Windows.Forms.Label lblStudentData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboxCourse;
    }
}
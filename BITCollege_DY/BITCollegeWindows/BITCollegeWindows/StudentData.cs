using BITCollege_DY.Data;
using BITCollege_DY.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BITCollegeWindows
{
    public partial class StudentData : Form
    {
        ///Given: Student and Registration data will be retrieved
        ///in this form and passed throughout application
        ///These variables will be used to store the current
        ///Student and selected Registration
        ConstructorData constructorData = new ConstructorData();
        BITCollege_DYContext context = new BITCollege_DYContext();


        /// <summary>
        /// This constructor will be used when this form is opened from
        /// the MDI Frame.
        /// </summary>
        public StudentData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  This constructor will be used when returning to StudentData
        /// from another form.  This constructor will pass back
        /// specific information about the student and registration
        /// based on activites taking place in another form.
        /// </summary>
        /// <param name="constructorData">constructorData object containing
        /// specific student and registration data.</param>
        public StudentData (ConstructorData constructor)
        {
            InitializeComponent();

            this.constructorData = constructor;

            studentNumberMaskedTextBox.Text = constructorData.Student.StudentNumber.ToString();

            studentNumberMaskedTextBox_Leave(null, null);
        }

        /// <summary>
        /// given: Open grading form passing constructor data.
        /// </summary>
        private void lnkUpdateGrade_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            PopulateConstructorData();
            Grading grading = new Grading(constructorData);
            grading.MdiParent = this.MdiParent;
            grading.Show();
            this.Close();

        }


        /// <summary>
        /// given: Open history form passing constructor data.
        /// </summary>
        private void lnkViewDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            PopulateConstructorData();
            History history = new History(constructorData);
            history.MdiParent = this.MdiParent;
            history.Show();
            this.Close();

        }

        private void PopulateConstructorData()
        {
            this.constructorData.Student = (Student)studentBindingSource.Current;
            this.constructorData.Registration = (Registration)registrationBindingSource.Current;
        }

        /// <summary>
        /// given:  Opens the form in top right corner of the frame.
        /// </summary>
        private void StudentData_Load(object sender, EventArgs e)
        {
            //keeps location of form static when opened and closed
            this.Location = new Point(0, 0);
        }

        private void studentNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(studentNumberMaskedTextBox.Text) || !studentNumberMaskedTextBox.MaskFull)
            {
                MessageBox.Show("Please enter a valid Student Number.", "Invalid Student Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                studentNumberMaskedTextBox.Focus();
                return;
            }

            var studentNumberText = studentNumberMaskedTextBox.Text;
            if (!long.TryParse(studentNumberText.Replace("-", ""), out long studentNumber))
            {
                MessageBox.Show("Please enter a valid Student Number.", "Invalid Student Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                studentNumberMaskedTextBox.Focus();
                return;
            }

            Student student = context.Students.FirstOrDefault(s => s.StudentNumber == studentNumber);

            if (student == null)
            {
                lnkUpdateGrade.Enabled = false;
                lnkViewDetails.Enabled = false;

                studentBindingSource.DataSource = typeof(Student);
                registrationBindingSource.DataSource = typeof(Registration);

                MessageBox.Show($"Student {studentNumber} does not exist.", "Invalid Student Number", MessageBoxButtons.OK, MessageBoxIcon.Error);

                studentNumberMaskedTextBox.Focus();
                return;
            }

            studentBindingSource.DataSource = student;

            var registrations = context.Registrations
                .Where(r => r.StudentId == student.StudentId)
                .ToList();

            if (registrations.Count == 0)
            {
                lnkUpdateGrade.Enabled = false;
                lnkViewDetails.Enabled = false;
                registrationBindingSource.DataSource = typeof(Registration);
            }
            else
            {
                registrationBindingSource.DataSource = registrations;

                lnkUpdateGrade.Enabled = true;
                lnkViewDetails.Enabled = true;
            }
            if (constructorData.Registration != null)
            {
                registrationNumberComboBox.Text = constructorData.Registration.RegistrationNumber.ToString();
            }
            else
            {
                registrationNumberComboBox.Text = string.Empty;
            }
        }
    }
}

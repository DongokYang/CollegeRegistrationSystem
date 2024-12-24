using BITCollege_DY.Data;
using BITCollegeWindows.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace BITCollegeWindows
{
    public partial class Grading : Form
    {

        ///given:  student and registration data will passed throughout 
        ///application. This object will be used to store the current
        ///student and selected registration
        ConstructorData constructorData;

        BITCollege_DYContext context = new BITCollege_DYContext();

        /// <summary>
        /// given:  This constructor will be used when called from the
        /// Student form.  This constructor will receive 
        /// specific information about the student and registration
        /// further code required:  
        /// </summary>
        /// <param name="constructorData">constructorData object containing
        /// specific student and registration data.</param>
        public Grading(ConstructorData constructor)
        {
            InitializeComponent();
            this.constructorData = constructor;
            studentBindingSource.DataSource = constructorData.Student;
            registrationBindingSource.DataSource = constructorData.Registration;
        }

        /// <summary>
        /// given: This code will navigate back to the Student form with
        /// the specific student and registration data that launched
        /// this form.
        /// </summary>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //return to student with the data selected for this form
            StudentData student = new StudentData(constructorData);
            student.MdiParent = this.MdiParent;
            student.Show();
            this.Close();
        }

        /// <summary>
        /// given:  Always open in this form in the top right corner of the frame.
        /// further code required:
        /// </summary>
        private void Grading_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);


                string courseType = constructorData.Registration.Course.CourseType;
                string courseMask = Utility.BusinessRules.CourseFormat(courseType);
                courseNumberMaskedTextBox.Mask = courseMask;


            if (constructorData?.Registration?.Grade != null)
            {
                gradeTextBox.Enabled = false;          
                lnkUpdate.Enabled = false;
                lblExisting.Visible = true; 
            }
            else
            {
                gradeTextBox.Enabled = true;           
                lnkUpdate.Enabled = true;
                lblExisting.Visible = false; 
            }

        }

        /// <summary>
        /// Handles the logic for updating a student grade
        /// </summary>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string cleanedInput = Numeric.ClearFormatting(gradeTextBox.Text, "%");
            if (!Numeric.IsNumeric(cleanedInput, NumberStyles.Number))
            {
                MessageBox.Show("Invalid input. Please enter the grade as a decimal value",
                                "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double gradeValue = double.Parse(cleanedInput) / 100;

            if (gradeValue < 0 || gradeValue > 1)
            {
                MessageBox.Show("The grade must be  between 0 and 1 after dividing by 100.",
                                "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var client = new CollegeRegistrationClient())
                {
                    client.UpdateGrade(constructorData.Registration.RegistrationId, gradeValue,null);
                }

                gradeTextBox.Enabled = false;

                MessageBox.Show("Grade updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the grade: {ex.Message}",
                                "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void descriptionLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}

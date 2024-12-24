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
    public partial class History : Form
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
        public History(ConstructorData constructorData)
        {
            InitializeComponent();

            this.constructorData = constructorData;
            studentBindingSource.DataSource = constructorData.Student;
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
        /// given:  Open this form in top right corner of the frame.
        /// further code required:
        /// </summary>
        private void History_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            try
            {
                var query = (from Registration in context.Registrations
                             join Course in context.Courses
                             on Registration.CourseId equals Course.CourseId
                             where Registration.StudentId == constructorData.Student.StudentId
                             select new
                             {
                                 Registration.RegistrationNumber,
                                 Registration.RegistrationDate,
                                 Course = Course.Title,
                                 Registration.Grade,
                                 Registration.Notes
                             }
                             );
                if (query.Count() > 0)
                {
                    registrationBindingSource.DataSource = query.ToList();

                }
                else
                {
                    registrationBindingSource.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
            }
        }

    }
}

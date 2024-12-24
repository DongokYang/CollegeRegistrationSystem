namespace BITCollegeWindows
{
    partial class StudentData
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label studentNumberLabel;
            System.Windows.Forms.Label dateCreatedLabel;
            System.Windows.Forms.Label outstandingFeesLabel;
            System.Windows.Forms.Label gradePointAverageLabel;
            System.Windows.Forms.Label registrationNumberLabel;
            System.Windows.Forms.Label courseNumberLabel;
            System.Windows.Forms.Label titleLabel;
            System.Windows.Forms.Label creditHoursLabel;
            System.Windows.Forms.Label fullNameLabel;
            System.Windows.Forms.Label addressLabel;
            this.registrationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.courseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.studentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gradePointStateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grpStudent = new System.Windows.Forms.GroupBox();
            this.addressLabel1 = new System.Windows.Forms.Label();
            this.fullNameLabel1 = new System.Windows.Forms.Label();
            this.gradePointAverageLabel1 = new System.Windows.Forms.Label();
            this.outstandingFeesLabel1 = new System.Windows.Forms.Label();
            this.dateCreatedLabel1 = new System.Windows.Forms.Label();
            this.studentNumberMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.grpRegistration = new System.Windows.Forms.GroupBox();
            this.creditHoursLabel1 = new System.Windows.Forms.Label();
            this.titleLabel1 = new System.Windows.Forms.Label();
            this.courseNumberLabel1 = new System.Windows.Forms.Label();
            this.registrationNumberComboBox = new System.Windows.Forms.ComboBox();
            this.lnkUpdateGrade = new System.Windows.Forms.LinkLabel();
            this.lnkViewDetails = new System.Windows.Forms.LinkLabel();
            studentNumberLabel = new System.Windows.Forms.Label();
            dateCreatedLabel = new System.Windows.Forms.Label();
            outstandingFeesLabel = new System.Windows.Forms.Label();
            gradePointAverageLabel = new System.Windows.Forms.Label();
            registrationNumberLabel = new System.Windows.Forms.Label();
            courseNumberLabel = new System.Windows.Forms.Label();
            titleLabel = new System.Windows.Forms.Label();
            creditHoursLabel = new System.Windows.Forms.Label();
            fullNameLabel = new System.Windows.Forms.Label();
            addressLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.courseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradePointStateBindingSource)).BeginInit();
            this.grpStudent.SuspendLayout();
            this.grpRegistration.SuspendLayout();
            this.SuspendLayout();
            // 
            // studentNumberLabel
            // 
            studentNumberLabel.AutoSize = true;
            studentNumberLabel.Location = new System.Drawing.Point(58, 34);
            studentNumberLabel.Name = "studentNumberLabel";
            studentNumberLabel.Size = new System.Drawing.Size(106, 16);
            studentNumberLabel.TabIndex = 0;
            studentNumberLabel.Text = "Student Number:";
            // 
            // dateCreatedLabel
            // 
            dateCreatedLabel.AutoSize = true;
            dateCreatedLabel.Location = new System.Drawing.Point(58, 118);
            dateCreatedLabel.Name = "dateCreatedLabel";
            dateCreatedLabel.Size = new System.Drawing.Size(90, 16);
            dateCreatedLabel.TabIndex = 2;
            dateCreatedLabel.Text = "Date Created:";
            // 
            // outstandingFeesLabel
            // 
            outstandingFeesLabel.AutoSize = true;
            outstandingFeesLabel.Location = new System.Drawing.Point(412, 119);
            outstandingFeesLabel.Name = "outstandingFeesLabel";
            outstandingFeesLabel.Size = new System.Drawing.Size(115, 16);
            outstandingFeesLabel.TabIndex = 4;
            outstandingFeesLabel.Text = "Outstanding Fees:";
            // 
            // gradePointAverageLabel
            // 
            gradePointAverageLabel.AutoSize = true;
            gradePointAverageLabel.Location = new System.Drawing.Point(55, 150);
            gradePointAverageLabel.Name = "gradePointAverageLabel";
            gradePointAverageLabel.Size = new System.Drawing.Size(136, 16);
            gradePointAverageLabel.TabIndex = 6;
            gradePointAverageLabel.Text = "Grade Point Average:";
            // 
            // registrationNumberLabel
            // 
            registrationNumberLabel.AutoSize = true;
            registrationNumberLabel.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.registrationBindingSource, "RegistrationNumber", true));
            registrationNumberLabel.Location = new System.Drawing.Point(58, 31);
            registrationNumberLabel.Name = "registrationNumberLabel";
            registrationNumberLabel.Size = new System.Drawing.Size(133, 16);
            registrationNumberLabel.TabIndex = 0;
            registrationNumberLabel.Text = "Registration Number:";
            // 
            // registrationBindingSource
            // 
            this.registrationBindingSource.DataSource = typeof(BITCollege_DY.Models.Registration);
            // 
            // courseNumberLabel
            // 
            courseNumberLabel.AutoSize = true;
            courseNumberLabel.Location = new System.Drawing.Point(60, 69);
            courseNumberLabel.Name = "courseNumberLabel";
            courseNumberLabel.Size = new System.Drawing.Size(104, 16);
            courseNumberLabel.TabIndex = 2;
            courseNumberLabel.Text = "Course Number:";
            // 
            // courseBindingSource
            // 
            this.courseBindingSource.DataSource = typeof(BITCollege_DY.Models.Course);
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new System.Drawing.Point(445, 82);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(36, 16);
            titleLabel.TabIndex = 4;
            titleLabel.Text = "Title:";
            // 
            // creditHoursLabel
            // 
            creditHoursLabel.AutoSize = true;
            creditHoursLabel.Location = new System.Drawing.Point(60, 105);
            creditHoursLabel.Name = "creditHoursLabel";
            creditHoursLabel.Size = new System.Drawing.Size(84, 16);
            creditHoursLabel.TabIndex = 6;
            creditHoursLabel.Text = "Credit Hours:";
            // 
            // fullNameLabel
            // 
            fullNameLabel.AutoSize = true;
            fullNameLabel.Location = new System.Drawing.Point(60, 63);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new System.Drawing.Size(71, 16);
            fullNameLabel.TabIndex = 10;
            fullNameLabel.Text = "Full Name:";
            // 
            // addressLabel
            // 
            addressLabel.AutoSize = true;
            addressLabel.Location = new System.Drawing.Point(60, 91);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new System.Drawing.Size(61, 16);
            addressLabel.TabIndex = 12;
            addressLabel.Text = "Address:";
            // 
            // studentBindingSource
            // 
            this.studentBindingSource.DataSource = typeof(BITCollege_DY.Models.Student);
            // 
            // gradePointStateBindingSource
            // 
            this.gradePointStateBindingSource.DataSource = typeof(BITCollege_DY.Models.GradePointState);
            // 
            // grpStudent
            // 
            this.grpStudent.Controls.Add(addressLabel);
            this.grpStudent.Controls.Add(this.addressLabel1);
            this.grpStudent.Controls.Add(fullNameLabel);
            this.grpStudent.Controls.Add(this.fullNameLabel1);
            this.grpStudent.Controls.Add(gradePointAverageLabel);
            this.grpStudent.Controls.Add(this.gradePointAverageLabel1);
            this.grpStudent.Controls.Add(outstandingFeesLabel);
            this.grpStudent.Controls.Add(this.outstandingFeesLabel1);
            this.grpStudent.Controls.Add(dateCreatedLabel);
            this.grpStudent.Controls.Add(this.dateCreatedLabel1);
            this.grpStudent.Controls.Add(studentNumberLabel);
            this.grpStudent.Controls.Add(this.studentNumberMaskedTextBox);
            this.grpStudent.Location = new System.Drawing.Point(47, 58);
            this.grpStudent.Margin = new System.Windows.Forms.Padding(4);
            this.grpStudent.Name = "grpStudent";
            this.grpStudent.Padding = new System.Windows.Forms.Padding(4);
            this.grpStudent.Size = new System.Drawing.Size(800, 246);
            this.grpStudent.TabIndex = 0;
            this.grpStudent.TabStop = false;
            this.grpStudent.Text = "Student Data";
            // 
            // addressLabel1
            // 
            this.addressLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addressLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "Address", true));
            this.addressLabel1.Location = new System.Drawing.Point(198, 91);
            this.addressLabel1.Name = "addressLabel1";
            this.addressLabel1.Size = new System.Drawing.Size(489, 23);
            this.addressLabel1.TabIndex = 13;
            // 
            // fullNameLabel1
            // 
            this.fullNameLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fullNameLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "FullName", true));
            this.fullNameLabel1.Location = new System.Drawing.Point(198, 63);
            this.fullNameLabel1.Name = "fullNameLabel1";
            this.fullNameLabel1.Size = new System.Drawing.Size(489, 23);
            this.fullNameLabel1.TabIndex = 11;
            // 
            // gradePointAverageLabel1
            // 
            this.gradePointAverageLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradePointAverageLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "GradePointAverage", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.gradePointAverageLabel1.Location = new System.Drawing.Point(198, 150);
            this.gradePointAverageLabel1.Name = "gradePointAverageLabel1";
            this.gradePointAverageLabel1.Size = new System.Drawing.Size(100, 23);
            this.gradePointAverageLabel1.TabIndex = 7;
            this.gradePointAverageLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // outstandingFeesLabel1
            // 
            this.outstandingFeesLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outstandingFeesLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "OutstandingFees", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.outstandingFeesLabel1.Location = new System.Drawing.Point(548, 117);
            this.outstandingFeesLabel1.Name = "outstandingFeesLabel1";
            this.outstandingFeesLabel1.Size = new System.Drawing.Size(138, 23);
            this.outstandingFeesLabel1.TabIndex = 5;
            this.outstandingFeesLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dateCreatedLabel1
            // 
            this.dateCreatedLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateCreatedLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "DateCreated", true));
            this.dateCreatedLabel1.Location = new System.Drawing.Point(198, 118);
            this.dateCreatedLabel1.Name = "dateCreatedLabel1";
            this.dateCreatedLabel1.Size = new System.Drawing.Size(100, 23);
            this.dateCreatedLabel1.TabIndex = 3;
            this.dateCreatedLabel1.Text = "\r\n";
            // 
            // studentNumberMaskedTextBox
            // 
            this.studentNumberMaskedTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "StudentNumber", true));
            this.studentNumberMaskedTextBox.Location = new System.Drawing.Point(198, 31);
            this.studentNumberMaskedTextBox.Mask = "0000-0000";
            this.studentNumberMaskedTextBox.Name = "studentNumberMaskedTextBox";
            this.studentNumberMaskedTextBox.Size = new System.Drawing.Size(100, 22);
            this.studentNumberMaskedTextBox.TabIndex = 1;
            this.studentNumberMaskedTextBox.Leave += new System.EventHandler(this.studentNumberMaskedTextBox_Leave);
            // 
            // grpRegistration
            // 
            this.grpRegistration.Controls.Add(creditHoursLabel);
            this.grpRegistration.Controls.Add(courseNumberLabel);
            this.grpRegistration.Controls.Add(this.creditHoursLabel1);
            this.grpRegistration.Controls.Add(titleLabel);
            this.grpRegistration.Controls.Add(this.titleLabel1);
            this.grpRegistration.Controls.Add(this.courseNumberLabel1);
            this.grpRegistration.Controls.Add(registrationNumberLabel);
            this.grpRegistration.Controls.Add(this.registrationNumberComboBox);
            this.grpRegistration.Location = new System.Drawing.Point(47, 331);
            this.grpRegistration.Margin = new System.Windows.Forms.Padding(4);
            this.grpRegistration.Name = "grpRegistration";
            this.grpRegistration.Padding = new System.Windows.Forms.Padding(4);
            this.grpRegistration.Size = new System.Drawing.Size(800, 190);
            this.grpRegistration.TabIndex = 1;
            this.grpRegistration.TabStop = false;
            this.grpRegistration.Text = "Registration Data";
            // 
            // creditHoursLabel1
            // 
            this.creditHoursLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.creditHoursLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Course.CreditHours", true));
            this.creditHoursLabel1.Location = new System.Drawing.Point(198, 98);
            this.creditHoursLabel1.Name = "creditHoursLabel1";
            this.creditHoursLabel1.Size = new System.Drawing.Size(103, 23);
            this.creditHoursLabel1.TabIndex = 7;
            // 
            // titleLabel1
            // 
            this.titleLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titleLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Course.Title", true));
            this.titleLabel1.Location = new System.Drawing.Point(487, 82);
            this.titleLabel1.Name = "titleLabel1";
            this.titleLabel1.Size = new System.Drawing.Size(199, 23);
            this.titleLabel1.TabIndex = 5;
            // 
            // courseNumberLabel1
            // 
            this.courseNumberLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.courseNumberLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Course.CourseNumber", true));
            this.courseNumberLabel1.Location = new System.Drawing.Point(198, 62);
            this.courseNumberLabel1.Name = "courseNumberLabel1";
            this.courseNumberLabel1.Size = new System.Drawing.Size(103, 23);
            this.courseNumberLabel1.TabIndex = 3;
            // 
            // registrationNumberComboBox
            // 
            this.registrationNumberComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.registrationBindingSource, "RegistrationId", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "00-00-00"));
            this.registrationNumberComboBox.DataSource = this.registrationBindingSource;
            this.registrationNumberComboBox.DisplayMember = "RegistrationNumber";
            this.registrationNumberComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.registrationNumberComboBox.FormattingEnabled = true;
            this.registrationNumberComboBox.Location = new System.Drawing.Point(198, 28);
            this.registrationNumberComboBox.Name = "registrationNumberComboBox";
            this.registrationNumberComboBox.Size = new System.Drawing.Size(103, 24);
            this.registrationNumberComboBox.TabIndex = 1;
            this.registrationNumberComboBox.ValueMember = "RegistrationId";
            // 
            // lnkUpdateGrade
            // 
            this.lnkUpdateGrade.AutoSize = true;
            this.lnkUpdateGrade.Enabled = false;
            this.lnkUpdateGrade.Location = new System.Drawing.Point(263, 571);
            this.lnkUpdateGrade.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkUpdateGrade.Name = "lnkUpdateGrade";
            this.lnkUpdateGrade.Size = new System.Drawing.Size(93, 16);
            this.lnkUpdateGrade.TabIndex = 2;
            this.lnkUpdateGrade.TabStop = true;
            this.lnkUpdateGrade.Text = "Update Grade";
            this.lnkUpdateGrade.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpdateGrade_LinkClicked);
            // 
            // lnkViewDetails
            // 
            this.lnkViewDetails.AutoSize = true;
            this.lnkViewDetails.Enabled = false;
            this.lnkViewDetails.Location = new System.Drawing.Point(508, 571);
            this.lnkViewDetails.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkViewDetails.Name = "lnkViewDetails";
            this.lnkViewDetails.Size = new System.Drawing.Size(81, 16);
            this.lnkViewDetails.TabIndex = 3;
            this.lnkViewDetails.TabStop = true;
            this.lnkViewDetails.Text = "View Details";
            this.lnkViewDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewDetails_LinkClicked);
            // 
            // StudentData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 629);
            this.Controls.Add(this.lnkViewDetails);
            this.Controls.Add(this.lnkUpdateGrade);
            this.Controls.Add(this.grpRegistration);
            this.Controls.Add(this.grpStudent);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StudentData";
            this.Text = "StudentData";
            this.Load += new System.EventHandler(this.StudentData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.courseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradePointStateBindingSource)).EndInit();
            this.grpStudent.ResumeLayout(false);
            this.grpStudent.PerformLayout();
            this.grpRegistration.ResumeLayout(false);
            this.grpRegistration.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStudent;
        private System.Windows.Forms.GroupBox grpRegistration;
        private System.Windows.Forms.LinkLabel lnkUpdateGrade;
        private System.Windows.Forms.LinkLabel lnkViewDetails;
        private System.Windows.Forms.MaskedTextBox studentNumberMaskedTextBox;
        private System.Windows.Forms.BindingSource studentBindingSource;
        private System.Windows.Forms.Label gradePointAverageLabel1;
        private System.Windows.Forms.Label outstandingFeesLabel1;
        private System.Windows.Forms.Label dateCreatedLabel1;
        private System.Windows.Forms.ComboBox registrationNumberComboBox;
        private System.Windows.Forms.BindingSource registrationBindingSource;
        private System.Windows.Forms.Label courseNumberLabel1;
        private System.Windows.Forms.Label creditHoursLabel1;
        private System.Windows.Forms.Label titleLabel1;
        private System.Windows.Forms.BindingSource courseBindingSource;
        private System.Windows.Forms.BindingSource gradePointStateBindingSource;
        private System.Windows.Forms.Label addressLabel1;
        private System.Windows.Forms.Label fullNameLabel1;
    }
}
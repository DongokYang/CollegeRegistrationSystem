/*
 * Name: Dongok Yang
 * Program: Business Information Technology
 * Course: ADEV-3008 Programming 3
 * Created: 2024-09-06
 * Updated: 2024-09-06
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BITCollege_DY.Data;
using System.Data.SqlClient;
using System.Data;
using Utility;


namespace BITCollege_DY.Models
{
    /// <summary>
    /// Represents a student
    /// </summary>
    public class Student
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }


        [Required]
        [ForeignKey("GradePointState")]
        public int GradePointStateId { get; set; }
        public virtual GradePointState GradePointState { get; set; }

        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }
        public virtual AcademicProgram AcademicProgram { get; set; }

        [Display(Name = "Student\nNumber")]
        public long StudentNumber { get; set; }

        [Required]
        [Display(Name = "First\nName")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "Last\nName")]
        public String LastName { get; set; }

        [Required]
        public String Address { get; set; }

        [Required]
        public String City { get; set; }

        [Required]
        [RegularExpression("^(N[BLSTU]|[AMN]B|[BQ]C|ON|PE|SK|YT)", ErrorMessage = "Must be one of the canadian province code")]
        public String Province { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime DateCreated { get; set; }

        [Range(0, 4.5)]
        [Display(Name = "Grade\nPoint\nAverage")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double? GradePointAverage { get; set; }

        [Required]
        [Display(Name = "Fees")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double? OutstandingFees { get; set; }

        public String Notes { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Display(Name = "Address")]
        public string FullAddress
        {
            get
            {
                return $"{Address} {City} {Province}";
            }
        }

        public virtual ICollection<Registration> Registration { get; set; }

        public void ChangeState()
        {
            BITCollege_DYContext db = new BITCollege_DYContext();
            GradePointState currentState = db.GradePointStates.Find(GradePointStateId);
            int previousId = 0;

            while (previousId != currentState.GradepointStateId)
            {
                currentState.StateChangeCheck(this);
                previousId = currentState.GradepointStateId;
                currentState = db.GradePointStates.Find(GradePointStateId);
                db.SaveChanges();
            }
            db.Dispose();
        }
        public void SetNextStudentNumber()
        {
            long? newNumber = StoredProcedures.NextNumber("NextStudent");
            StudentNumber = (newNumber != null) ? newNumber.Value : 0;
        }

    }

    /// <summary>
    /// Represents an academic program
    /// </summary>
    public class AcademicProgram
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AcademicProgramId { get; set; }

        [Required]
        [Display(Name = "Program")]
        public String ProgramAcronym { get; set; }

        [Required]
        [Display(Name = "Program\nName")]
        public String Description { get; set; }

        public virtual ICollection<Student> Student { get; set; }

        public virtual ICollection<Course> Course { get; set; }
    }

    /// <summary>
    /// Represents a grade point State
    /// </summary>
    public abstract class GradePointState
    {
        protected static BITCollege_DYContext db = new BITCollege_DYContext();

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GradepointStateId { get; set; }

        [Required]
        [Display(Name = "Lower\nLimit")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double LowerLimit { get; set; }

        [Required]
        [Display(Name = "Upper\nLimit")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double UpperLimit { get; set; }

        [Required]
        [Display(Name = "Tuition\nRate\nFactor")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double TuitionRateFactor { get; set; }

        [Display(Name = "State")]
        public string Description
        {
            get
            {
                return GetType().Name.Substring(0, GetType().Name.LastIndexOf("State"));
            }
        }

        public virtual ICollection<Student> Student { get; set; }
        /// <summary>
        /// blueprint for StateChangeCheck
        /// </summary>
        /// <param name="student">A student object</param>

        public abstract void StateChangeCheck(Student student);
        /// <summary>
        /// blueprint for TuitionRateAdjustment
        /// </summary>
        /// <param name="student">A student object</param>
        /// <returns>Adjusted tuition rate</returns>
        public abstract double TuitionRateAdjustment(Student student);

    }

    /// <summary>
    /// Represents a suspended state
    /// </summary>
    public class SuspendedState : GradePointState
    {
        private static SuspendedState suspendedState;

        /// <summary>
        /// Creates and instance of Suspended State
        /// </summary>
        private SuspendedState()
        {
            LowerLimit = 0.00;
            UpperLimit = 1.00;
            TuitionRateFactor = 1.1;
        }
        /// <summary>
        /// Ensures an instance of the Suspended State
        /// </summary>
        /// <returns>and Instance of Suspended State</returns>
        public static SuspendedState GetInstance()
        {
            if (suspendedState == null)
            {
                suspendedState = db.SuspendedStates.SingleOrDefault();
                if (suspendedState == null)
                {
                    suspendedState = new SuspendedState();
                    db.SuspendedStates.Add(suspendedState);
                    db.SaveChanges();
                }
            }
            return suspendedState;
        }

        /// <summary>
        /// Checks for a change in state
        /// </summary>
        /// <param name="student">a student object</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage > 1)
            {
                student.GradePointStateId = ProbationState.GetInstance().GradepointStateId;
            }
            db.SaveChanges();

        }
        /// <summary>
        /// Adjust's the students tuition rate
        /// </summary>
        /// <param name="student"> a student object</param>
        /// <returns>the adjusted tuition rate</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double adjustTuition = TuitionRateFactor;
            double currentGpa = student.GradePointAverage ?? 0.0;

            // test after replacing currentGpa > student.GradePointAverage
            if (currentGpa < 0.75)
            {
                adjustTuition += .02;
            }
            else if (currentGpa < 0.5)
            {
                adjustTuition += .05;
            }
            return adjustTuition;
        }
    }

    /// <summary>
    /// Represents a probation state
    /// </summary>
    public class ProbationState : GradePointState
    {
        private static ProbationState probationState;

        /// <summary>
        /// Creates and instance of Probation State
        /// </summary>
        private ProbationState()
        {
            LowerLimit = 1.00;
            UpperLimit = 2.00;
            TuitionRateFactor = 1.075;
        }
        /// <summary>
        /// Ensures an instance of the Probation state
        /// </summary>
        /// <returns>and Instance of Probation State</returns>
        public static ProbationState GetInstance()
        {
            if (probationState == null)
            {
                probationState = db.ProbationStates.SingleOrDefault();
                if (probationState == null)
                {
                    probationState = new ProbationState();
                    db.ProbationStates.Add(probationState);
                    db.SaveChanges();
                }
            }
            return probationState;
        }

        /// <summary>
        /// Checks for a change in state
        /// </summary>
        /// <param name="student">a student object</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage < 1)
            {
                student.GradePointStateId = SuspendedState.GetInstance().GradepointStateId;
            }
            if (student.GradePointAverage > 2)
            {
                student.GradePointStateId = RegularState.GetInstance().GradepointStateId;
            }
            db.SaveChanges();
        }
        /// <summary>
        /// Adjust's the students tuition rate
        /// </summary>
        /// <param name="student"> a student object</param>
        /// <returns>the adjusted tuition rate</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            // use a local variable (super important) 
            //x. >> local  
            // lambda operation 공부하기  
            double adjustTuition = TuitionRateFactor;
            IQueryable<Registration> registrations =
                db.Registrations.Where(x => x.StudentId == student.StudentId && x.Grade != null);

            if (registrations.Count() >= 5)
            {
                adjustTuition -= .04;
            }

            return adjustTuition;
        }
    }

    /// <summary>
    /// Represents a regular state
    /// </summary>
    public class RegularState : GradePointState
    {
        private static RegularState regularState;

        /// <summary>
        /// Creates and instance of Regular State
        /// </summary>
        private RegularState()
        {
            LowerLimit = 2.00;
            UpperLimit = 3.70;
            TuitionRateFactor = 1.0;
        }
        /// <summary>
        /// Ensures an instance of the Regular state
        /// </summary>
        /// <returns>and Instance of Regular State</returns>
        public static RegularState GetInstance()
        {
            if (regularState == null)
            {
                regularState = db.RegularStates.SingleOrDefault();
                if (regularState == null)
                {
                    regularState = new RegularState();
                    db.RegularStates.Add(regularState);
                    db.SaveChanges();
                }
            }
            return regularState;
        }


        /// <summary>
        /// Checks for a change in state
        /// </summary>
        /// <param name="student">a student object</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage < 2)
            {
                student.GradePointStateId = ProbationState.GetInstance().GradepointStateId;
            }
            if (student.GradePointAverage > 3.7)
            {
                student.GradePointStateId = HonoursState.GetInstance().GradepointStateId;
            }
            db.SaveChanges();
        }
        /// <summary>
        /// Adjust's the students tuition rate
        /// </summary>
        /// <param name="student"> a student object</param>
        /// <returns>the adjusted tuition rate</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double adjustTuition = TuitionRateFactor;
            return adjustTuition;
        }
    }

    /// <summary>
    /// Represents an honours state
    /// </summary>
    public class HonoursState : GradePointState
    {
        private static HonoursState honoursState;

        /// <summary>
        /// Creates and instance of Honours State
        /// </summary>
        private HonoursState()
        {
            LowerLimit = 3.70;
            UpperLimit = 4.50;
            TuitionRateFactor = 0.9;
        }
        /// <summary>
        /// Ensures an instance of the Honours state
        /// </summary>
        /// <returns>and Instance of Honours State</returns>
        public static HonoursState GetInstance()
        {
            if (honoursState == null)
            {
                honoursState = db.HonoursStates.SingleOrDefault();
                if (honoursState == null)
                {
                    honoursState = new HonoursState();
                    db.HonoursStates.Add(honoursState);
                    db.SaveChanges();
                }
            }
            return honoursState;
        }

        /// <summary>
        /// Checks for a change in state
        /// </summary>
        /// <param name="student">a student object</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage < 3.7)
            {
                student.GradePointStateId = RegularState.GetInstance().GradepointStateId;
            }
            db.SaveChanges();
        }
        /// <summary>
        /// Adjust's the students tuition rate
        /// </summary>
        /// <param name="student"> a student object</param>
        /// <returns>the adjusted tuition rate</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double adjustTuition = TuitionRateFactor;
            double currentGpa = student.GradePointAverage ?? 0.0;
            IQueryable<Registration> registrations =
                db.Registrations.Where(x => x.StudentId == student.StudentId && x.Grade != null);

            if (registrations.Count() >= 5)
            {
                adjustTuition -= .05;
            }
            if (currentGpa > 4.25)
            {
                adjustTuition -= .02;
            }
            return adjustTuition;
        }
    }


    /// <summary>
    /// Represents a course
    /// </summary>
    public abstract class Course
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }


        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }
        public virtual AcademicProgram AcademicProgram { get; set; }

        [Display(Name = "Course\nNumber")]
        public String CourseNumber { get; set; }

        [Required]
        public String Title { get; set; }

        [Required]
        [Display(Name = "Credit\nHours")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double CreditHours { get; set; }

        [Required]
        [Display(Name = "Tuition")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double TuitionAmount { get; set; }

        [Display(Name = "“Course\nType")]
        public string CourseType
        {
            get
            {
                return GetType().Name.Substring(0, GetType().Name.LastIndexOf("Course"));
            }
        }

        public String Notes { get; set; }

        public virtual ICollection<Registration> Registration { get; set; }

        public abstract void SetNextCourseNumber();
    }

    /// <summary>
    /// Represents a graded Course
    /// </summary>
    public class GradedCourse : Course
    {
        [Required]
        [Display(Name = "Assignments")]
        [DisplayFormat(DataFormatString = "{0:f2}%")]
        public double AssignmentWeight { get; set; }

        [Required]
        [Display(Name = "Exams")]
        [DisplayFormat(DataFormatString = "{0:f2}%")]
        public double ExamWeight { get; set; }
        public override void SetNextCourseNumber()
        {
            long? newNumber = StoredProcedures.NextNumber("NextGradedCourse");
            CourseNumber = (newNumber != null) ? "G-" + newNumber.Value : "A-0";
        }
    }

    /// <summary>
    /// Represents a mastery Course
    /// </summary>
    public class MasteryCourse : Course
    {
        [Required]
        [Display(Name = "Maximum\nAttempts")]
        public int MaximumAttempts { get; set; }
        public override void SetNextCourseNumber()
        {
            long? newNumber = StoredProcedures.NextNumber("NextMasteryCourse");
            CourseNumber = (newNumber != null) ? "M-" + newNumber.Value : "A-0";
        }
    }

    /// <summary>
    /// Represents an audit Course
    /// </summary>
    public class AuditCourse : Course
    {
        public override void SetNextCourseNumber()
        {
            long? newNumber = StoredProcedures.NextNumber("NextAuditCourse");
            CourseNumber = (newNumber != null) ? "A-" + newNumber.Value : "A-0";

        }
    }

    /// <summary>
    /// Represents a registration
    /// </summary>
    public class Registration
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RegistrationId { get; set; }


        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Display(Name = "Registration\nNumber")]
        public long RegistrationNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime RegistrationDate { get; set; }

        [Range(0, 1)]
        [Display(Name = "Grade\nPoint\nAverage")]
        [DisplayFormat(DataFormatString = "{0:F2}", NullDisplayText = "Ungraded")]
        public double? Grade { get; set; }

        public String Notes { get; set; }
        public void SetNextRegistrationNumber()
        {
            long? newNumber = StoredProcedures.NextNumber("NextRegistration");
            RegistrationNumber = (newNumber != null) ? (long)newNumber : 0;
        }
    }

    /// <summary>
    /// Finds a next unique number
    /// </summary>
    public abstract class NextUniqueNumber
    {
        protected static BITCollege_DYContext db = new BITCollege_DYContext();
        private static NextUniqueNumber nextUniqueNumber;
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextUniqueNumberId { get; set; }

        [Required]
        public long NextAvailableNumber { get; set; }

    }

    /// <summary>
    /// Finds a next unique number for next student
    /// </summary>
    public class NextStudent : NextUniqueNumber
    {
        private static NextStudent nextStudent;

        /// <summary>
        /// sets next available nubmer 
        /// </summary>
        private NextStudent()
        {
            this.NextAvailableNumber = 20000000;
        }

        /// <summary>
        /// Gets an instance for Nextstudent
        /// </summary>
        /// <returns> next student</returns>
        public static NextStudent GetInstance()
        {
            if (nextStudent == null)
            {
                nextStudent = db.NextStudents.FirstOrDefault();
                if (nextStudent == null)
                {
                    nextStudent = new NextStudent();
                    db.NextStudents.Add(nextStudent);
                    db.SaveChanges();
                }
            }
            return nextStudent;
        }
        public void GetNextStudent() { }
    }
    /// <summary>
    /// Finds a next unique number for next registration
    /// </summary>
    public class NextRegistration : NextUniqueNumber
    {
        private static NextRegistration nextRegistration;
        /// <summary>
        /// sets next available nubmer 
        /// </summary>
        private NextRegistration()
        {
            this.NextAvailableNumber = 700;
        }
        /// <summary>
        /// Gets an instance for NextRegistration
        /// </summary>
        /// <returns>Next Registration</returns>
        public static NextRegistration GetInstance()
        {
            if (nextRegistration == null)
            {
                nextRegistration = db.NextRegistrations.FirstOrDefault();
                if (nextRegistration == null)
                {
                    nextRegistration = new NextRegistration();
                    db.NextRegistrations.Add(nextRegistration);
                    db.SaveChanges();
                }
            }
            return nextRegistration;
        }

        public void GetNextRegistration() { }
    }
    /// <summary>
    /// Finds a next unique number for next masteryCourse 
    /// </summary>
    public class NextMasteryCourse : NextUniqueNumber
    {
        private static NextMasteryCourse nextMasteryCourse;
        /// <summary>
        /// sets next available nubmer 
        /// </summary>
        private NextMasteryCourse()
        {
            this.NextAvailableNumber = 20000;
        }
        /// <summary>
        /// Gets an instance for Next Mastery Course
        /// </summary>
        /// <returns>Next Mastery Course</returns>
        public static NextMasteryCourse GetInstance()
        {
            if (nextMasteryCourse == null)
            {
                nextMasteryCourse = db.NextMasteryCourses.FirstOrDefault();
                if (nextMasteryCourse == null)
                {
                    nextMasteryCourse = new NextMasteryCourse();
                    db.NextMasteryCourses.Add(nextMasteryCourse);
                    db.SaveChanges();
                }
            }
            return nextMasteryCourse;
        }
        public void GetNextMasteryCourse() { }
    }
    /// <summary>
    /// Finds a next unique number for Next GradedCourse
    /// </summary>
    public class NextGradedCourse : NextUniqueNumber
    {
        private static NextGradedCourse nextGradedCourse;
        /// <summary>
        /// sets next available nubmer 
        /// </summary>
        private NextGradedCourse()
        {
            this.NextAvailableNumber = 200000;
        }

        /// <summary>
        /// Gets an instance for Next GradedCourse
        /// </summary>
        /// <returns>Next GradedCourse</returns>
        public static NextGradedCourse GetInstance()
        {
            if (nextGradedCourse == null)
            {
                nextGradedCourse = db.NextGradedCourses.FirstOrDefault();
                if (nextGradedCourse == null)
                {
                    nextGradedCourse = new NextGradedCourse();
                    db.NextGradedCourses.Add(nextGradedCourse);
                    db.SaveChanges();
                }
            }
            return nextGradedCourse;
        }

        /// <summary>
        /// Finds a next unique number for Next Audit Course
        /// </summary>
        public void GetNextGradedCourse() { }
    }
    public class NextAuditCourse : NextUniqueNumber
    {
        private static NextAuditCourse nextAuditCourse;
        /// <summary>
        /// sets next available nubmer 
        /// </summary>
        private NextAuditCourse()
        {
            this.NextAvailableNumber = 2000;
        }
        /// <summary>
        /// Gets an instance for Next AuditCourse
        /// </summary>
        /// <returns>Next AuditCourse</returns>
        public static NextAuditCourse GetInstance()
        {
            if (nextAuditCourse == null)
            {
                nextAuditCourse = db.NextAuditCourses.FirstOrDefault();
                if (nextAuditCourse == null)
                {
                    nextAuditCourse = new NextAuditCourse();
                    db.NextAuditCourses.Add(nextAuditCourse);
                    db.SaveChanges();
                }
            }
            return nextAuditCourse;
        }
        public void GetNextAuditCourse() { }
    }

    /// <summary>
    /// find next number 
    /// </summary>
    public static class StoredProcedures
    {
        /// <summary>
        /// find next number 
        /// </summary>
        /// <param name="discriminator">which parameter to change</param>
        /// <returns>next available number </returns>
        public static long? NextNumber(string discriminator)
        {
            long? returnValue = 0;
            SqlConnection connection = new SqlConnection("Data Source=MAR\\SPACE; " +
            "Initial Catalog=BITCollege_DYContext;Integrated Security=True");

            try
            {
                SqlCommand storedProcedure = new SqlCommand("next_number", connection);
                storedProcedure.CommandType = CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@Discriminator", discriminator);
                SqlParameter outputParameter = new SqlParameter("@NewVal", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                storedProcedure.Parameters.Add(outputParameter);
                connection.Open();
                storedProcedure.ExecuteNonQuery();
                connection.Close();
                returnValue = (long?)outputParameter.Value;
            }
            catch (Exception ex)
            {
                returnValue = null;
            }
            return returnValue;

        }
    }

    public class Assignment5CheckIn
    {
        private BITCollege_DYContext db = new BITCollege_DYContext();
        /// <summary>
        /// Allows a student to register for a course
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="notes"></param>
        /// <returns>a number representing an error or success code</returns>
        public int RegisterCourse(int studentId, int courseId, string notes)
        {
            IQueryable<Registration> allRecords = db.Registrations.Where
                (x => x.StudentId == studentId && x.CourseId == courseId);

            Student student = db.Students.Find(studentId);
            Course course = db.Courses.Find(courseId);

            int returnCode = 0;

            IEnumerable<Registration> nullRecords = allRecords.Where(x => x.Grade == null);

            if (nullRecords.Count() > 0 ) {
                returnCode = -100;
            }

            if(BusinessRules.CourseTypeLookup(course.CourseType) == CourseType.MASTERY)
            {
                MasteryCourse masteryCourse = (MasteryCourse)course;
                IEnumerable<Registration> notNullRecords = allRecords.Where(x => x.Grade != null);
                if(notNullRecords.Count() >= masteryCourse.MaximumAttempts)
                {
                    returnCode = -200;
                }
            }

            try
            {
                if (returnCode == 0)
                {
                    Registration registration = new Registration();

                    registration.StudentId = studentId;
                    registration.CourseId = courseId;
                    registration.SetNextRegistrationNumber();
                    registration.RegistrationDate = DateTime.Now;
                    registration.Grade = null;
                    registration.Notes = notes;

                    db.Registrations.Add(registration);
                    db.SaveChanges();

                    student.OutstandingFees += course.TuitionAmount 
                        * student.GradePointState.TuitionRateAdjustment(student);
                }
            }
            catch (Exception)
            {

                returnCode = -300;
            }

            return returnCode;
        }
    }
}
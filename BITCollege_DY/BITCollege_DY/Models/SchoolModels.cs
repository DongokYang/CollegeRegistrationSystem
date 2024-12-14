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

        [Required]
        [Range(10000000, 99999999)]
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
                if(probationState == null)
                {
                    probationState = db.ProbationStates.SingleOrDefault();
                    if(probationState == null)
                    {
                        probationState= new ProbationState();
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
                if(student.GradePointAverage < 1)
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
            
                if(registrations.Count() >= 5)
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


        [Required]
        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }
        public virtual AcademicProgram AcademicProgram { get; set; }

        [Required]
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
    }

    /// <summary>
    /// Represents a mastery Course
    /// </summary>
    public class MasteryCourse : Course
    {
        [Required]
        [Display(Name = "Maximum\nAttempts")]
        public int MaximumAttempts { get; set; }

    }

    /// <summary>
    /// Represents an audit Course
    /// </summary>
    public class AuditCourse : Course
    {

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

        [Required]
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

    }
}

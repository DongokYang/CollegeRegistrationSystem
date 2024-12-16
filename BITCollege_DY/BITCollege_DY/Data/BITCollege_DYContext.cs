using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BITCollege_DY.Data
{
    public class BITCollege_DYContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BITCollege_DYContext() : base("name=BITCollege_DYContext")
        {
        }

        public System.Data.Entity.DbSet<BITCollege_DY.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<BITCollege_DY.Models.AcademicProgram> AcademicPrograms { get; set; }

        public System.Data.Entity.DbSet<BITCollege_DY.Models.GradePointState> GradePointStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_DY.Models.Course> Courses { get; set; }
        public System.Data.Entity.DbSet<BITCollege_DY.Models.Registration> Registrations { get; set; }

        public System.Data.Entity.DbSet<BITCollege_DY.Models.AuditCourse> AuditCourses { get; set; }
        public System.Data.Entity.DbSet<BITCollege_DY.Models.MasteryCourse> MasteryCourses { get; set; }
        public System.Data.Entity.DbSet<BITCollege_DY.Models.GradedCourse> GradedCourses { get; set; }
        public System.Data.Entity.DbSet<BITCollege_DY.Models.SuspendedState> SuspendedStates { get; set; }
        public System.Data.Entity.DbSet<BITCollege_DY.Models.ProbationState> ProbationStates { get; set; }
        public System.Data.Entity.DbSet<BITCollege_DY.Models.RegularState> RegularStates { get; set; }
        public System.Data.Entity.DbSet<BITCollege_DY.Models.HonoursState> HonoursStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_DY.Models.NextUniqueNumber> NextUniqueNumbers { get; set; }
        public System.Data.Entity.DbSet<BITCollege_DY.Models.NextStudent> NextStudents { get; set; }
        public System.Data.Entity.DbSet<BITCollege_DY.Models.NextGradedCourse> NextGradedCourses { get; set; }
        public System.Data.Entity.DbSet<BITCollege_DY.Models.NextAuditCourse> NextAuditCourses { get; set; }
        public System.Data.Entity.DbSet<BITCollege_DY.Models.NextMasteryCourse> NextMasteryCourses { get; set; }
        public System.Data.Entity.DbSet<BITCollege_DY.Models.NextRegistration> NextRegistrations { get; set; }
    }
}

namespace BITCollege_DY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicPrograms",
                c => new
                    {
                        AcademicProgramId = c.Int(nullable: false, identity: true),
                        ProgramAcronym = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AcademicProgramId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        AcademicProgramId = c.Int(nullable: false),
                        CourseNumber = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        CreditHours = c.Double(nullable: false),
                        TuitionAmount = c.Double(nullable: false),
                        Notes = c.String(),
                        AssignmentWeight = c.Double(),
                        ExamWeight = c.Double(),
                        MaximumAttempts = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.AcademicPrograms", t => t.AcademicProgramId, cascadeDelete: true)
                .Index(t => t.AcademicProgramId);
            
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        RegistrationId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RegistrationNumber = c.Long(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        Grade = c.Double(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.RegistrationId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        GradePointStateId = c.Int(nullable: false),
                        AcademicProgramId = c.Int(),
                        StudentNumber = c.Long(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Province = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        GradePointAverage = c.Double(),
                        OutstandingFees = c.Double(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.AcademicPrograms", t => t.AcademicProgramId)
                .ForeignKey("dbo.GradePointStates", t => t.GradePointStateId, cascadeDelete: true)
                .Index(t => t.GradePointStateId)
                .Index(t => t.AcademicProgramId);
            
            CreateTable(
                "dbo.GradePointStates",
                c => new
                    {
                        GradepointStateId = c.Int(nullable: false, identity: true),
                        LowerLimit = c.Double(nullable: false),
                        UpperLimit = c.Double(nullable: false),
                        TuitionRateFactor = c.Double(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.GradepointStateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Registrations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "GradePointStateId", "dbo.GradePointStates");
            DropForeignKey("dbo.Students", "AcademicProgramId", "dbo.AcademicPrograms");
            DropForeignKey("dbo.Registrations", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "AcademicProgramId", "dbo.AcademicPrograms");
            DropIndex("dbo.Students", new[] { "AcademicProgramId" });
            DropIndex("dbo.Students", new[] { "GradePointStateId" });
            DropIndex("dbo.Registrations", new[] { "CourseId" });
            DropIndex("dbo.Registrations", new[] { "StudentId" });
            DropIndex("dbo.Courses", new[] { "AcademicProgramId" });
            DropTable("dbo.GradePointStates");
            DropTable("dbo.Students");
            DropTable("dbo.Registrations");
            DropTable("dbo.Courses");
            DropTable("dbo.AcademicPrograms");
        }
    }
}

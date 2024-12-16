namespace BITCollege_DY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNextUniqueNumberModels2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "AcademicProgramId", "dbo.AcademicPrograms");
            DropIndex("dbo.Courses", new[] { "AcademicProgramId" });
            AlterColumn("dbo.Courses", "AcademicProgramId", c => c.Int());
            CreateIndex("dbo.Courses", "AcademicProgramId");
            AddForeignKey("dbo.Courses", "AcademicProgramId", "dbo.AcademicPrograms", "AcademicProgramId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "AcademicProgramId", "dbo.AcademicPrograms");
            DropIndex("dbo.Courses", new[] { "AcademicProgramId" });
            AlterColumn("dbo.Courses", "AcademicProgramId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "AcademicProgramId");
            AddForeignKey("dbo.Courses", "AcademicProgramId", "dbo.AcademicPrograms", "AcademicProgramId", cascadeDelete: true);
        }
    }
}

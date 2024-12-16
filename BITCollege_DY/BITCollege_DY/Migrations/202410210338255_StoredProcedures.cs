namespace BITCollege_DY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoredProcedures : DbMigration
    {
        public override void Up()
        {
            this.Sql(Properties.Resource.create_next_number);
        }
        
        public override void Down()
        {
            this.Sql(Properties.Resource.drop_next_number);
        }
    }
}

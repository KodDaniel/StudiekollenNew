namespace StudiekollenNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOfDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exams", "RandomOrder", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exams", "RandomOrder", c => c.Boolean());
        }
    }
}

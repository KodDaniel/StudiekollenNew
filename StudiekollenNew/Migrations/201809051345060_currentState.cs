namespace StudiekollenNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currentState : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "Query", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "Query", c => c.String());
        }
    }
}

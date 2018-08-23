namespace StudiekollenNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Question", newName: "Questions");
            RenameTable(name: "dbo.Test", newName: "Tests");
            DropTable("dbo.RoleViewModel");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoleViewModel",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            RenameTable(name: "dbo.Tests", newName: "Test");
            RenameTable(name: "dbo.Questions", newName: "Question");
        }
    }
}

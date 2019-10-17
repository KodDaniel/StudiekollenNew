namespace StudiekollenNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exams", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Exams", new[] { "UserId" });
            AlterColumn("dbo.Exams", "ExamName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Exams", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Exams", "UserId");
            AddForeignKey("dbo.Exams", "UserId", "dbo.AspNetUsers", "Id",  cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exams", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Exams", new[] { "UserId" });
            AlterColumn("dbo.Exams", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Exams", "ExamName", c => c.String());
            CreateIndex("dbo.Exams", "UserId");
            AddForeignKey("dbo.Exams", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}

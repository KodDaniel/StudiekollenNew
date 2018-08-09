namespace StudiekollenNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestTable", "User_Id", "dbo.User");
            DropForeignKey("dbo.TestTable", "UserId", "dbo.User");
            DropIndex("dbo.TestTable", new[] { "UserId" });
            DropIndex("dbo.TestTable", new[] { "User_Id" });
            RenameColumn(table: "dbo.User", name: "Id", newName: "UserId");
            CreateTable(
                "dbo.RoleViewModel",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.TestTable", "UserId");
            DropColumn("dbo.TestTable", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestTable", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.TestTable", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.RoleViewModel");
            RenameColumn(table: "dbo.User", name: "UserId", newName: "Id");
            CreateIndex("dbo.TestTable", "User_Id");
            CreateIndex("dbo.TestTable", "UserId");
            AddForeignKey("dbo.TestTable", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TestTable", "User_Id", "dbo.User", "Id");
        }
    }
}

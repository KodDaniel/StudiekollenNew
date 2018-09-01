namespace StudiekollenNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hej : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUserClaims", name: "UserClaimId", newName: "Id");
            RenameColumn(table: "dbo.AspNetRoles", name: "RoleId", newName: "Id");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.AspNetRoles", name: "Id", newName: "RoleId");
            RenameColumn(table: "dbo.AspNetUserClaims", name: "Id", newName: "UserClaimId");
        }
    }
}

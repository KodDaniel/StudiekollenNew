namespace StudiekollenNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ks : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MetaTagDetails", newName: "MetaTags");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MetaTags", newName: "MetaTagDetails");
        }
    }
}

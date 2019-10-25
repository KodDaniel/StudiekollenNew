namespace StudiekollenNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MetaTagDetails", "Title", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MetaTagDetails", "Title");
        }
    }
}

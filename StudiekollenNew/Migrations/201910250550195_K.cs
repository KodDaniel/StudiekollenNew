namespace StudiekollenNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class K : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MetaTagDetails", "PageUrl", c => c.String(maxLength: 100));
            AlterColumn("dbo.MetaTagDetails", "MetaKeyWords", c => c.String(maxLength: 100));
            AlterColumn("dbo.MetaTagDetails", "MetaDescription", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MetaTagDetails", "MetaDescription", c => c.String());
            AlterColumn("dbo.MetaTagDetails", "MetaKeyWords", c => c.String());
            AlterColumn("dbo.MetaTagDetails", "PageUrl", c => c.String());
        }
    }
}

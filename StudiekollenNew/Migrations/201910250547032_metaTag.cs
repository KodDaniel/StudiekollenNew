namespace StudiekollenNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class metaTag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MetaTagDetails",
                c => new
                    {
                        MetaTagId = c.Int(nullable: false, identity: true),
                        PageUrl = c.String(),
                        MetaKeyWords = c.String(),
                        MetaDescription = c.String(),
                    })
                .PrimaryKey(t => t.MetaTagId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MetaTagDetails");
        }
    }
}

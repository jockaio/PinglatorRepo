namespace Cantofy3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wordWordSearch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Words", "CreatedTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Words", "UpdateTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Words", "UpdatedBy", c => c.String(unicode: false));
            AddColumn("dbo.WordSearches", "SearchID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WordSearches", "SearchID");
            DropColumn("dbo.Words", "UpdatedBy");
            DropColumn("dbo.Words", "UpdateTime");
            DropColumn("dbo.Words", "CreatedTime");
        }
    }
}

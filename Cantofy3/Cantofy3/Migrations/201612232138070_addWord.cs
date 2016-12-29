namespace Cantofy3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addWord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WordSearches", "WordId", c => c.Int(nullable: false));
            CreateIndex("dbo.WordSearches", "WordId");
            AddForeignKey("dbo.WordSearches", "WordId", "dbo.Words", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WordSearches", "WordId", "dbo.Words");
            DropIndex("dbo.WordSearches", new[] { "WordId" });
            DropColumn("dbo.WordSearches", "WordId");
        }
    }
}

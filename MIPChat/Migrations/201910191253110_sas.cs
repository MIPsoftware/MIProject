namespace MIPChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sas : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Message", "AuthorId");
            AddForeignKey("dbo.Message", "AuthorId", "dbo.User", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "AuthorId", "dbo.User");
            DropIndex("dbo.Message", new[] { "AuthorId" });
        }
    }
}

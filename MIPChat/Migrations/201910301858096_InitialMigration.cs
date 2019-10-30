namespace MIPChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatModel",
                c => new
                    {
                        ChatId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        IsLocal = c.Boolean(nullable: false),
                        Icon = c.Binary(),
                    })
                .PrimaryKey(t => t.ChatId);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        MessageId = c.Guid(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                        ChatId = c.Guid(nullable: false),
                        Content = c.String(nullable: false),
                        TheTimeOfSending = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.ChatModel", t => t.ChatId, cascadeDelete: true)
                .Index(t => t.ChatId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        LastLogIn = c.DateTime(nullable: false),
                        LastLogOut = c.DateTime(nullable: false),
                        Icon = c.Binary(),
                        ChatModel_ChatId = c.Guid(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ChatModel", t => t.ChatModel_ChatId)
                .Index(t => t.ChatModel_ChatId);
            
            CreateTable(
                "dbo.FileModel",
                c => new
                    {
                        FileId = c.Guid(nullable: false),
                        Name = c.String(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "ChatModel_ChatId", "dbo.ChatModel");
            DropForeignKey("dbo.Message", "ChatId", "dbo.ChatModel");
            DropIndex("dbo.User", new[] { "ChatModel_ChatId" });
            DropIndex("dbo.Message", new[] { "ChatId" });
            DropTable("dbo.FileModel");
            DropTable("dbo.User");
            DropTable("dbo.Message");
            DropTable("dbo.ChatModel");
        }
    }
}

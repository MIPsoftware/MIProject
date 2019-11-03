namespace MIPChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chat",
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
                        UserId = c.Guid(nullable: false),
                        ChatId = c.Guid(nullable: false),
                        Content = c.String(nullable: false),
                        TheTimeOfSending = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Chat", t => t.ChatId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
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
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.File",
                c => new
                    {
                        FileId = c.Guid(nullable: false),
                        Name = c.String(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.UserChat",
                c => new
                    {
                        User_UserId = c.Guid(nullable: false),
                        Chat_ChatId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Chat_ChatId })
                .ForeignKey("dbo.User", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Chat", t => t.Chat_ChatId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Chat_ChatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "UserId", "dbo.User");
            DropForeignKey("dbo.UserChat", "Chat_ChatId", "dbo.Chat");
            DropForeignKey("dbo.UserChat", "User_UserId", "dbo.User");
            DropForeignKey("dbo.Message", "ChatId", "dbo.Chat");
            DropIndex("dbo.UserChat", new[] { "Chat_ChatId" });
            DropIndex("dbo.UserChat", new[] { "User_UserId" });
            DropIndex("dbo.Message", new[] { "ChatId" });
            DropIndex("dbo.Message", new[] { "UserId" });
            DropTable("dbo.UserChat");
            DropTable("dbo.File");
            DropTable("dbo.User");
            DropTable("dbo.Message");
            DropTable("dbo.Chat");
        }
    }
}

namespace MIPChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMig : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Message", name: "ChatId", newName: "Chat_ChatId");
            RenameColumn(table: "dbo.Message", name: "AuthorId", newName: "Author_UserId");
            RenameIndex(table: "dbo.Message", name: "IX_AuthorId", newName: "IX_Author_UserId");
            RenameIndex(table: "dbo.Message", name: "IX_ChatId", newName: "IX_Chat_ChatId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Message", name: "IX_Chat_ChatId", newName: "IX_ChatId");
            RenameIndex(table: "dbo.Message", name: "IX_Author_UserId", newName: "IX_AuthorId");
            RenameColumn(table: "dbo.Message", name: "Author_UserId", newName: "AuthorId");
            RenameColumn(table: "dbo.Message", name: "Chat_ChatId", newName: "ChatId");
        }
    }
}

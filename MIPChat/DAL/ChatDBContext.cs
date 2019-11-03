using MIPChat.DAL.Domain;
using MIPChat.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MIPChat.DAL
{
    public class ChatDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatModel> Chats { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public ChatDBContext() : base("ChatDBContext")
        {

        }

    }
}
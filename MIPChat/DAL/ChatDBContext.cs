using MIPChat.DAL.Domain;
using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MIPChat.DAL
{
    public class ChatDBContext: DbContext
    {
        public ChatDBContext(): base("ChatDBContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<FileModel> Files { get; set; }

        public DbSet<MessageModel> Messages { get; set; }
    }
}
using MIPChat.DAL.Repository;
using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIPChat.DAL.UnitOfWork
{
    public class ChatUnitOfWork : IChatUnitOfWork
    {
        private ChatDBContext context = new ChatDBContext();
        private IUsersRepository users;
        public IUsersRepository Users
        {
            get
            {
                return this.users ?? new UserRepository(context);
            }
        }
        
        private IChatRepository chats;
        public IChatRepository Chats
        {
            get
            {
                return this.chats ?? new ChatRepository(context);
            }
        }

        private IMessageRepository messages;
        public IMessageRepository Messages
        {
            get
            {
                return this.messages ?? new MessageRepository(context);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int CommitChanges()
        {
            throw new NotImplementedException();
        }
    }
}

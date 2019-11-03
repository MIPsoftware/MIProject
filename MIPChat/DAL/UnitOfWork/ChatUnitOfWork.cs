using MIPChat.DAL.Repository;
using System;

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

        public void CommitChanges()
        {
            context.SaveChanges();
        }
    }
}

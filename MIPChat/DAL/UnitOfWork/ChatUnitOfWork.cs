using MIPChat.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIPChat.DAL.UnitOfWork
{
    public class ChatUnitOfWork : IChatUnitOfWork
    {
        private readonly ChatDBContext _context;
        private readonly Lazy<IUsersRepository> _users;
        private readonly Lazy<IChatRepository> _chat;
        private readonly Lazy<IMessageRepository> _message;

        public ChatUnitOfWork()
        {
            _context = new ChatDBContext();

            _users = new Lazy<IUsersRepository>(() => new UserRepository(_context));
            _chat = new Lazy<IChatRepository>(() => new ChatRepository(_context));
            _message = new Lazy<IMessageRepository>(() => new MessageRepository(_context));
        }

        public IUsersRepository Users => _users.Value;
        public IChatRepository Chats => _chat.Value;
        public IMessageRepository Messages => _message.Value;

        public int CommitChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
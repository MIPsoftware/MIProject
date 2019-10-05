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

        public ChatUnitOfWork(ChatDBContext context)
        {
            _context = context;

            _users = new Lazy<IUsersRepository>(() => new UserRepository(_context));
            _chat = new Lazy<IChatRepository>(() => new ChatRepository(_context));
        }

        public IUsersRepository Users => _users.Value;

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